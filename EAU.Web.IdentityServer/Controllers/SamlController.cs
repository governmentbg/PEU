using ComponentSpace.Saml2;
using ComponentSpace.Saml2.Authentication;
using ComponentSpace.Saml2.Metadata.Export;
using ComponentSpace.Saml2.Session;
using EAU.Security;
using EAU.ServiceLimits.AspNetCore.Mvc;
using EAU.Users;
using EAU.Users.Models;
using EAU.Web.IdentityServer.Common;
using EAU.Web.IdentityServer.Extensions;
using EAU.Web.IdentityServer.Models;
using EAU.Web.IdentityServer.Security;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace EAU.Web.IdentityServer.Controllers
{
    [AllowAnonymous]
    public class SamlController : Controller
    {
        private readonly IEventService _events;
        private readonly IUsersLoginService _usersLoginService;
        private readonly IUsersService _usersService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer _localizer;
        private readonly Security.ICookieManager _cookieManager;
        private readonly ILogger _logger;
        private readonly IMessageStore<ErrorMessage> _errorMessageStore;
        private readonly IdentityServerOptions _idsrvOptions;
        private readonly ISsoSessionStore _ssoSession;
        private readonly string _eauthIdpUrl;
        private readonly IEAUClaimsHelper _claimsHelper;

        public SamlController(
            IEventService events,
            IUsersLoginService usersLoginService,
            IUsersService usersService,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer localizer,
            Security.ICookieManager cookieManager,
            ISsoSessionStore sessionStore,
            ILogger<SamlController> logger,
            IMessageStore<ErrorMessage> errorMessageStore,
            IdentityServerOptions options,
            IConfiguration configuration,
            IEAUClaimsHelper claimsHelper)
        {
            _events = events;
            _usersLoginService = usersLoginService;
            _usersService = usersService;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _cookieManager = cookieManager;
            _logger = logger;
            _errorMessageStore = errorMessageStore;
            _idsrvOptions = options;
            _ssoSession = sessionStore;
            _eauthIdpUrl = configuration.GetEAUSection().GetValue<string>("GL_EAUTH_BASE_URL").TrimEnd('/');
            _claimsHelper = claimsHelper;
        }

        [HttpGet]
        [ServiceLimiter(ServiceCode = "PEAU_FAILED_LOGIN_LIMIT", DoNotStopRequestProcessing = true)]
        public Task<IActionResult> InitiateSso(bool isLimited, string returnUrl, [FromServices] ISamlServiceProvider samlServiceProvider, [FromServices] ComponentSpace.Saml2.Events.SamlServiceProviderEvents events)
            => InitiateSsoAsync(isLimited, returnUrl, false, false, samlServiceProvider, events);

        [HttpGet]
        [ServiceLimiter(ServiceCode = "PEAU_FAILED_LOGIN_LIMIT", DoNotStopRequestProcessing = true)]
        public Task<IActionResult> RegisterAuthentication(bool isLimited, string returnUrl, [FromServices] ISamlServiceProvider samlServiceProvider, [FromServices] ComponentSpace.Saml2.Events.SamlServiceProviderEvents events)
            => InitiateSsoAsync(isLimited, returnUrl, true, false, samlServiceProvider, events);

        [HttpGet]
        [ServiceLimiter(ServiceCode = "PEAU_FAILED_LOGIN_LIMIT", DoNotStopRequestProcessing = true)]
        public Task<IActionResult> RegisterUserWithAuthentication(bool isLimited, string returnUrl, [FromServices] ISamlServiceProvider samlServiceProvider, [FromServices] ComponentSpace.Saml2.Events.SamlServiceProviderEvents events)
            => InitiateSsoAsync(isLimited, returnUrl, false, true, samlServiceProvider, events);
        
        [HttpGet]
        public async Task<IActionResult> SsoCallback()
        {
            var result = await HttpContext.AuthenticateAsync(IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme);
            var sessData = await _ssoSession.LoadAsync<EAuthSsoData>();

            #region Hanlde protocol athentication error

            string defaultErrMessage = _localizer["GL_USR_00020_E"];

            if (sessData == null)
            {
                _logger.LogWarning($"{nameof(SsoCallback)}: session data object is null!");

                var errorId = await CreateErrorAsync(defaultErrMessage, null);
                return RedirectToAction("Login", "Account", new { errorId });
            }

            await _ssoSession.RemoveAsync<EAuthSsoData>();

            if (result?.Succeeded != true)
            {
                string failureMessage = result.Failure?.Message;
                string errMessage = !string.IsNullOrEmpty(failureMessage) ? failureMessage : defaultErrMessage;

                _logger.LogWarning($"{nameof(SsoCallback)}: authentication result from external authentication failed: {errMessage}");

                var errorId = await CreateErrorAsync(errMessage, sessData.ReturnUrl);
                return RedirectToAction("Login", "Account", new { errorId });
            }

            #endregion

            string returnUrl = sessData.ReturnUrl;

            var userIdClaim = result.Principal?.FindFirst("urn:egov:bg:eauth:2.0:attributes:personIdentifier");

            if (userIdClaim == null)
            {
                _logger.LogWarning($"{nameof(SsoCallback)}: external auth was successful, but returned no personIdentifier claim!");

                var errorId = await CreateErrorAsync(defaultErrMessage, sessData.ReturnUrl);
                return RedirectToAction("Login", "Account", new { errorId });
            }

            if (!_claimsHelper.TryBuildClaim(userIdClaim.Value, EAUClaimTypes.PersonIdentifier, out Claim userIdentifierClaim))
            {
                _logger.LogWarning($"{nameof(SsoCallback)}: cannot extract eau PersonIdentifier from returned personIdentifier: {userIdClaim.Value}");

                var errorId = await CreateErrorAsync(defaultErrMessage, sessData.ReturnUrl);
                return RedirectToAction("Login", "Account", new { errorId });
            }

            if (!_claimsHelper.TryBuildPersonIdentifierTypeClaim(userIdClaim.Value, out Claim userIdentifierTypeClaim))
            {
                _logger.LogWarning($"{nameof(SsoCallback)}: cannot extract eau PersonIdentifierType from returned personIdentifierType: {userIdClaim.Value}");

                var errorId = await CreateErrorAsync(defaultErrMessage, sessData.ReturnUrl);
                return RedirectToAction("Login", "Account", new { errorId });
            }

            var userIdentifierParsed = userIdentifierClaim.Value;

            if (sessData.RegisterAuthentication)
            {
                var currentUserId = int.Parse(HttpContext.EAUUser().ClientID);

                var res = await _usersService.RegisterUserEAuthAuthentication(userIdentifierParsed, currentUserId, _httpContextAccessor.HttpContext.Connection.RemoteIpAddress, default);

                if (!res.IsSuccessfullyCompleted)
                {
                    returnUrl = await CreateErrorAndGetErrorUrl(_localizer["GL_USR_0012_E"], returnUrl);
                }
            }
            else if (sessData.RegisterUserWithAuthentication)
            {
                var resultCheck = await _usersService.CheckEAuthAlreadyAttachedToProfileAsync(userIdentifierParsed, CancellationToken.None);

                if (!resultCheck.IsSuccessfullyCompleted)
                {
                    // special case for NotConfirmed user 
                    if (resultCheck.Result != null && resultCheck.Result.Status == UserStatuses.NotConfirmed)
                    {
                        _logger.LogWarning($"{nameof(SsoCallback)}: user with eauth identifier {userIdentifierParsed} is not confirmed. Redirecting to login page for email resend.");

                        var userErrorId = await CreateNotConfirmedUserErrorAsync(resultCheck.Result.Email, resultCheck.Errors.First().Code, returnUrl);
                        return RedirectToAction("Login", "Account", new { userErrorId });
                    }
                    else
                    {
                        var warnCode = await CreateErrorAsync(resultCheck.Errors.First().Code, returnUrl);
                        return RedirectToAction("Error", "Home", new { warnCode });
                    }
                }
                else
                {
                    await AuthenticateUserRegistrationWithEauth(userIdentifierClaim, userIdentifierTypeClaim);
                }                
            }
            else
            {
                var authRes = await _usersLoginService.AuthenticateEAuthAsync(userIdentifierParsed, _httpContextAccessor.HttpContext.Connection.RemoteIpAddress);

                if (authRes.IsSuccess)
                {
                    // зареждане на допълнителни claims
                    var additionalLocalClaims = new List<Claim>();
                    var localSignInProps = new AuthenticationProperties();

                    additionalLocalClaims.AddRange(authRes.User.GetClaims());

                    if (!string.IsNullOrEmpty(authRes.LoginSessionID))
                        additionalLocalClaims.Add(new Claim(type: EAUClaimTypes.LoginSessionID, value: authRes.LoginSessionID));

                    additionalLocalClaims.Add(new Claim(EAUClaimTypes.UserIdentifiable, bool.TrueString));
                    additionalLocalClaims.Add(userIdentifierClaim);
                    additionalLocalClaims.Add(userIdentifierTypeClaim);

                    string name = additionalLocalClaims.GetName();
                    string subject = ClaimsHelper.BuildSubClaimValueForCIN(additionalLocalClaims.GetCIN().Value);
                    string provider = result.Properties.Items["PartnerName"];

                    await _events.RaiseAsync(new UserLoginSuccessEvent(provider, userIdentifierParsed, subject, name));

                    var isuser = new IdentityServerUser(subject)
                    {
                        DisplayName = name,
                        IdentityProvider = provider,
                        AdditionalClaims = additionalLocalClaims
                    };

                    // издаване на authentication cookie                               
                    await HttpContext.SignInAsync(isuser, localSignInProps);

                    // издаваме is_logged cookie
                    _cookieManager.EnsureIsLoggedCookie();
                }
                else
                {
                    // special case for NotConfirmed user 
                    if (authRes.User?.Status == UserStatuses.NotConfirmed)
                    {
                        _logger.LogWarning($"{nameof(SsoCallback)}: user with eauth identifier {userIdentifierParsed} is not confirmed. Redirecting to login page for email resend.");

                        var userErrorId = await CreateNotConfirmedUserErrorAsync(authRes.User.Email, "GL_USR_EAUTH_ATTACHED_TO_OTHER_NONCONFIRMED_USER_E", returnUrl);
                        return RedirectToAction("Login", "Account", new { userErrorId });
                    }

                    _logger.LogWarning($"{nameof(SsoCallback)}: cannot find user_authentication for identifier {userIdentifierParsed}");

                    string returnReason = ReturnReasons.EAuthNotAttachedToUser;
                    return RedirectToAction("Login", "Account", new { returnUrl, returnReason });
                }
            }

            returnUrl ??= "~/";

            return Redirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Metadata([FromServices] IConfigurationToMetadata configurationToMetadata)
        {
            var entityDescriptor = await configurationToMetadata.ExportAsync();
            var xmlElement = entityDescriptor.ToXml();

            Response.Headers.Add("Cache-Control", "public, max-age=300");

            return new ContentResult
            {
                ContentType = "application/xml;charset=UTF-8",
                Content = xmlElement.OuterXml,
                StatusCode = 200
            };
        }

        private async Task<IActionResult> InitiateSsoAsync(bool isLimited, string returnUrl, 
            bool userIsRegisteringAuthentication, bool userIsRegisteringWithAuthentication,
            ISamlServiceProvider samlServiceProvider, ComponentSpace.Saml2.Events.SamlServiceProviderEvents events)
        {
            if (!isLimited)
            {
                samlServiceProvider.Events = events;

                var sessData = new EAuthSsoData
                {
                    Id = Guid.NewGuid().ToString(),
                    ReturnUrl = returnUrl,
                    RegisterAuthentication = userIsRegisteringAuthentication,
                    RegisterUserWithAuthentication = userIsRegisteringWithAuthentication
                };
                await _ssoSession.SaveAsync(sessData);

                await samlServiceProvider.InitiateSsoAsync(_eauthIdpUrl);
            }
            else
            {
                ErrorViewModel model = new ErrorViewModel() { IsWarning = true, Error = new ErrorMessage() { Error = _localizer["GL_TOO_MANY_REQUESTS_E"], RedirectUri = returnUrl } };
                return View("Error", model);
            }

            return new EmptyResult();
        }

        private Task<string> CreateErrorAsync(string errorMessage, string redirectUri)
        {
            var errorModel = new ErrorMessage
            {
                Error = errorMessage,
                RedirectUri = redirectUri
            };

            var message = new Message<ErrorMessage>(errorModel, DateTime.UtcNow);
            return _errorMessageStore.WriteAsync(message);
        }

        private Task<string> CreateNotConfirmedUserErrorAsync(string email, string errorCode, string redirectUri)
        {
            var errorModel = new ErrorMessage
            {
                Error = $"{email}|{errorCode}",
                RedirectUri = redirectUri
            };

            var message = new Message<ErrorMessage>(errorModel, DateTime.UtcNow);
            return _errorMessageStore.WriteAsync(message);
        }

        private async Task<string> CreateErrorAndGetErrorUrl(string errorMessage, string redirectUri)
        {
            var id = await CreateErrorAsync(errorMessage, redirectUri);

            return $"~{_idsrvOptions.UserInteraction.ErrorUrl}?{_idsrvOptions.UserInteraction.ErrorIdParameter}={id}";
        }

        private async Task<string> CreateErrorCodeAndGetErrorUrl(string errorCode, string redirectUri)
        {
            var id = await CreateErrorAsync(errorCode, redirectUri);

            return $"~{_idsrvOptions.UserInteraction.ErrorUrl}?errorCode={id}";
        }        

        private async Task AuthenticateUserRegistrationWithEauth(Claim userIdentifierClaim, Claim userIdentifierTypeClaim)
        {
            var claims = new List<Claim> { 
                userIdentifierClaim, 
                userIdentifierTypeClaim ,
                new Claim(JwtClaimTypes.AuthenticationMethod, ClaimTypesConstants.AmrEauth),
                new Claim(EAUClaimTypes.UserIdentifiable, bool.TrueString)
            };

            string subject = ClaimsHelper.BuildSubClaimValueForPersonalId(userIdentifierClaim.Value);
            string name = userIdentifierClaim.Value;

            var isuser = new IdentityServerUser(subject)
            {
                DisplayName = name,
                AdditionalClaims = claims.Any() ? claims.ToArray() : null
            };
            // издаване на authentication cookie                               
            await HttpContext.SignInAsync(isuser);

            // издаваме is_logged cookie
            _cookieManager.EnsureIsLoggedCookie();
        }
    }

    public class EAuthSsoData
    {
        public string Id { get; set; }
        public bool RegisterAuthentication { get; set; }
        public bool RegisterUserWithAuthentication { get; set; }
        public string ReturnUrl { get; set; }
    }
}

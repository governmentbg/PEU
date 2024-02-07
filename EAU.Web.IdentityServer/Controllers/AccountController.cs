using ComponentSpace.Saml2.Metadata;
using EAU.Common;
using EAU.Security;
using EAU.ServiceLimits.AspNetCore.Mvc;
using EAU.Signing.BtrustRemoteClient;
using EAU.Users;
using EAU.Web.IdentityServer.Common;
using EAU.Web.IdentityServer.Extensions;
using EAU.Web.IdentityServer.Models;
using EAU.Web.IdentityServer.Security;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.IdentityServer.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуги за управление на акаунти.
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IIdentityServerInteractionService Interaction;
        private readonly IAuthenticationSchemeProvider SchemeProvider;
        private readonly IOptions<AuthenticationOptions> ProvidersOptions;
        private readonly IEventService Events;
        private readonly IStringLocalizer Localizer;
        private readonly Security.ICookieManager CookieManager;
        private readonly IUsersLoginService UsersLoginService;
        private readonly IUsersService UsersService;
        private readonly UsersOptions UserOptions;
        private readonly IdentityServerOptions Options;
        private readonly IdentityServerTLSOptions TLSOptions;
        private readonly IHttpContextAccessor HttpContextAccessor;
        private readonly IMessageStore<ErrorMessage> ErrorMessageStore;
        private readonly ILogger Logger;
        private readonly GlobalOptions GlobalOptions;
        private readonly Uri IdsrvUri;

        private static readonly string CertificateCookieName = ".clientcert";

        public AccountController(
            IIdentityServerInteractionService interaction,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IUsersLoginService usersLoginService,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer localizer,
            Security.ICookieManager cookieManager,
            IdentityServerOptions options,
            IOptionsMonitor<UsersOptions> userOptions,
            IOptionsMonitor<GlobalOptions> globalOptions,
            IOptionsMonitor<IdentityServerTLSOptions> tlsOptions,
            IMessageStore<ErrorMessage> errorMessageStore,
            IOptions<AuthenticationOptions> providersOptions,
            ILogger<AccountController> logger,
            IUsersService usersService)
        {
            Interaction = interaction;
            SchemeProvider = schemeProvider;
            Events = events;

            UsersLoginService = usersLoginService;
            HttpContextAccessor = httpContextAccessor;
            Localizer = localizer;
            CookieManager = cookieManager;
            Options = options;
            UserOptions = userOptions.CurrentValue;
            ErrorMessageStore = errorMessageStore;

            ProvidersOptions = providersOptions;
            Logger = logger;

            GlobalOptions = globalOptions.CurrentValue;
            TLSOptions = tlsOptions.CurrentValue;
            IdsrvUri = new Uri(GlobalOptions.GL_IDSRV_URL);
            UsersService = usersService;
        }

        /// <summary>
        /// Операция за влизане в системата.
        /// </summary>
        /// <param name="returnUrl">Url, на който да се върне потребителят.</param>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl, string errorID, string returnReason, string userErrorId)
        {
            // ако idsrv е конфигурариран само за Windows Auth пренасочваме директно към Challenge
            // но само ако няма грешка преди това
            if (ProvidersOptions.Value.EnableWindowsAuth
            && string.IsNullOrEmpty(errorID)
            && !ProvidersOptions.Value.EnableUsrNamePwdAuth
            && !ProvidersOptions.Value.EnableKEPAuth)
            {
                var winScheme = await GetWindowsAuthSchemeIfEnabled();

                if (winScheme == null)
                    throw new InvalidProgramException("Windows authentication scheme not enabled!");

                return RedirectToAction("Challenge", "External", new { returnUrl, provider = winScheme.Name });
            }

            var context = await Interaction.GetAuthorizationContextAsync(returnUrl);
            var mode = context?.Parameters?.Get("mode");

            if (!string.IsNullOrEmpty(mode))
            {
                if (mode == Http.RegistrationModes.Certificate)
                    return HandleRedirectForCertificateRegistration(returnUrl);
                else if (mode == Http.RegistrationModes.EAuth)
                    return HandleRedirectForEAuthRegistration(returnUrl);
                else
                {
                    Logger.LogWarning("Invalid LoginMode mode = {mode}", mode);
                    return BadRequest();
                }
            }

            var vm = await BuildLoginViewModelAsync(returnUrl);

            if (!string.IsNullOrEmpty(errorID))
            {
                var error = await Interaction.GetErrorContextAsync(errorID);
                if (!string.IsNullOrEmpty(error.Error))
                {
                    ModelState.AddModelError("", error.Error);
                }
            }
            else if (!string.IsNullOrEmpty(returnReason))
            {
                if (ReturnReasons.Items.TryGetValue(returnReason, out string errorCode))
                {
                    ModelState.AddModelError("", Localizer[errorCode]);
                }
                else
                {
                    ModelState.AddModelError("", ReturnReasons.DefaultErrorText);
                }
            }
            else if (!string.IsNullOrEmpty(userErrorId))
            {
                string notConfirmedUserEmail = null, notConfirmedUserErrorCode = null, redirectUriAfter = null;
                await Interaction.GetErrorContextAsync(userErrorId).ContinueWith(r =>
                {
                    var errorValue = r.Result.Error;
                    var errorValues = errorValue.Split('|');

                    notConfirmedUserEmail = errorValues[0];
                    notConfirmedUserErrorCode = errorValues[1];
                    redirectUriAfter = r.Result.RedirectUri;
                });
                return ReturnNotConfirmedUserViewWithResendMail(notConfirmedUserEmail, notConfirmedUserErrorCode, redirectUriAfter);
            }

            ViewData["Title"] = Localizer["GL_PORTAL_ENTRANCE_L"];
            return View(vm);
        }

        /// <summary>
        /// Операция за влизане в системата с КЕП.
        /// </summary>
        /// <param name="returnUrl">Url, на който да се върне потребителят.</param>
        [HttpGet]
        [ServiceLimiter(ServiceCode = "PEAU_FAILED_LOGIN_LIMIT", DoNotStopRequestProcessing = true)]
        public async Task<IActionResult> LoginKEP(bool isLimited, string returnUrl,
            [FromServices] IStringLocalizer localizer, [FromServices] IDataProtectorServiceProvider dataProtector, [FromServices] IEAUClaimsHelper claimsHelper,
            CancellationToken cancellationToken)
        {
            if (!ProvidersOptions.Value.EnableKEPAuth)
                throw new Exception("invalid authentication method");

            bool allowResendProfileRegistrationMail = false;
            string notConfirmedUsername = null;

            if (!isLimited)
            {
                X509Certificate2 clientCertificate = ReadClientCertificateFromCookie(dataProtector);

                if (clientCertificate == null)
                {
                    ViewData["showNotFoundKepInformation"] = true;
                    ViewData["returnUrl"] = returnUrl;
                    // "Неуспешен опит за вход поради липса на регистрирани сертификати или отказан избор за вход със сертификат."
                    // "Ако желаете да влезете отново със сертификат, моля рестартирайте браузъра преди това."
                    ViewData["message"] = string.Join(" ", Localizer["GL_USR_00015_E"].Value, Localizer["GL_USR_LOGIN_OTHER_KEP_I"].Value);
                }
                else
                {
                    var authResult = await UsersLoginService.AuthenticateCertificateAsync(clientCertificate, HttpContextAccessor.HttpContext.Connection.RemoteIpAddress, cancellationToken);

                    if (authResult.IsSuccess)
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.AddRange(authResult.User.GetClaims());
                        claims.Add(new Claim(JwtClaimTypes.AuthenticationMethod, ClaimTypesConstants.AmrCertificate));
                        claims.Add(new Claim(EAUClaimTypes.UserIdentifiable, bool.TrueString));

                        if (claimsHelper.TryBuildPersonIdentifierTypeClaim(clientCertificate, out Claim c1)) claims.Add(c1);
                        if (claimsHelper.TryBuildClaim(clientCertificate, EAUClaimTypes.PersonIdentifier, out Claim personIdentifierClaim)) claims.Add(personIdentifierClaim);
                        if (claimsHelper.TryBuildClaim(clientCertificate, EAUClaimTypes.PersonNames, out Claim personNamesClaim)) claims.Add(personNamesClaim);
                        if (claimsHelper.TryBuildClaim(clientCertificate, EAUClaimTypes.UIC, out Claim uicClaim)) claims.Add(uicClaim);

                        if (!string.IsNullOrEmpty(authResult.LoginSessionID))
                        {
                            claims.Add(new Claim(type: EAUClaimTypes.LoginSessionID, value: authResult.LoginSessionID));
                        }

                        string name = claims.GetName();
                        string subject = ClaimsHelper.BuildSubClaimValueForCIN(claims.GetCIN().Value);

                        var isuser = new IdentityServerUser(subject)
                        {
                            DisplayName = name,
                            AdditionalClaims = claims.ToArray()
                        };

                        await HttpContext.SignInAsync(isuser);

                        CookieManager.EnsureIsLoggedCookie();

                        await Events.RaiseAsync(new UserLoginSuccessEvent(name, subject, name));

                        if (Interaction.IsValidReturnUrl(returnUrl) || Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }

                        return Redirect("~/");
                    }

                    if (authResult.User != null)
                        await Events.RaiseAsync(new UserLoginFailureEvent(authResult.User.GetClaims().GetName(), "invalid credentials"));

                    if (authResult.UserLocked)
                        ModelState.AddModelError("", Localizer["GL_USR_00013_E"]);
                    else if (authResult.CertificateNotEnabled)
                        ModelState.AddModelError("", localizer["GL_USR_00009_E"]);
                    else if (authResult.UserDeactivated)
                        ModelState.AddModelError("", localizer["GL_USR_00016_E"]);
                    else if (authResult.NotConfirmedAccount)
                    {
                        notConfirmedUsername = authResult.User?.Email;
                        allowResendProfileRegistrationMail = true;
                    }                        
                }
            }
            else
            {
                ModelState.AddModelError("", Localizer["GL_TOO_MANY_REQUESTS_E"]);
            }

            var vm = await BuildLoginViewModelAsync(returnUrl, allowResendProfileRegistrationMail);

            // ensure username for ui
            if (string.IsNullOrEmpty(vm.Username) && allowResendProfileRegistrationMail)
            {
                vm.Username = notConfirmedUsername;
            }

            return View("Login", vm);
        }

        /// <summary>
        /// Операция за влизане в системата.
        /// </summary>
        /// <param name="model">Модел с данни за вход.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ServiceLimiter(ServiceCode = "PEAU_FAILED_LOGIN_LIMIT", DoNotStopRequestProcessing = true)]
        public async Task<IActionResult> Login(bool isLimited, LoginInputModel model, [FromServices] IStringLocalizer localizer)
        {
            if (!ProvidersOptions.Value.EnableUsrNamePwdAuth)
                throw new Exception("invalid authentication method");

            bool allowResendProfileRegistrationMail = false;

            if (!isLimited)
            {
                if (ModelState.IsValid)
                {
                    var usernamePrepared = model.Username.Trim();

                    var authResult = await UsersLoginService.AuthenticateAsync(usernamePrepared, model.Password, HttpContextAccessor.HttpContext.Connection.RemoteIpAddress);

                    if (authResult.IsSuccess)
                    {
                        #region Authentication Success

                        var user = authResult.User;

                        List<Claim> claims = new List<Claim>();
                        claims.AddRange(user.GetClaims());

                        if (!string.IsNullOrEmpty(authResult.LoginSessionID))
                        {
                            claims.Add(new Claim(type: EAUClaimTypes.LoginSessionID, value: authResult.LoginSessionID));
                        }

                        string name = claims.GetName();
                        string subject = ClaimsHelper.BuildSubClaimValueForCIN(claims.GetCIN().Value);

                        var isuser = new IdentityServerUser(subject)
                        {
                            DisplayName = name,
                            AdditionalClaims = claims.Any() ? claims.ToArray() : null
                        };

                        await HttpContext.SignInAsync(isuser);

                        CookieManager.EnsureIsLoggedCookie();

                        await Events.RaiseAsync(new UserLoginSuccessEvent(model.Username, subject, name));

                        // само ако е валиден returnUrl пренасочва към authorize endpoint
                        if (Interaction.IsValidReturnUrl(model.ReturnUrl) || Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }

                        return Redirect("~/");

                        #endregion
                    }

                    await Events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));

                    if (authResult.InvalidUsernamePassword)
                    {
                        string textPrepared;
                        if (authResult.UserWasLocked)
                        {
                            textPrepared = System.Text.RegularExpressions.Regex.Replace(Localizer["GL_USR_00019_I"], "<(.*)>", UserOptions.USR_LOCK_FOR_PERIOD.GetTimeTextPresentation());
                        }
                        if (authResult.IsUserProfileMigrated)
                        {
                            textPrepared = Localizer["GL_LOGIN_WITH_MIGRATED_USER_PROFILE_E"];
                        }
                        else
                        {
                            textPrepared = Localizer["GL_USR_INVALID_EMAIL_PASS_E"];
                        }

                        ModelState.AddModelError("", textPrepared);
                    }
                    else if (authResult.UserLocked)
                        ModelState.AddModelError("", Localizer["GL_USR_00013_E"]);
                    else if (authResult.UserDeactivated)
                        ModelState.AddModelError("", localizer["GL_USR_00016_E"]);
                    else if (authResult.NotConfirmedAccount)
                        allowResendProfileRegistrationMail = true;
                }
                else
                {
                    if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
                    {
                        ModelState.ClearValidationState("Username");
                        ModelState.ClearValidationState("Password");
                        ModelState.AddModelError("", Localizer["GL_USR_INVALID_EMAIL_PASS_E"]);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", Localizer["GL_TOO_MANY_REQUESTS_E"]);
            }

            var vm = await BuildLoginViewModelAsync(model, allowResendProfileRegistrationMail);
            return View(vm);
        }

        /// <summary>
        /// Адрес за извличане на клиентски сертфикат.
        /// </summary>
        /// <param name="isLimited">isLimited</param>
        /// <param name="returnUrl">returnUrl</param>
        /// <param name="options">options</param>
        /// <param name="dataProtector">dataProtector</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetCert(bool isLimited, string returnUrl,
            [FromServices] IOptions<GlobalOptions> options, [FromServices] IDataProtectorServiceProvider dataProtector, [FromServices] IHostEnvironment environment)
        {
            if (!ProvidersOptions.Value.EnableKEPAuth)
                throw new Exception("invalid authentication method");

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                Logger.LogInformation($"{nameof(GetCert)}: returnUrl is empty");
                return BadRequest();
            }

            if (false == environment.IsDevelopmentLocal() && false == EnsureUrlIsWithinIdentityServer(returnUrl)) return BadRequest();

            var clientCertificate = HttpContext.Connection.ClientCertificate;

            if (clientCertificate != null)
            {                
                var encryptedData = dataProtector.GetDataProtector().Protect(clientCertificate.RawData);                                
                var s = Convert.ToBase64String(encryptedData);

                Logger.LogInformation("clientCertificate found: " + s);
                Logger.LogInformation($"Wil create cookie {CertificateCookieName} domain: {TLSOptions.GL_IDSRV_CERT_COOKIE_DOMAIN}");

                Response.Cookies.Append(CertificateCookieName, s, new CookieOptions
                {
                    Domain = TLSOptions.GL_IDSRV_CERT_COOKIE_DOMAIN,
                    HttpOnly = true,
                    Secure = true
                });
            }
            else
            {
                Logger.LogInformation("clientCertificate not found!");
            }

            return Redirect(returnUrl);
        }

        [HttpGet]
        //[ServiceRateLimiter(ServiceCode = "FAILED_REGISTRATION_USER_LIMIT", DoNotStopRequestProcessing = true)]
        public async Task<IActionResult> RegistrationKEP(bool isLimited, string returnUrl,
            [FromServices] IDataProtectorServiceProvider dataProtector, CancellationToken cancellationToken)
        {
            if (false == ProvidersOptions.Value.EnableKEPAuth) return BadRequest();

            if (isLimited) return ReturnErrorView("GL_TOO_MANY_REQUESTS_E", returnUrl);

            var clientCertificate = ReadClientCertificateFromCookie(dataProtector);

            if (clientCertificate == null)
            {
                // "Неуспешен опит за вход поради липса на регистрирани сертификати или отказан избор за вход със сертификат."
                // "Ако желаете да влезете отново със сертификат, моля рестартирайте браузъра преди това."

                return ReturnErrorView("GL_USR_LOGIN_OTHER_KEP_I", returnUrl);
            }
            else
            {
                string errorCode = "GL_INTERNAL_SERVER_ERROR_E";
                try
                {
                    var resultCheck = await UsersService.CheckCertificateAlreadyAttachedToProfileAsync(clientCertificate, cancellationToken);

                    if (!resultCheck.IsSuccessfullyCompleted)
                    {
                        if (resultCheck.Result != null && resultCheck.Result.Status == Users.Models.UserStatuses.NotConfirmed)
                        {
                            return ReturnNotConfirmedUserViewWithResendMail(resultCheck.Result.Email, "GL_USR_CERT_ATTACHED_TO_OTHER_NONCONFIRMED_USER_E", returnUrl);
                        }
                        else
                        {
                            string cancelRegKepWithReturnUrl = Url.Action(nameof(CancelRegistrationWithKEP), "Account", new { returnUrl }, IdsrvUri.Scheme, IdsrvUri.Host);
                            return ReturnWarningView(resultCheck.Errors.First().Code, cancelRegKepWithReturnUrl);
                        }                        
                    }

                    await AuthenticateCertificateForRegistrationAsync(clientCertificate);

                    if (Interaction.IsValidReturnUrl(returnUrl) || Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }

                }
                catch (InvalidOperationException iop_ex)
                {
                    errorCode = "GL_USR_LOGIN_PROF_CERT_REQUIRED_E";
                }

                var context = await Interaction.GetAuthorizationContextAsync(returnUrl);

                await Interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                return ReturnErrorView(errorCode, returnUrl);
            }
        }

        /// <summary>
        /// Адрес-входна точка за регистриране на клиентски сертификат.
        /// </summary>
        /// <param name="returnUrl">returnUrl</param>
        /// <param name="userAccessor">userAccessor</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CertificateRegistrationBegin(string returnUrl, [FromServices] IEAUUserAccessor userAccessor)
        {
            if (string.IsNullOrWhiteSpace(returnUrl)) return BadRequest();

            if (!ProvidersOptions.Value.EnableKEPAuth) return BadRequest();

            if (userAccessor.User == null) return Unauthorized();

            string registerKEPReturnUrl = Url.Action(nameof(CertificateRegistrationEnd), "Account", new { returnUrl }, IdsrvUri.Scheme, IdsrvUri.Host);

            var mtlsUri = new Uri(TLSOptions.GL_MTLS_IDSRV_URL);
            string getCertUrl = Url.Action(nameof(GetCert), "Account", new { returnUrl = registerKEPReturnUrl }, mtlsUri.Scheme, mtlsUri.Host);

            return Redirect(getCertUrl);
        }

        /// <summary>
        /// Адрес на който се връща клиентският сертификат при регистрацията му.
        /// </summary>
        /// <param name="returnUrl">returnUrl</param>
        /// <param name="userAccessor">userAccessor</param>
        /// <param name="usersService">usersService</param>
        /// <param name="dataProtector">dataProtector</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CertificateRegistrationEnd(string returnUrl, [FromServices] IEAUUserAccessor userAccessor,
               [FromServices] IUsersService usersService, [FromServices] IDataProtectorServiceProvider dataProtector,
               [FromServices] IEAUClaimsHelper claimsHelper, CancellationToken cancellationToken)
        {
            if (userAccessor.User == null) return Unauthorized();

            X509Certificate2 clientCertificate = ReadClientCertificateFromCookie(dataProtector);

            if (clientCertificate == null)
            {
                ViewData["message"] = Localizer["GL_00025_E"].Value;
                ViewData["returnUrl"] = returnUrl;
                return View("Warning");
            }

            // не може да регистрираме сертификат без персонален идентификатор
            if (!claimsHelper.TryBuildClaim(clientCertificate, EAUClaimTypes.PersonIdentifier, out Claim c1))
            {
                ViewData["message"] = Localizer["GL_00013_E"].Value;
                ViewData["returnUrl"] = returnUrl;
                return View("Warning");
            }

            var res = await usersService.CreateUserCertificateAuthenticationAsync(clientCertificate,
                        int.Parse(userAccessor.User.ClientID), HttpContext.Connection.RemoteIpAddress, false, cancellationToken);

            if (false == res.IsSuccessfullyCompleted)
            {
                ViewData["message"] = Localizer[res.Errors.First().Code].Value;
                ViewData["returnUrl"] = returnUrl;
                return View("Warning");
            }

            return Redirect(returnUrl);
        }

        /// <summary>
        /// Операция за излизане от системата.
        /// </summary>
        /// <param name="logoutId">Идентификатор за излизане от системата.</param>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            var vm = await BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                return await Logout(vm);
            }

            return View(vm);
        }

        /// <summary>
        /// Операция за излизане от системата.
        /// </summary>
        /// <param name="model">Модел, съдържащ данни за излизане от системата.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            var vm = await BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // изтриваме локалното cookie
                await HttpContext.SignOutAsync();

                // изтриваме is_logged cookie
                CookieManager.RemoveIsLoggedCookie();

                // приключваме локалната логин сесия
                await UsersLoginService.LogoutCurrentLoginSessionAsync();

                // raise event
                await Events.RaiseAsync(new UserLogoutSuccessEvent(User.Claims.GetSubject(), User.Claims.GetName()));
            }
           
            // единственият externalprovider - eAuth - не поддържа signout
            //if (vm.TriggerExternalSignout)
            //{
            //    string url = Url.Action("Logout", new { logoutId = vm.LogoutId });

            //    return SignOut(new AuthenticationProperties { RedirectUri = url }, vm.ExternalAuthenticationScheme);
            //}

            return View("Logout", vm);
        }

        /// <summary>
        /// Отказ от вход в системата.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelLogin(LoginInputModel model)
        {
            if (!ProvidersOptions.Value.EnableUsrNamePwdAuth)
                throw new Exception("invalid authentication method");

            var context = await Interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            if (context != null)
            {
                await Interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                return Redirect(model.ReturnUrl);
            }
            else
            {
                return Redirect("~/");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResendRegMail(LoginInputModel model, System.Threading.CancellationToken token, [FromServices] IUsersService usersService, [FromServices] IStringLocalizer localizer)
        {
            if (!ProvidersOptions.Value.EnableUsrNamePwdAuth)
                throw new Exception("invalid authentication method");

            var resendResult = await usersService.ResendConfirmationEmailAsync(model.Username, token);
            ModelState.Clear();

            var vmresend = await BuildLoginViewModelAsync(model);

            if (!resendResult.IsSuccessfullyCompleted)
            {
                ModelState.AddModelError("", localizer["GL_USR_00016_I"]);
            }
            else
            {
                vmresend.SuccessMessage = localizer["GL_SEND_OK_I"];
            }

            return View("Login", vmresend);
        }

        [HttpGet]
        public async Task<IActionResult> CancelRegistrationWithKEP(string returnUrl)
        {
            var context = await Interaction.GetAuthorizationContextAsync(returnUrl);
            if (context != null)
            {
                await Interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                return Redirect(returnUrl);
            }
            else
            {
                return Redirect("~/");
            }
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model, bool allowResendProfileRegistrationMail = false)
        {
            var vm = await BuildLoginViewModelAsync(model.ReturnUrl, allowResendProfileRegistrationMail);
            vm.Username = model.Username;
            return vm;
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl, bool allowResendProfileRegistrationMail = false)
        {
            var context = await Interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
            {
                // показване на external IdP
                return new LoginViewModel
                {
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                    ExternalProviders = new ExternalProvider[] { new ExternalProvider { AuthenticationScheme = context.IdP } },
                    EnableWindowsAuth = ProvidersOptions.Value.EnableWindowsAuth,
                    EnableUsrNamePwdAuth = ProvidersOptions.Value.EnableUsrNamePwdAuth,
                    EnableKEPAuth = ProvidersOptions.Value.EnableKEPAuth,
                    EnableNRAAuth = ProvidersOptions.Value.EnableNRAAuth
                };
            }

            var schemes = await SchemeProvider.GetAllSchemesAsync();
            var winScheme = schemes.SingleOrDefault(s => s.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase));
            var nraScheme = schemes.SingleOrDefault(s => s.Name.Equals(AccountOptions.NRASchemeName, StringComparison.OrdinalIgnoreCase));

            ExternalProvider winProvider = null, nraProvider = null;

            if (winScheme != null)
            {
                winProvider = new ExternalProvider
                {
                    DisplayName = winScheme.DisplayName,
                    AuthenticationScheme = winScheme.Name
                };
            }

            if (nraScheme != null)
            {
                nraProvider = new ExternalProvider
                {
                    DisplayName = nraScheme.DisplayName,
                    AuthenticationScheme = nraScheme.Name
                };
            }

            string loginKEPReturnUrl = ProvidersOptions.Value.EnableKEPAuth ?
                Url.Action(nameof(LoginKEP), "Account", new { returnUrl }, IdsrvUri.Scheme, IdsrvUri.Host) : null;

            return new LoginViewModel
            {
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                WindowsProvider = winProvider,
                NRAProvider = nraProvider,
                AllowResendProfileRegistrationMail = allowResendProfileRegistrationMail,
                EnableWindowsAuth = ProvidersOptions.Value.EnableWindowsAuth,
                EnableUsrNamePwdAuth = ProvidersOptions.Value.EnableUsrNamePwdAuth,
                EnableKEPAuth = ProvidersOptions.Value.EnableKEPAuth,
                EnableNRAAuth = ProvidersOptions.Value.EnableNRAAuth,
                EnableEAuth = ProvidersOptions.Value.EnableEAuth,
                LoginKEPReturnUrl = loginKEPReturnUrl
            };
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            var context = await Interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // automatically sign-out
                vm.ShowLogoutPrompt = false;
            }

            // show the logout prompt
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information
            var logout = await Interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId,
                PostLogoutAutoRedirect = logout != null && !string.IsNullOrWhiteSpace(logout.PostLogoutRedirectUri) && ShouldAutoPostLogoutRedirectFromRequest(logout)
            };

            if (User?.Identity.GetAuthenticationMethods().SingleOrDefault(c => c.Type == JwtClaimTypes.AuthenticationMethod && c.Value == ClaimTypesConstants.AmrCertificate) != null)
            {
                vm.NotifyUserForWindowCloseText = Localizer["GL_USR_LOGIN_OTHER_KEP_I"];
            }
            else if (User?.Identity.GetAuthenticationMethods().SingleOrDefault(c => c.Type == JwtClaimTypes.AuthenticationMethod && c.Value == "external") != null &&
                User?.Identity.GetIdentityProvider() == "Windows")
            {
                vm.NotifyUserForWindowCloseText = Localizer["GL_USR_LOGIN_INFO_I"];
            }

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);

                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // ако няма, създаваме logout context за external IdP
                            vm.LogoutId = await Interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }
            return vm;
        }

        /// <summary>
        /// Ако в заявката за logout има параметри за автоматичен вход след това, се конструира адрес за редирект към логин-а
        /// </summary>
        private string GetLoginRequestForPostLogoutRedirect(LogoutRequest logout)
        {
            string clientId = logout.Parameters?["signinstate_client_id"];
            string redirect_uri = logout.Parameters?["signinstate_redirect_uri"];
            string state = logout.Parameters?["signinstate_state"];
            string nonce = logout.Parameters?["signinstate_nonce"];
            string response_type = logout.Parameters?["signinstate_response_type"];
            string scope = logout.Parameters?["signinstate_scope"];

            // all parameters are required to build post login redirect
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(redirect_uri) || string.IsNullOrEmpty(state) || string.IsNullOrEmpty(nonce) ||
                string.IsNullOrEmpty(response_type) || string.IsNullOrEmpty(scope))
                return null;

            // build callback url
            string returnUrlParam = HttpContext.GetIdentityServerBasePath() + EndpointConstants.AuthorizeCallbackEndpointAddress;

            returnUrlParam = returnUrlParam
                .AddQueryString("client_id", clientId)
                .AddQueryString("redirect_uri", redirect_uri)
                .AddQueryString("response_type", response_type)
                .AddQueryString("scope", scope)
                .AddQueryString("state", state)
                .AddQueryString("nonce", nonce);

            return $"{Url.Action("Login")}?{Options.UserInteraction.LoginReturnUrlParameter}={UrlEncoder.Default.Encode(returnUrlParam)}";
        }

        private bool ShouldAutoPostLogoutRedirectFromRequest(LogoutRequest logout)
        {
            var postlogoutredirectParam = logout.Parameters?["postlogoutregirect"];

            return !string.IsNullOrEmpty(postlogoutredirectParam) && string.Compare(postlogoutredirectParam, bool.TrueString, true) == 0;
        }

        private async Task<AuthenticationScheme> GetWindowsAuthSchemeIfEnabled() =>
            (await SchemeProvider.GetAllSchemesAsync()).Where(s => s.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase)).SingleOrDefault();

        /// <summary>
        /// Изчитане на клиентският сертификат от {CertificateCookieName}, конструиране на сертификата и изчистване на кукито.
        /// </summary>
        /// <param name="dataProtector">dataProtector</param>
        /// <returns></returns>
        private X509Certificate2 ReadClientCertificateFromCookie(IDataProtectorServiceProvider dataProtector)
        {
            X509Certificate2 clientCertificate = default;

            string cookieCert = Request.Cookies[CertificateCookieName];

            if (false == string.IsNullOrWhiteSpace(cookieCert))
            {
                try
                {
                    var encryptedData = Convert.FromBase64String(cookieCert);
                    var certdata = dataProtector.GetDataProtector().Unprotect(encryptedData);

                    clientCertificate = new X509Certificate2(certdata);

                    //clear cookie
                    Response.Cookies.Append(CertificateCookieName, string.Empty, new CookieOptions
                    {
                        Domain = TLSOptions.GL_IDSRV_CERT_COOKIE_DOMAIN,
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTimeOffset.FromUnixTimeSeconds(0)
                    });
                }
                catch (Exception ex)
                {
                    Logger.LogWarning($"Cannot construct certificate from cookie: {cookieCert}, error: {ex.Message}\r\n{ex.ToString()}");
                }
            }

            return clientCertificate;
        }

        private bool EnsureUrlIsWithinIdentityServer(string url)
        {
            var checkUri = new Uri(url);

            if (string.Compare(checkUri.Host, IdsrvUri.Host, true) != 0)
            {
                Logger.LogInformation($"Ensure local uri failed: {checkUri}");
                return false;
            }

            return true;
        }

        private IActionResult HandleRedirectForCertificateRegistration(string returnUrl)
        {
            if (false == ProvidersOptions.Value.EnableKEPAuth || string.IsNullOrEmpty(TLSOptions.GL_MTLS_IDSRV_URL))
                return BadRequest();

            var _idSrvMTLSUri = new Uri(TLSOptions.GL_MTLS_IDSRV_URL);

            var getCertReturnUrl = Url.Action(nameof(RegistrationKEP), "Account", new { returnUrl }, Request.IsHttps ? "https" : "http", Request.Host.ToUriComponent());

            /*използва се GetComponents(UriComponents.Host | UriComponents.Port, UriFormat.Unescaped) вместо HostAndPort, 
             * защото при изпозлване на порта по подразбиране за схемата - https - 443 той също се връща, а не е нужно и има проблеми с F5.
             */
            string getCertUrl = Url.Action(nameof(GetCert), "Account", new { returnUrl = getCertReturnUrl }, _idSrvMTLSUri.Scheme, _idSrvMTLSUri.GetComponents(UriComponents.Host | UriComponents.Port, UriFormat.Unescaped));

            return Redirect(getCertUrl);
        }

        private IActionResult HandleRedirectForEAuthRegistration(string returnUrl)
        {
            if (false == ProvidersOptions.Value.EnableEAuth)
                return BadRequest();

            string redirectToEAuthUrl = Url.Action("RegisterUserWithAuthentication", "Saml", new { returnUrl = returnUrl });

            return Redirect(redirectToEAuthUrl);
        }

        private async Task AuthenticateCertificateForRegistrationAsync(X509Certificate2 x509Certificate)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.AuthenticationMethod, ClaimTypesConstants.AmrCertificate),
                new Claim(EAUClaimTypes.UserIdentifiable, bool.TrueString)
            };
            
            var certificateBase64 = Convert.ToBase64String(x509Certificate.RawData);
            claims.Add(new Claim(EAUClaimTypes.Certificate, certificateBase64));

            string subject = ClaimsHelper.BuildSubClaimValueForCertThumbprint(x509Certificate.Thumbprint);
            string name = x509Certificate.Thumbprint;

            var isuser = new IdentityServerUser(subject)
            {
                DisplayName = name,
                AdditionalClaims = claims.Any() ? claims.ToArray() : null
            };
            await HttpContext.SignInAsync(isuser);

            CookieManager.EnsureIsLoggedCookie();
        }

        private IActionResult ReturnErrorView(string errorCode, string? returnUrl)
        {
            var model = new ErrorViewModel() { IsWarning = true, Error = new ErrorMessage() { Error = Localizer[errorCode], RedirectUri = returnUrl } };
            return View("Error", model);
        }

        private IActionResult ReturnWarningView(string errorCode, string? returnUrl)
        {
            ViewData["message"] = Localizer[errorCode].ToString();
            ViewData["returnUrl"] = returnUrl;

            return View("Warning");
        }

        private IActionResult ReturnNotConfirmedUserViewWithResendMail(string userEmail, string errorCode, string? returnUrl)
        {            
            var model = new NotConfirmedUserMessageModel { Username = userEmail, Error = Localizer[errorCode], ReturnUrl = returnUrl };
            return View("NotConfirmedUser", model);
        }
    }
}

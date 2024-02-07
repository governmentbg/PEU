using EAU.Security;
using EAU.ServiceLimits.AspNetCore;
using EAU.ServiceLimits.AspNetCore.Mvc;
using EAU.Signing.BtrustRemoteClient;
using EAU.Users;
using EAU.Users.Models;
using EAU.Web.Filters;
using EAU.Web.Mvc;
using EAU.Web.Portal.App.Models;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace EAU.Web.Portal.App.Controllers
{
    [Produces("application/json")]
    public class UsersController : BaseApiController
    {
        #region Fields

        private readonly IUsersService _usersService;

        #endregion

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        /// <summary>
        /// Операция за регистриране на потребител.
        /// </summary>
        /// <param name="model">Входящи данни за регистрация на потребител.</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(UserRegistrationResult), StatusCodes.Status200OK)]
        [ServiceLimiter(ServiceCode = "PEAU_REGISTRATION_LIMIT")]
        public async Task<IActionResult> Register([FromBody] RegisterInputModel model, [FromServices] ILogger<UsersController> logger, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var resultx = await HttpContext.AuthenticateAsync(PortalAppAuthenticationDefaults.UserRegistrationAuthScheme);

                if (!resultx.Succeeded)
                {
                    logger.LogWarning($"Registration request is not valid for {PortalAppAuthenticationDefaults.UserRegistrationAuthScheme} scheme.");
                    return OperationResult(new CNSys.OperationResult("GL_USR_MISSING_QUALIFIED_AUTH_E", "GL_USR_MISSING_QUALIFIED_AUTH_E"));
                }
                    
                var authMethodClaim = resultx.Principal.Claims.SingleOrDefault(c => c.Type == JwtClaimTypes.AuthenticationMethod);

                Claim? certificateValue = null, personalIdentClaim = null;
                if (authMethodClaim.Value == "cert")
                {
                    certificateValue = resultx.Principal.Claims.SingleOrDefault(c => c.Type == EAUClaimTypes.Certificate);

                    if (string.IsNullOrEmpty(certificateValue?.Value))
                    {
                        logger.LogWarning($"Registration request is valid, but no certificateValue claim is found in cookie.");
                        return OperationResult(new CNSys.OperationResult("GL_USR_MISSING_QUALIFIED_AUTH_E", "GL_USR_MISSING_QUALIFIED_AUTH_E"));
                    }                        
                }
                else if (authMethodClaim.Value == "eauth")
                {
                    personalIdentClaim = resultx.Principal.Claims.SingleOrDefault(c => c.Type == EAUClaimTypes.PersonIdentifier);

                    if (string.IsNullOrEmpty(personalIdentClaim?.Value))
                    {
                        logger.LogWarning($"Registration request is valid, but no PersonIdentifier claim is found in cookie.");
                        return OperationResult(new CNSys.OperationResult("GL_USR_MISSING_QUALIFIED_AUTH_E", "GL_USR_MISSING_QUALIFIED_AUTH_E"));
                    }                        
                }

                X509Certificate2? clientCert = null;

                try
                {
                    if (certificateValue != null)
                        clientCert = new X509Certificate2(Convert.FromBase64String(certificateValue.Value));
                }
                catch (Exception ex)
                {
                    logger.LogException(ex);
                    return OperationResult(new CNSys.OperationResult("GL_USR_MISSING_QUALIFIED_AUTH_E", "GL_USR_MISSING_QUALIFIED_AUTH_E"));
                }

                var result = await _usersService.BeginPublicUserRegistrationAsync(model.Email, model.Password, clientCert, personalIdentClaim?.Value, HttpContext.Connection.RemoteIpAddress, cancellationToken);

                if (result.Result != null && result.Result.EmailAlreadyExists && result.Result.EmailUserStillNotActivated)
                {
                    return Ok(result.Result);
                }
                else
                {
                    return OperationResult(result);
                }
            }

            return BadRequest();
        }


        /// <summary>
        /// Операция за изпращане на нов линк за активиране на регистрация.
        /// </summary>
        /// <param name="processGuid">Идентификатор на процеса на потребителя</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("RenewUserRegistration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceLimiter(ServiceCode = "PEAU_REGISTRATION_LIMIT")]
        public async Task<IActionResult> RenewUserRegistration([FromBody] Guid processGuid, CancellationToken cancellationToken)
        {
            if (processGuid != null)
            {
                var result = await _usersService.RenewPublicUserRegistrationAsync(processGuid, cancellationToken);

                return OperationResult(result);

            }

            return BadRequest();
        }

        /// <summary>
        /// Операция за потвърждаване на регистрация на потребител.
        /// </summary>
        /// <param name="processId">Guid на процес за завършване на регистрация</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CompleteRegistration/{processId}")]
        [ProducesResponseType(typeof(UserConfirmRegistrationResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> CompleteRegistration([FromRoute] Guid processId, CancellationToken cancellationToken)
        {
            if (processId != null)
            {
                var result = await _usersService.CompletePublicUserRegistrationAsync(processId, cancellationToken);

                if (!result.IsSuccessfullyCompleted && result.Errors.HasErrors && result.Errors.Any(x => x.Code == "GL_USR_0004_E"))
                    return Ok(result.Result);
                else
                    return OperationResult(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Операция за отказване на регистрация на потребител.
        /// </summary>
        /// <param name="processId">Guid на процес за отказване на регистрация</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CancelRegistration/{processId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelRegistration([FromRoute] Guid processId, CancellationToken cancellationToken)
        {
            if (processId != null)
            {
                var result = await _usersService.CancelPublicUserRegistrationAsync(processId, cancellationToken);

                return OperationResult(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Операция за получаване на нова парола
        /// </summary>
        /// <param name="email">Адрес на електронна поща на потребител</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("ResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ServiceLimiter(ServiceCode = "PEAU_PASS_LIMIT")]
        public async Task<IActionResult> ResetPassword([FromBody] string email, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var result = await _usersService.SendForgottenPasswordAsync(email, cancellationToken);

                return OperationResult(result);
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("ResendConfirmationEmail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResendConfirmationEmail([FromBody] string email, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                var result = await _usersService.ResendConfirmationEmailAsync(email, cancellationToken);

                return OperationResult(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Операция за изпращане на нов линк за смяна на забравена парола.
        /// </summary>
        /// <param name="processGuid">Идентификатор на процеса на потребителя</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("RenewResetPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RenewResetPassword([FromBody] Guid processGuid, CancellationToken cancellationToken)
        {
            if (processGuid != null)
            {
                var result = await _usersService.RenewForgottenPasswordAsync(processGuid, cancellationToken);

                return OperationResult(result);
            }

            return BadRequest();
        }


        /// <summary>
        /// Операция за получаване на нова парола
        /// </summary>
        /// <param name="model">Данни за генериране на нова парола.</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [HttpPut]
        [Route("CompleteForgottenPassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CompleteForgottenPassword([FromBody] CompleteForgottenPasswordModel model, CancellationToken cancellationToken)
        {
            if (model.ProcessId != null && !string.IsNullOrWhiteSpace(model.NewPassword))
            {
                var result = await _usersService.CompleteForgottenPasswordAsync(model.ProcessId, model.NewPassword, cancellationToken);

                return OperationResult(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Операция за смяна на потребителска парола.
        /// </summary>
        /// <param name="model">Входящи данни за смяна на потребителска парола.</param>
        /// <param name="userAccessor">Интерфейс за достъп до EAU потребител.</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("ChangePassword")]
        [ProducesResponseType(typeof(ChangePasswordInputModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordInputModel model, [FromServices] IEAUUserAccessor userAccessor, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(model.Email) && !string.IsNullOrWhiteSpace(model.CurrentPassword) && !string.IsNullOrWhiteSpace(model.Password))
            {
                var result = await _usersService.ChangePasswordAsync(userAccessor.User.CIN, model.Email, model.CurrentPassword, model.Password, cancellationToken);

                return OperationResult(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Операция за редактиране на потребителски профил.
        /// </summary>
        /// <param name="email">Електронен адрес на потребителя.</param>
        /// <param name="userAccessor">Интерфейс за достъп до EAU потребител.</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("UpdateProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateUserProfile([FromServices] IEAUUserAccessor userAccessor, [FromBody] string email, CancellationToken cancellationToken)
        {
            var userCIN = userAccessor.User.CIN;

            if (!string.IsNullOrWhiteSpace(email) && userCIN.HasValue)
            {
                var result = await _usersService.EditPublicUserAsync(userCIN.Value, email, cancellationToken);

                return OperationResult(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Деактивиране на потребителски профил.
        /// </summary>
        /// <param name="userAccessor">Интерфейс за достъп до EAU потребител.</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut]
        [Route("DeactivateUserProfile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeactivateUserProfile([FromServices] IEAUUserAccessor userAccessor, CancellationToken cancellationToken)
        {
            var userCIN = userAccessor.User.CIN;

            if (userCIN.HasValue)
            {
                var result = await _usersService.DeactivatePublicUserAsync(userCIN.Value, cancellationToken);

                return OperationResult(result);
            }

            return BadRequest();
        }

        /// <summary>
        /// Връща всички средства за автентикация на потребителя различни от потребителско име и парола.
        /// </summary>
        /// <param name="userAccessor">Интерфейс за достъп до EAU потребител.</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("UsersAuthTypes")]
        [ProducesResponseType(typeof(IEnumerable<UserAuthentication>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UsersAuthTypes([FromServices] IEAUUserAccessor userAccessor, CancellationToken cancellationToken)
        {
            var userCIN = userAccessor.User.CIN;

            if (userCIN.HasValue)
            {
                var userAuthentications = await _usersService.GetUsersAuthenticationTypesAsync(userCIN.Value, cancellationToken);

                return Ok(userAuthentications);
            }

            return BadRequest();
        }

        /// <summary>
        /// Изтрива сертификат като средство за автентикация на конкретния потребител.
        /// </summary>
        /// <param name="userAccessor">Интерфейс за достъп до EAU потребител.</param>
        /// <param name="userAuthenticationId">Идентификатор на средство за автентикация.</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("DeleteUserAuthentication/{userAuthenticationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUserAuthentication([FromServices] IEAUUserAccessor userAccessor, int userAuthenticationId, CancellationToken cancellationToken)
        {
            var userCIN = userAccessor.User.CIN;

            if (userCIN.HasValue)
            {
                var result = await _usersService.DeleteUserAuthenticationTypeAsync(userCIN.Value, userAuthenticationId, cancellationToken);

                if (result.IsSuccessfullyCompleted)
                    return Ok();
            }

            return BadRequest();
        }

        [Authorize]
        [HttpGet]
        [Route("Certificates")]
        [ProducesResponseType(typeof(IEnumerable<UserAuthenticationInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserCertificateAuthentications([FromServices] IEAUUserAccessor userAccessor, CancellationToken cancellationToken)
        {
            var userCertificates = await _usersService.GetUserAuthentications(userAccessor.User.LocalClientID.Value, AuthenticationTypes.Certificate, cancellationToken);
            return Ok(userCertificates);
        }

        [Authorize]
        [HttpGet]
        [Route("EAuthentications")]
        [ProducesResponseType(typeof(IEnumerable<UserAuthenticationInfo>), StatusCodes.Status200OK)]
        public async Task<IActionResult> UserEAuthAuthentications([FromServices] IEAUUserAccessor userAccessor, CancellationToken cancellationToken)
        {
            var userCertificates = await _usersService.GetUserAuthentications(userAccessor.User.LocalClientID.Value, AuthenticationTypes.EAuth, cancellationToken);
            return Ok(userCertificates);
        }

        [Authorize]
        [HttpPost]
        [Route("login")]
        public IActionResult Login()
        {
            return NoContent();
        }

        private readonly string[] limits = { "BASE_DATA_SERVICE_LIMIT" };

        [HttpPost]
        [Route("Logout")]
        [NoopServiceLimiter]
        public async Task<IActionResult> Logout([FromForm] string redirectUri, [FromServices] IServiceLimiter limiter, [FromServices] IEAUUserAccessor userAccessor)
        {
            if (await limiter.ShouldRateLimitAsync(limits, userAccessor.User?.CIN.HasValue == true ? userAccessor.User.CIN : null, userAccessor.RemoteIpAddress))
            {
                return Redirect($"~/TooManyRequests/");
            }

            var authProps = new AuthenticationProperties();

            if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                authProps.RedirectUri = redirectUri;
                authProps.SetParameter("postlogoutregirect", "true");
            }

            return SignOut(authProps,
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpGet]
        [Route("KeepSessionAlive")]
        public IActionResult KeepSessionAlive()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("current")]
        public IActionResult CurrentUserInfo([FromServices] IEAUUserAccessor userAccessor)
        {
            var currentPrincipal = userAccessor.User;
            var email = currentPrincipal.Claims.GetEmail();

            var idp = currentPrincipal.FindFirst(IdentityModel.JwtClaimTypes.IdentityProvider)?.Value;
            var amr = currentPrincipal.FindFirst(IdentityModel.JwtClaimTypes.AuthenticationMethod)?.Value;

            return Ok(new
            {
                Email = email,
                currentPrincipal.CIN,
                currentPrincipal.IsUserIdentifiable,
                idp,
                amr,
                currentPrincipal.UIC
            });
        }

        [HttpGet]
        [Route("{processGuid}/IsActiveLink")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserProcess(Guid processGuid, CancellationToken cancellationToken)
        {
            var userProcess = await _usersService.GetUserProcess(processGuid, cancellationToken);

            if (userProcess != null && userProcess.InvalidAfter.HasValue && userProcess.InvalidAfter.Value > DateTime.Now)
                return Ok(true);

            return Ok(false);
        }

        [HttpPost]
        [Route("registrationByCertBegin")]
        public IActionResult UserRegistrationCertBegin()
        {
            var authProps = new AuthenticationProperties();
            authProps.SetParameter("mode", EAU.Web.Http.RegistrationModes.Certificate);
            authProps.SetParameter("forceRedirectUri", "true");
            authProps.RedirectUri = "/users/registration";

            return Challenge(authProps, PortalAppAuthenticationDefaults.UserRegistrationAuthScheme);
        }

        [HttpPost]
        [Route("registrationByEAuthBegin")]
        public async Task<IActionResult> UserRegistrationEAuthBegin()
        {
            var result = await HttpContext.AuthenticateAsync(PortalAppAuthenticationDefaults.UserRegistrationAuthScheme);
            if (!result.Succeeded)
            {
                var authProps = new AuthenticationProperties();
                authProps.SetParameter("mode", EAU.Web.Http.RegistrationModes.EAuth);
                authProps.SetParameter("forceRedirectUri", "true");
                authProps.RedirectUri = "/users/registration";

                return Challenge(authProps, PortalAppAuthenticationDefaults.UserRegistrationAuthScheme);
            }

            return Ok();
        }

        [HttpPost]
        [Route("registrationFormEnd")]
        public IActionResult FinishRegistrationWork([FromQuery] string email)
        {
            return SignoutFromRegistrationScheme($"/users/registration-form-complete?email={HttpUtility.UrlEncode(email)}");
        }

        [HttpPost]
        [Route("registrationCancel")]
        public IActionResult CancelRegistrationWork()
        {
            return SignoutFromRegistrationScheme("/", false, "cancelRegistration");
        }

        [HttpGet]
        [Route("registrationData")]
        public async Task<IActionResult> UserRegistrationData()
        {
            var result = await HttpContext.AuthenticateAsync(PortalAppAuthenticationDefaults.UserRegistrationAuthScheme);

            if (!result.Succeeded)
            {
                return Unauthorized();
            }

            var subject = result.Principal.Claims.GetSubject();

            string email = null;
            var certClaim = result.Principal.Claims.GetCertificateClaim();
            if (!string.IsNullOrEmpty(certClaim))
            {
                email = ExtractAndParseEmailFromCertClaim(certClaim);
            }

            return Ok(new { subject, email });
        }

        private IActionResult SignoutFromRegistrationScheme(string? redirectUri = null, bool? postLogoutRegirect = true, string? signoutReason = null)
        {
            var authProps = new AuthenticationProperties();

            if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                authProps.RedirectUri = redirectUri;
            }

            if (postLogoutRegirect.GetValueOrDefault())
            {
                authProps.SetParameter("postlogoutregirect", "true");
            }

            if (!string.IsNullOrWhiteSpace(signoutReason))
            {
                authProps.SetParameter("logoutReason", signoutReason);
            }

            return SignOut(authProps,
                PortalAppAuthenticationDefaults.UserRegistrationAuthScheme,
                PortalAppAuthenticationDefaults.UserRegistrationChallengeScheme);
        }

        private string ExtractAndParseEmailFromCertClaim(string certClaim)
        {
            try
            {
                var certBytes = Convert.FromBase64String(certClaim);
                var cert = new X509Certificate2(certBytes);

                foreach (var subjectPart in cert.Subject.Split(','))
                {
                    var data = subjectPart.Trim();

                    if (!string.IsNullOrEmpty(data))
                    {
                        var m = Regex.Match(data, "E\\s*=\\s*(?<value>[A-Za-z0-9\\s.@].*)");

                        if (m.Success)
                        {
                            Group g = m.Groups["value"];
                            var emailValue = g.Value;

                            if (!string.IsNullOrEmpty(emailValue) && ValidateEmail(emailValue))
                                return emailValue;
                        }
                    }
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static bool ValidateEmail(string email)
        {
            return Regex.Match(email, "(?=^.{1,64}@)^[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])(;(?=.{1,64}@)[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9]))*$", RegexOptions.IgnoreCase).Success;
        }
    }
}
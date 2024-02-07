using ComponentSpace.Saml2.Session;
using EAU.Web.IdentityServer.Common;
using EAU.Web.IdentityServer.Models;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace EAU.Web.IdentityServer.Controllers
{
    /// <summary>
    /// Начален контролер на приложението.
    /// </summary>
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        /// <summary>
        /// Операция за зареждане на начална страница.
        /// </summary>
        /// <returns>Начална страница.</returns>
        public IActionResult Index([FromServices] IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                return View();
            }

            return NotFound();
        }

        /// <summary>
        /// Операция за зареждане на страница за грешка.
        /// </summary>
        /// <param name="errorId">Идентификатор на грешка.</param>
        /// <returns>Страница за грешка.</returns>
        public async Task<IActionResult> Error(string errorId, string? errorCode, string? warnCode, [FromServices] IIdentityServerInteractionService interactionService, [FromServices] IStringLocalizer localizer)
        {
            ErrorViewModel model = null;

            if (!string.IsNullOrEmpty(errorId))
            {
                await interactionService.GetErrorContextAsync(errorId).ContinueWith(r =>
                {
                    model = new ErrorViewModel() { Error = r.Result };
                });
            }
            else if (!string.IsNullOrEmpty(errorCode))
            {
                await interactionService.GetErrorContextAsync(errorCode).ContinueWith(r =>
                {
                    var errorText = localizer[r.Result.Error].Value;
                    var errorMessage = new ErrorMessage { Error = errorText, RedirectUri = r.Result.RedirectUri };

                    model = new ErrorViewModel() { Error = errorMessage };
                });
            }
            else if (!string.IsNullOrEmpty(warnCode))
            {
                await interactionService.GetErrorContextAsync(warnCode).ContinueWith(r =>
                {
                    var errorText = localizer[r.Result.Error].Value;
                    var errorMessage = new ErrorMessage { Error = errorText, RedirectUri = r.Result.RedirectUri };

                    model = new ErrorViewModel() { Error = errorMessage, IsWarning = true };
                });
            }

            return View("Error", model);
        }

        /// <summary>
        /// Операция за зареждане на страница за грешка от приложението.
        /// </summary>
        /// <returns>Страница за грешка.</returns>
        public async Task<IActionResult> AppError([FromServices] IStringLocalizer localizer, [FromServices] ISsoSessionStore _ssoSession, [FromServices] IMessageStore<ErrorMessage> errorMessageStore)
        {
            ErrorViewModel model = null;
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature.Error != null)
            {
                model = new ErrorViewModel();

                if (exceptionHandlerPathFeature.Error.GetType().Namespace.StartsWith("ComponentSpace.Saml2"))
                {
                    var sessData = await _ssoSession.LoadAsync<EAuthSsoData>();

                    var samlDefaultErrorMsg = localizer["GL_USR_00020_E"].Value;
                    var errorId = await CreateErrorAsync(samlDefaultErrorMsg, sessData?.ReturnUrl, errorMessageStore);

                    return RedirectToAction("Login", "Account", new { errorId });
                }
            }

            return View("Error", model);
        }

        /// <summary>
        /// Операция за зареждане на страница за грешка по даден код на статус.
        /// </summary>
        /// <param name="statusCode">Код на статус.</param>
        /// <returns>Страница за грешка.</returns>
        [Route("/Home/ErrorStatusCodePage/{statusCode}")]
        public IActionResult ErrorStatusCodePage(string statusCode, [FromServices] IStringLocalizer localizer)
        {
            var model = new ErrorViewModel();
            model.Error = new IdentityServer4.Models.ErrorMessage();

            if (statusCode == Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized.ToString())
            {
                model.Error.Error = localizer["GL_NO_RESOURCE_ACCESS_E"].Value;
            }
            else
                model.Error.Error = statusCode;

            return View("Error", model);
        }

        private async Task<string> CreateErrorAsync(string errorMessage, string redirectUri, IMessageStore<ErrorMessage> _errorMessageStore)
        {
            var errorModel = new ErrorMessage
            {
                Error = errorMessage,
                RedirectUri = redirectUri
            };

            var message = new Message<ErrorMessage>(errorModel, System.DateTime.UtcNow);
            return await _errorMessageStore.WriteAsync(message);
        }
    }
}


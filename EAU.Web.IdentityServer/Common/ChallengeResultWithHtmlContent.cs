using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EАU.Web.IdentityServer.Common
{
    /// <summary>
    /// Наследник на ChallengeResult, който добавя и Html съдържание към response.
    /// </summary>
    public class ChallengeResultWithHtmlContent : ChallengeResult
    {
        public Controller Controller { get; private set; }

        public string ViewName { get; private set; }

        public ChallengeResultWithHtmlContent() : base() { }

        public ChallengeResultWithHtmlContent(string authenticationScheme, Controller controller, string viewName) : base(authenticationScheme)
        {
            Controller = controller;
            ViewName = viewName;
        }

        public ChallengeResultWithHtmlContent(IList<string> authenticationSchemes) : base(authenticationSchemes) { }

        public ChallengeResultWithHtmlContent(AuthenticationProperties properties) : base(properties) { }

        public ChallengeResultWithHtmlContent(string authenticationScheme, AuthenticationProperties properties) : base(authenticationScheme, properties) { }

        public ChallengeResultWithHtmlContent(IList<string> authenticationSchemes, AuthenticationProperties properties) : base(authenticationSchemes, properties) { }

        public override async Task ExecuteResultAsync(ActionContext context)
        {
            await base.ExecuteResultAsync(context);

            var html = await Controller.RenderViewAsHtmlContentAsync(ViewName, null);

            await context.HttpContext.Response.WriteHtmlAsync(html);
        }
    }
}

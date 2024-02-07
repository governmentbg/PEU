using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace EAU.Web.IdentityServer.Security
{
    public class NRAAUthOptions : AuthenticationSchemeOptions
    {
        public string IssueTokenEndpoint { get; set; }
        public CookieBuilder AuthenticationPropertiesCookie { get; set; }
        public TokenValidationParameters TokenValidationParameters { get; set; }
        public string EPZEUParticipantId { get; set; }
    }

    /// <summary>
    /// Реализация на AuthenticationHandler за автентификация с ПИК на НАП.
    /// </summary>
    public class NRAAuthenticationHandler : AuthenticationHandler<NRAAUthOptions>
    {
        private readonly ISecureDataFormat<AuthenticationProperties> SecureDataFormat;
        private readonly IStringLocalizer Localizer;

        public NRAAuthenticationHandler(
            IStringLocalizer localizer,
            ISecureDataFormat<AuthenticationProperties> secureDataFormat,
            IOptionsMonitor<NRAAUthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            Localizer = localizer;
            SecureDataFormat = secureDataFormat;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var jwt = Context.Request.Query["jwt"];

            if (string.IsNullOrEmpty(jwt))
                return Task.FromResult(AuthenticateResult.NoResult());

            var principal = new JwtSecurityTokenHandler().ValidateToken(jwt, Options.TokenValidationParameters, out SecurityToken token);

            AuthenticationProperties properties = null;

            if (Request.Cookies.TryGetValue(Options.AuthenticationPropertiesCookie.Name, out string propsCookie))
            {
                properties = SecureDataFormat.Unprotect(propsCookie);
            }

            if (properties == null)
            {
                // props cookie either expired or badly decoded
                return Task.FromResult(AuthenticateResult.Fail(Localizer["GL_EXPIRED_SESSION_ENTRANCE_PIC_NRA_E"].Value));
            }

            // remote props cookie
            var emptyPropCookieOpts = Options.AuthenticationPropertiesCookie?.Build(Context);
            emptyPropCookieOpts.Expires = DateTimeOffset.FromUnixTimeSeconds(0);
            Response.Cookies.Append(Options.AuthenticationPropertiesCookie.Name, string.Empty, emptyPropCookieOpts);

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, properties, Scheme.Name)));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            var propertiesCookie = SecureDataFormat.Protect(properties);

            Response.Cookies.Append(Options.AuthenticationPropertiesCookie.Name, propertiesCookie, Options.AuthenticationPropertiesCookie?.Build(Context));

            Response.Redirect(BuildIssuerRedirectUrl());

            return Task.CompletedTask;
        }

        private string BuildIssuerRedirectUrl() => Options.IssueTokenEndpoint;
    }
}

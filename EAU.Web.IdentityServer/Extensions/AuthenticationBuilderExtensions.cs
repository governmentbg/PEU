using EAU.Web.IdentityServer;
using EAU.Web.IdentityServer.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Microsoft.AspNetCore.Authentication
{ 
    public static class AuthenticationBuilderExtensions
    {
        /// <summary>
        /// Добавя автентификация чрез ПИК на НАП.
        /// </summary>
        public static AuthenticationBuilder AddNRAAuthentication(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            var nraConfig = configuration.GetSection("NRA");

            string tokenEndpointUrl = configuration.GetEAUSection().GetValue<string>("GL_INTGR_NRA_API");
            string signingKey = nraConfig.GetValue<string>("SigningKey");
            string issuerName = nraConfig.GetValue<string>("IssuerName") ?? "NRA";
            string participantId = nraConfig.GetValue<string>("ParticipantId") ?? "eau";

            TimeSpan nraSessionDuration = (bool)nraConfig.GetSection("SessionDuration")?.Exists() ?
                nraConfig.GetValue<TimeSpan>("SessionDuration") : new TimeSpan(0, 5, 0);

            return builder.AddScheme<NRAAUthOptions, NRAAuthenticationHandler>(AccountOptions.NRASchemeName, options =>
            {
                options.IssueTokenEndpoint = tokenEndpointUrl;
                options.EPZEUParticipantId = participantId;
                options.AuthenticationPropertiesCookie = new CookieBuilder()
                {
                    Name = "nra.session",
                    Expiration = nraSessionDuration,
                    HttpOnly = true
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = issuerName,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(signingKey))
                };
            });
        }


        /// <summary>
        /// Добавя автентификация чрез Е-Авт
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static AuthenticationBuilder AddSamlAutenticationForEAuth(this AuthenticationBuilder builder, IConfiguration configuration)
        {            
            return builder.AddSaml(configuration.GetEAUSection().GetValue<string>("GL_EAUTH_BASE_URL"), options =>
            {
                options.SignInScheme = IdentityServer4.IdentityServerConstants.ExternalCookieAuthenticationScheme;
                options.PartnerName = (ctx) => configuration.GetEAUSection().GetValue<string>("GL_EAUTH_BASE_URL").TrimEnd('/');
                options.LoginCompletionUrl = (ctx, rs) => EAuthConstants.LoginCompletionUrl;

                options.Events = new ComponentSpace.Saml2.Authentication.SamlAuthenticationEvents
                {
                    OnError = (ctx, err) =>
                    {
                        // прехвърляме грешката за да бъде прихваната от нашия ExceptionHanlder
                        throw err;
                    }
                };
            });
        }
    }
}
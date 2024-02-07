using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.AspNetCore.Authentication
{
    public static class PortalAppAuthenticationDefaults
    {
        public const string UserRegistrationAuthScheme = "UserRegistrationCookies";
        public const string UserRegistrationChallengeScheme = "UserRegistrationOidc";
    }

    public static class AuthenticationBuilderPortalAppExtensions
    {
        public static AuthenticationBuilder AddCookieAuthenticationWithOpenIdConnectChallengeForUserRegistration(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
        {
            return authenticationBuilder
                .AddCookie(PortalAppAuthenticationDefaults.UserRegistrationAuthScheme, options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.ExpireTimeSpan = new TimeSpan(0, 10, 0);
                    options.SlidingExpiration = true;
                    options.ForwardChallenge = PortalAppAuthenticationDefaults.UserRegistrationChallengeScheme;
                    configuration.GetEAUSection().GetSection("UserRegistrationCookieAuthentication").Bind(options);
                })
                .AddOpenIdConnectChallenge(PortalAppAuthenticationDefaults.UserRegistrationChallengeScheme, configuration,
                    openIdConnectSetup =>
                    {
                        configuration.GetEAUSection().GetSection("UserRegistrationOpenIdConnectAuthentication").Bind(openIdConnectSetup);
                        openIdConnectSetup.SignInScheme = PortalAppAuthenticationDefaults.UserRegistrationAuthScheme;
                        openIdConnectSetup.CallbackPath = new PathString("/signin-oidc-reg");
                        openIdConnectSetup.SignedOutCallbackPath = new PathString("/signout-callback-oidc-reg");
                    });
        }
    }
}

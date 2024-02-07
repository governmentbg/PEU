using EAU.Common;
using EAU.Security;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Authentication
{
    public static class AuthenticationBuilderExtensions
    {
        public static AuthenticationBuilder AddOpenIdConnectChallenge(this AuthenticationBuilder authenticationBuilder,
            string schemeName, IConfiguration configuration, Action<OpenIdConnectOptions>? configureOpenIdConnectOptions = null)
        {
            return authenticationBuilder.AddOpenIdConnect(schemeName, openIdConnectSetup =>
            {
                openIdConnectSetup.Authority = configuration.GetEAUSection().Get<GlobalOptions>().GL_IDSRV_URL;
                openIdConnectSetup.ResponseType = "code";
                openIdConnectSetup.UsePkce = true;
                openIdConnectSetup.GetClaimsFromUserInfoEndpoint = true;
                openIdConnectSetup.SaveTokens = true;
                openIdConnectSetup.RequireHttpsMetadata = true;

                // default mappings for user info claims
                openIdConnectSetup.ClaimActions.MapUniqueJsonKey(EAUClaimTypes.CIN, EAUClaimTypes.CIN);
                openIdConnectSetup.ClaimActions.MapUniqueJsonKey(EAUClaimTypes.UserIdentifiable, EAUClaimTypes.UserIdentifiable);
                openIdConnectSetup.ClaimActions.MapUniqueJsonKey(EAUClaimTypes.LoginSessionID, EAUClaimTypes.LoginSessionID);
                openIdConnectSetup.ClaimActions.MapUniqueJsonKey(EAUClaimTypes.PersonIdentifier, EAUClaimTypes.PersonIdentifier);
                openIdConnectSetup.ClaimActions.MapUniqueJsonKey(EAUClaimTypes.PersonIdentifierType, EAUClaimTypes.PersonIdentifierType);
                openIdConnectSetup.ClaimActions.MapUniqueJsonKey(EAUClaimTypes.PersonNames, EAUClaimTypes.PersonNames);
                openIdConnectSetup.ClaimActions.MapUniqueJsonKey(EAUClaimTypes.UIC, EAUClaimTypes.UIC);
                openIdConnectSetup.ClaimActions.MapUniqueJsonKey(EAUClaimTypes.Certificate, EAUClaimTypes.Certificate);
                openIdConnectSetup.ClaimActions.MapUniqueJsonKey(ClaimTypes.WindowsAccountName, ClaimTypes.WindowsAccountName);
                openIdConnectSetup.ClaimActions.Add(new AspNetCore.Authentication.OAuth.Claims.JsonKeyClaimAction("role", null, "role"));

                // default handling
                // ако заявката е AJAX - връщане на 401 Unauthorized
                // ако заявката е стандарна - само се подсигурява, че Referer е от същия домейн, т.е от приложението.
                openIdConnectSetup.Events.OnRedirectToIdentityProvider = e =>
                {
                    bool isAjaxCall = e.Request.Headers != null && e.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
                    var cookieName = configuration.GetEAUSection().GetSection("CookieAuthentication").GetSection("Cookie").GetValue<string>("Name");

                    if (isAjaxCall)
                    {
                        if (e.Request.Cookies.ContainsKey(cookieName))
                            e.Response.Headers.Add("Session-Expired", "Session-Expired");

                        e.Response.Headers.Remove("Set-Cookie");
                        e.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        e.HandleResponse();

                        return System.Threading.Tasks.Task.CompletedTask;
                    }

                    var preventRedirectToReferer = configuration.GetEAUSection().GetSection("OpenIdConnectAuthentication").GetValue<bool?>("PreventRedirectToReferer");
                    bool preventSettingRedirectUri = preventRedirectToReferer != null && preventRedirectToReferer.Value;

                    if (preventSettingRedirectUri == false && !string.IsNullOrWhiteSpace(e.Request.Headers["Referer"]))
                    {
                        var referer = e.Request.Headers["Referer"];
                        var refUri = new Uri(referer);

                        e.ProtocolMessage.UiLocales = e.Request.Cookies["currentLang"];

                        if (string.Compare(refUri.Host, e.Request.Host.Host, true) == 0)
                            e.Properties.RedirectUri = referer;
                    }

                    if (e.Properties.Parameters != null && e.Properties.Parameters.TryGetValue("mode", out object? modeValue))
                    {
                        e.ProtocolMessage.SetParameter("mode", modeValue.ToString());
                    }

                    return System.Threading.Tasks.Task.CompletedTask;
                };

                openIdConnectSetup.Events.OnRedirectToIdentityProviderForSignOut = e =>
                {
                    //сетваме флага за редиректване към зададено от нас uri на true
                    if (e.Properties.Parameters.ContainsKey("postlogoutregirect"))
                        e.ProtocolMessage.SetParameter("postlogoutregirect", "true");

                    if (e.Properties.Parameters.ContainsKey("logoutReason"))
                        e.ProtocolMessage.SetParameter("logoutReason", e.Properties.Parameters["logoutReason"].ToString());

                    return System.Threading.Tasks.Task.CompletedTask;
                };

                // handle на отказ на потребителя от вход - просто връщаме където
                // е бил преди редиректа към IdentityProvider-а
                openIdConnectSetup.Events.OnAccessDenied = ctx =>
                {
                    if (false == string.IsNullOrWhiteSpace(ctx.ReturnUrl))
                    {
                        var retUri = new Uri(ctx.ReturnUrl);

                        if (string.Compare(retUri.Host, ctx.Request.Host.Host, true) == 0)
                        {
                            ctx.Response.Redirect(ctx.ReturnUrl);
                            ctx.HandleResponse();
                        }
                    }

                    return System.Threading.Tasks.Task.CompletedTask;
                };

                // тук в този хендлър е мястото където може да се прихваща тази грешка, която се получава
                // ако дадеш back в браузъра отново към IS, след като си се логнал вече например.
                openIdConnectSetup.Events.OnRemoteFailure = ctx =>
                {
                    if (ctx.Failure?.Message == "Correlation failed."
                            && !string.IsNullOrEmpty(ctx.Properties.RedirectUri))
                    {
                        var l = ctx.HttpContext.RequestServices.GetRequiredService<ILogger<OpenIdConnectHandler>>();
                        l.LogWarning($"Correlation failed in OnRemoteFailure: will redirect to {ctx.Properties.RedirectUri}");

                        ctx.Response.Redirect(ctx.Properties.RedirectUri);
                        ctx.HandleResponse();
                    }
                    return System.Threading.Tasks.Task.CompletedTask;
                };

                openIdConnectSetup.Events.OnMessageReceived = ctx =>
                {
                    return Task.CompletedTask;
                };

                openIdConnectSetup.Events.OnTicketReceived = ctx =>
                {
                    return Task.CompletedTask;
                };

                openIdConnectSetup.Events.OnUserInformationReceived = ctx =>
                {
                    return Task.CompletedTask;
                };

                configureOpenIdConnectOptions?.Invoke(openIdConnectSetup);
            });
        }
    }
}

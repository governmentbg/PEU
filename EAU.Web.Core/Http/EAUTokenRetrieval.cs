using IdentityModel.AspNetCore.OAuth2Introspection;
using Microsoft.AspNetCore.Http;
using System;

namespace EAU.Web.Http
{
    /// <summary>
    /// Предоставя стратегии за извличане на Access token от заявка.
    /// </summary>
    public static class EAUTokenRetrieval
    {
        public static Func<HttpRequest, string> DefaultTokenRetrieval()
        {
            return (request) =>
            {
                string token = TokenRetrieval.FromAuthorizationHeader().Invoke(request);

                if (string.IsNullOrEmpty(token))
                    token = TokenRetrieval.FromQueryString().Invoke(request);

                return token;
            };
        }
    }
}

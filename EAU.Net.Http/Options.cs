using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Net.Http
{
    public class HttpClientNames
    {
        public const string IdentityTokenApi = "idsrvtoken.api";
    }

    public class EAUHttpClientsOptions
    {
        public Dictionary<string, HttpClientOptions> HttpClients { get; set; }
    }

    public class HttpClientOptions
    {
        public TimeSpan TimeOut { get; set; }
        public HttpClientAuthenticationOptions Auth { get; set; }
    }

    public class HttpClientAuthenticationOptions
    {
        public string RequiredScopes { get; set; }
        public string AuthenticationClientID { get; set; }

        public bool DelegateIfHasSubject { get; set; }
    }

    public class HttpAuthenticationClients
    {
        public Dictionary<string, AuthenticationClientOptions> Clients { get; set; }
    }

    public class AuthenticationClientOptions
    {
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
    }
}

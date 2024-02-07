using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Net.Http
{
    public class AuthenticationMessageHandler : DelegatingHandler
    {
        private readonly AuthenticationClientOptions _AuthenticationClientOptions;
        private readonly HttpClientAuthenticationOptions _httpClientAuthenticationOptions;
        private readonly IIndentityServiceTokenRequestClient _indentityServicerTokenRequestClient;

        public AuthenticationMessageHandler(
            IIndentityServiceTokenRequestClient indentityServicerTokenRequestClient,
            AuthenticationClientOptions authenticationClientOptions,
            HttpClientAuthenticationOptions httpClientAuthenticationOptions)
        {
            _indentityServicerTokenRequestClient = indentityServicerTokenRequestClient;
            _AuthenticationClientOptions = authenticationClientOptions;
            _httpClientAuthenticationOptions = httpClientAuthenticationOptions;

        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var token = await _indentityServicerTokenRequestClient.RequestClientCredentialsTokenAsync(new IndentityServiceTokenRequest()
            {
                ClientID = _AuthenticationClientOptions.ClientID,
                ClientSecret = _AuthenticationClientOptions.ClientSecret,
                DelegateIfHasSubject = _httpClientAuthenticationOptions.DelegateIfHasSubject,
                RequiredScopes = _httpClientAuthenticationOptions.RequiredScopes
            }, cancellationToken);

            if (string.IsNullOrEmpty(token))
                throw new Exception($"Cannot make a call without access token");

            request.SetBearerToken(token);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

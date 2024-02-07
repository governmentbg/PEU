using EAU.Net.Http.Authentication;
using IdentityModel.Client;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Net.Http
{
    public class IndentityServiceTokenRequest
    {
        public string ClientID { get; set; }

        public string ClientSecret { get; set; }

        public string RequiredScopes { get; set; }

        public bool DelegateIfHasSubject { get; set; }
    }


    public interface IIndentityServiceTokenRequestClient
    {
        Task<string> RequestClientCredentialsTokenAsync(IndentityServiceTokenRequest options, CancellationToken cancellationToken);
    }

    public class IndentityServicerTokenRequestClient : IIndentityServiceTokenRequestClient
    {
        private const string _subClaimType = "sub";

        private readonly object _syncRoot;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IMemoryCache _memoryCache;
        private readonly IClaimsPrincipalAccessor _principalAccessor;
        private readonly ILogger _logger;

        public IndentityServicerTokenRequestClient(IHttpClientFactory httpClientFactory, ILogger<IndentityServicerTokenRequestClient> logger) : this(httpClientFactory, null, null, logger)
        {
        }

        public IndentityServicerTokenRequestClient(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, ILogger<IndentityServicerTokenRequestClient> logger) : this(httpClientFactory, memoryCache, null, logger)
        {
        }

        public IndentityServicerTokenRequestClient(IHttpClientFactory httpClientFactory, IClaimsPrincipalAccessor claimsPrincipalAccessor, ILogger<IndentityServicerTokenRequestClient> logger) : this(httpClientFactory, null, claimsPrincipalAccessor, logger)
        {
        }

        public IndentityServicerTokenRequestClient(IHttpClientFactory httpClientFactory, IMemoryCache memoryCache, IClaimsPrincipalAccessor claimsPrincipalAccessor, ILogger<IndentityServicerTokenRequestClient> logger)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _memoryCache = memoryCache;
            _principalAccessor = claimsPrincipalAccessor;
            _logger = logger;
            _syncRoot = new object();

            if (_memoryCache == null)
                _logger.LogWarning("No IMemoryCache is injected. Access Tokens will not be reused!");
        }

        public Task<string> RequestClientCredentialsTokenAsync(IndentityServiceTokenRequest options, CancellationToken cancellationToken)
        {
            string subject = ExtractByType(_subClaimType);
            string wan = ExtractByType(ClaimTypes.WindowsAccountName);

            var claimsPrincipal = _principalAccessor?.ClaimsPrincipal;

            if (_memoryCache == null)
            {
                return GenerateAccessTokenAsync(subject, wan, claimsPrincipal, options, cancellationToken).ContinueWith((item) => { return item.Result.AccessToken; });
            }
            else
            {
                var cacheKey = GenerateKey(subject, wan, options);
                Task<string> accessTokenTask = null;

                if (!_memoryCache.TryGetValue(cacheKey, out accessTokenTask))
                {
                    /*заключваме, за да може да не се създават задачи за едно и също нещо !*/
                    lock (_syncRoot)
                    {
                        if (!_memoryCache.TryGetValue(cacheKey, out accessTokenTask))
                        {
                            /*не ползваме using конструкция, за да може при наличието на грешка, да не вкарваме елемента в кеша.*/
                            var cacheItem = _memoryCache.CreateEntry(cacheKey);

                            _logger.LogInformation("Create Cache Key={cacheKey} For Subject={subject}", cacheKey, subject);

                            CancellationTokenSource cts = new CancellationTokenSource();

                            cacheItem.AddExpirationToken(new CancellationChangeToken(cts.Token));

                            accessTokenTask = CreateAccessGenerationTokenAsync(cts, subject, wan, claimsPrincipal, options, cancellationToken);

                            cacheItem.SetValue(accessTokenTask);

                            cacheItem.Dispose();
                        }
                    }
                }
                return accessTokenTask;
            }
        }


        private async Task<string> CreateAccessGenerationTokenAsync(CancellationTokenSource tokenInvalidationCts, string subject, string windowsAccountName, ClaimsPrincipal claimsPrincipal, IndentityServiceTokenRequest options, CancellationToken cancellationToken)
        {
            /*тук трябва да развържем изпълнението от текущата нишка, за да може да се създаде максимално бързо задачата и последващите заявки да блокират върху нея.*/
            await Task.Yield();

            try
            {
                Stopwatch stopwatch = Stopwatch.StartNew();

                TokenResponse tokenResponse = await GenerateAccessTokenAsync(subject, windowsAccountName, claimsPrincipal, options, cancellationToken);

                tokenInvalidationCts.CancelAfter(new TimeSpan(0, 0, tokenResponse.ExpiresIn).Subtract(stopwatch.Elapsed));

                return tokenResponse.AccessToken;
            }
            catch
            {
                //Тук инвалидираме кеша, защото иначе ще кешираме провалила се задача и така номенклатурата никога няма да се зареди отново.
                tokenInvalidationCts.Cancel();

                throw;
            }
        }

        private async Task<TokenResponse> GenerateAccessTokenAsync(string subject, string windowsAccountName, ClaimsPrincipal principal, IndentityServiceTokenRequest options, CancellationToken cancellationToken)
        {
            using (HttpClient httpClient = GetClient())
            {
                TokenResponse tokenResponse = default;
                if (options.DelegateIfHasSubject && (!string.IsNullOrWhiteSpace(subject) || !string.IsNullOrWhiteSpace(windowsAccountName)))
                {
                    string grantType = null, tokenType = null, tokenValue = null;
                    if (!string.IsNullOrWhiteSpace(subject))
                    {
                        string userAccessToken = await _principalAccessor.GetTokenAsync();

                        if (string.IsNullOrWhiteSpace(userAccessToken))
                        {
                            throw new InvalidOperationException("CurrentPrincipal accessToken not found!");
                        }

                        grantType = "delegation";
                        tokenType = "token";
                        tokenValue = userAccessToken;
                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(windowsAccountName))
                        {
                            throw new ArgumentNullException(nameof(windowsAccountName));
                        }

                        grantType = "weak_delegation";
                        tokenType = "windows_account_name";
                        tokenValue = windowsAccountName;
                    }

                    TokenRequest tokenRequest2 = new TokenRequest
                    {
                        GrantType = grantType,
                        ClientId = options.ClientID,
                        ClientSecret = options.ClientSecret,
                        Parameters = new Parameters(new Dictionary<string, string>
                        {
                            {
                                "scope",
                                options.RequiredScopes
                            },
                            {
                                tokenType,
                                tokenValue
                            }
                        }) 
                    };

                    LogDebugInfo(subject, windowsAccountName, options);

                    tokenResponse = await httpClient.RequestTokenAsync(tokenRequest2, cancellationToken);
                }
                else
                {
                    ClientCredentialsTokenRequest tokenRequest = new ClientCredentialsTokenRequest
                    {
                        ClientId = options.ClientID,
                        ClientSecret = options.ClientSecret,
                        Scope = options.RequiredScopes
                    };

                    _logger.LogDebug($"Issuing access token - grant type: client_credentials, scope: {options.RequiredScopes}, clientid: {options.ClientID}");
                    tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(tokenRequest, cancellationToken);
                }

                if (tokenResponse.IsError)
                {
                    throw new Exception("Access token response error for api scope: " + options.RequiredScopes + ", clientid: " + options.ClientID + ", error: " + tokenResponse.Error, tokenResponse.Exception);
                }

                return tokenResponse;
            }
        }

        private void LogDebugInfo(string subject, string windowsAccountName, IndentityServiceTokenRequest options)
        {
            var logInfo = "Issuing access token - grant type: delegation, ";

            if (!String.IsNullOrWhiteSpace(subject))
                logInfo = String.Concat(logInfo, "subject: ", subject);
            else if (!String.IsNullOrWhiteSpace(windowsAccountName))
                logInfo = String.Concat(logInfo, "wan: ", windowsAccountName);

            logInfo = String.Concat(logInfo, ", scope: ", options.RequiredScopes, ", clientid: ", options.ClientID);

            _logger.LogDebug(logInfo);
        }

        private HttpClient GetClient()
        {
            var client = _httpClientFactory.CreateClient(HttpClientNames.IdentityTokenApi);

            return client;
        }

        private string ExtractByType(string type)
        {
            ClaimsPrincipal claimsPrincipal = _principalAccessor?.ClaimsPrincipal;
            object obj;
            if (claimsPrincipal == null)
            {
                obj = null;
            }
            else
            {
                IEnumerable<Claim> claims = claimsPrincipal.Claims;
                obj = ((claims != null) ? (from item in claims
                                           where item.Type == type
                                           select item).SingleOrDefault()?.Value : null);
            }

            return (string)obj;
        }

        private string GenerateKey(string subject, string windowsAccountName, IndentityServiceTokenRequest options)
        {
            var key = "at_";
            if (options.DelegateIfHasSubject)
            {
                if (!String.IsNullOrWhiteSpace(subject))
                    key = String.Concat(key, "s:", subject, "_");

                if (!String.IsNullOrWhiteSpace(windowsAccountName))
                    key = String.Concat(key, "wan:", windowsAccountName, "_");
            }

            key = String.Concat(key, "cl:", options.ClientID, "_scopes:", options.RequiredScopes);

            return key;
        }
    }
}

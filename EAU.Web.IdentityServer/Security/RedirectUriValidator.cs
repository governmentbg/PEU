using EAU.Nomenclatures;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.IdentityServer.Security
{
    public class RedirectUriValidator : StrictRedirectUriValidator
    {
        private readonly List<string> _activeLanguageCodes;
        private readonly ConcurrentDictionary<string, IEnumerable<string>> _clientPostLogoutUrisPrepared = new ConcurrentDictionary<string, IEnumerable<string>>();

        public RedirectUriValidator(ILanguages languages) : base()
        {
            _activeLanguageCodes = languages.Search().Where(l => l.IsActive.GetValueOrDefault()).Select(l => l.Code).ToList();
        }

        /// <summary>
        /// Determines whether a post logout URI is valid for a client.
        /// </summary>
        /// <param name="requestedUri">The requested URI.</param>
        /// <param name="client">The client.</param>
        /// <returns>
        ///   <c>true</c> is the URI is valid; <c>false</c> otherwise.
        /// </returns>
        public override Task<bool> IsPostLogoutRedirectUriValidAsync(string requestedUri, Client client)
        {
            var uris = _clientPostLogoutUrisPrepared.GetOrAdd(client.ClientId, (cid) =>
            {
                var clientUris = new List<string>();

                foreach (var lcode in _activeLanguageCodes)
                {
                    clientUris.AddRange(client.PostLogoutRedirectUris.Select(u => u.Replace("{lang}", lcode)));
                }

                return clientUris.Distinct();
            });

            return Task.FromResult(StringCollectionContainsString(uris, requestedUri));
        }
    }
}

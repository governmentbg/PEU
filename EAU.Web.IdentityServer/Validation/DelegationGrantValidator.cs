using IdentityServer4.Models;
using IdentityServer4.Validation;
using EAU.Security;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.IdentityServer.Validation
{
    /// <summary>
    /// Имплементация на IExtensionGrantValidator за delegation tokens.
    /// </summary>
    public class DelegationGrantValidator : IExtensionGrantValidator
    {
        private readonly ITokenValidator _validator;

        public DelegationGrantValidator(ITokenValidator validator)
        {
            _validator = validator;
        }

        public string GrantType => CustomGrantTypes.Delegation;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var userToken = context.Request.Raw.Get("token");

            if (string.IsNullOrEmpty(userToken))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            var result = await _validator.ValidateAccessTokenAsync(userToken);
            if (result.IsError)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            // get user's identity
            var sub = result.Claims.GetSubject();

            // extract claims to be copied to delegation token
            var claimsToCopy = result.Claims.Where(c => c.Type == EAUClaimTypes.LoginSessionID);

            context.Result = new GrantValidationResult(sub, GrantType, claims: claimsToCopy);
            return;
        }
    }
}

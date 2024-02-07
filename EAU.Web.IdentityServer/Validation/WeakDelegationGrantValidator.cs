using EAU.Users;
using EAU.Web.IdentityServer.Common;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EAU.Web.IdentityServer.Validation
{
    /// <summary>
    /// Имплементация на IExtensionGrantValidator за legacy grant_type.
    /// </summary>
    public class WeakDelegationGrantValidator : IExtensionGrantValidator
    {
        private readonly ITokenValidator _validator;
        private readonly IServiceProvider _serviceProvider;

        public WeakDelegationGrantValidator(ITokenValidator validator,
                                            IServiceProvider serviceProvider
            )
        {
            _validator = validator;
            _serviceProvider = serviceProvider;
        }

        public string GrantType => CustomGrantTypes.WeakDelegation;

        public async Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var wan = context.Request.Raw.Get("windows_account_name");

            if (string.IsNullOrEmpty(wan))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant);
                return;
            }

            var authResult = await _serviceProvider.GetRequiredService<IUsersLoginService>().AuthenticateWindowsWeakAsync(wan);
            if (!authResult.IsSuccess)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient);
                return;
            }

            var wanPrepared = ClaimsHelper.BuildSubClaimValueForCIN(authResult.User.CIN.Value);

            context.Result = new GrantValidationResult(wanPrepared, GrantType);
        }
    }
}

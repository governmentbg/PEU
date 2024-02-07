using ComponentSpace.Saml2.Assertions;
using EAU.Security;
using EAU.Users;
using EAU.Users.Models;
using EAU.Users.Repositories;
using EAU.Web.IdentityServer.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.IdentityServer.Common
{
    /// <summary>
    /// Имплементация на IProfileService.
    /// </summary>
    public class EAUProfileService : IProfileService
    {
        private readonly IUsersSearchService UsersService;

        public EAUProfileService(IUsersSearchService usersService)
        {
            UsersService = usersService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            if (!TryGetValueFromSubject(context.Subject, out int? cin, out string? certTb, out string? pid))
                throw new InvalidOperationException($"Cannot parse cin from sub value {context.Subject.Claims.GetSubject()}!");

            List<Claim> userClaims = new List<Claim>();

            if (cin != null)
            {
                // load all predefined claims
                userClaims = (await SearchClaimsAsync(
                new UserSearchCriteria()
                {
                    CIN = cin
                })).ToList();
            }
            
            List<string> missingClaimTypes = new List<string>();
            foreach (var item in context.RequestedClaimTypes)
            {
                if (!userClaims.Any(c => c.Type == item))
                    missingClaimTypes.Add(item);
            }

            foreach (var item in missingClaimTypes)
            {
                var claimFound = context.Subject.Claims.Where(c => c.Type == item).SingleOrDefault();
                if (claimFound != null)
                    userClaims.Add(claimFound);
            }

            context.AddRequestedClaims(userClaims);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
            return Task.CompletedTask;
        }

        private async Task<IEnumerable<Claim>> SearchClaimsAsync(UserSearchCriteria criteria)
        {
            criteria.UserStatuses = new List<UserStatuses>() { UserStatuses.Active };
            criteria.LoadUserPermissions = true;

            var user = (await UsersService.SearchUsersAsync(criteria, CancellationToken.None)).SingleOrDefault();

            if (user == null)
                throw new InvalidOperationException($"User with email = {criteria.Email} not found!");

            return user.GetClaims();
        }

        private bool TryGetValueFromSubject(ClaimsPrincipal subject, out int? cin, out string? certTb, out string? pid)
        {
            cin = null; certTb = null; pid = null;

            var subParts = subject.Claims.GetSubjectDecoded();

            if (subParts.Length == 1 && int.TryParse(subParts[0], out int cinParsed))
            {
                cin = cinParsed;
                return true;
            }
            else if (subParts.Length == 2 && subParts[0] == "cin" && int.TryParse(subParts[1], out cinParsed))
            {
                cin = cinParsed;
                return true;
            }
            else if (subParts.Length == 2 && subParts[0] == "certtb" && !string.IsNullOrEmpty(subParts[1]))
            {
                certTb = subParts[1];
                return true;
            }
            else if (subParts.Length == 2 && subParts[0] == "pid" && !string.IsNullOrEmpty(subParts[1]))
            {
                pid = subParts[1];
                return true;
            }
            else
            {
                return false;
            }                
        }
    }
}

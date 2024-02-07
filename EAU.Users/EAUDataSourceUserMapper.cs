using EAU.Security;
using EAU.Users.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users
{
    internal class EAUDataSourceUserMapper : IDataSourceUserMapper
    {
        private readonly IUsersSearchService _usersSearchService;
        public EAUDataSourceUserMapper(IUsersSearchService usersService)
        {
            _usersSearchService = usersService;
        }

        public IEnumerable<Claim> GetInterestedClaims(ClaimsPrincipal principal)
        {
            /*не се връщат клаимове, защото няма нужда се синхронизират данните*/
            return Enumerable.Empty<Claim>();
        }

        public async Task<string> MapAndSyncEAUUserToLocalUserAsync(ClaimsPrincipal principal, CancellationToken cancellationToken)
        {
            return (await _usersSearchService.SearchUsersAsync(new UserSearchCriteria() { CIN = principal.Claims.GetCIN().Value }, cancellationToken)).SingleOrDefault()?.UserID.ToString();
        }
    }
}

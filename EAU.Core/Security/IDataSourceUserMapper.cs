using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Security
{
    public interface IDataSourceUserMapper
    {
        IEnumerable<Claim> GetInterestedClaims(ClaimsPrincipal principal);
        Task<string> MapAndSyncEAUUserToLocalUserAsync(ClaimsPrincipal principal, CancellationToken cansellationToken);
    }
}

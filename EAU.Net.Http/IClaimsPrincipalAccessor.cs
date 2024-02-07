using System.Security.Claims;
using System.Threading.Tasks;

namespace EAU.Net.Http.Authentication
{
    /// <summary>
    /// Осигурява достъп до текущия ClaimsPrincipal.
    /// </summary>
    public interface IClaimsPrincipalAccessor
    {
        ClaimsPrincipal ClaimsPrincipal { get; }

        Task<string> GetTokenAsync();
    }
}

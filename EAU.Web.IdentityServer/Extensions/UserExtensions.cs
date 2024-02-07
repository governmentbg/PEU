using EAU.Security;
using EAU.Users.Models;
using IdentityModel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EAU.Web.IdentityServer.Extensions
{
    public static class UserExtensions
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            yield return new Claim(EAUClaimTypes.CIN, user.CIN.ToString());
            yield return new Claim(JwtClaimTypes.Name, user.Email);
            yield return new Claim(JwtClaimTypes.Email, user.Email);

            if (!string.IsNullOrEmpty(user.Username))
            {
                yield return new Claim(ClaimTypes.WindowsAccountName, user.Username);
            }

            if (user.UserPermissions != null && user.UserPermissions.Any())
            {
                foreach (var up in user.UserPermissions)
                {
                    yield return new Claim(JwtClaimTypes.Role, up.Permission?.ToString());
                }
            }
        }
    }
}

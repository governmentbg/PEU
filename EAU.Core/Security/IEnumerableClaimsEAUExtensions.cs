using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;

namespace EAU.Security
{
    public static class IEnumerableClaimsEAUExtensions
    {
        public static string GetSubject(this IEnumerable<Claim> claims)
        {
            return claims.GetClaim("sub")?.Value;
        }

        public static string[] GetSubjectDecoded(this IEnumerable<Claim> claims)
        {
            return claims.GetSubject()?.Split(':');
        }

        public static string GetName(this IEnumerable<Claim> claims)
        {
            return claims.GetClaim("name")?.Value;
        }

        public static string GetEmail(this IEnumerable<Claim> claims)
        {
            return claims.GetClaim("email")?.Value;
        }

        public static string[] GetRoles(this IEnumerable<Claim> claims)
        {
            return claims.Where(c => c.Type == "role").Select(c => c.Value).ToArray();
        }

        public static int? GetCIN(this IEnumerable<Claim> claims)
        {
            if (int.TryParse(claims.GetClaim(EAUClaimTypes.CIN)?.Value, out int ret))
                return ret;
            else
                return null;
        }

        public static Guid? GetLoginSessionID(this IEnumerable<Claim> claims)
        {
            var loginSessionID = claims.GetClaim(EAUClaimTypes.LoginSessionID)?.Value;

            if (!string.IsNullOrEmpty(loginSessionID))
                return Guid.Parse(loginSessionID);
            else
                return null;
        }

        public static Claim GetClaim(this IEnumerable<Claim> claims, string type)
        {
            return claims?.Where((item) => { return item.Type == type; }).SingleOrDefault();
        }

        public static bool? GetIsUserIdentifiable(this IEnumerable<Claim> claims)
        {
            var isserIdentifiable = claims.GetClaim(EAUClaimTypes.UserIdentifiable)?.Value;
            bool isserIdentifiableRes = false;

            if (bool.TryParse(isserIdentifiable, out isserIdentifiableRes))
            {
                return isserIdentifiableRes;
            }
            else
                return null;
        }

        public static string GetCertificateClaim(this IEnumerable<Claim> claims)
        {
            return claims.GetClaim(EAUClaimTypes.Certificate)?.Value;
        }
    }
}

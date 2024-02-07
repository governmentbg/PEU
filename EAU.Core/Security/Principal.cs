using System.Collections.Generic;
using System.Security.Claims;

namespace EAU.Security
{
    /// <summary>
    /// Helper за създаване на Principal обекти
    /// </summary>
    public static class Principal
    {
        /// <summary>
        /// Създава Principal от AnonymousIdenity.
        /// </summary>
        public static ClaimsPrincipal Anonymous => new ClaimsPrincipal(AnonymousIdenity);

        /// <summary>
        /// Създава празен ClaimsIdentity.
        /// </summary>
        public static ClaimsIdentity AnonymousIdenity
        {
            get
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, "")
                };

                return new ClaimsIdentity(claims);
            }
        }
    }
}

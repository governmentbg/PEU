using EAU.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Http
{
    internal class EAUPrincipalTranformation : IClaimsTransformation
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IDataSourceUserMapper _dataSourceUserMapper;

        public EAUPrincipalTranformation(IMemoryCache memoryCache, IDataSourceUserMapper dataSourceUserMapper)
        {
            _memoryCache = memoryCache;
            _dataSourceUserMapper = dataSourceUserMapper;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var user = principal;

            /*Ако потребителя е вече EAUPrincipal не правим нищо!*/
            if (user is EAUPrincipal ||
                user is null)
                return user;

            var userCIN = user.Claims.GetCIN();

            string clientID;

            if (userCIN.HasValue)
            {
                object cacheKey = GetCacheKey(userCIN.Value);

                int claimsHashCode = GetClaimsHashCode(_dataSourceUserMapper.GetInterestedClaims(user));

                if (!_memoryCache.TryGetValue(cacheKey, out Tuple<string, int> userData) ||
                    userData.Item2 != claimsHashCode)
                {
                    #region  Синхронизираме данните в локална база.

                    userData = new Tuple<string, int>(await _dataSourceUserMapper.MapAndSyncEAUUserToLocalUserAsync(user, CancellationToken.None), claimsHashCode);

                    using (var cacheEntry = _memoryCache.CreateEntry(cacheKey))
                    {
                        cacheEntry.Value = userData;
                        cacheEntry.Priority = CacheItemPriority.High;
                    }

                    #endregion
                }

                clientID = userData.Item1;
            }
            else
                clientID = EAUPrincipal.SystemLocalUserID.ToString();

            var epzeuPrincipal = new EAUPrincipal(user, clientID);

            return epzeuPrincipal as ClaimsPrincipal;
        }

        private object GetCacheKey(int CIN)
        {
            return "EAU_USER_" + CIN;
        }

        private int GetClaimsHashCode(IEnumerable<Claim> claims)
        {
            /*TODO: atanas да се използва по-добро генериране на хешкод*/
            if (claims == null)
                return 0;
            else
                return string.Join("~", claims.Select(item => item.Value)).GetHashCode();
        }
    }
}

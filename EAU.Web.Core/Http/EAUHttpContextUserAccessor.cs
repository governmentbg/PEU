using EAU.Net.Http.Authentication;
using EAU.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Http
{
    /// <summary>
    /// Имплементация на IEAUUserAccessor, базирана на IHttpContextAccessor.
    /// </summary>
    internal class EAUHttpContextUserAccessor : IEAUUserAccessor, IClaimsPrincipalAccessor, IEAUUserImpersonation
    {
        private static AsyncLocal<EAUPrincipal> _impersonatedUser = new AsyncLocal<EAUPrincipal>();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public EAUHttpContextUserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public EAUPrincipal User { get => _impersonatedUser.Value ?? (_httpContextAccessor?.HttpContext?.User as EAUPrincipal); }

        public Guid? UserSessionID => _httpContextAccessor?.HttpContext?.GetUserSessionID();

        public IPAddress RemoteIpAddress => _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress;

        public ClaimsPrincipal ClaimsPrincipal => _httpContextAccessor?.HttpContext?.User;

        public static EAUPrincipal SystemUser { get; } = new EAUPrincipal(Principal.Anonymous, EAUPrincipal.SystemLocalUserID.ToString());

        public Task<string> GetTokenAsync()
        {
            return _httpContextAccessor.HttpContext.GetTokenAsync("access_token");
        }

        public EAUUserImpersonationControl Impersonate(EAUPrincipal user)
        {
            var oldUser = _impersonatedUser.Value;

            _impersonatedUser.Value = user;

            return new EAUUserImpersonationControl(() =>
            {
                _impersonatedUser.Value = oldUser;
            });
        }
    }
}

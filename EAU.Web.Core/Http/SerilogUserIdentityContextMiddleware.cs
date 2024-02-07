using EAU.Security;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;

namespace EAU.Web.Http
{
    public class SerilogUserIdentityContextMiddleware
    {
        private readonly IEAUUserAccessor _userAccessor;
        private readonly RequestDelegate _next;

        public SerilogUserIdentityContextMiddleware(IEAUUserAccessor userAccessor, RequestDelegate next)
        {
            _userAccessor = userAccessor;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            using (LogContext.PushProperty("user_subject", _userAccessor?.User?.Subject ?? string.Empty))
            using (LogContext.PushProperty("user_session_id", _userAccessor?.UserSessionID))
                await _next(context);
        }
    }
}

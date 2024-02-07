using Microsoft.Extensions.Primitives;
using System.Net;
using System.Threading.Tasks;

namespace EAU.ServiceLimits.AspNetCore
{
    public interface IServiceLimiter
    {
        Task<bool> ShouldRateLimitAsync(StringValues serviceCodes, int? userCIN, IPAddress userIPAddress);
    }
}

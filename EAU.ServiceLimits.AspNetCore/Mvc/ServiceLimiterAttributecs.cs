using EAU.Security;
using EAU.Web.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading.Tasks;

namespace EAU.ServiceLimits.AspNetCore.Mvc
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ServiceLimiterAttribute : Attribute, IAsyncResourceFilter, IServiceLimiterFilter
    {
        public ServiceLimiterAttribute()
        {
            ResultModelName = "isLimited";
            DoNotStopRequestProcessing = false;
        }

        /// <summary>
        /// Кода на лимита, който ще се проверява. Допълнително към този лимит, се лимитира и чрез базовия лимит за предоставяне на данни.
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// По подразбиране стойността е false, което при достигнат лимит, филтъра връща директно 429.
        /// Ако се подаде true, то не се спира обработката на заявката и ще се подаде като boolean поле с име ResultModelName.
        /// </summary>
        public bool DoNotStopRequestProcessing { get; set; }

        /// <summary>
        /// Името на полето, в което се слага резултата от лимитирането. 
        /// По подразбиране е "isLimited"
        /// </summary>
        public string ResultModelName { get; set; }

        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (context.IsEffectivePolicy<IServiceLimiterFilter>(this))
            {
                return OnResourceExecutionInternalAsync(context, next);
            }
            else
                return next();
        }

        private async Task OnResourceExecutionInternalAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            IServiceLimiter limiter = context.HttpContext.RequestServices.GetRequiredService<IServiceLimiter>();
            IEAUUserAccessor userAccessor = context.HttpContext.RequestServices.GetRequiredService<IEAUUserAccessor>();

            StringValues serviceLimitingCodes = string.IsNullOrWhiteSpace(ServiceCode) ? new string[] { "BASE_DATA_SERVICE_LIMIT" } : new string[] { ServiceCode, "BASE_DATA_SERVICE_LIMIT" };

            var shouldRateLimit = await limiter.ShouldRateLimitAsync(serviceLimitingCodes, userAccessor.User?.CIN.HasValue == true ? userAccessor.User.CIN : null, userAccessor.RemoteIpAddress);

            if (DoNotStopRequestProcessing)
            {
                if (shouldRateLimit)
                    context.RouteData.Values.Add(ResultModelName, shouldRateLimit);

                await next();
            }
            else
            {
                if (!shouldRateLimit)
                {
                    await next();
                }
                else
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status429TooManyRequests);
                }
            }
        }
    }
}

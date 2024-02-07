using EAU.Security;
using EAU.Web.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EAU.Web.Filters;

namespace EAU.ServiceLimits.AspNetCore.Mvc
{
    public class ContextLimiterToken
    {
        public string CIN { get; set; }
        public string UserIP { get; set; }
        public string ContextID { get; set; }
        public ISet<KeyValuePair<string, string>> ContextValue { get; set; }

        public override string ToString()
        {
            return string.Format("CIN:{0}, UserID:{1}, ContextID:{2}, ContextValue:{3}", CIN, UserIP, ContextID, string.Join(':', ContextValue));
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ContextServiceLimiterAttribute : Attribute, IAsyncResourceFilter, IServiceLimiterFilter
    {
        private static HashSet<KeyValuePair<string, string>> EmptyContextValue = new HashSet<KeyValuePair<string, string>>();

        private static Action<ILogger, ContextLimiterToken, ContextLimiterToken, string[], Exception> _SkipContextLimiterTokenNotValid;

        private readonly string contextLimiterTokenHeaderName = "X-ContextLimiter-Token";

        private Lazy<Settings> _settings;

        private StringValues BaseDataServiceLimitCode => _settings.Value.BaseDataServiceLimitCode;
        private StringValues ContextServiceLimitCodes => _settings.Value.ContextServiceLimitCodes;

        private class Settings
        {
            public StringValues BaseDataServiceLimitCode;
            public StringValues ContextServiceLimitCodes;
        }

        static ContextServiceLimiterAttribute()
        {
            _SkipContextLimiterTokenNotValid = LoggerMessage.Define<ContextLimiterToken, ContextLimiterToken, string[]>(LogLevel.Information, new EventId(1, "SkipContextLimiterTokenNotValid"), "Context Limiter Token Not Valid. RequestContextData:[{0}], RequestedTokenData:[{1}], AdditionalAllowedContextIDs:{2}");
        }

        public ContextServiceLimiterAttribute()
        {
            _settings = new Lazy<Settings>(() =>
            {
                var ret = new Settings();

                ret.BaseDataServiceLimitCode = "BASE_DATA_SERVICE_LIMIT";

                ret.ContextServiceLimitCodes = new string[] { ret.BaseDataServiceLimitCode, ServiceCode };
                return ret;
            });
        }

        /// <summary>
        /// Кода на лимита, който ще се проверява. Допълнително към този лимит, се лимитира и чрез базовия лимит за предоставяне на данни.
        /// </summary>
        public string ServiceCode { get; set; }

        public string GenerationContextID { get; set; }

        public string[] AdditionalAllowedContextIDs { get; set; }

        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            if (context.IsEffectivePolicy<IServiceLimiterFilter>(this))
            {
                return OnResourceExecutionInternalAsync(context, next);
            }
            else
                return next();
        }

        protected virtual ISet<KeyValuePair<string, string>> CreateContextValue(ResourceExecutingContext context)
        {
            return EmptyContextValue;
        }

        #region helpers

        private async Task OnResourceExecutionInternalAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            IDataProtectorService protectorService = context.HttpContext.RequestServices.GetRequiredService<IDataProtectorService>();
            IEAUUserAccessor userAccessor = context.HttpContext.RequestServices.GetRequiredService<IEAUUserAccessor>();
            IServiceLimiter limiterService = context.HttpContext.RequestServices.GetRequiredService<IServiceLimiter>();

            bool skipContextServiceLimiting = false;
            StringValues responseTokenString = default;

            if (context.HttpContext.Request.Headers.TryGetValue(contextLimiterTokenHeaderName, out StringValues requestTokenString))
            {
                ILogger logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ContextServiceLimiterAttribute>>();

                var token = DeserializeToken(protectorService.Unprotect(requestTokenString));

                skipContextServiceLimiting = IsSkipContextLimiterTokenValid(context, userAccessor, token, logger);

                if (skipContextServiceLimiting)
                    responseTokenString = requestTokenString;
                else if (!string.IsNullOrEmpty(GenerationContextID)) /*ако има подаден идентификатор на конекст за създаване*/
                    responseTokenString = protectorService.Protect(SerializeToken(CreateToken(context, userAccessor)));
            }
            else if (!string.IsNullOrEmpty(GenerationContextID))
            {
                //Създаваме хедър с използваните критерии.
                responseTokenString = protectorService.Protect(SerializeToken(CreateToken(context, userAccessor)));
            }

            if (!await limiterService.ShouldRateLimitAsync(
                skipContextServiceLimiting ? BaseDataServiceLimitCode : ContextServiceLimitCodes,
                userAccessor.User?.CIN.HasValue == true ? userAccessor.User.CIN : null,
                userAccessor.RemoteIpAddress))
            {
                /*Връщаме Token - а само, ако заявката не е лимитирана!*/
                if (responseTokenString.Count > 0)
                    context.HttpContext.Response.Headers.Add(contextLimiterTokenHeaderName, responseTokenString);

                var ret = await next();
                return;
            }
            else
            {
                context.Result = new StatusCodeResult(StatusCodes.Status429TooManyRequests);
            }
        }

        private string SerializeToken(ContextLimiterToken token)
        {
            return EAUJsonSerializer.Serialize(token);
        }

        private ContextLimiterToken DeserializeToken(string tokenString)
        {
            return EAUJsonSerializer.Deserialize<ContextLimiterToken>(tokenString);
        }

        private ContextLimiterToken CreateToken(ResourceExecutingContext context, IEAUUserAccessor userAccessor)
        {
            ContextLimiterToken ret = new ContextLimiterToken();

            ret.CIN = (userAccessor.User != null && userAccessor.User.CIN.HasValue) ? userAccessor.User.CIN.ToString() : "";

            ret.UserIP = userAccessor.RemoteIpAddress.ToString();

            ret.ContextID = GenerationContextID;

            ret.ContextValue = CreateContextValue(context) ?? EmptyContextValue;

            if (ret.ContextValue.Count == 0)
            {
                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<ContextServiceLimiterAttribute>>();
                var httpRequestFeature = context.HttpContext.Features.Get<IHttpRequestFeature>();

                logger.LogWarning("ContextValue for URL={0} is not generated", httpRequestFeature.RawTarget);
            }

            return ret;
        }

        private bool IsSkipContextLimiterTokenValid(ResourceExecutingContext context, IEAUUserAccessor userAccessor, ContextLimiterToken requestedToken, ILogger logger)
        {
            ContextLimiterToken requestData = CreateToken(context, userAccessor);

            bool ret = false;

            if (
                requestData.CIN == requestedToken.CIN &&
                requestData.UserIP == requestedToken.UserIP && 

                (requestData.ContextID == requestedToken.ContextID ||
                (AdditionalAllowedContextIDs != null && AdditionalAllowedContextIDs.Contains(requestedToken.ContextID))
                )
                )
            {
                /*стойността на контекста от токъна, трябва да го има в този от текущата заявката.*/
                ret = requestedToken.ContextValue.IsSubsetOf(requestData.ContextValue);
            }

            if (!ret)
            {
                _SkipContextLimiterTokenNotValid(
                    logger,
                    requestData,
                    requestedToken,
                    AdditionalAllowedContextIDs,
                    null);
            }

            return ret;
        }

        #endregion
    }
}

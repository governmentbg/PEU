using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EAU.Web.Api.Private.Code
{
    public class RawTextContentFormatter : TextInputFormatter
    {
        public RawTextContentFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/plain"));

            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanReadType(Type type)
        {
            return type == typeof(string);
        }


        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context, Encoding encoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<RawTextContentFormatter>>();


            try
            {
                using (var reader = new StreamReader(httpContext.Request.Body, encoding))
                {
                    string bodyData = await reader.ReadToEndAsync();

                    return await InputFormatterResult.SuccessAsync(bodyData);
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, ex.Message);

                return await InputFormatterResult.FailureAsync();
            }
        }
    }
}

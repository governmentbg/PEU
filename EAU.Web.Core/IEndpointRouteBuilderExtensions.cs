using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Microsoft.AspNetCore.Builder
{
    public static class IEndpointRouteBuilderExtensions
    {
        internal static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder builder)
        {
            var options = builder.ApplicationServices.GetRequiredService<IOptionsMonitor<SwaggerGenOptions>>();

            var defaultSwaggerDocument = options.CurrentValue.SwaggerGeneratorOptions.SwaggerDocs.Values.Single();

            builder.UseSwagger();

            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"../{c.RoutePrefix}/{defaultSwaggerDocument.Version}/swagger.json", defaultSwaggerDocument.Title);
            });

            return builder;
        }

        public static IEndpointRouteBuilder MapSwagger(this IEndpointRouteBuilder endpoints)
        {
            endpoints.Map("/swagger/{**restUrl}", endpoints.CreateApplicationBuilder()
                    .Use((context, next) =>
                    {
                        /*Clear the Endpoint of the current request. This is necessary for the StaticFileMiddlware to work properly.*/
                        context.SetEndpoint(null);
                        return next();
                    })
                    .UseSwaggerMiddleware()
                    .Build())
                    .WithDisplayName("swagger");

            return endpoints;
        }
    }
}

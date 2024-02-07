using EAU.Web.Filters;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionSwaggerExtensions
    {
        private const string ASSEMBLY_DOCUMENTATION_SEARCH_PATTERN = @"^EAU.*";

        public static IServiceCollection AddEAUSwaggerGen(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment hostingEnvironment,
            string version,
            string assemblyDocumentationSearchPattern = ASSEMBLY_DOCUMENTATION_SEARCH_PATTERN)
        {
            var ret = services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version, new OpenApiInfo { Title = hostingEnvironment.ApplicationName, Version = version });

                #region Include Xml Comments 

                DirectoryInfo di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
                var xmlFileNames = di.GetFiles("*.xml")
                                    .Where(x => Regex.IsMatch(x.Name, assemblyDocumentationSearchPattern, RegexOptions.IgnoreCase) == true)
                                    .Select(t => Regex.Replace(t.Name, ".xml", "", RegexOptions.IgnoreCase));

                var dllFileNames = di.GetFiles("*.dll")
                                    .Where(x => Regex.IsMatch(x.Name, assemblyDocumentationSearchPattern, RegexOptions.IgnoreCase) == true)
                                    .Select(t => Regex.Replace(t.Name, ".dll", "", RegexOptions.IgnoreCase));

                foreach (string regexFilteredXmlFileName in xmlFileNames)
                {
                    if (dllFileNames.Any(t => string.Compare(t, regexFilteredXmlFileName) == 0))
                    {
                        options.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{regexFilteredXmlFileName}.xml"));
                    }
                }

                #endregion

                options.SchemaGeneratorOptions.UseAllOfToExtendReferenceSchemas = true;

                #region Add Custom Schema for TimeSpan

                //https://swagger.io/docs/specification/data-models/data-types/#string
                //https://xml2rfc.tools.ietf.org/public/rfc/html/rfc3339.html#anchor14
                Func<OpenApiSchema> timeSpanMapper = () => new OpenApiSchema() { Type = "string", Format = "duration" };

                options.MapType<TimeSpan>(timeSpanMapper);
                options.MapType<TimeSpan?>(timeSpanMapper);

                #endregion

                options.OperationFilter<AuthorizeCheckOperationFilter>();

            });

            return ret;
        }
    }
}

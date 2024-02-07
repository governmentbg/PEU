using EAU.Web.FileUploadProtection;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionEPZEUCommonExtensions
    {
        public static IServiceCollection ConfigureFileUploadProtectionOptions(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<FileUploadProtectionOptions>(config.GetSection("EAU"));

            services.PostConfigure<FileUploadProtectionOptions>(options =>
            {
                string jsonConfiguration = options.GL_DOCUMENT_ALLOWED_FORMATS;

                options.AllowedFiles = EAUJsonSerializer.Deserialize<List<FileUploadProtectionConfiguration>>(jsonConfiguration);
                options.AllowedFileExtentionsWithoutMagicNumbers = options.AllowedFiles.Where(f => f.HasMagicNumber.GetValueOrDefault() == false).Select(f => f.Extension).ToArray();
                options.AllowedFileExtensions = string.Format(".{0}", string.Join(", .", options.AllowedFiles.Select(el => el.Extension)));
            });

            return services;
        }
    }
}

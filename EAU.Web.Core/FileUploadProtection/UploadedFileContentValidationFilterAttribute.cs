using EAU.Web.FileUploadProtection.CustomFileFormats;
using FileSignatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EAU.Web.FileUploadProtection
{
    /// <summary>
    /// Филтър за разрешени типове файлове за качване на сървъра.
    /// Ползва Nuget package: FileSignatures.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class UploadedFileContentValidationFilterAttribute : Attribute, IAsyncResourceFilter
    {
        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            IFormFileCollection requestFiles = context.HttpContext.Request.Form.Files;

            if (requestFiles != null && requestFiles.Count > 0)
            {
                var fileUploadOptions = context.HttpContext.RequestServices.GetRequiredService<IOptionsMonitor<FileUploadProtectionOptions>>();

                context.HttpContext.Request.EnableBuffering();

                if(AreFilesContentValid(requestFiles, fileUploadOptions.CurrentValue))
                {
                    await next();
                }
                else
                {
                    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
            }
            else
            {
                await next();
            }
        }

        private bool AreFilesContentValid(IFormFileCollection requestFiles, FileUploadProtectionOptions options)
        {
            foreach (IFormFile file in requestFiles)
            {
                string uploadedFileExtention = Path.GetExtension(file.FileName).Replace(".", "");

                if (string.IsNullOrEmpty(uploadedFileExtention))
                    return false;

                var configurationForUploadedFile = options.AllowedFiles.SingleOrDefault(c => string.Compare(c.Extension, uploadedFileExtention, true) == 0);

                if (configurationForUploadedFile == null)
                    return false;

                Stream currFileStream = null;
                try
                {
                    currFileStream = file.OpenReadStream();
                    FileFormatInspector inspector = CreateFileFormatInspector();

                    FileFormat detectedUploadedFileType = inspector.DetermineFileFormat(currFileStream);
                    if (options.AllowedFileExtentionsWithoutMagicNumbers.Contains(uploadedFileExtention.ToLower()))
                    {
                        //Ако файловото разширение е на файл без магични байтове.
                        if (detectedUploadedFileType == null)
                            return true;
                        else
                        {
                            if (!configurationForUploadedFile.MimeTypes.Contains(detectedUploadedFileType.MediaType))
                                return false;
                        }
                    }
                    else
                    {
                        if (detectedUploadedFileType == null
                           || !configurationForUploadedFile.MimeTypes.Contains(detectedUploadedFileType.MediaType))
                            return false;
                    }
                }
                finally
                {
                    if(currFileStream != null)
                        currFileStream.Position = 0;
                }
            }

            return true;
        }

        private FileFormatInspector CreateFileFormatInspector()
        {
            var assembly = typeof(SwxFileFormat).GetTypeInfo().Assembly;

            var allAvilabelFileFormats = FileFormatLocator.GetFormats(assembly, true);

            FileFormatInspector inspector = new FileFormatInspector(allAvilabelFileFormats);

            return inspector;
        }
    }
}

using System.Collections.Generic;

namespace EAU.Web.FileUploadProtection
{
    public class FileUploadProtectionConfiguration
    {
        public string Extension { get; set; }

        public List<string> MimeTypes { get; set; }

        public bool? HasMagicNumber { get; set; }
    }

    public class FileUploadProtectionOptions
    {
        public string GL_DOCUMENT_ALLOWED_FORMATS { get; set; }

        public string[] AllowedFileExtentionsWithoutMagicNumbers { get; set; }

        public List<FileUploadProtectionConfiguration> AllowedFiles { get; set; }
        public string AllowedFileExtensions { get; set; }
    }
}

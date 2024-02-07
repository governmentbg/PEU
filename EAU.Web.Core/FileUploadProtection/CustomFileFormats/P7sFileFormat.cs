namespace EAU.Web.FileUploadProtection.CustomFileFormats
{
    /// <summary>
    /// Капсолира данни за файлов формат p7s (подписани имейл съобщения).
    /// Информация за p7s е взета от https://filext.com/file-extension/P7S
    /// </summary>
    public class P7sFileFormat : FileSignatures.FileFormat
    {
        public P7sFileFormat()
            : base(new byte[] { 0x30 }, "application/pkcs7-signature", "p7s")
        {
        }
    }
}

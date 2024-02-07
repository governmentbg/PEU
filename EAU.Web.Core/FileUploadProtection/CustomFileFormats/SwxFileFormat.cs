namespace EAU.Web.FileUploadProtection.CustomFileFormats
{
    /// <summary>
    /// Капсолира данни за файлов формат sxw (OpenOffice).
    /// Информация за sxw е взета от https://filext.com/file-extension/SXW
    /// </summary>
    public class SwxFileFormat : FileSignatures.FileFormat
    {
        public SwxFileFormat() :
            base(new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00 }, "application/vnd.sun.xml.writer", "sxw")
        {
        }
    }
}

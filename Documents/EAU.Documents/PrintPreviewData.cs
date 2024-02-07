using System.Xml;

namespace EAU.Documents
{
    /// <summary>
    /// Обект за данни за XSL трансформация.
    /// </summary>
    public class PrintPreviewData
    {
        /// <summary>
        /// XSL трансформация.
        /// </summary>
        public string Xslt { get; set; }

        /// <summary>
        /// Resolver за XSL трансформация.
        /// </summary>
        public XmlUrlResolver Resolver { get; set; }
    }
}

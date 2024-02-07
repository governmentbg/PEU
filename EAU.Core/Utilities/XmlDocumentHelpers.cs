using System.IO;
using System.Text;
using System.Xml;

namespace EAU.Utilities
{
    public class XmlDocumentHelpers
    {
        public static MemoryStream GetXmlDocumentStream(string xml, Encoding encoding)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = true;
            doc.LoadXml(xml);

            MemoryStream ms = new MemoryStream();

            XmlWriterSettings sett = new XmlWriterSettings();
            sett.Encoding = encoding;
            sett.Indent = false;

            using (XmlWriter wr = XmlWriter.Create(ms, sett))
            {
                doc.WriteTo(wr);
                wr.Flush();
            }

            ms.Position = 0;

            return ms;
        }

        public static MemoryStream GetXmlDocumentStreamUTF8Encoding(string xml)
        {
            return GetXmlDocumentStream(xml, new UTF8Encoding(false));
        }
    }
}

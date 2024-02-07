using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace EAU.Documents
{
    public enum DocumentModes
    {
        NewApplication = 1,
        RemovingIrregularitiesApplication = 2,
        EditDocument = 3,
        SignDocument = 4,
        EditAndSignDocument = 5,
        ViewDocument = 6,
        AdditionalApplication = 7,
        WithdrawService = 8
    }

    public class DocumentFormData
    {
        public object Form { get; set; }

        public List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumenTypeID)> AttachedDocuments { get; set; } = new List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumenTypeID)>();
    }

    public static class DocumentFormServiceExtensions
    {
        private static int DEFAULT_BUFFER_SIZE = 1024;

        public static XmlDocument SerializeDomainForm(this IDocumentFormService documentFormService, object domainForm)
        {
            XmlDocument documentXML = new XmlDocument();

            documentXML.AppendChild(documentXML.CreateXmlDeclaration("1.0", "utf-8", "no"));

            var navigator = documentXML.CreateNavigator();

            using (XmlWriter xmlWriter = navigator.AppendChild())
            {
                documentFormService.SerializeDomainForm(xmlWriter, domainForm);
            }

            return documentXML;
        }

        public static object DeserializeDomainForm(this IDocumentFormService documentFormService, XmlDocument xmlDocument)
        {
            var navigator = xmlDocument.CreateNavigator();

            using (XmlReader reader = navigator.ReadSubtree())
            {
                return documentFormService.DeserializeDomainFormAsync(reader);
            }
        }

        public static async Task<Stream> SerializeDocumentFormAsync(this IDocumentFormService documentFormService, object form, CancellationToken cancellationToken)
        {
            MemoryStream ret = new MemoryStream();

            await documentFormService.SerializeDocumentFormAsync(ret, form, cancellationToken);

            ret.Position = 0;

            return ret;
        }

        public static async Task<string> SerializeDocumentFormAsStringAsync(this IDocumentFormService documentFormService, object form, CancellationToken cancellationToken)
        {
            string ret;
            using (var stream = new MemoryStream())
            {
                await documentFormService.SerializeDocumentFormAsync(stream, form, cancellationToken);
                stream.Position = 0;
                // info: StreamReader's default encoding is UTF8NoBOM
                using (var reader = new StreamReader(stream))
                {
                    ret = reader.ReadToEnd();
                }
            }

            return ret;
        }

        public static async Task<object> DeserializeDocumentFormAsync(this IDocumentFormService documentFormService, string jsonString, CancellationToken cancellationToken)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, Encoding.UTF8, DEFAULT_BUFFER_SIZE, true))
            {
                writer.Write(jsonString);
            }

            stream.Position = 0;

            var form = await documentFormService.DeserializeDocumentFormAsync(stream, cancellationToken);
            return form;
        }
    }

    public interface IDocumentFormService
    {
        void SerializeDomainForm(XmlWriter writer, object domainForm);

        object DeserializeDomainFormAsync(XmlReader reader);

        Task SerializeDocumentFormAsync(Stream utf8Json, object form, CancellationToken cancellationToken);

        Task<object> DeserializeDocumentFormAsync(Stream jsonStream, CancellationToken cancellationToken);

        Task<DocumentFormData> TransformToDocumentFormAsync(object domainForm, CancellationToken cancellationToken);

        Task<object> TransformToDomainFormAsync(DocumentFormData documentFormData, CancellationToken cancellationToken);

        Task<List<object>> GetDocumentSignatures(XmlDocument request);

        object CreateDocumentForm();

        /// <summary>
        /// Път до елемент в XML документ (XPath), където ще бъде положен подписа (само за  XAdES Enveloped).
        /// </summary>
        string SignatureXpath { get; }

        /// <summary>
        /// Речник в който се съхраняват namespces-те, включени в SignatureXpath, като за ключове се използват съответстващите им префикси.
        /// </summary>
        Dictionary<string, string> SignatureXPathNamespaces { get; }
        
        /// <summary>
        /// Създаване на форма за отказ от услуга
        /// </summary>
        /// <param name="domainForm"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DocumentFormData> BuildWithdrawServiceFormAsync(object domainForm, CancellationToken cancellationToken);
    }
}

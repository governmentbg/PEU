using EAU.Utilities;
using System.IO;

namespace EAU.Services.DocumentProcesses.Models
{
    /// <summary>
    /// Тип на данните.
    /// </summary>
    public enum DocumentProcessContentTypes
    {
        FormJSON = 1,
        FromXML = 2,
        AttachedDocument = 3
    }

    public class DocumentProcessContent
    {
        /// <summary>
        /// Уникален идентификатор на съдържание на процес.
        /// </summary>
        [DapperColumn("document_process_content_id")]
        public long? DocumentProcessContentID { get; set; }

        /// <summary>
        /// Уникален идентификатор на данни за процеси на заявяване на услуга.
        /// </summary>
        [DapperColumn("document_process_id")]
        public long? DocumentProcessID { get; set; }

        /// <summary>
        /// Тип на данните.
        /// </summary>
        [DapperColumn("type")]
        public DocumentProcessContentTypes? Type { get; set; }

        /// <summary>
        /// Съдържание.
        /// </summary>
        [DapperColumn("content")]
        public Stream Content { get; set; }

        /// <summary>
        /// Текстово съдържание.
        /// </summary>
        [DapperColumn("text_content")]
        public string TextContent { get; set; }

        /// <summary>
        /// Размер(в байтове) на съдържанието.
        /// </summary>
        [DapperColumn("length")]
        public int? Length { get; set; }
    }
}

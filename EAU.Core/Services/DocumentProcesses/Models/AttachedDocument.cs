using EAU.Utilities;
using System;

namespace EAU.Services.DocumentProcesses.Models
{
    public class AttachedDocument
    {
        /// <summary>
        /// Уникален идентификатор на прикачен документ към процес.
        /// </summary>
        [DapperColumn("attached_document_id")]
        public long? AttachedDocumentID { get; set; }

        /// <summary>
        ///  Уникален идентификатор.
        /// </summary>
        [DapperColumn("attached_document_guid")]
        public Guid? AttachedDocumentGuid { get; set; }

        /// <summary>
        /// Уникален идентификатор на данни за процеси на заявяване на услуга.
        /// </summary>
        [DapperColumn("document_process_id")]
        public long? DocumentProcessID { get; set; }

        /// <summary>
        /// Уникален идентификатор на съдържание на прикачен документ.
        /// </summary>
        [DapperColumn("document_process_content_id")]
        public long? DocumentProcessContentID { get; set; }

        /// <summary>
        /// Уникален идентификатор на вид документ.
        /// </summary>
        [DapperColumn("document_type_id")]
        public int? DocumentTypeID { get; set; }

        /// <summary>
        /// Описание на документа.
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }

        /// <summary>
        /// MIME тип на документа.
        /// </summary>
        [DapperColumn("mime_type")]
        public string MimeType { get; set; }

        /// <summary>
        /// Име на файла.
        /// </summary>
        [DapperColumn("file_name")]
        public string FileName { get; set; }

        /// <summary>
        /// HTML съдържание на документ по шаблон.
        /// </summary>
        [DapperColumn("html_template_content")]
        public string HtmlContent { get; set; }

        /// <summary>
        /// Идентификатор на заявката за подписване в модула за подписване.
        /// </summary>
        [DapperColumn("signing_guid")]
        public Guid? SigningGuid { get; set; }

        public DocumentProcessContent Content { get; set; }
    }
}

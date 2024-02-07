using EAU.Utilities;
using System;
using System.Collections.Generic;

namespace EAU.Services.DocumentProcesses.Models
{
    public enum ProcessStatuses
    {
        /// <summary>
        /// В процес на подаване.
        /// </summary>
        InProcess = 1,

        /// <summary>
        /// В преоцес на подписване.
        /// </summary>
        Signing = 2,

        /// <summary>
        /// Изпраща се
        /// </summary>
        Sending = 4,

        /// <summary>
        /// Прието
        /// </summary>
        Accepted = 5,

        /// <summary>
        /// Грешка при приемане
        /// </summary>
        ErrorInAccepting = 6,

        /// <summary>
        /// Регистрирано
        /// </summary>
        Registered = 7,

        /// <summary>
        /// Не регистрирано
        /// </summary>
        NotRegistered = 8
    }
       
    public enum DocumentProcessTypes
    {
        Portal = 1,
        BackOffice = 2,
        PortalAdditionalApp = 3
    }

    public enum DocumentProcessModes
    {
        Read = 1,
        Write = 2,
        Sign = 3,
        WriteAndSign = 4
    }

    /// <summary>
    /// Процеси на заявяване на услуга
    /// </summary>
    public class DocumentProcess
    {
        /// <summary>
        /// Уникален идентификатор на данни за процеси на заявяване на услуга.
        /// </summary>
        [DapperColumn("document_process_id")]
        public long? DocumentProcessID { get; set; }

        /// <summary>
        /// Идентификатор на заявителя.
        /// </summary>
        [DapperColumn("applicant_id")]
        public int? ApplicantID { get; set; }

        /// <summary>
        /// Идентификатор на види документ инициирал процеса.
        /// </summary>
        [DapperColumn("document_type_id")]
        public int? DocumentTypeID { get; set; }

        /// <summary>
        /// Идентификатор на услугата инициирала процеса.
        /// </summary>
        [DapperColumn("service_id")]
        public int? ServiceID { get; set; }

        /// <summary>
        /// Статус на пакета.
        /// </summary>
        [DapperColumn("status")]
        public ProcessStatuses? Status { get; set; }

        /// <summary>
        /// Мод в който е вдигнат процеса.
        /// </summary>
        [DapperColumn("mode")]
        public DocumentProcessModes? Mode { get; set; }

        /// <summary>
        /// Тип на процеса.
        /// </summary>
        [DapperColumn("type")]
        public DocumentProcessTypes? Type { get; set; }

        /// <summary>
        /// Допълнителни данни описващи заявленията.
        /// </summary>      
        [DapperColumn("additional_data")]
        public AdditionalData AdditionalData { get; set; }

        /// <summary>
        /// Идентификатор на заявката за подписване в модула за подписване.
        /// </summary>
        [DapperColumn("signing_guid")]
        public Guid? SigningGuid { get; set; }

        /// <summary>
        /// Съобщение за грешка при обработката на процеса.
        /// </summary>
        [DapperColumn("error_message")]
        public string ErrorMessage { get; set; }

        /// <summary>
        /// УРИ на преписка.
        /// </summary>
        [DapperColumn("case_file_uri")]
        public string CaseFileURI { get; set; }

        /// <summary>
        /// УРИ на съобщение че потвърждаването не се получава.
        /// </summary>
        [DapperColumn("not_acknowledged_message_uri")]
        public string NotAcknowledgedMessageURI { get; set; }


        /// <summary>
        /// Уникален идентификатор на заявката от Backoffice системата.
        /// </summary>
        [DapperColumn("request_id")]
        public string RequestID { get; set; }

        /// <summary>
        /// Дата на създаване.
        /// </summary>
        [DapperColumn("created_on")]
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Съдържание на пакети за вписване.
        /// </summary>
        public List<DocumentProcessContent> ProcessContents { get; set; }

        public List<AttachedDocument> AttachedDocuments { get; set; }
    }
}

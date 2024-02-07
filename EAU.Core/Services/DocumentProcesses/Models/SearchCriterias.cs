using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Services.DocumentProcesses.Models
{
    public class DocumentProcessSearchCriteria
    {
        /// <summary>
        /// Уникален идентификатор на данни за процеси на заявяване на услуга.
        /// </summary>       
        public long? DocumentProcessID { get; set; }

        /// <summary>
        /// Идентификатор на услугата инициирала процеса.
        /// </summary>
        public int? ServiceID { get; set; }

        /// <summary>
        /// Търсене по CIN на заявителя.
        /// </summary>
        public int? ApplicantCIN { get; set; }

        public string RequestID { get; set; }

        public Guid? SigningGiud { get; set; }

        /// <summary>
        /// Тип на документния процес.
        /// </summary>
        public DocumentProcessTypes? DocumentProcessType { get; set; }

        public DocumentProcessLoadOption LoadOption { get; set; }
    }

    public class DocumentProcessLoadOption
    {
        /// <summary>
        /// Флаг за зареждане на документи на процеса.
        /// </summary>
        public bool? LoadAttachedDocument { get; set; }

        /// <summary>
        /// Флаг за Json съдържанието на заявлението.
        /// </summary>
        public bool? LoadFormJsonContent { get; set; }

        /// <summary>
        /// Флаг за Xml съдържанието на заявлението.
        /// </summary>
        public bool? LoadFormXmlContent { get; set; }

        /// <summary>
        /// Флаг за всички съдържания на пакета.
        /// </summary>
        public bool? LoadAllContents { get; set; }

        public bool? LoadWithLock { get; set; }

    }

    public class AttachedDocumentSearchCriteria
    {
        /// <summary>
        /// Уникален идентификатор на прикачен документ към процес.
        /// </summary>       
        public long? AttachedDocumentID { get; set; }

        /// <summary>
        /// Уникален идентификатор на данни за процеси на заявяване на услуга.
        /// </summary>       
        public long? DocumentProcessID { get; set; }

        public AttachedDocumentLoadOption LoadOption { get; set; }

        public Guid? SignGuid { get; set; }
    }

    public class AttachedDocumentLoadOption
    {
        /// <summary>
        /// Флаг за зареждане на пакети за вписване.
        /// </summary>
        public bool LoadContent { get; set; }
    }

    public class DocumentProcessContentSearchCriteria
    {
        /// <summary>
        /// Уникален идентификатор на данни за процеси на заявяване на услуга.
        /// </summary>       
        public List<long> DocumentProcessIDs { get; set; }

        /// <summary>
        /// Тип на данните.
        /// </summary>      
        public DocumentProcessContentTypes? Type { get; set; }

        public DocumentProcessContentLoadOption LoadOption { get; set; }
    }

    /// <summary>
    /// Критерии за търсене на данни за процеси на заявяване на услуга.
    /// </summary>
    public class DocumentProcessContentLoadOption
    {
        /// <summary>
        /// Флаг за зареждане на пакети за вписване.
        /// </summary>
        public bool LoadContent { get; set; }
    }
}

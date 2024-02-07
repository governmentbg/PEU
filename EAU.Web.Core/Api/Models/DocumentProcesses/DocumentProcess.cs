using EAU.Services.DocumentProcesses.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace EAU.Web.Api.Models.DocumentProcesses
{
    public class DocumentProcess
    {
        /// <summary>
        /// Уникален идентификатор на данни за процеси на заявяване на услуга.
        /// </summary>
        public long? DocumentProcessID { get; set; }

        /// <summary>
        /// Идентификатор на види документ инициирал процеса.
        /// </summary>   
        public int? DocumentTypeID { get; set; }

        /// <summary>
        /// Идентификатор на услугата инициирала процеса.
        /// </summary>      
        public int? ServiceID { get; set; }

        /// <summary>
        /// Статус на пакета
        /// </summary>
        public ProcessStatuses? Status { get; set; }

        /// <summary>
        /// Мод в който е вдигнат процеса
        /// </summary>     
        public DocumentProcessModes? Mode { get; set; }

        /// <summary>
        /// Допълнителни данни описващи заявленията.
        /// </summary>       
        public AdditionalData AdditionalData { get; set; }

        /// <summary>
        /// Идентификатор на заявката за подписване в модула за подписване.
        /// </summary>    
        public Guid? SigningGuid { get; set; }

        /// <summary>
        /// Съобщение за грешка при обработката на процеса
        /// </summary>      
        public string ErrorMessage { get; set; }

        /// <summary>
        /// УРИ на преписка
        /// </summary>       
        public string CaseFileURI { get; set; }

        /// <summary>
        /// УРИ на съобщение че потвърждаването не се получава
        /// </summary>       
        public string NotAcknowledgedMessageURI { get; set; }

        /// <summary>
        /// Дата на създаване.
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Съдържание
        /// </summary>
        public JsonElement? Form { get; set; }

        /// <summary>
        /// Фла указващ, дали има промяна в номенклатурите след създаване на черновата.
        /// </summary>
        public bool HasChangesInApplicationsNomenclature { get; set; }

        /// <summary>
        /// Фла указващ, дали е промене заявителя.
        /// </summary>
        public bool HasChangedApplicant { get; set; }

        /// <summary>
        /// Прикачени документи към заявлението
        /// </summary>
        public List<AttachedDocument> AttachedDocuments { get; set; }       
    }
}

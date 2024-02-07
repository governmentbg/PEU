using EAU.Nomenclatures.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Services.ServiceInstances.Models
{
    /// <summary>
    /// Инстанция на услуга.
    /// </summary>
    public class ServiceInstance
    {
        /// <summary>
        /// Идентификатор на инстанция на услуга.
        /// </summary>
        [DapperColumn("service_instance_id")]
        public long? ServiceInstanceID { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        [DapperColumn("status")]
        public ServiceInstanceStatuses? Status { get; set; }

        /// <summary>
        /// Идентификатор на заявител.
        /// </summary>
        [DapperColumn("applicant_id")]
        public int? ApplicantID { get; set; }

        /// <summary>
        /// Дата на създаване на иснтанция на услугата.
        /// </summary>
        [DapperColumn("service_instance_date")]
        public DateTime? ServiceInstanceDate { get; set; }

        /// <summary>
        /// Идентификатор на услуга.
        /// </summary>
        [DapperColumn("service_id")]
        public int? ServiceID { get; set; }

        /// <summary>
        /// УРИ на преписка.
        /// </summary>
        [DapperColumn("case_file_uri")]
        public string CaseFileURI { get; set; }

        /// <summary>
        /// Допълнителни данни описващи услугата.
        /// </summary>
        [DapperColumn("additional_data")]
        public AdditionalData AdditionalData { get; set; }

        /// <summary>
        /// Дата на последна промяна
        /// </summary>
        [DapperColumn("updated_on")]        
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Дата на последния статус
        /// </summary>
        [DapperColumn("status_date")]
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// Услуга.
        /// </summary>
        public Service NomService { get; set; }
    }

    /// <summary>
    /// Статуси на инстанция на услуга. 1 = Текущ; 2 = Изпълнен; 3 = Прекратен;
    /// </summary>
    public enum ServiceInstanceStatuses
    {
        /// <summary>
        /// Текущ.
        /// </summary>
        InProcess = 1,

        /// <summary>
        /// Изпълнен.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// Прекратен.
        /// </summary>
        Rejected = 3
    }
}
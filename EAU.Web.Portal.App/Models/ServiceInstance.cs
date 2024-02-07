using EAU.Services.ServiceInstances.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Portal.App.Models
{
    /// <summary>
    /// Инстанция на услуга.
    /// </summary>
    public class ServiceInstance
    {
        /// <summary>
        /// Идентификатор на инстанция на услуга.
        /// </summary>
        public long? ServiceInstanceID { get; set; }

        /// <summary>
        /// Статус.
        /// </summary>
        public ServiceInstanceStatuses? Status { get; set; }

        /// <summary>
        /// Дата на създаване на инстанция на услуга.
        /// </summary>
        public DateTime? ServiceInstanceDate { get; set; }

        /// <summary>
        /// Име на услуга.
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// Идентификатор на услуга.
        /// </summary>
        public int? ServiceID { get; set; }

        /// <summary>
        /// УРИ на преписка.
        /// </summary>
        public string CaseFileURI { get; set; }

        /// <summary>
        /// Допълнителни данни описващи услугата.
        /// </summary>    
        public AdditionalData AdditionalData { get; set; }

        /// <summary>
        /// Дата на последна промяна
        /// </summary>        
        public DateTime? UpdatedOn { get; set; }
    }
}

using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Models
{
    /// <summary>
    /// Основни данни на документа заявлението за издаване на СУМПС за чужденци. Отговаря на данните в таб "Обстоятелства" 
    /// </summary>
    public class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM
    {
        /// <summary>
        /// Списък с видове документи заявени за издаване/преиздаване
        /// </summary>
        public List<IdentityDocumentType> IdentityDocumentsType { get; set; }

        /// <summary>
        /// Идентификационни данни на чужденеца
        /// </summary>
        public ForeignIdentityBasicDataVM ForeignIdentityBasicData { get; set; }

        /// <summary>
        /// Адрес на чужданеца в република България
        /// </summary>
        public PersonAddress Address { get; set; }

        /// <summary>
        /// СДВР/РПУ в което се изпълнява услугата
        /// </summary>
        public PoliceDepartment PoliceDepartment { get; set; }

        /// <summary>
        /// Документ за задгранично пътуване.
        /// Еквивалентен на Национал документ от страната чиито гражданин е лицето
        /// </summary>
        public TravelDocumentVM TravelDocument { get; set; }

        /// <summary>
        /// Променени данни за документа за задгранично пътуване
        /// </summary>
        public TravelDocumentVM NewTravelDocument { get; set; }

        /// <summary>
        /// Други гражданства на лицето, ако са налични такива
        /// </summary>
        public CitizenshipVM OtherCitizenship { get; set; }

        public bool? HasDocumentForDisabilities { get; set; }

        public string ServiceCode { get; set; }
    }
}
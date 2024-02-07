using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models.Forms
{
    public class ApplicationForIssuingDriverLicenseDataVM
    {
        public List<IdentityDocumentType> IdentificationDocuments { get; set; }

        public PersonDataExtendedVM Person { get; set; }

        public bool IsBulgarianCitizen { get; set; }

        public PersonIdentificationForeignStatut? ForeignStatut { get; set; }

        /// <summary>
        /// Документ за задгранично пътуване.
        /// Еквивалентен на Национал документ от страната чиито гражданин е лицето
        /// </summary>
        public TravelDocumentVM TravelDocument { get; set; }

        public CitizenshipVM Citizenship { get; set; }

        public string PersonFamily { get; set; }

        public string OtherNames { get; set; }

        /// <summary>
        /// Постоянен или настоящ адрес на заявителя
        /// </summary>
        public PersonAddress Address { get; set; }

        public PoliceDepartment PoliceDepartment { get; set; }

        public BIDPersonalIdentificationDocumentReceivePlace? ReceivePlace { get; set; }

        public bool? HasDocumentForDisabilities { get; set; }

        public string Phone { get; set; }

        public string ServiceCode { get; set; }
    }
}

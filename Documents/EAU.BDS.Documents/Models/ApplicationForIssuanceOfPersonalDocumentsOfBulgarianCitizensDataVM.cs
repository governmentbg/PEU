using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Models
{
    public class ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM
    {
        public List<IdentityDocumentType> IdentificationDocuments { get; set; }

        public PersonDataExtendedVM Person { get; set; }

        public PersonAddress PermanentAddress { get; set; }

        public PoliceDepartment PoliceDepartment { get; set; }

        public BIDPersonalIdentificationDocumentReceivePlace? ReceivePlace { get; set; }

        public ParentDataVM MotherData { get; set; }

        public ParentDataVM FatherData { get; set; }

        public SpouseDataVM SpouseData { get; set; }

        public string AbroadAddress { get; set; }

        public bool? HasDocumentForDisabilities { get; set; }

        public string Phone { get; set; }

        public string ServiceCode { get; set; }
    }
}
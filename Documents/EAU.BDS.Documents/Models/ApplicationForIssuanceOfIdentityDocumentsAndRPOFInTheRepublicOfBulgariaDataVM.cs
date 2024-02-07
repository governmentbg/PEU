using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Models
{
    public class ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM
    {
        /// <summary>
        /// Списък с видове документи заявени за издаване/преиздаване
        /// </summary>
        public List<IdentityDocumentType> IdentityDocumentsType { get; set; }

        public ForeignIdentityBasicDataVM ForeignIdentityBasicData { get; set; }

        public PersonAddress PermanentAddress { get; set; }

        public PersonAddress PresentAddress { get; set; }

        public IdentityDocumentForeignCitizenBasicData PreviousIdentityDocument { get; set; }

        public TravelDocumentVM TravelDocument { get; set; }

        public TravelDocumentVM NewTravelDocument { get; set; }

        public string AbroadAddress { get; set; }

        public ForeignerParentDataVM MotherData { get; set; }

        public ForeignerParentDataVM FatherData { get; set; }

        public ForeignerSpouseDataVM SpouseData { get; set; }

        public PoliceDepartment PoliceDepartment { get; set; }

        public bool? HasDocumentForDisabilities { get; set; }

        public string ServiceCode { get; set; }
    }
}

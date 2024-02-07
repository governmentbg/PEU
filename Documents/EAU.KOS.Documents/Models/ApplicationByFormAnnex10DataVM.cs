using EAU.Documents.Domain.Models;
using EAU.Documents.Models;

namespace EAU.KOS.Documents.Models
{
    public class ApplicationByFormAnnex10DataVM
    {
        public string SunauServiceUri { get; set; }

        public PersonalInformationVM PersonalInformation { get; set; }

        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public string IssuingDocument { get; set; }

        public PersonBasicDataVM PersonGrantedFromIssuingDocument { get; set; }

        public string SpecificDataForIssuingDocumentsForKOS { get; set; }

        public bool ServicesWithOuterDocumentForThirdPerson { get; set; }

        public bool? IsRecipientEntity { get; set; }

        public bool OnlyGDNPPoliceDepartment { get; set; }

        public bool IsSpecificDataForIssuingDocumentsForKOSRequired { get; set; }

        public bool? AgreementToReceiveERefusal { get; set; }

        public PersonAddress PersistedPersonAddress { get; set; }
    }
}
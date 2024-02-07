using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.PBZN.Documents.Domain.Models;

namespace EAU.PBZN.Documents.Models.Forms
{
    public class CertificateToWorkWithFluorinatedGreenhouseGassesVM : SigningDocumentFormVMBase<OfficialVM>
    {
        public AISCaseURIVM AISCaseURI { get;set; }

        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

        public string CertificateToWorkWithFluorinatedGreenhouseGassesHeader { get; set; }

        public string CertificateValidity { get; set; }

        public string CertificateToWorkWithFluorinatedGreenhouseGassesGround { get; set; }

        public string CertificateToWorkWithFluorinatedGreenhouseGassesActivities { get; set; }

        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM EntityData { get; set; }

        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM PersonData { get; set; }

        public string CertificateToWorkWithFluorinatedGreenhouseGassesPersonsGround { get; set; }

        public string IdentificationPhoto { get; set; }

        public System.DateTime? DocumentReceiptOrSigningDate { get; set; }

        public string AdministrativeBodyName { get; set; }

        public PoliceDepartment PoliceDepartment { get; set; }
    }
}
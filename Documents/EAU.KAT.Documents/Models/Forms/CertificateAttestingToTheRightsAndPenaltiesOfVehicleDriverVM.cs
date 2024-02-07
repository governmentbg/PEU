using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models.Forms
{
    /// <summary>
    ///  Удостоверение за правата и наложените наказания на водач на МПС
    /// </summary>
    public class CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverVM : SigningDocumentFormVMBase<OfficialVM>
    {
        #region Propertioes

        public AISCaseURIVM AISCaseURI { get; set; }

        public System.DateTime? DocumentReceiptOrSigningDate { get; set; }

        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

        public string CertificateNumber { get; set; }

        public string CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverHeader { get; set; }

        public string CertificateData { get; set; }

        public string CertificateData1 { get; set; }

        public ANDCertificateReason? ANDCertificateReason { get; set; }

        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public string AdministrativeBodyName { get; set; }

        public System.DateTime? ReportDate { get; set; }
       
        #endregion        
    }
}
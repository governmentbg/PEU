using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using System;

namespace EAU.PBZN.Documents.Models.Forms
{
    public class CertificateForAccidentVM : SigningDocumentFormVMBase<OfficialVM>
    {
        public AISCaseURIVM AISCaseURI { get; set; }

        public DateTime? DocumentReceiptOrSigningDate { get; set; }

        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

        public string CertificateForAccidentHeader { get; set; }

        public string CertificateForAccidentData { get; set; }

        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public DocumentMustServeToVM DocumentMustServeTo { get; set; }

        public string AdministrativeBodyName { get; set; }
    }
}
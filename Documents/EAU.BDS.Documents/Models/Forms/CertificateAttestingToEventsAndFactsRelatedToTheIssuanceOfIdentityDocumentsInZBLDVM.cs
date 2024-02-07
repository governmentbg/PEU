using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Models;
using EAU.Documents.Models;
using EAU.Documents.Models.Forms;
using System;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Models.Forms
{
    /// <summary>
    /// Документи, удостоверяващи събития и факти, свързани с издаването на български лични документи
    /// </summary>
    public class CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM : SigningDocumentFormVMBase<OfficialVM>
    {
        public AISCaseURIVM AISCaseURI { get; set; }

        public DateTime? DocumentReceiptOrSigningDate { get; set; }

        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData { get; set; }

        public ElectronicServiceApplicantVM ElectronicServiceApplicant { get; set; }

		public string CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeader { get; set; }

        public string CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDData { get; set; }

		public DocumentMustServeToVM DocumentMustServeTo { get; set; }

        public PoliceDepartment IssuingPoliceDepartment { get; set; }

        public string AdministrativeBodyName { get; set; }

        public DateTime? ReportDate { get; set; }

        public List<IdentityDocumentData> IdentityDocuments { get; set; }
    }
}
using EAU.Documents;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents
{
    public class CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverService
        : DocumentFormServiceBase<CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver, CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverVM>
    {
        public CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3125_CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "cattrapovd:CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver/cattrapovd:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"cattrapovd", "http://ereg.egov.bg/segment/R-3125"}
                };
            }
        }

        protected override void PrepareDomainDocumentInternal(CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            formDoamin.XMLDigitalSignature = new EAU.Documents.Domain.Models.XMLDigitalSignature();
        }
    }
}
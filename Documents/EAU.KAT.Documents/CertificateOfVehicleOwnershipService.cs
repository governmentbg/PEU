using EAU.Documents;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents
{
    public class CertificateOfVehicleOwnershipService
        : DocumentFormServiceBase<CertificateOfVehicleOwnership, CertificateOfVehicleOwnershipVM>
    {
        public CertificateOfVehicleOwnershipService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.CertificateOfVehicleOwnership;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3133_CertificateOfVehicleOwnership.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "covo:CertificateOfVehicleOwnership/covo:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"covo", "http://ereg.egov.bg/segment/R-3133"}
                };
            }
        }

        protected override void PrepareDomainDocumentInternal(CertificateOfVehicleOwnership formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            formDoamin.XMLDigitalSignature = new EAU.Documents.Domain.Models.XMLDigitalSignature();
        }
    }
}
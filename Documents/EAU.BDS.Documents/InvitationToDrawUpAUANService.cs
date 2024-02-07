using EAU.BDS.Documents.Domain;
using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.BDS.Documents.Models.Forms;
using EAU.BDS.Documents.XSLT;
using EAU.Documents;
using System;
using System.Collections.Generic;

namespace EAU.BDS.Documents
{
    public class InvitationToDrawUpAUANService
        : DocumentFormServiceBase<InvitationToDrawUpAUAN, InvitationToDrawUpAUANVM>
    {
        public InvitationToDrawUpAUANService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisBDS.CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLD;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3254_InvitationToDrawUpAUAN.xslt",
                    Resolver = new BDSEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "itdauan:InvitationToDrawUpAUAN/itdauan:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"itdauan", "http://ereg.egov.bg/segment/R-3254"}
                };
            }
        }

        protected override void PrepareDomainDocumentInternal(InvitationToDrawUpAUAN formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            formDoamin.XMLDigitalSignature = new EAU.Documents.Domain.Models.XMLDigitalSignature();
        }
    }
}
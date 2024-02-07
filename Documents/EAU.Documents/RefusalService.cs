using EAU.Documents.Domain;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.Documents
{
    public class RefusalService : DocumentFormServiceBase<Refusal, RefusalVM>
    {
        public RefusalService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUris.Refusal;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3137_Refusal.xsl",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "r:Refusal/r:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"r", "http://ereg.egov.bg/segment/R-3137"}
                };
            }
        }

        protected override void PrepareDomainDocumentInternal(Refusal formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            formDoamin.XMLDigitalSignature = new EAU.Documents.Domain.Models.XMLDigitalSignature();
        }
    }
}
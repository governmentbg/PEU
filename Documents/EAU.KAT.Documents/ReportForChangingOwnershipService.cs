using EAU.Documents;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents
{
    public class ReportForChangingOwnershipService : DocumentFormServiceBase<ReportForChangingOwnership, ReportForChangingOwnershipVM>
    {
        public ReportForChangingOwnershipService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ReportForChangingOwnership;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3306_ReportForChangingOwnership.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "rpfo:ReportForChangingOwnership/rpfo:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"rpfo", "http://ereg.egov.bg/segment/R-3306"}
                };
            }
        }

        protected override void PrepareDomainDocumentInternal(ReportForChangingOwnership formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            formDoamin.XMLDigitalSignature = new EAU.Documents.Domain.Models.XMLDigitalSignature();
        }
    }
}
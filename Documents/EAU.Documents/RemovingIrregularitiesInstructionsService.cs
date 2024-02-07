using EAU.Documents.Domain;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.Documents
{
    public class RemovingIrregularitiesInstructionsService : DocumentFormServiceBase<RemovingIrregularitiesInstructions, RemovingIrregularitiesInstructionsVM>
    {
        public RemovingIrregularitiesInstructionsService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUris.RemovingIrregularitiesInstructionsUri;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3010_RemovingIrregularitiesInstructions.xsl",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "rii:RemovingIrregularitiesInstructions/rii:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"rii", "http://ereg.egov.bg/segment/R-3010"}
                };
            }
        }

        protected override void PrepareDomainDocumentInternal(RemovingIrregularitiesInstructions formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            formDoamin.XMLDigitalSignature = new EAU.Documents.Domain.Models.XMLDigitalSignature();
        }
    }
}

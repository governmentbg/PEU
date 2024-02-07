using EAU.Documents.Domain;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.Documents
{
    public class DocumentsWillBeIssuedMessageService : DocumentFormServiceBase<DocumentsWillBeIssuedMessage, DocumentsWillBeIssuedMessageVM>
    {
        public DocumentsWillBeIssuedMessageService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUris.DocumentsWillBeIssuedMessage;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3102_DocumentWillBeIssuedMessage.xslt",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "dwbim:DocumentsWillBeIssuedMessage/dwbim:Signature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"dwbim", "http://ereg.egov.bg/segment/R-3102"}
                };
            }
        }
    }
}

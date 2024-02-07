using CNSys;
using EAU.Documents.Domain;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Documents
{
    public class ReceiptAcknowledgedMessageService : DocumentFormServiceBase<ReceiptAcknowledgedMessage, ReceiptAcknowledgedMessageVM>
    {
        public ReceiptAcknowledgedMessageService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected async override Task<OperationResult> InitializeDocumentFormInternalAsync(DocumentFormInitializationRequest request, CancellationToken cancellationToken)
        {
            var result = await base.InitializeDocumentFormInternalAsync(request, cancellationToken);

            if (!result.IsSuccessfullyCompleted)
            {
                return result;
            }

            return result;
        }

        protected override string DocumentTypeUri => DocumentTypeUris.ReceiptAcknowledgeMessageUri;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "ReceiptAcknowledgedMessage.xslt",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "ram:ReceiptAcknowledgedMessage/ram:Signature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"ram", "http://ereg.egov.bg/segment/0009-000019"}
                };
            }
        }
    }
}

using CNSys;
using EAU.Documents.Domain;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Documents
{
    public class ReceiptNotAcknowledgedMessageService : DocumentFormServiceBase<ReceiptNotAcknowledgedMessage, ReceiptNotAcknowledgedMessageVM>
    {
        public ReceiptNotAcknowledgedMessageService(IServiceProvider serviceProvider) : base(serviceProvider)
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

        protected override string DocumentTypeUri => DocumentTypeUris.ReceiptNotAcknowledgeMessageUri;

        protected override PrintPreviewData PrintPreviewData { get { return null; } }

        public override string SignatureXpath
        {
            get
            {
                return "rnam:ReceiptNotAcknowledgedMessage/rnam:Signature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"rnam", "http://ereg.egov.bg/segment/0009-000017"}
                };
            }
        }
    }
}

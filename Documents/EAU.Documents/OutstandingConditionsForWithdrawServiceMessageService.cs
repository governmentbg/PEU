using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents
{    
    public class OutstandingConditionsForWithdrawServiceMessageService
        : DocumentFormServiceBase<OutstandingConditionsForWithdrawServiceMessage, OutstandingConditionsForWithdrawServiceMessageVM>
    {
        public OutstandingConditionsForWithdrawServiceMessageService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUris.OutstandingConditionsForWithdrawServiceMessage;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3100_OutstandingConditionsForWithdrawServiceMessage.xslt",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "ocfsosm:OutstandingConditionsForWithdrawServiceMessage/ocfsosm:Signature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"ocfsosm", "http://ereg.egov.bg/segment/R-3119"}
                };
            }
        }
    }
}

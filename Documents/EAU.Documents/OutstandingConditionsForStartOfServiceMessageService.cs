using EAU.Documents.Domain;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.Documents
{
    public class OutstandingConditionsForStartOfServiceMessageService 
        : DocumentFormServiceBase<OutstandingConditionsForStartOfServiceMessage, OutstandingConditionsForStartOfServiceMessageVM>
    {
        public OutstandingConditionsForStartOfServiceMessageService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUris.OutstandingConditionsForStartOfServiceMessage;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3100_OutstandingConditionsForStartOfServiceMessage.xslt",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "ocfsosm:OutstandingConditionsForStartOfServiceMessage/ocfsosm:Signature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"ocfsosm", "http://ereg.egov.bg/segment/R-3100"}
                };
            }
        }
    }
}

using EAU.Documents.Domain;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.Documents
{
    public class TerminationOfServiceMessageService : DocumentFormServiceBase<TerminationOfServiceMessage, TerminationOfServiceMessageVM>
    {
        public TerminationOfServiceMessageService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUris.TerminationOfServiceMessage;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3101_TerminationOfServiceMessage.xslt",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "tosm:TerminationOfServiceMessage/tosm:Signature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"tosm", "http://ereg.egov.bg/segment/R-3101"}
                };
            }
        }
    }
}

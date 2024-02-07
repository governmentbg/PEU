using EAU.Documents.Domain;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents
{
    internal class PaymentInstructionsService : DocumentFormServiceBase<PaymentInstructions, PaymentInstructionsVM>
    {
        public PaymentInstructionsService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUris.PaymentInstructions;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3103_PaymentInstructions.xslt",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "pi:PaymentInstructions/pi:Signature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"pi", "http://ereg.egov.bg/segment/R-3103"}
                };
            }
        }
    }
}

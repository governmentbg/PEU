using EAU.Documents.Domain;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Models.Forms;
using EAU.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.Documents
{
    public class ReceiptAcknowledgedPaymentForMOIService 
        : DocumentFormServiceBase<ReceiptAcknowledgedPaymentForMOI, ReceiptAcknowledgedPaymentForMOIVM>
    {
        public ReceiptAcknowledgedPaymentForMOIService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUris.ReceiptAcknowledgedPaymentForMOI;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3122_ReceiptAcknowledgedPaymentForMOI.xslt",
                    Resolver = new CommonEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "rapfmoi:ReceiptAcknowledgedPaymentForMOI/rapfmoi:Signature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"rapfmoi", "http://ereg.egov.bg/segment/R-3122"}
                };
            }
        }
    }
}
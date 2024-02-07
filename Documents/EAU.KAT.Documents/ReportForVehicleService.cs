using EAU.Documents;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents
{
    public class ReportForVehicleService : DocumentFormServiceBase<ReportForVehicle, ReportForVehicleVM>
    {
        public ReportForVehicleService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.ReportForVehicle;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3340_ReportForVehicle.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "rfv:ReportForVehicle/rfv:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"rfv", "http://ereg.egov.bg/segment/R-3340"}
                };
            }
        }

        protected override void PrepareDomainDocumentInternal(ReportForVehicle formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            formDoamin.XMLDigitalSignature = new EAU.Documents.Domain.Models.XMLDigitalSignature();
        }
    }
}
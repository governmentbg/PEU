using EAU.Documents;
using EAU.KAT.Documents.Domain;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.KAT.Documents.Models.Forms;
using EAU.KAT.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents
{
    public class NotificationForTemporaryRegistrationPlatesService
        : DocumentFormServiceBase<NotificationForTemporaryRegistrationPlates, NotificationForTemporaryRegistrationPlatesVM>
    {
        public NotificationForTemporaryRegistrationPlatesService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisKAT.NotificationForTemporaryRegistrationPlates;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3335_NotificationForTemporaryRegistrationPlates.xslt",
                    Resolver = new KATEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "covo:NotificationForTemporaryRegistrationPlates/covo:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"covo", "http://ereg.egov.bg/segment/R-3335"}
                };
            }
        }

        protected override void PrepareDomainDocumentInternal(NotificationForTemporaryRegistrationPlates formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            formDoamin.XMLDigitalSignature = new EAU.Documents.Domain.Models.XMLDigitalSignature();
        }
    }
}
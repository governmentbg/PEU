using EAU.Documents;
using EAU.PBZN.Documents.Domain;
using EAU.PBZN.Documents.Domain.Models.Forms;
using EAU.PBZN.Documents.Models.Forms;
using EAU.PBZN.Documents.XSLT;
using System;
using System.Collections.Generic;

namespace EAU.PBZN.Documents
{
    public class CertificateToWorkWithFluorinatedGreenhouseGassesService 
        : DocumentFormServiceBase<CertificateToWorkWithFluorinatedGreenhouseGasses, CertificateToWorkWithFluorinatedGreenhouseGassesVM>
    {
        public CertificateToWorkWithFluorinatedGreenhouseGassesService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        protected override string DocumentTypeUri => DocumentTypeUrisPBZN.CertificateToWorkWithFluorinatedGreenhouseGasses;

        protected override PrintPreviewData PrintPreviewData
        {
            get
            {
                return new PrintPreviewData()
                {
                    Xslt = "R-3145_CertificateToWorkWithFluorinatedGreenhouseGasses.xslt",
                    Resolver = new PBZNEmbeddedXmlResourceResolver()
                };
            }
        }

        public override string SignatureXpath
        {
            get
            {
                return "ctwwfgg:CertificateToWorkWithFluorinatedGreenhouseGasses/ctwwfgg:XMLDigitalSignature";
            }
        }

        public override Dictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return new Dictionary<string, string>()
                {
                    {"ctwwfgg", "http://ereg.egov.bg/segment/R-3145"}
                };
            }
        }

        protected override void PrepareDomainDocumentInternal(CertificateToWorkWithFluorinatedGreenhouseGasses formDoamin, List<(byte[] Content, string Description, Guid Guid, string MimeType, string FileName, int? DocumentTypeID)> AttachedDocuments)
        {
            base.PrepareDomainDocumentInternal(formDoamin, AttachedDocuments);

            formDoamin.XMLDigitalSignature = new EAU.Documents.Domain.Models.XMLDigitalSignature();
        }
    }
}
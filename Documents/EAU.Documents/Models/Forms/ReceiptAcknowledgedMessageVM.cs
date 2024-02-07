using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models.Forms
{
    /// <summary>
    /// Потвърждаване на получаването
    /// </summary>
    public class ReceiptAcknowledgedMessageVM : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        public ElectronicServiceProviderBasicDataVM ElectronicServiceProvider
        {
            get;
            set;
        }

        public DocumentElectronicTransportType? TransportType
        {
            get;
            set;
        }
                
        public System.DateTime? ReceiptTime
        {
            get;
            set;
        }

        public ReceiptAcknowledgedMessageRegisteredByVM RegisteredBy
        {
            get;
            set;
        }

        public string CaseAccessIdentifier
        {
            get;
            set;
        }

        public ElectronicServiceApplicantVM Applicant
        {
            get;
            set;
        }

        public string ApplicationDocumentTypeName
        {
            get
            {
                return base.DocumentTypeName;
            }
        }

        public DocumentTypeURI ApplicationDocumentTypeURI
        {
            get
            {
                return base.DocumentTypeURI;
            }
        }
    }
}

using EAU.Documents.Domain.Models;
using System;

namespace EAU.Documents.Models.Forms
{
    public class ReceiptNotAcknowledgedMessageVM : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        public DocumentURI MessageURI
        {
            get;
            set;
        }

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

        public ElectronicDocumentDiscrepancyType[] Discrepancies
        {
            get;
            set;
        }

        public ElectronicServiceApplicantVM Applicant
        {
            get;
            set;
        }

        public DateTime? MessageCreationTime
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

using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models.Forms
{
    /// <summary>
    /// Уведомление за изпълнена ЕАУ
    /// </summary>
    public class DocumentsWillBeIssuedMessageVM 
        : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {       
        public ElectronicServiceProviderBasicDataVM ElectronicServiceProviderBasicData
        {
            get;
            set;
        }
        public ElectronicServiceApplicantVM ElectronicServiceApplicant
        {
            get;
            set;
        }
        public AISCaseURIVM AISCaseURI
        {
            get;
            set;
        }

        public System.DateTime? DocumentReceiptOrSigningDate
        {
            get;
            set;
        }
        public string IdentityDocumentsWillBeIssuedMessage
        {
            get;
            set;
        }
        public PoliceDepartment PoliceDepartment
        {
            get;
            set;
        }
        public System.DateTime? DeliveryDate
        {
            get;
            set;
        }
    }
}

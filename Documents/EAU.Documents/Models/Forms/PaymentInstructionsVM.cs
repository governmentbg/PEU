using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models.Forms
{
    /// <summary>
    /// Указания за заплащане
    /// </summary>
    public class PaymentInstructionsVM 
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
        public string BankName
        {
            get;
            set;
        }
        public string BIC
        {
            get;
            set;
        }
        public string IBAN
        {
            get;
            set;
        }
        public string PaymentReason
        {
            get;
            set;
        }
        public decimal Amount
        {
            get;
            set;
        }
        public string AmountCurrency
        {
            get;
            set;
        }
        public DeadlineVM DeadlineForPayment
        {
            get;
            set;
        }
        public string DeadlineMessage
        {
            get;
            set;
        }
        public string PaymentInstructionsHeader
        {
            get;
            set;
        }
        public string PepCin
        {
            get;
            set;
        }
    }
}

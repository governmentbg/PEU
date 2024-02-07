using EAU.Documents.Domain.Models;
using System;

namespace EAU.Documents.Models.Forms
{
    /// <summary>
    /// Потвърждаване за получаване на заплащане в МВР
    /// </summary>
    public class ReceiptAcknowledgedPaymentForMOIVM 
        : SigningDocumentFormVMBase<DigitalSignatureContainerVM>
    {
        public DateTime DocumentReceiptOrSigningDate
        {
            get;
            set;
        }
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
        public string ReceiptAcknowledgedPaymentMessage
        {
            get;
            set;
        }
        public string AdministrativeBodyName
        {
            get;
            set;
        }
    }
}

namespace EAU.Documents.Domain
{
    public static class DocumentTypeUris
    {       
        public const string ReceiptNotAcknowledgeMessageUri = "0010-000001";
        /// <summary>
        /// Потвърждаване на получаването
        /// </summary>
        public const string ReceiptAcknowledgeMessageUri = "0010-000002";
        public const string RemovingIrregularitiesInstructionsUri = "0010-003010";
                
        public const string IndividualAdministrativeActRefusalUri = "0010-000009";
        public const string RefusalWithoutConsideringTerminationProceedingsUri = "0010-003202";

        /// <summary>
        /// Указания за заплащане
        /// </summary>
        public const string PaymentInstructions = "0010-003103";

        /// <summary>
        /// Съобщение за неизпълнени условия за предоставяне на услугата
        /// </summary>
        public const string OutstandingConditionsForStartOfServiceMessage = "0010-003100";

        /// <summary>
        /// Уведомление за прекратяване на услуга
        /// </summary>
        public const string TerminationOfServiceMessage = "0010-003101";

        /// <summary>
        /// Уведомление за изпълнена ЕАУ
        /// </summary>
        public const string DocumentsWillBeIssuedMessage = "0010-003102";

        /// <summary>
        /// Потвърждаване за получаване на заплащане в МВР
        /// </summary>
        public const string ReceiptAcknowledgedPaymentForMOI = "0010-003122";

        /// <summary>
        /// Отказ
        /// </summary>
        public const string Refusal = "0010-003137";

        /// <summary>
        /// Съобщение за предприети действия
        /// </summary>      
        public const string ActionsTakenMessage = "0010-003149";

        /// <summary>
        /// Съобщение за предприети действия
        /// </summary>      
        public const string ApplicationForWithdrawService = "0010-003059";

        /// <summary>
        /// Съобщение за неизпълнени условия за подаване на заявление за отказ
        /// </summary>
        public const string OutstandingConditionsForWithdrawServiceMessage = "0010-003119";
    }
}

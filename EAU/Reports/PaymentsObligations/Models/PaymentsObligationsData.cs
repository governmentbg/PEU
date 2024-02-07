using EAU.Payments.Obligations.Models;
using EAU.Payments.PaymentRequests.Models;
using EAU.Payments.RegistrationsData.Models;
using EAU.Utilities;
using System;

namespace EAU.Reports.PaymentsObligations.Models
{
    public class PaymentsObligationsData
    {
        /// <summary>
        /// Тип на регистрационните данни.
        /// </summary>
        [DapperColumn("registration_data_type")]
        public RegistrationDataTypes? RegistrationDataType { get; set; }

        /// <summary>
        /// Дата на изпращане към платежен портал.
        /// </summary>
        [DapperColumn("send_date")]
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// Платена сума.
        /// </summary>
        [DapperColumn("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Допълнителни данни. : reason; boricaCode; transactionNumber; portalUrl; resultTime; clientId; hmac; data; vposResultGid; errorMessage; okCancelUrl;
        /// </summary>
        [DapperColumn("payment_request_data")]
        public AdditionalData PaymentRequestData { get; set; }

        /// <summary>
        /// Номер на плащане от външна система.
        /// </summary>
        [DapperColumn("external_portal_payment_number")]
        public string ExternalPortalPaymentNumber { get; set; }

        /// <summary>
        /// Статус на заявка за плащане: 1 = Нова; 2 = Изпратена; 3 = Платена; 4 = Отказана; 5 = Изтекла;
        /// </summary>
        [DapperColumn("status")]
        public PaymentRequestStatuses? Status { get; set; }

        /// <summary>
        /// Дата на плащане.
        /// </summary>
        [DapperColumn("pay_date")]
        public DateTime? PayDate { get; set; }

        /// <summary>
        /// Вид идентификатор на заплащащо лице: 1 = ЕГН; 2 = ЛНЧ; 3 = БУЛСТАТ;
        /// </summary>
        [DapperColumn("payer_ident_type")]
        public ObligedPersonIdentTypes? PayerIdentType { get; set; }

        /// <summary>
        /// Идентификатор на заплащащо лице.
        /// </summary>
        [DapperColumn("payer_ident")]
        public string PayerIdent { get; set; }

        /// <summary>
        /// Допълнителни данни. : serviceInstanceID, paymentInstructionURI, obigedPersonIdent, obigedPersonIdentType, isServed, discount, documentType, documentSeries, documentNumber, amount, issueDate
        /// </summary>
        [DapperColumn("obligation_data")]
        public AdditionalData ObligationData { get; set; }

        /// <summary>
        /// Вид идентификатор на задължено лице: 1 = ЕГН; 2 = ЛНЧ; 3 = БУЛСТАТ;
        /// </summary>
        [DapperColumn("obliged_person_ident_type")]
        public ObligedPersonIdentTypes? ObligedPersonIdentType { get; set; }

        /// <summary>
        /// Идентификатор на задължено лице.
        /// </summary>
        [DapperColumn("obliged_person_ident")]
        public string ObligedPersonIdent { get; set; }

        /// <summary>
        /// Система предоставила задължението - 1= КАТ, 2=НАИФ НРБЛД(БДС)
        /// </summary>
        [DapperColumn("and_source_id")]
        public ANDSourceIds? ANDSourceId { get; set; }
    }
}
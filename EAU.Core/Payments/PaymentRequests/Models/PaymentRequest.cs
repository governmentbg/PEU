using EAU.Payments.Obligations.Models;
using EAU.Payments.RegistrationsData.Models;
using EAU.Utilities;
using System;

namespace EAU.Payments.PaymentRequests.Models
{
    /// <summary>
    /// Заявка за плащане.
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        /// Идентифкатор на заявка за плащане.
        /// </summary>
        [DapperColumn("payment_request_id")]
        public long? PaymentRequestID { get; set; }

        /// <summary>
        /// Идентификатор на система за електронни разплащания.
        /// </summary>
        [DapperColumn("registration_data_id")]
        public int? RegistrationDataID { get; set; }

        /// <summary>
        /// Тип на регистрационните данни.
        /// </summary>
        [DapperColumn("registration_data_type")]
        public RegistrationDataTypes? RegistrationDataType { get; set; }

        /// <summary>
        /// Статус на заявка за плащане: 1 = Нова; 2 = Изпратена; 3 = Платена; 4 = Отказана; 5 = Изтекла;
        /// </summary>
        [DapperColumn("status")]
        public PaymentRequestStatuses? Status { get; set; }

        /// <summary>
        /// Идентифкатор за задължение.
        /// </summary>
        [DapperColumn("obligation_id")]
        public long? ObligationID { get; set; }

        /// <summary>
        /// Име на задължено лице.
        /// </summary>
        [DapperColumn("obliged_person_name")]
        public string ObligedPersonName { get; set; }

        /// <summary>
        /// Идентификатор на задължено лице.
        /// </summary>
        [DapperColumn("obliged_person_ident")]
        public string ObligedPersonIdent { get; set; }

        /// <summary>
        /// Вид идентификатор на задължено лице: 1 = ЕГН; 2 = ЛНЧ; 3 = БУЛСТАТ;
        /// </summary>
        [DapperColumn("obliged_person_ident_type")]
        public ObligedPersonIdentTypes? ObligedPersonIdentType { get; set; }

        /// <summary>
        /// Дата на изпращане към платежен портал.
        /// </summary>
        [DapperColumn("send_date")]
        public DateTime? SendDate { get; set; }

        /// <summary>
        /// Дата на плащане.
        /// </summary>
        [DapperColumn("pay_date")]
        public DateTime? PayDate { get; set; }

        /// <summary>
        /// Номер на плащане от външна система.
        /// </summary>
        [DapperColumn("external_portal_payment_number")]
        public string ExternalPortalPaymentNumber { get; set; }

        /// <summary>
        /// Платена сума.
        /// </summary>
        [DapperColumn("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Допълнителни данни. : reason; boricaCode; transactionNumber; portalUrl; resultTime; clientId; hmac; data; vposResultGid; errorMessage; okCancelUrl;
        /// </summary>
        [DapperColumn("additional_data")]
        public AdditionalData AdditionalData { get; set; }

        /// <summary>
        /// Идентификатор на заплащащо лице.
        /// </summary>
        [DapperColumn("payer_ident")]
        public string PayerIdent { get; set; }

        /// <summary>
        /// Вид идентификатор на заплащащо лице: 1 = ЕГН; 2 = ЛНЧ; 3 = БУЛСТАТ;
        /// </summary>
        [DapperColumn("payer_ident_type")]
        public ObligedPersonIdentTypes? PayerIdentType { get; set; }
    }

    /// <summary>
    /// Статуси на заявка за плащане: 1 = Нова; 2 = Изпратена; 3 = Платена; 4 = Отказана; 5 = Изтекла; 6 - Дублирана;
    /// </summary>
    public enum PaymentRequestStatuses
    {
        /// <summary>
        /// 1 = Нова;
        /// </summary>
        New = 1,

        /// <summary>
        /// 2 = Изпратена;
        /// </summary>
        Sent = 2,

        /// <summary>
        /// 3 = Платена;
        /// </summary>
        Paid = 3,

        /// <summary>
        /// 4 = Отказана;
        /// </summary>
        Cancelled = 4,

        /// <summary>
        /// 5 = Изтекла;
        /// </summary>
        Expired = 5,

        /// <summary>
        /// 6 - Дублирана
        /// </summary>
        Duplicate = 6
    }
}
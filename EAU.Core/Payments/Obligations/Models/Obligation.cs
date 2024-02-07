using EAU.Payments.PaymentRequests.Models;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.Obligations.Models
{
    /// <summary>
    /// Обект за задължение.
    /// </summary>
    public class Obligation
    {
        /// <summary>
        /// Идентифкатор за задължение.
        /// </summary>
        [DapperColumn("obligation_id")]
        public long? ObligationID { get; set; }

        /// <summary>
        /// Статус на задължение в ЕПЗЕУ: 0 = Необработено, 1 = В процес на обработка; 2 = Платен; 3 = Обработен (Уведомен е бекен-а);
        /// </summary>
        [DapperColumn("status")]
        public ObligationStatuses? Status { get; set; }

        /// <summary>
        /// Сума.
        /// </summary>
        [DapperColumn("amount")]
        public decimal? Amount { get; set; }

        /// <summary>
        /// Сума с отстъпка.
        /// </summary>
        [DapperColumn("discount_amount")]
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// При банка.
        /// </summary>
        [DapperColumn("bank_name")]
        public string BankName { get; set; }

        /// <summary>
        /// BIC.
        /// </summary>
        [DapperColumn("bic")]
        public string Bic { get; set; }

        /// <summary>
        /// IBAN.
        /// </summary>
        [DapperColumn("iban")]
        public string Iban { get; set; }

        /// <summary>
        /// Основание за плащане.
        /// </summary>
        [DapperColumn("payment_reason")]
        public string PaymentReason { get; set; }

        /// <summary>
        /// Идентификатор на ПЕП на ДАЕУ.
        /// </summary>
        [DapperColumn("pep_cin")]
        public string PepCin { get; set; }

        /// <summary>
        /// Дата, на която изтича.
        /// </summary>
        [DapperColumn("expiration_date")]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Идентификатор на заявител.
        /// </summary>
        [DapperColumn("applicant_id")]
        public int? ApplicantID { get; set; }

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
        /// Дата на задължението.
        /// </summary>
        [DapperColumn("obligation_date")]
        public DateTime? ObligationDate { get; set; }

        /// <summary>
        /// Идентификатор на задължението. Кодирана 1-ца за БДС или 2-ка за КАТ
        /// "sourceId(AndSourceIDs)|documentType|documentIdentifier"
        /// </summary>
        [DapperColumn("obligation_identifier")]
        public string ObligationIdentifier { get; set; }

        /// <summary>
        /// Вид плащане: 1 = Инстанция на услуга; 2 = Задължение на плащане към АНД;
        /// </summary>
        [DapperColumn("type")]
        public ObligationTypes? Type { get; set; }

        /// <summary>
        /// Идентификатор на инстанция на услуга.
        /// </summary>
        [DapperColumn("service_instance_id")]
        public long? ServiceInstanceID { get; set; }

        /// <summary>
        /// Идентификатор на услуга.
        /// </summary>
        [DapperColumn("service_id")]
        public int? ServiceID { get; set; }

        /// <summary>
        /// Допълнителни данни. : serviceInstanceID, paymentInstructionURI, obigedPersonIdent, obigedPersonIdentType, isServed, discount, documentType, documentSeries, documentNumber, amount, issueDate
        /// </summary>
        [DapperColumn("additional_data")]
        public AdditionalData AdditionalData { get; set; }

        /// <summary>
        /// Система предоставила задължението - 1= КАТ, 2=НАИФ НРБЛД(БДС)
        /// </summary>
        [DapperColumn("and_source_id")]
        public ANDSourceIds? ANDSourceId { get; set; }

        /// <summary>
        /// Заявки за плащане.
        /// </summary>
        public List<PaymentRequest> PaymentRequests { get; set; }
    }

    /// <summary>
    /// Видове задължения: 1 = Инстанция на услуга; 2 = Задължение на плащане към пътна полиция;
    /// </summary>
    public enum ObligationTypes
    {
        /// <summary>
        /// Инстанция на услуга.
        /// </summary>
        ServiceInstance = 1,

        /// <summary>
        /// Задължение на плащане към АНД.
        /// </summary>
        AND = 2
    }

    /// <summary>
    /// Типове документи по КАТ АНД А-Х51: 1 = Фиш; 2 = Наказателно постановление; 3 = Споразумение
    /// </summary>
    public enum KATDocumentTypes
    {
        /// <summary>
        /// Фиш.
        /// </summary>
        TICKET = 1,

        /// <summary>
        /// Наказателно постановление.
        /// </summary>
        PENAL_DECREE = 2,

        /// <summary>
        /// Споразумение.
        /// </summary>
        AGREEMENT = 3
    }

    /// <summary>
    /// Статус на задължение в ЕПЗЕУ: 0 = Необработено, 1 = В процес на обработка; 2 = Платен; 3 = Обработен (Уведомен е бекен-а);
    /// </summary>
    public enum ObligationStatuses
    {
        /// <summary>
        /// 0 = Необработено
        /// </summary>
        Pending = 0,

        /// <summary>
        /// 1 = В процес на обработка.
        /// </summary>
        InProcess = 1,

        /// <summary>
        /// 2 = Платен
        /// </summary>
        Paid = 2,

        /// <summary>
        /// 3 = Обработен (Уведомен е бекен-а)
        /// </summary>
        Processed = 3,
    }

    /// <summary>
    /// Видове идентификатори на задължено лице: 1 = ЕГН; 2 = ЛНЧ; 3 = БУЛСТАТ;
    /// </summary>
    public enum ObligedPersonIdentTypes
    {
        /// <summary>
        /// 1 = ЕГН.
        /// </summary>
        EGN = 1,

        /// <summary>
        /// 2 = ЛНЧ.
        /// </summary>
        LNC = 2,

        /// <summary>
        /// 3 = БУЛСТАТ.
        /// </summary>
        BULSTAT = 3
    }

    public enum ANDSourceIds
    {
        /// <summary>
        /// АИС АНД - КАТ
        /// </summary>
        KAT = 1,

        /// <summary>
        /// НАИФ НРБЛД
        /// </summary>
        BDS = 2
    }
}

using System;

namespace EAU.Payments.PaymentRequests.PepDaeu.Models
{
    /// <summary>
    /// Обект за комуникация с ПЕП на ДАЕУ.
    /// </summary>
    public class PaymentMessageRequest
    {
        /// <summary>
        /// идентификатор на задължението за плащане в АИС на доставчика на ЕАУ;
        /// </summary>
        public string AisPaymentId { get; set; }

        /// <summary>
        /// (*):доставчик на ЕАУ;
        /// </summary>
        public string ServiceProviderName { get; set; }

        /// <summary>
        /// (*):име на банката, в която е сметката на доставчика на ЕАУ;
        /// </summary>
        public string ServiceProviderBank { get; set; }

        /// <summary>
        /// (*):BIC код на сметката на доставчика на ЕАУ;
        /// </summary>
        public string ServiceProviderBIC { get; set; }

        /// <summary>
        /// (*):IBAN код на сметката на доставчика на ЕАУ;
        /// </summary>
        public string ServiceProviderIBAN { get; set; }

        /// <summary>
        /// (*):валута в която се плаща задължението (три символа, пр. "BGN");
        /// </summary>
        public string Currency { get; set; }


        /// <summary>
        /// код на плащане;
        /// </summary>
        public string PaymentTypeCode { get; set; }

        /// <summary>
        /// (*):сума на задължението (десетичен разделител ".", до 2 символа след десетичния разделител, пр. "2.33");
        /// </summary>
        public decimal? PaymentAmount { get; set; }

        /// <summary>
        /// (*):основание за плащане;
        /// </summary>
        public string PaymentReason { get; set; }

        /// <summary>
        /// (*):тип на идентификатора на задължено лице ("1", "2" или "3" -> ЕГН = 1, ЛНЧ = 2, БУЛСТАТ = 3);
        /// </summary>
        public int? ApplicantUinTypeId { get; set; }

        /// <summary>
        /// (*):идентификатор на задължено лице;
        /// </summary>
        public string ApplicantUin { get; set; }

        /// <summary>
        /// (*):име на задължено лице;
        /// </summary>
        public string ApplicantName { get; set; }

        /// <summary>
        /// тип на документ (референтен документ за плащане);
        /// </summary>
        public int PaymentReferenceType { get; set; }

        /// <summary>
        /// (*):номер на документ (референтен документ за плащане);
        /// </summary>
        public string PaymentReferenceNumber { get; set; }

        /// <summary>
        /// (*):дата на документ (референтен документ за плащане);
        /// </summary>
        public DateTime? PaymentReferenceDate { get; set; }

        /// <summary>
        /// дата на изтичане на заявката за плащане;
        /// </summary>
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// допълнителна информация;
        /// </summary>
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// УРИ на ЕАУ;
        /// </summary>
        public string AdministrativeServiceUri { get; set; }

        /// <summary>
        /// УРИ на доставчик на ЕАУ;
        /// </summary>
        public string AdministrativeServiceSupplierUri { get; set; }

        /// <summary>
        /// URL за нотификации при смяна на статус на задължение;
        /// </summary>
        public string AdministrativeServiceNotificationURL { get; set; }
    }
}

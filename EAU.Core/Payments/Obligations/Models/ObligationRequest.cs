using System;

namespace EAU.Payments.Obligations.Models
{
    /// <summary>
    /// Заявка за създаване на задължение.
    /// </summary>
    public class ObligationRequest
    {
        /// <summary>
        /// Вид плащане: 1 = Инстанция на услуга; 2 = Задължение на плащане към АНД;
        /// </summary>
        public ObligationTypes? Type { get; set; }

        /// <summary>
        /// Идентификатор на заявител.
        /// </summary>
        public int? ApplicantID { get; set; }

        /// <summary>
        /// Идентификатор на задължението. кодирана 4-ка за акт или 2-ка за услуги
        /// "documentType|documentSeries|documentNumber|totalAmount"
        /// "DocumentUri|ServiceInstanceID"
        /// </summary>
        public string ObligationIdentifier { get; set; }

        /// <summary>
        /// Дата на задължението.
        /// </summary>
        public DateTime? ObligationDate { get; set; }

        /// <summary>
        /// Име на задължено лице.
        /// </summary>
        public string ObligedPersonName { get; set; }

        /// <summary>
        /// Идентификатор на задължено лице.
        /// </summary>
        public string ObligedPersonIdent { get; set; }

        /// <summary>
        /// Вид идентификатор на задължено лице: 1 = ЕГН; 2 = ЛНЧ; 3 = БУЛСТАТ;
        /// </summary>
        public ObligedPersonIdentTypes? ObligedPersonIdentType { get; set; }

        /// <summary>
        /// Критерии за търсене с които да се верифицира съществуването на задължението (само за Type = 2)
        /// </summary>
        public ObligationSearchCriteria ObligationSearchCriteria { get; set; }
    }
}

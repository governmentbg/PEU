using EAU.Common.Models;
using System.Collections.Generic;

namespace EAU.Payments.RegistrationsData.Models
{
    /// <summary>
    /// Критерии за търсене на регистрационни данни на ПЕАУ в системата на платежен оператор ePay / ПЕП на ДАЕУ.
    /// </summary>
    public class RegistrationDataSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Идентификатори на регистрационните данни.
        /// </summary>
        public List<int> RegistrationDataIDs { get; set; }

        /// <summary>
        /// Тип на регистрационните данни.
        /// </summary>
        public RegistrationDataTypes? Type { get; set; }

        /// <summary>
        /// Сметка на регистрационните данни.
        /// </summary>
        public string IBAN { get; set; }

        /// <summary>
        /// КИН на ПЕАУ - клиентски номер в личните данни на търговеца.
        /// </summary>
        public string Cin { get; set; }
    }
}

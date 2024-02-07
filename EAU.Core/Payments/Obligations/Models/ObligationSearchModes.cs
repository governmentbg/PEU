using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Payments.Obligations.Models
{
    /// <summary>
    /// Режими за търсене на задължения: 1 = Задължения за "Моите плащания"; 2 = Задължения за "АНД"; 3 = Задължения за "Инстанции на услуги";
    /// </summary>
    public enum ObligationSearchModes
    {
        /// <summary>
        /// Задължения за "Моите плащания".
        /// </summary>
        MyPayments = 1,

        /// <summary>
        /// Задължения за "АНД"
        /// </summary>
        AND = 2,

        /// <summary>
        /// Задължения за "Инстанции на услуги"
        /// </summary>
        ServiceInstances = 3,
    }
}
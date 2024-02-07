using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Audit.Models
{
    /// <summary>
    /// Видове обекти: 1 = Документ; 2 = Преписка по услуга; 3 = Потребителски профил.; 4 = Средство за автентикация; 5 = Потребител;
    /// </summary>
    public enum ObjectTypes
    {
        /// <summary>
        /// Документ.
        /// </summary>
        Document = 1,

        /// <summary>
        /// Преписка по услуга.
        /// </summary>
        ServiceCaseFile = 2,

        /// <summary>
        /// Потребителски профил.
        /// </summary>
        UserProfile = 3,

        /// <summary>
        /// Средство за автентикация.
        /// </summary>
        AuthenticationMeans = 4,

        /// <summary>
        /// Потребител.
        /// </summary>
        User = 5
    }
}
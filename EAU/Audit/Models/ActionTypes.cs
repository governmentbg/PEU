using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Audit.Models
{
    /// <summary>
    /// Видове действия: 1 = Подаване; 2 = Преглед; 3 = Редакция; 4 = Логин; 5 = Добавяне; 6 = Изтриване;
    /// </summary>
    public enum ActionTypes
    {
        /// <summary>
        /// Подаване.
        /// </summary>
        Submission = 1,

        /// <summary>
        /// Преглед.
        /// </summary>
        Preview = 2,

        /// <summary>
        /// Редакция.
        /// </summary>
        Edit = 3,

        /// <summary>
        /// Логин.
        /// </summary>
        Login = 4,

        /// <summary>
        /// Добавяне.
        /// </summary>
        Add = 5,

        /// <summary>
        /// Изтриване.
        /// </summary>
        Delete = 6,
    }
}

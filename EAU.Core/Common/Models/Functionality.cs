using EAU.Utilities;
using System;

namespace EAU.Common.Models
{
    /// <summary>
    /// Клас за работа с функционалности.
    /// </summary>
    public class Functionality
    {
        /// <summary>
        /// Уникален идентификатор на функционалност.
        /// </summary>
        [DapperColumn("functionality_id")]
        public Functionalities? FunctionalityID { get; set; }

        /// <summary>
        /// Име.
        /// </summary>
        [DapperColumn("name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание.
        /// </summary>
        [DapperColumn("description")]
        public string Description { get; set; }
    }
}

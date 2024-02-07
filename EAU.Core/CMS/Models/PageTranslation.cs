using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.CMS.Models
{
    /// <summary>
    /// Превод на страница.
    /// </summary>
    public class PageTranslation
    {
        /// <summary>
        /// Идентификатор на страница.
        /// </summary>
        [DapperColumn("page_id")]
        public int? PageID { get; set; }

        /// <summary>
        /// Идентификатор на запис за език.
        /// </summary>
        [DapperColumn("language_id")]
        public int LanguageID { get; set; }

        /// <summary>
        /// Превод на заглавие на страница.
        /// </summary>
        [DapperColumn("title")]
        public string Title { get; set; }

        /// <summary>
        /// Превод на съдържание на страница.
        /// </summary>
        [DapperColumn("content")]
        public string Content { get; set; }

        /// <summary>
        /// Флаг указващ дали даддения запис е преведен за избрания език.
        /// </summary>
        [DapperColumn("is_translated")]
        public bool? IsTranslated { get; set; }
    }
}

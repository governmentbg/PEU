using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.CMS.Models
{
    /// <summary>
    /// Страница.
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Идентификатор на страница.
        /// </summary>
        [DapperColumn("page_id")]
        public int? PageID { get; set; }

        /// <summary>
        /// Код на страница.
        /// </summary>
        [DapperColumn("code")]
        public string Code { get; set; }

        /// <summary>
        /// Заглавие на страница.
        /// </summary>
        [DapperColumn("title")]
        public string Title { get; set; }

        /// <summary>
        /// Съдържание на страница.
        /// </summary>
        [DapperColumn("content")]
        public string Content { get; set; }

        /// <summary>
        /// Дата на последна редакция на страницата.
        /// </summary>
        [DapperColumn("updated_on")]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Флаг указващ дали дадения запис е преведен за избрания език.
        /// </summary>
        [DapperColumn("is_translated")]
        public bool? IsTranslated { get; set; }
    }
}

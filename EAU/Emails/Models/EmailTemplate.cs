using EAU.Utilities;

namespace EAU.Emails.Models
{
    /// <summary>
    /// Шаблон на имейл.
    /// </summary>
    public class EmailTemplate
    {
        /// <summary>
        /// Идентификатор на шаблон на имейл съобщение.
        /// </summary>
        [DapperColumn("template_id")]
        public int? TemplateID { get; set; }

        /// <summary>
        /// Тема на съобщението.
        /// </summary>
        [DapperColumn("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Тяло на съобщението.
        /// </summary>
        [DapperColumn("body")]
        public string Body { get; set; }

        /// <summary>
        /// Флаг, указващ дали съдържанието е HTML.
        /// </summary>
        [DapperColumn("is_body_html")]
        public bool IsBodyHtml { get; set; }
    }
}

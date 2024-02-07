using System.Collections.Generic;

namespace EAU.Emails.Models
{
    /// <summary>
    /// Заявка за създаване на имейл нотификация.
    /// </summary>
    public class EmailNotificationRequest
    {
        /// <summary>
        /// Идентификатор на шаблон.
        /// </summary>
        public int? TemplateID { get; set; }

        /// <summary>
        /// Списък с двойки име на параметър - стойност с която да бъде заменен.
        /// </summary>
        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// Приоритет на имейл: 1 - нисък, 2 - нормален, 3 - висок.
        /// </summary>
        public EmailPriority? Priority { get; set; }

        /// <summary>
        /// Списък от получатели на имейл;
        /// </summary>
        public EmailRecipient[] Recipients { get; set; }

        /// <summary>
        /// Флаг указващ дали да бъде изпратен отделен имейл за всеки получател.
        /// </summary>
        public bool? SeparateMailPerRecipient { get; set; }

        /// <summary>
        /// Флаг указващ дали да бъде добавена транслитерация.
        /// </summary>
        public bool? Transliterate { get; set; }

        /// <summary>
        /// Идентификатор на идемпотентна опепрация.
        /// </summary>
        public string OperationID { get; set; }
    }

    /// <summary>
    /// Обект за отговор от работа с операция за създаване на имейл нотификация.
    /// </summary>
    public class EmailNotificationResponse
    {
        /// <summary>
        /// Списък от идентификатори на имейли.
        /// </summary>
        public List<int> EmailIDs { get; set; }
    }
}

using EAU.Emails.Models;
using EAU.Nomenclatures;

namespace EAU.Emails.Cache
{
    /// <summary>
    /// Достъп до шаблон на емайл
    /// </summary>
    public interface IEmailsCache : ILoadable
    {
        /// <summary>
        /// Операция за взимане на шаблони за имейли.
        /// </summary>
        /// <param name="emailTemplateID"></param>
        /// <returns></returns>
        EmailTemplate GetEmailTemplate(int emailTemplateID);
    }
}

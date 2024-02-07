using EAU.Audit.Models;
using EAU.Common.Cache;
using EAU.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Admin.App.Models
{
    /// <summary>
    /// Събитие за одит.
    /// </summary>
    public class LogAction
    {
        /// <summary>
        /// Идентификатор на събитието.
        /// </summary>
        public long? LogActionID { get; set; }

        /// <summary>
        /// Дата на събитието - дата,час, минути, секунди и милисекунди на настъпване на събитието.
        /// </summary>
        public DateTime? LogActionDate { get; set; }

        /// <summary>
        /// Тип на обект, за който е събитието.
        /// </summary>
        public ObjectTypes? ObjectType { get; set; }

        /// <summary>
        /// Събитие, за което се записват данните за одит.
        /// </summary>
        public ActionTypes? ActionType { get; set; }

        /// <summary>
        /// Функционалност/модул през който е настъпило събитието.
        /// </summary>
        public EAU.Common.Models.Functionalities? Functionality { get; set; }

        /// <summary>
        /// Стойност на ключов атрибут на обекта - в зависимост от обекта това може да бъде УРИ на преписка, УРИ на документ или 
        /// потребителско име за обект потребител.  Ключовият атрибут за които се пази стойността е дефиниран в списъка на събитията
        /// и обектите за които се прави одитен запис.  
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Идентификатор на логин сесия.
        /// </summary>
        public Guid? LoginSessionID { get; set; }

        /// <summary>
        /// Потребител - връзка към потребителски профил в ПЕАУ, който е извършил действието.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// IP адрес на потребителя, извършващ действието.
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Допълнителна информация към одитния запис - към основния одитен запис трябва да се съхранява, допълнителна информация
        /// като например за критерии за търсене, номер на последна версия на промяна с възможност за преглед на данните от версията
        /// и други, които са специфични за конкретен обект и събитие
        /// </summary>
        public AdditionalData AdditionalData { get; set; }

        /// <summary>
        /// Имейл на потребител.
        /// </summary>
        public string UserEmail { get; set; }
    }
}

using EAU.Audit.Models;
using EAU.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EAU.Web.Admin.App.Models
{
    /// <summary>
    /// Критерии за търсене за работа с одит
    /// </summary>
    public class LogActionSearchCriteria : BasePagedSearchCriteria
    {
        /// <summary>
        /// Режим за търсене на записи в одит: Търсене в оперативна база = 1; Търсене в архивна база = 2.
        /// </summary>
        public LogActionSearchModes Mode { get; set; }

        /// <summary>
        /// Идентификатори на записи в одит.
        /// </summary>
        public int[] LogActionIDs { get; set; }

        /// <summary>
        /// Период от.
        /// </summary>
        public DateTime? LogActionDateFrom { get; set; }

        /// <summary>
        /// Период до.
        /// </summary>
        public DateTime? LogActionDateTo { get; set; }

        /// <summary>
        /// Типа обект.
        /// </summary>
        public ObjectTypes? ObjectType { get; set; }

        /// <summary>
        /// Събитие.
        /// </summary>
        public ActionTypes? ActionType { get; set; }

        /// <summary>
        /// Функционалност/модул през който е настъпило събитието..
        /// </summary>
        public Functionalities? Functionality { get; set; }

        /// <summary>
        /// Стойност на ключов атрибут на обекта - в зависимост от обекта това може да бъде УРИ на преписка, УРИ на документ или 
        /// потребителско име за обект потребител.  Ключовият атрибут за които се пази стойността е дефиниран в списъка на събитията
        /// и обектите за които се прави одитен запис.  
        public string Key { get; set; }

        /// <summary>
        /// Профил на потребителят, извършващ действието - данни за връзка към потребителски профил. Запазват се само ако потребителят се е автентикирал.
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// IP адрес.
        /// </summary>
        public string IpAddress { get; set; }
    }
}

using EAU.Common.Models;
using EAU.Utilities;
using System;

namespace EAU.Audit.Models
{
    /// <summary>
    /// Събитие за одит.
    /// </summary>
    public class LogAction
    {
        /// <summary>
        /// Идентификатор на събитието.
        /// </summary>
        [DapperColumn("log_action_id")]
        public long? LogActionID { get; set; }

        /// <summary>
        /// Дата на събитието - дата,час, минути, секунди и милисекунди на настъпване на събитието.
        /// </summary>
        [DapperColumn("log_action_date")]
        public DateTime? LogActionDate { get; set; }

        /// <summary>
        /// Тип на обект, за който е събитието.
        /// </summary>
        [DapperColumn("object_type_id")]
        public ObjectTypes? ObjectType { get; set; }

        /// <summary>
        /// Събитие, за което се записват данните за одит.
        /// </summary>
        [DapperColumn("action_type_id")]
        public ActionTypes? ActionType { get; set; }

        /// <summary>
        /// Функционалност/модул през който е настъпило събитието.
        /// </summary>
        [DapperColumn("functionality_id")]
        public Functionalities? Functionality { get; set; }

        /// <summary>
        /// Стойност на ключов атрибут на обекта - в зависимост от обекта това може да бъде УРИ на преписка, УРИ на документ или 
        /// потребителско име за обект потребител.  Ключовият атрибут за които се пази стойността е дефиниран в списъка на събитията
        /// и обектите за които се прави одитен запис.  
        /// </summary>
        [DapperColumn("key")]
        public string Key { get; set; }

        /// <summary>
        /// Идентификатор на логин сесия.
        /// </summary>
        [DapperColumn("login_session_id")]
        public Guid? LoginSessionID { get; set; }

        /// <summary>
        /// Потребител - връзка към потребителски профил в ПЕАУ, който е извършил действието.
        /// </summary>
        [DapperColumn("user_id")]
        public int? UserID { get; set; }

        /// <summary>
        /// IP адрес на потребителя, извършващ действието.
        /// </summary>
        [DapperColumn("ip_address")]
        public byte[] IpAddress { get; set; }

        /// <summary>
        /// Допълнителна информация към одитния запис - към основния одитен запис трябва да се съхранява, допълнителна информация
        /// като например за критерии за търсене, номер на последна версия на промяна с възможност за преглед на данните от версията
        /// и други, които са специфични за конкретен обект и събитие
        /// Ключове:
        /// DocumentTypeName
        /// CaseFileURI
        /// NomService.Name
        /// Email
        /// PermissionIDs
        /// identifier
        /// Issuer
        /// OtherInformation
        /// ValidTo
        /// ValidFrom
        /// Username
        /// AuthType
        /// </summary>
        [DapperColumn("additional_data")]
        public AdditionalData AdditionalData { get; set; }

        /// <summary>
        /// Имейл на потребител.
        /// </summary>
        [DapperColumn("email")]
        public string UserEmail { get; set; }
    }
}

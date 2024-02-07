using EAU.Utilities;
using System;

namespace EAU.Users.Models
{
    /// <summary>
    /// Тип на процеса: 1 = Регистриране на профил; 2 = Смяна на парола;
    /// </summary>
    public enum UserProcessTypes
    {
        /// <summary>
        /// Регистриране на профил
        /// </summary>
        Registration = 1,

        /// <summary>
        /// Смяна на парола
        /// </summary>
        ChangePassword = 2
    }

    /// <summary>
    /// Статус на процеса
    /// </summary>
    public enum UserProcessStatuses
    {
        /// <summary>
        /// Неприключил
        /// </summary>
        NotCompleted = 1,

        /// <summary>
        /// Потвърден
        /// </summary>
        Confirmed = 2,

        /// <summary>
        /// Отказан
        /// </summary>
        Cancelled = 3
    }

    /// <summary>
    /// Процес по регистрация на потребител.
    /// </summary>
    public class UserProcess
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [DapperColumn("process_id")]
        public int? ProcessID { get; set; }

        /// <summary>
        /// Идентификатор на потребителски процес.
        /// </summary>
        [DapperColumn("process_guid")]
        public Guid ProcessGuid { get; set; }

        /// <summary>
        /// Идентификатор на потребител.
        /// </summary>
        [DapperColumn("user_id")]
        public int UserID { get; set; }

        /// <summary>
        /// Дата, след която процесът е невалиден.
        /// </summary>
        [DapperColumn("invalid_after")]
        public DateTime? InvalidAfter { get; set; }

        /// <summary>
        /// Статус на процес.
        /// </summary>
        [DapperColumn("status")]
        public UserProcessStatuses? Status { get; set; }

        /// <summary>
        /// Статус на процес.
        /// </summary>
        [DapperColumn("process_type")]
        public UserProcessTypes? ProcessType { get; set; }
    }
}

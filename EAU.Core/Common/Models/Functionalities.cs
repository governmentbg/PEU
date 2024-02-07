namespace EAU.Common.Models
{
    /// <summary>
    /// Функционалности:
    /// 1 = Одит; 2 = Потребители; 3 = Подписване; 4 = Системни лимит; 5 = Портал;
    /// </summary>
    public enum Functionalities
    {
        /// <summary>
        /// Одит.
        /// </summary>
        Audit = 1,

        /// <summary>
        /// Потребители.
        /// </summary>
        Users = 2,

        /// <summary>
        /// Подписване.
        /// </summary>
        Signing = 3,

        /// <summary>
        /// Системни лимит.
        /// </summary>
        SystemLimits = 4,

        /// <summary>
        /// Портал.
        /// </summary>
        Portal = 5
    }
}

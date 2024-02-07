namespace EAU.Users.Models
{
    /// <summary>
    /// Статус на профил: 0 = Непотвърден; 1 = Активен; 2 = Неактивен; 3 = Заключен;
    /// </summary>
    public enum UserStatuses
    {
        /// <summary>
        /// Непотвърден
        /// </summary>
        NotConfirmed = 1,
        /// <summary>
        /// Активен
        /// </summary>
        Active = 2,
        /// <summary>
        /// Неактивен
        /// </summary>
        Inactive = 3,
        /// <summary>
        /// Заключен
        /// </summary>
        Locked = 4,
        /// <summary>
        /// Деактивиран
        /// </summary>
        Deactivated = 5
    }

    /// <summary>
    /// Начини на автентификация: 1 = потребителско име и парола; 2 = активна директория; 3 = електронен сертификат, 4 - НАП, 5 - Е-Авт
    /// </summary>
    public enum AuthenticationTypes
    {
        /// <summary>
        /// потребителско име и парола
        /// </summary>
        UsernamePassword = 1,

        /// <summary>
        /// активна директория
        /// </summary>
        ActiveDirectory = 2,

        /// <summary>
        /// електронен сертификат
        /// </summary>
        Certificate = 3,

        /// <summary>
        /// НАП
        /// </summary>
        NRA =  4,

        /// <summary>
        /// Е-Авт
        /// </summary>
        EAuth = 5
    }
}

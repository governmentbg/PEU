
namespace EAU.Users.Models
{
    /// <summary>
    /// Резултат от опит за автентификация на потребител.
    /// </summary>
    public class AuthenticationResult
    {
        /// <summary>
        /// Идентификатор на login сесията.
        /// </summary>
        public string LoginSessionID { get; set; }

        /// <summary>
        /// Потребител.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Флаг, указващ дали потребителското име и паролата са валидни.
        /// </summary>
        public bool InvalidUsernamePassword { get; set; }

        /// <summary>
        /// Флаг, указващ дали профилът на потребителя е заключен.
        /// </summary>
        public bool UserLocked { get; set; }

        /// <summary>
        /// Флаг, указващ дали профилът на потребителя е бил заключен в текущата операция.
        /// </summary>
        public bool UserWasLocked { get; set; }

        /// <summary>
        /// Флаг, указващ дали профилът на потребителя не е потвърден.
        /// </summary>
        public bool NotConfirmedAccount { get; set; }

        /// <summary>
        /// Флаг, указващ дали профилът на потребителя е деактивиран.
        /// </summary>
        public bool UserDeactivated { get; set; }

        /// <summary>
        /// Флаг, указващ дали сертификата с който се опитва да се автентификира, не е активиран за потребителя.
        /// </summary>
        public bool CertificateNotEnabled { get; set; }

        /// <summary>
        /// Идентификатор на сертификат.
        /// </summary>
        public int? CertificateID { get; set; }

        /// <summary>
        /// Флаг, указващ дали потребителя влиза за първи път в профила си след , като профила му е мигриран от страрата система
        /// </summary>
        public bool IsUserProfileMigrated { get; set; }

        /// <summary>
        /// Флаг, указващ дали опита за автентикация е успешен.
        /// </summary>
        public bool IsSuccess =>
            User != null && !InvalidUsernamePassword && !UserLocked && !NotConfirmedAccount && !UserDeactivated && !CertificateNotEnabled;
    }
}

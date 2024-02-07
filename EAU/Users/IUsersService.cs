using CNSys;
using EAU.Users.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users
{
    public class UserConfirmRegistrationResult
    {
        public bool IsProcessExpired { get; set; }
    }

    public class UserRegistrationResult
    {
        public bool EmailAlreadyExists { get; set; }
        public bool EmailUserStillNotActivated { get; set; }
    }

    /// <summary>
    /// Предоставя методи за работа с потребители.
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// Регистрация на потребител.
        /// </summary>
        /// <param name="email">адрес на електронна поща</param>
        /// <param name="password">парола</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult<UserRegistrationResult>> BeginPublicUserRegistrationAsync(string email, string password, X509Certificate2? certificate, string? personalIdentifier, IPAddress iPAddress, CancellationToken cancellationToken);

        /// <summary>
        /// Потвърждаване на регистрация на потребител.
        /// </summary>
        /// <param name="processGuid">идентификатор на процеса по регистрация</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult<UserConfirmRegistrationResult>> CompletePublicUserRegistrationAsync(Guid processGuid, CancellationToken cancellationToken);

        /// <summary>
        /// Отказ от регистрация на потребител.
        /// </summary>
        /// <param name="processGuid">идентификатор на процеса по регистрация</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> CancelPublicUserRegistrationAsync(Guid processGuid, CancellationToken cancellationToken);

        /// <summary>
        /// Подновяване на процес за регистрация на профил.
        /// </summary>
        /// <param name="processGuid">идентификатор на процеса по регистрация</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> RenewPublicUserRegistrationAsync(Guid processGuid, CancellationToken cancellationToken);

        /// <summary>
        /// Изпращане на заявка за промяна на парола на потребител.
        /// </summary>
        /// <param name="email">Електронна поща на потребителя,</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> SendForgottenPasswordAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Изпраща нов линк за смяна на забравена парола
        /// </summary>
        /// <param name="processGuid">Идентификатор на процеса на потребителя.</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> RenewForgottenPasswordAsync(Guid processGuid, CancellationToken cancellationToken);

        /// <summary>
        /// Потвърждаване на заявка за промяна на парола на потребител и отразяване на новата парола.
        /// </summary>
        /// <param name="processGuid"></param>
        /// <param name="changedPassword"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> CompleteForgottenPasswordAsync(Guid processGuid, string changedPassword, CancellationToken cancellationToken);

        /// <summary>
        /// Промяна на парола на потребител.
        /// </summary>
        /// <param name="userCIN">КИН на потребителя</param>
        /// <param name="email">Имейл на потребителя</param>
        /// <param name="currentPassword">Текуща парола</param>
        /// <param name="newPassword">Нова парола</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult<bool>> ChangePasswordAsync(int? userCIN, string email, string currentPassword, string newPassword, CancellationToken cancellationToken);

        /// <summary>
        /// Повторно изпращане на съобщение за регистрация на електронна поща за непотвърдени профили.
        /// </summary>
        /// <param name="email">Имейл на потребителя</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult> ResendConfirmationEmailAsync(string email, CancellationToken cancellationToken);

        /// <summary>
        /// Регистриране на вътрешен потребител
        /// </summary>
        /// <param name="email">адрес на електронна поща</param>
        /// <param name="username">потребителско име</param>
        /// <param name="isActive">дали потребителя е активен</param>
        /// <param name="permissions">списък с роли</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> RegisterInternalUserAsync(string email, string username, bool isActive, UserPermissions[] permissions, CancellationToken cancellationToken);

        /// <summary>
        /// Редактиране на вътрешен потребител
        /// </summary>
        /// <param name="userId">Идентификатор на потребител</param>
        /// <param name="email">адрес на електронна поща</param>
        /// <param name="isActive">дали е активен</param>
        /// <param name="permissions">списък с роли</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> EditInternalUserAsync(int userId, string email, bool isActive, UserPermissions[] permissions, CancellationToken cancellationToken);

        /// <summary>
        /// Редактиране на публичен потребител
        /// </summary>
        /// <param name="userCIN">КИН</param>
        /// <param name="email">адрес на електронна поща</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> EditPublicUserAsync(int userCIN, string email, CancellationToken cancellationToken);

        /// <summary>
        /// Деактивиране (забрави ме) на потребителски профил
        /// </summary>
        /// <param name="userCIN">КИН на потребителя</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> DeactivatePublicUserAsync(int userCIN, CancellationToken cancellationToken);

        /// <summary>
        /// Добавяне на средство за автентификация - ПИК на НАП.
        /// </summary>
        /// <param name="identifier">идентификатор от нап</param>
        /// <param name="currentUserId">ид на текущия потребител</param>
        /// <param name="ipAddress">IP адрес на текущото действие</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> RegisterUserNRAAuthentication(string identifier, int currentUserId, IPAddress ipAddress, CancellationToken cancellationToken);

        /// <summary>
        /// Добавяне на средство за автентификация - Е-Авт.
        /// </summary>
        /// <param name="identifier">идентификатор от Е-Авт</param>
        /// <param name="currentUserId">ид на текущия потребител</param>
        /// <param name="ipAddress">IP адрес на текущото действие</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> RegisterUserEAuthAuthentication(string identifier, int currentUserId, IPAddress ipAddress, CancellationToken cancellationToken);

        /// <summary>
        /// Добавяне на средство за автентификация - сертификат
        /// </summary>
        /// <param name="certificate">сертификат</param>
        /// <param name="currentUserId">ид на текущия потребител</param>
        /// <param name="ipAddress">IP адрес на текущото действие</param>
        /// <param name="isUserRegistration">Дали е процес на регистрация на потребител</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> CreateUserCertificateAuthenticationAsync(X509Certificate2 certificate, int currentUserId, IPAddress ipAddress, bool isUserRegistration, CancellationToken cancellationToken);

        /// <summary>
        /// Връща средствата за автентикация на потребителя, различни от потребителско име и парола.
        /// </summary>
        /// <param name="userCIN">КИН на потребителя</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<IEnumerable<UserAuthentication>> GetUsersAuthenticationTypesAsync(int userCIN, CancellationToken cancellationToken);

        /// <summary>
        /// Изтрива сертификат като средство за автентикация на конкретния потребител.
        /// </summary>
        /// <param name="userCIN">КИН на потребителя</param>
        /// <param name="userAuthenticationId">Идентификатор на средство за автентикация.</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<OperationResult<bool>> DeleteUserAuthenticationTypeAsync(int userCIN, int userAuthenticationId, CancellationToken cancellationToken);

        /// <summary>
        /// Връща средствата за автентикация на потребителя.
        /// </summary>
        /// <param name="userId">ид на потребителя</param>
        /// <param name="authType">тип на автентикацията</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<IEnumerable<UserAuthenticationInfo>> GetUserAuthentications(int userId, AuthenticationTypes authType, CancellationToken cancellationToken);

        /// <summary>
        /// Връща потребителски процес по идентификатор.
        /// </summary>
        /// <param name="processGuid">гуид на процеса</param>
        /// <param name="cancellationToken">cancellationToken</param>
        /// <returns></returns>
        Task<UserProcess> GetUserProcess(Guid processGuid, CancellationToken cancellationToken);

        /// <summary>
        /// Проверява дали сертификата е вече регистриран.
        /// </summary>
        /// <param name="certificate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult<User>> CheckCertificateAlreadyAttachedToProfileAsync(X509Certificate2 certificate, CancellationToken cancellationToken);

        /// <summary>
        /// Проверява дали identifier е вече регистриран.
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<OperationResult<User>> CheckEAuthAlreadyAttachedToProfileAsync(string identifier, CancellationToken cancellationToken);
    }
}
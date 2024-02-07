using CNSys;
using CNSys.Data;
using EAU.Audit;
using EAU.Audit.Models;
using EAU.Security;
using EAU.Users.Models;
using EAU.Users.Repositories;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users
{
    /// <summary>
    /// Интерфейс за вход/изход на потребител с различни методи.
    /// </summary>
    public interface IUsersLoginService
    {
        /// <summary>
        /// Автентикиране.
        /// </summary>
        /// <param name="email">Имейл.</param>
        /// <param name="password">Парола.</param>
        /// <param name="ipAddress">IP адрес.</param>
        /// <returns>Резултат от опит за автентификация на потребител.</returns>
        Task<AuthenticationResult> AuthenticateAsync(string email, string password, IPAddress ipAddress);

        /// <summary>
        /// Автентикиране, чрез windows-автентикация.
        /// </summary>
        /// <param name="username">Потребителско име.</param>
        /// <returns>Резултат от опит за автентификация на потребител.</returns>
        Task<AuthenticationResult> AuthenticateWindowsWeakAsync(string username);

        /// <summary>
        /// Автентикиране, чрез windows-автентикация.
        /// </summary>
        /// <param name="username">Потребителско име.</param>
        /// <param name="ipAddress">IP адрес.</param>
        /// <returns>Резултат от опит за автентификация на потребител.</returns>
        Task<AuthenticationResult> AuthenticateWindowsAsync(string username, IPAddress ipAddress);

        /// <summary>
        /// Автентикиране чрез КЕП.
        /// </summary>
        /// <param name="clientCertificate">Клиентски сертификат</param>
        /// <param name="ipAddress">IP адрес</param>
        /// <returns></returns>
        Task<AuthenticationResult> AuthenticateCertificateAsync(X509Certificate2 clientCertificate, IPAddress ipAddress, CancellationToken cancellationToken);

        /// <summary>
        /// Автентикиране чрез ПИК на НАП.
        /// </summary>
        /// <param name="identifier">идентификатор от НАП</param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<AuthenticationResult> AuthenticateNRAAsync(string identifier, IPAddress ipAddress);

        /// <summary>
        /// Автентикиране чрез Е-Авт
        /// </summary>
        /// <param name="identifier">идентификатор от Е-Авт</param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        Task<AuthenticationResult> AuthenticateEAuthAsync(string identifier, IPAddress ipAddress);

        /// <summary>
        /// Разлогване на текущата логин сесия.
        /// </summary>
        /// <returns></returns>
        Task LogoutCurrentLoginSessionAsync();

        /// <summary>
        /// Автентикиране чрез начин ResouceOwner.
        /// </summary>
        /// <param name="username">Потребителско име.</param>
        /// <param name="password">Парола.</param>
        /// <param name="ipAddress">IP адрес.</param>
        /// <returns>Резултат от опит за автентификация на потребител.</returns>
        Task<AuthenticationResult> AuthenticateResouceOwnerAuthenticationAsync(string username, string password, IPAddress ipAddress);
    };

    /// <summary>
    /// Съдържа константи за видове UserOperationActions.
    /// </summary>
    public static class UserOperationActions
    {
        public static readonly string Login = "Login";
    }

    /// <summary>
    /// Имплементация на IUsersLoginService за вход/изход на потребител с различни методи.
    /// </summary>
    public class UsersLoginService : IUsersLoginService
    {
        private readonly IOptionsMonitor<UsersOptions> _оptionsMonitor;

        public UsersLoginService(IUsersRepository usersRepository,
            IUserPasswordService passwordValidationService,
            IAuditService auditService,
            IOptionsMonitor<UsersOptions> оptionsMonitor,
            IEAUUserAccessor userAccessor,
            IUsersAuthenticationRepository usersAuthenticationRepository,
            IUserLoginSessionRepository userLoginSessionRepository,
            IUsersProcessesRepository usersProcessesRepository,
            IUserLoginAttemptRepository userLoginAttemptRepository,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IStringLocalizer localizer,
            ICertificateRepository certificateRepository)
        {
            UsersRepository = usersRepository;
            PasswordValidationService = passwordValidationService;
            AuditService = auditService;
            UserAccessor = userAccessor;

            _оptionsMonitor = оptionsMonitor;
            UsersAuthenticationRepository = usersAuthenticationRepository;
            UserLoginSessionRepository = userLoginSessionRepository;
            UserLoginAttemptRepository = userLoginAttemptRepository;
            DbContextOperationExecutor = dbContextOperationExecutor;
            Localizer = localizer;
            CertificateRepository = certificateRepository;
        }

        #region Properties

        private IUsersRepository UsersRepository { get; set; }
        private IUserLoginAttemptRepository UserLoginAttemptRepository { get; set; }

        private IUserPasswordService PasswordValidationService { get; set; }
        private IAuditService AuditService { get; set; }
        private IEAUUserAccessor UserAccessor { get; set; }
        private IUsersAuthenticationRepository UsersAuthenticationRepository { get; set; }

        private IUserLoginSessionRepository UserLoginSessionRepository { get; set; }        
        private IDbContextOperationExecutor DbContextOperationExecutor { get; set; }
        private readonly IStringLocalizer Localizer;
        private readonly ICertificateRepository CertificateRepository;

        #endregion

        public Task<AuthenticationResult> AuthenticateAsync(string email, string password, IPAddress ipAddress)
        {
            return AuthenticateInternalAsync(email, password, ipAddress, AuthenticationTypes.UsernamePassword, AuthenticateUsernamePasswordInternal);
        }

        public async Task<AuthenticationResult> AuthenticateCertificateAsync(X509Certificate2 clientCertificate, IPAddress ipAddress, CancellationToken cancellationToken)
        {
            return await AuthenticateInternalAsync(clientCertificate.SerialNumber, null, ipAddress, AuthenticationTypes.Certificate,
                async (id, pass) =>
                {
                    int? certificateID = null;
                    bool lockedCredentials = false, notConfirmedAccount = false, certificateNotEnabled = false;
                    string certHash = clientCertificate.Thumbprint.ToLower(); //ползваме хеш на целия сертификат със sha1
                    var userAuthentication = (await UsersAuthenticationRepository
                       .SearchAsync(new UserAuthenticationSearchCriteria()
                       {
                           CertificateHash = certHash
                       }, cancellationToken)).SingleOrDefault(ua => ua.IsActive);

                    User userFound = null;
                    
                    if (userAuthentication == null)
                    {    
                        certificateNotEnabled = true;
                    }
                    else
                    {
                        userFound = (await UsersRepository.SearchAsync(new UserSearchCriteria()
                        {
                            UserIDs = new List<int>() { userAuthentication.UserID.Value },
                            UserStatuses = new List<UserStatuses>() { UserStatuses.NotConfirmed, UserStatuses.Active, UserStatuses.Locked }
                        })).SingleOrDefault();

                        if (userAuthentication.IsLocked)
                            lockedCredentials = true;

                        if (userFound?.Status == UserStatuses.NotConfirmed)
                            notConfirmedAccount = true;

                        if (userAuthentication.IsActive && !userAuthentication.IsLocked)
                            certificateID = userAuthentication.CertificateID;
                    }

                    return new AuthenticationResultInternal()
                    {
                        UserLocked = lockedCredentials,
                        NotConfirmedAccount = notConfirmedAccount,
                        User = userFound,
                        UserAuthentication = userAuthentication,
                        CertificateNotEnabled = certificateNotEnabled,
                        CertificateID = certificateID
                    };
                });
        }

        public async Task<AuthenticationResult> AuthenticateResouceOwnerAuthenticationAsync(string username, string password, IPAddress ipAddress)
        {
            var authResultInternal = await AuthenticateUsernamePasswordInternal(username, password);
            bool userWasLocked = false;

            if (authResultInternal.UserLocked || authResultInternal.InvalidUsernamePassword)
            {
                var failedAttempt = await SetFailedLoginAttempt(username, AuthenticationTypes.UsernamePassword);

                if (authResultInternal.UserAuthentication != null)
                    userWasLocked = await EnsureLockedUserAfterLoginAttempt(failedAttempt, authResultInternal.UserAuthentication);
            }

            var authResult = new AuthenticationResult()
            {
                LoginSessionID = null,
                User = authResultInternal.User,
                InvalidUsernamePassword = authResultInternal.InvalidUsernamePassword,
                UserLocked = authResultInternal.UserLocked,
                UserWasLocked = userWasLocked
            };

            return authResult;
        }

        public async Task<AuthenticationResult> AuthenticateWindowsWeakAsync(string username)
        {
            var authResult = await AuthenticateInternal(username);

            return new AuthenticationResult()
            {
                User = authResult.User,
                InvalidUsernamePassword = authResult.InvalidUsernamePassword,
                UserLocked = authResult.UserLocked,
                UserDeactivated = authResult.UserDeactivated,
                NotConfirmedAccount = authResult.NotConfirmedAccount,
                CertificateNotEnabled = authResult.CertificateNotEnabled
            };

        }

        public async Task<AuthenticationResult> AuthenticateWindowsAsync(string username, IPAddress ipAddress)
        {
            return await AuthenticateInternalAsync(username, null, ipAddress, AuthenticationTypes.ActiveDirectory,
                (id, pass) =>
                {
                    return AuthenticateInternal(username);
                });
        }

        public Task<AuthenticationResult> AuthenticateNRAAsync(string identifier, IPAddress ipAddress)
            => AuthenticateExternalProviderAsync(identifier, AuthenticationTypes.NRA, ipAddress);

        public Task<AuthenticationResult> AuthenticateEAuthAsync(string identifier, IPAddress ipAddress)
            => AuthenticateExternalProviderAsync(identifier, AuthenticationTypes.EAuth, ipAddress);

        private async Task<AuthenticationResult> AuthenticateExternalProviderAsync(string identifier, AuthenticationTypes authenticationType, IPAddress ipAddress)
        {
            return await AuthenticateInternalAsync(identifier, null, ipAddress, authenticationType,
                async (id, pass) =>
                {
                    bool invalidCredentials = false, lockedCredentials = false, notConfirmedAccount = false;
                    var userAuthentication = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria()
                    {
                        Username = identifier,
                        AuthenticationType = authenticationType
                    })).SingleOrDefault();

                    User userFound = null;

                    if (userAuthentication == null)
                    {
                        invalidCredentials = true;
                    }
                    else
                    {
                        userFound = (await UsersRepository.SearchAsync(new UserSearchCriteria() { UserIDs = new List<int>() { userAuthentication.UserID.Value } })).SingleOrDefault();

                        if (userAuthentication.IsLocked)
                            lockedCredentials = true;

                        int? currentUserId = null;
                        if (!string.IsNullOrEmpty(UserAccessor.User?.ClientID) && int.TryParse(UserAccessor.User?.ClientID, out int principalId))
                            currentUserId = principalId;

                        if (userFound == null || (currentUserId != null && userFound.UserID != currentUserId.Value) || userFound.Status != UserStatuses.Active)
                        {
                            invalidCredentials = true;
                        }
                    }

                    return new AuthenticationResultInternal()
                    {
                        InvalidUsernamePassword = invalidCredentials,
                        UserLocked = lockedCredentials,
                        NotConfirmedAccount = notConfirmedAccount,
                        User = userFound,
                        UserAuthentication = userAuthentication
                    };
                });
        }

        private async Task<AuthenticationResultInternal> AuthenticateInternal(string username)
        {
            bool invalidCredentials = false, lockedCredentials = false, notConfirmedAccount = false;

            var userAuthentication = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria()
            {
                Username = username,
                AuthenticationType = AuthenticationTypes.ActiveDirectory
            }, CancellationToken.None)).SingleOrDefault();

            User userFound = null;

            if (userAuthentication == null)
            {
                invalidCredentials = true;
            }
            else
            {
                userFound = (await UsersRepository.SearchAsync(new UserSearchCriteria() { UserIDs = new List<int>() { userAuthentication.UserID.Value } })).SingleOrDefault();

                if (userFound == null || userFound.Status != UserStatuses.Active)
                {
                    invalidCredentials = true;
                }
            }

            return new AuthenticationResultInternal()
            {
                InvalidUsernamePassword = invalidCredentials,
                UserLocked = lockedCredentials,
                NotConfirmedAccount = notConfirmedAccount,
                User = userFound,
                UserAuthentication = userAuthentication
            };
        }

        private async Task<AuthenticationResult> AuthenticateInternalAsync(
            string loginIdentifier, string password, IPAddress ipAddress, AuthenticationTypes authenticationType,
            Func<string, string, Task<AuthenticationResultInternal>> userFunction)
        {
            Guid loginSessionID = Guid.NewGuid();
            bool userWasLocked = false;

            var res = await DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var authResult = await userFunction(loginIdentifier, password);

                if (!authResult.IsSuccess)
                {
                    if (authResult.UserAuthentication != null && authResult.UserAuthentication.IsLocked &&
                        DateTime.Compare(authResult.UserAuthentication.LockedUntil.Value, DateTime.Now) < 0)
                    {
                        await ClearFailedLoginAttempt(loginIdentifier, authenticationType);
                        await ClearUserAuthenticationLock(authResult.UserAuthentication);
                    }
                    else
                    {
                        var failedAttempt = await SetFailedLoginAttempt(loginIdentifier, authenticationType);

                        if (authResult.UserAuthentication != null)
                            userWasLocked = await EnsureLockedUserAfterLoginAttempt(failedAttempt, authResult.UserAuthentication);
                    }
                }
                else
                {
                    var userSessionID = UserAccessor.UserSessionID.Value;

                    await SaveLoginSession(userSessionID, loginSessionID,
                        authenticationType, ipAddress, authResult.User.UserID.Value, authResult.CertificateID);

                    await ClearFailedLoginAttempt(loginIdentifier, authenticationType);

                    if (authResult.UserAuthentication != null && authResult.UnlockUserAuthentication)
                        await ClearUserAuthenticationLock(authResult.UserAuthentication);

                    var logAction = new LogAction()
                    {
                        ObjectType = ObjectTypes.User,
                        ActionType = ActionTypes.Login,
                        Functionality = Common.Models.Functionalities.Users,
                        UserEmail = authResult.User.Email,
                        Key = authResult.User.Email,
                        LoginSessionID = loginSessionID,
                        IpAddress = ipAddress.GetAddressBytes(),
                        AdditionalData = new Utilities.AdditionalData()
                    };
                    logAction.AdditionalData.Add("AuthType", authenticationType.ToString());

                    await AuditService.CreateLogActionAsync(logAction, token);
                }

                return new OperationResult<AuthenticationResultInternal>(OperationResultTypes.SuccessfullyCompleted) { Result = authResult };
            }, CancellationToken.None);

            return new AuthenticationResult()
            {
                LoginSessionID = loginSessionID.ToString(),
                User = res.Result.User,
                InvalidUsernamePassword = res.Result.InvalidUsernamePassword,
                UserLocked = res.Result.UserLocked,
                UserWasLocked = userWasLocked,
                UserDeactivated = res.Result.UserDeactivated,
                NotConfirmedAccount = res.Result.NotConfirmedAccount,
                CertificateNotEnabled = res.Result.CertificateNotEnabled,
                IsUserProfileMigrated = res.Result.IsUserProfileMigrated
            };
        }

        public async Task LogoutCurrentLoginSessionAsync()
        {
            var loginSessionID = UserAccessor.User?.LoginSessionID;

            if (loginSessionID.HasValue)
            {
                var loginSession = (await UserLoginSessionRepository.SearchAsync(new UserLoginSessionSearchCriteria() { LoginSessionIDs = new Guid[] { loginSessionID.Value } })).SingleOrDefault();

                if (loginSession != null)
                {
                    loginSession.LogoutDate = DateTime.Now;
                    await UserLoginSessionRepository.UpdateAsync(loginSession);
                }
            }
        }        

        private async Task<UserLoginAttempt> SetFailedLoginAttempt(string loginName, AuthenticationTypes authenticationType)
        {
            IUserLoginAttemptRepository userLoginAttemptEntity = UserLoginAttemptRepository;

            var failedAttempt = (await userLoginAttemptEntity.SearchAsync(new UserLoginAttemptSearchCriteria() { LoginName = loginName }))
                                    .SingleOrDefault(la => la.AuthenticationType == authenticationType);

            if (failedAttempt != null)
            {
                failedAttempt.FailedLoginAttempts++;
                userLoginAttemptEntity.Update(failedAttempt);
            }
            else
            {
                failedAttempt = new UserLoginAttempt()
                {
                    AuthenticationType = authenticationType,
                    LoginName = loginName,
                    FailedLoginAttempts = 1
                };
                await userLoginAttemptEntity.CreateAsync(failedAttempt);
            }
            return failedAttempt;
        }

        private async Task ClearFailedLoginAttempt(string loginName, AuthenticationTypes authenticationType)
        {
            IUserLoginAttemptRepository userLoginAttemptEntity = UserLoginAttemptRepository;

            var failedAttempt = (await userLoginAttemptEntity.SearchAsync(new UserLoginAttemptSearchCriteria() { LoginName = loginName }))
                                    .SingleOrDefault(la => la.AuthenticationType == authenticationType);

            if (failedAttempt != null)
            {
                await userLoginAttemptEntity.DeleteAsync(failedAttempt);
            }
        }

        private async Task<bool> EnsureLockedUserAfterLoginAttempt(UserLoginAttempt failedLoginAttempt, UserAuthentication userAuthentication)
        {
            if (failedLoginAttempt.FailedLoginAttempts >= _оptionsMonitor.CurrentValue.USR_MAX_LOGIN_ATTEMPT_COUNT)
            {
                userAuthentication.IsLocked = true;
                userAuthentication.LockedUntil = DateTime.Now.Add(_оptionsMonitor.CurrentValue.USR_LOCK_FOR_PERIOD);

                await UsersAuthenticationRepository.UpdateAsync(userAuthentication);

                return true;
            }

            return false;
        }

        private Task SaveLoginSession(
            Guid userSessionID, Guid loginSessionID,
            AuthenticationTypes authenticationType, IPAddress ipAddress, int logedUserID, int? certificateID)
        {
            var loginSession = new UserLoginSession()
            {
                UserSessionID = userSessionID,
                LoginSessionID = loginSessionID,
                AuthenticationType = authenticationType,
                IpAddress = ipAddress.GetAddressBytes(),
                LoginDate = DateTime.Now,
                UserID = logedUserID,
                CertificateID = certificateID
            };

            return UserLoginSessionRepository.CreateAsync(loginSession);
        }

        private async Task<AuthenticationResultInternal> AuthenticateUsernamePasswordInternal(string id, string pass)
        {
            bool invalidCredentials = false;
            bool lockedCreadentials = false;
            bool notConfirmedProfile = false, deactivatedProfile = false;
            bool unlockUser = false;
            bool isPasswordHashNull = false;
            UserAuthentication userAuthentication = null;

            var userFound = (await UsersRepository.SearchAsync(new UserSearchCriteria()
            {
                Email = id
            })).SingleOrDefault();

            if (userFound == null)
            {
                invalidCredentials = true;
            }
            else if (userFound.Status == UserStatuses.NotConfirmed)
            {
                notConfirmedProfile = true;
            }
            else if (userFound.Status == UserStatuses.Inactive || userFound.Status == UserStatuses.Deactivated)
            {
                deactivatedProfile = true;
            }
            else
            {
                if (userFound.Status == UserStatuses.Locked)
                    lockedCreadentials = true;

                userAuthentication = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria()
                {
                    UserID = userFound.UserID,
                    AuthenticationType = AuthenticationTypes.UsernamePassword
                })).SingleOrDefault();

                if (userAuthentication == null)
                {
                    invalidCredentials = true;  
                }
                else if (string.IsNullOrEmpty(userAuthentication.PasswordHash))
                {
                    invalidCredentials = true;
                    isPasswordHashNull = true;
                }
                else if (userAuthentication.IsLocked)
                {
                    if (userAuthentication.LockedUntil < DateTime.Now)
                    {
                        // do check password before performing unlock
                        if (!PasswordValidationService.ValidateWithHash(pass, userAuthentication.PasswordHash))
                        {
                            invalidCredentials = true;
                        }
                        else
                        {
                            unlockUser = true;
                        }
                    }
                    else
                    {
                        lockedCreadentials = true;
                    }
                }
                else if (!PasswordValidationService.ValidateWithHash(pass, userAuthentication.PasswordHash))
                {
                    invalidCredentials = true;
                }
            }

            return new AuthenticationResultInternal()
            {
                InvalidUsernamePassword = invalidCredentials,
                UserLocked = lockedCreadentials,
                UnlockUserAuthentication = unlockUser,
                NotConfirmedAccount = notConfirmedProfile,
                UserDeactivated = deactivatedProfile,
                User = userFound,
                UserAuthentication = userAuthentication,
                IsUserProfileMigrated = isPasswordHashNull
            };
        }

        private Task ClearUserAuthenticationLock(UserAuthentication userAuthentication)
        {
            if (userAuthentication.LockedUntil > DateTime.Now)
                throw new InvalidOperationException("UserAuthentication LockedUntil has not passed.");

            userAuthentication.IsLocked = false;
            userAuthentication.LockedUntil = null;

            return UsersAuthenticationRepository.UpdateAsync(userAuthentication);
        }

        private string BuildUserProcessConfirmationUrl(string userProcessIdent, string processAction)
        {
            //TODO
            return null;

            //var epzeuPublicUrl = AppParameters.GetParameter("GL_EPZEU_PUBLIC_UI_URL").ValueString;
            //var profileConfirmationUrl = StaticPages.GetStaticPage("EP_USR_CONFIRMATION").Url;

            //return new Uri(new Uri(epzeuPublicUrl), profileConfirmationUrl.Replace("{EP_USR_PROCESS_GUID}", userProcessIdent).Replace("{EP_USR_CONFIRM_ACTION}", processAction)).ToString();
        }

        class AuthenticationResultInternal : AuthenticationResult
        {
            public bool UnlockUserAuthentication { get; set; }

            public UserAuthentication UserAuthentication { get; set; }
        }
    }
}

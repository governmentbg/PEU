using CNSys;
using CNSys.Data;
using EAU.Audit;
using EAU.Audit.Models;
using EAU.Common;
using EAU.Emails;
using EAU.Security;
using EAU.Users.Models;
using EAU.Users.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users
{
    /// <summary>
    /// Имплементация на IUsersService интерфейса.
    /// </summary>
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository UsersRepository;
        private readonly IUsersProcessesRepository UsersProcessesRepository;
        private readonly IUsersAuthenticationRepository UsersAuthenticationRepository;
        private readonly IUsersPermissionRepository UsersPermissionRepository;
        private readonly ICertificateRepository CertificateRepository;
        private readonly IDbContextOperationExecutor DbContextOperationExecutor;
        private readonly IUserPasswordService UserPasswordService;
        private readonly IEmailNotificationService EmailNotificationService;
        private readonly IAuditService AuditService;
        private readonly IEAUUserAccessor UserAccessor;
        private readonly IUserProcessLinkBuilderService UserProcessLinkBuilder;
        private readonly IOptionsMonitor<GlobalOptions> Options;
        private readonly ILogger Logger;

        private const string E_USER_DATA_NOT_FOUND = "GL_USR_0001_E";
        private const string E_REGISTRATION_PROCESS_NOT_FOUND = "GL_CANNOT_CANCEL_REGISTRATION_E";
        private const string E_REGISTRATION_PROCESS_ERROR = "GL_USR_0003_E";
        private const string E_REGISTRATION_PROCESS_EXPIRED = "GL_USR_0004_E";
        private const string E_CHANGEPASS_PROCESS_NOT_FOUND = "GL_USR_0005_E";
        private const string E_CHANGEPASS_PROCESS_ERROR = "GL_USR_0006_E";
        private const string E_CHANGEPASS_PROCESS_EXPIRED = "GL_USR_0007_E";
        private const string E_CHANGEPASS_ERROR = "GL_USR_0008_E";
        private const string E_CURRENTPASS_NOTMATCHED = "GL_USR_0009_E";
        private const string E_USER_HAS_ACTIVE_AUTHS = "GL_EMAIL_IS_ALREADY_REGISTERED_E";
        private const string E_CERT_ATTACHED_TO_OTHER_USER = "GL_USR_0012_E";
        private const string E_CERT_ALREADY_ATTACHED = "GL_USR_0013_E";
        private const string E_EMAIL_IS_ALREADY_REGISTERED = "GL_EMAIL_IS_ALREADY_REGISTERED_E";
        private const string E_CERT_ATTACHED_TO_OTHER_ACTIVE_USER = "GL_USR_CERT_ATTACHED_TO_OTHER_ACTIVE_USER_E";
        private const string E_CERT_ATTACHED_TO_OTHER_NOTCONFIRMED_USER = "GL_USR_CERT_ATTACHED_TO_OTHER_NONCONFIRMED_USER_E";
        private const string E_EAUTH_ATTACHED_TO_OTHER_ACTIVE_USER = "GL_USR_EAUTH_ATTACHED_TO_OTHER_ACTIVE_USER_E";
        private const string E_EAUTH_ATTACHED_TO_OTHER_NOTCONFIRMED_USER = "GL_USR_EAUTH_ATTACHED_TO_OTHER_NONCONFIRMED_USER_E";
        private const string E_CANNOT_CANCEL_REGISTRATION = "GL_CANNOT_CANCEL_REGISTRATION_E";

        public UsersService(IUsersRepository usersRepository,
            IUsersProcessesRepository usersProcessesRepository,
            IUsersAuthenticationRepository usersAuthenticationRepository,
            IUserPasswordService userPasswordService,
            IDbContextOperationExecutor dbContextOperationExecutor,
            IUsersPermissionRepository usersPermissionRepository,
            ICertificateRepository certificateRepository,
            IEmailNotificationService emailNotificationService,
            IAuditService auditService,
            IEAUUserAccessor userAccessor,
            IUserProcessLinkBuilderService userProcessLinkBuilder,
            IOptionsMonitor<GlobalOptions> globalOptions,
            ILogger<UsersService> logger)
        {
            UsersRepository = usersRepository;
            UsersProcessesRepository = usersProcessesRepository;
            UsersAuthenticationRepository = usersAuthenticationRepository;
            UserPasswordService = userPasswordService;
            DbContextOperationExecutor = dbContextOperationExecutor;
            UsersPermissionRepository = usersPermissionRepository;
            CertificateRepository = certificateRepository;
            EmailNotificationService = emailNotificationService;
            AuditService = auditService;
            UserAccessor = userAccessor;
            UserProcessLinkBuilder = userProcessLinkBuilder;
            Options = globalOptions;
            Logger = logger;
        }

        #region IUsersService

        public async Task<OperationResult<UserRegistrationResult>> BeginPublicUserRegistrationAsync(string email, string password, X509Certificate2? certificate, string? personalIdentifier, IPAddress iPAddress, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            var res = await DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                bool emailAlreadyRegisterd = false;
                User user = default;
                try
                {
                    user = new User
                    {
                        Email = email,
                        Status = UserStatuses.NotConfirmed,
                    };
                    await UsersRepository.CreateAsync(user);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null && ex.InnerException is Microsoft.Data.SqlClient.SqlException sqlEx)
                    {
                        if (sqlEx.Message.Length >= 10 && "101" == sqlEx.Message.Substring(0, 10).TrimEnd(' '))
                        {
                            emailAlreadyRegisterd = true;
                        }
                    }
                }

                var ret = new UserRegistrationResult
                {
                    EmailAlreadyExists = emailAlreadyRegisterd,
                    EmailUserStillNotActivated = false
                };

                if (emailAlreadyRegisterd) return ReturnErrorResult(E_EMAIL_IS_ALREADY_REGISTERED, ret);

                // email authentication
                var emailAuthentication = new UserAuthentication
                {
                    UserID = user.UserID.Value,
                    PasswordHash = UserPasswordService.CreateHash(password),
                    AuthenticationType = AuthenticationTypes.UsernamePassword,
                    IsActive = true
                };
                await UsersAuthenticationRepository.CreateAsync(emailAuthentication);

                // cert authentication
                if (certificate != null)
                {
                    var saveCertResult = await CreateUserCertificateAuthenticationAsync(certificate, user.UserID.Value, iPAddress, true, cancellationToken);

                    if (!saveCertResult.IsSuccessfullyCompleted)
                        return ReturnErrorResult(saveCertResult.Errors.First().Code, ret);
                }

                //eauth 
                if (!string.IsNullOrEmpty(personalIdentifier))
                {
                    var saveCertResult = await RegisterExternalUserAuthentication(personalIdentifier, user.UserID.Value, AuthenticationTypes.EAuth, iPAddress, true, cancellationToken);

                    if (!saveCertResult.IsSuccessfullyCompleted)
                        return ReturnErrorResult(saveCertResult.Errors.First().Code, ret);
                }

                var regProcess = new UserProcess
                {
                    ProcessType = UserProcessTypes.Registration,
                    UserID = user.UserID.Value,
                    ProcessGuid = Guid.NewGuid(),
                    Status = UserProcessStatuses.NotCompleted,
                    InvalidAfter = DateTime.Now.Add(Options.CurrentValue.GL_USR_PROCESS_CONFIRM_PERIOD)
                };
                await UsersProcessesRepository.CreateAsync(regProcess);

                var emailsResponse = await CreateRegistrationEmailAndSend(email, regProcess);

                if (!emailsResponse.IsSuccessfullyCompleted)
                    throw new Exception("Error in create mail for user registration.");

                return ReturnSuccessfulResult(ret);

            }, cancellationToken);

            if (res.Result.EmailAlreadyExists)
            {
                var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { Email = email });
                var userFound = usersFound.SingleOrDefault();

                if (userFound != null && userFound.Status == UserStatuses.NotConfirmed)
                {
                    res.Result.EmailUserStillNotActivated = true;
                }
            }

            return res;
        }

        public Task<OperationResult<UserConfirmRegistrationResult>> CompletePublicUserRegistrationAsync(Guid processGuid, CancellationToken cancellationToken)
        {
            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var processesFound = await UsersProcessesRepository.SearchAsync(new UserProcessesSearchCriteria { ProcessGuids = new List<Guid> { processGuid } }, token);
                var process = processesFound.SingleOrDefault();

                var ret = new UserConfirmRegistrationResult { IsProcessExpired = false };

                if (process == null) return ReturnErrorResult(E_REGISTRATION_PROCESS_NOT_FOUND, ret);

                if (process.Status != UserProcessStatuses.NotCompleted) return ReturnErrorResult(E_REGISTRATION_PROCESS_ERROR, ret);

                if (process.ProcessType != UserProcessTypes.Registration) return ReturnErrorResult(E_REGISTRATION_PROCESS_ERROR, ret);

                if (DateTime.Compare(DateTime.Now, process.InvalidAfter.Value) > 0)
                {
                    ret.IsProcessExpired = true;
                    return ReturnErrorResult(E_REGISTRATION_PROCESS_EXPIRED, ret);
                }

                var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { UserIDs = new List<int> { process.UserID } }, token);
                var user = usersFound.SingleOrDefault();

                if (user == null || user.Status != UserStatuses.NotConfirmed) return ReturnErrorResult(E_USER_DATA_NOT_FOUND, ret);

                user.Status = UserStatuses.Active;
                process.Status = UserProcessStatuses.Confirmed;

                await UsersRepository.UpdateAsync(user, token);
                await UsersProcessesRepository.UpdateAsync(process, token);

                return ReturnSuccessfulResult(ret);
            }, cancellationToken);
        }

        public Task<OperationResult<bool>> CancelPublicUserRegistrationAsync(Guid processGuid, CancellationToken cancellationToken)
        {
            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var processesFound = await UsersProcessesRepository.SearchAsync(new UserProcessesSearchCriteria { ProcessGuids = new List<Guid> { processGuid } }, token);
                var process = processesFound.SingleOrDefault();

                // процесът по регистрация вече е изтрит/отказан
                if (process == null) return new OperationResult<bool>() { Result = true };

                if (process.Status == UserProcessStatuses.Confirmed) return ReturnErrorResult(E_CANNOT_CANCEL_REGISTRATION);

                var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { UserIDs = new List<int> { process.UserID } }, token);
                var user = usersFound.SingleOrDefault();

                if (user == null || user.Status != UserStatuses.NotConfirmed) return ReturnErrorResult(E_USER_DATA_NOT_FOUND);

                await UsersRepository.DeleteUserDataAsync(user, token);

                return ReturnSuccessfulResult();
            }, cancellationToken);
        }

        public Task<OperationResult<bool>> RenewPublicUserRegistrationAsync(Guid processGuid, CancellationToken cancellationToken)
        {
            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var processesFound = await UsersProcessesRepository.SearchAsync(new UserProcessesSearchCriteria { ProcessGuids = new List<Guid> { processGuid } }, token);
                var process = processesFound.SingleOrDefault();

                if (process == null) return ReturnErrorResult(E_REGISTRATION_PROCESS_NOT_FOUND);

                if (process.Status != UserProcessStatuses.NotCompleted) return ReturnErrorResult(E_REGISTRATION_PROCESS_ERROR);

                if (process.ProcessType != UserProcessTypes.Registration) return ReturnErrorResult(E_REGISTRATION_PROCESS_ERROR);

                if (DateTime.Compare(DateTime.Now, process.InvalidAfter.Value) < 0) return ReturnErrorResult(E_REGISTRATION_PROCESS_EXPIRED);

                var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { UserIDs = new List<int> { process.UserID } }, token);
                var user = usersFound.SingleOrDefault();

                if (user == null || user.Status != UserStatuses.NotConfirmed) return ReturnErrorResult(E_USER_DATA_NOT_FOUND);

                process.Status = UserProcessStatuses.Cancelled;

                await UsersProcessesRepository.UpdateAsync(process, token);

                var newRegProcess = new UserProcess
                {
                    ProcessType = UserProcessTypes.Registration,
                    UserID = user.UserID.Value,
                    ProcessGuid = Guid.NewGuid(),
                    Status = UserProcessStatuses.NotCompleted,
                    InvalidAfter = DateTime.Now.Add(Options.CurrentValue.GL_USR_PROCESS_CONFIRM_PERIOD)
                };
                await UsersProcessesRepository.CreateAsync(newRegProcess);

                var emailsResponse = await CreateRegistrationEmailAndSend(user.Email, newRegProcess);

                if (!emailsResponse.IsSuccessfullyCompleted)
                    throw new Exception("Error in create mail for user registration.");

                return ReturnSuccessfulResult();
            }, cancellationToken);
        }

        public Task<OperationResult<bool>> SendForgottenPasswordAsync(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { Email = email }, token);
                var user = usersFound.SingleOrDefault();

                if (user == null) return ReturnErrorResult(E_USER_DATA_NOT_FOUND);

                var passProcess = new UserProcess
                {
                    ProcessType = UserProcessTypes.ChangePassword,
                    UserID = user.UserID.Value,
                    ProcessGuid = Guid.NewGuid(),
                    Status = UserProcessStatuses.NotCompleted,
                    InvalidAfter = DateTime.Now.Add(Options.CurrentValue.GL_USR_PROCESS_CONFIRM_PERIOD)
                };
                await UsersProcessesRepository.CreateAsync(passProcess);

                var changePassUriLink = UserProcessLinkBuilder.BuildChangePasswordLink(new Uri(Options.CurrentValue.GL_EAU_PUBLIC_APP), passProcess);

                var emailsResponse = await EmailNotificationService.CreateEmailNotificationAsync(new Emails.Models.EmailNotificationRequest
                {
                    TemplateID = 2,
                    Parameters = new Dictionary<string, string>
                    {
                        { "{ACTIVATION_LINK}", changePassUriLink.ToString() },
                        { "{DEADLINE}", passProcess.InvalidAfter.Value.ToString() }
                    },
                    Priority = Emails.Models.EmailPriority.Normal,
                    Recipients = new Emails.Models.EmailRecipient[]
                       {
                            new Emails.Models.EmailRecipient() { Address = email, DisplayName = email, Type = Emails.Models.AddressTypes.To }
                       }
                });

                if (!emailsResponse.IsSuccessfullyCompleted)
                    throw new Exception("Error in create mail for user registration.");

                return ReturnSuccessfulResult();
            }, cancellationToken);
        }

        public Task<OperationResult<bool>> RenewForgottenPasswordAsync(Guid processGuid, CancellationToken cancellationToken)
        {
            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var processesFound = await UsersProcessesRepository.SearchAsync(new UserProcessesSearchCriteria { ProcessGuids = new List<Guid> { processGuid } }, token);
                var process = processesFound.SingleOrDefault();

                if (process == null) return ReturnErrorResult(E_REGISTRATION_PROCESS_NOT_FOUND);

                if (process.Status != UserProcessStatuses.NotCompleted) return ReturnErrorResult(E_REGISTRATION_PROCESS_ERROR);

                if (process.ProcessType != UserProcessTypes.ChangePassword) return ReturnErrorResult(E_REGISTRATION_PROCESS_ERROR);

                var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { UserIDs = new List<int> { process.UserID } }, token);
                var user = usersFound.SingleOrDefault();

                if (user == null) return ReturnErrorResult(E_USER_DATA_NOT_FOUND);

                var passProcess = new UserProcess
                {
                    ProcessType = UserProcessTypes.ChangePassword,
                    UserID = user.UserID.Value,
                    ProcessGuid = Guid.NewGuid(),
                    Status = UserProcessStatuses.NotCompleted,
                    InvalidAfter = DateTime.Now.Add(Options.CurrentValue.GL_USR_PROCESS_CONFIRM_PERIOD)
                };

                await UsersProcessesRepository.CreateAsync(passProcess);

                var changePassUriLink = UserProcessLinkBuilder.BuildChangePasswordLink(new Uri(Options.CurrentValue.GL_EAU_PUBLIC_APP), passProcess);
                var emailsResponse = await EmailNotificationService.CreateEmailNotificationAsync(new Emails.Models.EmailNotificationRequest
                {
                    TemplateID = 2,
                    Parameters = new Dictionary<string, string>
                    {
                        { "{ACTIVATION_LINK}", changePassUriLink.ToString() },
                        { "{DEADLINE}", passProcess.InvalidAfter.Value.ToString() }
                    },
                    Priority = Emails.Models.EmailPriority.Normal,
                    Recipients = new Emails.Models.EmailRecipient[]
                    {
                        new Emails.Models.EmailRecipient() { Address = user.Email, DisplayName = user.Email, Type = Emails.Models.AddressTypes.To }
                    }
                });

                if (!emailsResponse.IsSuccessfullyCompleted)
                    throw new Exception("Error in create mail for user registration.");

                return ReturnSuccessfulResult();
            }, cancellationToken);
        }

        public Task<OperationResult<bool>> CompleteForgottenPasswordAsync(Guid processGuid, string changedPassword, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(changedPassword)) throw new ArgumentNullException(nameof(changedPassword));

            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var processesFound = await UsersProcessesRepository.SearchAsync(new UserProcessesSearchCriteria { ProcessGuids = new List<Guid> { processGuid } }, token);
                var process = processesFound.SingleOrDefault();

                if (process == null) return ReturnErrorResult(E_CHANGEPASS_PROCESS_NOT_FOUND);

                if (process.Status != UserProcessStatuses.NotCompleted) return ReturnErrorResult(E_CHANGEPASS_PROCESS_ERROR);

                if (process.ProcessType != UserProcessTypes.ChangePassword) return ReturnErrorResult(E_CHANGEPASS_PROCESS_ERROR);

                if (DateTime.Compare(DateTime.Now, process.InvalidAfter.Value) > 0) return ReturnErrorResult(E_CHANGEPASS_PROCESS_EXPIRED);

                var userAuthsFound = await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria { UserID = process.UserID, AuthenticationType = AuthenticationTypes.UsernamePassword });
                var passwordAuthentication = userAuthsFound.SingleOrDefault();

                if (passwordAuthentication == null) return ReturnErrorResult(E_CHANGEPASS_PROCESS_ERROR);

                passwordAuthentication.PasswordHash = UserPasswordService.CreateHash(changedPassword);
                await UsersAuthenticationRepository.UpdateAsync(passwordAuthentication, token);

                process.Status = UserProcessStatuses.Confirmed;
                await UsersProcessesRepository.UpdateAsync(process, token);

                return ReturnSuccessfulResult();

            }, cancellationToken);
        }

        public Task<OperationResult<bool>> ChangePasswordAsync(int? userCIN, string email, string currentPassword, string newPassword, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(email) && userCIN == null) throw new InvalidOperationException();
            if (string.IsNullOrEmpty(currentPassword)) throw new ArgumentNullException(nameof(currentPassword));
            if (string.IsNullOrEmpty(newPassword)) throw new ArgumentNullException(nameof(newPassword));

            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { Email = email, CIN = userCIN }, token);
                var user = usersFound.SingleOrDefault();

                if (user == null) return ReturnErrorResult(E_USER_DATA_NOT_FOUND);

                var userAuthsFound = await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria { UserID = user.UserID, AuthenticationType = AuthenticationTypes.UsernamePassword });
                var passwordAuthentication = userAuthsFound.SingleOrDefault();

                if (passwordAuthentication == null) return ReturnErrorResult(E_CHANGEPASS_ERROR);

                if (false == UserPasswordService.ValidateWithHash(currentPassword, passwordAuthentication.PasswordHash)) return ReturnErrorResult(E_CURRENTPASS_NOTMATCHED);

                passwordAuthentication.PasswordHash = UserPasswordService.CreateHash(newPassword);
                await UsersAuthenticationRepository.UpdateAsync(passwordAuthentication, token);

                return ReturnSuccessfulResult();

            }, cancellationToken);
        }

        public async Task<OperationResult> ResendConfirmationEmailAsync(string email, CancellationToken cancellationToken)
        {
            var user = (await UsersRepository.SearchAsync(new UserSearchCriteria() { Email = email })).SingleOrDefault();

            var regProcess = (await UsersProcessesRepository.SearchAsync(new UserProcessesSearchCriteria()
            {
                UserIDs = new List<int>() { user.UserID.Value }
            }))
            .SingleOrDefault(up => up.Status == UserProcessStatuses.NotCompleted && up.ProcessType == UserProcessTypes.Registration);

            if (regProcess == null)
                return new OperationResult(OperationResultTypes.CompletedWithError);

            if (DateTime.Compare(DateTime.Now, regProcess.InvalidAfter.Value) > 0)
            {
                var resultRenew = await RenewPublicUserRegistrationAsync(regProcess.ProcessGuid, cancellationToken);
                return new OperationResult(resultRenew.OperationResultType);
            }

            var registrationUriLink = UserProcessLinkBuilder.BuildConfirmRegistrationLink(new Uri(Options.CurrentValue.GL_EAU_PUBLIC_APP), regProcess);
            var cancelUriLink = UserProcessLinkBuilder.BuildCancelRegistrationLink(new Uri(Options.CurrentValue.GL_EAU_PUBLIC_APP), regProcess);

            var emailsResponse = await EmailNotificationService.CreateEmailNotificationAsync(new Emails.Models.EmailNotificationRequest
            {
                TemplateID = 1,
                Parameters = new Dictionary<string, string>
                    {
                        { "{CIN}", user.CIN.ToString() },
                        { "{ACTIVATION_LINK}", registrationUriLink.ToString() },
                        { "{CANCEL_LINK}", cancelUriLink.ToString() },
                        { "{DEADLINE}", regProcess.InvalidAfter.Value.ToString() },
                        { "{EMAIL}", email }
                    },
                Priority = Emails.Models.EmailPriority.Normal,
                Recipients = new Emails.Models.EmailRecipient[]
                   {
                            new Emails.Models.EmailRecipient() { Address = email, DisplayName = email, Type = Emails.Models.AddressTypes.To }
                   }
            });

            return new OperationResult(emailsResponse.IsSuccessfullyCompleted && emailsResponse.Result.EmailIDs.Any()
                ? OperationResultTypes.SuccessfullyCompleted : OperationResultTypes.CompletedWithError);
        }

        public Task<OperationResult<bool>> RegisterInternalUserAsync(string email, string username, bool isActive, UserPermissions[] permissions, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));

            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var user = new User
                {
                    Email = email,
                    Status = isActive ? UserStatuses.Active : UserStatuses.Inactive,
                };
                await UsersRepository.CreateAsync(user);

                var emailAuthentication = new UserAuthentication
                {
                    UserID = user.UserID.Value,
                    Username = username,
                    AuthenticationType = AuthenticationTypes.ActiveDirectory,
                    IsActive = true
                };
                await UsersAuthenticationRepository.CreateAsync(emailAuthentication);

                if (permissions != null && permissions.Any())
                {
                    foreach (var permission in permissions)
                    {
                        await UsersPermissionRepository.CreateAsync(new UserPermission { UserID = user.UserID, PermissionID = (int)permission });
                    }
                }

                return ReturnSuccessfulResult();

            }, cancellationToken);
        }

        public Task<OperationResult<bool>> EditInternalUserAsync(int userId, string email, bool isActive, UserPermissions[] permissions, CancellationToken cancellationToken)
        {
            if (userId == default) throw new ArgumentException(nameof(userId));
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));

            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { UserIDs = new List<int> { userId } }, cancellationToken);
                var user = usersFound.SingleOrDefault();

                if (user == null) return ReturnErrorResult(E_USER_DATA_NOT_FOUND);

                var userAuthenticationsCheck = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria { UserID = user.UserID }))
                    .Where(au => au.IsActive && au.AuthenticationType == AuthenticationTypes.ActiveDirectory);

                // потребителят не е вътрешен
                if (!userAuthenticationsCheck.Any()) throw new InvalidOperationException();

                var logAction = new LogAction()
                {
                    ObjectType = ObjectTypes.UserProfile,
                    ActionType = ActionTypes.Edit,
                    Functionality = Common.Models.Functionalities.Users,
                    UserID = user.UserID,
                    Key = email,
                    LoginSessionID = UserAccessor.User.LoginSessionID,
                    IpAddress = UserAccessor.RemoteIpAddress.GetAddressBytes(),
                    AdditionalData = new Utilities.AdditionalData()
                };
                logAction.AdditionalData.Add("Email", user.Email);

                user.Email = email;
                user.Status = isActive ? UserStatuses.Active : UserStatuses.Inactive;
                await UsersRepository.UpdateAsync(user);

                // deactivate current permissions
                var currentPermissions = await UsersPermissionRepository.SearchAsync(new UserPermissionSearchCriteria { UserIDs = new List<int> { user.UserID.Value } });
                if (currentPermissions.Any())
                {
                    logAction.AdditionalData.Add("PermissionIDs", String.Join(", ", currentPermissions.Select(p => p.PermissionID)));

                    foreach (var permission in currentPermissions)
                    {
                        await UsersPermissionRepository.DeleteAsync(new UserPermission { UserID = user.UserID, PermissionID = permission.PermissionID });
                    }
                }
                await AuditService.CreateLogActionAsync(logAction, cancellationToken);

                // add new permissions
                if (permissions != null && permissions.Any())
                {
                    foreach (var permission in permissions)
                    {
                        await UsersPermissionRepository.CreateAsync(new UserPermission { UserID = user.UserID, PermissionID = (int)permission });
                    }
                }

                return ReturnSuccessfulResult();

            }, cancellationToken);
        }

        public async Task<OperationResult<bool>> EditPublicUserAsync(int userCIN, string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email)) throw new ArgumentNullException(nameof(email));

            var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { CIN = userCIN }, cancellationToken);
            var user = usersFound.SingleOrDefault();
            var currentEmail = user.Email;

            if (user == null) return ReturnErrorResult(E_USER_DATA_NOT_FOUND);

            var logAction = new LogAction()
            {
                ObjectType = ObjectTypes.UserProfile,
                ActionType = ActionTypes.Edit,
                Functionality = Common.Models.Functionalities.Users,
                UserID = user.UserID,
                Key = email,
                LoginSessionID = UserAccessor.User.LoginSessionID,
                IpAddress = UserAccessor.RemoteIpAddress.GetAddressBytes(),
                AdditionalData = new Utilities.AdditionalData()
            };
            logAction.AdditionalData.Add("Email", user.Email);

            await AuditService.CreateLogActionAsync(logAction, cancellationToken);

            user.Email = email;
            await UsersRepository.UpdateAsync(user);

            var emailsResponse = await EmailNotificationService.CreateEmailNotificationAsync(new Emails.Models.EmailNotificationRequest
            {
                TemplateID = 3,
                Parameters = new Dictionary<string, string>
                {
                    { "{EMAIL}", email }
                },
                Priority = Emails.Models.EmailPriority.Normal,
                Recipients = new Emails.Models.EmailRecipient[]
                {
                    new Emails.Models.EmailRecipient() { Address = currentEmail, DisplayName = currentEmail, Type = Emails.Models.AddressTypes.To }
                }
            });

            if (!emailsResponse.IsSuccessfullyCompleted)
                throw new Exception("Error in create mail for user edit profile.");

            return ReturnSuccessfulResult();
        }

        public async Task<OperationResult<bool>> DeactivatePublicUserAsync(int userCIN, CancellationToken cancellationToken)
        {
            if (userCIN == default) throw new ArgumentException(nameof(userCIN));

            var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { CIN = userCIN }, cancellationToken);
            var user = usersFound.SingleOrDefault();

            if (user == null) return ReturnErrorResult(E_USER_DATA_NOT_FOUND);

            // към профила не трябва да има закачени средства за автентификация

            var userAuthenticationsCheck = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria { UserID = user.UserID }))
                .Where(au => au.IsActive && au.AuthenticationType != AuthenticationTypes.UsernamePassword);

            if (userAuthenticationsCheck.Any()) return ReturnErrorResult(E_USER_HAS_ACTIVE_AUTHS);

            // хеширане на идентификатора:имейла и деактивиране на профила
            var userEmail = user.Email;
            user.Email = CreateSha256($"{user.UserID.ToString()}:{user.Email}");
            user.Status = UserStatuses.Deactivated;
            await UsersRepository.UpdateAsync(user);

            var logAction = new LogAction
            {
                ObjectType = ObjectTypes.UserProfile,
                ActionType = ActionTypes.Delete,
                Functionality = Common.Models.Functionalities.Users,
                UserID = user.UserID,
                Key = userEmail,
                LoginSessionID = UserAccessor.User.LoginSessionID,
                IpAddress = UserAccessor.RemoteIpAddress.GetAddressBytes(),
                AdditionalData = new Utilities.AdditionalData()
            };

            await AuditService.CreateLogActionAsync(logAction, cancellationToken);

            return ReturnSuccessfulResult();
        }

        public Task<OperationResult<bool>> RegisterUserNRAAuthentication(string identifier, int currentUserId, IPAddress ipAddress, CancellationToken cancellationToken)
            => RegisterExternalUserAuthentication(identifier, currentUserId, AuthenticationTypes.NRA, ipAddress, false, cancellationToken);

        public Task<OperationResult<bool>> RegisterUserEAuthAuthentication(string identifier, int currentUserId, IPAddress ipAddress, CancellationToken cancellationToken)
            => RegisterExternalUserAuthentication(identifier, currentUserId, AuthenticationTypes.EAuth, ipAddress, false, cancellationToken);

        private Task<OperationResult<bool>> RegisterExternalUserAuthentication(string identifier, int currentUserId, AuthenticationTypes authenticationType, IPAddress ipAddress, bool isUserRegistration, CancellationToken cancellationToken)
        {
            return DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                var existingUserAuthentication = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria()
                {
                    Username = identifier,
                    AuthenticationType = authenticationType
                })).SingleOrDefault();

                if (existingUserAuthentication != null && (existingUserAuthentication.IsActive || existingUserAuthentication.UserID != currentUserId))
                {
                    return ReturnErrorResult(string.Empty);
                }
                else
                {
                    var userAuthentication = new UserAuthentication
                    {
                        AuthenticationType = authenticationType,
                        UserID = currentUserId,
                        Username = identifier
                    };

                    await UsersAuthenticationRepository.CreateAsync(userAuthentication);

                    if (!isUserRegistration)
                    {
                        var logAction = new LogAction()
                        {
                            ObjectType = ObjectTypes.AuthenticationMeans,
                            ActionType = ActionTypes.Add,
                            Functionality = Common.Models.Functionalities.Users,
                            UserID = currentUserId,
                            Key = UserAccessor.User.CIN.ToString(),
                            LoginSessionID = UserAccessor.User.LoginSessionID,
                            IpAddress = ipAddress.GetAddressBytes(),
                            AdditionalData = new Utilities.AdditionalData()
                        };
                        logAction.AdditionalData.Add("identifier", identifier);

                        await AuditService.CreateLogActionAsync(logAction, token);
                    }
                    
                    return ReturnSuccessfulResult();
                }
            }, cancellationToken);
        }

        public async Task<OperationResult<bool>> CreateUserCertificateAuthenticationAsync(X509Certificate2 certificate, int currentUserId, IPAddress ipAddress, bool isUserRegistration, CancellationToken cancellationToken)
        {
            if (certificate == null) throw new ArgumentNullException(nameof(certificate));

            // prepare thumbprint + certnumber for search
            var thumpPrintPrepared = certificate.GetCertHashString().Trim().Replace(" ", "").ToUpper();
            var serNumberPrepared = certificate.SerialNumber.Trim().Replace(" ", "").ToUpper();

            var certsSearched = await CertificateRepository.SearchAsync(new CertificateSearchCriteria { CertHash = thumpPrintPrepared, CertSerialNumber = serNumberPrepared, LoadContent = false });
            var certFoundInDb = certsSearched.SingleOrDefault();

            var res = await DbContextOperationExecutor.ExecuteAsync(async (dbContext, token) =>
            {
                Certificate userCertificate = null;
                if (certFoundInDb != null)
                {
                    var activeUserCertAuthentication = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria()
                    {
                        CertificateHash = certFoundInDb.CertHash,
                        AuthenticationType = AuthenticationTypes.Certificate
                    })).SingleOrDefault();

                    if (activeUserCertAuthentication != null && activeUserCertAuthentication.UserID != currentUserId)
                        return ReturnErrorResult(E_CERT_ATTACHED_TO_OTHER_USER);

                    if (activeUserCertAuthentication != null && activeUserCertAuthentication.UserID == currentUserId)
                        return ReturnErrorResult(E_CERT_ALREADY_ATTACHED);

                    // ако сертификата е в базата, но не е закачен към профил сега
                    userCertificate = certFoundInDb;
                }
                else
                {
                    userCertificate = new Certificate
                    {
                        Subject = certificate.Subject,
                        SerialNumber = serNumberPrepared,
                        Issuer = certificate.Issuer,
                        CertHash = thumpPrintPrepared,
                        Content = certificate.GetRawCertData(),
                        NotAfter = certificate.NotAfter,
                        NotBefore = certificate.NotBefore
                    };
                    await CertificateRepository.CreateAsync(userCertificate);
                }

                var userAuthentication = new UserAuthentication
                {
                    AuthenticationType = AuthenticationTypes.Certificate,
                    UserID = currentUserId,
                    CertificateID = userCertificate.CertificateID
                };

                await UsersAuthenticationRepository.CreateAsync(userAuthentication);

                if (!isUserRegistration)
                {
                    var logAction = new LogAction()
                    {
                        ObjectType = ObjectTypes.AuthenticationMeans,
                        ActionType = ActionTypes.Add,
                        Functionality = Common.Models.Functionalities.Users,
                        UserID = currentUserId,
                        Key = userCertificate.SerialNumber,
                        LoginSessionID = UserAccessor?.User?.LoginSessionID,
                        IpAddress = ipAddress.GetAddressBytes(),
                        AdditionalData = new Utilities.AdditionalData()
                    };
                    logAction.AdditionalData.Add("Issuer", userCertificate.Issuer);
                    logAction.AdditionalData.Add("ValidFrom", userCertificate.NotBefore.Value.ToString());
                    logAction.AdditionalData.Add("ValidTo", userCertificate.NotAfter.Value.ToString());
                    logAction.AdditionalData.Add("OtherInformation", userCertificate.Subject);

                    await AuditService.CreateLogActionAsync(logAction, token);
                }
                
                return ReturnSuccessfulResult();
            }, cancellationToken);

            return res;
        }

        public async Task<OperationResult<User>> CheckCertificateAlreadyAttachedToProfileAsync(X509Certificate2 certificate, CancellationToken cancellationToken)
        {
            if (certificate == null) throw new ArgumentNullException(nameof(certificate));

            // prepare thumbprint + certnumber for search
            var thumpPrintPrepared = certificate.GetCertHashString().Trim().Replace(" ", "").ToUpper();
            var serNumberPrepared = certificate.SerialNumber.Trim().Replace(" ", "").ToUpper();

            var certsSearched = await CertificateRepository.SearchAsync(new CertificateSearchCriteria { CertHash = thumpPrintPrepared, CertSerialNumber = serNumberPrepared, LoadContent = false }, cancellationToken);
            var certFoundInDb = certsSearched.SingleOrDefault();

            if (certFoundInDb != null)
            {
                var activeUserCertAuthentication = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria()
                {
                    CertificateHash = certFoundInDb.CertHash,
                    AuthenticationType = AuthenticationTypes.Certificate
                }, cancellationToken)).SingleOrDefault();

                if (activeUserCertAuthentication != null)
                {
                    var userFound = (await UsersRepository.SearchAsync(new UserSearchCriteria { UserIDs = new List<int> { activeUserCertAuthentication.UserID.Value } }, cancellationToken)).SingleOrDefault();

                    if (userFound.Status == UserStatuses.NotConfirmed)
                        return new OperationResult<User>(E_CERT_ATTACHED_TO_OTHER_NOTCONFIRMED_USER, E_CERT_ATTACHED_TO_OTHER_NOTCONFIRMED_USER) { Result = userFound };
                    else 
                        return new OperationResult<User>(E_CERT_ATTACHED_TO_OTHER_ACTIVE_USER, E_CERT_ATTACHED_TO_OTHER_ACTIVE_USER) { Result = userFound };
                }
            }

            return new OperationResult<User>(OperationResultTypes.SuccessfullyCompleted);
        }

        public async Task<OperationResult<User>> CheckEAuthAlreadyAttachedToProfileAsync(string identifier, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(identifier)) throw new ArgumentNullException(nameof(identifier));
            
                var activeUserAuthentication = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria()
                {
                    Username = identifier,
                    AuthenticationType = AuthenticationTypes.EAuth
                }, cancellationToken)).SingleOrDefault();

                if (activeUserAuthentication != null)
                {
                    var userFound = (await UsersRepository.SearchAsync(new UserSearchCriteria { UserIDs = new List<int> { activeUserAuthentication.UserID.Value } }, cancellationToken)).SingleOrDefault();

                    if (userFound.Status == UserStatuses.NotConfirmed)
                        return new OperationResult<User>(E_EAUTH_ATTACHED_TO_OTHER_NOTCONFIRMED_USER, E_EAUTH_ATTACHED_TO_OTHER_NOTCONFIRMED_USER) { Result = userFound };
                    else
                        return new OperationResult<User>(E_EAUTH_ATTACHED_TO_OTHER_ACTIVE_USER, E_EAUTH_ATTACHED_TO_OTHER_ACTIVE_USER) { Result = userFound };
                }

            return new OperationResult<User>(OperationResultTypes.SuccessfullyCompleted);
        }

        public async Task<IEnumerable<UserAuthentication>> GetUsersAuthenticationTypesAsync(int userCIN, CancellationToken cancellationToken)
        {
            if (userCIN == default) throw new ArgumentException(nameof(userCIN));

            var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { CIN = userCIN }, cancellationToken);
            var user = usersFound.SingleOrDefault();

            if (user == null) return null;

            //Средства за автентикация различни от потребителско име и парола
            var usersAuthenticationTypes = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria { UserID = user.UserID }))
                .Where(au => au.IsActive && au.AuthenticationType != AuthenticationTypes.UsernamePassword);

            return usersAuthenticationTypes;
        }

        public async Task<OperationResult<bool>> DeleteUserAuthenticationTypeAsync(int userCIN, int userAuthenticationId, CancellationToken cancellationToken)
        {
            if (userCIN == default) throw new ArgumentException(nameof(userCIN));

            var usersFound = await UsersRepository.SearchAsync(new UserSearchCriteria { CIN = userCIN }, cancellationToken);
            var user = usersFound.SingleOrDefault();

            if (user == null) return null;

            //Средства за автентикация различни от потребителско име и парола
            var usersAuthenticationTypes = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria
            {
                UserID = user.UserID,
                AuthenticationIDs = new List<int>() { userAuthenticationId }
            })).Where(au => au.IsActive && au.AuthenticationType != AuthenticationTypes.UsernamePassword).ToList();

            if (usersAuthenticationTypes == null || usersAuthenticationTypes.Count != 1 || usersAuthenticationTypes[0].UserID != user.UserID)
                throw new ArgumentException(nameof(userCIN));

            await UsersAuthenticationRepository.DeleteAsync(usersAuthenticationTypes[0]);

            var logAction = new LogAction()
            {
                ObjectType = ObjectTypes.AuthenticationMeans,
                ActionType = ActionTypes.Delete,
                Functionality = Common.Models.Functionalities.Users,
                UserID = user.UserID,
                LoginSessionID = UserAccessor.User.LoginSessionID,
                IpAddress = UserAccessor.RemoteIpAddress.GetAddressBytes(),
            };

            logAction.AdditionalData = new Utilities.AdditionalData();

            if (usersAuthenticationTypes[0].AuthenticationType == AuthenticationTypes.Certificate)
            {
                var certificate = (await CertificateRepository
                    .SearchAsync(new CertificateSearchCriteria() { CertificateIDs = new List<int>() { usersAuthenticationTypes[0].CertificateID.Value } })).First();
                logAction.Key = certificate.SerialNumber;
                logAction.AdditionalData.Add("Issuer", certificate.Issuer);
                logAction.AdditionalData.Add("ValidFrom", certificate.NotBefore.Value.ToString());
                logAction.AdditionalData.Add("ValidTo", certificate.NotAfter.Value.ToString());
                logAction.AdditionalData.Add("OtherInformation", certificate.Subject);
            }
            else
            {
                logAction.Key = userCIN.ToString();
                logAction.AdditionalData.Add("Username", user.Username);
            }

            await AuditService.CreateLogActionAsync(logAction, cancellationToken);


            return ReturnSuccessfulResult();
        }

        public async Task<IEnumerable<UserAuthenticationInfo>> GetUserAuthentications(int userId, AuthenticationTypes authType, CancellationToken cancellationToken)
        {
            if (userId == default) throw new ArgumentException(nameof(userId));

            //Средства за автентикация различни от потребителско име и парола
            var authentications = (await UsersAuthenticationRepository.SearchAsync(new UserAuthenticationSearchCriteria { UserID = userId, AuthenticationType = authType }))
                .Where(au => au.IsActive);

            if (!authentications.Any()) return Enumerable.Empty<UserAuthenticationInfo>();

            if (authentications.Any(x => x.CertificateID.HasValue))
            {
                var certIds = authentications.Select(au => au.CertificateID.Value).ToList();

                if (certIds.Any())
                {
                    var certs = await CertificateRepository.SearchAsync(new CertificateSearchCriteria { CertificateIDs = certIds });

                    var ret = certs.Select(c => new UserAuthenticationInfo
                    {
                        Issuer = c.Issuer,
                        SerialNumber = c.SerialNumber,
                        Subject = c.Subject,
                        ValidTo = c.NotAfter.Value.DateTime,
                        UserAuthenticationId = authentications.Single(au => au.CertificateID == c.CertificateID).AuthenticationID.Value
                    });

                    return ret;
                }
            }
            else if (authentications.Any(x => x.AuthenticationID.HasValue) && authType == AuthenticationTypes.EAuth)
            {
                var ret = new List<UserAuthenticationInfo>
                {
                    new UserAuthenticationInfo() { UserAuthenticationId = authentications.First(au => au.AuthenticationID.HasValue).AuthenticationID.Value }
                };

                return ret;
            }

            return Enumerable.Empty<UserAuthenticationInfo>();
        }

        public async Task<UserProcess> GetUserProcess(Guid processGuid, CancellationToken cancellationToken)
        {
            var processFound = await UsersProcessesRepository.SearchAsync(new UserProcessesSearchCriteria { ProcessGuids = new List<Guid> { processGuid } }, cancellationToken);
            return processFound.SingleOrDefault();
        }

        private async Task<OperationResult<Emails.Models.EmailNotificationResponse>> CreateRegistrationEmailAndSend(string email, UserProcess regProcess)
        {
            var registrationUriLink = UserProcessLinkBuilder.BuildConfirmRegistrationLink(new Uri(Options.CurrentValue.GL_EAU_PUBLIC_APP), regProcess);
            var cancelUriLink = UserProcessLinkBuilder.BuildCancelRegistrationLink(new Uri(Options.CurrentValue.GL_EAU_PUBLIC_APP), regProcess);

            var emailsResponse = await EmailNotificationService.CreateEmailNotificationAsync(new Emails.Models.EmailNotificationRequest
            {
                TemplateID = 1,
                Parameters = new Dictionary<string, string>
                    {
                        { "{EMAIL}", email },
                        { "{ACTIVATION_LINK}", registrationUriLink.ToString() },
                        { "{DEADLINE}", regProcess.InvalidAfter.Value.ToString() },
                        { "{CANCEL_LINK}", cancelUriLink.ToString() }
                    },
                Priority = Emails.Models.EmailPriority.Normal,
                Recipients = new Emails.Models.EmailRecipient[]
                   {
                            new Emails.Models.EmailRecipient() { Address = email, DisplayName = email, Type = Emails.Models.AddressTypes.To }
                   }
            });

            return emailsResponse;
        }

        #endregion

        private OperationResult<bool> ReturnErrorResult(string error) 
        {
            Logger.LogInformation($"Returning error result {error}");
            return new OperationResult<bool>(error, error) { Result = false }; 
        }

        private OperationResult<T> ReturnErrorResult<T>(string error, T result)
        {
            Logger.LogInformation($"Returning error result {error} for {result.GetType()}");
            return new OperationResult<T>(error, error) { Result = result };
        }

        private OperationResult<bool> ReturnSuccessfulResult() => new OperationResult<bool>(OperationResultTypes.SuccessfullyCompleted) { Result = true };

        private OperationResult<UserConfirmRegistrationResult> ReturnSuccessfulResult(UserConfirmRegistrationResult result)
            => new OperationResult<UserConfirmRegistrationResult>(OperationResultTypes.SuccessfullyCompleted) { Result = result };

        private OperationResult<UserRegistrationResult> ReturnSuccessfulResult(UserRegistrationResult result)
            => new OperationResult<UserRegistrationResult>(OperationResultTypes.SuccessfullyCompleted) { Result = result };

        public static string CreateSha256(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            using (var sha = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(input);
                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
    }
}
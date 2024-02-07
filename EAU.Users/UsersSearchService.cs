using EAU.Users.Models;
using EAU.Users.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Users
{
    public class UsersSearchService : IUsersSearchService
    {
        private readonly IUsersRepository UsersRepository;
        private readonly IUserLoginSessionRepository UserLoginSessionRepository;
        private readonly IUsersPermissionRepository UsersPermissionRepository;
        private readonly ICertificateRepository CertificateRepository;

        public UsersSearchService(IUsersRepository usersRepository,
            IUserLoginSessionRepository userLoginSessionRepository,
            IUsersPermissionRepository usersPermissionRepository,
            ICertificateRepository certificateRepository)
        {
            UsersRepository = usersRepository;
            UserLoginSessionRepository = userLoginSessionRepository;
            UsersPermissionRepository = usersPermissionRepository;
            CertificateRepository = certificateRepository;
        }

        public async Task<IEnumerable<UserLoginSession>> SearchLoginSessionAsync(UserLoginSessionSearchCriteria searchCriteria, bool? LoadUser, bool? LoadCertificate, CancellationToken cancellationToken)
        {
            var loginSessions = (await UserLoginSessionRepository.SearchAsync(searchCriteria, cancellationToken)).ToList();
            if (loginSessions != null && loginSessions.Any())
            { 
                List<User> users = null;
                if (LoadUser == true)
                {
                    var userIDs = loginSessions.Where(t => t.UserID.HasValue).Select(t => t.UserID.Value).ToList();
                    if (userIDs != null && userIDs.Any())
                    {
                        users = (await UsersRepository.SearchAsync(new UserSearchCriteria() { UserIDs = userIDs }, cancellationToken)).ToList();
                    }
                    
                }

                List<Certificate> certificates = null;

                if (LoadCertificate == true)
                {
                    var certificateIDs = loginSessions.Where(t => t.CertificateID.HasValue).Select(t => t.CertificateID.Value).ToList();
                    if (certificateIDs != null && certificateIDs.Any())
                    {
                        certificates = (await CertificateRepository.SearchAsync(new CertificateSearchCriteria() { CertificateIDs = certificateIDs }, cancellationToken)).ToList();
                    }

                }

                foreach (var loginSession in loginSessions)
                {
                    if (users != null)
                    {
                        loginSession.User = users.SingleOrDefault(t => t.UserID == loginSession.UserID);
                    }

                    if (certificates != null)
                    {
                        loginSession.Certificate = certificates.SingleOrDefault(c => c.CertificateID == loginSession.CertificateID);
                    }
                }
            }
            

            return loginSessions;
        }

        public async Task<IEnumerable<User>> SearchUsersAsync(UserSearchCriteria usersSearchCriteria, CancellationToken cancellationToken)
        {
            var users = (await UsersRepository.SearchAsync(usersSearchCriteria, cancellationToken)).ToList();

            if (users.Any())
            {
                if (usersSearchCriteria.LoadUserPermissions)
                {
                    var userPermissions = (await UsersPermissionRepository.SearchAsync(new UserPermissionSearchCriteria()
                    {
                        UserIDs = users.Select(u => u.UserID.Value).ToList()
                    }, cancellationToken)).ToList();

                    foreach (var user in users)
                    {
                        user.UserPermissions = userPermissions.Where(up => up.UserID == user.UserID).ToList();
                    }
                }
            }

            return users;
        }
    }
}

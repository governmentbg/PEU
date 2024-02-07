using CNSys;
using EAU.Security;
using EAU.Users.Repositories;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EAU.Users
{
    /// <summary>
    /// Реализация на IUserInteractionService
    /// </summary>
    public class UserInteractionService : IUserInteractionService
    {
        private readonly IEAUUserAccessor _userAccessor;
        private readonly IUserLoginSessionRepository _loginSessionRepository;
        private readonly ICertificateRepository _certificateRepository;
        private readonly IStringLocalizer _localizer;

        public UserInteractionService(
            IEAUUserAccessor userAccessor,
            IUserLoginSessionRepository loginSessionRepository,
            ICertificateRepository certificateRepository,
            IStringLocalizer localizer)
        {
            _userAccessor = userAccessor;
            _loginSessionRepository = loginSessionRepository;
            _certificateRepository = certificateRepository;
            _localizer = localizer;
        }

        public async Task<OperationResult<X509Certificate2>> GetCurrentUserSessionCertificateAsync()
        {
            var loginSessionID = _userAccessor.User?.LoginSessionID;

            if (loginSessionID == null) throw new Exception("LoginSessionID is null!");

            var loginSessions = await _loginSessionRepository.SearchAsync(new Users.Models.UserLoginSessionSearchCriteria
            {
                LoginSessionIDs = new Guid[] { loginSessionID.Value }
            });
            var loginSession = loginSessions.SingleOrDefault();

            if (loginSession == null) throw new Exception("LoginSession not found!");

            if (loginSession.CertificateID == null)
            {
                // трябва да се е логнал със КЕП
                var errors = new ErrorCollection { 
                    new TextError(_localizer["GL_00011_E"].Value, _localizer["GL_00011_E"].Value) 
                };

                return new OperationResult<X509Certificate2>(new ErrorCollection(errors));
            }

            var certificatesDb = await _certificateRepository.SearchAsync(new CertificateSearchCriteria
            {
                CertificateIDs = new List<int> { loginSession.CertificateID.Value },
                LoadContent = true
            });
            var certificate = certificatesDb.SingleOrDefault();

            if (certificate == null) throw new Exception("certificate not found!");

            var x509cert = new X509Certificate2(certificate.Content);

            return new OperationResult<X509Certificate2>(OperationResultTypes.SuccessfullyCompleted) { Result = x509cert };
        }
    }
}

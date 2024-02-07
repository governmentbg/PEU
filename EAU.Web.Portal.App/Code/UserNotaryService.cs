using CNSys;
using EAU.Security;
using EAU.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using WAIS.Integration.Notary;
using WAIS.Integration.Notary.Models;

namespace EAU.Web.Portal.App
{
    /// <summary>
    /// Имплеметация на INotaryHelper за EAU.Web.Portal.App
    /// </summary>
    public class UserNotaryService : IUserNotaryService
    {
        private readonly INotaryServicesClientFactory _notaryServicesClientFactory;
        private readonly IUserInteractionService _userInteractionService;
        private readonly IEAUUserAccessor _userAccessor;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStringLocalizer _localizer;
        private readonly ILogger _logger;
        private readonly string ClaimsIssuer = "eau.portal";

        public UserNotaryService(INotaryServicesClientFactory notaryServicesClientFactory,
            IUserInteractionService userInteractionService,
            IEAUUserAccessor userAccessor,
            IHttpContextAccessor httpContextAccessor,
            IStringLocalizer localizer,
            ILogger<UserNotaryService> logger)
        {
            _notaryServicesClientFactory = notaryServicesClientFactory;
            _userInteractionService = userInteractionService;
            _userAccessor = userAccessor;
            _httpContextAccessor = httpContextAccessor;
            _localizer = localizer;
            _logger = logger;
        }

        public async Task<UserNotaryInfo> GetUserNotaryInfoAsync(CancellationToken cancellationToken)
        {
            var currentUser = _userAccessor.User;

            var notaryRightsClaim = currentUser.Claims.SingleOrDefault(c => c.Type == NotaryClaimNames.IsNotaryClaimType);

            if (notaryRightsClaim != null)
            {
                var notNumberClaimFound = currentUser.Claims.SingleOrDefault(c => c.Type == NotaryClaimNames.NotaryNumberClaimType);

                return new UserNotaryInfo
                {
                    HasNotaryRights = bool.Parse(notaryRightsClaim.Value),
                    NotaryNumber = notNumberClaimFound?.Value
                };
            }

            var certResult = await _userInteractionService.GetCurrentUserSessionCertificateAsync();

            if (!certResult.IsSuccessfullyCompleted || certResult.Result == null)
            {
                _logger.LogInformation($"Returning {nameof(UserNotaryInfo)} with errors: no certificate is read from session.");

                return new UserNotaryInfo(certResult.Errors);
            }
                

            var notaryAuthInfo = await _notaryServicesClientFactory.GetNotaryServicesClient()
                .GetAuthenticationInfoAsync(certResult.Result, cancellationToken);

            if (!notaryAuthInfo.IsSuccessfullyCompleted)
            {
                _logger.LogInformation($"Returning {nameof(UserNotaryInfo)} with errors: notary service client returns errors.");

                return new UserNotaryInfo(new ErrorCollection { new TextError("GL_SYSTEM_UNAVAILABLE_E", _localizer["GL_SYSTEM_UNAVAILABLE_E"].Value) });
            }

            var notaryClaimsAdded = BuildAdditionalNotaryClaims(notaryAuthInfo.Response);

            await AddClaimsToCurrentUser(_userAccessor, notaryClaimsAdded);

            return new UserNotaryInfo
            {
                HasNotaryRights = notaryAuthInfo.Response.IsNotaryWithRights,
                NotaryNumber = notaryAuthInfo.Response.NotaryNumber
            };
        }

        private IEnumerable<Claim> BuildAdditionalNotaryClaims(AuthenticationResponseType notaryResponse)
        {
            var notaryClaimsAdded = new List<Claim>();

            var isNotaryClaim = new Claim(type: NotaryClaimNames.IsNotaryClaimType,
                value: notaryResponse.IsNotaryWithRights.ToString(), valueType: null, issuer: ClaimsIssuer);

            notaryClaimsAdded.Add(isNotaryClaim);

            if (notaryResponse.IsNotaryWithRights)
            {
                var notaryNumbersClaim = new Claim(type: NotaryClaimNames.NotaryNumberClaimType,
                    value: notaryResponse.NotaryNumber, valueType: null, issuer: ClaimsIssuer);
                notaryClaimsAdded.Add(notaryNumbersClaim);
            }

            return notaryClaimsAdded;
        }

        private async Task AddClaimsToCurrentUser(IEAUUserAccessor _userAccessor, IEnumerable<Claim> claims)
        {
            _userAccessor.User.AddIdentity(new ClaimsIdentity(claims));
            await _httpContextAccessor.HttpContext.SignInAsync(_userAccessor.User);
        }        
    }

    public static class NotaryClaimNames
    {
        public static string IsNotaryClaimType = "is_notary";
        public static string NotaryNumberClaimType = "notary_numbers";
    }
}

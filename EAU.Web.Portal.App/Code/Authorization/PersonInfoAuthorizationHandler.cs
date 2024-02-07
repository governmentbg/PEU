using EAU.Security;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace EAU.Web.Portal.App.Code.Authorization
{
    /// <summary>
    /// AuthorizationHandler за достъп до ресурс PersonInfo идентификатор.
    /// Валидна оторизация ако текущият потребител е нотариус или търси идентификатор за себе си.
    /// </summary>
    public class PersonInfoAuthorizationHandler : AuthorizationHandler<PersonInfoAuthorizationRequirement, string>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PersonInfoAuthorizationRequirement requirement, string resource)
        {
            if (!string.IsNullOrEmpty(resource))
            {                
                if (context.User.Claims.GetIsUserIdentifiable().GetValueOrDefault())
                {
                    var currentUserPid = context.User.Claims.GetClaim(EAUClaimTypes.PersonIdentifier)?.Value;

                    var isnotaryClaim = context.User.Claims.GetClaim(NotaryClaimNames.IsNotaryClaimType);
                    bool isNotary = isnotaryClaim != null && bool.TryParse(isnotaryClaim.Value, out bool isNotaryValue) && isNotaryValue;

                    if (isNotary || string.Compare(currentUserPid, resource, true) == 0)
                        context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}

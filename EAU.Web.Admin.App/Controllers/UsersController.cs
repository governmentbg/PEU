using EAU.DirectoryServices;
using EAU.DirectoryServices.Models;
using EAU.Security;
using EAU.Users;
using EAU.Users.Models;
using EAU.Web.Admin.App.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EAU.Web.Admin.App.Controllers
{
    /// <summary>
    /// Контролер реализиращ уеб услуга за работа с потребители.
    /// </summary>
    [Produces("application/json")]
    public class UsersController : BaseApiController
    {
        #region Fields

        private readonly IUsersService _usersService;
        private readonly IUsersSearchService _usersSearchService;

        #endregion

        public UsersController(IUsersService usersService, IUsersSearchService usersSearchService)
        {
            _usersService = usersService;
            _usersSearchService = usersSearchService;
        }

        /// <summary>
        /// Изчитане на потребителите от активна директория.
        /// </summary>
        /// <param name="ldapUserService">Услуга за работа с потребители от активна директория.</param>
        /// <param name="user">Модел на потребител от активна директория.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLDAPUsers")]
        [ProducesResponseType(typeof(IEnumerable<LDAPUser>), StatusCodes.Status200OK)]
        public IActionResult Get([FromServices] ILDAPUserService ldapUserService, [FromQuery] LDAPUser user)
        {
            int? cnt = null;
            var ldapUsers = ldapUserService.SearchUser(user.Username, user.FirstName, user.Surname, user.LastName, null, 1, 1000, ref cnt);
            return Ok(ldapUsers);
        }

        /// <summary>
        /// Изчитане на потребителски профили.
        /// </summary>
        /// <param name="criteria">Критерии за търсене на потребителски профили.</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUsersProfiles")]
        [ProducesResponseType(typeof(IEnumerable<Users.Models.User>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersProfiles([FromQuery] UserSearchCriteria criteria, CancellationToken cancellationToken)
        {
            var result = await _usersSearchService.SearchUsersAsync(criteria, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Операция за изчитане на логин сесия.
        /// </summary>
        /// <param name="loginSessionID">Идентификатор на логин сесия.</param>
        /// <param name="cancellationToken">Токен по отказване.</param>
        /// <returns>Логин сесия.</returns>
        [HttpGet]
        [Route("LognSession/{loginSessionID}")]
        [ProducesResponseType(typeof(Models.UserLoginSessionVM), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLognSession(Guid loginSessionID, CancellationToken cancellationToken)
        {
            var result = (await _usersSearchService.SearchLoginSessionAsync(new UserLoginSessionSearchCriteria() 
            {
                LoginSessionIDs = new Guid[] { loginSessionID }
             }, true, true, cancellationToken)).SingleOrDefault();

            Models.UserLoginSessionVM mappedRes = null;
            if (result != null)
            {
                mappedRes = Mapper.Map<Models.UserLoginSessionVM>(result);

                if (result.User != null)
                {
                    mappedRes.User = Mapper.Map<Models.UserVM>(result.User);
                }

                if (result.Certificate != null)
                {
                    mappedRes.Certificate = Mapper.Map<Models.CertificateVM>(result.Certificate);
                }
            }

            return Ok(mappedRes);
        }

        /// <summary>
        /// Операция за регистриране на от активна директория.
        /// </summary>
        /// <param name="model">Входящи данни за регистрация на потребител.</param>
        /// <param name="cancellationToken">Токън за спиране на процеса при нужда.</param>
        /// <returns></returns>
        [HttpPost]
        [Route("InternalUserRegister")]
        [ProducesResponseType(typeof(InternalUserModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] InternalUserModel model, CancellationToken cancellationToken)
        {
            var result = await _usersService.RegisterInternalUserAsync(model.Email, model.Username, model.IsActive, model.UserPermisions, cancellationToken);
            return OperationResult(result);
        }

        [HttpPut]
        [Route("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int userId, InternalUserModel model, CancellationToken cancellationToken)
        {
            var result = await _usersService.EditInternalUserAsync(userId, model.Email, model.IsActive, model.UserPermisions, cancellationToken);
            return OperationResult(result);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login()
        {
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Logout")]
        public IActionResult Logout([FromForm] string redirectUri)
        {
            var authProps = new AuthenticationProperties();

            if (!string.IsNullOrWhiteSpace(redirectUri))
            {
                authProps.RedirectUri = redirectUri;
                authProps.SetParameter("postlogoutregirect", "true");
            }

            return SignOut(authProps,
                Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme,
                Microsoft.AspNetCore.Authentication.OpenIdConnect.OpenIdConnectDefaults.AuthenticationScheme);
        }

        [Authorize]
        [HttpGet]
        [Route("KeepSessionAlive")]
        public IActionResult KeepSessionAlive()
        {
            return Ok();
        }

        [HttpGet]
        [Route("current")]
        public IActionResult CurrentUserInfo([FromServices] IEAUUserAccessor userAccessor)
        {
            var currentPrincipal = userAccessor.User;
            var email = currentPrincipal.Claims.GetEmail();
            var roles = currentPrincipal.Claims.GetRoles();

            return Ok(new
            {
                Email = email,
                currentPrincipal.CIN,
                currentPrincipal.IsUserIdentifiable,
                Roles = roles
            });
        }
    }
}
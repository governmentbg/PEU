using EAU.Users.Models;
using System.ComponentModel.DataAnnotations;

namespace EAU.Web.Admin.App.Models
{
    public class InternalUserModel
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public bool IsActive { get; set; }

        public UserPermissions[] UserPermisions { get; set; }
    }
}

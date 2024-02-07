using System;
using System.ComponentModel.DataAnnotations;

namespace EAU.Web.Portal.App.Models
{
    public class RegisterInputModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public bool AcceptedTerms { get; set; }
    }

    public class ChangePasswordInputModel
    {        
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }

    public class CompleteForgottenPasswordModel
    {
        public Guid ProcessId { get; set; }

        public string NewPassword { get; set; }
    }
}

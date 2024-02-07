using EAU.Common.Models;
using System;

namespace EAU.DirectoryServices.Models
{
    public class LDAPUserSearchCritaria : BasePagedSearchCriteria
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string DisplayName { get; set; }
    }

    public class LDAPUser
    {
        public Guid AccountGuid { get; set; }
       
        public string Username { get; set; }
       
        public string FirstName { get; set; }
       
        public string Surname { get; set; }
         
        public string LastName { get; set; }

        public string Email { get; set; }
         
        public string FullName { get; set; }
      
        public string DisplayName { get; set; }
    }
}
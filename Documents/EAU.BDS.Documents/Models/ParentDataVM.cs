using EAU.Documents.Domain.Models;
using System;

namespace EAU.BDS.Documents.Models
{
    public class ParentDataVM
    {
        public bool UnknownParent { get; set; }

        public PersonNames Names { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
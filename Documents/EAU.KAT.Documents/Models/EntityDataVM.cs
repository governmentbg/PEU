using EAU.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class EntityDataVM
    {
        public string Name
        {
            get;
            set;
        }
        public string Identifier
        {
            get;
            set;
        }

        public string IdentifierType
        {
            get;
            set;
        }

        public string FullName
        {
            get;
            set;
        }
        public string NameTrans 
        {
            get;
            set;
        }
        public string RecStatus
        {
            get;
            set;
        }
        public PersonAddress EntityManagmentAddress
        {
            get;
            set;
        }
        public Status Status
        {
            get;
            set;
        }
    }
}

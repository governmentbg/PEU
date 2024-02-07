using EAU.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class PersonIdentifierVM
    {
        public string Item
        {
            get;
            set;
        }

        public PersonIdentifierChoiceType? ItemElementName
        {
            get;
            set;
        }
    }
}

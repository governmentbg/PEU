using EAU.Documents.Domain.Models;

namespace EAU.Documents.Models
{
    public class PersonBasicDataVM 
    {
        public PersonNames Names { get; set; }

        public PersonIdentifier Identifier { get; set; }

        public IdentityDocumentBasicDataVM IdentityDocument { get; set; }
    }
}
using EAU.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class OwnerVM
    {
        public PersonEntityChoiceType? Type { get; set; }

        public PersonIdentifier PersonIdentifier { get; set; }

        public string EntityIdentifier { get; set; }
    }
}
using EAU.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class PersonDataVM
    {
        public PersonDataVM()
        {
            ValidateIdentityNumber = true;
        }

        public PersonNames Names { get; set; }

        public PersonIdentifierVM Identifier { get; set; }

        public PersonIdentificationData PersonIdentification { get; set; }

        public string IdentityNumber { get; set; }

        public string PlaceOfBirth { get; set; }

        public int? Height { get; set; }

        public string BDSCategoryCode { get; set; }

        public bool ValidateIdentityNumber { get; set; }

        public PersonBasicData PersonBasicData { get; set; }

        public BIDEyesColor? EyesColor { get; set; }

        public BIDMaritalStatus? MaritalStatus { get; set; }

        public System.DateTime? DeathDate { get; set; }

        public PersonAddress PermanentAddress { get; set; }

        public Status Status { get; set; }
    }
}
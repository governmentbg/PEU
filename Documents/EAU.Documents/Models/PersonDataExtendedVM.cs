using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.Documents.Models
{
    public class PersonDataExtendedVM
    {
        public List<IdentityDocumentType> IdentificationDocuments { get; set; }
        
        public PersonIdentificationData PersonIdentification { get; set; }

        public string PlaceOfBirth { get; set; }

        public BIDEyesColor? EyesColor { get; set; }

        public BIDMaritalStatus? MaritalStatus { get; set; }

        public int? Height { get; set; }
    }
}
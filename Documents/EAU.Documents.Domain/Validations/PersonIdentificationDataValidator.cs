using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class PersonIdentificationDataValidator : EAUValidator<PersonIdentificationData>
    {
        public PersonIdentificationDataValidator()
        {
            EAURuleFor(m => m.Names).EAUInjectValidator();
            EAURuleFor(m => m.NamesLatin).EAUInjectValidator();
            EAURuleFor(m => m.Identifier).EAUInjectValidator();
            EAURuleFor(m => m.Gender).EAUInjectValidator();
        }
    }
}
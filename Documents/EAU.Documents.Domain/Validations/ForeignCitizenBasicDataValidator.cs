using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ForeignCitizenBasicDataValidator : EAUValidator<ForeignCitizenBasicData>
    {
        public ForeignCitizenBasicDataValidator()
        {
            EAURuleFor(m => m.Names).EAUInjectValidator(); //ForeignCitizenNamesValidator
            EAURuleFor(m => m.PlaceOfBirth).EAUInjectValidator(); //ForeignCitizenPlaceOfBirthValidator
            EAURuleFor(m => m.IdentityDocument).EAUInjectValidator(); //ForeignCitizenIdentityDocumentValidator
        }
    }
}

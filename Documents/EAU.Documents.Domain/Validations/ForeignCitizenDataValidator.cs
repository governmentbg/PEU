using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ForeignCitizenDataValidator : EAUValidator<ForeignCitizenData>
    {
        public ForeignCitizenDataValidator()
        {
            EAURuleFor(m => m.ForeignCitizenNames).EAUInjectValidator(); //ForeignCitizenNamesValidator
            EAURuleFor(m => m.GenderName).MinLengthValidatior(1);
            EAURuleFor(m => m.GenderCode).MinLengthValidatior(1);
            EAURuleFor(m => m.Citizenships).EAUInjectValidator(); //CitizenshipValidator
        }
    }
}
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class CitizenshipRegistrationBasicDataValidator : EAUValidator<CitizenshipRegistrationBasicData>
    {
        public CitizenshipRegistrationBasicDataValidator()
        {
            EAURuleFor(m => m.PersonBasicData).EAUInjectValidator("All"); //PersonBasicDataValidator
            EAURuleFor(m => m.GenderCode).MinLengthValidatior(1);
            EAURuleFor(m => m.GenderCode).MinLengthValidatior(1);
            EAURuleFor(m => m.Citizenships).EAUInjectValidator(); //CitizenshipValidator
        }
    }
}

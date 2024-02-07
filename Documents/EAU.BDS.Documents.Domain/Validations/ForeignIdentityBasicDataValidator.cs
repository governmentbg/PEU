using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ForeignIdentityBasicDataValidator : EAUValidator<ForeignIdentityBasicData>
    {
        public ForeignIdentityBasicDataValidator()
        {
            EAURuleFor(m => m.ForeignCitizenData).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.EGN).EgnValidation();
            EAURuleFor(m => m.LNCh).LnchValidation();
            EAURuleFor(m => m.Phone).RequiredField().MinLengthValidatior(1).PhoneValidation();
            EAURuleFor(m => m.Education).MinLengthValidatior(1);
        }
    }
}
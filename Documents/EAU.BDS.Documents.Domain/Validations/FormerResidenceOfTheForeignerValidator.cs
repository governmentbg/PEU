using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class FormerResidenceOfTheForeignerValidator : EAUValidator<FormerResidenceOfTheForeigner>
    {
        public FormerResidenceOfTheForeignerValidator()
        {
            EAURuleFor(m => m.PresentAddress).EAUInjectValidator();
        }
    }
}
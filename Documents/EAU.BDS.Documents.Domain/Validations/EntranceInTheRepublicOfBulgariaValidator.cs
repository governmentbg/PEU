using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class EntranceInTheRepublicOfBulgariaValidator : EAUValidator<EntranceInTheRepublicOfBulgaria>
    {
        public EntranceInTheRepublicOfBulgariaValidator()
        {
            EAURuleFor(m => m.VisaDocument).EAUInjectValidator();
        }
    }
}
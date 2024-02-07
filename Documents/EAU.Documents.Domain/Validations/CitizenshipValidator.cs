using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class CitizenshipValidator : EAUValidator<Citizenship>
    {
        public CitizenshipValidator()
        {
            EAURuleFor(m => m.CountryGRAOCode).MinLengthValidatior(1);
            EAURuleFor(m => m.CountryName).MinLengthValidatior(1);
        }
    }
}
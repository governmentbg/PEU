using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class IssuerCountryValidator : EAUValidator<IssuerCountry>
    {
        public IssuerCountryValidator()
        {
            //EAURuleFor(m => m.CountryGRAOCode).RequiredField().MinLengthValidatior(1);
            EAURuleFor(m => m.CountryName).RequiredField().MinLengthValidatior(1);
        }
    }
}

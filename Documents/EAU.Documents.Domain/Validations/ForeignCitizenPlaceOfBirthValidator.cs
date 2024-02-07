using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ForeignCitizenPlaceOfBirthValidator : EAUValidator<ForeignCitizenPlaceOfBirth>
    {
        public ForeignCitizenPlaceOfBirthValidator()
        {
            EAURuleFor(m => m.CountryCode).MatchesValidatior("[A-Z]{2}");
            EAURuleFor(m => m.CountryName).MatchesValidatior("[А-Яа-я]+([\' -][А-Яа-я]+)*");
            EAURuleFor(m => m.SettlementName).MatchesValidatior("[А-Яа-я]+([\' -][А-Яа-я]+)*");
        }
    }
}

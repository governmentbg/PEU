using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class PlaceOfBirthAbroadValidator : EAUValidator<PlaceOfBirthAbroad>
    {
        public PlaceOfBirthAbroadValidator()
        {
            EAURuleFor(m => m.CountryGRAOCode).RequiredFieldFromSection();
            EAURuleFor(m => m.CountryName).RequiredFieldFromSection();
            EAURuleFor(m => m.SettlementAbroadName)
                .RequiredFieldFromSection()
                .MatchesValidatior("[А-Яа-я]+([\' -][А-Яа-я]+)*", ErrorMessagesConstants.FieldCanContainsOnly, "А-Яа-я '  -");
        }
    }
}
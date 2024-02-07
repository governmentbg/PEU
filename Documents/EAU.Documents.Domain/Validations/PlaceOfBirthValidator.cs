using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class PlaceOfBirthValidator : EAUValidator<PlaceOfBirth>
    {
        public PlaceOfBirthValidator()
        {
            EAURuleFor(m => m.DistrictGRAOCode)
                .RequiredField()
                .RangeLengthValidatior(1, 2)
                .MatchesValidatior("\\d{1,2}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.DistrictGRAOName).RangeLengthValidatior(1, 25);

            EAURuleFor(m => m.MunicipalityGRAOCode)
                .RequiredField()
                .RangeLengthValidatior(2, 3)
                .MatchesValidatior("\\d{2,3}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.MunicipalityGRAOName).RangeLengthValidatior(1, 25);

            EAURuleFor(m => m.SettlementGRAOCode)
                .RequiredField()
                .RangeLengthValidatior(3, 5)
                .MatchesValidatior("\\d{3,5}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.SettlementGRAOName).RangeLengthValidatior(1, 25);
        }
    }
}
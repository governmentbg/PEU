using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class GRAOAddressValidator<T> : EAUValidator<T> where T: GRAOAddress
    {
        public GRAOAddressValidator()
        {
            EAURuleFor(m => m.DistrictGRAOCode)
                .RequiredFieldFromSection()
                .RangeLengthValidatior(1,2)
                .MatchesValidatior("\\d{1,2}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.DistrictGRAOName).RangeLengthValidatior(1, 25);

            EAURuleFor(m => m.MunicipalityGRAOCode)
                .RequiredFieldFromSection()
                .RangeLengthValidatior(2, 3)
                .MatchesValidatior("\\d{2,3}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.MunicipalityGRAOName).RangeLengthValidatior(1, 25);

            EAURuleFor(m => m.SettlementGRAOCode)
                .RequiredFieldFromSection()
                .RangeLengthValidatior(3, 5)
                .MatchesValidatior("\\d{3,5}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.SettlementGRAOName).RangeLengthValidatior(1, 25);

            EAURuleFor(m => m.StreetGRAOCode)                
                .LengthValidatior(5)
                .MatchesValidatior("\\d{5}", ErrorMessagesConstants.OnlyDigitsAllowed);

            EAURuleFor(m => m.StreetText)                
                .MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$", ErrorMessagesConstants.FieldCanContainsOnly, "букви на кирилица, букви на латиница, арабски цифри, празна позиция или един от следните ~@#$%^&*()_+{}|\":><.,/\\?';-=|!.*/");

            EAURuleFor(m => m.BuildingNumber).MinLengthValidatior(1);
            EAURuleFor(m => m.Entrance).MinLengthValidatior(1);
            EAURuleFor(m => m.Floor).MinLengthValidatior(1);
            EAURuleFor(m => m.Apartment).MinLengthValidatior(1);


        }
    }

    public class PersonAddressValidator : GRAOAddressValidator<PersonAddress>
    {
        public PersonAddressValidator() : base()
        {

        }
    }
}

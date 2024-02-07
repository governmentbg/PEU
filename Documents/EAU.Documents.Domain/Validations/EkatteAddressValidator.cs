using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class EkatteAddressValidator<T> : EAUValidator<T> where T : EkatteAddress
    {
        public EkatteAddressValidator()
        {
            EAURuleFor(m => m.DistrictCode)
                .RequiredFieldFromSection()
                .LengthValidatior(3)
                .MatchesValidatior("[A-Z]{3}", ErrorMessagesConstants.FieldCanContainsOnly, "A-Z"); //TODO: add ekatte validation
            EAURuleFor(m => m.DistrictName).RangeLengthValidatior(1, 25);

            EAURuleFor(m => m.MunicipalityCode)
                .RequiredFieldFromSection()
                .LengthValidatior(5)
                .MatchesValidatior("[A-Z]{3}[0-9]{2}", ErrorMessagesConstants.FieldCanContainsOnly, "[A-Z]{3}[0-9]{2}"); //TODO: add ekatte validation
            EAURuleFor(m => m.MunicipalityName).RangeLengthValidatior(1, 25);

            EAURuleFor(m => m.SettlementCode)
                .RequiredFieldFromSection()
                .LengthValidatior(5)
                .MatchesValidatior("^\\d{5}$", ErrorMessagesConstants.OnlyDigitsAllowed); //TODO: add ekatte validation
            EAURuleFor(m => m.SettlementName).RangeLengthValidatior(1, 25);

            EAURuleFor(m => m.AreaCode)
                .MatchesValidatior("[0-9]{5}-[0-9]{2}", ErrorMessagesConstants.FieldCanContainsOnly, "[0-9]{5}-[0-9]{2}");

            EAURuleFor(m => m.PostCode)
                .RequiredFieldFromSection()
                .LengthValidatior(4)
                .MatchesValidatior("^\\d{4}$", ErrorMessagesConstants.OnlyDigitsAllowed);

            EAURuleFor(m => m.Street)
                .RequiredField()
                .MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"„“':>=|!<.,/\\?;-]+$", ErrorMessagesConstants.FieldCanContainsOnly, "букви на кирилица, букви на латиница, арабски цифри, празна позиция или един от следните ~@#$%^&*()_+{}|\":><.,/\\?';-=|!.*/")
                .When(m => string.IsNullOrEmpty(m.HousingEstate));
        }
    }

    public class EntityAddressValidator : EkatteAddressValidator<EntityAddress>
    {
        public EntityAddressValidator()
        {
        }
    }
}

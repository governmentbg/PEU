using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;


namespace EAU.Documents.Domain.Validations
{
    public class ElectronicServiceApplicantContactDataValidator : EAUValidator<ElectronicServiceApplicantContactData>
    {
        public ElectronicServiceApplicantContactDataValidator()
        {
            EAURuleFor(m => m.DistrictCode)
                .LengthValidatior(3)
                .MatchesValidatior("[A-Z]{3}", ErrorMessagesConstants.FieldCanContainsOnly, "A-Z"); //TODO: add ekatte validation
            EAURuleFor(m => m.DistrictName).RangeLengthValidatior(1, 25);
            EAURuleFor(m => m.MunicipalityCode)
                .LengthValidatior(5)
                .MatchesValidatior("[A-Z]{3}[0-9]{2}", ErrorMessagesConstants.FieldCanContainsOnly, "[A-Z]{3}[0-9]{2}"); //TODO: add ekatte validation
            EAURuleFor(m => m.MunicipalityName).RangeLengthValidatior(1, 25);
            EAURuleFor(m => m.SettlementCode)
                .LengthValidatior(5)
                .MatchesValidatior("\\d{5}", ErrorMessagesConstants.OnlyDigitsAllowed); //TODO: add ekatte validation
            EAURuleFor(m => m.SettlementName).RangeLengthValidatior(1, 25);
            EAURuleFor(m => m.PostCode).LengthValidatior(4);
            EAURuleFor(m => m.AddressDescription)
                .MinLengthValidatior(1)
                .MatchesValidatior(@"^[А-Яа-я\w\d\s\',.""-]+$", ErrorMessagesConstants.FieldCanContainsOnly, "А-Я а-я - , . \" ");
            EAURuleFor(m => m.PostOfficeBox).MinLengthValidatior(1);
            EAURuleFor(m => m.PhoneNumbers).EAUInjectValidator(); //PhoneNumbersValidator
        }
    }
}

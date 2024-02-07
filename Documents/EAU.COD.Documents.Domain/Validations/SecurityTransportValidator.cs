using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;


namespace EAU.COD.Documents.Domain.Validations
{
    public class SecurityTransportValidator : EAUValidator<SecurityTransport>
    {
        public SecurityTransportValidator()
        {
            RuleSet("Required", () => {
                EAURuleFor(m => m.VehicleOwnershipType).RequiredField();
                EAURuleFor(m => m.RegistrationNumber).RequiredField();
                EAURuleFor(m => m.RegistrationNumber).MaxLengthValidatior(20);
                EAURuleFor(m => m.MakeAndModel).RequiredField();
                EAURuleFor(m => m.MakeAndModel).MaxLengthValidatior(100);
                EAURuleFor(m => m.RegistrationNumber).MatchesValidatior("^[А-Я0-9]+$").WithEAUErrorCode(ErrorMessagesConstants.CharsAllowedCyrillicCapitalOnlyAndNums);
                EAURuleFor(m => m.MakeAndModel).MatchesValidatior("^[А-Яа-яa-zA-Z0-9\\s\\.]+$").WithEAUErrorCode(ErrorMessagesConstants.AllowedLettersAndNumsAndDotOnly);                                
            });
            RuleSet("NotRequired", () => {
                EAURuleFor(m => m.RegistrationNumber).MatchesValidatior("^[А-Я0-9]+$").WithEAUErrorCode(ErrorMessagesConstants.CharsAllowedCyrillicCapitalOnlyAndNums);
                EAURuleFor(m => m.MakeAndModel).MatchesValidatior("^[А-Яа-яa-zA-Z0-9\\s\\.]+$").WithEAUErrorCode(ErrorMessagesConstants.AllowedLettersAndNumsAndDotOnly);
                EAURuleFor(m => m.RegistrationNumber).MaxLengthValidatior(20);
                EAURuleFor(m => m.MakeAndModel).MaxLengthValidatior(100);
            });
        }
    }
}

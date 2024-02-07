using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.COD.Documents.Domain.Validations
{
    public class AlarmAndSecurityActivityValidator : EAUValidator<AlarmAndSecurityActivity>
    {
        public AlarmAndSecurityActivityValidator()
        {
            RuleSet("New", () => {
                EAURuleFor(m => m.ActualDate).RequiredField();
                EAURuleFor(m => m.SecurityObjectName).RequiredField();
                EAURuleFor(m => m.SecurityObjectName).MaxLengthValidatior(150);
                EAURuleFor(m => m.Address).RequiredField();
                EAURuleForEach(m => m.SecurityTransports).EAUInjectValidator("NotRequired");
                EAURuleFor(m => m.AISCHODDistrictId).RequiredField();

                EAURuleFor(m => m.SecurityObjectName).MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols);
                EAURuleFor(m => m.Address).MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@№#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols2);
                EAURuleFor(m => m.Address).MaxLengthValidatior(250);
            });
            RuleSet("Termination", () => {
                EAURuleFor(m => m.TerminationDate).RequiredField();
                EAURuleFor(m => m.ContractTypeNumberDate).RequiredField().MaxLengthValidatior(50);
                EAURuleFor(m => m.AISCHODDistrictId).RequiredField();
                EAURuleFor(m => m.SecurityObjectName).RequiredField();
                EAURuleFor(m => m.SecurityObjectName).MaxLengthValidatior(150);
                EAURuleFor(m => m.SecurityObjectName).MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols);
                EAURuleFor(m => m.Address).MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@№#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols2);
                EAURuleFor(m => m.Address).MaxLengthValidatior(250);
            });
        }
    }
}




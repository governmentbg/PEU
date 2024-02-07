using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.COD.Documents.Domain.Validations
{
    public class SecurityTransportingCargoValidator : EAUValidator<SecurityTransportingCargo>
    {
        public SecurityTransportingCargoValidator()
        {
            RuleSet("New", () => {
                EAURuleFor(m => m.ActualDate).RequiredField();
                EAURuleFor(m => m.ObjectTypes).RequiredField();
                EAURuleFor(m => m.TerritorialScopeFrom).RequiredField();
                EAURuleFor(m => m.TerritorialScopeTo).RequiredField();
                EAURuleFor(m => m.TerritorialScopeFrom).MaxLengthValidatior(150);
                EAURuleFor(m => m.TerritorialScopeTo).MaxLengthValidatior(150);
                EAURuleFor(m => m.SecurityType).RequiredField();
                EAURuleFor(m => m.SecurityTransports).RequiredField();
                EAURuleForEach(m => m.SecurityTransports).EAUInjectValidator("Required");

                EAURuleFor(m => m.ObjectTypes).MatchesValidatior("^[а-яА-Я\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicNumbersSymbols);
                EAURuleFor(m => m.TerritorialScopeFrom).MatchesValidatior("^[а-яА-Я\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicNumbersSymbols);
                EAURuleFor(m => m.TerritorialScopeTo).MatchesValidatior("^[а-яА-Я\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicNumbersSymbols);

            });
            RuleSet("Termination", () => {
                EAURuleFor(m => m.ContractTypeNumberDate).RequiredField().MaxLengthValidatior(50);
                EAURuleFor(m => m.TerminationDate).RequiredField();
                EAURuleFor(m => m.TerritorialScopeFrom).MaxLengthValidatior(150);
                EAURuleFor(m => m.TerritorialScopeTo).MaxLengthValidatior(150);

                EAURuleFor(m => m.ObjectTypes).MatchesValidatior("^[а-яА-Я\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicNumbersSymbols);
                EAURuleFor(m => m.TerritorialScopeFrom).MatchesValidatior("^[а-яА-Я\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicNumbersSymbols);
                EAURuleFor(m => m.TerritorialScopeTo).MatchesValidatior("^[а-яА-Я\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicNumbersSymbols);
            });
        }
    }
}




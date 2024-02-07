using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.COD.Documents.Domain.Validations
{
    public class SelfProtectionPersonsPropertyValidator : EAUValidator<SelfProtectionPersonsProperty>
    {
        public SelfProtectionPersonsPropertyValidator()
        {
            RuleSet("New", () =>
            {
                EAURuleFor(m => m.ActualDate).RequiredField();
                EAURuleFor(m => m.SecurityObjectName).RequiredField();
                EAURuleFor(m => m.SecurityObjectName).MaxLengthValidatior(150);
                EAURuleFor(m => m.Address).RequiredField();
                EAURuleFor(m => m.SecurityType).RequiredField();
                EAURuleFor(m => m.SecurityTransports).RequiredField().When(m => m.HasTransport == true);
                EAURuleForEach(m => m.SecurityTransports).EAUInjectValidator("Required");

                EAURuleFor(m => m.AISCHODDistrictId).RequiredField();
                EAURuleFor(m => m.AISCHODAccessRegimeTypeId).RequiredField();
                EAURuleFor(m => m.AISCHODControlTypeId).RequiredField();

                EAURuleFor(m => m.SecurityObjectName).MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols);
                EAURuleFor(m => m.Address).MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@№#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols2);
                EAURuleFor(m => m.Address).MaxLengthValidatior(250);
            });
            RuleSet("Termination", () =>
            {
                EAURuleFor(m => m.ContractTypeNumberDate).RequiredField().MaxLengthValidatior(50);
                EAURuleFor(m => m.AISCHODDistrictId).RequiredField();
                EAURuleFor(m => m.TerminationDate).RequiredField();
                EAURuleFor(m => m.SecurityObjectName).RequiredField();
                EAURuleFor(m => m.SecurityObjectName).MaxLengthValidatior(150);
                EAURuleFor(m => m.SecurityObjectName).MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols);
                EAURuleFor(m => m.Address).MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@№#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols2);
                EAURuleFor(m => m.Address).MaxLengthValidatior(250);
            });
        }
    }
}


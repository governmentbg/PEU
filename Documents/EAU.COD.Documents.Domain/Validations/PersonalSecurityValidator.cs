using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.COD.Documents.Domain.Validations
{
    public class PersonalSecurityValidator : EAUValidator<PersonalSecurity>
    {
        public PersonalSecurityValidator()
        {
            RuleSet("New", () => {
                EAURuleFor(m => m.ActualDate).RequiredField();
                EAURuleFor(m => m.GuardedPersonType).RequiredField();
                EAURuleFor(m => m.GuardedPerson).RequiredField().InjectValidator("All");
                EAURuleFor(m => m.Position).RequiredField();
                EAURuleFor(m => m.Position).MaxLengthValidatior(50);
                EAURuleFor(m => m.PlaceOfWork).RequiredField();
                EAURuleFor(m => m.PlaceOfWork).MaxLengthValidatior(150);
                EAURuleFor(m => m.Address).RequiredField();
                EAURuleFor(m => m.Address).MaxLengthValidatior(250);
                EAURuleFor(m => m.SecurityType).RequiredField();
                EAURuleFor(m => m.ClothintType).RequiredField();
                EAURuleFor(m => m.SecurityTransports).RequiredField();
                EAURuleForEach(m => m.SecurityTransports).EAUInjectValidator("Required");
                EAURuleFor(m => m.AISCHODDistrictId).RequiredField();

                EAURuleFor(m => m.Position).MatchesValidatior("^[а-яА-Я\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicNumbersSymbols);
                EAURuleFor(m => m.PlaceOfWork).MatchesValidatior("^[а-яА-Я\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicNumbersSymbols);
                EAURuleFor(m => m.Address).MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@№#$%^&*()_{}|\"':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols2);
            });
            RuleSet("Termination", () => {
                EAURuleFor(m => m.AISCHODDistrictId).RequiredField();
                EAURuleFor(m => m.ContractTypeNumberDate).RequiredField().MaxLengthValidatior(50);
                EAURuleFor(m => m.TerminationDate).RequiredField();
                EAURuleFor(m => m.GuardedPersonType).RequiredField();
                EAURuleFor(m => m.GuardedPerson).RequiredField().InjectValidator("NameAndIdent");
            });
        }
    }
}



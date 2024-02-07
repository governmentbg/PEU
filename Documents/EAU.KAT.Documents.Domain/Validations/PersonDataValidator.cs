using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class PersonDataValidator : EAUValidator<PersonData>
    {
        public PersonDataValidator()
        {
            RuleSet("All", () => {
                EAURuleFor(m => m.PersonBasicData).EAUInjectValidator("All");
                //Status - празен валидатор.
                EAURuleFor(m => m.PermanentAddress).EAUInjectValidator();
            });

            RuleSet("PersonIdentifierDocument", () => {
                EAURuleFor(m => m.PersonBasicData).EAUInjectValidator("WithoutNames");
                //Status - празен валидатор.
                EAURuleFor(m => m.PermanentAddress).EAUInjectValidator();
            });

            RuleSet("PersonIdentifier", () => {
                EAURuleFor(m => m.PersonBasicData).EAUInjectValidator("OnlyIdentifier");
                //Status - празен валидатор.
                EAURuleFor(m => m.PermanentAddress).EAUInjectValidator();
            });
        }
    }
}
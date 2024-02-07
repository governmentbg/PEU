using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class PersonBasicDataValidator : EAUValidator<PersonBasicData>
    {
        public PersonBasicDataValidator()
        {
            RuleSet("All", () =>
            {
                EAURuleFor(m => m.Names).RequiredFieldFromSection().EAUInjectValidator(); //PersonNamesValidator
                EAURuleFor(m => m.Identifier).RequiredFieldFromSection().EAUInjectValidator(); //PersonIdentifierValidator
                EAURuleFor(m => m.IdentityDocument).RequiredFieldFromSection().EAUInjectValidator("All"); //IdentityDocumentBasicDataValidator
            });

            RuleSet("WithoutNames", () =>
            {
                EAURuleFor(m => m.Identifier).RequiredFieldFromSection().EAUInjectValidator(); //PersonIdentifierValidator

                When(m => m.Identifier != null
                    && !string.IsNullOrEmpty(m.Identifier.Item)
                    && (
                    (m.Identifier.ItemElementName == PersonIdentifierChoiceType.EGN
                    && (new CnsysValidatorBase()).ValidateEGN(m.Identifier.Item)
                    && !(new CnsysValidatorBase()).isMinorPerson(m.Identifier.Item)) || m.Identifier.ItemElementName == PersonIdentifierChoiceType.LNCh)
                    , () =>
                    {
                        EAURuleFor(m => m.IdentityDocument)
                            .RequiredField(); //IdentityDocumentBasicDataValidator
                    })
                .Otherwise(() =>
                {
                    EAURuleFor(m => m.IdentityDocument); //IdentityDocumentBasicDataValidator
                });
            });

            RuleSet("OnlyIdentifier", () =>
            {
                EAURuleFor(m => m.Identifier).RequiredFieldFromSection().EAUInjectValidator(); //PersonIdentifierValidator
            });

            RuleSet("NamesAndIdent", () =>
            {
                EAURuleFor(m => m.Names).RequiredFieldFromSection().EAUInjectValidator(); //PersonNamesValidator
                EAURuleFor(m => m.Identifier).RequiredFieldFromSection().EAUInjectValidator(); //PersonIdentifierValidator
            });
        }
    }
}

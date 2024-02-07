using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.COD.Documents.Domain.Validations
{
    public class PersonAssignorDataValidator : EAUValidator<PersonAssignorData>
    {
        public PersonAssignorDataValidator()
        {
            RuleSet("All", () =>
            {
                EAURuleFor(m => m.FullName).RequiredField();
                EAURuleFor(m => m.FullName).CyrillicNameValidatior();
                EAURuleFor(m => m.FullName).MaxLengthValidatior(150);
                EAURuleFor(m => m.Identifier).RequiredField();
                EAURuleFor(m => m.Identifier).MaxLengthValidatior(10);
                EAURuleFor(m => m.Identifier).MatchesValidatior("^[0-9]+$", ErrorMessagesConstants.OnlyDigitsAllowed);
                EAURuleFor(m => m.GuardedType).RequiredField();
            });
            RuleSet("NameAndIdent", () =>
            {
                EAURuleFor(m => m.FullName).RequiredField();
                EAURuleFor(m => m.FullName).CyrillicNameValidatior();
                EAURuleFor(m => m.FullName).MaxLengthValidatior(150);
                EAURuleFor(m => m.Identifier).RequiredField();
                EAURuleFor(m => m.Identifier).MatchesValidatior("^[0-9]+$", ErrorMessagesConstants.OnlyDigitsAllowed);
                EAURuleFor(m => m.Identifier).MaxLengthValidatior(10);
            });
            RuleSet("NameAndIdentAndCitizenship", () =>
            {
                EAURuleFor(m => m.FullName).RequiredField();
                EAURuleFor(m => m.FullName).CyrillicNameValidatior();
                EAURuleFor(m => m.FullName).MaxLengthValidatior(150);
                EAURuleFor(m => m.Identifier).RequiredField();
                EAURuleFor(m => m.Identifier).MatchesValidatior("^[0-9]+$", ErrorMessagesConstants.OnlyDigitsAllowed);
                EAURuleFor(m => m.Identifier).MaxLengthValidatior(10);
                EAURuleFor(m => m.IdentifierType).RequiredField();
                EAURuleFor(m => m.Citizenship).RequiredField();
            });
        }
        public override ValidationResult Validate(ValidationContext<PersonAssignorData> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            var validIdentifier = false;
            CnsysValidatorBase v = new CnsysValidatorBase();
            if (instance.Identifier != null)
            {
                if (instance.Identifier.Length == 10)
                {
                    validIdentifier = v.ValidateEGN(instance.Identifier) || v.ValidateLNCH(instance.Identifier);
                }
                else
                    if (instance.Identifier.Length == 8)
                    validIdentifier = v.isValidBirthDateString(instance.Identifier, 1900, true);
            }

            if (!validIdentifier)
            {
                PlaceHolder[] parameters = new PlaceHolder[]
                            {
                                    new PlaceHolder()
                                    {
                                        Name = "Field",
                                        ResourceCode = FluentValidationExtensions.GetObjectPropertyResourseCode(instance.GetType(), "Identifier")
                                    }
                            };
                AddValidationFailure(validationRes, ErrorMessagesConstants.DefaultRegexErrorMessage, parameters);

            }

            return validationRes;
        }
    }
}

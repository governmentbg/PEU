using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class AmmunitionValidator : EAUValidator<Ammunition>
    {
        public AmmunitionValidator()
        {
            EAURuleFor(m => m.Count)
                .RequiredField()
                .MatchesValidatior(@"\d{1,10}", ErrorMessagesConstants.DefaultNonNumericErrorText)
                .MaxLengthValidatior(10);
        }

        public override ValidationResult Validate(ValidationContext<Ammunition> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if(string.IsNullOrEmpty(instance.TradeName) && string.IsNullOrEmpty(instance.Caliber))
            {
                AddValidationFailure(validationRes, "DOC_KOS_Document_MissingData_Fields_E");
            }

            return validationRes;
        }
    }
}
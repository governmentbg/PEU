using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.Documents.Domain.Validations
{
    public class ForeignEntityBasicDataValidator : EAUValidator<ForeignEntityBasicData>
    {
        public ForeignEntityBasicDataValidator()
        {
            EAURuleFor(m => m.ForeignEntityName).RequiredFieldFromSection().MinLengthValidatior(1);
            EAURuleFor(m => m.CountryISO3166TwoLetterCode).RangeLengthValidatior(1, 2).CountryISO3166TwoLetterCodeValidation();
            EAURuleFor(m => m.CountryNameCyrillic).RequiredFieldFromSection().CyrillicNameValidatior();
            EAURuleFor(m => m.ForeignEntityIdentifier).MinLengthValidatior(1);
            EAURuleFor(m => m.ForeignEntityOtherData).MinLengthValidatior(1);
        }

        public override ValidationResult Validate(ValidationContext<ForeignEntityBasicData> context)
        {
            ValidationResult validationRes = base.Validate(context);
            ForeignEntityBasicData instance = context.InstanceToValidate;

            if (!(!string.IsNullOrEmpty(instance.ForeignEntityRegister) &&
                !string.IsNullOrEmpty(instance.ForeignEntityIdentifier) &&
                string.IsNullOrEmpty(instance.ForeignEntityOtherData))
                &&
                !(string.IsNullOrEmpty(instance.ForeignEntityRegister) &&
                string.IsNullOrEmpty(instance.ForeignEntityIdentifier) &&
                !string.IsNullOrEmpty(instance.ForeignEntityOtherData)))
            {
                AddValidationFailure(validationRes, ErrorMessagesConstants.InvalidForeignEntityBasicData);
            }

            return validationRes;
        }
    }
}

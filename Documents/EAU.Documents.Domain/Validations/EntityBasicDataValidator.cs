using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class EntityBasicDataValidator : EAUValidator<EntityBasicData>
    {
        public EntityBasicDataValidator()
        {
            EAURuleFor(m => m.Name).RequiredField().MinLengthValidatior(1);
            EAURuleFor(m => m.Identifier).RequiredField();

            EAURuleFor(m => m.Identifier).Must(obj =>
            {
                if (string.IsNullOrEmpty(obj))
                {
                    return true;
                }
                else
                {
                    CnsysValidatorBase cnsy = new CnsysValidatorBase();
                    return cnsy.ValidateUICBulstat(obj);
                }
            }).WithEAUErrorCode(ErrorMessagesConstants.InvalidBULSTATAndEIK);
        }
    }
}

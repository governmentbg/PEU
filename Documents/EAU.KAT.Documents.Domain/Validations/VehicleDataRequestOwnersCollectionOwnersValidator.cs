using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleDataRequestOwnersCollectionOwnersValidator : EAUValidator<VehicleDataRequestOwnersCollectionOwners>
    {
        public VehicleDataRequestOwnersCollectionOwnersValidator()
        {
            EAURuleFor(m => m.Item).RequiredField().SetInheritanceValidator(v =>
            {
                v.EAUAdd(new PersonIdentifierValidator());
            });

            When(m => m.Item is string, () =>
            {
                EAURuleFor(x => x.Item).Must(obj =>
                {
                    string val = obj.ToString();
                    if (string.IsNullOrEmpty(val))
                    {
                        return true;
                    }
                    else
                    {
                        CnsysValidatorBase cnsy = new CnsysValidatorBase();
                        return cnsy.ValidateUICBulstat(val) /*|| cnsy.ValidateEGN(val) || cnsy.ValidateLNCH(val)*/;
                    }
                }).WithEAUErrorCode(ErrorMessagesConstants.InvalidBULSTATAndEIK);
            });
        }
    }
}
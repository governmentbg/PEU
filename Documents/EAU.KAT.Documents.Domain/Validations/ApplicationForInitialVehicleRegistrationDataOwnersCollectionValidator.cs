using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using System.Linq;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForInitialVehicleRegistrationDataOwnersCollectionValidator : EAUValidator<ApplicationForInitialVehicleRegistrationDataOwnersCollection>
    {
        public ApplicationForInitialVehicleRegistrationDataOwnersCollectionValidator()
        {
            EAURuleFor(m => m.InitialVehicleRegistrationOwnerData)
                .RequiredField()
                .Must((obj) =>
                {
                    if (obj == null || obj.Count <= 1)
                        return true;
                    else
                        return obj.Count == obj.Select(el =>
                        {
                            if (el.Item is PersonIdentifier identifier)
                                return identifier.Item;
                            else
                                return el.Item.ToString();
                        }).Distinct().Count();
                }).WithEAUErrorCode(ErrorMessagesConstants.DuplicateElementsInCollection);

            EAURuleForEach(m => m.InitialVehicleRegistrationOwnerData).EAUInjectValidator();
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForInitialVehicleRegistrationDataOwnersCollection> context)
        {
            ValidationResult res = base.Validate(context);
            ApplicationForInitialVehicleRegistrationDataOwnersCollection obj = context.InstanceToValidate;

            if (obj != null)
            {
                if (obj.InitialVehicleRegistrationOwnerData.Where(el => el.IsOwnerOfVehicleRegistrationCoupon.GetValueOrDefault()).Count() > 1)
                {
                    //Може да има само един собственик, който да е "притежател на СРМПС".
                    AddValidationFailure(res, "DOC_KAT_OnlyOneOwnerOfVehicleRegistrationCoupon_E");
                }

                if (obj.InitialVehicleRegistrationOwnerData.Where(el => el.IsVehicleRepresentative.GetValueOrDefault()).Count() > 1)
                {
                    //Може да има само един собственик, който да представи ППС в пункта „Пътна полиция“ за регистрация.
                    AddValidationFailure(res, "DOC_KAT_OnlyOneOwnerVehicleRepresentative_E");
                }
            }

            return res;
        }
    }
}

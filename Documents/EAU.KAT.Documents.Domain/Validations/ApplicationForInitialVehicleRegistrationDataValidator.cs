using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;
using EAU.Documents.Domain.Validations;
using FluentValidation.Results;
using System.Linq;
using EAU.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForInitialVehicleRegistrationDataValidator : EAUValidator<ApplicationForInitialVehicleRegistrationData>
    {
        public ApplicationForInitialVehicleRegistrationDataValidator()
        {
            EAURuleFor(m => m.VehicleIdentificationData).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.OwnersCollection).RequiredField().EAUInjectValidator();

            When(m => m.OwnersCollection != null
                && m.OwnersCollection.InitialVehicleRegistrationOwnerData != null
                && !m.OwnersCollection.InitialVehicleRegistrationOwnerData.Any(o => o.IsOwnerOfVehicleRegistrationCoupon.GetValueOrDefault())
                , () =>
                {
                    EAURuleFor(m => m.OwnerOfRegistrationCoupon).RequiredField();
                });

            EAURuleFor(m => m.OwnerOfRegistrationCoupon).EAUInjectValidator();

            When(m => m.OtherUserVehicleRepresentative.GetValueOrDefault(), () => 
            {
                EAURuleFor(m => m.VehicleUserData).RequiredField().EAUInjectValidator();
            });

            EAURuleFor(m => m.Phone)
                .RequiredField()
                .PhoneValidation();
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForInitialVehicleRegistrationData> context)
        {
            var res = base.Validate(context);
            ApplicationForInitialVehicleRegistrationData obj = context.InstanceToValidate;

            if (obj != null)
            {
                if (obj.OwnersCollection != null && obj.OwnersCollection.InitialVehicleRegistrationOwnerData != null)
                {
                    bool hasOwnerReprezentative = obj.OwnersCollection.InitialVehicleRegistrationOwnerData.Any(o => o.IsVehicleRepresentative.GetValueOrDefault());

                    if (hasOwnerReprezentative
                    &&
                    (
                        (obj.OwnerOfRegistrationCoupon != null && obj.OwnerOfRegistrationCoupon.IsVehicleRepresentative.GetValueOrDefault())
                        ||
                        (obj.VehicleUserData != null && obj.VehicleUserData.IsVehicleRepresentative.GetValueOrDefault())
                    ))
                    {
                        //Само едно лице може да представи ППС в пункта „Пътна полиция“ за регистрация.
                        AddValidationFailure(res, "DOC_KAT_OnlyOneCanBeVehicleRepresentative_E");
                    }

                    if (obj.OwnerOfRegistrationCoupon != null
                        && obj.OwnersCollection.InitialVehicleRegistrationOwnerData
                            .Any(o => o.IsOwnerOfVehicleRegistrationCoupon.GetValueOrDefault()))
                    {
                        //Може да бъде въведен само един "Притежател на СРМПС".
                        AddValidationFailure(res, "DOC_KAT_OnlyOneCanBeOwnerOfVehicleRegistrationCoupon_E");
                    }

                    var ownersIdentifiers = obj.OwnersCollection.InitialVehicleRegistrationOwnerData.Select(el =>
                    {
                        if (el.Item is PersonIdentifier identifier)
                            return identifier.Item;
                        else
                            return el.Item.ToString();
                    }).ToList();

                    if (obj.OwnerOfRegistrationCoupon != null 
                        && obj.OwnerOfRegistrationCoupon.Item != null
                        && (
                            (obj.OwnerOfRegistrationCoupon.Item is string firm && ownersIdentifiers.Contains(firm))
                            ||
                            (obj.OwnerOfRegistrationCoupon.Item is PersonIdentifier person && ownersIdentifiers.Contains(person.Item))
                            ))
                    {
                        //Данните за "Притежател на СРМПС" съвпадат с данните на собственик на ППС. 
                        AddValidationFailure(res, "DOC_KAT_DuplicationForOwnerOfVehicleRegistrationCouponWithOwner_E");
                    }

                    if (obj.VehicleUserData != null 
                        && obj.VehicleUserData.Item != null
                        && (
                            (obj.VehicleUserData.Item is string usrFirm && ownersIdentifiers.Contains(usrFirm))
                            ||
                            (obj.VehicleUserData.Item is PersonIdentifier userPerson && ownersIdentifiers.Contains(userPerson.Item))
                            ))
                    {
                        //Данните за "ползвател на ППС" съвпадат с данните на собственик на ППС. 
                        AddValidationFailure(res, "DOC_KAT_DuplicationForUserOfVehicleRegistrationWithOwner_E");
                    }
                }
            }

            return res;
        }
    }
}
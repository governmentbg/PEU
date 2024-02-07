using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using FluentValidation.Results;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForChangeRegistrationOfVehiclesDataValidator : EAUValidator<ApplicationForChangeRegistrationOfVehiclesData>
    {
        public ApplicationForChangeRegistrationOfVehiclesDataValidator()
        {
            EAURuleFor(m => m.VehicleOwnershipChangeType).RequiredField();
            EAURuleFor(m => m.VehicleChangeOwnershipData).RequiredSection();

            When(m => m.VehicleOwnershipChangeType == VehicleOwnershipChangeType.ChangeOwnership, () =>
            {
                EAURuleForEach(m => m.VehicleChangeOwnershipData).EAUInjectValidator("Change");
            });

            When(m => m.VehicleOwnershipChangeType == VehicleOwnershipChangeType.Barter, () =>
            {
                EAURuleForEach(m => m.VehicleChangeOwnershipData).EAUInjectValidator("Barter");
            });
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForChangeRegistrationOfVehiclesData> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (instance.VehicleChangeOwnershipData != null && instance.VehicleChangeOwnershipData.Count > 1)
            {

                List<string> list = new List<string>();

                foreach (VehicleChangeOwnershipData item in instance.VehicleChangeOwnershipData)
                {
                    list.Add(item.VehicleRegistrationData.RegistrationNumber);
                }

                list = list.Distinct().ToList();


                if (instance.VehicleChangeOwnershipData.Count != list.Count())
                {
                    //В полето за идентификационни данни за ППС има дублиран регистрационен номер на ППС
                    AddValidationFailure(validationRes, "DOC_GL_DUPLICATE_REGNUMBER_E");
                }
            }


            return validationRes;
        }
    }
}
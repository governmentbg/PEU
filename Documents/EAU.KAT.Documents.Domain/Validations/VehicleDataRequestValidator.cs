using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;
using System.Linq;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleDataRequestValidator : EAUValidator<VehicleDataRequest>
    {
        public VehicleDataRequestValidator()
        {
            EAURuleFor(m => m.RegistrationData).RequiredField();

            When(m => string.Compare(m.ServiceCode, "38", true) == 0, () => {
                EAURuleFor(m => m.RegistrationData).EAUInjectValidator("RoadVehicleRegistrationDataWithoutType");
            }).Otherwise(() => {
                EAURuleFor(m => m.RegistrationData).EAUInjectValidator("RoadVehicleRegistrationData");
            });

            EAURuleFor(m => m.Reasons)
                .RequiredField()
                .Must((resons) => {
                    if (resons == null || resons.Count <= 1)
                        return true;
                    else
                        return resons.Count == resons.Select(el => el.Code).Distinct().Count();
                }).WithEAUErrorCode(ErrorMessagesConstants.DuplicateElementsInCollection)
                .When(m => string.Compare(m.ServiceCode, "36", true) == 0 || string.Compare(m.ServiceCode, "38", true) == 0);
            EAURuleForEach(m => m.Reasons)
                .EAUInjectValidator()
                .When(m => string.Compare(m.ServiceCode, "36", true) == 0 || string.Compare(m.ServiceCode, "38", true) == 0);

            EAURuleFor(m => m.Phone)
                .RequiredField()
                .PhoneValidation();

            EAURuleFor(m => m.OwnersCollection)
                .RequiredField()
                .EAUInjectValidator();
        }
    }
}
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleDataItemVehicleSuspensionValidator : EAUValidator<VehicleDataItemVehicleSuspension>
    {
        public VehicleDataItemVehicleSuspensionValidator()
        {
            EAURuleFor(m => m.VehSuspensionReason).RequiredField();
        }
    }
}
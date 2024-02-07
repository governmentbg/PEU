using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleOwnerInformationItemValidator : EAUValidator<VehicleOwnerInformationItem>
    {
        public VehicleOwnerInformationItemValidator()
        {
            EAURuleFor(m => m.Address).EAUInjectValidator();
        }
    }
}
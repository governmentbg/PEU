using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleOwnerInformationCollectionValidator : EAUValidator<VehicleOwnerInformationCollection>
    {
        public VehicleOwnerInformationCollectionValidator()
        {
            EAURuleFor(m => m.Items).RequiredField();
            EAURuleForEach(m => m.Items).EAUInjectValidator();
        }
    }
}
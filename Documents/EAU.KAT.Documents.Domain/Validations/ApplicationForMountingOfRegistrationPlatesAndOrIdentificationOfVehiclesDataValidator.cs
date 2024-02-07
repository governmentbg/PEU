using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataValidator : EAUValidator<ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData>
    {
        public ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesDataValidator()
        {
            EAURuleFor(m => m.PoliceDepartment).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.ApplicationText).RequiredField();
            EAURuleFor(m => m.Phone).RequiredField().MinLengthValidatior(1).PhoneValidation();            
        }
    }
}
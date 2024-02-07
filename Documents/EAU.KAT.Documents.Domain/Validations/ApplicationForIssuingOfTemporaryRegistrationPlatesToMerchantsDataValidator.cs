using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataValidator : EAUValidator<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData>
    {
        public ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataValidator()
        {
            EAURuleFor(m => m.TemporaryPlatesCount).RequiredField().InclusiveBetween(1, 99).WithEAUErrorCode(ErrorMessagesConstants.FiledValueMustBeBetween, "1", "99");
            EAURuleFor(m => m.OperationalNewVehicleMakes).MaxLengthValidatior(500);
            EAURuleFor(m => m.OperationalSecondHandVehicleMakes).MaxLengthValidatior(500);
            EAURuleFor(m => m.VehicleDealershipAddress).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.AuthorizedPersons).RequiredField();
            EAURuleForEach(m => m.AuthorizedPersons).EAUInjectValidator();
            EAURuleFor(m => m.Phone).RequiredField().MaxLengthValidatior(100);
        }
    }
}
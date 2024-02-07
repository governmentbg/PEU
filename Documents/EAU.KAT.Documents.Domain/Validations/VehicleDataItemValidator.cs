using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleDataItemValidator : EAUValidator<VehicleDataItem>
    {
        public VehicleDataItemValidator()
        {
            EAURuleFor(m => m.RegistrationNumber).MatchesValidatior("([А-Я0-9])*");
            EAURuleFor(m => m.MakeModel).MinLengthValidatior(1);
            EAURuleFor(m => m.PreviousRegistrationNumber).MinLengthValidatior(1);
            EAURuleFor(m => m.IdentificationNumber).MinLengthValidatior(1);
            EAURuleFor(m => m.EngineNumber).MinLengthValidatior(1);
            EAURuleFor(m => m.Type).MinLengthValidatior(1);
            EAURuleForEach(m => m.VehicleSuspension).EAUInjectValidator();
        }
    }
}
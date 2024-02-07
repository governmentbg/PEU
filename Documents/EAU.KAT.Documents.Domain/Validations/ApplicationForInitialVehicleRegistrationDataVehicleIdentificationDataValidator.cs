using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForInitialVehicleRegistrationDataVehicleIdentificationDataValidator : EAUValidator<ApplicationForInitialVehicleRegistrationDataVehicleIdentificationData>
    {
        public ApplicationForInitialVehicleRegistrationDataVehicleIdentificationDataValidator()
        {
            EAURuleFor(m => m.IdentificationNumber)
               .RequiredField()
               .RangeLengthValidatior(1, 20)
               .MatchesValidatior("^[A-HJ-NPR-Z0-9]+$", ErrorMessagesConstants.FieldCanContainsOnly, "главни букви на латиница без I,O и Q и цифри");

            EAURuleFor(m => m.ApprovalCountryCode)
                .RequiredField()
                .MatchesValidatior("^e{1}[0-9]{1,2}$");

            EAURuleFor(m => m.ImportCountryCode)
                .RequiredField();

            EAURuleFor(m => m.AdditionalInfo)
                .MaxLengthValidatior(255);
        }
    }
}

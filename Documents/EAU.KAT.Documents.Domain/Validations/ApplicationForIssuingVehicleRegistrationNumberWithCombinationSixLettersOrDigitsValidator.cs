using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsValidator
        : EAUValidator<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigits>
    {
        public ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData).EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
    }
}
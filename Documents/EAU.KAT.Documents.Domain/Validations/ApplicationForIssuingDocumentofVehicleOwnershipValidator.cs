using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentofVehicleOwnershipValidator : EAUValidator<ApplicationForIssuingDocumentofVehicleOwnership>
    {
        public ApplicationForIssuingDocumentofVehicleOwnershipValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuingDocumentofVehicleOwnershipData).EAUInjectValidator();
        }
    }
}
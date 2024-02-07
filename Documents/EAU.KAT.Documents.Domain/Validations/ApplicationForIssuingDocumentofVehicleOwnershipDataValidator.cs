using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentofVehicleOwnershipDataValidator : EAUValidator<ApplicationForIssuingDocumentofVehicleOwnershipData>
    {
        public ApplicationForIssuingDocumentofVehicleOwnershipDataValidator()
        {
            When(m => m.PermanentAddress != null, () =>
            {
                EAURuleFor(m => m.PermanentAddress).EAUInjectValidator();
            });

            When(m => m.CurrentAddress != null, () =>
            {
                EAURuleFor(m => m.CurrentAddress).EAUInjectValidator();
            });
            EAURuleFor(m => m.DocumentFor).RequiredField();
            EAURuleFor(m => m.Item)
                .RequiredField()
                .EAUInjectValidator()
                .When(m => m.DocumentFor != DocumentFor.OwnershipOfAllVehicles);
            EAURuleFor(m => m.OwnershipCertificateReason).RequiredField();
        }
    }
}
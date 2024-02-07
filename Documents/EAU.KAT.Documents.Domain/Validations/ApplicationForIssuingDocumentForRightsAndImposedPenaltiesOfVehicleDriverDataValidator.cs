using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataValidator : EAUValidator<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData>
    {
        public ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataValidator()
        {
            When(m => m.PermanentAddress != null, () =>
            {
                EAURuleFor(m => m.PermanentAddress).EAUInjectValidator();
            });

            When(m => m.CurrentAddress != null, () =>
            {
                EAURuleFor(m => m.CurrentAddress).EAUInjectValidator();
            });
            EAURuleFor(m => m.ANDCertificateReason).RequiredField();
        }
    }
}
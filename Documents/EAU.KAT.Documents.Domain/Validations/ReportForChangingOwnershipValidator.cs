using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ReportForChangingOwnershipValidator : EAUValidator<ReportForChangingOwnership>
    {
        public ReportForChangingOwnershipValidator()
        {
            EAURuleFor(m => m.ElectronicServiceApplicant)
                .EAUInjectValidator(); //ElectronicServiceApplicantValidator
            //EAURuleFor(m => m.DocumentURI)
            //    .EAUInjectValidator(); //DocumentURIValidator има ClearRules
            EAURuleFor(m => m.DocumentTypeURI)
                .EAUInjectValidator(); //DocumentTypeURIValidator
            EAURuleFor(m => m.DocumentTypeName).RequiredField();
            EAURuleFor(m => m.AISCaseURI).RequiredXmlElement();
            EAURuleFor(m => m.VehicleRegistrationData).EAUInjectValidator("Full");
            EAURuleFor(m => m.OldOwnersData).EAUInjectValidator();
            EAURuleFor(m => m.NewOwnersData).EAUInjectValidator();
            //EAURuleForEach(m => m.LocalTaxes).EAUInjectValidator(); - празен валидатор.
            //EAURuleForEach(m => m.PeriodicTechnicalCheck).EAUInjectValidator(); - празен валидатор.
            EAURuleFor(m => m.AdministrativeBodyName).MinLengthValidatior(1);
        }
    }
}
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models.Forms;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesValidator : EAUValidator<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGasses>
    {
        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.IssuingPoliceDepartment).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData).RequiredSection().EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();       
        }
    }
}

using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataValidator : EAUValidator<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData>
    {
        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataValidator()
        {
            EAURuleFor(m => m.PermanentAddress).EAUInjectValidator();
            EAURuleFor(m => m.CurrentAddress).EAUInjectValidator();
                
            When(x => x.CertificateType == CertificateType.Issuing, () =>
            {
                EAURuleFor(m => m.CertificateNumber).RequiredField();
            });
        }
    }
}

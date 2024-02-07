using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataIssuingCertificateValidator : EAUValidator<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataIssuingCertificate>
    {
        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataIssuingCertificateValidator()
        {
            EAURuleFor(m => m.CertificateNumber).RequiredField();
        }
    }
}

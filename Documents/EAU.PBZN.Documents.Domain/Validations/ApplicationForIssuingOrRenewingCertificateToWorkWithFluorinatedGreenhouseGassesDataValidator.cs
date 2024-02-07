using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models;
using FluentValidation;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataValidator : EAUValidator<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData>
    {
        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataValidator()
        {
            EAURuleFor(m => m.WorkPhone).RequiredField().PhoneValidation();

            EAURuleFor(m => m.Item)
                .RequiredFieldFromSection()
                .SetInheritanceValidator(v =>
                {
                    v.EAUAdd(new ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataValidator());
                    v.EAUAdd(new ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataValidator());
                });
        }
    }
}

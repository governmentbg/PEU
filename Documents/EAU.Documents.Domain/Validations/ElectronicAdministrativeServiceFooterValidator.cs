using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ElectronicAdministrativeServiceFooterValidator : EAUValidator<ElectronicAdministrativeServiceFooter>
    {
        public ElectronicAdministrativeServiceFooterValidator()
        {
            EAURuleFor(m => m.ApplicationSigningTime).RequiredFieldFromSection();
            EAURuleFor(m => m.XMLDigitalSignature).RequiredFieldFromSection();
        }
    }
}
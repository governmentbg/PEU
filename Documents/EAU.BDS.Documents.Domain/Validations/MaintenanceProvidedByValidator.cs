using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class MaintenanceProvidedByValidator : EAUValidator<MaintenanceProvidedBy>
    {
        public MaintenanceProvidedByValidator()
        {
            EAURuleFor(m => m.Name).MinLengthValidatior(1);
            EAURuleFor(m => m.ForeignCitizenNames).EAUInjectValidator();
        }
    }
}
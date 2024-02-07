using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class LicenseInfoValidator : EAUValidator<LicenseInfo>
    {
        public LicenseInfoValidator()
        {
            EAURuleFor(m => m.IssuingPoliceDepartment).EAUInjectValidator();
            EAURuleFor(m => m.PermitNumber).RequiredField();
            EAURuleFor(m => m.PermitType).RequiredField();
            EAURuleFor(m => m.PermitTypeName).RequiredField();
            EAURuleFor(m => m.Reason).RequiredField();
            EAURuleFor(m => m.ReasonName).RequiredField();
            EAURuleFor(m => m.ValidityPeriodStart).RequiredField();
            EAURuleFor(m => m.HolderName).RequiredField();
            EAURuleFor(m => m.HolderIdentifier).RequiredField();
            EAURuleFor(m => m.Content).RequiredField();
        }
    }
}
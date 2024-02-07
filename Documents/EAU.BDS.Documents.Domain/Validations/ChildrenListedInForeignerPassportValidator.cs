using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ChildrenListedInForeignerPassportValidator : EAUValidator<ChildrenListedInForeignerPassport>
    {
        public ChildrenListedInForeignerPassportValidator()
        {
            EAURuleFor(m => m.ForeignCitizenNames).EAUInjectValidator();
        }
    }
}
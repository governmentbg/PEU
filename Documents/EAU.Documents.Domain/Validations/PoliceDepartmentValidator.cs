using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class PoliceDepartmentValidator : EAUValidator<PoliceDepartment>
    {
        public PoliceDepartmentValidator()
        {
            EAURuleFor(m => m.PoliceDepartmentCode).MatchesValidatior("\\d*");
            EAURuleFor(m => m.PoliceDepartmentName).RequiredField().MinLengthValidatior(1);
        }
    }
}
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class GenderDataValidator : EAUValidator<GenderData>
    {
        public GenderDataValidator()
        {
            EAURuleFor(m => m.Genders).RequiredField();
        }
    }
}
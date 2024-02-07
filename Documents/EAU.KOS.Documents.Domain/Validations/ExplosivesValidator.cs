using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class ExplosivesValidator : EAUValidator<Explosives>
    {
        public ExplosivesValidator()
        {
            EAURuleFor(m => m.NumberOON).RequiredField();
            EAURuleFor(m => m.Quantity)
                .RequiredField()
                .MaxLengthValidatior(20);
            EAURuleFor(m => m.Measure).RequiredField();
        }
    }
}
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class PyrotechnicsValidator : EAUValidator<Pyrotechnics>
    {
        public PyrotechnicsValidator()
        {
            EAURuleFor(m => m.Kind).RequiredField();
            EAURuleFor(m => m.Quantity)
                .RequiredField()
                .MaxLengthValidatior(20);
            EAURuleFor(m => m.Measure).RequiredField();
        }
    }
}
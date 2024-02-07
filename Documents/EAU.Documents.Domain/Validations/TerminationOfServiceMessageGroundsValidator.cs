using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class TerminationOfServiceMessageGroundsValidator : EAUValidator<TerminationOfServiceMessageGrounds>
    {
        public TerminationOfServiceMessageGroundsValidator()
        {
            EAURuleFor(m => m.TerminationOfServiceMessageGround)
                .RequiredField()
                .MinLengthValidatior(1);
        }
    }
}
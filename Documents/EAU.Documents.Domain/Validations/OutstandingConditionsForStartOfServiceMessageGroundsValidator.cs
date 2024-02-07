using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class OutstandingConditionsForStartOfServiceMessageGroundsValidator : EAUValidator<OutstandingConditionsForStartOfServiceMessageGrounds>
    {
        public OutstandingConditionsForStartOfServiceMessageGroundsValidator()
        {
            EAURuleFor(m => m.OutstandingConditionsForStartOfServiceMessageGround)
                .RequiredField()
                .MinLengthValidatior(1);
        }
    }
}
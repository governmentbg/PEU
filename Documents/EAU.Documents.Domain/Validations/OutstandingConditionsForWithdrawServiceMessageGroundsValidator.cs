using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class OutstandingConditionsForWithdrawServiceMessageGroundsValidator : EAUValidator<OutstandingConditionsForWithdrawServiceMessageGrounds>
    {
        public OutstandingConditionsForWithdrawServiceMessageGroundsValidator()
        {
            EAURuleFor(m => m.OutstandingConditionsForWithdrawServiceMessageGround)
                .RequiredField()
                .MinLengthValidatior(1);
        }
    }
}

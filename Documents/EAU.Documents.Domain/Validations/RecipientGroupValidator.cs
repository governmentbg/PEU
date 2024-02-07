using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class RecipientGroupValidator : EAUValidator<RecipientGroup>
    {
        public RecipientGroupValidator()
        {
            EAURuleForEach(m => m.Authors).EAUInjectValidator(); //ElectronicStatementAuthorValidator
            EAURuleFor(m => m.Authors).CollectionWithOneElement();

            When(m => m.Recipients == null, () =>
            {
                EAURuleFor(m => m.Recipients).RequiredField();
            })
            .Otherwise(() =>
            {
                EAURuleFor(m => m.Recipients).Must((obj, val) => val.Count > 0).WithErrorCode(ErrorMessagesConstants.DefaultNotEmptyErrorMessage);
                EAURuleForEach(m => m.Recipients).EAUInjectValidator(); //ElectronicServiceRecipientValidator
            });
        }
    }
}

using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class TransferContainerValidator : EAUValidator<TransferContainer>
    {
        public TransferContainerValidator()
        {
            EAURuleFor(m => m.DocumentURI).RequiredFieldFromSection().EAUInjectValidator(); //DocumentURIValidator

            EAURuleFor(m => m.ShortTransferDescription)
                .MinLengthValidatior(1)
                .MatchesValidatior(@"^[А-Яа-я0-9]+[А-Яа-я0-9\s-\.,?!:%\(\)\'""]*$");

            EAURuleFor(m => m.ExpandedTransferDescription)
                .MinLengthValidatior(1)
                .MatchesValidatior(@"^[А-Яа-я0-9]+[А-Яа-я0-9\s-\.,?!:%\(\)\'""]*$");

            EAURuleForEach(m => m.TransferredDocuments).EAUInjectValidator(); //TransferredDocumentValidator
        }
    }
}

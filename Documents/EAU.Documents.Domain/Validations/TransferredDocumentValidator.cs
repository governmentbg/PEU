using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class TransferredDocumentValidator : EAUValidator<TransferredDocument>
    {
        public TransferredDocumentValidator()
        {
            EAURuleFor(m => m.DocumentURI).RequiredFieldFromSection().EAUInjectValidator(); //DocumentURIValidator

            EAURuleFor(m => m.ShortTransferDescription)
                .RequiredFieldFromSection()
                .MinLengthValidatior(1)
                .MatchesValidatior(@"^[А-Яа-я0-9]+[А-Яа-я0-9\s-\.,?!:%\(\)\'""]*$");
                
            EAURuleFor(m => m.ExpandedTransferDescription)
                .RequiredFieldFromSection()
                .MinLengthValidatior(1)
                .MatchesValidatior(@"^[А-Яа-я0-9]+[А-Яа-я0-9\s-\.,?!:%\(\)\'""]*$");

            EAURuleFor(m => m.FileType)
                .MatchesValidatior("[a-z]+/[-+\\.a-zA-Z0-9]+", ErrorMessagesConstants.FieldCanContainsOnly, "A-Z a-z 0-9 - +");

            EAURuleFor(m => m.FileContent).RequiredFieldFromSection();
        }
    }
}


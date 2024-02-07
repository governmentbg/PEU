using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class DocumentURIValidator : EAUValidator<DocumentURI>
    {
        public DocumentURIValidator()
        {
            EAURuleFor(m => m.RegisterIndex).RequiredFieldFromSection();

            EAURuleFor(m => m.SequenceNumber).RequiredFieldFromSection();

            EAURuleFor(m => m.ReceiptOrSigningDate).RequiredFieldFromSection();
        }
    }
}

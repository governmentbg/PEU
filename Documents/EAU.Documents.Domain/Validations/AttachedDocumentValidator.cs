using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class AttachedDocumentValidator : EAUValidator<AttachedDocument>
    {
        public AttachedDocumentValidator()
        {
            EAURuleFor(m => m.Description).MinLengthValidatior(1);
            EAURuleFor(m => m.UniqueIdentifier).MinLengthValidatior(1);
            EAURuleFor(m => m.FileType).MatchesValidatior("[a-z]+/[-+\\.a-zA-Z0-9]+");
            EAURuleFor(m => m.FileName).RequiredFieldFromSection().MinLengthValidatior(1);
            EAURuleFor(m => m.FileContent).RequiredFieldFromSection();
            EAURuleFor(m => m.Description).RequiredFieldFromSection();
        }
    }
}

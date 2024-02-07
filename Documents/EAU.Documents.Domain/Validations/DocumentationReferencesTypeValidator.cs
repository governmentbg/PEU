using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class DocumentationReferencesTypeValidator : EAUValidator<DocumentationReferencesType>
    {
        public DocumentationReferencesTypeValidator()
        {
            EAURuleFor(m => m.DocumentationReference).RequiredField();
        }
    }
}

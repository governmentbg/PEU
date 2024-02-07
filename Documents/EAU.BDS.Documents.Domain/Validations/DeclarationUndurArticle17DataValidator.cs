using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class DeclarationUndurArticle17DataValidator : EAUValidator<DeclarationUndurArticle17Data>
    {
        public DeclarationUndurArticle17DataValidator()
        {
            EAURuleFor(m => m.DocType).RequiredField();
            EAURuleFor(m => m.PermanentAddress).EAUInjectValidator();
            EAURuleFor(m => m.PresentAddress).EAUInjectValidator();
            EAURuleFor(m => m.ReasonData).EAUInjectValidator();
        }
    }
}

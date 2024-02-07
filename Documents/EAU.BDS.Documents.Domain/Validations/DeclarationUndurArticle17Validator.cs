using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class DeclarationUndurArticle17Validator : EAUValidator<DeclarationUndurArticle17>
    {
        public DeclarationUndurArticle17Validator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.DeclarationUndurArticle17Data).EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
    }
}

using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class DeclarationValidator : EAUValidator<Declaration>
    {
        public DeclarationValidator()
        {
            EAURuleFor(m => m.DeclarationName).MinLengthValidatior(1);
        }
    }
}
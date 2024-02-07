using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class CRLIdentifierTypeValidator : EAUValidator<CRLIdentifierType>
    {
        public CRLIdentifierTypeValidator()
        {
            EAURuleFor(m => m.Issuer).RequiredField();
        }
    }
}

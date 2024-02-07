using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ForeignCitizenIdentityDocumentValidator : EAUValidator<ForeignCitizenIdentityDocument>
    {
        public ForeignCitizenIdentityDocumentValidator()
        {
            EAURuleFor(m => m.DocumentNumber).MinLengthValidatior(1);
            EAURuleFor(m => m.DocumentType).MinLengthValidatior(1);
        }
    }
}

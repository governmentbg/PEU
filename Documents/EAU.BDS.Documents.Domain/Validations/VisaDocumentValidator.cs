using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class VisaDocumentValidator : EAUValidator<VisaDocument>
    {
        public VisaDocumentValidator()
        {
            EAURuleFor(m => m.IdentityNumber).MinLengthValidatior(1);
            EAURuleFor(m => m.IdentityIssuer).MinLengthValidatior(1);
        }
    }
}
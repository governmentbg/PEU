using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class DocumentTypeURIValidator : EAUValidator<DocumentTypeURI>
    {
        public DocumentTypeURIValidator()
        {
            EAURuleFor(m => m.RegisterIndex)
                .RequiredField()
                .GreaterThanOrEqualTo(1).WithEAUErrorCode(ErrorMessagesConstants.FiledGreatherThanOrEqual, "1");

            EAURuleFor(m => m.BatchNumber)
                .RequiredField()
                .GreaterThanOrEqualTo(1).WithEAUErrorCode(ErrorMessagesConstants.FiledGreatherThanOrEqual, "1");
        }
    }
}

using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class NoticeReferenceTypeValidator : EAUValidator<NoticeReferenceType>
    {
        public NoticeReferenceTypeValidator()
        {
            EAURuleFor(m => m.Organization).RequiredField();
            EAURuleFor(m => m.NoticeNumbers).RequiredField();
        }
    }
}
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class DocumentMustServeToValidator : EAUValidator<DocumentMustServeTo>
    {
        public DocumentMustServeToValidator()
        {
            EAURuleFor(m => m.Item)
                .RequiredField()
                .MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$", ErrorMessagesConstants.FieldCanContainsOnly, "[А-Яа-яA-Za-z0-9~@#$%^&*()_+{}|\":><.,/? ';-=|!]");
        }
    }
}

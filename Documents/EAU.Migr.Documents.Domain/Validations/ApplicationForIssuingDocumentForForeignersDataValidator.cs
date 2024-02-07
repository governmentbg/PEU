using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Migr.Documents.Domain.Models;

namespace EAU.Migr.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentForForeignersDataValidator : EAUValidator<ApplicationForIssuingDocumentForForeignersData>
    {
        public ApplicationForIssuingDocumentForForeignersDataValidator()
        {
            EAURuleFor(m => m.Address)
                .RequiredField()
                .EAUInjectValidator();
            EAURuleFor(m => m.Citizenship)
               .RequiredField()
               .EAUInjectValidator();
            EAURuleFor(m => m.BirthDate).RequiredField();
            EAURuleFor(m => m.BirthDate).BirthDateValidation();
            EAURuleFor(m => m.CertificateFor).RequiredField()
                .MatchesValidatior("^[а-яА-Яa-zA-Z\\s+\\d+~@#$%^&*()_{}|\"':>=|!<.,/\\\\?;-]+$", ErrorMessagesConstants.FieldCanContainsOnly, "[А-Яа-яA-Za-z0-9~@#$%^&*()_+{}|\":><.,/? ';-=|!]");            
            EAURuleFor(m => m.DocumentMustServeTo)
                .RequiredField()
                .EAUInjectValidator();
        }
    }
}

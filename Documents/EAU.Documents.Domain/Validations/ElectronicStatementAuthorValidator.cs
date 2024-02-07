using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ElectronicStatementAuthorValidator : EAUValidator<ElectronicStatementAuthor>
    {
        public ElectronicStatementAuthorValidator()
        {
            EAURuleFor(m => m.AuthorQuality).RequiredField();

            EAURuleFor(m => m.ItemPersonBasicData).EAUInjectValidator()
                .When(m => m.ItemElementName == ElectronicStatementAuthor.ItemChoiceType.Person); //PersonBasicDataValidator

            EAURuleFor(m => m.ItemForeignCitizenBasicData).EAUInjectValidator()
                .When(m => m.ItemElementName == ElectronicStatementAuthor.ItemChoiceType.ForeignCitizen); //ForeignCitizenBasicDataValidator
        }
    }
}

using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class PersonIdentifierValidator : EAUValidator<PersonIdentifier>
    {
        public PersonIdentifierValidator()
        {
            EAURuleFor(m => m.Item).RequiredField().EgnValidation().When(m => m.ItemElementName == PersonIdentifierChoiceType.EGN);
            EAURuleFor(m => m.Item).RequiredField().LnchValidation().When(m => m.ItemElementName == PersonIdentifierChoiceType.LNCh);
        }
    }
}

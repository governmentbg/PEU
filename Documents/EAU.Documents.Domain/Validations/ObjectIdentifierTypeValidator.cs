using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ObjectIdentifierTypeValidator : EAUValidator<ObjectIdentifierType>
    {
        public ObjectIdentifierTypeValidator()
        {
            EAURuleFor(m => m.Identifier).RequiredField();
            EAURuleFor(m => m.DocumentationReferences).EAUInjectValidator(); //DocumentationReferencesTypeValidator
        }
    }
}
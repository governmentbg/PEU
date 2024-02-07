using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class EncryptedKeyTypeValidator : EAUValidator<EncryptedKeyType>
    {
        public EncryptedKeyTypeValidator()
        {
            EAURuleFor(m => m.EncryptionMethod).EAUInjectValidator(); //EncryptionMethodTypeValidator
        }
    }
}
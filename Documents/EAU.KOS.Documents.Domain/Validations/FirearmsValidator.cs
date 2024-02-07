using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class FirearmsValidator : EAUValidator<Firearms>
    {
        public FirearmsValidator()
        {
            EAURuleFor(m => m.Brand).RequiredField();
            EAURuleFor(m => m.SerialNumber).RequiredField();
            EAURuleFor(m => m.KindCode).RequiredField();
            EAURuleFor(m => m.KindName).RequiredField();
        }
    }
}
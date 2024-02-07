using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ElectronicServiceProviderBasicDataValidator : EAUValidator<ElectronicServiceProviderBasicData>
    {
        public ElectronicServiceProviderBasicDataValidator()
        {
            EAURuleFor(m => m.EntityBasicData).RequiredField();
            EAURuleFor(m => m.EntityBasicData).EAUInjectValidator().When(m => m.EntityBasicData != null); //EntityBasicDataValidator
        }
    }
}

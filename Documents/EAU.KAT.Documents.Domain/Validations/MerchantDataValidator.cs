using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class MerchantDataValidator : EAUValidator<MerchantData>
    {
        public MerchantDataValidator()
        {
            EAURuleFor(m => m.CompanyCase).EAUInjectValidator();
            EAURuleFor(m => m.EntityManagementAddress).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.CorrespondingAddress).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.Phone).RequiredField().PhoneValidation();
        }
    }
}
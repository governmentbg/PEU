using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingDriverLicenseDataValidator : EAUValidator<ApplicationForIssuingDriverLicenseData>
    {
        public ApplicationForIssuingDriverLicenseDataValidator()
        {
            EAURuleFor(m => m.IdentificationDocuments).RequiredField();
            EAURuleFor(m => m.Person).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.Phone).RequiredField().MinLengthValidatior(1).PhoneValidation();
            EAURuleFor(m => m.Address).RequiredField().EAUInjectValidator();
        }
    }
}
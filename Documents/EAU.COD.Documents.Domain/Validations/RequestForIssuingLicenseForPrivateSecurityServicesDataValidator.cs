using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.COD.Documents.Domain.Validations
{
    public class RequestForIssuingLicenseForPrivateSecurityServicesDataValidator : EAUValidator<RequestForIssuingLicenseForPrivateSecurityServicesData>
    {
        public RequestForIssuingLicenseForPrivateSecurityServicesDataValidator()
        {
            EAURuleFor(m => m.EntityManagementAddress).RequiredFieldFromSection().EAUInjectValidator();
            EAURuleFor(m => m.CorrespondingAddress).RequiredFieldFromSection().EAUInjectValidator();
            EAURuleFor(m => m.WorkPhone).MinLengthValidatior(1).PhoneValidation();
            EAURuleFor(m => m.MobilePhone).MinLengthValidatior(1).PhoneValidation();
            EAURuleForEach(m => m.SecurityServiceTypes).EAUInjectValidator();          
            EAURuleFor(m => m.SecurityServiceTypes).RequiredField().EAUInjectValidator();
        }
    }
}
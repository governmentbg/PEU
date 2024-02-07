using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.COD.Documents.Domain.Validations
{
    public class RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypesValidator : EAUValidator<RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypes>
    {
        public RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypesValidator()
        {
            EAURuleFor(m => m.TerritorialScopeOfServices).EAUInjectValidator().When(m => m.PointOfPrivateSecurityServicesLawSpecified);
        }
    }
}
using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataValidator : EAUValidator<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData>
    {
        public ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataValidator()
        {
            EAURuleFor(m => m.ForeignIdentityBasicData).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.OtherCitizenship).EAUInjectValidator();
            EAURuleFor(m => m.Address).EAUInjectValidator();
        }
    }
}
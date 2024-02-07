using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensValidator : EAUValidator<ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizens>
    {
        public ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            //EAURuleFor(m => m.IdentificationPhotoAndSignature).EAUInjectValidator();
            EAURuleFor(m => m.IssuingPoliceDepartment).EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
    }
}
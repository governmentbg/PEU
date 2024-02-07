using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class RequestForApplicationForIssuingDuplicateDrivingLicenseValidator : EAUValidator<RequestForApplicationForIssuingDuplicateDrivingLicense>
    {
        public RequestForApplicationForIssuingDuplicateDrivingLicenseValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
        }
    }
}
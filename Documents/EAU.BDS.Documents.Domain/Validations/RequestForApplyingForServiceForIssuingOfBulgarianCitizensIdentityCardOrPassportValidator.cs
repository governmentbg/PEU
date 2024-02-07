using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportValidator : EAUValidator<RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport>
    {
        public RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.IdentificationDocuments).CollectionWithAtLeastOneElement();
        }
    }
}
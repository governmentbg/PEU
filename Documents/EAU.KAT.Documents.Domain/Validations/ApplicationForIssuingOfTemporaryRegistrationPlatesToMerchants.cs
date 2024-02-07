using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsValidator : EAUValidator<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants>
    {
        public ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.MerchantData).RequiredSection().EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData).RequiredSection().EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
    }
}
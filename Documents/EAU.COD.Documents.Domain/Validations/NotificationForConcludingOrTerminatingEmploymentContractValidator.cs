using EAU.COD.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.COD.Documents.Domain.Validations
{
    public class NotificationForConcludingOrTerminatingEmploymentContractValidator : EAUValidator<NotificationForConcludingOrTerminatingEmploymentContract>
    {
        public NotificationForConcludingOrTerminatingEmploymentContractValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.NotificationForConcludingOrTerminatingEmploymentContractData).RequiredSection().EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
    }
}
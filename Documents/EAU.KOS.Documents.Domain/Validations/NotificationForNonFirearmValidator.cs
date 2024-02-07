using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models.Forms;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class NotificationForNonFirearmValidator : EAUValidator<NotificationForNonFirearm>
    {
        public NotificationForNonFirearmValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.NotificationForNonFirearmData).EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
    }
}
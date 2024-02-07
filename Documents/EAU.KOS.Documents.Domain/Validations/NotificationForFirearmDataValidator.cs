using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class NotificationForFirearmDataValidator : EAUValidator<NotificationForFirearmData>
    {
        public NotificationForFirearmDataValidator()
        {
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.ApplicantInformation).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.TechnicalSpecificationsOfWeapons).RequiredField();
            EAURuleForEach(m => m.TechnicalSpecificationsOfWeapons).EAUInjectValidator();

            EAURuleFor(m => m.PurchaserUIC).RequiredField().UICBulstatValidation();
        }
    }
}
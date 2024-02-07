using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class NotificationForControlCouponDataValidator : EAUValidator<NotificationForControlCouponData>
    {
        public NotificationForControlCouponDataValidator()
        {
            EAURuleFor(m => m.LicenseInfo).EAUInjectValidator();
            EAURuleForEach(m => m.ControlCouponData).EAUInjectValidator();
            EAURuleFor(m => m.ControlCouponData).CollectionWithAtLeastOneElement();
        }
    }
}
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;
using FluentValidation;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class ControlCouponDataItemValidator : EAUValidator<ControlCouponDataItem>
    {
        public ControlCouponDataItemValidator()
        {
            EAURuleFor(m => m.CategoryCode).RequiredField();
            EAURuleFor(m => m.CategoryName).RequiredField();
            EAURuleFor(m => m.Item).RequiredField().SetInheritanceValidator(v =>
            {
                v.EAUAdd(new AmmunitionValidator());
                v.EAUAdd(new PyrotechnicsValidator());
                v.EAUAdd(new ExplosivesValidator());
                v.EAUAdd(new FirearmsValidator());
            });
        }
    }
}
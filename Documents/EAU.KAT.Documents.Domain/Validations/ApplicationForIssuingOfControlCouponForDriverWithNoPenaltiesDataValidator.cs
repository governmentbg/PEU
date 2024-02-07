using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataValidator : EAUValidator<ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesData>
    {
        public ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataValidator()
        {
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredField().EAUInjectValidator();

            When(m => m.PermanentAddress != null, () =>
            {
                EAURuleFor(m => m.PermanentAddress).EAUInjectValidator();
            });

            When(m => m.CurrentAddress != null, () =>
            {
                EAURuleFor(m => m.CurrentAddress).EAUInjectValidator();
            });
        }
    }
}
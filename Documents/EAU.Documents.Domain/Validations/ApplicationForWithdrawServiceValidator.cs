using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.Documents.Domain.Validations
{
    public class ApplicationForWithdrawServiceValidator : EAUValidator<ApplicationForWithdrawService>
    {
        public ApplicationForWithdrawServiceValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForWithdrawServiceData).EAUInjectValidator();
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForWithdrawService> context)
        {
            ValidationResult validationRes = base.Validate(context);

            return validationRes;
        }
    }
}
using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ApplicationForWithdrawServiceDataValidator : EAUValidator<ApplicationForWithdrawServiceData>
    {
        public ApplicationForWithdrawServiceDataValidator()
        {
            EAURuleFor(m => m.CaseFileURI).RequiredField();
            EAURuleFor(m => m.IssuingDocument).RequiredField();
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredField().EAUInjectValidator();
        }
    }
}
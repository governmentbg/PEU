using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ElectronicServiceApplicantValidator : EAUValidator<ElectronicServiceApplicant>
    {
        public ElectronicServiceApplicantValidator()
        {
            EAURuleFor(m => m.RecipientGroups).CollectionWithOneElement();
            EAURuleForEach(m => m.RecipientGroups).EAUInjectValidator(); //RecipientGroupValidator
            EAURuleFor(m => m.EmailAddress).RequiredField().EmailValidation();
        }
    }
}

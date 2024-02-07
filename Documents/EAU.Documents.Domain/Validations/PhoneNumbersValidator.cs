using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class PhoneNumbersValidator : EAUValidator<PhoneNumbers>
    {
        public PhoneNumbersValidator()
        {
            EAURuleForEach(m => m.PhoneNumberCollection).PhoneValidation();
        }
    }
}

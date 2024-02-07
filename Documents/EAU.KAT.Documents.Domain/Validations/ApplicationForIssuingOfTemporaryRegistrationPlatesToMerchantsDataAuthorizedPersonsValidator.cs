using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataAuthorizedPersonsValidator : EAUValidator<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataAuthorizedPersons>
    {
        public ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataAuthorizedPersonsValidator()
        {
            EAURuleFor(m => m.FullName)
                .RequiredField()
                .RangeLengthValidatior(1, 150);
            EAURuleFor(m => m.Identifier)
                .RequiredField()
                .EAUInjectValidator();
        }
    }
}

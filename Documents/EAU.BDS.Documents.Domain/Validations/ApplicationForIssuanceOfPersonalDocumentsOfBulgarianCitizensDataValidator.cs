using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataValidator : EAUValidator<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData>
    {
        public ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataValidator()
        {
            EAURuleFor(m => m.IdentificationDocuments).RequiredField();
            EAURuleFor(m => m.Person).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.Phone).RequiredField().MinLengthValidatior(1).PhoneValidation();
            EAURuleFor(m => m.ChangedNames).EAUInjectValidator();
            EAURuleFor(m => m.PermanentAddress).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.ChangedAddress).EAUInjectValidator();
            EAURuleFor(m => m.AuthorizedPersonData).EAUInjectValidator();
            EAURuleFor(m => m.FirstParentTrusteeGuardianData).EAUInjectValidator();
            EAURuleFor(m => m.SecondParentTrusteeGuardianData).EAUInjectValidator();
            EAURuleFor(m => m.OtherCitizenship).EAUInjectValidator();
            EAURuleFor(m => m.AbroadAddress).MatchesValidatior(@"^[а-яА-Яa-zA-Z\s+\d+~@#$%^&*()_{}|""':>=|!<.,/\\?;-]+$").WithEAUErrorCode(ErrorMessagesConstants.FieldValidationCyrillicLatinNumbersSymbols);
        }
    }
}
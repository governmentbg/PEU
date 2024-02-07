using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ParentTrusteeGuardianDataValidator : EAUValidator<ParentTrusteeGuardianData>
    {
        public ParentTrusteeGuardianDataValidator()
        {
            EAURuleFor(m => m.IdentityDocumentBasicData).EAUInjectValidator("All");
            EAURuleFor(m => m.PersonIdentificationData).EAUInjectValidator();
        }
    }
}
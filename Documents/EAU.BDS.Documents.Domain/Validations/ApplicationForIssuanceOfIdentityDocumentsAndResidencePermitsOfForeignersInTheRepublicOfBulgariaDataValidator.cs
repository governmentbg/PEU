using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations 
{
    public class ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaDataValidator : EAUValidator<ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData>
    {
        public ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaDataValidator()
        {
            EAURuleFor(m => m.ForeignIdentityBasicData).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.OtherIdentityDocument).EAUInjectValidator();
            EAURuleFor(m => m.ChangedNames).EAUInjectValidator();
            EAURuleFor(m => m.WasForeignerBulgarianCitizen).EAUInjectValidator();
            EAURuleFor(m => m.EntranceInTheRepublicOfBulgaria).EAUInjectValidator();
            EAURuleFor(m => m.MotherData).EAUInjectValidator();
            EAURuleFor(m => m.FatherData).EAUInjectValidator();
            EAURuleFor(m => m.SpouseData).EAUInjectValidator();
            EAURuleForEach(m => m.BrothersSistersData).EAUInjectValidator();
            EAURuleForEach(m => m.ChildrensData).EAUInjectValidator();
            EAURuleFor(m => m.ParentTrusteeGuardianData).EAUInjectValidator();
            EAURuleFor(m => m.MaintenanceProvidedBy).EAUInjectValidator();
            EAURuleForEach(m => m.ChildrensListedInForeignerPassport).EAUInjectValidator();
            EAURuleForEach(m => m.FormerResidencesOfTheForeigner).EAUInjectValidator();
            EAURuleFor(m => m.OtherCitizenship).EAUInjectValidator();
        }
    }
}
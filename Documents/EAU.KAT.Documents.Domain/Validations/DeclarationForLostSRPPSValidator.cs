using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class DeclarationForLostSRPPSValidator : EAUValidator<DeclarationForLostSRPPS>
    {
        public DeclarationForLostSRPPSValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleFor(m => m.Data).EAUInjectValidator();
        }
    }
}

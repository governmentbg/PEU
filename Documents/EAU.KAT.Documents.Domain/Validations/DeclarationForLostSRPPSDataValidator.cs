using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class DeclarationForLostSRPPSDataValidator : EAUValidator<DeclarationForLostSRPPSData>
    {
        public DeclarationForLostSRPPSDataValidator()
        {
            EAURuleFor(m => m.Declaration).RequiredField().EAUInjectValidator();
        }
    }
}

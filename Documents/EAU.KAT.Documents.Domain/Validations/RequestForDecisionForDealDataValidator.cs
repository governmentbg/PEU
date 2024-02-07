using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class RequestForDecisionForDealDataValidator : EAUValidator<RequestForDecisionForDealData>
    {
        public RequestForDecisionForDealDataValidator()
        {
            EAURuleFor(m => m.VehicleRegistrationData).RequiredField().EAUInjectValidator("Full");
            EAURuleFor(m => m.BuyersCollection).RequiredField();
            EAURuleFor(m => m.OwnersCollection).RequiredField();
            EAURuleForEach(m => m.BuyersCollection).EAUInjectValidator();
            EAURuleForEach(m => m.OwnersCollection).EAUInjectValidator();           
        }
    }
}
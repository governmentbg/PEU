using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleChangeOwnershipDataValidator : EAUValidator<VehicleChangeOwnershipData>
    {
        public VehicleChangeOwnershipDataValidator()
        {
            RuleSet("Change", () =>
            {
                EAURuleFor(m => m.CurrentOwnersCollection).CollectionWithAtLeastOneElement();
                EAURuleFor(m => m.NewOwnersCollection).CollectionWithAtLeastOneElement();
                EAURuleFor(m => m.VehicleRegistrationData).RequiredField().EAUInjectValidator("Full");               
                EAURuleForEach(m => m.CurrentOwnersCollection).EAUInjectValidator();
                EAURuleForEach(m => m.NewOwnersCollection).EAUInjectValidator();
            });

            RuleSet("Barter", () =>
            {
                EAURuleFor(m => m.CurrentOwnersCollection).CollectionWithAtLeastOneElement();
                EAURuleFor(m => m.NewOwnersCollection).CollectionWithAtLeastOneElement();
                EAURuleFor(m => m.VehicleRegistrationData).RequiredField().EAUInjectValidator("Full");
                EAURuleForEach(m => m.CurrentOwnersCollection).EAUInjectValidator();
                EAURuleForEach(m => m.NewOwnersCollection).EAUInjectValidator();
            });

        }
    }
}
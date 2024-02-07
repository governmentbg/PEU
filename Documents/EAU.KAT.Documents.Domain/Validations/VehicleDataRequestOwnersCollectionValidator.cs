using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;
using System.Linq;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleDataRequestOwnersCollectionValidator : EAUValidator<VehicleDataRequestOwnersCollection>
    {
        public VehicleDataRequestOwnersCollectionValidator()
        {
            EAURuleFor(m => m.Owners)
                .RequiredField()
                .Must((obj) => 
                {
                    if (obj == null || obj.Count <= 1)
                        return true;
                    else
                        return obj.Count == obj.Select(el => 
                        {
                            if (el.Item is PersonIdentifier identifier)
                                return identifier.Item;
                            else
                                return el.Item.ToString();
                         }).Distinct().Count();
                }).WithEAUErrorCode(ErrorMessagesConstants.DuplicateElementsInCollection);
            EAURuleForEach(m => m.Owners).EAUInjectValidator();
        }
    }
}
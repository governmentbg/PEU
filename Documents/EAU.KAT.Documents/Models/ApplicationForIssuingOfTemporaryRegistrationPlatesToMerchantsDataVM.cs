using EAU.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataVM
    {
        public int? TemporaryPlatesCount { get; set; }

        public string OperationalNewVehicleMakes { get; set; }

        public string OperationalSecondHandVehicleMakes { get; set; }

        public EntityAddress VehicleDealershipAddress { get; set; }

        public AuthorizedPersonCollectionVM AuthorizedPersons { get; set; }

        public string Phone { get; set; }

        public bool? AgreementToReceiveERefusal { get; set; }
    }
}
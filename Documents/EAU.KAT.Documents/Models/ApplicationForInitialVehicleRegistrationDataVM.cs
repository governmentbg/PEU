using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Models
{
    public class ApplicationForInitialVehicleRegistrationDataVM
    {
        public ApplicationForInitialVehicleRegistrationDataVehicleIdentificationData VehicleIdentificationData { get; set; }

        public ApplicationForInitialVehicleRegistrationDataOwnersCollectionVM OwnersCollection { get; set; }

        public InitialVehicleRegistrationUserOrOwnerOfSRMPSVM OwnerOfRegistrationCoupon { get; set; }

        public bool? OtherUserVehicleRepresentative { get; set; }

        public InitialVehicleRegistrationUserOrOwnerOfSRMPSVM VehicleUserData { get; set; }

        public string Phone { get; set; }

        public bool? AgreementToReceiveERefusal { get; set; }
    }
}
namespace EAU.KAT.Documents.Models
{
    public class InitialVehicleRegistrationOwnerDataVM : InitialVehicleRegistrationUserOrOwnerOfSRMPSVM
    {
        public bool? IsOwnerOfVehicleRegistrationCoupon { get; set; }
    }

    public class InitialVehicleRegistrationUserOrOwnerOfSRMPSVM : OwnerVM
    {
        public bool? IsVehicleRepresentative { get; set; }
    }
}

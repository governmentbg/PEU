using EAU.KAT.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models
{
    public class ApplicationForChangeRegistrationOfVehiclesDataVM
    {
        public VehicleOwnershipChangeType VehicleOwnershipChangeType { get; set; }

        public VehicleRegistrationChangeWithBarterVM ChangeRegistrationWithBarterVM { get; set; }

        public VehicleRegistrationChangeVM ChangeRegistrationWithPersonOrEntity { get; set; }

        public string NotaryIdentityNumber { get; set; }
    }

    /// <summary>
    /// Замяна на ППС между двама собственици
    /// </summary>
    public class VehicleRegistrationChangeWithBarterVM
    {
        public VehicleRegistrationDataVM FirstVehicleData { get; set; }

        public VehicleRegistrationDataVM SecondVehicleData { get; set; }

        public List<VehicleOwnerDataVM> FirstVehicleOwners { get; set; }

        public List<VehicleOwnerDataVM> SecondVehicleOwners { get; set; }
    }

    /// <summary>
    /// Промяна на собственост на регистрирани ППС, собственост на едно ЮЛ или ФЛ
    /// </summary>
    public class VehicleRegistrationChangeVM
    {
        public List<VehicleRegistrationDataVM> VehicleRegistrationData { get; set; }

        public List<VehicleOwnerDataVM> CurrentOwners { get; set; }

        public List<VehicleBuyerDataVM> NewOwners { get; set; }
    }
}
using EAU.KAT.Documents.Domain.Models;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models
{
    public class VehicleDataItemVM
    {
        public string RegistrationNumber
        {
            get;
            set;
        }
        public string MakeModel
        {
            get;
            set;
        }
        public string PreviousRegistrationNumber
        {
            get;
            set;
        }
        public string IdentificationNumber
        {
            get;
            set;
        }
        public string EngineNumber
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }
        public DateTime? VehicleFirstRegistrationDate
        {
            get;
            set;
        }

        public List<VehicleDataItemVehicleSuspension> VehicleSuspension
        {
            get;
            set;
        }
        public DateTime? VehicleOwnerStartDate
        {
            get;
            set;
        }
        public DateTime? VehicleOwnerEndDate
        {
            get;
            set;
        }
    }
}

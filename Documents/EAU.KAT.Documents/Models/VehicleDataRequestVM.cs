using EAU.KAT.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models
{
    public class VehicleDataRequestVM
    {
        public VehicleRegistrationData RegistrationData { get; set; }

        public OwnersCollectionVM OwnersCollection { get; set; }

        public string ServiceCode { get; set; }

        public string ServiceName { get; set; }

        public List<AISKATReason> Reasons { get; set; }

        public string Phone { get; set; }

        public bool? AgreementToReceiveERefusal { get; set; }
    }
}
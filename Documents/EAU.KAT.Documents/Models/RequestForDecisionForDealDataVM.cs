using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Models
{

    public class RequestForDecisionForDealDataVM
    {
        public PersonAddress PermanentAddress
        {
            get;
            set;
        }

        public List<VehicleOwnerDataVM> OwnersCollection
        {
            get;
            set;
        }

        public List<VehicleBuyerDataVM> BuyersCollection
        {
            get;
            set;
        }

        public VehicleRegistrationDataVM VehicleRegistrationData
        {
            get;
            set;
        }

        public string NotaryIdentityNumber
        {
            get;
            set;
        }
    }
}

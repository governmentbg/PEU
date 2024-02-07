using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents.Domain.Models
{
    public class GRAOAddress
    {
        public string DistrictGRAOCode { get; set; }

        public string DistrictGRAOName { get; set; }

        public string MunicipalityGRAOCode { get; set; }

        public string MunicipalityGRAOName { get; set; }

        public string SettlementGRAOCode { get; set; }

        public string SettlementGRAOName { get; set; }

        public string StreetGRAOCode { get; set; }

        public string StreetText { get; set; }

        public string BuildingNumber { get; set; }

        public string Entrance { get; set; }

        public string Floor { get; set; }

        public string Apartment { get; set; }
    }
}

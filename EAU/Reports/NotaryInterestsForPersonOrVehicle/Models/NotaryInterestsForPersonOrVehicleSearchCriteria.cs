using EAU.Common.Models;
using System;

namespace EAU.Reports.NotaryInterestsForPersonOrVehicle.Models
{
    /// <summary>
    /// Критерии за търсене за справка за проявен интерес от нотариална кантора за физическо лице или МПС
    /// </summary>
    public class NotaryInterestsForPersonOrVehicleSearchCriteria : BasePagedSearchCriteria
    {
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public string VehicleRegNumber { get; set; }

        public string PersonIdentifier { get; set; }
    }
}
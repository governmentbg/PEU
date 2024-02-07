using EAU.Audit.Models;
using System;

namespace EAU.Reports.NotaryInterestsForPersonOrVehicle.Models
{
    public class NotaryInterestsForPersonOrVehicleReportData
    {
        public string NotaryUserEmail { get; set; }
        public string NotaryUserNames { get; set; }
        public string NotaryUserIdentifier { get; set; }
        public DocumentAccessedDataValue[] DocumentAccessedDataValues { get; set; }
        public string IPAddress { get; set; }
        public DateTime? InterestDate { get; set; }
    }
}

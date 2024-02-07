using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Reports.NotaryInterestsForPersonOrVehicle.Models
{
    public class DocumentAccessDataReportSearchCriteria
    {
        public byte? DataType { get; set; }
        public string DataValue { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int? DocumentTypeId { get; set; }
        public byte[] DataTypesInResult { get; set; }
    }
}

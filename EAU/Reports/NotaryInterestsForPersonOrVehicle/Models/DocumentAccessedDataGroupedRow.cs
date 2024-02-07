using EAU.Utilities;
using System;

namespace EAU.Reports.NotaryInterestsForPersonOrVehicle.Models
{
    public class DocumentAccessedDataGroupedRow
    {
        [DapperColumn("document_uri")]
        public string DocumentUri { get; set; }

        [DapperColumn("document_type_id")]
        public int? DocumentTypeId { get; set; }

        [DapperColumn("group_data")]
        public string GroupData { get; set; }

        [DapperColumn("created_by")]
        public int? UserId { get; set; }

        [DapperColumn("applicant_names")]
        public string ApplicantNames { get; set; }

        [DapperColumn("applicant_identifier")]
        public string ApplicantIdentifier { get; set; }

        [DapperColumn("created_on")]
        public DateTime? DateOn { get; set; }

        [DapperColumn("ip_address")]
        public byte[] IpAddress { get; set; }
    }
}

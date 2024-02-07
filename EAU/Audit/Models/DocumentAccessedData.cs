using EAU.Utilities;
using System.Collections.Generic;

namespace EAU.Audit.Models
{
    public class DocumentAccessedData
    {
        [DapperColumn("id")]
        public long? Id { get; set; }

        [DapperColumn("document_uri")]
        public string DocumentUri { get; set; }

        [DapperColumn("document_type_id")]
        public int? DocumentTypeId { get; set; }

        [DapperColumn("applicant_names")]
        public string ApplicantNames { get; set; }

        [DapperColumn("applicant_names")]
        public string ApplicantIdentifier { get; set; }

        /// <summary>
        /// IP адрес на потребителя, извършващ действието.
        /// </summary>
        [DapperColumn("ip_address")]
        public byte[] IpAddress { get; set; }

        public IEnumerable<DocumentAccessedDataValue> DataValues { get; set; }
    }

    public class DocumentAccessedDataValue
    {
        [DapperColumn("document_accessed_data_id")]
        public long? DocumentAccessedDataId { get; set; }

        [DapperColumn("data_type")]
        public DocumentAccessedDataTypes? DataType { get; set; }

        [DapperColumn("data_value")]
        public string DataValue { get; set; }
    }

    public enum DocumentAccessedDataTypes : byte
    {
        /// <summary>
        /// ЕГН/ЛНЧ/ЛН
        /// </summary>
        PersonIdentifier = 1,

        /// <summary>
        /// ЕГН/ЛНЧ/ЛН и имена
        /// </summary>
        PersonIdentifierAndNames = 2,

        /// <summary>
        /// Регистрационен номер на ППС
        /// </summary>
        VehicleRegNumber = 3
    }
}

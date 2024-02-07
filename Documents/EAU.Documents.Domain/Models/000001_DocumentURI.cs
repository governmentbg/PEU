using System;
using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    /// <summary>
    /// УРИ на регистриран документ в официален документен регистър
    /// </summary>
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000001")]
    public class DocumentURI
    {
        public DocumentURI()
        { 
        }

        /// <summary>
        /// Регистров индекс.
        /// </summary>
        public int? RegisterIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Пореден номер на документ.
        /// </summary>
        public int? SequenceNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Дата на получаване или подписване на документ.
        /// </summary>
        [XmlElement(DataType = "date")]
        public DateTime? ReceiptOrSigningDate
        {
            get;
            set;
        }
    }
}

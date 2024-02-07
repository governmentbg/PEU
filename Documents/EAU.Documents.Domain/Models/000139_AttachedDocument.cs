using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    /// <summary>
    /// Приложен документ.
    /// </summary>
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000139")]
    [XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000139")]
    public class AttachedDocument 
    {
        [XmlElement("AttachedDocumentFileContent")]
        public byte[] FileContent
        {
            get;
            set;
        }

        [XmlElement("AttachedDocumentDescription")]
        public string Description
        {
            get;
            set;
        }

        [XmlElement("AttachedDocumentUniqueIdentifier")]
        public string UniqueIdentifier
        {
            get;
            set;
        }

        [XmlElement("FileType")]
        public string FileType
        {
            get;
            set;
        }

        [XmlElement("AttachedDocumentFileName")]
        public string FileName
        {
            get;
            set;
        }

        [XmlElement("AttachedDocumentTypeCode")]
        public string TypeCode
        {
            get;
            set;
        }
    }
}

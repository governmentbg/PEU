using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000010")]
    public partial class ForeignCitizenIdentityDocument 
    {
        public string DocumentNumber
        {
            get;
            set;
        }

        public string DocumentType
        {
            get;
            set;
        }
    }
}

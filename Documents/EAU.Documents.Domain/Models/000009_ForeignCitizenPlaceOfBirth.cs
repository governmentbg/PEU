using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000009")]
    public partial class ForeignCitizenPlaceOfBirth 
    {
        public string CountryCode
        {
            get;
            set;
        }

        public string CountryName
        {
            get;
            set;
        }

        public string SettlementName
        {
            get;
            set;
        }
    }
}

using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000006", IncludeInSchema = false)]
    [System.SerializableAttribute()]
    public enum PersonIdentifierChoiceType
    {
        /// <summary>
        /// Egn.
        /// </summary>
        EGN = 0,

        /// <summary>
        /// LNCH.
        /// </summary>
        LNCh = 1,
    }

    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000006")]
    [System.SerializableAttribute()]
    public partial class PersonIdentifier 
    {
        [XmlElement("EGN", typeof(string))]
        [XmlElement("LNCh", typeof(string))]
        [XmlChoiceIdentifier("ItemElementName")]
        public string Item
        {
            get;
            set;
        }

        [XmlIgnore()]
        public PersonIdentifierChoiceType ItemElementName
        {
            get;
            set;
        }
    }
}

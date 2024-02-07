using System;
using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000011")]
    public partial class ForeignCitizenBasicData 
    {
        private DateTime? birthDateField;

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public ForeignCitizenNames Names
        {
            get;
            set;
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BirthDateSpecified
        {
            get
            {
                return this.birthDateField.HasValue;
            }
        }

        [System.Xml.Serialization.XmlElementAttribute(DataType = "date", Order = 2)]
        public DateTime? BirthDate
        {
            get { return birthDateField; }
            set { birthDateField = value; }
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public ForeignCitizenPlaceOfBirth PlaceOfBirth
        {
            get;
            set;
        }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public ForeignCitizenIdentityDocument IdentityDocument
        {
            get;
            set;
        }
    }
}

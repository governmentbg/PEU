using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    [XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000012")]
    public class ElectronicStatementAuthor 
    {
        [XmlType(IncludeInSchema = false)]
        public enum ItemChoiceType
        {
            /// <summary>
            /// Person.
            /// </summary>
            Person,

            /// <summary>
            /// ForeignCitizen.
            /// </summary>
            ForeignCitizen
        }      
        private object authorQualityField;
        [System.Xml.Serialization.XmlElementAttribute("AuthorQuality", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("AuthorQualityType", typeof(ElectronicServiceAuthorQualityType))]
        public object AuthorQuality
        {
            get
            {
                return this.authorQualityField;
            }
            set
            {
                this.authorQualityField = value;
            }
        }

        [XmlElement(ElementName = "Person", Type = typeof(PersonBasicData))]
        [XmlElement(ElementName = "ForeignCitizen", Type = typeof(ForeignCitizenBasicData))]
        [XmlChoiceIdentifier(MemberName = "ItemElementName")]
        public object Item
        {
            get;
            set;
        }
        
        [XmlIgnore]
        public PersonBasicData ItemPersonBasicData
        {

            get { return Item as PersonBasicData; }
        }

        [XmlIgnore]
        public ForeignCitizenBasicData ItemForeignCitizenBasicData
        {
            get { return Item as ForeignCitizenBasicData; }
        }
        
        [XmlIgnore]
        public ItemChoiceType ItemElementName
        {
            get
            {
                if (Item is PersonBasicData)
                    return ItemChoiceType.Person;
                else if (Item is ForeignCitizenBasicData)
                    return ItemChoiceType.ForeignCitizen;
                else
                    return ItemChoiceType.Person;
            }
            set
            {
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/R-1050")]
    public enum ElectronicServiceAuthorQualityType
    {
        //лично качество (за собствени нужди)
        [System.Xml.Serialization.XmlEnumAttribute("R-1001")]
        Personal,
        //в качеството на пълномощник на друго физическо или юридическо лице
        [System.Xml.Serialization.XmlEnumAttribute("R-1002")]
        Representative,
        //в качеството на законен представител на юридическо лице
        [System.Xml.Serialization.XmlEnumAttribute("R-1003")]
        LegalRepresentative,
        //в качеството на длъжностно лице
        [System.Xml.Serialization.XmlEnumAttribute("R-1004")]
        Official,
    }
}

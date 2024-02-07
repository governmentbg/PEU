using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    public enum RecipientChoiceType
    {
        /// <summary>
        /// Person.
        /// </summary>
        Person,

        /// <summary>
        /// ForeignCitizen.
        /// </summary>
        ForeignPerson,

        /// <summary>
        /// Entity.
        /// </summary>
        Entity,

        /// <summary>
        /// ForeignEntity.
        /// </summary>
        ForeignEntity
    }

    [XmlRoot(Namespace = "http://ereg.egov.bg/segment/0009-000015")]
    public class ElectronicServiceRecipient 
    {
        private object _item;

        //[XmlType(IncludeInSchema = false)]
        //public enum ChoiceType
        //{
        //    /// <summary>
        //    /// Person.
        //    /// </summary>
        //    Person,

        //    /// <summary>
        //    /// ForeignCitizen.
        //    /// </summary>
        //    ForeignPerson,

        //    /// <summary>
        //    /// Entity.
        //    /// </summary>
        //    Entity,

        //    /// <summary>
        //    /// ForeignEntity.
        //    /// </summary>
        //    ForeignEntity
        //}

        [XmlElement(ElementName = "Person", Type = typeof(PersonBasicData))]
        [XmlElement(ElementName = "ForeignPerson", Type = typeof(ForeignCitizenBasicData))]
        [XmlElement(ElementName = "Entity", Type = typeof(EntityBasicData))]
        [XmlElement(ElementName = "ForeignEntity", Type = typeof(ForeignEntityBasicData))]
        [XmlChoiceIdentifier(MemberName = "ItemName")]
        public object Item
        {
            get { return _item; }
            set { _item = value; }
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
        public EntityBasicData ItemEntityBasicData
        {
            get { return Item as EntityBasicData; }
        }

        [XmlIgnore]
        public ForeignEntityBasicData ItemForeignEntityBasicData
        {
            get { return Item as ForeignEntityBasicData; }
        }

        [XmlIgnore]
        public RecipientChoiceType ItemName
        {
            get
            {
                if (Item is PersonBasicData)
                    return RecipientChoiceType.Person;
                else if (Item is ForeignCitizenBasicData)
                    return RecipientChoiceType.ForeignPerson;
                else if (Item is EntityBasicData)
                    return RecipientChoiceType.Entity;
                else if (Item is ForeignEntityBasicData)
                    return RecipientChoiceType.ForeignEntity;
                else
                    return RecipientChoiceType.Person;
            }
            set
            {
            }
        }
    }
}

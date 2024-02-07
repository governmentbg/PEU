using System;
using System.Linq;
using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000014", IncludeInSchema = false)]
    public enum ForeignEntityIdentifierChoiceType
    {
        /// <summary>
        /// Номер на регистър в друга държава.
        /// </summary>
        ForeignEntityIdentifier = 0,

        /// <summary>
        /// Допълнителни данни.
        /// </summary>
        ForeignEntityOtherData = 1,

        /// <summary>
        /// Име на регистър в друга държава.
        /// </summary>
        ForeignEntityRegister = 2,
    }

    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000014")]
    public partial class ForeignEntityBasicData 
    {
        private string[] _itemsField;

        public string ForeignEntityName
        {
            get;
            set;
        }

        public string CountryISO3166TwoLetterCode
        {
            get;
            set;
        }

        public string CountryNameCyrillic
        {
            get;
            set;
        }

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("ForeignEntityIdentifier", typeof(string))]
        //[System.Xml.Serialization.XmlElementAttribute("ForeignEntityOtherData", typeof(string))]
        //[System.Xml.Serialization.XmlElementAttribute("ForeignEntityRegister", typeof(string))]
        //[System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
        //public string[] Items
        //{
        //    get
        //    {
        //        return this._itemsField;
        //    }
        //    set
        //    {
        //        this._itemsField = value;
        //    }
        //}

        ///// <remarks/>
        //[System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
        //[System.Xml.Serialization.XmlIgnoreAttribute()]
        //public ForeignEntityIdentifierChoiceType[] ItemsElementName
        //{
        //    get
        //    {
        //        return this._itemsElementNameField;
        //    }
        //    set
        //    {
        //        this._itemsElementNameField = value;
        //    }
        //}

        [XmlElement("ForeignEntityIdentifier", typeof(string))]
        [XmlElement("ForeignEntityRegister", typeof(string))]
        [XmlElement("ForeignEntityOtherData", typeof(string))]
        [XmlChoiceIdentifier("ItemsElementName")]
        public string[] Items
        {
            get
            {
                if (string.IsNullOrEmpty(ForeignEntityRegister) &&
                    string.IsNullOrEmpty(ForeignEntityIdentifier) &&
                    string.IsNullOrEmpty(ForeignEntityOtherData))
                    return null;
                else if (!string.IsNullOrEmpty(ForeignEntityRegister) &&
                    !string.IsNullOrEmpty(ForeignEntityIdentifier) &&
                    string.IsNullOrEmpty(ForeignEntityOtherData))
                    return new string[] { ForeignEntityRegister, ForeignEntityIdentifier };
                else if (string.IsNullOrEmpty(ForeignEntityRegister) &&
                    string.IsNullOrEmpty(ForeignEntityIdentifier) &&
                    !string.IsNullOrEmpty(ForeignEntityOtherData))
                    return new string[] { ForeignEntityOtherData };
                else
                    throw new NotImplementedException();
            }
            set
            {
                _itemsField = value;

                if (_itemsField == null || 
                    _itemsField.Count() == 0)
                {
                    ForeignEntityRegister = null;
                    ForeignEntityIdentifier = null;
                    ForeignEntityOtherData = null;
                }
                else if (_itemsField.Count() == 2)
                {
                    ForeignEntityRegister = _itemsField[0];
                    ForeignEntityIdentifier = _itemsField[1];
                    ForeignEntityOtherData = null;
                }
                else if (_itemsField.Count() == 1)
                {
                    ForeignEntityRegister = null;
                    ForeignEntityIdentifier = null;
                    ForeignEntityOtherData = _itemsField[0];
                }
                else
                    throw new NotImplementedException();
            }
        }

        [XmlElement("ItemsElementName")]
        [XmlIgnore()]
        public ForeignEntityIdentifierChoiceType[] ItemsElementName
        {
            get
            {
                if (Items == null)
                    return new ForeignEntityIdentifierChoiceType[] { };
                else if (Items.Count() == 2)
                    return new ForeignEntityIdentifierChoiceType[] 
                    {
                        ForeignEntityIdentifierChoiceType.ForeignEntityRegister,
                        ForeignEntityIdentifierChoiceType.ForeignEntityIdentifier
                    };
                else if (Items.Count() == 1)
                    return new ForeignEntityIdentifierChoiceType[]
                    {
                        ForeignEntityIdentifierChoiceType.ForeignEntityOtherData
                    };
                else
                    throw new NotImplementedException("Not Allowed !!!");
            }
            set
            {
            }
        }

        [XmlIgnore]
        public string ForeignEntityRegister
        {
            get;
            set;
        }

        [XmlIgnore]
        public string ForeignEntityIdentifier
        {
            get;
            set;
        }

        [XmlIgnore]
        public string ForeignEntityOtherData
        {
            get;
            set;
        }
    }
}

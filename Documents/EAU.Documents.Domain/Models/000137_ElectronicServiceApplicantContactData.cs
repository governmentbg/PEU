using System.Collections.Generic;
using System.Xml.Serialization;

namespace EAU.Documents.Domain.Models
{
    /// <summary>
	/// Данни за контакт със заявителя на електронната административна услуга
	/// </summary>
    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
    public class ElectronicServiceApplicantContactData
    {
        #region Private Fields

        private PhoneNumbers _phoneNumbers;
        private FaxNumbers _faxNumbers;

        #endregion

        /// <summary>
        /// Код на област.
        /// </summary>
        public string DistrictCode
        {
            get;
            set;
        }

        /// <summary>
        /// Област.
        /// </summary>
        public string DistrictName
        {
            get;
            set;
        }

        /// <summary>
        /// Код на община.
        /// </summary>
        public string MunicipalityCode
        {
            get;
            set;
        }

        /// <summary>
        /// Община.
        /// </summary>
        public string MunicipalityName
        {
            get;
            set;
        }

        /// <summary>
        /// Код на населено място.
        /// </summary>
        public string SettlementCode
        {
            get;
            set;
        }

        /// <summary>
        /// Населено място.
        /// </summary>
        public string SettlementName
        {
            get;
            set;
        }

        /// <summary>
        /// Пощенски код.
        /// </summary>
        public string PostCode
        {
            get;
            set;
        }

        /// <summary>
        /// Описание на адрес в рамките на населено място.
        /// </summary>
        public string AddressDescription
        {
            get;
            set;
        }

        /// <summary>
        /// Пощенска кутия.
        /// </summary>
        public string PostOfficeBox
        {
            get;
            set;
        }

        /// <summary>
        /// Телефонни номерера за контакт със заявителя на електронна административна услуга.
        /// </summary>
        public PhoneNumbers PhoneNumbers
        {
            get
            {
                return _phoneNumbers;
            }
            set
            {
                _phoneNumbers = value;
            }
        }

        /// <summary>
        /// Номерa на факс за контакт със заявител на електронна административна услуга
        /// </summary>
        public FaxNumbers FaxNumbers
        {
            get
            {
                return _faxNumbers;
            }
            set
            {
                _faxNumbers = value;
            }
        }        
       
	}

    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
    public class PhoneNumbers
    {
        [XmlElement("PhoneNumber", typeof(string))]
        public List<string> PhoneNumberCollection
        {
            get;
            set;
        }
    }

    [XmlType(Namespace = "http://ereg.egov.bg/segment/0009-000137")]
    public class FaxNumbers
    {
        [XmlElement("ElectronicServiceApplicantFaxNumber", typeof(string))]
        public List<string> FaxNumberCollection
        {
            get;
            set;
        }
    }
}

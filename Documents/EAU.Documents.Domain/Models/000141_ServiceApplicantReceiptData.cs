using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAU.Documents.Domain.Models
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000141")]
    public partial class ServiceApplicantReceiptData
    {
        private ServiceResultReceiptMethods? serviceResultReceiptMethodField;
        private object itemField;
        public ServiceResultReceiptMethods? ServiceResultReceiptMethod
        {
            get
            {
                return this.serviceResultReceiptMethodField;
            }
            set
            {
                this.serviceResultReceiptMethodField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ServiceResultReceiptMethodSpecified
        {
            get
            {
                return ServiceResultReceiptMethod.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("ApplicantAdress", typeof(ServiceApplicantReceiptDataAddress))]
        [System.Xml.Serialization.XmlElementAttribute("MunicipalityAdministrationAdress", typeof(ServiceApplicantReceiptDataMunicipalityAdministrationAdress))]
        [System.Xml.Serialization.XmlElementAttribute("PostOfficeBox", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("UnitInAdministration", typeof(ServiceApplicantReceiptDataUnitInAdministration))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnore]
        public ServiceApplicantReceiptDataAddress ItemServiceApplicantReceiptDataAddress
        {

            get { return Item as ServiceApplicantReceiptDataAddress; }
        }

        [System.Xml.Serialization.XmlIgnore]
        public ServiceApplicantReceiptDataMunicipalityAdministrationAdress ItemServiceApplicantReceiptDataMunicipalityAdministrationAdress
        {

            get { return Item as ServiceApplicantReceiptDataMunicipalityAdministrationAdress; }
        }

        [System.Xml.Serialization.XmlIgnore]
        public ServiceApplicantReceiptDataUnitInAdministration ItemServiceApplicantReceiptDataUnitInAdministration
        {

            get { return Item as ServiceApplicantReceiptDataUnitInAdministration; }
        }

        [System.Xml.Serialization.XmlIgnore]
        public string ItemString
        {

            get { return Item as string; }
        }
    }

    /// <summary>
    /// Номенклатура на начините на получаване на резултат от услуга.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/nomenclature/0007-000013")]
    public enum ServiceResultReceiptMethods
    {

        /// <summary>
        /// Чрез електронна поща/уеб базирано приложение.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000076")]
        EmailOrWebApplication = 1,

        /// <summary>
        /// На гише.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000077")]
        Desk = 2,

        /// <summary>
        /// На гише в общинска администрация.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000078")]
        DeskInAdministration = 3,

        /// <summary>
        /// Чрез пощенски куриерски служби, на посочения адрес за кореспонденция.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000079")]
        CourierToAddress = 4,

        /// <summary>
        /// Чрез пощенски куриерски служби, на друг адрес.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000080")]
        CourierToOtherAddress = 5,

        /// <summary>
        /// Чрез пощенски куриерски служби, пощенска кутия.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("0006-000081")]
        CourierToMailBox = 6,

        /// <summary>
        /// На гише в поисканата от заявителя администрация.
        /// </summary>
        [System.Xml.Serialization.XmlEnumAttribute("R-6001")]
        UnitInAdministration = 7,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000141")]
    public partial class ServiceApplicantReceiptDataAddress : EkatteAddress
    {
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000141")]
    public partial class ServiceApplicantReceiptDataMunicipalityAdministrationAdress 
    {

        private string districtCodeField;

        private string districtNameField;

        private string municipalityCodeField;

        private string municipalityNameField;

        private string mayoraltyCodeField;

        private string mayoraltyField;

        private string areaCodeField;

        private string areaNameField;

        /// <remarks/>
        public string DistrictCode
        {
            get
            {
                return this.districtCodeField;
            }
            set
            {
                this.districtCodeField = value;
            }
        }

        /// <remarks/>
        public string DistrictName
        {
            get
            {
                return this.districtNameField;
            }
            set
            {
                this.districtNameField = value;
            }
        }

        /// <remarks/>
        public string MunicipalityCode
        {
            get
            {
                return this.municipalityCodeField;
            }
            set
            {
                this.municipalityCodeField = value;
            }
        }

        /// <remarks/>
        public string MunicipalityName
        {
            get
            {
                return this.municipalityNameField;
            }
            set
            {
                this.municipalityNameField = value;
            }
        }

        /// <remarks/>
        public string MayoraltyCode
        {
            get
            {
                return this.mayoraltyCodeField;
            }
            set
            {
                this.mayoraltyCodeField = value;
            }
        }

        /// <remarks/>
        public string Mayoralty
        {
            get
            {
                return this.mayoraltyField;
            }
            set
            {
                this.mayoraltyField = value;
            }
        }

        /// <remarks/>
        public string AreaCode
        {
            get
            {
                return this.areaCodeField;
            }
            set
            {
                this.areaCodeField = value;
            }
        }

        /// <remarks/>
        public string AreaName
        {
            get
            {
                return this.areaNameField;
            }
            set
            {
                this.areaNameField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000141")]
    public partial class ServiceApplicantReceiptDataUnitInAdministration
    {
        private EntityBasicData entityBasicDataField;
        private string administrativeDepartmentNameField;
        private string administrativeDepartmentCodeField;
        public EntityBasicData EntityBasicData
        {
            get
            {
                return this.entityBasicDataField;
            }
            set
            {
                this.entityBasicDataField = value;
            }
        }
        public string AdministrativeDepartmentName
        {
            get
            {
                return this.administrativeDepartmentNameField;
            }
            set
            {
                this.administrativeDepartmentNameField = value;
            }
        }
        public string AdministrativeDepartmentCode
        {
            get
            {
                return this.administrativeDepartmentCodeField;
            }
            set
            {
                this.administrativeDepartmentCodeField = value;
            }
        }
    }
}

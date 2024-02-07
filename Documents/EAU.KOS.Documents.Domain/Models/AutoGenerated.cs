using EAU.Documents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Xml.Serialization;

namespace EAU.KOS.Documents.Domain.Models
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3042")]
	public partial class ApplicationByFormAnnex9Data
	{
		private PersonalInformation personalInformationField;
		private string issuingDocumentField;
		private PersonBasicData personGrantedFromIssuingDocumentField;
		private string specificDataForIssuingDocumentsForKOSField;
		private bool? agreementToReceiveERefusalField;
		public PersonalInformation PersonalInformation
		{
			get
			{
				return this.personalInformationField;
			}
			set
			{
				this.personalInformationField = value;
			}
		}
		public string IssuingDocument
		{
			get
			{
				return this.issuingDocumentField;
			}
			set
			{
				this.issuingDocumentField = value;
			}
		}
		public PersonBasicData PersonGrantedFromIssuingDocument
		{
			get
			{
				return this.personGrantedFromIssuingDocumentField;
			}
			set
			{
				this.personGrantedFromIssuingDocumentField = value;
			}
		}
		public string SpecificDataForIssuingDocumentsForKOS
		{
			get
			{
				return this.specificDataForIssuingDocumentsForKOSField;
			}
			set
			{
				this.specificDataForIssuingDocumentsForKOSField = value;
			}
		}
		public bool? AgreementToReceiveERefusal
		{
			get
			{
				return this.agreementToReceiveERefusalField;
			}
			set
			{
				this.agreementToReceiveERefusalField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool AgreementToReceiveERefusalSpecified
		{
			get
			{
				return AgreementToReceiveERefusal.HasValue;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3114")]
	public partial class ApplicationByFormAnnex10Data
	{
		private PersonalInformation personalInformationField;
		private string issuingDocumentField;
		private PersonBasicData personGrantedFromIssuingDocumentField;
		private string specificDataForIssuingDocumentsForKOSField;
		private bool? agreementToReceiveERefusalField;
		public PersonalInformation PersonalInformation
		{
			get
			{
				return this.personalInformationField;
			}
			set
			{
				this.personalInformationField = value;
			}
		}
		public string IssuingDocument
		{
			get
			{
				return this.issuingDocumentField;
			}
			set
			{
				this.issuingDocumentField = value;
			}
		}
		public PersonBasicData PersonGrantedFromIssuingDocument
		{
			get
			{
				return this.personGrantedFromIssuingDocumentField;
			}
			set
			{
				this.personGrantedFromIssuingDocumentField = value;
			}
		}
		public string SpecificDataForIssuingDocumentsForKOS
		{
			get
			{
				return this.specificDataForIssuingDocumentsForKOSField;
			}
			set
			{
				this.specificDataForIssuingDocumentsForKOSField = value;
			}
		}
		public bool? AgreementToReceiveERefusal
		{
			get
			{
				return this.agreementToReceiveERefusalField;
			}
			set
			{
				this.agreementToReceiveERefusalField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool AgreementToReceiveERefusalSpecified
		{
			get
			{
				return AgreementToReceiveERefusal.HasValue;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3053")]
	public partial class NotificationForFirearmData
	{
		private PoliceDepartment issuingPoliceDepartmentField;
		private PersonalInformation applicantInformationField;
		private string purchaserUICField;
		private bool? agreementToReceiveERefusalField;
		private List<TechnicalSpecificationOfWeapon> technicalSpecificationsOfWeaponsField;
		public PoliceDepartment IssuingPoliceDepartment
		{
			get
			{
				return this.issuingPoliceDepartmentField;
			}
			set
			{
				this.issuingPoliceDepartmentField = value;
			}
		}
		public PersonalInformation ApplicantInformation
		{
			get
			{
				return this.applicantInformationField;
			}
			set
			{
				this.applicantInformationField = value;
			}
		}
		public string PurchaserUIC
		{
			get
			{
				return this.purchaserUICField;
			}
			set
			{
				this.purchaserUICField = value;
			}
		}
		public bool? AgreementToReceiveERefusal
		{
			get
			{
				return this.agreementToReceiveERefusalField;
			}
			set
			{
				this.agreementToReceiveERefusalField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool AgreementToReceiveERefusalSpecified
		{
			get
			{
				return AgreementToReceiveERefusal.HasValue;
			}
		}
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public List<TechnicalSpecificationOfWeapon> TechnicalSpecificationsOfWeapons
		{
			get
			{
				return this.technicalSpecificationsOfWeaponsField;
			}
			set
			{
				this.technicalSpecificationsOfWeaponsField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3054")]
	public partial class TechnicalSpecificationOfWeapon
	{
		private string weaponTypeCodeField;
		private string weaponTypeNameField;
		private string weaponPurposeCodeField;
		private string weaponPurposeNameField;
		private string makeField;
		private string modelField;
		private string caliberField;
		private string weaponNumberField;
		public string WeaponTypeCode
		{
			get
			{
				return this.weaponTypeCodeField;
			}
			set
			{
				this.weaponTypeCodeField = value;
			}
		}
		public string WeaponTypeName
		{
			get
			{
				return this.weaponTypeNameField;
			}
			set
			{
				this.weaponTypeNameField = value;
			}
		}
		public string WeaponPurposeCode
		{
			get
			{
				return this.weaponPurposeCodeField;
			}
			set
			{
				this.weaponPurposeCodeField = value;
			}
		}
		public string WeaponPurposeName
		{
			get
			{
				return this.weaponPurposeNameField;
			}
			set
			{
				this.weaponPurposeNameField = value;
			}
		}
		public string Make
		{
			get
			{
				return this.makeField;
			}
			set
			{
				this.makeField = value;
			}
		}
		public string Model
		{
			get
			{
				return this.modelField;
			}
			set
			{
				this.modelField = value;
			}
		}
		public string Caliber
		{
			get
			{
				return this.caliberField;
			}
			set
			{
				this.caliberField = value;
			}
		}
		public string WeaponNumber
		{
			get
			{
				return this.weaponNumberField;
			}
			set
			{
				this.weaponNumberField = value;
			}
		}
	}

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3051")]
    public partial class NotificationForNonFirearmData
    {
        private PoliceDepartment issuingPoliceDepartmentField;
        private WeaponNoticeType weaponNoticeTypeField;
        private PersonalInformation applicantInformationField;
        private string purchaserInformationField;
        private List<TechnicalSpecificationOfWeapon> technicalSpecificationsOfWeaponsField;
        private bool? agreementToReceiveERefusalField;
        public PoliceDepartment IssuingPoliceDepartment
        {
            get
            {
                return this.issuingPoliceDepartmentField;
            }
            set
            {
                this.issuingPoliceDepartmentField = value;
            }
        }
        public WeaponNoticeType WeaponNoticeType
        {
            get
            {
                return this.weaponNoticeTypeField;
            }
            set
            {
                this.weaponNoticeTypeField = value;
            }
        }
        public PersonalInformation ApplicantInformation
        {
            get
            {
                return this.applicantInformationField;
            }
            set
            {
                this.applicantInformationField = value;
            }
        }
        public string PurchaserInformation
        {
            get
            {
                return this.purchaserInformationField;
            }
            set
            {
                this.purchaserInformationField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<TechnicalSpecificationOfWeapon> TechnicalSpecificationsOfWeapons
        {
            get
            {
                return this.technicalSpecificationsOfWeaponsField;
            }
            set
            {
                this.technicalSpecificationsOfWeaponsField = value;
            }
        }
        public bool? AgreementToReceiveERefusal
        {
            get
            {
                return this.agreementToReceiveERefusalField;
            }
            set
            {
                this.agreementToReceiveERefusalField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AgreementToReceiveERefusalSpecified
        {
            get
            {
                return AgreementToReceiveERefusal.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3262")]
    public partial class LicenseInfo
    {
        private string permitNumberField;
        private string permitTypeField;
		private string permitTypeNameField;
		private string reasonField;
		private string reasonNameField;
		private DateTime? validityPeriodStartField;
		private DateTime? validityPeriodEndField;
		private string holderNameField;
		private string holderIdentifierField;
		private string contentField;
        private PoliceDepartment issuingPoliceDepartmentField;
        public string PermitNumber
        {
            get
            {
                return this.permitNumberField;
            }
            set
            {
                this.permitNumberField = value;
            }
        }
        public string PermitType
        {
            get
            {
                return this.permitTypeField;
            }
            set
            {
                this.permitTypeField = value;
            }
        }
        public string PermitTypeName
        {
            get
            {
                return this.permitTypeNameField;
            }
            set
            {
                this.permitTypeNameField = value;
            }
        }
        public string Reason
        {
            get
            {
                return this.reasonField;
            }
            set
            {
                this.reasonField = value;
            }
        }
        public string ReasonName
        {
            get
            {
                return this.reasonNameField;
            }
            set
            {
                this.reasonNameField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ValidityPeriodStart
        {
            get
            {
                return this.validityPeriodStartField;
            }
            set
            {
                this.validityPeriodStartField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValidityPeriodStartSpecified
        {
            get
            {
                return ValidityPeriodStart.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ValidityPeriodEnd
        {
            get
            {
                return this.validityPeriodEndField;
            }
            set
            {
                this.validityPeriodEndField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ValidityPeriodEndSpecified
        {
            get
            {
                return ValidityPeriodEnd.HasValue;
            }
        }
        public string HolderName
        {
            get
            {
                return this.holderNameField;
            }
            set
            {
                this.holderNameField = value;
            }
        }
        public string HolderIdentifier
        {
            get
            {
                return this.holderIdentifierField;
            }
            set
            {
                this.holderIdentifierField = value;
            }
        }
        public string Content
        {
            get
            {
                return this.contentField;
            }
            set
            {
                this.contentField = value;
            }
        }
        public PoliceDepartment IssuingPoliceDepartment
        {
            get
            {
                return this.issuingPoliceDepartmentField;
            }
            set
            {
                this.issuingPoliceDepartmentField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3263")]
    public partial class Ammunition
    {
        private string tradeNameField;
        private string numberOONField;
        private string caliberField;
        private string countField;
        public string TradeName
        {
            get
            {
                return this.tradeNameField;
            }
            set
            {
                this.tradeNameField = value;
            }
        }
        public string NumberOON
        {
            get
            {
                return this.numberOONField;
            }
            set
            {
                this.numberOONField = value;
            }
        }
        public string Caliber
        {
            get
            {
                return this.caliberField;
            }
            set
            {
                this.caliberField = value;
            }
        }
        public string Count
        {
            get
            {
                return this.countField;
            }
            set
            {
                this.countField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3264")]
    public partial class Pyrotechnics
    {
        private string tradeNameField;
        private string kindField;
        private string quantityField;
        private string measureField;
        public string TradeName
        {
            get
            {
                return this.tradeNameField;
            }
            set
            {
                this.tradeNameField = value;
            }
        }
        public string Kind
        {
            get
            {
                return this.kindField;
            }
            set
            {
                this.kindField = value;
            }
        }
        public string Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }
        public string Measure
        {
            get
            {
                return this.measureField;
            }
            set
            {
                this.measureField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3265")]
    public partial class Explosives
    {
        private string tradeNameField;
        private string numberOONField;
        private string quantityField;
        private string measureField;
        public string TradeName
        {
            get
            {
                return this.tradeNameField;
            }
            set
            {
                this.tradeNameField = value;
            }
        }
        public string NumberOON
        {
            get
            {
                return this.numberOONField;
            }
            set
            {
                this.numberOONField = value;
            }
        }
        public string Quantity
        {
            get
            {
                return this.quantityField;
            }
            set
            {
                this.quantityField = value;
            }
        }
        public string Measure
        {
            get
            {
                return this.measureField;
            }
            set
            {
                this.measureField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3266")]
    public partial class Firearms
    {
        private string brandField;
        private string modelField;
        private string caliberField;
        private string serialNumberField;
        private string kindCodeField;
        private string kindNameField;
        public string Brand
        {
            get
            {
                return this.brandField;
            }
            set
            {
                this.brandField = value;
            }
        }
        public string Model
        {
            get
            {
                return this.modelField;
            }
            set
            {
                this.modelField = value;
            }
        }
        public string Caliber
        {
            get
            {
                return this.caliberField;
            }
            set
            {
                this.caliberField = value;
            }
        }
        public string SerialNumber
        {
            get
            {
                return this.serialNumberField;
            }
            set
            {
                this.serialNumberField = value;
            }
        }
        public string KindCode
        {
            get
            {
                return this.kindCodeField;
            }
            set
            {
                this.kindCodeField = value;
            }
        }
        public string KindName
        {
            get
            {
                return this.kindNameField;
            }
            set
            {
                this.kindNameField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3267")]
    public partial class ControlCouponDataItem
    {
        private string categoryCodeField;
        private string categoryNameField;
        private object itemField;
        public string CategoryCode
        {
            get
            {
                return this.categoryCodeField;
            }
            set
            {
                this.categoryCodeField = value;
            }
        }
        public string CategoryName
        {
            get
            {
                return this.categoryNameField;
            }
            set
            {
                this.categoryNameField = value;
            }
        }
        [System.Xml.Serialization.XmlElement(typeof(Ammunition), ElementName = "Ammunition")]
        [System.Xml.Serialization.XmlElement(typeof(Pyrotechnics), ElementName = "Pyrotechnics")]
        [System.Xml.Serialization.XmlElement(typeof(Explosives), ElementName = "Explosives")]
        [System.Xml.Serialization.XmlElement(typeof(Firearms), ElementName = "Firearms")]
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
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3261")]
    public partial class NotificationForControlCouponData
    {
        private LicenseInfo licenseInfoField;
		private List<ControlCouponDataItem> controlCouponDataField;
        public LicenseInfo LicenseInfo
        {
            get
            {
                return this.licenseInfoField;
            }
            set
            {
                this.licenseInfoField = value;
            }
        }
        [System.Xml.Serialization.XmlArray(IsNullable = true)]
        [System.Xml.Serialization.XmlArrayItem(typeof(ControlCouponDataItem))]
        public List<ControlCouponDataItem> ControlCouponData 
		{
            get
            {
                return this.controlCouponDataField;
            }
            set
            {
                this.controlCouponDataField = value;
            }
        }
    }
}
using EAU.Documents.Domain.Models;
using System;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Domain.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3252")]
    public partial class DeclarationUndurArticle17Data
    {
        private BulgarianIdentityDocumentTypes? docTypeField;
        private PersonAddress permanentAddressField;
        private PersonAddress presentAddressField;
		private IssuingBgPersonalDocumentReasonData reasonDataField;
        [System.Xml.Serialization.XmlElementAttribute("DocType", typeof(BulgarianIdentityDocumentTypes?))]
        public BulgarianIdentityDocumentTypes? DocType
        {
            get
            {
                return this.docTypeField;
            }
            set
            {
                this.docTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocTypeSpecified
        {
            get
            {
                return this.docTypeField.HasValue;
            }
        }
        public PersonAddress PermanentAddress
        {
            get
            {
                return this.permanentAddressField;
            }
            set
            {
                this.permanentAddressField = value;
            }
        }
        public PersonAddress PresentAddress
        {
            get
            {
                return this.presentAddressField;
            }
            set
            {
                this.presentAddressField = value;
            }
        }
        public IssuingBgPersonalDocumentReasonData ReasonData
        {
            get
            {
                return this.reasonDataField;
            }
            set
            {
                this.reasonDataField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3253")]
    public partial class IssuingBgPersonalDocumentReasonData
    {
        private IssuingBgPersonalDocumentReasonNomenclature? reasonField;
        private string factsAndCircumstancesField;
        [System.Xml.Serialization.XmlElementAttribute("Reason", typeof(IssuingBgPersonalDocumentReasonNomenclature?))]
        public IssuingBgPersonalDocumentReasonNomenclature? Reason
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReasonSpecified
        {
            get
            {
                return this.reasonField.HasValue;
            }
        }
        public string FactsAndCircumstances
        {
            get
            {
                return this.factsAndCircumstancesField;
            }
            set
            {
                this.factsAndCircumstancesField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/value/R-3250")]
    public partial class IssuedBulgarianIdentityDocumentInfo
    {
        private int? issuingYearField;
        private BulgarianIdentityDocumentTypes? docTypeField;
        [System.Xml.Serialization.XmlElementAttribute("IssuingYear", typeof(int?))]
        public int? IssuingYear
        {
            get
            {
                return this.issuingYearField;
            }
            set
            {
                this.issuingYearField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IssuingYearSpecified
        {
            get
            {
                return this.issuingYearField.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("DocType", typeof(BulgarianIdentityDocumentTypes?))]
        public BulgarianIdentityDocumentTypes? DocType
        {
            get
            {
                return this.docTypeField;
            }
            set
            {
                this.docTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DocTypeSpecified
        {
            get
            {
                return this.docTypeField.HasValue;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/value/R-3034")]
    public partial class OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments
    {
        private string nessesaryInformationField;
        private List<string> documentsNumbersField;
        private List<IssuedBulgarianIdentityDocumentInfo> documentsInfosField;
        private List<DataContainsInCertificateNomenclature> includedDataInCertificateField;
        public string NessesaryInformation
        {
            get
            {
                return this.nessesaryInformationField;
            }
            set
            {
                this.nessesaryInformationField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("DocumentNumber", typeof(string))]
        public List<string> DocumentNumbers
        {
            get
            {
                return this.documentsNumbersField;
            }
            set
            {
                this.documentsNumbersField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("DocumentInfo", typeof(IssuedBulgarianIdentityDocumentInfo))]
        public List<IssuedBulgarianIdentityDocumentInfo> DocumentsInfos
        {
            get
            {
                return this.documentsInfosField;
            }
            set
            {
                this.documentsInfosField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("IncludedDataInCertificate", typeof(DataContainsInCertificateNomenclature))]
        public List<DataContainsInCertificateNomenclature> IncludsDataInCertificate
        {
            get
            {
                return this.includedDataInCertificateField;
            }
            set
            {
                this.includedDataInCertificateField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3033")]
    public partial class IssuedBulgarianIdentityDocumentsInPeriod
    {
        private System.DateTime identitityIssueDateField;
        private System.DateTime identitityExpireDateField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime IdentitityIssueDate
        {
            get
            {
                return this.identitityIssueDateField;
            }
            set
            {
                this.identitityIssueDateField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime IdentitityExpireDate
        {
            get
            {
                return this.identitityExpireDateField;
            }
            set
            {
                this.identitityExpireDateField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3041")]
    public partial class DocumentToBeIssuedFor
    {
        private object itemField;
        private DocumentMustServeTo documentMustServeToField;
        [System.Xml.Serialization.XmlElementAttribute("IssuedBulgarianIdentityDocumentsInPeriod", typeof(IssuedBulgarianIdentityDocumentsInPeriod))]
        [System.Xml.Serialization.XmlElementAttribute("OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments", typeof(OtherIndormationConnectedWithIssuedBulgarianIdentityDocuments))]
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
        public DocumentMustServeTo DocumentMustServeTo
        {
            get
            {
                return this.documentMustServeToField;
            }
            set
            {
                this.documentMustServeToField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3038")]
    public partial class ApplicationForIssuingDocumentData
    {
        private PersonalInformation personalInformationField;
        private DocumentToBeIssuedFor documentToBeIssuedForField;
        private AddressForIssuing addressForIssuingField;
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
        public DocumentToBeIssuedFor DocumentToBeIssuedFor
        {
            get
            {
                return this.documentToBeIssuedForField;
            }
            set
            {
                this.documentToBeIssuedForField = value;
            }
        }
        public AddressForIssuing AddressForIssuing
        {
            get
            {
                return this.addressForIssuingField;
            }
            set
            {
                this.addressForIssuingField = value;
            }
        }
    }

	
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3006")]
	public partial class ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData
	{
		private List<IdentityDocumentType> identificationDocumentsField;
		private PersonData personField;
		private string phoneField;
		private ChangedNames changedNamesField;
		private PersonAddress permanentAddressField;
		private PersonAddress changedAddressField;
		private ParentData motherDataField;
		private ParentData fatherDataField;
		private CitizenshipRegistrationBasicData spouseDataField;
		private AuthorizedPersonData authorizedPersonDataField;
		private ParentTrusteeGuardianData firstParentTrusteeGuardianDataField;
		private ParentTrusteeGuardianData secondParentTrusteeGuardianDataField;
		private Citizenship otherCitizenshipField;
		private string abroadAddressField;
		private bool? hasDocumentForDisabilitiesField;
		private string serviceCodeField;
		[System.Xml.Serialization.XmlArrayItemAttribute("IdentificationDocumentType", IsNullable = false)]
		public List<IdentityDocumentType> IdentificationDocuments
		{
			get
			{
				return this.identificationDocumentsField;
			}
			set
			{
				this.identificationDocumentsField = value;
			}
		}
		public PersonData Person
		{
			get
			{
				return this.personField;
			}
			set
			{
				this.personField = value;
			}
		}
		public string Phone
		{
			get
			{
				return this.phoneField;
			}
			set
			{
				this.phoneField = value;
			}
		}
		public ChangedNames ChangedNames
		{
			get
			{
				return this.changedNamesField;
			}
			set
			{
				this.changedNamesField = value;
			}
		}
		public PersonAddress PermanentAddress
		{
			get
			{
				return this.permanentAddressField;
			}
			set
			{
				this.permanentAddressField = value;
			}
		}
		public PersonAddress ChangedAddress
		{
			get
			{
				return this.changedAddressField;
			}
			set
			{
				this.changedAddressField = value;
			}
		}
		public ParentData MotherData
		{
			get
			{
				return this.motherDataField;
			}
			set
			{
				this.motherDataField = value;
			}
		}
		public ParentData FatherData
		{
			get
			{
				return this.fatherDataField;
			}
			set
			{
				this.fatherDataField = value;
			}
		}
		public CitizenshipRegistrationBasicData SpouseData
		{
			get
			{
				return this.spouseDataField;
			}
			set
			{
				this.spouseDataField = value;
			}
		}
		public AuthorizedPersonData AuthorizedPersonData
		{
			get
			{
				return this.authorizedPersonDataField;
			}
			set
			{
				this.authorizedPersonDataField = value;
			}
		}
		public ParentTrusteeGuardianData FirstParentTrusteeGuardianData
		{
			get
			{
				return this.firstParentTrusteeGuardianDataField;
			}
			set
			{
				this.firstParentTrusteeGuardianDataField = value;
			}
		}
		public ParentTrusteeGuardianData SecondParentTrusteeGuardianData
		{
			get
			{
				return this.secondParentTrusteeGuardianDataField;
			}
			set
			{
				this.secondParentTrusteeGuardianDataField = value;
			}
		}
		public Citizenship OtherCitizenship
		{
			get
			{
				return this.otherCitizenshipField;
			}
			set
			{
				this.otherCitizenshipField = value;
			}
		}
		public string AbroadAddress
		{
			get
			{
				return this.abroadAddressField;
			}
			set
			{
				this.abroadAddressField = value;
			}
		}
		public bool? HasDocumentForDisabilities
		{
			get
			{
				return this.hasDocumentForDisabilitiesField;
			}
			set
			{
				this.hasDocumentForDisabilitiesField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool HasDocumentForDisabilitiesSpecified
		{
			get
			{
				return HasDocumentForDisabilities.HasValue;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ServiceCode
		{
			get
			{
				return this.serviceCodeField;
			}
			set
			{
				this.serviceCodeField = value;
			}
		}
	}
	
	
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3020")]
	public partial class ChangedNames
	{
		private PersonNames namesField;
		private PersonNamesLatin namesLatinField;
		public PersonNames Names
		{
			get
			{
				return this.namesField;
			}
			set
			{
				this.namesField = value;
			}
		}
		public PersonNamesLatin NamesLatin
		{
			get
			{
				return this.namesLatinField;
			}
			set
			{
				this.namesLatinField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3018")]
	public partial class AuthorizedPersonData
	{
		private IdentityDocumentBasicData identityDocumentBasicDataField;
		private PersonIdentificationData personIdentificationDataField;
		public IdentityDocumentBasicData IdentityDocumentBasicData
		{
			get
			{
				return this.identityDocumentBasicDataField;
			}
			set
			{
				this.identityDocumentBasicDataField = value;
			}
		}
		public PersonIdentificationData PersonIdentificationData
		{
			get
			{
				return this.personIdentificationDataField;
			}
			set
			{
				this.personIdentificationDataField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3019")]
	public partial class ParentTrusteeGuardianData
	{
		private IdentityDocumentBasicData identityDocumentBasicDataField;
		private PersonIdentificationData personIdentificationDataField;
		public IdentityDocumentBasicData IdentityDocumentBasicData
		{
			get
			{
				return this.identityDocumentBasicDataField;
			}
			set
			{
				this.identityDocumentBasicDataField = value;
			}
		}
		public PersonIdentificationData PersonIdentificationData
		{
			get
			{
				return this.personIdentificationDataField;
			}
			set
			{
				this.personIdentificationDataField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3035")]
	public partial class ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData
	{
		private ForeignIdentityBasicData foreignIdentityBasicDataField;
		private TravelDocument travelDocumentField;
		private IdentityDocumentForeignCitizenBasicData previousIdentityDocumentField;
		private IdentityDocumentForeignCitizenBasicData otherIdentityDocumentField;
		private string stayInBulgariaField;
		private PersonAddress permanentAddressField;
		private PersonAddress presentAddressField;
		private ChangedNames changedNamesField;
		private WasForeignerBulgarianCitizen wasForeignerBulgarianCitizenField;
		private string abroadAddressField;
		private EntranceInTheRepublicOfBulgaria entranceInTheRepublicOfBulgariaField;
		private ForeignIdentityBasicData motherDataField;
		private ForeignIdentityBasicData fatherDataField;
		private ForeignIdentityBasicData spouseDataField;
		private List<ForeignIdentityBasicData> brothersSistersDataField;
		private List<ForeignIdentityBasicData> childrensDataField;
		private ParentTrusteeGuardianData parentTrusteeGuardianDataField;
		private MaintenanceProvidedBy maintenanceProvidedByField;
		private List<ChildrenListedInForeignerPassport> childrensListedInForeignerPassportField;
		private List<FormerResidenceOfTheForeigner> formerResidencesOfTheForeignerField;
		private Citizenship otherCitizenshipField;
		private bool? imposedCompulsoryAdministrativeMeasureField;
		private string serviceInformationField;
		private string serviceCodeField;
		public ForeignIdentityBasicData ForeignIdentityBasicData
		{
			get
			{
				return this.foreignIdentityBasicDataField;
			}
			set
			{
				this.foreignIdentityBasicDataField = value;
			}
		}
		public TravelDocument TravelDocument
		{
			get
			{
				return this.travelDocumentField;
			}
			set
			{
				this.travelDocumentField = value;
			}
		}
		public IdentityDocumentForeignCitizenBasicData PreviousIdentityDocument
		{
			get
			{
				return this.previousIdentityDocumentField;
			}
			set
			{
				this.previousIdentityDocumentField = value;
			}
		}
		public IdentityDocumentForeignCitizenBasicData OtherIdentityDocument
		{
			get
			{
				return this.otherIdentityDocumentField;
			}
			set
			{
				this.otherIdentityDocumentField = value;
			}
		}
		public string StayInBulgaria
		{
			get
			{
				return this.stayInBulgariaField;
			}
			set
			{
				this.stayInBulgariaField = value;
			}
		}
		public PersonAddress PermanentAddress
		{
			get
			{
				return this.permanentAddressField;
			}
			set
			{
				this.permanentAddressField = value;
			}
		}
		public PersonAddress PresentAddress
		{
			get
			{
				return this.presentAddressField;
			}
			set
			{
				this.presentAddressField = value;
			}
		}
		public ChangedNames ChangedNames
		{
			get
			{
				return this.changedNamesField;
			}
			set
			{
				this.changedNamesField = value;
			}
		}
		public WasForeignerBulgarianCitizen WasForeignerBulgarianCitizen
		{
			get
			{
				return this.wasForeignerBulgarianCitizenField;
			}
			set
			{
				this.wasForeignerBulgarianCitizenField = value;
			}
		}
		public string AbroadAddress
		{
			get
			{
				return this.abroadAddressField;
			}
			set
			{
				this.abroadAddressField = value;
			}
		}
		public EntranceInTheRepublicOfBulgaria EntranceInTheRepublicOfBulgaria
		{
			get
			{
				return this.entranceInTheRepublicOfBulgariaField;
			}
			set
			{
				this.entranceInTheRepublicOfBulgariaField = value;
			}
		}
		public ForeignIdentityBasicData MotherData
		{
			get
			{
				return this.motherDataField;
			}
			set
			{
				this.motherDataField = value;
			}
		}
		public ForeignIdentityBasicData FatherData
		{
			get
			{
				return this.fatherDataField;
			}
			set
			{
				this.fatherDataField = value;
			}
		}
		public ForeignIdentityBasicData SpouseData
		{
			get
			{
				return this.spouseDataField;
			}
			set
			{
				this.spouseDataField = value;
			}
		}
		[System.Xml.Serialization.XmlArrayItemAttribute("BroderSisterData", IsNullable = false)]
		public List<ForeignIdentityBasicData> BrothersSistersData
		{
			get
			{
				return this.brothersSistersDataField;
			}
			set
			{
				this.brothersSistersDataField = value;
			}
		}
		[System.Xml.Serialization.XmlArrayItemAttribute("ChildrenData", IsNullable = false)]
		public List<ForeignIdentityBasicData> ChildrensData
		{
			get
			{
				return this.childrensDataField;
			}
			set
			{
				this.childrensDataField = value;
			}
		}
		public ParentTrusteeGuardianData ParentTrusteeGuardianData
		{
			get
			{
				return this.parentTrusteeGuardianDataField;
			}
			set
			{
				this.parentTrusteeGuardianDataField = value;
			}
		}
		public MaintenanceProvidedBy MaintenanceProvidedBy
		{
			get
			{
				return this.maintenanceProvidedByField;
			}
			set
			{
				this.maintenanceProvidedByField = value;
			}
		}
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public List<ChildrenListedInForeignerPassport> ChildrensListedInForeignerPassport
		{
			get
			{
				return this.childrensListedInForeignerPassportField;
			}
			set
			{
				this.childrensListedInForeignerPassportField = value;
			}
		}
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public List<FormerResidenceOfTheForeigner> FormerResidencesOfTheForeigner
		{
			get
			{
				return this.formerResidencesOfTheForeignerField;
			}
			set
			{
				this.formerResidencesOfTheForeignerField = value;
			}
		}
		public Citizenship OtherCitizenship
		{
			get
			{
				return this.otherCitizenshipField;
			}
			set
			{
				this.otherCitizenshipField = value;
			}
		}
		public bool? ImposedCompulsoryAdministrativeMeasure
		{
			get
			{
				return this.imposedCompulsoryAdministrativeMeasureField;
			}
			set
			{
				this.imposedCompulsoryAdministrativeMeasureField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool ImposedCompulsoryAdministrativeMeasureSpecified
		{
			get
			{
				return ImposedCompulsoryAdministrativeMeasure.HasValue;
			}
		}
		public string ServiceInformation
		{
			get
			{
				return this.serviceInformationField;
			}
			set
			{
				this.serviceInformationField = value;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ServiceCode
		{
			get
			{
				return this.serviceCodeField;
			}
			set
			{
				this.serviceCodeField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3022")]
	public partial class ForeignIdentityBasicData
	{
		private ForeignCitizenData foreignCitizenDataField;
		private string eGNField;
		private string lNChField;
		private BIDEyesColor? eyesColorField;
		private BIDMaritalStatus? maritalStatusField;
		private int? heightField;
		private string phoneField;
		private string educationField;
		public ForeignCitizenData ForeignCitizenData
		{
			get
			{
				return this.foreignCitizenDataField;
			}
			set
			{
				this.foreignCitizenDataField = value;
			}
		}
		public string EGN
		{
			get
			{
				return this.eGNField;
			}
			set
			{
				this.eGNField = value;
			}
		}
		public string LNCh
		{
			get
			{
				return this.lNChField;
			}
			set
			{
				this.lNChField = value;
			}
		}
		public BIDEyesColor? EyesColor
		{
			get
			{
				return this.eyesColorField;
			}
			set
			{
				this.eyesColorField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool EyesColorSpecified
		{
			get
			{
				return EyesColor.HasValue;
			}
		}
		public BIDMaritalStatus? MaritalStatus
		{
			get
			{
				return this.maritalStatusField;
			}
			set
			{
				this.maritalStatusField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool MaritalStatusSpecified
		{
			get
			{
				return MaritalStatus.HasValue;
			}
		}
		public int? Height
		{
			get
			{
				return this.heightField;
			}
			set
			{
				this.heightField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool HeightSpecified
		{
			get
			{
				return Height.HasValue;
			}
		}
		public string Phone
		{
			get
			{
				return this.phoneField;
			}
			set
			{
				this.phoneField = value;
			}
		}
		public string Education
		{
			get
			{
				return this.educationField;
			}
			set
			{
				this.educationField = value;
			}
		}
	}
	
	
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3024")]
	public partial class WasForeignerBulgarianCitizen
	{
		private bool? wasPersonBulgarianCitizenField;
		private System.DateTime? fromDateField;
		private System.DateTime? toDateField;
		private ForeignCitizenNames foreignCitizenNamesField;
		public bool? WasPersonBulgarianCitizen
		{
			get
			{
				return this.wasPersonBulgarianCitizenField;
			}
			set
			{
				this.wasPersonBulgarianCitizenField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool WasPersonBulgarianCitizenSpecified
		{
			get
			{
				return WasPersonBulgarianCitizen.HasValue;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? FromDate
		{
			get
			{
				return this.fromDateField;
			}
			set
			{
				this.fromDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool FromDateSpecified
		{
			get
			{
				return FromDate.HasValue;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? ToDate
		{
			get
			{
				return this.toDateField;
			}
			set
			{
				this.toDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool ToDateSpecified
		{
			get
			{
				return ToDate.HasValue;
			}
		}
		public ForeignCitizenNames ForeignCitizenNames
		{
			get
			{
				return this.foreignCitizenNamesField;
			}
			set
			{
				this.foreignCitizenNamesField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3025")]
	public partial class EntranceInTheRepublicOfBulgaria
	{
		private System.DateTime? entranceInCountyrDateField;
		private string borderCheckpointField;
		private VisaDocument visaDocumentField;
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? EntranceInCountyrDate
		{
			get
			{
				return this.entranceInCountyrDateField;
			}
			set
			{
				this.entranceInCountyrDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool EntranceInCountyrDateSpecified
		{
			get
			{
				return EntranceInCountyrDate.HasValue;
			}
		}
		public string BorderCheckpoint
		{
			get
			{
				return this.borderCheckpointField;
			}
			set
			{
				this.borderCheckpointField = value;
			}
		}
		public VisaDocument VisaDocument
		{
			get
			{
				return this.visaDocumentField;
			}
			set
			{
				this.visaDocumentField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3026")]
	public partial class VisaDocument
	{
		private VisaTypes? visaTypesField;
		private string identityDocumentSeriesField;
		private string identityNumberField;
		private System.DateTime? identitityIssueDateField;
		private string identityIssuerField;
		private string identityDocumentPeriodField;
		private string purposeOfComingField;
		public VisaTypes? VisaTypes
		{
			get
			{
				return this.visaTypesField;
			}
			set
			{
				this.visaTypesField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool VisaTypesSpecified
		{
			get
			{
				return VisaTypes.HasValue;
			}
		}
		public string IdentityDocumentSeries
		{
			get
			{
				return this.identityDocumentSeriesField;
			}
			set
			{
				this.identityDocumentSeriesField = value;
			}
		}
		public string IdentityNumber
		{
			get
			{
				return this.identityNumberField;
			}
			set
			{
				this.identityNumberField = value;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? IdentitityIssueDate
		{
			get
			{
				return this.identitityIssueDateField;
			}
			set
			{
				this.identitityIssueDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool IdentitityIssueDateSpecified
		{
			get
			{
				return IdentitityIssueDate.HasValue;
			}
		}
		public string IdentityIssuer
		{
			get
			{
				return this.identityIssuerField;
			}
			set
			{
				this.identityIssuerField = value;
			}
		}
		public string IdentityDocumentPeriod
		{
			get
			{
				return this.identityDocumentPeriodField;
			}
			set
			{
				this.identityDocumentPeriodField = value;
			}
		}
		public string PurposeOfComing
		{
			get
			{
				return this.purposeOfComingField;
			}
			set
			{
				this.purposeOfComingField = value;
			}
		}
	}
	
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3027")]
	public partial class MaintenanceProvidedBy
	{
		private string nameField;
		private ForeignCitizenNames foreignCitizenNamesField;
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}
		public ForeignCitizenNames ForeignCitizenNames
		{
			get
			{
				return this.foreignCitizenNamesField;
			}
			set
			{
				this.foreignCitizenNamesField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3028")]
	public partial class ChildrenListedInForeignerPassport
	{
		private ForeignCitizenNames foreignCitizenNamesField;
		public ForeignCitizenNames ForeignCitizenNames
		{
			get
			{
				return this.foreignCitizenNamesField;
			}
			set
			{
				this.foreignCitizenNamesField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3029")]
	public partial class FormerResidenceOfTheForeigner
	{
		private System.DateTime? fromDateField;
		private System.DateTime? toDateField;
		private PersonAddress presentAddressField;
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? FromDate
		{
			get
			{
				return this.fromDateField;
			}
			set
			{
				this.fromDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool FromDateSpecified
		{
			get
			{
				return FromDate.HasValue;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? ToDate
		{
			get
			{
				return this.toDateField;
			}
			set
			{
				this.toDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool ToDateSpecified
		{
			get
			{
				return ToDate.HasValue;
			}
		}
		public PersonAddress PresentAddress
		{
			get
			{
				return this.presentAddressField;
			}
			set
			{
				this.presentAddressField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3036")]
	public partial class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensData
	{
		private ForeignIdentityBasicData foreignIdentityBasicDataField;
		private string stayInBulgariaField;
		private TravelDocument travelDocumentField;
		private Citizenship otherCitizenshipField;
		private PersonAddress addressField;
		private string serviceInformationField;
		private string serviceCodeField;
		public ForeignIdentityBasicData ForeignIdentityBasicData
		{
			get
			{
				return this.foreignIdentityBasicDataField;
			}
			set
			{
				this.foreignIdentityBasicDataField = value;
			}
		}
		public string StayInBulgaria
		{
			get
			{
				return this.stayInBulgariaField;
			}
			set
			{
				this.stayInBulgariaField = value;
			}
		}
		public TravelDocument TravelDocument
		{
			get
			{
				return this.travelDocumentField;
			}
			set
			{
				this.travelDocumentField = value;
			}
		}
		public Citizenship OtherCitizenship
		{
			get
			{
				return this.otherCitizenshipField;
			}
			set
			{
				this.otherCitizenshipField = value;
			}
		}
		public PersonAddress Address
		{
			get
			{
				return this.addressField;
			}
			set
			{
				this.addressField = value;
			}
		}
		public string ServiceInformation
		{
			get
			{
				return this.serviceInformationField;
			}
			set
			{
				this.serviceInformationField = value;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
		public string ServiceCode
		{
			get
			{
				return this.serviceCodeField;
			}
			set
			{
				this.serviceCodeField = value;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3046")]
	public partial class CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDOfficial
	{
		private object itemField;
        private string positionField;
        [System.Xml.Serialization.XmlElementAttribute("Position", typeof(string))]
        public string Position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("ForeignCitizenNames", typeof(ForeignCitizenNames))]
		[System.Xml.Serialization.XmlElementAttribute("PersonNames", typeof(PersonNames))]
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
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3007")]
	public partial class IdentityDocumentData
	{
		private string identityNumberField;
		private System.DateTime? identitityIssueDateField;
		private System.DateTime? identitityExpireDateField;
		private string identityIssuerField;
		private IdentityDocumentType? identityDocumentTypeField;
		private string identityDocumentStatusField;
		public string IdentityNumber
		{
			get
			{
				return this.identityNumberField;
			}
			set
			{
				this.identityNumberField = value;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? IdentitityIssueDate
		{
			get
			{
				return this.identitityIssueDateField;
			}
			set
			{
				this.identitityIssueDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool IdentitityIssueDateSpecified
		{
			get
			{
				return IdentitityIssueDate.HasValue;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? IdentitityExpireDate
		{
			get
			{
				return this.identitityExpireDateField;
			}
			set
			{
				this.identitityExpireDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool IdentitityExpireDateSpecified
		{
			get
			{
				return IdentitityExpireDate.HasValue;
			}
		}
		public string IdentityIssuer
		{
			get
			{
				return this.identityIssuerField;
			}
			set
			{
				this.identityIssuerField = value;
			}
		}
		public IdentityDocumentType? IdentityDocumentType
		{
			get
			{
				return this.identityDocumentTypeField;
			}
			set
			{
				this.identityDocumentTypeField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool IdentityDocumentTypeSpecified
		{
			get
			{
				return IdentityDocumentType.HasValue;
			}
		}
		public string IdentityDocumentStatus
		{
			get
			{
				return this.identityDocumentStatusField;
			}
			set
			{
				this.identityDocumentStatusField = value;
			}
		}
	}
}
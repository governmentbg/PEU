using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.PBZN.Documents.Domain.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3106")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3106", IsNullable = false)]
	public partial class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesData
	{
		private object itemField;
		private string workPhoneField;
		[System.Xml.Serialization.XmlElementAttribute("ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData", typeof(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData))]
		[System.Xml.Serialization.XmlElementAttribute("ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData", typeof(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData))]
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
		public string WorkPhone
		{
			get
			{
				return this.workPhoneField;
			}
			set
			{
				this.workPhoneField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3110")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3110", IsNullable = false)]
	public partial class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData
	{
		private EntityAddress entityManagementAddressField;
		private EntityAddress correspondingAddressField;
		private string declaredScopeOfCertificationField;
		private List<CertifiedPersonel> availableCertifiedPersonnelField;
		public EntityAddress EntityManagementAddress
		{
			get
			{
				return this.entityManagementAddressField;
			}
			set
			{
				this.entityManagementAddressField = value;
			}
		}
		public EntityAddress CorrespondingAddress
		{
			get
			{
				return this.correspondingAddressField;
			}
			set
			{
				this.correspondingAddressField = value;
			}
		}
		public string DeclaredScopeOfCertification
		{
			get
			{
				return this.declaredScopeOfCertificationField;
			}
			set
			{
				this.declaredScopeOfCertificationField = value;
			}
		}
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public List<CertifiedPersonel> AvailableCertifiedPersonnel
		{
			get
			{
				return this.availableCertifiedPersonnelField;
			}
			set
			{
				this.availableCertifiedPersonnelField = value;
			}
		}
	}
	
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3111")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3111", IsNullable = false)]
	public partial class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData
	{
		private PersonAddress permanentAddressField;
		private PersonAddress currentAddressField;
		private CertificateType certificateTypeField;
		private string certificateNumberField;
		private string diplomaNumberField;
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
		public PersonAddress CurrentAddress
		{
			get
			{
				return this.currentAddressField;
			}
			set
			{
				this.currentAddressField = value;
			}
		}
		public CertificateType CertificateType
		{
			get
			{
				return this.certificateTypeField;
			}
			set
			{
				this.certificateTypeField = value;
			}
		}
		public string CertificateNumber
		{
			get
			{
				return this.certificateNumberField;
			}
			set
			{
				this.certificateNumberField = value;
			}
		}
		public string DiplomaNumber
		{
			get
			{
				return this.diplomaNumberField;
			}
			set
			{
				this.diplomaNumberField = value;
			}
		}
	}
	
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3111")]
	public partial class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataIssuingCertificate
	{
		private string certificateNumberField;
		public string CertificateNumber
		{
			get
			{
				return this.certificateNumberField;
			}
			set
			{
				this.certificateNumberField = value;
			}
		}
	}
	
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3112")]
	public partial class CertifiedPersonel
	{
		private string personFirstNameField;
		private string personLastNameField;
		private string certificateNumberField;
		public string PersonFirstName
		{
			get
			{
				return this.personFirstNameField;
			}
			set
			{
				this.personFirstNameField = value;
			}
		}
		public string PersonLastName
		{
			get
			{
				return this.personLastNameField;
			}
			set
			{
				this.personLastNameField = value;
			}
		}
		public string CertificateNumber
		{
			get
			{
				return this.certificateNumberField;
			}
			set
			{
				this.certificateNumberField = value;
			}
		}
	}


	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3124")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3124", IsNullable = false)]
	public partial class ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData
	{
		private EntityAddress entityManagementAddressField;
		private EntityAddress corespondingAddressField;
		private string phoneNumberField;
		private string documentCertifyingTheAccidentOccurredOrOtherInformationField;
		private DocumentMustServeTo documentMustServeToField;
		private bool? includeInformation107Field;
		private bool? includeInformation133Field;
		public EntityAddress EntityManagementAddress
		{
			get
			{
				return this.entityManagementAddressField;
			}
			set
			{
				this.entityManagementAddressField = value;
			}
		}
		public EntityAddress CorespondingAddress
		{
			get
			{
				return this.corespondingAddressField;
			}
			set
			{
				this.corespondingAddressField = value;
			}
		}
		public string PhoneNumber
		{
			get
			{
				return this.phoneNumberField;
			}
			set
			{
				this.phoneNumberField = value;
			}
		}
		public string DocumentCertifyingTheAccidentOccurredOrOtherInformation
		{
			get
			{
				return this.documentCertifyingTheAccidentOccurredOrOtherInformationField;
			}
			set
			{
				this.documentCertifyingTheAccidentOccurredOrOtherInformationField = value;
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
		public bool? IncludeInformation107
		{
			get
			{
				return this.includeInformation107Field;
			}
			set
			{
				this.includeInformation107Field = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool IncludeInformation107Specified
		{
			get
			{
				return IncludeInformation107.HasValue;
			}
		}
		public bool? IncludeInformation133
		{
			get
			{
				return this.includeInformation133Field;
			}
			set
			{
				this.includeInformation133Field = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool IncludeInformation133Specified
		{
			get
			{
				return IncludeInformation133.HasValue;
			}
		}
	}

	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3143")]
	public partial class CertificateForAccidentOfficial
	{
		private object itemField;
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
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3145")]
	public partial class CertificateToWorkWithFluorinatedGreenhouseGassesOfficial
	{
		private object itemField;
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
}
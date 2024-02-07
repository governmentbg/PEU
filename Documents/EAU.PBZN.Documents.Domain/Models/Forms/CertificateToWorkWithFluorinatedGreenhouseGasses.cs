using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using System;
using System.Collections.Generic;

namespace EAU.PBZN.Documents.Domain.Models.Forms
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3145")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3145", IsNullable = false)]
	public partial class CertificateToWorkWithFluorinatedGreenhouseGasses : IDocumentForm
	{
		private DocumentTypeURI documentTypeURIField;
		private string documentTypeNameField;
		private DocumentURI documentURIField;
		private AISCaseURI aISCaseURIField;
		private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
		private ElectronicServiceApplicant electronicServiceApplicantField;
		private string certificateToWorkWithFluorinatedGreenhouseGassesHeaderField;
		private string certificateValidityField;
		private string certificateToWorkWithFluorinatedGreenhouseGassesGroundField;
		private string certificateToWorkWithFluorinatedGreenhouseGassesActivitiesField;
		private object itemField;
		private string certificateToWorkWithFluorinatedGreenhouseGassesPersonsGroundField;
		private Byte[] identificationPhotoField;
		private System.DateTime? documentReceiptOrSigningDateField;
		private string administrativeBodyNameField;
		private CertificateToWorkWithFluorinatedGreenhouseGassesOfficial officialField;
		private PoliceDepartment policeDepartmentField;
		private XMLDigitalSignature xMLDigitalSignatureField;
		public DocumentTypeURI DocumentTypeURI
		{
			get
			{
				return this.documentTypeURIField;
			}
			set
			{
				this.documentTypeURIField = value;
			}
		}
		public string DocumentTypeName
		{
			get
			{
				return this.documentTypeNameField;
			}
			set
			{
				this.documentTypeNameField = value;
			}
		}
		public DocumentURI DocumentURI
		{
			get
			{
				return this.documentURIField;
			}
			set
			{
				this.documentURIField = value;
			}
		}
		public AISCaseURI AISCaseURI
		{
			get
			{
				return this.aISCaseURIField;
			}
			set
			{
				this.aISCaseURIField = value;
			}
		}
		public ElectronicServiceProviderBasicData ElectronicServiceProviderBasicData
		{
			get
			{
				return this.electronicServiceProviderBasicDataField;
			}
			set
			{
				this.electronicServiceProviderBasicDataField = value;
			}
		}
		public ElectronicServiceApplicant ElectronicServiceApplicant
		{
			get
			{
				return this.electronicServiceApplicantField;
			}
			set
			{
				this.electronicServiceApplicantField = value;
			}
		}
		public string CertificateToWorkWithFluorinatedGreenhouseGassesHeader
		{
			get
			{
				return this.certificateToWorkWithFluorinatedGreenhouseGassesHeaderField;
			}
			set
			{
				this.certificateToWorkWithFluorinatedGreenhouseGassesHeaderField = value;
			}
		}
		public string CertificateValidity
		{
			get
			{
				return this.certificateValidityField;
			}
			set
			{
				this.certificateValidityField = value;
			}
		}
		public string CertificateToWorkWithFluorinatedGreenhouseGassesGround
		{
			get
			{
				return this.certificateToWorkWithFluorinatedGreenhouseGassesGroundField;
			}
			set
			{
				this.certificateToWorkWithFluorinatedGreenhouseGassesGroundField = value;
			}
		}
		public string CertificateToWorkWithFluorinatedGreenhouseGassesActivities
		{
			get
			{
				return this.certificateToWorkWithFluorinatedGreenhouseGassesActivitiesField;
			}
			set
			{
				this.certificateToWorkWithFluorinatedGreenhouseGassesActivitiesField = value;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute("CertificateToWorkWithFluorinatedGreenhouseGassesEntityData", typeof(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData))]
		[System.Xml.Serialization.XmlElementAttribute("CertificateToWorkWithFluorinatedGreenhouseGassesPersonData", typeof(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData))]
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
		public string CertificateToWorkWithFluorinatedGreenhouseGassesPersonsGround
		{
			get
			{
				return this.certificateToWorkWithFluorinatedGreenhouseGassesPersonsGroundField;
			}
			set
			{
				this.certificateToWorkWithFluorinatedGreenhouseGassesPersonsGroundField = value;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
		public Byte[] IdentificationPhoto
		{
			get
			{
				return this.identificationPhotoField;
			}
			set
			{
				this.identificationPhotoField = value;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? DocumentReceiptOrSigningDate
		{
			get
			{
				return this.documentReceiptOrSigningDateField;
			}
			set
			{
				this.documentReceiptOrSigningDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool DocumentReceiptOrSigningDateSpecified
		{
			get
			{
				return DocumentReceiptOrSigningDate.HasValue;
			}
		}
		public string AdministrativeBodyName
		{
			get
			{
				return this.administrativeBodyNameField;
			}
			set
			{
				this.administrativeBodyNameField = value;
			}
		}
		public CertificateToWorkWithFluorinatedGreenhouseGassesOfficial Official
		{
			get
			{
				return this.officialField;
			}
			set
			{
				this.officialField = value;
			}
		}
		public PoliceDepartment PoliceDepartment
		{
			get
			{
				return this.policeDepartmentField;
			}
			set
			{
				this.policeDepartmentField = value;
			}
		}
		public XMLDigitalSignature XMLDigitalSignature
		{
			get
			{
				return this.xMLDigitalSignatureField;
			}
			set
			{
				this.xMLDigitalSignatureField = value;
			}
		}
	}
}
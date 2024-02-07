using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Domain.Models.Forms
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3021")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3021", IsNullable = false)]
	public partial class ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgaria : IApplicationForm
	{
		private ElectronicAdministrativeServiceHeader electronicAdministrativeServiceHeaderField;
		private ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData applicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaDataField;
		private List<IdentityDocumentType> identificationDocumentsField;
		private PoliceDepartment issuingPoliceDepartmentField;
		private IdentificationPhotoAndSignature identificationPhotoAndSignatureField;
		private ElectronicAdministrativeServiceFooter electronicAdministrativeServiceFooterField;
		private bool? hasDocumentForDisabilitiesField;
		private List<Declaration> declarationsField;
		private List<AttachedDocument> attachedDocumentsField;
		private ServiceTermType? serviceTermTypeField;
		private ServiceApplicantReceiptData serviceApplicantReceiptDataField;
		public ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader
		{
			get
			{
				return this.electronicAdministrativeServiceHeaderField;
			}
			set
			{
				this.electronicAdministrativeServiceHeaderField = value;
			}
		}
		public ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData ApplicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaData
		{
			get
			{
				return this.applicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaDataField;
			}
			set
			{
				this.applicationForIssuanceOfIdentityDocumentsAndResidencePermitsOfForeignersInTheRepublicOfBulgariaDataField = value;
			}
		}
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
		public IdentificationPhotoAndSignature IdentificationPhotoAndSignature
		{
			get
			{
				return this.identificationPhotoAndSignatureField;
			}
			set
			{
				this.identificationPhotoAndSignatureField = value;
			}
		}
		public ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter
		{
			get
			{
				return this.electronicAdministrativeServiceFooterField;
			}
			set
			{
				this.electronicAdministrativeServiceFooterField = value;
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
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public List<Declaration> Declarations
		{
			get
			{
				return this.declarationsField;
			}
			set
			{
				this.declarationsField = value;
			}
		}
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public List<AttachedDocument> AttachedDocuments
		{
			get
			{
				return this.attachedDocumentsField;
			}
			set
			{
				this.attachedDocumentsField = value;
			}
		}
		public ServiceTermType? ServiceTermType
		{
			get
			{
				return this.serviceTermTypeField;
			}
			set
			{
				this.serviceTermTypeField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool ServiceTermTypeSpecified
		{
			get
			{
				return ServiceTermType.HasValue;
			}
		}
		public ServiceApplicantReceiptData ServiceApplicantReceiptData
		{
			get
			{
				return this.serviceApplicantReceiptDataField;
			}
			set
			{
				this.serviceApplicantReceiptDataField = value;
			}
		}
	}
}

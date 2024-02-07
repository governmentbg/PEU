using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Domain.Models.Forms
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3046")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3046", IsNullable = false)]
	public partial class CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLD : IDocumentForm
	{
		private DocumentTypeURI documentTypeURIField;
		private string documentTypeNameField;
		private DocumentURI documentURIField;
		private AISCaseURI aISCaseURIField;
		private System.DateTime? documentReceiptOrSigningDateField;
		private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
		private ElectronicServiceApplicant electronicServiceApplicantField;
		private string certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeaderField;
		private string certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDDataField;
		private DocumentMustServeTo documentMustServeToField;
		private PoliceDepartment issuingPoliceDepartmentField;
		private string administrativeBodyNameField;
		private CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDOfficial officialField;
		private System.DateTime? reportDateField;
		private List<IdentityDocumentData> identityDocumentsField;
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
		public string CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeader
		{
			get
			{
				return this.certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeaderField;
			}
			set
			{
				this.certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeaderField = value;
			}
		}
		public string CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDData
		{
			get
			{
				return this.certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDDataField;
			}
			set
			{
				this.certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDDataField = value;
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
		public CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDOfficial Official
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
		public System.DateTime? ReportDate
		{
			get
			{
				return this.reportDateField;
			}
			set
			{
				this.reportDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool ReportDateSpecified
		{
			get
			{
				return ReportDate.HasValue;
			}
		}
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public List<IdentityDocumentData> IdentityDocuments
		{
			get
			{
				return this.identityDocumentsField;
			}
			set
			{
				this.identityDocumentsField = value;
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
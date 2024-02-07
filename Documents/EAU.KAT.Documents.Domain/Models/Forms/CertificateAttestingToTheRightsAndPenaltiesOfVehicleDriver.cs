using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;

namespace EAU.KAT.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Удостоверение за правата и наложените наказания на водач на МПС
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3125")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3125", IsNullable = false)]
    public partial class CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver : IDocumentForm
    {
		private DocumentTypeURI documentTypeURIField;
		private string documentTypeNameField;
		private DocumentURI documentURIField;
		private AISCaseURI aISCaseURIField;
		private System.DateTime? documentReceiptOrSigningDateField;
		private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
		private ElectronicServiceApplicant electronicServiceApplicantField;
		private string certificateAttestingToTheRightsAndPenaltiesOfVehicleDriverHeaderField;
		private string certificateDataField;
		private string certificateData1Field;
		private string certificateNumberField;
		private ANDCertificateReason? aNDCertificateReasonField;
		private PoliceDepartment issuingPoliceDepartmentField;
		private string administrativeBodyNameField;
		private CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverOfficial officialField;
		private System.DateTime? reportDateField;
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
		public string CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverHeader
		{
			get
			{
				return this.certificateAttestingToTheRightsAndPenaltiesOfVehicleDriverHeaderField;
			}
			set
			{
				this.certificateAttestingToTheRightsAndPenaltiesOfVehicleDriverHeaderField = value;
			}
		}
		public string CertificateData
		{
			get
			{
				return this.certificateDataField;
			}
			set
			{
				this.certificateDataField = value;
			}
		}
		public string CertificateData1
		{
			get
			{
				return this.certificateData1Field;
			}
			set
			{
				this.certificateData1Field = value;
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
		public ANDCertificateReason? ANDCertificateReason
		{
			get
			{
				return this.aNDCertificateReasonField;
			}
			set
			{
				this.aNDCertificateReasonField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool ANDCertificateReasonSpecified
		{
			get
			{
				return ANDCertificateReason.HasValue;
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
		public CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverOfficial Official
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

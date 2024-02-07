using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;

namespace EAU.KAT.Documents.Domain.Models.Forms
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3340")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3340", IsNullable = false)]
	public partial class ReportForVehicle : IDocumentForm
	{
		private DocumentTypeURI documentTypeURIField;
		private string documentTypeNameField;
		private DocumentURI documentURIField;
		private AISCaseURI aISCaseURIField;
		private ElectronicServiceApplicant electronicServiceApplicantField;
		private ReportForVehicleRPSSVehicleData rPSSVehicleDataField;
		private ReportForVehicleEUCARISData eUCARISDataField;
		private ReportForVehicleOwners ownersField;
		private ReportForVehicleGuaranteeFund guaranteeFundField;
		private ReportForVehiclePeriodicTechnicalCheck periodicTechnicalCheckField;
		private System.DateTime? documentReceiptOrSigningDateField;
		private string administrativeBodyNameField;
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
		public ReportForVehicleRPSSVehicleData RPSSVehicleData
		{
			get
			{
				return this.rPSSVehicleDataField;
			}
			set
			{
				this.rPSSVehicleDataField = value;
			}
		}
		public ReportForVehicleEUCARISData EUCARISData
		{
			get
			{
				return this.eUCARISDataField;
			}
			set
			{
				this.eUCARISDataField = value;
			}
		}
		public ReportForVehicleOwners Owners
		{
			get
			{
				return this.ownersField;
			}
			set
			{
				this.ownersField = value;
			}
		}
		public ReportForVehicleGuaranteeFund GuaranteeFund
		{
			get
			{
				return this.guaranteeFundField;
			}
			set
			{
				this.guaranteeFundField = value;
			}
		}
		public ReportForVehiclePeriodicTechnicalCheck PeriodicTechnicalCheck
		{
			get
			{
				return this.periodicTechnicalCheckField;
			}
			set
			{
				this.periodicTechnicalCheckField = value;
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

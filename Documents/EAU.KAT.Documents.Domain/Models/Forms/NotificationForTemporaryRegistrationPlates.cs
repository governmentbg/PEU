using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;

namespace EAU.KAT.Documents.Domain.Models.Forms
{
	/// <summary>
	/// Уведомление за определени временни табели
	/// </summary>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3335")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3335", IsNullable = false)]
	public partial class NotificationForTemporaryRegistrationPlates : IDocumentForm
	{
		private DocumentTypeURI documentTypeURIField;
		private string documentTypeNameField;
		private DocumentURI documentURIField;
		private AISCaseURI aISCaseURIField;
		private string administrativeBodyNameField;
		private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
		private ElectronicServiceApplicant electronicServiceApplicantField;
		private EntityAddress entityManagementAddressField;
		private int? countOfSetsOfTemporaryPlatesField;
		private string countOfSetsOfTemporaryPlatesTextField;
		private string registrationNumbersForEachSetField;
		private System.DateTime? documentReceiptOrSigningDateField;
		private NotificationForTemporaryRegistrationPlatesOfficial officialField;
		private XMLDigitalSignature xMLDigitalSignatureField;
		private PoliceDepartment issuingPoliceDepartmentField;
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
		public int? CountOfSetsOfTemporaryPlates
		{
			get
			{
				return this.countOfSetsOfTemporaryPlatesField;
			}
			set
			{
				this.countOfSetsOfTemporaryPlatesField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool CountOfSetsOfTemporaryPlatesSpecified
		{
			get
			{
				return CountOfSetsOfTemporaryPlates.HasValue;
			}
		}
		public string CountOfSetsOfTemporaryPlatesText
		{
			get
			{
				return this.countOfSetsOfTemporaryPlatesTextField;
			}
			set
			{
				this.countOfSetsOfTemporaryPlatesTextField = value;
			}
		}
		public string RegistrationNumbersForEachSet
		{
			get
			{
				return this.registrationNumbersForEachSetField;
			}
			set
			{
				this.registrationNumbersForEachSetField = value;
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
		public NotificationForTemporaryRegistrationPlatesOfficial Official
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
}
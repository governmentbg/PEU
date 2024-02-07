using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using System.Collections.Generic;

namespace EAU.BDS.Documents.Domain.Models.Forms
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3001")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3001", IsNullable = false)]
	public partial class RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassport : IApplicationForm
	{
		private ElectronicAdministrativeServiceHeader electronicAdministrativeServiceHeaderField;
		private List<IdentityDocumentType> identificationDocumentsField;
		private ServiceTermType? serviceTermTypeField;
		private ServiceApplicantReceiptData serviceApplicantReceiptDataField;
		private ElectronicAdministrativeServiceFooter electronicAdministrativeServiceFooterField;
		private List<Declaration> declarationsField;
		private List<AttachedDocument> attachedDocumentsField;
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
		[System.Xml.Serialization.XmlArrayItemAttribute("IdentificationDocument", IsNullable = false)]
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
	}
}

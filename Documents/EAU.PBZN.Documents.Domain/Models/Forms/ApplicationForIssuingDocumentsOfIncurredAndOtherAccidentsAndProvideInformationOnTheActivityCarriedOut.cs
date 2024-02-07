using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using System.Collections.Generic;

namespace EAU.PBZN.Documents.Domain.Models.Forms
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3123")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3123", IsNullable = false)]
	public partial class ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut : IApplicationForm
	{
		private ElectronicAdministrativeServiceHeader electronicAdministrativeServiceHeaderField;
		private ServiceApplicantReceiptData serviceApplicantReceiptDataField;
		private PoliceDepartment issuingPoliceDepartmentField;
		private ServiceTermType? serviceTermTypeField;
		private ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData applicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataField;
		private List<Declaration> declarationsField;
		private List<AttachedDocument> attachedDocumentsField;
		private ElectronicAdministrativeServiceFooter electronicAdministrativeServiceFooterField;
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
		public ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData
		{
			get
			{
				return this.applicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataField;
			}
			set
			{
				this.applicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataField = value;
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
	}
}
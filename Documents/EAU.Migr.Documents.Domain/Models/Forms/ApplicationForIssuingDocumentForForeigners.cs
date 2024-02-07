using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace EAU.Migr.Documents.Domain.Models.Forms
{
    [GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[SerializableAttribute()]
	[DebuggerStepThroughAttribute()]
	[DesignerCategoryAttribute("code")]
	[XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3115")]
	[XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3115", IsNullable = false)]
	public partial class ApplicationForIssuingDocumentForForeigners : IApplicationForm
	{
		private ElectronicAdministrativeServiceHeader electronicAdministrativeServiceHeaderField;
		private ServiceTermType? serviceTermTypeField;
		private ServiceApplicantReceiptData serviceApplicantReceiptDataField;
		private ApplicationForIssuingDocumentForForeignersData applicationForIssuingDocumentForForeignersDataField;
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

		[XmlIgnoreAttribute()]
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

		public ApplicationForIssuingDocumentForForeignersData ApplicationForIssuingDocumentForForeignersData
		{
			get
			{
				return this.applicationForIssuingDocumentForForeignersDataField;
			}
			set
			{
				this.applicationForIssuingDocumentForForeignersDataField = value;
			}
		}

		[XmlArrayItemAttribute(IsNullable = false)]
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

		[XmlArrayItemAttribute(IsNullable = false)]
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
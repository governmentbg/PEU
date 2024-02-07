using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Domain.Models.Forms
{
	/// <summary>
	/// Заявление за  предоставяне на временни табели с регистрационен номер от лица – търговци, по смисъла на Търговския закон
	/// </summary>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3324")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3324", IsNullable = false)]
	public partial class ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchants : IApplicationForm
	{
		private ElectronicAdministrativeServiceHeader electronicAdministrativeServiceHeaderField;
		private ServiceTermType? serviceTermTypeField;
		private ServiceApplicantReceiptData serviceApplicantReceiptDataField;
		private ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData applicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataField;
		private MerchantData merchantDataField;
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
		public ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData
		{
			get
			{
				return this.applicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataField;
			}
			set
			{
				this.applicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataField = value;
			}
		}
		public MerchantData MerchantData
		{
			get
			{
				return this.merchantDataField;
			}
			set
			{
				this.merchantDataField = value;
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

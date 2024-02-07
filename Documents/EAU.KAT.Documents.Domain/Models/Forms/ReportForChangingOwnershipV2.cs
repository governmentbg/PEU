using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Domain.Models.Forms
{
	/// <summary>
	/// Справка за промяна на собственост на ПС V2
	/// </summary>
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3341")]
	[System.Xml.Serialization.XmlRootAttribute("ReportForChangingOwnershipV2", Namespace = "http://ereg.egov.bg/segment/R-3341", IsNullable = false)]
	public partial class ReportForChangingOwnershipV2 : IDocumentForm
	{
		private DocumentTypeURI documentTypeURIField;
		private string documentTypeNameField;
		private DocumentURI documentURIField;
		private AISCaseURI aISCaseURIField;
		private ElectronicServiceApplicant electronicServiceApplicantField;
		private List<ReportForChangingOwnershipV2VehicleData> vehicleDataField;
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
		[System.Xml.Serialization.XmlElementAttribute("VehicleData")]
		public List<ReportForChangingOwnershipV2VehicleData> VehicleData
		{
			get
			{
				return this.vehicleDataField;
			}
			set
			{
				this.vehicleDataField = value;
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
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3341")]
	public partial class ReportForChangingOwnershipV2VehicleData
	{
		private VehicleRegistrationData vehicleRegistrationDataField;
		private ReportForChangingOwnershipV2VehicleDataOldOwners oldOwnersField;
		private ReportForChangingOwnershipV2VehicleDataNewOwners newOwnersField;
		private List<Status> localTaxesField;
		private ReportForChangingOwnershipV2VehicleDataPeriodicTechnicalCheck periodicTechnicalCheckField;
		private ReportForChangingOwnershipV2VehicleDataGuaranteeFund guaranteeFundField;
		public VehicleRegistrationData VehicleRegistrationData
		{
			get
			{
				return this.vehicleRegistrationDataField;
			}
			set
			{
				this.vehicleRegistrationDataField = value;
			}
		}
		public ReportForChangingOwnershipV2VehicleDataOldOwners OldOwners
		{
			get
			{
				return this.oldOwnersField;
			}
			set
			{
				this.oldOwnersField = value;
			}
		}
		public ReportForChangingOwnershipV2VehicleDataNewOwners NewOwners
		{
			get
			{
				return this.newOwnersField;
			}
			set
			{
				this.newOwnersField = value;
			}
		}
		[System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
		public List<Status> LocalTaxes
		{
			get
			{
				return this.localTaxesField;
			}
			set
			{
				this.localTaxesField = value;
			}
		}
		public ReportForChangingOwnershipV2VehicleDataPeriodicTechnicalCheck PeriodicTechnicalCheck
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
		public ReportForChangingOwnershipV2VehicleDataGuaranteeFund GuaranteeFund
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
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3341")]
	public partial class ReportForChangingOwnershipV2VehicleDataOldOwners
	{
		private List<Object> itemsField;
		[System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
		[System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
		public List<Object> Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3341")]
	public partial class ReportForChangingOwnershipV2VehicleDataNewOwners
	{
		private List<Object> itemsField;
		[System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
		[System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
		public List<Object> Items
		{
			get
			{
				return this.itemsField;
			}
			set
			{
				this.itemsField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3341")]
	public partial class ReportForChangingOwnershipV2VehicleDataPeriodicTechnicalCheck
	{
		private System.DateTime? nextInspectionDateField;
		private List<Status> statusField;
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? NextInspectionDate
		{
			get
			{
				return this.nextInspectionDateField;
			}
			set
			{
				this.nextInspectionDateField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool NextInspectionDateSpecified
		{
			get
			{
				return NextInspectionDate.HasValue;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute("Status")]
		public List<Status> Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}
	}
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3341")]
	public partial class ReportForChangingOwnershipV2VehicleDataGuaranteeFund
	{
		private System.DateTime? policyValidToField;
		private List<Status> statusField;
		[System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
		public System.DateTime? PolicyValidTo
		{
			get
			{
				return this.policyValidToField;
			}
			set
			{
				this.policyValidToField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool PolicyValidToSpecified
		{
			get
			{
				return PolicyValidTo.HasValue;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute("Status")]
		public List<Status> Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}
	}
}

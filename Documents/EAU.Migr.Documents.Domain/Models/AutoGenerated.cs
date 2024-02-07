using EAU.Documents.Domain.Models;

namespace EAU.Migr.Documents.Domain.Models
{
	[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
	[System.SerializableAttribute()]
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3116")]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3116", IsNullable = false)]
	public partial class ApplicationForIssuingDocumentForForeignersData
	{
		private string birthDateField;
		private PersonAddress addressField;
		private string certificateForField;
		private Citizenship citizenshipField;
		private DocumentMustServeTo documentMustServeToField;

		public string BirthDate
		{
			get
			{
				return this.birthDateField;
			}
			set
			{
				this.birthDateField = value;
			}
		}

		public PersonAddress Address
		{
			get
			{
				return this.addressField;
			}
			set
			{
				this.addressField = value;
			}
		}

		public string CertificateFor
		{
			get
			{
				return this.certificateForField;
			}
			set
			{
				this.certificateForField = value;
			}
		}

		public Citizenship Citizenship
		{
			get
			{
				return this.citizenshipField;
			}
			set
			{
				this.citizenshipField = value;
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
	}
}
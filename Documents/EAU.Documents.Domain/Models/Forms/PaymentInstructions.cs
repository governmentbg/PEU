namespace EAU.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Указания за заплащане
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3103")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3103", IsNullable = false)]
    public partial class PaymentInstructions : IDocumentForm
    {
		private DocumentTypeURI documentTypeURIField;
		private string documentTypeNameField;
		private DocumentURI documentURIField;
		private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
		private ElectronicServiceApplicant electronicServiceApplicantField;
		private AISCaseURI aISCaseURIField;
		private string bankNameField;
		private string bICField;
		private string iBANField;
		private string paymentReasonField;
		private double? amountField;
		private string amountCurrencyField;
		private string deadlineForPaymentField;
		private string deadlineMessageField;
		private XMLDigitalSignature signatureField;
		private string paymentInstructionsHeaderField;
		private System.DateTime? documentReceiptOrSigningDateField;
		private string pepCinField;
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
		public string BankName
		{
			get
			{
				return this.bankNameField;
			}
			set
			{
				this.bankNameField = value;
			}
		}
		public string BIC
		{
			get
			{
				return this.bICField;
			}
			set
			{
				this.bICField = value;
			}
		}
		public string IBAN
		{
			get
			{
				return this.iBANField;
			}
			set
			{
				this.iBANField = value;
			}
		}
		public string PaymentReason
		{
			get
			{
				return this.paymentReasonField;
			}
			set
			{
				this.paymentReasonField = value;
			}
		}
		public double? Amount
		{
			get
			{
				return this.amountField;
			}
			set
			{
				this.amountField = value;
			}
		}
		[System.Xml.Serialization.XmlIgnoreAttribute()]
		public bool AmountSpecified
		{
			get
			{
				return Amount.HasValue;
			}
		}
		public string AmountCurrency
		{
			get
			{
				return this.amountCurrencyField;
			}
			set
			{
				this.amountCurrencyField = value;
			}
		}
		[System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
		public string DeadlineForPayment
		{
			get
			{
				return this.deadlineForPaymentField;
			}
			set
			{
				this.deadlineForPaymentField = value;
			}
		}
		public string DeadlineMessage
		{
			get
			{
				return this.deadlineMessageField;
			}
			set
			{
				this.deadlineMessageField = value;
			}
		}
		public XMLDigitalSignature Signature
		{
			get
			{
				return this.signatureField;
			}
			set
			{
				this.signatureField = value;
			}
		}
		public string PaymentInstructionsHeader
		{
			get
			{
				return this.paymentInstructionsHeaderField;
			}
			set
			{
				this.paymentInstructionsHeaderField = value;
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
		public string PepCin
		{
			get
			{
				return this.pepCinField;
			}
			set
			{
				this.pepCinField = value;
			}
		}
	}
}

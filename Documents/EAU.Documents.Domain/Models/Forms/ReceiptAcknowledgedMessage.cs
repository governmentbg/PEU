namespace EAU.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Потвърждаване на получаването
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000019")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000019", IsNullable = false)]
    public partial class ReceiptAcknowledgedMessage : IDocumentForm
    {

        private ElectronicServiceProviderBasicData electronicServiceProviderField;

        private DocumentElectronicTransportType? transportTypeField;
                
        private DocumentURI documentURIField;

        private System.DateTime? receiptTimeField;

        private ReceiptAcknowledgedMessageRegisteredBy registeredByField;

        private string caseAccessIdentifierField;

        private ElectronicServiceApplicant applicantField;

        private DocumentTypeURI documentTypeURIField;

        private string documentTypeNameField;

        private XMLDigitalSignature signatureField;

        /// <remarks/>
        public ElectronicServiceProviderBasicData ElectronicServiceProvider
        {
            get
            {
                return this.electronicServiceProviderField;
            }
            set
            {
                this.electronicServiceProviderField = value;
            }
        }

        /// <remarks/>
        public DocumentElectronicTransportType? TransportType
        {
            get
            {
                return this.transportTypeField;
            }
            set
            {
                this.transportTypeField = value;
            }
        }

        /// <remarks/>
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

        /// <remarks/>
        public System.DateTime? ReceiptTime
        {
            get
            {
                return this.receiptTimeField;
            }
            set
            {
                this.receiptTimeField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReceiptTimeSpecified
        {
            get
            {
                return ReceiptTime.HasValue;
            }
        }

        /// <remarks/>
        public ReceiptAcknowledgedMessageRegisteredBy RegisteredBy
        {
            get
            {
                return this.registeredByField;
            }
            set
            {
                this.registeredByField = value;
            }
        }

        /// <remarks/>
        public string CaseAccessIdentifier
        {
            get
            {
                return this.caseAccessIdentifierField;
            }
            set
            {
                this.caseAccessIdentifierField = value;
            }
        }

        /// <remarks/>
        public ElectronicServiceApplicant Applicant
        {
            get
            {
                return this.applicantField;
            }
            set
            {
                this.applicantField = value;
            }
        }

        /// <remarks/>
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

        /// <remarks/>
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

        /// <remarks/>
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
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000019")]
    public partial class ReceiptAcknowledgedMessageRegisteredBy 
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("AISURI", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("Officer", typeof(ReceiptAcknowledgedMessageRegisteredByOfficer))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/0009-000019")]
    public partial class ReceiptAcknowledgedMessageRegisteredByOfficer 
    {

        private AISUserNames personNamesField;

        private string aISUserIdentifierField;

        /// <remarks/>
        public AISUserNames PersonNames
        {
            get
            {
                return this.personNamesField;
            }
            set
            {
                this.personNamesField = value;
            }
        }

        /// <remarks/>
        public string AISUserIdentifier
        {
            get
            {
                return this.aISUserIdentifierField;
            }
            set
            {
                this.aISUserIdentifierField = value;
            }
        }
    }
}

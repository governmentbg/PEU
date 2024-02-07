namespace EAU.Documents.Domain.Models.Forms
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000017")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000017", IsNullable = false)]
    public partial class ReceiptNotAcknowledgedMessage : IDocumentForm
    {

        private DocumentURI messageURIField;

        private ElectronicServiceProviderBasicData electronicServiceProviderField;

        private DocumentElectronicTransportType? transportTypeField;
        
        private ElectronicDocumentDiscrepancyType[] discrepanciesField;

        private ElectronicServiceApplicant applicantField;

        private DocumentTypeURI documentTypeURIField;

        private string documentTypeNameField;

        private System.DateTime? messageCreationTimeField;
                
        private XMLDigitalSignature signatureField;

        /// <remarks/>
        public DocumentURI MessageURI
        {
            get
            {
                return this.messageURIField;
            }
            set
            {
                this.messageURIField = value;
            }
        }

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
        [System.Xml.Serialization.XmlArrayItemAttribute("Discrepancy", IsNullable = false)]
        public ElectronicDocumentDiscrepancyType[] Discrepancies
        {
            get
            {
                return this.discrepanciesField;
            }
            set
            {
                this.discrepanciesField = value;
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
        public System.DateTime? MessageCreationTime
        {
            get
            {
                return this.messageCreationTimeField;
            }
            set
            {
                this.messageCreationTimeField = value;
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MessageCreationTimeSpecified
        {
            get
            {
                return MessageCreationTime.HasValue;
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
}

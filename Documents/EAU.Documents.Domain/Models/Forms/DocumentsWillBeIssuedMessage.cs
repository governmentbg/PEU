namespace EAU.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Уведомление за изпълнена ЕАУ
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3102")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3102", IsNullable = false)]
    public partial class DocumentsWillBeIssuedMessage : IDocumentForm
    {
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private DocumentURI documentURIField;
        private System.DateTime? documentReceiptOrSigningDateField;
        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private AISCaseURI aISCaseURIField;
        private string identityDocumentsWillBeIssuedMessageField;
        private PoliceDepartment policeDepartmentField;
        private System.DateTime? deliveryDateField;
        private string administrativeBodyNameField;
        private XMLDigitalSignature signatureField;
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
        public string IdentityDocumentsWillBeIssuedMessage
        {
            get
            {
                return this.identityDocumentsWillBeIssuedMessageField;
            }
            set
            {
                this.identityDocumentsWillBeIssuedMessageField = value;
            }
        }
        public PoliceDepartment PoliceDepartment
        {
            get
            {
                return this.policeDepartmentField;
            }
            set
            {
                this.policeDepartmentField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? DeliveryDate
        {
            get
            {
                return this.deliveryDateField;
            }
            set
            {
                this.deliveryDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeliveryDateSpecified
        {
            get
            {
                return DeliveryDate.HasValue;
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

namespace EAU.Documents.Domain.Models.Forms
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3138")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3138", IsNullable = false)]
    public partial class ActionsTakenMessage : IDocumentForm
    {
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private DocumentURI documentURIField;
        private AISCaseURI aISCaseURIField;
        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private string actionsTakenMessageHeaderField;
        private string actionsTakenMessageMessageField;
        private PoliceDepartment policeDepartmentField;
        private string administrativeBodyNameField;
        private System.DateTime? documentReceiptOrSigningDateField;
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
        public string ActionsTakenMessageHeader
        {
            get
            {
                return this.actionsTakenMessageHeaderField;
            }
            set
            {
                this.actionsTakenMessageHeaderField = value;
            }
        }
        public string ActionsTakenMessageMessage
        {
            get
            {
                return this.actionsTakenMessageMessageField;
            }
            set
            {
                this.actionsTakenMessageMessageField = value;
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

using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;

namespace EAU.BDS.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Покана за съставяне на АУАН по чл. 40, ал.2 от ЗАНН.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3254")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3254", IsNullable = false)]
    public partial class InvitationToDrawUpAUAN : IDocumentForm
    {
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private DocumentURI documentURIField;
        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
        private AISCaseURI aISCaseURIField;
        private string titleField;
        private string contentField;
        private string administrativeBodyNameField;
        private System.DateTime? documentReceiptOrSigningDateField;
        private InvitationToDrawUpAUANOfficial officialField;
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
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
        public string Content
        {
            get
            {
                return this.contentField;
            }
            set
            {
                this.contentField = value;
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
        public InvitationToDrawUpAUANOfficial Official
        {
            get
            {
                return this.officialField;
            }
            set
            {
                this.officialField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3254")]
    public partial class InvitationToDrawUpAUANOfficial
    {
        private object itemField;
        private string positionField;
        [System.Xml.Serialization.XmlElementAttribute("Position", typeof(string))]
        public string Position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("ForeignCitizenNames", typeof(ForeignCitizenNames))]
        [System.Xml.Serialization.XmlElementAttribute("PersonNames", typeof(PersonNames))]
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
}

using System.Collections.Generic;
namespace EAU.Documents.Domain.Models
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000021")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000021", IsNullable = false)]
    public partial class TransferContainer 
    {
        
        private DocumentURI documentURIField;

        private string shortTransferDescriptionField;

        private string expandedTransferDescriptionField;

        private List<TransferredDocument> transferredDocumentsField;

        private XMLDigitalSignature signatureField;

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
        public string ShortTransferDescription
        {
            get
            {
                return this.shortTransferDescriptionField;
            }
            set
            {
                this.shortTransferDescriptionField = value;
            }
        }

        /// <remarks/>
        public string ExpandedTransferDescription
        {
            get
            {
                return this.expandedTransferDescriptionField;
            }
            set
            {
                this.expandedTransferDescriptionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Document", IsNullable = false)]
        public List<TransferredDocument> TransferredDocuments
        {
            get
            {
                return this.transferredDocumentsField;
            }
            set
            {
                this.transferredDocumentsField = value;
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

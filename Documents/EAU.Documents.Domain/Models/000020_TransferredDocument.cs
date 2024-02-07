using System.IO;

namespace EAU.Documents.Domain.Models
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.42")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/0009-000020")]
    public partial class TransferredDocument 
    {

        private DocumentURI documentURIField;

        private string shortTransferDescriptionField;

        private string expandedTransferDescriptionField;

        private string fileTypeField;

        private byte[] fileContentField;

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
        public string FileType
        {
            get
            {
                return this.fileTypeField;
            }
            set
            {
                this.fileTypeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "base64Binary")]
        public byte[] FileContent
        {
            get
            {
                return this.fileContentField;
            }
            set
            {
                this.fileContentField = value;
            }
        }

        #region Public Methods

        public Stream GetStreamContent()
        {
            return new MemoryStream(this.FileContent);
        }

        #endregion
    }
}

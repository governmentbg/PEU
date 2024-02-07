using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents.Domain.Models.Forms
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(TypeName = "OutstandingConditionsForWithdrawServiceMessage", Namespace = "http://ereg.egov.bg/segment/R-3119")]
    [System.Xml.Serialization.XmlRootAttribute("OutstandingConditionsForWithdrawServiceMessage", Namespace = "http://ereg.egov.bg/segment/R-3119", IsNullable = false)]
    public partial class OutstandingConditionsForWithdrawServiceMessage : IDocumentForm
    {
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private DocumentURI documentURIField;
        private System.DateTime? documentReceiptOrSigningDateField;
        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private string outstandingConditionsForWithdrawServiceMessageHeaderField;
        private AISCaseURI aISCaseURIField;
        private List<OutstandingConditionsForWithdrawServiceMessageGrounds> groundsField;
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
        public string OutstandingConditionsForWithdrawServiceMessageHeader
        {
            get
            {
                return this.outstandingConditionsForWithdrawServiceMessageHeaderField;
            }
            set
            {
                this.outstandingConditionsForWithdrawServiceMessageHeaderField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("Grounds")]
        public List<OutstandingConditionsForWithdrawServiceMessageGrounds> Grounds
        {
            get
            {
                return this.groundsField;
            }
            set
            {
                this.groundsField = value;
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

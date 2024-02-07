using EAU.Documents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Съобщение за неизпълнени условия за предоставяне на услугата
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3100")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3100", IsNullable = false)]
    public partial class OutstandingConditionsForStartOfServiceMessage : IDocumentForm
    {
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private DocumentURI documentURIField;
        private System.DateTime? documentReceiptOrSigningDateField;
        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private string outstandingConditionsForStartOfServiceMessageHeaderField;
        private AISCaseURI aISCaseURIField;
        private List<OutstandingConditionsForStartOfServiceMessageGrounds> groundsField;
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
        public string OutstandingConditionsForStartOfServiceMessageHeader
        {
            get
            {
                return this.outstandingConditionsForStartOfServiceMessageHeaderField;
            }
            set
            {
                this.outstandingConditionsForStartOfServiceMessageHeaderField = value;
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
        public List<OutstandingConditionsForStartOfServiceMessageGrounds> Grounds
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

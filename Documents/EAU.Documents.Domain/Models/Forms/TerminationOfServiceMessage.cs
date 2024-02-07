using EAU.Documents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Уведомление за прекратяване на услуга
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3101")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3101", IsNullable = false)]
    public partial class TerminationOfServiceMessage: IDocumentForm
    {
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private DocumentURI documentURIField;
        private System.DateTime? documentReceiptOrSigningDateField;
        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private string terminationOfServiceMessageHeaderField;
        private AISCaseURI aISCaseURIField;
        private List<TerminationOfServiceMessageGrounds> groundsField;
        private string terminationDocumentRegistrationNumberField;
        private System.DateTime? terminationDateField;
        private PoliceDepartment policeDepartmentField;
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
        public string TerminationOfServiceMessageHeader
        {
            get
            {
                return this.terminationOfServiceMessageHeaderField;
            }
            set
            {
                this.terminationOfServiceMessageHeaderField = value;
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
        public List<TerminationOfServiceMessageGrounds> Grounds
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
        public string TerminationDocumentRegistrationNumber
        {
            get
            {
                return this.terminationDocumentRegistrationNumberField;
            }
            set
            {
                this.terminationDocumentRegistrationNumberField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? TerminationDate
        {
            get
            {
                return this.terminationDateField;
            }
            set
            {
                this.terminationDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TerminationDateSpecified
        {
            get
            {
                return TerminationDate.HasValue;
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

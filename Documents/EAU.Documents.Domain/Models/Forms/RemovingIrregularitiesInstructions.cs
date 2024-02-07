using CNSys;
using System.Collections.Generic;

namespace EAU.Documents.Domain.Models.Forms
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3010")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3010", IsNullable = false)]
    public partial class RemovingIrregularitiesInstructions : IDocumentForm
    {
        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private DocumentURI irregularityDocumentURIField;
        private System.DateTime? irregularityDocumentReceiptOrSigningDateField;
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private string removingIrregularitiesInstructionsHeaderField;
        private DocumentURI applicationDocumentURIField;
        private System.DateTime? applicationDocumentReceiptOrSigningDateField;
        private AISCaseURI aISCaseURIField;
        private List<RemovingIrregularitiesInstructionsIrregularities> irregularitiesField;
        private string deadlineCorrectionIrregularitiesField;
        private string administrativeBodyNameField;
        private RemovingIrregularitiesInstructionsOfficial officialField;
        private XMLDigitalSignature xMLDigitalSignatureField;
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
        public DocumentURI IrregularityDocumentURI
        {
            get
            {
                return this.irregularityDocumentURIField;
            }
            set
            {
                this.irregularityDocumentURIField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? IrregularityDocumentReceiptOrSigningDate
        {
            get
            {
                return this.irregularityDocumentReceiptOrSigningDateField;
            }
            set
            {
                this.irregularityDocumentReceiptOrSigningDateField = value;
            }
        }
        //Manualy added in next time when we made automatic code generation, we bust use weeked schemas
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IrregularityDocumentReceiptOrSigningDateSpecified
        {
            get
            {
                return this.irregularityDocumentReceiptOrSigningDateField.HasValue;
            }
        }
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
        public string RemovingIrregularitiesInstructionsHeader
        {
            get
            {
                return this.removingIrregularitiesInstructionsHeaderField;
            }
            set
            {
                this.removingIrregularitiesInstructionsHeaderField = value;
            }
        }
        public DocumentURI ApplicationDocumentURI
        {
            get
            {
                return this.applicationDocumentURIField;
            }
            set
            {
                this.applicationDocumentURIField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ApplicationDocumentReceiptOrSigningDate
        {
            get
            {
                return this.applicationDocumentReceiptOrSigningDateField;
            }
            set
            {
                this.applicationDocumentReceiptOrSigningDateField = value;
            }
        }
        //Manualy added in next time when we made automatic code generation, we bust use weeked schemas
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ApplicationDocumentReceiptOrSigningDateSpecified
        {
            get
            {
                return this.applicationDocumentReceiptOrSigningDateField.HasValue;
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
        [System.Xml.Serialization.XmlElementAttribute("Irregularities")]
        public List<RemovingIrregularitiesInstructionsIrregularities> Irregularities
        {
            get
            {
                return this.irregularitiesField;
            }
            set
            {
                this.irregularitiesField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
        public string DeadlineCorrectionIrregularities
        {
            get
            {
                return this.deadlineCorrectionIrregularitiesField;
            }
            set
            {
                this.deadlineCorrectionIrregularitiesField = value;
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
        public RemovingIrregularitiesInstructionsOfficial Official
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
        [EmptyCheckIgnore]
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
}

using EAU.Documents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Documents.Domain.Models.Forms
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3202")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3202", IsNullable = false)]
    public partial class RefusalWithoutConsideringTerminationProceedings : IDocumentForm
    {
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private DocumentURI documentURIField;
        private System.DateTime? documentReceiptOrSigningDateField;
        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private string individualAdministrativeActRefusalHeaderField;
        private AISCaseURI aISCaseURIField;
        private string individualAdministrativeActRefusalLegalGroundField;
        private string individualAdministrativeActRefusalFactualGroundField;
        private string individualAdministrativeActRefusalAppealTermField;
        private string individualAdministrativeActRefusalAppealAuthorityField;
        private string administrativeBodyNameField;
        private List<RefusalWithoutConsideringTerminationProceedingsOfficial> officialField;
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
        public string IndividualAdministrativeActRefusalHeader
        {
            get
            {
                return this.individualAdministrativeActRefusalHeaderField;
            }
            set
            {
                this.individualAdministrativeActRefusalHeaderField = value;
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
        public string IndividualAdministrativeActRefusalLegalGround
        {
            get
            {
                return this.individualAdministrativeActRefusalLegalGroundField;
            }
            set
            {
                this.individualAdministrativeActRefusalLegalGroundField = value;
            }
        }
        public string IndividualAdministrativeActRefusalFactualGround
        {
            get
            {
                return this.individualAdministrativeActRefusalFactualGroundField;
            }
            set
            {
                this.individualAdministrativeActRefusalFactualGroundField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "duration")]
        public string IndividualAdministrativeActRefusalAppealTerm
        {
            get
            {
                return this.individualAdministrativeActRefusalAppealTermField;
            }
            set
            {
                this.individualAdministrativeActRefusalAppealTermField = value;
            }
        }
        public string IndividualAdministrativeActRefusalAppealAuthority
        {
            get
            {
                return this.individualAdministrativeActRefusalAppealAuthorityField;
            }
            set
            {
                this.individualAdministrativeActRefusalAppealAuthorityField = value;
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
        [System.Xml.Serialization.XmlElementAttribute("Official")]
        public List<RefusalWithoutConsideringTerminationProceedingsOfficial> Official
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
    }
}

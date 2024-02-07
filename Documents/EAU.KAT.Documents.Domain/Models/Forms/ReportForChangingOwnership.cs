using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Справка за промяна на собственост на ПС
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()] 
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3306")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3306", IsNullable = false)]
    public partial class ReportForChangingOwnership : IDocumentForm
    {
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private DocumentURI documentURIField;
        private AISCaseURI aISCaseURIField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private VehicleRegistrationData vehicleRegistrationDataField;
        private ReportForChangingOwnershipOldOwnersData oldOwnersDataField;
        private ReportForChangingOwnershipNewOwnersData newOwnersDataField;
        private List<Status> localTaxesField;
        private List<Status> periodicTechnicalCheckField;
        private ReportForChangingOwnershipGuaranteeFund guaranteeFundField;
        private System.DateTime? documentReceiptOrSigningDateField;
        private string administrativeBodyNameField;
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
        public VehicleRegistrationData VehicleRegistrationData
        {
            get
            {
                return this.vehicleRegistrationDataField;
            }
            set
            {
                this.vehicleRegistrationDataField = value;
            }
        }
        public ReportForChangingOwnershipOldOwnersData OldOwnersData
        {
            get
            {
                return this.oldOwnersDataField;
            }
            set
            {
                this.oldOwnersDataField = value;
            }
        }
        public ReportForChangingOwnershipNewOwnersData NewOwnersData
        {
            get
            {
                return this.newOwnersDataField;
            }
            set
            {
                this.newOwnersDataField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Status> LocalTaxes
        {
            get
            {
                return this.localTaxesField;
            }
            set
            {
                this.localTaxesField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Status> PeriodicTechnicalCheck
        {
            get
            {
                return this.periodicTechnicalCheckField;
            }
            set
            {
                this.periodicTechnicalCheckField = value;
            }
        }
        public ReportForChangingOwnershipGuaranteeFund GuaranteeFund
        {
            get
            {
                return this.guaranteeFundField;
            }
            set
            {
                this.guaranteeFundField = value;
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

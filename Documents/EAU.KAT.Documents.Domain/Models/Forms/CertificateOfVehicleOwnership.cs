using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Models;
using EAU.KAT.Documents.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.KAT.Documents.Domain.Models.Forms
{
    /// <summary>
    /// Удостоверение за бивша/настояща собственост на ПС
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3133")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3133", IsNullable = false)]
    public partial class CertificateOfVehicleOwnership: IDocumentForm
    {
        private DocumentTypeURI documentTypeURIField;
        private string documentTypeNameField;
        private DocumentURI documentURIField;
        private AISCaseURI aISCaseURIField;
        private System.DateTime? documentReceiptOrSigningDateField;
        private string certificateNumberField;
        private ElectronicServiceProviderBasicData electronicServiceProviderBasicDataField;
        private ElectronicServiceApplicant electronicServiceApplicantField;
        private string certificateOfVehicleOwnershipHeaderField;
        private PersonAddress permanentAddressField;
        private PoliceDepartment issuingPoliceDepartmentField;
        private DocumentFor? certificateKindField;
        private VehicleData vehicleDataField;
        private VehicleOwnerInformationCollection vehicleOwnerInformationCollectionField;
        private OwnershipCertificateReason? ownershipCertificateReasonField;
        private System.DateTime? kATVerificationDateTimeField;
        private string administrativeBodyNameField;
        private CertificateOfVehicleOwnershipOfficial officialField;
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
        public string CertificateNumber
        {
            get
            {
                return this.certificateNumberField;
            }
            set
            {
                this.certificateNumberField = value;
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
        public string CertificateOfVehicleOwnershipHeader
        {
            get
            {
                return this.certificateOfVehicleOwnershipHeaderField;
            }
            set
            {
                this.certificateOfVehicleOwnershipHeaderField = value;
            }
        }
        public PersonAddress PermanentAddress
        {
            get
            {
                return this.permanentAddressField;
            }
            set
            {
                this.permanentAddressField = value;
            }
        }
        public PoliceDepartment IssuingPoliceDepartment
        {
            get
            {
                return this.issuingPoliceDepartmentField;
            }
            set
            {
                this.issuingPoliceDepartmentField = value;
            }
        }
        public DocumentFor? CertificateKind
        {
            get
            {
                return this.certificateKindField;
            }
            set
            {
                this.certificateKindField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CertificateKindSpecified
        {
            get
            {
                return CertificateKind.HasValue;
            }
        }
        public VehicleData VehicleData
        {
            get
            {
                return this.vehicleDataField;
            }
            set
            {
                this.vehicleDataField = value;
            }
        }
        public VehicleOwnerInformationCollection VehicleOwnerInformationCollection
        {
            get
            {
                return this.vehicleOwnerInformationCollectionField;
            }
            set
            {
                this.vehicleOwnerInformationCollectionField = value;
            }
        }
        public OwnershipCertificateReason? OwnershipCertificateReason
        {
            get
            {
                return this.ownershipCertificateReasonField;
            }
            set
            {
                this.ownershipCertificateReasonField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OwnershipCertificateReasonSpecified
        {
            get
            {
                return OwnershipCertificateReason.HasValue;
            }
        }
        public System.DateTime? KATVerificationDateTime
        {
            get
            {
                return this.kATVerificationDateTimeField;
            }
            set
            {
                this.kATVerificationDateTimeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool KATVerificationDateTimeSpecified
        {
            get
            {
                return KATVerificationDateTime.HasValue;
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
        public CertificateOfVehicleOwnershipOfficial Official
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
}

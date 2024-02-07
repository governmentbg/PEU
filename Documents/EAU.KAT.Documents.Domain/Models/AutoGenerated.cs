using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using System;
using System.Collections.Generic;

namespace EAU.KAT.Documents.Domain.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3340")]
    public partial class ReportForVehicleOwners
    {
        private List<Object> itemsField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
        public List<Object> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3340")]
    public partial class ReportForVehicleGuaranteeFund
    {
        private System.DateTime? policyValidToField;
        private List<Status> statusField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? PolicyValidTo
        {
            get
            {
                return this.policyValidToField;
            }
            set
            {
                this.policyValidToField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PolicyValidToSpecified
        {
            get
            {
                return PolicyValidTo.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("Status")]
        public List<Status> Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3340")]
    public partial class ReportForVehicleRPSSVehicleData
    {
        private VehicleRegistrationData vehicleRegistrationDataField;
        private List<ReportForVehicleRPSSVehicleDataOwners> ownersField;
        private List<Status> statusesField;
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
        [System.Xml.Serialization.XmlElementAttribute("Owners")]
        public List<ReportForVehicleRPSSVehicleDataOwners> Owners
        {
            get
            {
                return this.ownersField;
            }
            set
            {
                this.ownersField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Status> Statuses
        {
            get
            {
                return this.statusesField;
            }
            set
            {
                this.statusesField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3340")]
    public partial class ReportForVehicleRPSSVehicleDataOwners
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3105")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3105", IsNullable = false)]
    public partial class ApplicationForIssuingDocumentofVehicleOwnershipData
    {
        private PersonAddress permanentAddressField;
        private PersonAddress currentAddressField;
        private DocumentFor documentForField;
        private ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMake itemField;
        private OwnershipCertificateReason? ownershipCertificateReasonField;
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
        public PersonAddress CurrentAddress
        {
            get
            {
                return this.currentAddressField;
            }
            set
            {
                this.currentAddressField = value;
            }
        }
        public DocumentFor DocumentFor
        {
            get
            {
                return this.documentForField;
            }
            set
            {
                this.documentForField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("RegistrationAndMake")]
        public ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMake Item
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
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3105")]
    public partial class ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMake
    {
        private string registrationNumberField;
        private string makeModelField;
        public string RegistrationNumber
        {
            get
            {
                return this.registrationNumberField;
            }
            set
            {
                this.registrationNumberField = value;
            }
        }
        public string MakeModel
        {
            get
            {
                return this.makeModelField;
            }
            set
            {
                this.makeModelField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3118")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3118", IsNullable = false)]
    public partial class ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverData
    {
        private PersonAddress permanentAddressField;
        private PersonAddress currentAddressField;
        private ANDCertificateReason? aNDCertificateReasonField;
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
        public PersonAddress CurrentAddress
        {
            get
            {
                return this.currentAddressField;
            }
            set
            {
                this.currentAddressField = value;
            }
        }
        public ANDCertificateReason? ANDCertificateReason
        {
            get
            {
                return this.aNDCertificateReasonField;
            }
            set
            {
                this.aNDCertificateReasonField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ANDCertificateReasonSpecified
        {
            get
            {
                return ANDCertificateReason.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3125")]
    public partial class CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverOfficial
    {
        private object itemField;
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3128")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3128", IsNullable = false)]
    public partial class VehicleDataItem
    {
        private string registrationNumberField;
        private string makeModelField;
        private string previousRegistrationNumberField;
        private string identificationNumberField;
        private string engineNumberField;
        private string typeField;
        private System.DateTime? vehicleFirstRegistrationDateField;
        private List<VehicleDataItemVehicleSuspension> vehicleSuspensionField;
        private System.DateTime? vehicleOwnerStartDateField;
        private System.DateTime? vehicleOwnerEndDateField;
        public string RegistrationNumber
        {
            get
            {
                return this.registrationNumberField;
            }
            set
            {
                this.registrationNumberField = value;
            }
        }
        public string MakeModel
        {
            get
            {
                return this.makeModelField;
            }
            set
            {
                this.makeModelField = value;
            }
        }
        public string PreviousRegistrationNumber
        {
            get
            {
                return this.previousRegistrationNumberField;
            }
            set
            {
                this.previousRegistrationNumberField = value;
            }
        }
        public string IdentificationNumber
        {
            get
            {
                return this.identificationNumberField;
            }
            set
            {
                this.identificationNumberField = value;
            }
        }
        public string EngineNumber
        {
            get
            {
                return this.engineNumberField;
            }
            set
            {
                this.engineNumberField = value;
            }
        }
        public string Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? VehicleFirstRegistrationDate
        {
            get
            {
                return this.vehicleFirstRegistrationDateField;
            }
            set
            {
                this.vehicleFirstRegistrationDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VehicleFirstRegistrationDateSpecified
        {
            get
            {
                return VehicleFirstRegistrationDate.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("VehicleSuspension")]
        public List<VehicleDataItemVehicleSuspension> VehicleSuspension
        {
            get
            {
                return this.vehicleSuspensionField;
            }
            set
            {
                this.vehicleSuspensionField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? VehicleOwnerStartDate
        {
            get
            {
                return this.vehicleOwnerStartDateField;
            }
            set
            {
                this.vehicleOwnerStartDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VehicleOwnerStartDateSpecified
        {
            get
            {
                return VehicleOwnerStartDate.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? VehicleOwnerEndDate
        {
            get
            {
                return this.vehicleOwnerEndDateField;
            }
            set
            {
                this.vehicleOwnerEndDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VehicleOwnerEndDateSpecified
        {
            get
            {
                return VehicleOwnerEndDate.HasValue;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3128")]
    public partial class VehicleDataItemVehicleSuspension
    {
        private System.DateTime? vehSuspensionDateField;
        private string vehSuspensionReasonField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? VehSuspensionDate
        {
            get
            {
                return this.vehSuspensionDateField;
            }
            set
            {
                this.vehSuspensionDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VehSuspensionDateSpecified
        {
            get
            {
                return VehSuspensionDate.HasValue;
            }
        }
        public string VehSuspensionReason
        {
            get
            {
                return this.vehSuspensionReasonField;
            }
            set
            {
                this.vehSuspensionReasonField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3129")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3129", IsNullable = false)]
    public partial class VehicleDataItemCollection
    {
        private List<VehicleDataItem> itemsField;
        [System.Xml.Serialization.XmlElementAttribute("Items")]
        public List<VehicleDataItem> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3130")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3130", IsNullable = false)]
    public partial class VehicleData
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("VehicleDataItem", typeof(VehicleDataItem))]
        [System.Xml.Serialization.XmlElementAttribute("VehicleDataItemCollection", typeof(VehicleDataItemCollection))]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3131")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3131", IsNullable = false)]
    public partial class VehicleOwnerInformationItem
    {
        private object itemField;
        private VehicleOwnerAddress addressField;
        [System.Xml.Serialization.XmlElementAttribute("VehicleOwnerCompanyInformation", typeof(EntityBasicData))]
        [System.Xml.Serialization.XmlElementAttribute("VehicleOwnerPersonalBGInformation", typeof(PersonBasicData))]
        [System.Xml.Serialization.XmlElementAttribute("VehicleOwnerPersonalFInformation", typeof(ForeignCitizenBasicData))]
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
        public VehicleOwnerAddress Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3135")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3135", IsNullable = false)]
    public partial class VehicleOwnerAddress
    {
        private string districtCodeField;
        private string districtNameField;
        private string municipalityCodeField;
        private string municipalityNameField;
        private string residenceCodeField;
        private string residenceNameField;
        private string addressSupplementField;
        public string DistrictCode
        {
            get
            {
                return this.districtCodeField;
            }
            set
            {
                this.districtCodeField = value;
            }
        }
        public string DistrictName
        {
            get
            {
                return this.districtNameField;
            }
            set
            {
                this.districtNameField = value;
            }
        }
        public string MunicipalityCode
        {
            get
            {
                return this.municipalityCodeField;
            }
            set
            {
                this.municipalityCodeField = value;
            }
        }
        public string MunicipalityName
        {
            get
            {
                return this.municipalityNameField;
            }
            set
            {
                this.municipalityNameField = value;
            }
        }
        public string ResidenceCode
        {
            get
            {
                return this.residenceCodeField;
            }
            set
            {
                this.residenceCodeField = value;
            }
        }
        public string ResidenceName
        {
            get
            {
                return this.residenceNameField;
            }
            set
            {
                this.residenceNameField = value;
            }
        }
        public string AddressSupplement
        {
            get
            {
                return this.addressSupplementField;
            }
            set
            {
                this.addressSupplementField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3132")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3132", IsNullable = false)]
    public partial class VehicleOwnerInformationCollection
    {
        private List<VehicleOwnerInformationItem> itemsField;
        [System.Xml.Serialization.XmlElementAttribute("Items")]
        public List<VehicleOwnerInformationItem> Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3133")]
    public partial class CertificateOfVehicleOwnershipOfficial
    {
        private object itemField;
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3300")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3300", IsNullable = false)]
    public partial class PersonData
    {
        private PersonBasicData personBasicDataField;
        private BIDMaritalStatus? maritalStatusField;
        private System.DateTime? deathDateField;
        private Status statusField;
        private PersonAddress permanentAddressField;
        private string bDSCategoryCodeField;
        private string bDSCategoryValueField;
        public PersonBasicData PersonBasicData
        {
            get
            {
                return this.personBasicDataField;
            }
            set
            {
                this.personBasicDataField = value;
            }
        }
        public BIDMaritalStatus? MaritalStatus
        {
            get
            {
                return this.maritalStatusField;
            }
            set
            {
                this.maritalStatusField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool MaritalStatusSpecified
        {
            get
            {
                return MaritalStatus.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? DeathDate
        {
            get
            {
                return this.deathDateField;
            }
            set
            {
                this.deathDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DeathDateSpecified
        {
            get
            {
                return DeathDate.HasValue;
            }
        }
        public Status Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
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
        public string BDSCategoryCode
        {
            get
            {
                return this.bDSCategoryCodeField;
            }
            set
            {
                this.bDSCategoryCodeField = value;
            }
        }
        public string BDSCategoryValue
        {
            get
            {
                return this.bDSCategoryValueField;
            }
            set
            {
                this.bDSCategoryValueField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3301")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3301", IsNullable = false)]
    public partial class Status
    {
        private string codeField;
        private bool? blockingField;
        private string descriptionField;
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        public bool? Blocking
        {
            get
            {
                return this.blockingField;
            }
            set
            {
                this.blockingField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool BlockingSpecified
        {
            get
            {
                return Blocking.HasValue;
            }
        }
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3302")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3302", IsNullable = false)]
    public partial class EntityData
    {
        private string nameField;
        private string nameTransField;
        private string fullNameField;
        private string identifierField;
        private string identifierTypeField;
        private Status statusField;
        private string recStatusField;
        private PersonAddress entityManagmentAddressField;
        private string legalFormField;
        private string legalFormCodeField;
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        public string NameTrans
        {
            get
            {
                return this.nameTransField;
            }
            set
            {
                this.nameTransField = value;
            }
        }
        public string FullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
            }
        }
        public string Identifier
        {
            get
            {
                return this.identifierField;
            }
            set
            {
                this.identifierField = value;
            }
        }
        public string IdentifierType
        {
            get
            {
                return this.identifierTypeField;
            }
            set
            {
                this.identifierTypeField = value;
            }
        }
        public Status Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
        public string RecStatus
        {
            get
            {
                return this.recStatusField;
            }
            set
            {
                this.recStatusField = value;
            }
        }
        public PersonAddress EntityManagmentAddress
        {
            get
            {
                return this.entityManagmentAddressField;
            }
            set
            {
                this.entityManagmentAddressField = value;
            }
        }
        public string LegalForm
        {
            get
            {
                return this.legalFormField;
            }
            set
            {
                this.legalFormField = value;
            }
        }
        public string LegalFormCode
        {
            get
            {
                return this.legalFormCodeField;
            }
            set
            {
                this.legalFormCodeField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3303")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3303", IsNullable = false)]
    public partial class VehicleRegistrationData
    {
        private string registrationNumberField;
        private string identificationNumberField;
        private string engineNumberField;
        private string registrationCertificateNumberField;
        private RegistrationCertificateTypeNomenclature? registrationCertificateTypeField;
        private System.DateTime? dateOfFirstRegField;
        private PersonAddress regAddressField;
        private PoliceDepartment policeDepartmentField;
        private System.DateTime? nextVehicleInspectionField;
        private VehicleCategory vehicleCategoryField;
        private List<Status> statusesField;
        private bool? hasActiveSeizureField;
        private bool? hasCustomsLimitationField;
        private string makeAndModelField;
        public string RegistrationNumber
        {
            get
            {
                return this.registrationNumberField;
            }
            set
            {
                this.registrationNumberField = value;
            }
        }
        public string IdentificationNumber
        {
            get
            {
                return this.identificationNumberField;
            }
            set
            {
                this.identificationNumberField = value;
            }
        }
        public string EngineNumber
        {
            get
            {
                return this.engineNumberField;
            }
            set
            {
                this.engineNumberField = value;
            }
        }
        public string RegistrationCertificateNumber
        {
            get
            {
                return this.registrationCertificateNumberField;
            }
            set
            {
                this.registrationCertificateNumberField = value;
            }
        }
        public RegistrationCertificateTypeNomenclature? RegistrationCertificateType
        {
            get
            {
                return this.registrationCertificateTypeField;
            }
            set
            {
                this.registrationCertificateTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RegistrationCertificateTypeSpecified
        {
            get
            {
                return RegistrationCertificateType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? DateOfFirstReg
        {
            get
            {
                return this.dateOfFirstRegField;
            }
            set
            {
                this.dateOfFirstRegField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DateOfFirstRegSpecified
        {
            get
            {
                return DateOfFirstReg.HasValue;
            }
        }
        public PersonAddress RegAddress
        {
            get
            {
                return this.regAddressField;
            }
            set
            {
                this.regAddressField = value;
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
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? NextVehicleInspection
        {
            get
            {
                return this.nextVehicleInspectionField;
            }
            set
            {
                this.nextVehicleInspectionField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NextVehicleInspectionSpecified
        {
            get
            {
                return NextVehicleInspection.HasValue;
            }
        }
        public VehicleCategory VehicleCategory
        {
            get
            {
                return this.vehicleCategoryField;
            }
            set
            {
                this.vehicleCategoryField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Status> Statuses
        {
            get
            {
                return this.statusesField;
            }
            set
            {
                this.statusesField = value;
            }
        }
        public bool? HasActiveSeizure
        {
            get
            {
                return this.hasActiveSeizureField;
            }
            set
            {
                this.hasActiveSeizureField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool HasActiveSeizureSpecified
        {
            get
            {
                return HasActiveSeizure.HasValue;
            }
        }
        public bool? HasCustomsLimitation
        {
            get
            {
                return this.hasCustomsLimitationField;
            }
            set
            {
                this.hasCustomsLimitationField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool HasCustomsLimitationSpecified
        {
            get
            {
                return HasCustomsLimitation.HasValue;
            }
        }
        public string MakeAndModel
        {
            get
            {
                return this.makeAndModelField;
            }
            set
            {
                this.makeAndModelField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3307")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3307", IsNullable = false)]
    public partial class VehicleCategory
    {
        private string codeField;
        private string nameField;
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3304")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3304", IsNullable = false)]
    public partial class RequestForDecisionForDealData
    {
        private VehicleRegistrationData vehicleRegistrationDataField;
        private List<RequestForDecisionForDealDataOwner> ownersCollectionField;
        private List<RequestForDecisionForDealDataBuyer> buyersCollectionField;
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
        [System.Xml.Serialization.XmlArrayItemAttribute("Owner", IsNullable = false)]
        public List<RequestForDecisionForDealDataOwner> OwnersCollection
        {
            get
            {
                return this.ownersCollectionField;
            }
            set
            {
                this.ownersCollectionField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("Buyer", IsNullable = false)]
        public List<RequestForDecisionForDealDataBuyer> BuyersCollection
        {
            get
            {
                return this.buyersCollectionField;
            }
            set
            {
                this.buyersCollectionField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3304")]
    public partial class RequestForDecisionForDealDataOwner
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3304")]
    public partial class RequestForDecisionForDealDataBuyer
    {
        private object itemField;
        private string emailAddressField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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
        public string EmailAddress
        {
            get
            {
                return this.emailAddressField;
            }
            set
            {
                this.emailAddressField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3306")]
    public partial class ReportForChangingOwnershipOldOwnersData
    {
        private List<ReportForChangingOwnershipOldOwnersDataOldOwners> oldOwnersField;
        private List<Status> statusField;
        [System.Xml.Serialization.XmlElementAttribute("OldOwners")]
        public List<ReportForChangingOwnershipOldOwnersDataOldOwners> OldOwners
        {
            get
            {
                return this.oldOwnersField;
            }
            set
            {
                this.oldOwnersField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Status> Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3306")]
    public partial class ReportForChangingOwnershipOldOwnersDataOldOwners
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3306")]
    public partial class ReportForChangingOwnershipNewOwnersData
    {
        private List<ReportForChangingOwnershipNewOwnersDataNewOwners> newOwnersField;
        private List<Status> statusField;
        [System.Xml.Serialization.XmlElementAttribute("NewOwners")]
        public List<ReportForChangingOwnershipNewOwnersDataNewOwners> NewOwners
        {
            get
            {
                return this.newOwnersField;
            }
            set
            {
                this.newOwnersField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Status> Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3306")]
    public partial class ReportForChangingOwnershipNewOwnersDataNewOwners
    {
        private object itemField;
        private string emailAddressField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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
        public string EmailAddress
        {
            get
            {
                return this.emailAddressField;
            }
            set
            {
                this.emailAddressField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3306")]
    public partial class ReportForChangingOwnershipGuaranteeFund
    {
        private System.DateTime? policyValidToField;
        private List<Status> statusField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? PolicyValidTo
        {
            get
            {
                return this.policyValidToField;
            }
            set
            {
                this.policyValidToField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PolicyValidToSpecified
        {
            get
            {
                return PolicyValidTo.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("Status")]
        public List<Status> Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3310")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3310", IsNullable = false)]
    public partial class DataForPrintSRMPSData
    {
        private DataForPrintSRMPSDataHolderData holderDataField;
        private DataForPrintSRMPSDataUserData userDataField;
        private List<DataForPrintSRMPSDataNewOwner> newOwnersField;
        public DataForPrintSRMPSDataHolderData HolderData
        {
            get
            {
                return this.holderDataField;
            }
            set
            {
                this.holderDataField = value;
            }
        }
        public DataForPrintSRMPSDataUserData UserData
        {
            get
            {
                return this.userDataField;
            }
            set
            {
                this.userDataField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("NewOwner", IsNullable = false)]
        public List<DataForPrintSRMPSDataNewOwner> NewOwners
        {
            get
            {
                return this.newOwnersField;
            }
            set
            {
                this.newOwnersField = value;
            }
        }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3310")]
    public partial class DataForPrintSRMPSDataHolderData
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3310")]
    public partial class DataForPrintSRMPSDataUserData
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3310")]
    public partial class DataForPrintSRMPSDataNewOwner
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3158")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3158", IsNullable = false)]
    public partial class ApplicationForIssuingOfDuplicateOfDrivingLicenseControlCouponData
    {
        private PoliceDepartment issuingPoliceDepartmentField;
        private PersonAddress permanentAddressField;
        private PersonAddress currentAddressField;
        private bool? agreementToReceiveERefusalField;
        private CouponDuplicateIssuensReason? couponDuplicateIssuensReasonField;
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
        public PersonAddress CurrentAddress
        {
            get
            {
                return this.currentAddressField;
            }
            set
            {
                this.currentAddressField = value;
            }
        }
        public bool? AgreementToReceiveERefusal
        {
            get
            {
                return this.agreementToReceiveERefusalField;
            }
            set
            {
                this.agreementToReceiveERefusalField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AgreementToReceiveERefusalSpecified
        {
            get
            {
                return AgreementToReceiveERefusal.HasValue;
            }
        }
        public CouponDuplicateIssuensReason? CouponDuplicateIssuensReason
        {
            get
            {
                return this.couponDuplicateIssuensReasonField;
            }
            set
            {
                this.couponDuplicateIssuensReasonField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CouponDuplicateIssuensReasonSpecified
        {
            get
            {
                return CouponDuplicateIssuensReason.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3156")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3156", IsNullable = false)]
    public partial class ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesData
    {
        private PoliceDepartment issuingPoliceDepartmentField;
        private PersonAddress permanentAddressField;
        private PersonAddress currentAddressField;
        private bool? agreementToReceiveERefusalField;
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
        public PersonAddress CurrentAddress
        {
            get
            {
                return this.currentAddressField;
            }
            set
            {
                this.currentAddressField = value;
            }
        }
        public bool? AgreementToReceiveERefusal
        {
            get
            {
                return this.agreementToReceiveERefusalField;
            }
            set
            {
                this.agreementToReceiveERefusalField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AgreementToReceiveERefusalSpecified
        {
            get
            {
                return AgreementToReceiveERefusal.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3313")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3313", IsNullable = false)]
    public partial class VehicleDataRequest
    {
        private VehicleRegistrationData registrationDataField;
        private VehicleDataRequestOwnersCollection ownersCollectionField;
        private string serviceCodeField;
        private string serviceNameField;
        private List<AISKATReason> reasonsField;
        private string phoneField;
        private bool? agreementToReceiveERefusalField;
        public VehicleRegistrationData RegistrationData
        {
            get
            {
                return this.registrationDataField;
            }
            set
            {
                this.registrationDataField = value;
            }
        }
        public VehicleDataRequestOwnersCollection OwnersCollection
        {
            get
            {
                return this.ownersCollectionField;
            }
            set
            {
                this.ownersCollectionField = value;
            }
        }
        public string ServiceCode
        {
            get
            {
                return this.serviceCodeField;
            }
            set
            {
                this.serviceCodeField = value;
            }
        }
        public string ServiceName
        {
            get
            {
                return this.serviceNameField;
            }
            set
            {
                this.serviceNameField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("Reason", IsNullable = false)]
        public List<AISKATReason> Reasons
        {
            get
            {
                return this.reasonsField;
            }
            set
            {
                this.reasonsField = value;
            }
        }
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
        public bool? AgreementToReceiveERefusal
        {
            get
            {
                return this.agreementToReceiveERefusalField;
            }
            set
            {
                this.agreementToReceiveERefusalField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AgreementToReceiveERefusalSpecified
        {
            get
            {
                return AgreementToReceiveERefusal.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3313")]
    public partial class VehicleDataRequestOwnersCollection
    {
        private List<VehicleDataRequestOwnersCollectionOwners> ownersField;
        [System.Xml.Serialization.XmlElementAttribute("Owners")]
        public List<VehicleDataRequestOwnersCollectionOwners> Owners
        {
            get
            {
                return this.ownersField;
            }
            set
            {
                this.ownersField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3315")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3315", IsNullable = false)]
    public partial class AISKATReason
    {
        private string codeField;
        private string nameField;
        public string Code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3313")]
    public partial class VehicleDataRequestOwnersCollectionOwners
    {
        private object itemField;
        [System.Xml.Serialization.XmlElementAttribute("Item", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PersonIdentifier", typeof(PersonIdentifier))]
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3340")]
    public partial class ReportForVehiclePeriodicTechnicalCheck
    {
        private System.DateTime? nextInspectionDateField;
        private List<Status> statusField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? NextInspectionDate
        {
            get
            {
                return this.nextInspectionDateField;
            }
            set
            {
                this.nextInspectionDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NextInspectionDateSpecified
        {
            get
            {
                return NextInspectionDate.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("Status")]
        public List<Status> Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3320")]
    public partial class DeclarationForLostSRPPSData
    {
        private string declarationField;
        public string Declaration
        {
            get
            {
                return this.declarationField;
            }
            set
            {
                this.declarationField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3323")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3323", IsNullable = false)]
    public partial class ApplicationForMountingOfRegistrationPlatesAndOrIdentificationOfVehiclesData
    {
        private PoliceDepartment policeDepartmentField;
        private string applicationTextField;
        private string phoneField;
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
        public string ApplicationText
        {
            get
            {
                return this.applicationTextField;
            }
            set
            {
                this.applicationTextField = value;
            }
        }
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3325")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3325", IsNullable = false)]
    public partial class ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsData
    {
        private int? temporaryPlatesCountField;
        private string operationalNewVehicleMakesField;
        private string operationalSecondHandVehicleMakesField;
        private EntityAddress vehicleDealershipAddressField;
        private List<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataAuthorizedPersons> authorizedPersonsField;
        private string phoneField;
        private bool? agreementToReceiveERefusalField;
        public int? TemporaryPlatesCount
        {
            get
            {
                return this.temporaryPlatesCountField;
            }
            set
            {
                this.temporaryPlatesCountField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool TemporaryPlatesCountSpecified
        {
            get
            {
                return TemporaryPlatesCount.HasValue;
            }
        }
        public string OperationalNewVehicleMakes
        {
            get
            {
                return this.operationalNewVehicleMakesField;
            }
            set
            {
                this.operationalNewVehicleMakesField = value;
            }
        }
        public string OperationalSecondHandVehicleMakes
        {
            get
            {
                return this.operationalSecondHandVehicleMakesField;
            }
            set
            {
                this.operationalSecondHandVehicleMakesField = value;
            }
        }
        public EntityAddress VehicleDealershipAddress
        {
            get
            {
                return this.vehicleDealershipAddressField;
            }
            set
            {
                this.vehicleDealershipAddressField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("AuthorizedPersons")]
        public List<ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataAuthorizedPersons> AuthorizedPersons
        {
            get
            {
                return this.authorizedPersonsField;
            }
            set
            {
                this.authorizedPersonsField = value;
            }
        }
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
        public bool? AgreementToReceiveERefusal
        {
            get
            {
                return this.agreementToReceiveERefusalField;
            }
            set
            {
                this.agreementToReceiveERefusalField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AgreementToReceiveERefusalSpecified
        {
            get
            {
                return AgreementToReceiveERefusal.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3326")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3326", IsNullable = false)]
    public partial class MerchantData
    {
        private MerchantDataCompanyCase companyCaseField;
        private EntityAddress entityManagementAddressField;
        private EntityAddress correspondingAddressField;
        private string phoneField;
        public MerchantDataCompanyCase CompanyCase
        {
            get
            {
                return this.companyCaseField;
            }
            set
            {
                this.companyCaseField = value;
            }
        }
        public EntityAddress EntityManagementAddress
        {
            get
            {
                return this.entityManagementAddressField;
            }
            set
            {
                this.entityManagementAddressField = value;
            }
        }
        public EntityAddress CorrespondingAddress
        {
            get
            {
                return this.correspondingAddressField;
            }
            set
            {
                this.correspondingAddressField = value;
            }
        }
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3326")]
    public partial class MerchantDataCompanyCase
    {
        private string numberField;
        private string courtNameField;
        public string Number
        {
            get
            {
                return this.numberField;
            }
            set
            {
                this.numberField = value;
            }
        }
        public string CourtName
        {
            get
            {
                return this.courtNameField;
            }
            set
            {
                this.courtNameField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3325")]
    public partial class ApplicationForIssuingOfTemporaryRegistrationPlatesToMerchantsDataAuthorizedPersons
    {
        private string fullNameField;
        private PersonIdentifier identifierField;
        public string FullName
        {
            get
            {
                return this.fullNameField;
            }
            set
            {
                this.fullNameField = value;
            }
        }
        public PersonIdentifier Identifier
        {
            get
            {
                return this.identifierField;
            }
            set
            {
                this.identifierField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3328")]
    [System.Xml.Serialization.XmlRootAttribute("ApplicationForChangeRegistrationOfVehiclseData", Namespace = "http://ereg.egov.bg/segment/R-3328", IsNullable = false)]
    public partial class ApplicationForChangeRegistrationOfVehiclesData
    {
        private VehicleOwnershipChangeType? vehicleOwnershipChangeTypeField;
        private List<VehicleChangeOwnershipData> vehicleChangeOwnershipDataField;
        public VehicleOwnershipChangeType? VehicleOwnershipChangeType
        {
            get
            {
                return this.vehicleOwnershipChangeTypeField;
            }
            set
            {
                this.vehicleOwnershipChangeTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VehicleOwnershipChangeTypeSpecified
        {
            get
            {
                return VehicleOwnershipChangeType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<VehicleChangeOwnershipData> VehicleChangeOwnershipData
        {
            get
            {
                return this.vehicleChangeOwnershipDataField;
            }
            set
            {
                this.vehicleChangeOwnershipDataField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3329")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3329", IsNullable = false)]
    public partial class VehicleChangeOwnershipData
    {
        private VehicleRegistrationData vehicleRegistrationDataField;
        private List<VehicleChangeOwnershipDataCurrentOwner> currentOwnersCollectionField;
        private List<VehicleChangeOwnershipDataNewOwner> newOwnersCollectionField;
        private bool? availableDocumentForPaidAnnualTaxField;
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
        [System.Xml.Serialization.XmlArrayItemAttribute("CurrentOwner", IsNullable = false)]
        public List<VehicleChangeOwnershipDataCurrentOwner> CurrentOwnersCollection
        {
            get
            {
                return this.currentOwnersCollectionField;
            }
            set
            {
                this.currentOwnersCollectionField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute("NewOwner", IsNullable = false)]
        public List<VehicleChangeOwnershipDataNewOwner> NewOwnersCollection
        {
            get
            {
                return this.newOwnersCollectionField;
            }
            set
            {
                this.newOwnersCollectionField = value;
            }
        }
        public bool? AvailableDocumentForPaidAnnualTax
        {
            get
            {
                return this.availableDocumentForPaidAnnualTaxField;
            }
            set
            {
                this.availableDocumentForPaidAnnualTaxField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AvailableDocumentForPaidAnnualTaxSpecified
        {
            get
            {
                return AvailableDocumentForPaidAnnualTax.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3329")]
    public partial class VehicleChangeOwnershipDataCurrentOwner
    {
        private object itemField;
        private bool? isFarmerField;
        private VehicleOwnerAdditionalCircumstances? vehicleOwnerAdditionalCircumstancesField;
        private string successorDataField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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
        public bool? IsFarmer
        {
            get
            {
                return this.isFarmerField;
            }
            set
            {
                this.isFarmerField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IsFarmerSpecified
        {
            get
            {
                return IsFarmer.HasValue;
            }
        }
        public VehicleOwnerAdditionalCircumstances? VehicleOwnerAdditionalCircumstances
        {
            get
            {
                return this.vehicleOwnerAdditionalCircumstancesField;
            }
            set
            {
                this.vehicleOwnerAdditionalCircumstancesField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VehicleOwnerAdditionalCircumstancesSpecified
        {
            get
            {
                return VehicleOwnerAdditionalCircumstances.HasValue;
            }
        }
        public string SuccessorData
        {
            get
            {
                return this.successorDataField;
            }
            set
            {
                this.successorDataField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3329")]
    public partial class VehicleChangeOwnershipDataNewOwner
    {
        private object itemField;
        private bool? isFarmerField;
        private VehicleOwnerAdditionalCircumstances? vehicleOwnerAdditionalCircumstancesField;
        private string emailAddressField;
        [System.Xml.Serialization.XmlElementAttribute("EntityData", typeof(EntityData))]
        [System.Xml.Serialization.XmlElementAttribute("PersonData", typeof(PersonData))]
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
        public bool? IsFarmer
        {
            get
            {
                return this.isFarmerField;
            }
            set
            {
                this.isFarmerField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IsFarmerSpecified
        {
            get
            {
                return IsFarmer.HasValue;
            }
        }
        public VehicleOwnerAdditionalCircumstances? VehicleOwnerAdditionalCircumstances
        {
            get
            {
                return this.vehicleOwnerAdditionalCircumstancesField;
            }
            set
            {
                this.vehicleOwnerAdditionalCircumstancesField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VehicleOwnerAdditionalCircumstancesSpecified
        {
            get
            {
                return VehicleOwnerAdditionalCircumstances.HasValue;
            }
        }
        public string EmailAddress
        {
            get
            {
                return this.emailAddressField;
            }
            set
            {
                this.emailAddressField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3331")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3331", IsNullable = false)]
    public partial class ApplicationForInitialVehicleRegistrationData
    {
        private ApplicationForInitialVehicleRegistrationDataVehicleIdentificationData vehicleIdentificationDataField;
        private ApplicationForInitialVehicleRegistrationDataOwnersCollection ownersCollectionField;
        private InitialVehicleRegistrationOwnerData ownerOfRegistrationCouponField;
        private bool? otherUserVehicleRepresentativeField;
        private InitialVehicleRegistrationOwnerData vehicleUserDataField;
        private string phoneField;
        private bool? agreementToReceiveERefusalField;
        public ApplicationForInitialVehicleRegistrationDataVehicleIdentificationData VehicleIdentificationData
        {
            get
            {
                return this.vehicleIdentificationDataField;
            }
            set
            {
                this.vehicleIdentificationDataField = value;
            }
        }
        public ApplicationForInitialVehicleRegistrationDataOwnersCollection OwnersCollection
        {
            get
            {
                return this.ownersCollectionField;
            }
            set
            {
                this.ownersCollectionField = value;
            }
        }
        public InitialVehicleRegistrationOwnerData OwnerOfRegistrationCoupon
        {
            get
            {
                return this.ownerOfRegistrationCouponField;
            }
            set
            {
                this.ownerOfRegistrationCouponField = value;
            }
        }
        public bool? OtherUserVehicleRepresentative
        {
            get
            {
                return this.otherUserVehicleRepresentativeField;
            }
            set
            {
                this.otherUserVehicleRepresentativeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool OtherUserVehicleRepresentativeSpecified
        {
            get
            {
                return OtherUserVehicleRepresentative.HasValue;
            }
        }
        public InitialVehicleRegistrationOwnerData VehicleUserData
        {
            get
            {
                return this.vehicleUserDataField;
            }
            set
            {
                this.vehicleUserDataField = value;
            }
        }
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
        public bool? AgreementToReceiveERefusal
        {
            get
            {
                return this.agreementToReceiveERefusalField;
            }
            set
            {
                this.agreementToReceiveERefusalField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AgreementToReceiveERefusalSpecified
        {
            get
            {
                return AgreementToReceiveERefusal.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3331")]
    public partial class ApplicationForInitialVehicleRegistrationDataVehicleIdentificationData
    {
        private string identificationNumberField;
        private string approvalCountryCodeField;
        private string importCountryCodeField;
        private string importCountryNameField;
        private string colorCodeField;
        private string colorNameField;
        private string additionalInfoField;
        public string IdentificationNumber
        {
            get
            {
                return this.identificationNumberField;
            }
            set
            {
                this.identificationNumberField = value;
            }
        }
        public string ApprovalCountryCode
        {
            get
            {
                return this.approvalCountryCodeField;
            }
            set
            {
                this.approvalCountryCodeField = value;
            }
        }
        public string ImportCountryCode
        {
            get
            {
                return this.importCountryCodeField;
            }
            set
            {
                this.importCountryCodeField = value;
            }
        }
        public string ImportCountryName
        {
            get
            {
                return this.importCountryNameField;
            }
            set
            {
                this.importCountryNameField = value;
            }
        }
        public string ColorCode
        {
            get
            {
                return this.colorCodeField;
            }
            set
            {
                this.colorCodeField = value;
            }
        }
        public string ColorName
        {
            get
            {
                return this.colorNameField;
            }
            set
            {
                this.colorNameField = value;
            }
        }
        public string AdditionalInfo
        {
            get
            {
                return this.additionalInfoField;
            }
            set
            {
                this.additionalInfoField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3331")]
    public partial class ApplicationForInitialVehicleRegistrationDataOwnersCollection
    {
        private List<InitialVehicleRegistrationOwnerData> initialVehicleRegistrationOwnerDataField;
        [System.Xml.Serialization.XmlElementAttribute("InitialVehicleRegistrationOwnerData")]
        public List<InitialVehicleRegistrationOwnerData> InitialVehicleRegistrationOwnerData
        {
            get
            {
                return this.initialVehicleRegistrationOwnerDataField;
            }
            set
            {
                this.initialVehicleRegistrationOwnerDataField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3332")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3332", IsNullable = false)]
    public partial class InitialVehicleRegistrationOwnerData
    {
        private object itemField;
        private bool? isVehicleRepresentativeField;
        private bool? isOwnerOfVehicleRegistrationCouponField;
        [System.Xml.Serialization.XmlElementAttribute("Item", typeof(string))]
        [System.Xml.Serialization.XmlElementAttribute("PersonIdentifier", typeof(PersonIdentifier))]
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
        public bool? IsVehicleRepresentative
        {
            get
            {
                return this.isVehicleRepresentativeField;
            }
            set
            {
                this.isVehicleRepresentativeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IsVehicleRepresentativeSpecified
        {
            get
            {
                return IsVehicleRepresentative.HasValue;
            }
        }
        public bool? IsOwnerOfVehicleRegistrationCoupon
        {
            get
            {
                return this.isOwnerOfVehicleRegistrationCouponField;
            }
            set
            {
                this.isOwnerOfVehicleRegistrationCouponField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IsOwnerOfVehicleRegistrationCouponSpecified
        {
            get
            {
                return IsOwnerOfVehicleRegistrationCoupon.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3334")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3334", IsNullable = false)]
    public partial class ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData
    {
        private PoliceDepartment issuingPoliceDepartmentField;
        private string platesTypeCodeField;
        private string platesTypeNameField;
        private PlatesContentTypes? platesContentTypeField;
        private string aISKATVehicleTypeCodeField;
        private string aISKATVehicleTypeNameField;
        private int? rectangularPlatesCountField;
        private int? squarePlatesCountField;
        private string provinceCode;
        private string wishedRegistrationNumberField;
        private bool? agreementToReceiveERefusalField;
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
        public string PlatesTypeCode
        {
            get
            {
                return this.platesTypeCodeField;
            }
            set
            {
                this.platesTypeCodeField = value;
            }
        }
        public string PlatesTypeName
        {
            get
            {
                return this.platesTypeNameField;
            }
            set
            {
                this.platesTypeNameField = value;
            }
        }
        public PlatesContentTypes? PlatesContentType
        {
            get
            {
                return this.platesContentTypeField;
            }
            set
            {
                this.platesContentTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PlatesContentTypeSpecified
        {
            get
            {
                return PlatesContentType.HasValue;
            }
        }
        public string AISKATVehicleTypeCode
        {
            get
            {
                return this.aISKATVehicleTypeCodeField;
            }
            set
            {
                this.aISKATVehicleTypeCodeField = value;
            }
        }
        public string AISKATVehicleTypeName
        {
            get
            {
                return this.aISKATVehicleTypeNameField;
            }
            set
            {
                this.aISKATVehicleTypeNameField = value;
            }
        }
        public int? RectangularPlatesCount
        {
            get
            {
                return this.rectangularPlatesCountField;
            }
            set
            {
                this.rectangularPlatesCountField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool RectangularPlatesCountSpecified
        {
            get
            {
                return RectangularPlatesCount.HasValue;
            }
        }
        public int? SquarePlatesCount
        {
            get
            {
                return this.squarePlatesCountField;
            }
            set
            {
                this.squarePlatesCountField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SquarePlatesCountSpecified
        {
            get
            {
                return SquarePlatesCount.HasValue;
            }
        }
        public string ProvinceCode
        {
            get
            {
                return this.provinceCode;
            }
            set
            {
                this.provinceCode = value;
            }
        }
        public string WishedRegistrationNumber
        {
            get
            {
                return this.wishedRegistrationNumberField;
            }
            set
            {
                this.wishedRegistrationNumberField = value;
            }
        }
        public bool? AgreementToReceiveERefusal
        {
            get
            {
                return this.agreementToReceiveERefusalField;
            }
            set
            {
                this.agreementToReceiveERefusalField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AgreementToReceiveERefusalSpecified
        {
            get
            {
                return AgreementToReceiveERefusal.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3335")]
    public partial class NotificationForTemporaryRegistrationPlatesOfficial
    {
        private object itemField;
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3340")]
    public partial class ReportForVehicleEUCARISData
    {
        private bool? canUseCertificateForRegistrationField;
        private List<Status> statusesField;
        private string colorCodeField;
        private string colorNameField;
        private string additionalInfoField;
        public bool? CanUseCertificateForRegistration
        {
            get
            {
                return this.canUseCertificateForRegistrationField;
            }
            set
            {
                this.canUseCertificateForRegistrationField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CanUseCertificateForRegistrationSpecified
        {
            get
            {
                return CanUseCertificateForRegistration.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("Statuses")]
        public List<Status> Statuses
        {
            get
            {
                return this.statusesField;
            }
            set
            {
                this.statusesField = value;
            }
        }
        public string ColorCode
        {
            get
            {
                return this.colorCodeField;
            }
            set
            {
                this.colorCodeField = value;
            }
        }
        public string ColorName
        {
            get
            {
                return this.colorNameField;
            }
            set
            {
                this.colorNameField = value;
            }
        }
        public string AdditionalInfo
        {
            get
            {
                return this.additionalInfoField;
            }
            set
            {
                this.additionalInfoField = value;
            }
        }
    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3056")]
    public partial class ApplicationForIssuingDriverLicenseData
    {
        private List<IdentityDocumentType> identificationDocumentsField;
        private EAU.Documents.Domain.Models.PersonData personField;
        private bool isBulgarianCitizenField;
        private PersonIdentificationForeignStatut? foreignStatutField;
        private TravelDocument travelDocumentField;
        private Citizenship foreignCitizenshipField;
        private string phoneField;
        private string personFamilyField;
        private string otherNamesField;
        private PersonAddress addressField;
        private bool? hasDocumentForDisabilitiesField;
        private string serviceCodeField;
        [System.Xml.Serialization.XmlArrayItemAttribute("IdentificationDocumentType", IsNullable = false)]
        public List<IdentityDocumentType> IdentificationDocuments
        {
            get
            {
                return this.identificationDocumentsField;
            }
            set
            {
                this.identificationDocumentsField = value;
            }
        }
        public EAU.Documents.Domain.Models.PersonData Person
        {
            get
            {
                return this.personField;
            }
            set
            {
                this.personField = value;
            }
        }
        public bool IsBulgarianCitizen
        {
            get
            {
                return this.isBulgarianCitizenField;
            }
            set
            {
                this.isBulgarianCitizenField = value;
            }
        }
        public PersonIdentificationForeignStatut? ForeignStatut
        {
            get
            {
                return this.foreignStatutField;
            }
            set
            {
                this.foreignStatutField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ForeignStatutSpecified
        {
            get
            {
                return ForeignStatut.HasValue;
            }
        }
        public TravelDocument TravelDocument
        {
            get
            {
                return this.travelDocumentField;
            }
            set
            {
                this.travelDocumentField = value;
            }
        }
        public Citizenship ForeignCitizenship
        {
            get
            {
                return this.foreignCitizenshipField;
            }
            set
            {
                this.foreignCitizenshipField = value;
            }
        }
        public string Phone
        {
            get
            {
                return this.phoneField;
            }
            set
            {
                this.phoneField = value;
            }
        }
        public string PersonFamily
        {
            get
            {
                return this.personFamilyField;
            }
            set
            {
                this.personFamilyField = value;
            }
        }
        public string OtherNames
        {
            get
            {
                return this.otherNamesField;
            }
            set
            {
                this.otherNamesField = value;
            }
        }
        public PersonAddress Address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }
        public bool? HasDocumentForDisabilities
        {
            get
            {
                return this.hasDocumentForDisabilitiesField;
            }
            set
            {
                this.hasDocumentForDisabilitiesField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool HasDocumentForDisabilitiesSpecified
        {
            get
            {
                return HasDocumentForDisabilities.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string ServiceCode
        {
            get
            {
                return this.serviceCodeField;
            }
            set
            {
                this.serviceCodeField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3055")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3055", IsNullable = false)]
    public partial class ApplicationForIssuingDriverLicense : IApplicationForm
    {
        private ElectronicAdministrativeServiceHeader electronicAdministrativeServiceHeaderField;
        private ServiceTermType? serviceTermTypeField;
        private ServiceApplicantReceiptData serviceApplicantReceiptDataField;
        private IdentificationPhotoAndSignature identificationPhotoAndSignatureField;
        private ApplicationForIssuingDriverLicenseData applicationForIssuingDriverLicenseDataField;
        private PoliceDepartment issuingPoliceDepartmentField;
        private BIDPersonalIdentificationDocumentReceivePlace? receivePlaceField;
        private List<Declaration> declarationsField;
        private List<AttachedDocument> attachedDocumentsField;
        private ElectronicAdministrativeServiceFooter electronicAdministrativeServiceFooterField;
        public ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader
        {
            get
            {
                return this.electronicAdministrativeServiceHeaderField;
            }
            set
            {
                this.electronicAdministrativeServiceHeaderField = value;
            }
        }
        public ServiceTermType? ServiceTermType
        {
            get
            {
                return this.serviceTermTypeField;
            }
            set
            {
                this.serviceTermTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ServiceTermTypeSpecified
        {
            get
            {
                return ServiceTermType.HasValue;
            }
        }
        public ServiceApplicantReceiptData ServiceApplicantReceiptData
        {
            get
            {
                return this.serviceApplicantReceiptDataField;
            }
            set
            {
                this.serviceApplicantReceiptDataField = value;
            }
        }
        public IdentificationPhotoAndSignature IdentificationPhotoAndSignature
        {
            get
            {
                return this.identificationPhotoAndSignatureField;
            }
            set
            {
                this.identificationPhotoAndSignatureField = value;
            }
        }
        public ApplicationForIssuingDriverLicenseData ApplicationForIssuingDriverLicenseData
        {
            get
            {
                return this.applicationForIssuingDriverLicenseDataField;
            }
            set
            {
                this.applicationForIssuingDriverLicenseDataField = value;
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
        public BIDPersonalIdentificationDocumentReceivePlace? ReceivePlace
        {
            get
            {
                return this.receivePlaceField;
            }
            set
            {
                this.receivePlaceField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ReceivePlaceSpecified
        {
            get
            {
                return ReceivePlace.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<Declaration> Declarations
        {
            get
            {
                return this.declarationsField;
            }
            set
            {
                this.declarationsField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<AttachedDocument> AttachedDocuments
        {
            get
            {
                return this.attachedDocumentsField;
            }
            set
            {
                this.attachedDocumentsField = value;
            }
        }
        public ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter
        {
            get
            {
                return this.electronicAdministrativeServiceFooterField;
            }
            set
            {
                this.electronicAdministrativeServiceFooterField = value;
            }
        }
    }
}
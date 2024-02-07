using EAU.Documents.Domain.Models;
using System.Collections.Generic;

namespace EAU.COD.Documents.Domain.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/value/R-2120")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/value/R-2120", IsNullable = false)]
    public partial class TerritorialScopeOfServices
    {
        private ScopeOfCertification? scopeOfCertificationField;
        private List<TerritorialScopeOfServicesDistricts> districtsField;
        public ScopeOfCertification? ScopeOfCertification
        {
            get
            {
                return this.scopeOfCertificationField;
            }
            set
            {
                this.scopeOfCertificationField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ScopeOfCertificationSpecified
        {
            get
            {
                return ScopeOfCertification.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("Districts")]
        public List<TerritorialScopeOfServicesDistricts> Districts
        {
            get
            {
                return this.districtsField;
            }
            set
            {
                this.districtsField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/value/R-2120")]
    public partial class TerritorialScopeOfServicesDistricts
    {
        private string districtGRAOCodeField;
        private string districtGRAONameField;
        public string DistrictGRAOCode
        {
            get
            {
                return this.districtGRAOCodeField;
            }
            set
            {
                this.districtGRAOCodeField = value;
            }
        }
        public string DistrictGRAOName
        {
            get
            {
                return this.districtGRAONameField;
            }
            set
            {
                this.districtGRAONameField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3109")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://ereg.egov.bg/segment/R-3109", IsNullable = false)]
    public partial class RequestForIssuingLicenseForPrivateSecurityServicesData
    {
        private EntityAddress entityManagementAddressField;
        private EntityAddress correspondingAddressField;
        private string workPhoneField;
        private string mobilePhoneField;
        private List<RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypes> securityServiceTypesField;
        private bool? agreementToReceiveERefusalField;
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
        public string WorkPhone
        {
            get
            {
                return this.workPhoneField;
            }
            set
            {
                this.workPhoneField = value;
            }
        }
        public string MobilePhone
        {
            get
            {
                return this.mobilePhoneField;
            }
            set
            {
                this.mobilePhoneField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute("SecurityServiceTypes")]
        public List<RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypes> SecurityServiceTypes
        {
            get
            {
                return this.securityServiceTypesField;
            }
            set
            {
                this.securityServiceTypesField = value;
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://ereg.egov.bg/segment/R-3109")]
    public partial class RequestForIssuingLicenseForPrivateSecurityServicesDataSecurityServiceTypes
    {
        private PointOfPrivateSecurityServicesLaw? pointOfPrivateSecurityServicesLawField;
        private TerritorialScopeOfServices territorialScopeOfServicesField;
        public PointOfPrivateSecurityServicesLaw? PointOfPrivateSecurityServicesLaw
        {
            get
            {
                return this.pointOfPrivateSecurityServicesLawField;
            }
            set
            {
                this.pointOfPrivateSecurityServicesLawField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PointOfPrivateSecurityServicesLawSpecified
        {
            get
            {
                return PointOfPrivateSecurityServicesLaw.HasValue;
            }
        }
        public TerritorialScopeOfServices TerritorialScopeOfServices
        {
            get
            {
                return this.territorialScopeOfServicesField;
            }
            set
            {
                this.territorialScopeOfServicesField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3151")]
    public partial class NotificationForConcludingOrTerminatingEmploymentContractData
    {
        private PoliceDepartment issuingPoliceDepartmentField;
        private NotificationOfEmploymentContractType? notificationOfEmploymentContractTypeField;
        private List<NewEmployeeRequest> newEmployeeRequestsField;
        private List<RemoveEmployeeRequest> removeEmployeeRequestsField;
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
        public NotificationOfEmploymentContractType? NotificationOfEmploymentContractType
        {
            get
            {
                return this.notificationOfEmploymentContractTypeField;
            }
            set
            {
                this.notificationOfEmploymentContractTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NotificationOfEmploymentContractTypeSpecified
        {
            get
            {
                return NotificationOfEmploymentContractType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<NewEmployeeRequest> NewEmployeeRequests
        {
            get
            {
                return this.newEmployeeRequestsField;
            }
            set
            {
                this.newEmployeeRequestsField = value;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<RemoveEmployeeRequest> RemoveEmployeeRequests
        {
            get
            {
                return this.removeEmployeeRequestsField;
            }
            set
            {
                this.removeEmployeeRequestsField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3153")]
    public partial class NewEmployeeRequest
    {
        private Employee employeeField;
        private string contractNumberField;
        private System.DateTime? contractDateField;
        private ContractType? contractTypeField;
        private string contractPeriodInMonthsField;
        public Employee Employee
        {
            get
            {
                return this.employeeField;
            }
            set
            {
                this.employeeField = value;
            }
        }
        public string ContractNumber
        {
            get
            {
                return this.contractNumberField;
            }
            set
            {
                this.contractNumberField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ContractDate
        {
            get
            {
                return this.contractDateField;
            }
            set
            {
                this.contractDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ContractDateSpecified
        {
            get
            {
                return ContractDate.HasValue;
            }
        }
        public ContractType? ContractType
        {
            get
            {
                return this.contractTypeField;
            }
            set
            {
                this.contractTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ContractTypeSpecified
        {
            get
            {
                return ContractType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "integer")]
        public string ContractPeriodInMonths
        {
            get
            {
                return this.contractPeriodInMonthsField;
            }
            set
            {
                this.contractPeriodInMonthsField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3152")]
    public partial class Employee
    {
        private string fullNameField;
        private string identifierField;
        private EmployeeIdentifierType? employeeIdentifierTypeField;
        private string aISCHODEmployeeIDField;
        private EmployeeCitizenshipType? citizenshipField;
        private string securityObjectField;
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
        public EmployeeIdentifierType? EmployeeIdentifierType
        {
            get
            {
                return this.employeeIdentifierTypeField;
            }
            set
            {
                this.employeeIdentifierTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool EmployeeIdentifierTypeSpecified
        {
            get
            {
                return EmployeeIdentifierType.HasValue;
            }
        }
        public string AISCHODEmployeeID
        {
            get
            {
                return this.aISCHODEmployeeIDField;
            }
            set
            {
                this.aISCHODEmployeeIDField = value;
            }
        }
        public EmployeeCitizenshipType? Citizenship
        {
            get
            {
                return this.citizenshipField;
            }
            set
            {
                this.citizenshipField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CitizenshipSpecified
        {
            get
            {
                return Citizenship.HasValue;
            }
        }
        public string SecurityObject
        {
            get
            {
                return this.securityObjectField;
            }
            set
            {
                this.securityObjectField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3154")]
    public partial class RemoveEmployeeRequest
    {
        private Employee employeeField;
        private string contractTerminationNumberField;
        private System.DateTime? contractTerminationDateField;
        private System.DateTime? contractTerminationEffectiveDateField;
        private string contractTerminationNoteField;
        public Employee Employee
        {
            get
            {
                return this.employeeField;
            }
            set
            {
                this.employeeField = value;
            }
        }
        public string ContractTerminationNumber
        {
            get
            {
                return this.contractTerminationNumberField;
            }
            set
            {
                this.contractTerminationNumberField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ContractTerminationDate
        {
            get
            {
                return this.contractTerminationDateField;
            }
            set
            {
                this.contractTerminationDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ContractTerminationDateSpecified
        {
            get
            {
                return ContractTerminationDate.HasValue;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ContractTerminationEffectiveDate
        {
            get
            {
                return this.contractTerminationEffectiveDateField;
            }
            set
            {
                this.contractTerminationEffectiveDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ContractTerminationEffectiveDateSpecified
        {
            get
            {
                return ContractTerminationEffectiveDate.HasValue;
            }
        }
        public string ContractTerminationNote
        {
            get
            {
                return this.contractTerminationNoteField;
            }
            set
            {
                this.contractTerminationNoteField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3162")]
    public partial class SecurityObjectsData
    {
        private List<SecurityObject> securityObjectsField;
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<SecurityObject> SecurityObjects
        {
            get
            {
                return this.securityObjectsField;
            }
            set
            {
                this.securityObjectsField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3176")]
    public partial class SecurityObject
    {
        private PointOfPrivateSecurityServicesLaw? pointOfPrivateSecurityServicesLawField;
        private PersonalSecurity personalSecurityField;
        private ProtectionPersonsProperty protectionPersonsPropertyField;
        private AlarmAndSecurityActivity alarmAndSecurityActivityField;
        private SelfProtectionPersonsProperty selfProtectionPersonsPropertyField;
        private SecurityOfSitesRealEstate securityOfSitesRealEstateField;
        private SecurityOfEvents securityOfEventsField;
        private SecurityTransportingCargo securityTransportingCargoField;
        private ProtectionOfAgriculturalProperty protectionOfAgriculturalPropertyField;
        public PointOfPrivateSecurityServicesLaw? PointOfPrivateSecurityServicesLaw
        {
            get
            {
                return this.pointOfPrivateSecurityServicesLawField;
            }
            set
            {
                this.pointOfPrivateSecurityServicesLawField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool PointOfPrivateSecurityServicesLawSpecified
        {
            get
            {
                return PointOfPrivateSecurityServicesLaw.HasValue;
            }
        }
        public PersonalSecurity PersonalSecurity
        {
            get
            {
                return this.personalSecurityField;
            }
            set
            {
                this.personalSecurityField = value;
            }
        }
        public ProtectionPersonsProperty ProtectionPersonsProperty
        {
            get
            {
                return this.protectionPersonsPropertyField;
            }
            set
            {
                this.protectionPersonsPropertyField = value;
            }
        }
        public AlarmAndSecurityActivity AlarmAndSecurityActivity
        {
            get
            {
                return this.alarmAndSecurityActivityField;
            }
            set
            {
                this.alarmAndSecurityActivityField = value;
            }
        }
        public SelfProtectionPersonsProperty SelfProtectionPersonsProperty
        {
            get
            {
                return this.selfProtectionPersonsPropertyField;
            }
            set
            {
                this.selfProtectionPersonsPropertyField = value;
            }
        }
        public SecurityOfSitesRealEstate SecurityOfSitesRealEstate
        {
            get
            {
                return this.securityOfSitesRealEstateField;
            }
            set
            {
                this.securityOfSitesRealEstateField = value;
            }
        }
        public SecurityOfEvents SecurityOfEvents
        {
            get
            {
                return this.securityOfEventsField;
            }
            set
            {
                this.securityOfEventsField = value;
            }
        }
        public SecurityTransportingCargo SecurityTransportingCargo
        {
            get
            {
                return this.securityTransportingCargoField;
            }
            set
            {
                this.securityTransportingCargoField = value;
            }
        }
        public ProtectionOfAgriculturalProperty ProtectionOfAgriculturalProperty
        {
            get
            {
                return this.protectionOfAgriculturalPropertyField;
            }
            set
            {
                this.protectionOfAgriculturalPropertyField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3170")]
    public partial class AlarmAndSecurityActivity
    {
        private System.DateTime? actualDateField;
        private string securityObjectNameField;
        private string districtCodeField;
        private string districtNameField;
        private string addressField;
        private SecurityType? securityTypeField;
        private List<SecurityTransport> securityTransportsField;
        private System.DateTime? terminationDateField;
        private string aISCHODDistrictIdField;
        private string aISCHODDistrictNameField;
        private string contractTypeNumberDateField;
        private string contractTerminationNoteField;
        private string aISCHODObjectIDField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ActualDate
        {
            get
            {
                return this.actualDateField;
            }
            set
            {
                this.actualDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualDateSpecified
        {
            get
            {
                return ActualDate.HasValue;
            }
        }
        public string SecurityObjectName
        {
            get
            {
                return this.securityObjectNameField;
            }
            set
            {
                this.securityObjectNameField = value;
            }
        }
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
        public string Address
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
        public SecurityType? SecurityType
        {
            get
            {
                return this.securityTypeField;
            }
            set
            {
                this.securityTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SecurityTypeSpecified
        {
            get
            {
                return SecurityType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<SecurityTransport> SecurityTransports
        {
            get
            {
                return this.securityTransportsField;
            }
            set
            {
                this.securityTransportsField = value;
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
        public string AISCHODDistrictId
        {
            get
            {
                return this.aISCHODDistrictIdField;
            }
            set
            {
                this.aISCHODDistrictIdField = value;
            }
        }
        public string AISCHODDistrictName
        {
            get
            {
                return this.aISCHODDistrictNameField;
            }
            set
            {
                this.aISCHODDistrictNameField = value;
            }
        }
        public string ContractTypeNumberDate
        {
            get
            {
                return this.contractTypeNumberDateField;
            }
            set
            {
                this.contractTypeNumberDateField = value;
            }
        }
        public string ContractTerminationNote
        {
            get
            {
                return this.contractTerminationNoteField;
            }
            set
            {
                this.contractTerminationNoteField = value;
            }
        }
        public string AISCHODObjectID
        {
            get
            {
                return this.aISCHODObjectIDField;
            }
            set
            {
                this.aISCHODObjectIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3167")]
    public partial class PersonalSecurity
    {
        private System.DateTime? actualDateField;
        private GuardedPersonType? guardedPersonTypeField;
        private PersonAssignorData guardedPersonField;
        private string positionField;
        private string placeOfWorkField;
        private string addressField;
        private SecurityType? securityTypeField;
        private ClothintType? clothintTypeField;
        private List<SecurityTransport> securityTransportsField;
        private System.DateTime? terminationDateField;
        private string aISCHODDistrictIdField;
        private string aISCHODDistrictNameField;
        private string contractTypeNumberDateField;
        private string contractTerminationNoteField;
        private string aISCHODObjectIDField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ActualDate
        {
            get
            {
                return this.actualDateField;
            }
            set
            {
                this.actualDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualDateSpecified
        {
            get
            {
                return ActualDate.HasValue;
            }
        }
        public GuardedPersonType? GuardedPersonType
        {
            get
            {
                return this.guardedPersonTypeField;
            }
            set
            {
                this.guardedPersonTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool GuardedPersonTypeSpecified
        {
            get
            {
                return GuardedPersonType.HasValue;
            }
        }
        public PersonAssignorData GuardedPerson
        {
            get
            {
                return this.guardedPersonField;
            }
            set
            {
                this.guardedPersonField = value;
            }
        }
        public string Position
        {
            get
            {
                return this.positionField;
            }
            set
            {
                this.positionField = value;
            }
        }
        public string PlaceOfWork
        {
            get
            {
                return this.placeOfWorkField;
            }
            set
            {
                this.placeOfWorkField = value;
            }
        }
        public string Address
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
        public SecurityType? SecurityType
        {
            get
            {
                return this.securityTypeField;
            }
            set
            {
                this.securityTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SecurityTypeSpecified
        {
            get
            {
                return SecurityType.HasValue;
            }
        }
        public ClothintType? ClothintType
        {
            get
            {
                return this.clothintTypeField;
            }
            set
            {
                this.clothintTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ClothintTypeSpecified
        {
            get
            {
                return ClothintType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<SecurityTransport> SecurityTransports
        {
            get
            {
                return this.securityTransportsField;
            }
            set
            {
                this.securityTransportsField = value;
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
        public string AISCHODDistrictId
        {
            get
            {
                return this.aISCHODDistrictIdField;
            }
            set
            {
                this.aISCHODDistrictIdField = value;
            }
        }
        public string AISCHODDistrictName
        {
            get
            {
                return this.aISCHODDistrictNameField;
            }
            set
            {
                this.aISCHODDistrictNameField = value;
            }
        }
        public string ContractTypeNumberDate
        {
            get
            {
                return this.contractTypeNumberDateField;
            }
            set
            {
                this.contractTypeNumberDateField = value;
            }
        }
        public string ContractTerminationNote
        {
            get
            {
                return this.contractTerminationNoteField;
            }
            set
            {
                this.contractTerminationNoteField = value;
            }
        }
        public string AISCHODObjectID
        {
            get
            {
                return this.aISCHODObjectIDField;
            }
            set
            {
                this.aISCHODObjectIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3175")]
    public partial class ProtectionOfAgriculturalProperty
    {
        private System.DateTime? actualDateField;
        private string securityObjectNameField;
        private string districtCodeField;
        private string districtNameField;
        private string addressField;
        private SecurityType? securityTypeField;
        private List<SecurityTransport> securityTransportsField;
        private System.DateTime? terminationDateField;
        private string aISCHODDistrictIdField;
        private string aISCHODDistrictNameField;
        private string contractTypeNumberDateField;
        private string contractTerminationNoteField;
        private string aISCHODObjectIDField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ActualDate
        {
            get
            {
                return this.actualDateField;
            }
            set
            {
                this.actualDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualDateSpecified
        {
            get
            {
                return ActualDate.HasValue;
            }
        }
        public string SecurityObjectName
        {
            get
            {
                return this.securityObjectNameField;
            }
            set
            {
                this.securityObjectNameField = value;
            }
        }
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
        public string Address
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
        public SecurityType? SecurityType
        {
            get
            {
                return this.securityTypeField;
            }
            set
            {
                this.securityTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SecurityTypeSpecified
        {
            get
            {
                return SecurityType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<SecurityTransport> SecurityTransports
        {
            get
            {
                return this.securityTransportsField;
            }
            set
            {
                this.securityTransportsField = value;
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
        public string AISCHODDistrictId
        {
            get
            {
                return this.aISCHODDistrictIdField;
            }
            set
            {
                this.aISCHODDistrictIdField = value;
            }
        }
        public string AISCHODDistrictName
        {
            get
            {
                return this.aISCHODDistrictNameField;
            }
            set
            {
                this.aISCHODDistrictNameField = value;
            }
        }
        public string ContractTypeNumberDate
        {
            get
            {
                return this.contractTypeNumberDateField;
            }
            set
            {
                this.contractTypeNumberDateField = value;
            }
        }
        public string ContractTerminationNote
        {
            get
            {
                return this.contractTerminationNoteField;
            }
            set
            {
                this.contractTerminationNoteField = value;
            }
        }
        public string AISCHODObjectID
        {
            get
            {
                return this.aISCHODObjectIDField;
            }
            set
            {
                this.aISCHODObjectIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3169")]
    public partial class ProtectionPersonsProperty
    {
        private System.DateTime? actualDateField;
        private string securityObjectNameField;
        private string districtCodeField;
        private string districtNameField;
        private string addressField;
        private SecurityType? securityTypeField;
        private List<SecurityTransport> securityTransportsField;
        private AccessRegimeType? accessRegimeTypeField;
        private ControlType? controlTypeField;
        private System.DateTime? terminationDateField;
        private string aISCHODDistrictIdField;
        private string aISCHODDistrictNameField;
        private string aISCHODAccessRegimeTypeIdField;
        private string aISCHODAccessRegimeTypeNameField;
        private string aISCHODControlTypeIdField;
        private string aISCHODControlTypeNameField;
        private string contractTypeNumberDateField;
        private string contractTerminationNoteField;
        private string aISCHODObjectIDField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ActualDate
        {
            get
            {
                return this.actualDateField;
            }
            set
            {
                this.actualDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualDateSpecified
        {
            get
            {
                return ActualDate.HasValue;
            }
        }
        public string SecurityObjectName
        {
            get
            {
                return this.securityObjectNameField;
            }
            set
            {
                this.securityObjectNameField = value;
            }
        }
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
        public string Address
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
        public SecurityType? SecurityType
        {
            get
            {
                return this.securityTypeField;
            }
            set
            {
                this.securityTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SecurityTypeSpecified
        {
            get
            {
                return SecurityType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<SecurityTransport> SecurityTransports
        {
            get
            {
                return this.securityTransportsField;
            }
            set
            {
                this.securityTransportsField = value;
            }
        }
        public AccessRegimeType? AccessRegimeType
        {
            get
            {
                return this.accessRegimeTypeField;
            }
            set
            {
                this.accessRegimeTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AccessRegimeTypeSpecified
        {
            get
            {
                return AccessRegimeType.HasValue;
            }
        }
        public ControlType? ControlType
        {
            get
            {
                return this.controlTypeField;
            }
            set
            {
                this.controlTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ControlTypeSpecified
        {
            get
            {
                return ControlType.HasValue;
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
        public string AISCHODDistrictId
        {
            get
            {
                return this.aISCHODDistrictIdField;
            }
            set
            {
                this.aISCHODDistrictIdField = value;
            }
        }
        public string AISCHODDistrictName
        {
            get
            {
                return this.aISCHODDistrictNameField;
            }
            set
            {
                this.aISCHODDistrictNameField = value;
            }
        }
        public string AISCHODAccessRegimeTypeId
        {
            get
            {
                return this.aISCHODAccessRegimeTypeIdField;
            }
            set
            {
                this.aISCHODAccessRegimeTypeIdField = value;
            }
        }
        public string AISCHODAccessRegimeTypeName
        {
            get
            {
                return this.aISCHODAccessRegimeTypeNameField;
            }
            set
            {
                this.aISCHODAccessRegimeTypeNameField = value;
            }
        }
        public string AISCHODControlTypeId
        {
            get
            {
                return this.aISCHODControlTypeIdField;
            }
            set
            {
                this.aISCHODControlTypeIdField = value;
            }
        }
        public string AISCHODControlTypeName
        {
            get
            {
                return this.aISCHODControlTypeNameField;
            }
            set
            {
                this.aISCHODControlTypeNameField = value;
            }
        }
        public string ContractTypeNumberDate
        {
            get
            {
                return this.contractTypeNumberDateField;
            }
            set
            {
                this.contractTypeNumberDateField = value;
            }
        }
        public string ContractTerminationNote
        {
            get
            {
                return this.contractTerminationNoteField;
            }
            set
            {
                this.contractTerminationNoteField = value;
            }
        }
        public string AISCHODObjectID
        {
            get
            {
                return this.aISCHODObjectIDField;
            }
            set
            {
                this.aISCHODObjectIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3173")]
    public partial class SecurityOfEvents
    {
        private System.DateTime? actualDateField;
        private string securityObjectNameField;
        private string districtCodeField;
        private string districtNameField;
        private string addressField;
        private SecurityType? securityTypeField;
        private List<SecurityTransport> securityTransportsField;
        private AccessRegimeType? accessRegimeTypeField;
        private ControlType? controlTypeField;
        private System.DateTime? terminationDateField;
        private string aISCHODDistrictIdField;
        private string aISCHODDistrictNameField;
        private string aISCHODAccessRegimeTypeIdField;
        private string aISCHODAccessRegimeTypeNameField;
        private string aISCHODControlTypeIdField;
        private string aISCHODControlTypeNameField;
        private string contractTypeNumberDateField;
        private string contractTerminationNoteField;
        private string aISCHODObjectIDField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ActualDate
        {
            get
            {
                return this.actualDateField;
            }
            set
            {
                this.actualDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualDateSpecified
        {
            get
            {
                return ActualDate.HasValue;
            }
        }
        public string SecurityObjectName
        {
            get
            {
                return this.securityObjectNameField;
            }
            set
            {
                this.securityObjectNameField = value;
            }
        }
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
        public string Address
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
        public SecurityType? SecurityType
        {
            get
            {
                return this.securityTypeField;
            }
            set
            {
                this.securityTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SecurityTypeSpecified
        {
            get
            {
                return SecurityType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<SecurityTransport> SecurityTransports
        {
            get
            {
                return this.securityTransportsField;
            }
            set
            {
                this.securityTransportsField = value;
            }
        }
        public AccessRegimeType? AccessRegimeType
        {
            get
            {
                return this.accessRegimeTypeField;
            }
            set
            {
                this.accessRegimeTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AccessRegimeTypeSpecified
        {
            get
            {
                return AccessRegimeType.HasValue;
            }
        }
        public ControlType? ControlType
        {
            get
            {
                return this.controlTypeField;
            }
            set
            {
                this.controlTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ControlTypeSpecified
        {
            get
            {
                return ControlType.HasValue;
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
        public string AISCHODDistrictId
        {
            get
            {
                return this.aISCHODDistrictIdField;
            }
            set
            {
                this.aISCHODDistrictIdField = value;
            }
        }
        public string AISCHODDistrictName
        {
            get
            {
                return this.aISCHODDistrictNameField;
            }
            set
            {
                this.aISCHODDistrictNameField = value;
            }
        }
        public string AISCHODAccessRegimeTypeId
        {
            get
            {
                return this.aISCHODAccessRegimeTypeIdField;
            }
            set
            {
                this.aISCHODAccessRegimeTypeIdField = value;
            }
        }
        public string AISCHODAccessRegimeTypeName
        {
            get
            {
                return this.aISCHODAccessRegimeTypeNameField;
            }
            set
            {
                this.aISCHODAccessRegimeTypeNameField = value;
            }
        }
        public string AISCHODControlTypeId
        {
            get
            {
                return this.aISCHODControlTypeIdField;
            }
            set
            {
                this.aISCHODControlTypeIdField = value;
            }
        }
        public string AISCHODControlTypeName
        {
            get
            {
                return this.aISCHODControlTypeNameField;
            }
            set
            {
                this.aISCHODControlTypeNameField = value;
            }
        }
        public string ContractTypeNumberDate
        {
            get
            {
                return this.contractTypeNumberDateField;
            }
            set
            {
                this.contractTypeNumberDateField = value;
            }
        }
        public string ContractTerminationNote
        {
            get
            {
                return this.contractTerminationNoteField;
            }
            set
            {
                this.contractTerminationNoteField = value;
            }
        }
        public string AISCHODObjectID
        {
            get
            {
                return this.aISCHODObjectIDField;
            }
            set
            {
                this.aISCHODObjectIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3172")]
    public partial class SecurityOfSitesRealEstate
    {
        private System.DateTime? actualDateField;
        private string securityObjectNameField;
        private string districtCodeField;
        private string districtNameField;
        private string addressField;
        private SecurityType? securityTypeField;
        private List<SecurityTransport> securityTransportsField;
        private AccessRegimeType? accessRegimeTypeField;
        private ControlType? controlTypeField;
        private System.DateTime? terminationDateField;
        private string aISCHODDistrictIdField;
        private string aISCHODDistrictNameField;
        private string aISCHODAccessRegimeTypeIdField;
        private string aISCHODAccessRegimeTypeNameField;
        private string aISCHODControlTypeIdField;
        private string aISCHODControlTypeNameField;
        private string contractTypeNumberDateField;
        private string contractTerminationNoteField;
        private string aISCHODObjectIDField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ActualDate
        {
            get
            {
                return this.actualDateField;
            }
            set
            {
                this.actualDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualDateSpecified
        {
            get
            {
                return ActualDate.HasValue;
            }
        }
        public string SecurityObjectName
        {
            get
            {
                return this.securityObjectNameField;
            }
            set
            {
                this.securityObjectNameField = value;
            }
        }
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
        public string Address
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
        public SecurityType? SecurityType
        {
            get
            {
                return this.securityTypeField;
            }
            set
            {
                this.securityTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SecurityTypeSpecified
        {
            get
            {
                return SecurityType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<SecurityTransport> SecurityTransports
        {
            get
            {
                return this.securityTransportsField;
            }
            set
            {
                this.securityTransportsField = value;
            }
        }
        public AccessRegimeType? AccessRegimeType
        {
            get
            {
                return this.accessRegimeTypeField;
            }
            set
            {
                this.accessRegimeTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AccessRegimeTypeSpecified
        {
            get
            {
                return AccessRegimeType.HasValue;
            }
        }
        public ControlType? ControlType
        {
            get
            {
                return this.controlTypeField;
            }
            set
            {
                this.controlTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ControlTypeSpecified
        {
            get
            {
                return ControlType.HasValue;
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
        public string AISCHODDistrictId
        {
            get
            {
                return this.aISCHODDistrictIdField;
            }
            set
            {
                this.aISCHODDistrictIdField = value;
            }
        }
        public string AISCHODDistrictName
        {
            get
            {
                return this.aISCHODDistrictNameField;
            }
            set
            {
                this.aISCHODDistrictNameField = value;
            }
        }
        public string AISCHODAccessRegimeTypeId
        {
            get
            {
                return this.aISCHODAccessRegimeTypeIdField;
            }
            set
            {
                this.aISCHODAccessRegimeTypeIdField = value;
            }
        }
        public string AISCHODAccessRegimeTypeName
        {
            get
            {
                return this.aISCHODAccessRegimeTypeNameField;
            }
            set
            {
                this.aISCHODAccessRegimeTypeNameField = value;
            }
        }
        public string AISCHODControlTypeId
        {
            get
            {
                return this.aISCHODControlTypeIdField;
            }
            set
            {
                this.aISCHODControlTypeIdField = value;
            }
        }
        public string AISCHODControlTypeName
        {
            get
            {
                return this.aISCHODControlTypeNameField;
            }
            set
            {
                this.aISCHODControlTypeNameField = value;
            }
        }
        public string ContractTypeNumberDate
        {
            get
            {
                return this.contractTypeNumberDateField;
            }
            set
            {
                this.contractTypeNumberDateField = value;
            }
        }
        public string ContractTerminationNote
        {
            get
            {
                return this.contractTerminationNoteField;
            }
            set
            {
                this.contractTerminationNoteField = value;
            }
        }
        public string AISCHODObjectID
        {
            get
            {
                return this.aISCHODObjectIDField;
            }
            set
            {
                this.aISCHODObjectIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3171")]
    public partial class SelfProtectionPersonsProperty
    {
        private System.DateTime? actualDateField;
        private string securityObjectNameField;
        private string addressField;
        private SecurityType? securityTypeField;
        private bool? hasTransportField;
        private List<SecurityTransport> securityTransportsField;
        private AccessRegimeType? accessRegimeTypeField;
        private ControlType? controlTypeField;
        private System.DateTime? terminationDateField;
        private string aISCHODDistrictIdField;
        private string aISCHODDistrictNameField;
        private string aISCHODAccessRegimeTypeIdField;
        private string aISCHODAccessRegimeTypeNameField;
        private string aISCHODControlTypeIdField;
        private string aISCHODControlTypeNameField;
        private string contractTypeNumberDateField;
        private string contractTerminationNoteField;
        private string aISCHODObjectIDField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ActualDate
        {
            get
            {
                return this.actualDateField;
            }
            set
            {
                this.actualDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualDateSpecified
        {
            get
            {
                return ActualDate.HasValue;
            }
        }
        public string SecurityObjectName
        {
            get
            {
                return this.securityObjectNameField;
            }
            set
            {
                this.securityObjectNameField = value;
            }
        }
        public string Address
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
        public SecurityType? SecurityType
        {
            get
            {
                return this.securityTypeField;
            }
            set
            {
                this.securityTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SecurityTypeSpecified
        {
            get
            {
                return SecurityType.HasValue;
            }
        }
        public bool? HasTransport
        {
            get
            {
                return this.hasTransportField;
            }
            set
            {
                this.hasTransportField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool HasTransportSpecified
        {
            get
            {
                return HasTransport.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<SecurityTransport> SecurityTransports
        {
            get
            {
                return this.securityTransportsField;
            }
            set
            {
                this.securityTransportsField = value;
            }
        }
        public AccessRegimeType? AccessRegimeType
        {
            get
            {
                return this.accessRegimeTypeField;
            }
            set
            {
                this.accessRegimeTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AccessRegimeTypeSpecified
        {
            get
            {
                return AccessRegimeType.HasValue;
            }
        }
        public ControlType? ControlType
        {
            get
            {
                return this.controlTypeField;
            }
            set
            {
                this.controlTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ControlTypeSpecified
        {
            get
            {
                return ControlType.HasValue;
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
        public string AISCHODDistrictId
        {
            get
            {
                return this.aISCHODDistrictIdField;
            }
            set
            {
                this.aISCHODDistrictIdField = value;
            }
        }
        public string AISCHODDistrictName
        {
            get
            {
                return this.aISCHODDistrictNameField;
            }
            set
            {
                this.aISCHODDistrictNameField = value;
            }
        }
        public string AISCHODAccessRegimeTypeId
        {
            get
            {
                return this.aISCHODAccessRegimeTypeIdField;
            }
            set
            {
                this.aISCHODAccessRegimeTypeIdField = value;
            }
        }
        public string AISCHODAccessRegimeTypeName
        {
            get
            {
                return this.aISCHODAccessRegimeTypeNameField;
            }
            set
            {
                this.aISCHODAccessRegimeTypeNameField = value;
            }
        }
        public string AISCHODControlTypeId
        {
            get
            {
                return this.aISCHODControlTypeIdField;
            }
            set
            {
                this.aISCHODControlTypeIdField = value;
            }
        }
        public string AISCHODControlTypeName
        {
            get
            {
                return this.aISCHODControlTypeNameField;
            }
            set
            {
                this.aISCHODControlTypeNameField = value;
            }
        }
        public string ContractTypeNumberDate
        {
            get
            {
                return this.contractTypeNumberDateField;
            }
            set
            {
                this.contractTypeNumberDateField = value;
            }
        }
        public string ContractTerminationNote
        {
            get
            {
                return this.contractTerminationNoteField;
            }
            set
            {
                this.contractTerminationNoteField = value;
            }
        }
        public string AISCHODObjectID
        {
            get
            {
                return this.aISCHODObjectIDField;
            }
            set
            {
                this.aISCHODObjectIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3174")]
    public partial class SecurityTransportingCargo
    {
        private System.DateTime? actualDateField;
        private SecurityType? securityTypeField;
        private List<SecurityTransport> securityTransportsField;
        private string objectTypesField;
        private string territorialScopeFromField;
        private string territorialScopeToField;
        private System.DateTime? terminationDateField;
        private string contractTypeNumberDateField;
        private string contractTerminationNoteField;
        private string aISCHODObjectIDField;
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ActualDate
        {
            get
            {
                return this.actualDateField;
            }
            set
            {
                this.actualDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ActualDateSpecified
        {
            get
            {
                return ActualDate.HasValue;
            }
        }
        public SecurityType? SecurityType
        {
            get
            {
                return this.securityTypeField;
            }
            set
            {
                this.securityTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool SecurityTypeSpecified
        {
            get
            {
                return SecurityType.HasValue;
            }
        }
        [System.Xml.Serialization.XmlArrayItemAttribute(IsNullable = false)]
        public List<SecurityTransport> SecurityTransports
        {
            get
            {
                return this.securityTransportsField;
            }
            set
            {
                this.securityTransportsField = value;
            }
        }
        public string ObjectTypes
        {
            get
            {
                return this.objectTypesField;
            }
            set
            {
                this.objectTypesField = value;
            }
        }
        public string TerritorialScopeFrom
        {
            get
            {
                return this.territorialScopeFromField;
            }
            set
            {
                this.territorialScopeFromField = value;
            }
        }
        public string TerritorialScopeTo
        {
            get
            {
                return this.territorialScopeToField;
            }
            set
            {
                this.territorialScopeToField = value;
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
        public string ContractTypeNumberDate
        {
            get
            {
                return this.contractTypeNumberDateField;
            }
            set
            {
                this.contractTypeNumberDateField = value;
            }
        }
        public string ContractTerminationNote
        {
            get
            {
                return this.contractTerminationNoteField;
            }
            set
            {
                this.contractTerminationNoteField = value;
            }
        }
        public string AISCHODObjectID
        {
            get
            {
                return this.aISCHODObjectIDField;
            }
            set
            {
                this.aISCHODObjectIDField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3168")]
    public partial class SecurityTransport
    {
        private VehicleOwnershipType? vehicleOwnershipTypeField;
        private string registrationNumberField;
        private string makeAndModelField;
        public VehicleOwnershipType? VehicleOwnershipType
        {
            get
            {
                return this.vehicleOwnershipTypeField;
            }
            set
            {
                this.vehicleOwnershipTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool VehicleOwnershipTypeSpecified
        {
            get
            {
                return VehicleOwnershipType.HasValue;
            }
        }
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3165")]
    public partial class PersonAssignorData
    {
        private string identifierField;
        private string fullNameField;
        private GuardedType? guardedTypeField;
        private PersonAssignorCitizenshipType? citizenshipField;
        private PersonAssignorIdentifierType? identifierTypeField;
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
        public GuardedType? GuardedType
        {
            get
            {
                return this.guardedTypeField;
            }
            set
            {
                this.guardedTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool GuardedTypeSpecified
        {
            get
            {
                return GuardedType.HasValue;
            }
        }
        public PersonAssignorCitizenshipType? Citizenship
        {
            get
            {
                return this.citizenshipField;
            }
            set
            {
                this.citizenshipField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool CitizenshipSpecified
        {
            get
            {
                return Citizenship.HasValue;
            }
        }
        public PersonAssignorIdentifierType? IdentifierType
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool IdentifierTypeSpecified
        {
            get
            {
                return IdentifierType.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3161")]
    public partial class NotificationForTakingOrRemovingFromSecurityData
    {
        private PoliceDepartment issuingPoliceDepartmentField;
        private NotificationType? notificationTypeField;
        private SecurityContractData securityContractDataField;
        private ContractAssignor contractAssignorField;
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
        public NotificationType? NotificationType
        {
            get
            {
                return this.notificationTypeField;
            }
            set
            {
                this.notificationTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool NotificationTypeSpecified
        {
            get
            {
                return NotificationType.HasValue;
            }
        }
        public SecurityContractData SecurityContractData
        {
            get
            {
                return this.securityContractDataField;
            }
            set
            {
                this.securityContractDataField = value;
            }
        }
        public ContractAssignor ContractAssignor
        {
            get
            {
                return this.contractAssignorField;
            }
            set
            {
                this.contractAssignorField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3163")]
    public partial class SecurityContractData
    {
        private string contractNumberField;
        private string documentNumberField;
        private System.DateTime? contractDateField;
        private string contractTypeField;
        private bool? contractIsExpiredField;
        public string ContractNumber
        {
            get
            {
                return this.contractNumberField;
            }
            set
            {
                this.contractNumberField = value;
            }
        }
        public string DocumentNumber
        {
            get
            {
                return this.documentNumberField;
            }
            set
            {
                this.documentNumberField = value;
            }
        }
        [System.Xml.Serialization.XmlElementAttribute(DataType = "date")]
        public System.DateTime? ContractDate
        {
            get
            {
                return this.contractDateField;
            }
            set
            {
                this.contractDateField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ContractDateSpecified
        {
            get
            {
                return ContractDate.HasValue;
            }
        }
        public string ContractType
        {
            get
            {
                return this.contractTypeField;
            }
            set
            {
                this.contractTypeField = value;
            }
        }
        public bool? ContractIsExpired
        {
            get
            {
                return this.contractIsExpiredField;
            }
            set
            {
                this.contractIsExpiredField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ContractIsExpiredSpecified
        {
            get
            {
                return ContractIsExpired.HasValue;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3164")]
    public partial class ContractAssignor
    {
        private AssignorPersonEntityType? assignorPersonEntityTypeField;
        private PersonAssignorData personAssignorDataField;
        private EntityAssignorData entityAssignorDataField;
        public AssignorPersonEntityType? AssignorPersonEntityType
        {
            get
            {
                return this.assignorPersonEntityTypeField;
            }
            set
            {
                this.assignorPersonEntityTypeField = value;
            }
        }
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool AssignorPersonEntityTypeSpecified
        {
            get
            {
                return AssignorPersonEntityType.HasValue;
            }
        }
        public PersonAssignorData PersonAssignorData
        {
            get
            {
                return this.personAssignorDataField;
            }
            set
            {
                this.personAssignorDataField = value;
            }
        }
        public EntityAssignorData EntityAssignorData
        {
            get
            {
                return this.entityAssignorDataField;
            }
            set
            {
                this.entityAssignorDataField = value;
            }
        }
    }

    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.18020")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://ereg.egov.bg/segment/R-3166")]
    public partial class EntityAssignorData
    {
        private string identifierField;
        private string fullNameField;
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
    }
}
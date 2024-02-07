import { observable } from 'mobx';
import { TypeSystem, BaseDataModel } from 'cnsys-core';
import { moduleContext } from '../ModuleContext';
import moment from 'moment';
import {ApplicationFormVMBase, PoliceDepartment, EntityAddress } from 'eau-documents';

export enum PersonAssignorCitizenshipType {
	Bulgarian = 1,
	Foreign = 2,
} 
TypeSystem.registerEnumInfo(PersonAssignorCitizenshipType, 'PersonAssignorCitizenshipType', moduleContext.moduleName); 

export enum EmployeeCitizenshipType {
	Bulgarian = 1,
	Foreign = 2,
} 
TypeSystem.registerEnumInfo(EmployeeCitizenshipType, 'EmployeeCitizenshipType', moduleContext.moduleName); 

export enum PersonAssignorIdentifierType {
	EGN = 1,
	LN = 2,
	Date = 3,
} 
TypeSystem.registerEnumInfo(PersonAssignorIdentifierType, 'PersonAssignorIdentifierType', moduleContext.moduleName); 

export enum PointOfPrivateSecurityServicesLaw {
	PersonalSecurityServicesForPersons = 0,
	PropertySecurityServices = 1,
	AlarmAndSecurityActivity = 2,
	EntityPropertySelfProtection = 3,
	RealEstatSecurity = 4,
	EventsSecurityServices = 5,
	ValuablesAndCargoesSecurityServices = 6,
	AgriculturalAndPropertyProtection = 7,
} 
TypeSystem.registerEnumInfo(PointOfPrivateSecurityServicesLaw, 'PointOfPrivateSecurityServicesLaw', moduleContext.moduleName); 

export enum ScopeOfCertification {
	WholeCountry = 1,
	SelectedDistricts = 2,
} 
TypeSystem.registerEnumInfo(ScopeOfCertification, 'ScopeOfCertification', moduleContext.moduleName); 

export enum NotificationOfEmploymentContractType {
	Concluding = 0,
	Terminating = 1,
} 
TypeSystem.registerEnumInfo(NotificationOfEmploymentContractType, 'NotificationOfEmploymentContractType', moduleContext.moduleName); 

export enum ContractType {
	Unlimited = 0,
	ForPeriod = 1,
} 
TypeSystem.registerEnumInfo(ContractType, 'ContractType', moduleContext.moduleName); 

export enum EmployeeIdentifierType {
	EGN = 1,
	LN = 2,
} 
TypeSystem.registerEnumInfo(EmployeeIdentifierType, 'EmployeeIdentifierType', moduleContext.moduleName); 

export enum SecurityType {
	Armed = 1,
	Unarmed = 2,
} 
TypeSystem.registerEnumInfo(SecurityType, 'SecurityType', moduleContext.moduleName); 

export enum AccessRegimeType {
	Performing = 2,
	NotPerforming = 3,
} 
TypeSystem.registerEnumInfo(AccessRegimeType, 'AccessRegimeType', moduleContext.moduleName); 

export enum ControlType {
	VideoControl = 4,
	Monitoring = 5,
} 
TypeSystem.registerEnumInfo(ControlType, 'ControlType', moduleContext.moduleName); 

export enum ClothintType {
	Casual = 1,
	Uniform = 2,
} 
TypeSystem.registerEnumInfo(ClothintType, 'ClothintType', moduleContext.moduleName); 

export enum GuardedPersonType {
	GuardedPerson = 1,
	Representative = 2,
} 
TypeSystem.registerEnumInfo(GuardedPersonType, 'GuardedPersonType', moduleContext.moduleName); 

export enum VehicleOwnershipType {
	Own = 1,
	Rented = 2,
} 
TypeSystem.registerEnumInfo(VehicleOwnershipType, 'VehicleOwnershipType', moduleContext.moduleName); 

export enum GuardedType {
	Man = 1,
	Woman = 2,
	Boy = 3,
	Girl = 4,
} 
TypeSystem.registerEnumInfo(GuardedType, 'GuardedType', moduleContext.moduleName); 

export enum NotificationType {
	NewSecurityContr235789 = 1,
	NewSecurityContr4 = 2,
	TerminationSecurityContr235789 = 3,
	TerminationSecurityContr4 = 4,
} 
TypeSystem.registerEnumInfo(NotificationType, 'NotificationType', moduleContext.moduleName); 

export enum AssignorPersonEntityType {
	Person = 1,
	Entity = 2,
} 
TypeSystem.registerEnumInfo(AssignorPersonEntityType, 'AssignorPersonEntityType', moduleContext.moduleName); 

@TypeSystem.typeDecorator('RequestForIssuingLicenseForPrivateSecurityServicesVM', moduleContext.moduleName)
export class RequestForIssuingLicenseForPrivateSecurityServicesVM extends ApplicationFormVMBase {
	@observable private _circumstances: RequestForIssuingLicenseForPrivateSecurityServicesDataVM = null;

	@TypeSystem.propertyDecorator(RequestForIssuingLicenseForPrivateSecurityServicesDataVM ? RequestForIssuingLicenseForPrivateSecurityServicesDataVM : moduleContext.moduleName + '.RequestForIssuingLicenseForPrivateSecurityServicesDataVM')
	public set circumstances(val: RequestForIssuingLicenseForPrivateSecurityServicesDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): RequestForIssuingLicenseForPrivateSecurityServicesDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RequestForIssuingLicenseForPrivateSecurityServicesDataVM', moduleContext.moduleName)
export class RequestForIssuingLicenseForPrivateSecurityServicesDataVM extends BaseDataModel {
	@observable private _securityServiceTypes: SecurityServiceTypesVM[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityServiceTypesVM ? SecurityServiceTypesVM : moduleContext.moduleName + '.SecurityServiceTypesVM')
	public set securityServiceTypes(val: SecurityServiceTypesVM[]) {
		this._securityServiceTypes = val;
	}


	public get securityServiceTypes(): SecurityServiceTypesVM[] {
		return this._securityServiceTypes;
	}

	@observable private _entityManagementAddress: EntityAddress = null;

	@TypeSystem.propertyDecorator(EntityAddress ? EntityAddress : moduleContext.moduleName + '.EntityAddress')
	public set entityManagementAddress(val: EntityAddress) {
		this._entityManagementAddress = val;
	}


	public get entityManagementAddress(): EntityAddress {
		return this._entityManagementAddress;
	}

	@observable private _correspondingAddress: EntityAddress = null;

	@TypeSystem.propertyDecorator(EntityAddress ? EntityAddress : moduleContext.moduleName + '.EntityAddress')
	public set correspondingAddress(val: EntityAddress) {
		this._correspondingAddress = val;
	}


	public get correspondingAddress(): EntityAddress {
		return this._correspondingAddress;
	}

	@observable private _mobilePhone: string = null;

	@TypeSystem.propertyDecorator('string')
	public set mobilePhone(val: string) {
		this._mobilePhone = val;
	}


	public get mobilePhone(): string {
		return this._mobilePhone;
	}

	@observable private _sunauServiceUri: string = null;

	@TypeSystem.propertyDecorator('string')
	public set sunauServiceUri(val: string) {
		this._sunauServiceUri = val;
	}


	public get sunauServiceUri(): string {
		return this._sunauServiceUri;
	}

	@observable private _issuingDocumentForCOD: string = null;

	@TypeSystem.propertyDecorator('string')
	public set issuingDocumentForCOD(val: string) {
		this._issuingDocumentForCOD = val;
	}


	public get issuingDocumentForCOD(): string {
		return this._issuingDocumentForCOD;
	}

	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _agreementToReceiveERefusal: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set agreementToReceiveERefusal(val: boolean) {
		this._agreementToReceiveERefusal = val;
	}


	public get agreementToReceiveERefusal(): boolean {
		return this._agreementToReceiveERefusal;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SecurityServiceTypesVM', moduleContext.moduleName)
export class SecurityServiceTypesVM extends BaseDataModel {
	@observable private _pointOfPrivateSecurityServicesLaw: PointOfPrivateSecurityServicesLaw = null;

	@TypeSystem.propertyDecorator(PointOfPrivateSecurityServicesLaw ? PointOfPrivateSecurityServicesLaw : moduleContext.moduleName + '.PointOfPrivateSecurityServicesLaw')
	public set pointOfPrivateSecurityServicesLaw(val: PointOfPrivateSecurityServicesLaw) {
		this._pointOfPrivateSecurityServicesLaw = val;
	}


	public get pointOfPrivateSecurityServicesLaw(): PointOfPrivateSecurityServicesLaw {
		return this._pointOfPrivateSecurityServicesLaw;
	}

	@observable private _territorialScopeOfServices: TerritorialScopeOfServicesVM = null;

	@TypeSystem.propertyDecorator(TerritorialScopeOfServicesVM ? TerritorialScopeOfServicesVM : moduleContext.moduleName + '.TerritorialScopeOfServicesVM')
	public set territorialScopeOfServices(val: TerritorialScopeOfServicesVM) {
		this._territorialScopeOfServices = val;
	}


	public get territorialScopeOfServices(): TerritorialScopeOfServicesVM {
		return this._territorialScopeOfServices;
	}

	@observable private _isSelected: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isSelected(val: boolean) {
		this._isSelected = val;
	}


	public get isSelected(): boolean {
		return this._isSelected;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('TerritorialScopeOfServicesVM', moduleContext.moduleName)
export class TerritorialScopeOfServicesVM extends BaseDataModel {
	@observable private _scopeOfCertification: ScopeOfCertification = null;

	@TypeSystem.propertyDecorator(ScopeOfCertification ? ScopeOfCertification : moduleContext.moduleName + '.ScopeOfCertification')
	public set scopeOfCertification(val: ScopeOfCertification) {
		this._scopeOfCertification = val;
	}


	public get scopeOfCertification(): ScopeOfCertification {
		return this._scopeOfCertification;
	}

	@observable private _districts: TerritorialScopeOfServicesDistrictsVM[] = null;

	@TypeSystem.propertyArrayDecorator(TerritorialScopeOfServicesDistrictsVM ? TerritorialScopeOfServicesDistrictsVM : moduleContext.moduleName + '.TerritorialScopeOfServicesDistrictsVM')
	public set districts(val: TerritorialScopeOfServicesDistrictsVM[]) {
		this._districts = val;
	}


	public get districts(): TerritorialScopeOfServicesDistrictsVM[] {
		return this._districts;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('TerritorialScopeOfServicesDistrictsVM', moduleContext.moduleName)
export class TerritorialScopeOfServicesDistrictsVM extends BaseDataModel {
	@observable private _districtGRAOCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtGRAOCode(val: string) {
		this._districtGRAOCode = val;
	}


	public get districtGRAOCode(): string {
		return this._districtGRAOCode;
	}

	@observable private _districtGRAOName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtGRAOName(val: string) {
		this._districtGRAOName = val;
	}


	public get districtGRAOName(): string {
		return this._districtGRAOName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotificationForConcludingOrTerminatingEmploymentContractVM', moduleContext.moduleName)
export class NotificationForConcludingOrTerminatingEmploymentContractVM extends ApplicationFormVMBase {
	@observable private _circumstances: NotificationForConcludingOrTerminatingEmploymentContractDataVM = null;

	@TypeSystem.propertyDecorator(NotificationForConcludingOrTerminatingEmploymentContractDataVM ? NotificationForConcludingOrTerminatingEmploymentContractDataVM : moduleContext.moduleName + '.NotificationForConcludingOrTerminatingEmploymentContractDataVM')
	public set circumstances(val: NotificationForConcludingOrTerminatingEmploymentContractDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): NotificationForConcludingOrTerminatingEmploymentContractDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotificationForConcludingOrTerminatingEmploymentContractDataVM', moduleContext.moduleName)
export class NotificationForConcludingOrTerminatingEmploymentContractDataVM extends BaseDataModel {
	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _notificationOfEmploymentContractType: NotificationOfEmploymentContractType = null;

	@TypeSystem.propertyDecorator(NotificationOfEmploymentContractType ? NotificationOfEmploymentContractType : moduleContext.moduleName + '.NotificationOfEmploymentContractType')
	public set notificationOfEmploymentContractType(val: NotificationOfEmploymentContractType) {
		this._notificationOfEmploymentContractType = val;
	}


	public get notificationOfEmploymentContractType(): NotificationOfEmploymentContractType {
		return this._notificationOfEmploymentContractType;
	}

	@observable private _newEmployeeRequests: NewEmployeeRequest[] = null;

	@TypeSystem.propertyArrayDecorator(NewEmployeeRequest ? NewEmployeeRequest : moduleContext.moduleName + '.NewEmployeeRequest')
	public set newEmployeeRequests(val: NewEmployeeRequest[]) {
		this._newEmployeeRequests = val;
	}


	public get newEmployeeRequests(): NewEmployeeRequest[] {
		return this._newEmployeeRequests;
	}

	@observable private _removeEmployeeRequests: RemoveEmployeeRequest[] = null;

	@TypeSystem.propertyArrayDecorator(RemoveEmployeeRequest ? RemoveEmployeeRequest : moduleContext.moduleName + '.RemoveEmployeeRequest')
	public set removeEmployeeRequests(val: RemoveEmployeeRequest[]) {
		this._removeEmployeeRequests = val;
	}


	public get removeEmployeeRequests(): RemoveEmployeeRequest[] {
		return this._removeEmployeeRequests;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NewEmployeeRequest', moduleContext.moduleName)
export class NewEmployeeRequest extends BaseDataModel {
	@observable private _employee: Employee = null;

	@TypeSystem.propertyDecorator(Employee ? Employee : moduleContext.moduleName + '.Employee')
	public set employee(val: Employee) {
		this._employee = val;
	}


	public get employee(): Employee {
		return this._employee;
	}

	@observable private _contractNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractNumber(val: string) {
		this._contractNumber = val;
	}


	public get contractNumber(): string {
		return this._contractNumber;
	}

	@observable private _contractDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set contractDate(val: moment.Moment) {
		this._contractDate = val;
	}


	public get contractDate(): moment.Moment {
		return this._contractDate;
	}

	@observable private _contractDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set contractDateSpecified(val: boolean) {
		this._contractDateSpecified = val;
	}


	public get contractDateSpecified(): boolean {
		return this._contractDateSpecified;
	}

	@observable private _contractType: ContractType = null;

	@TypeSystem.propertyDecorator(ContractType ? ContractType : moduleContext.moduleName + '.ContractType')
	public set contractType(val: ContractType) {
		this._contractType = val;
	}


	public get contractType(): ContractType {
		return this._contractType;
	}

	@observable private _contractTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set contractTypeSpecified(val: boolean) {
		this._contractTypeSpecified = val;
	}


	public get contractTypeSpecified(): boolean {
		return this._contractTypeSpecified;
	}

	@observable private _contractPeriodInMonths: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractPeriodInMonths(val: string) {
		this._contractPeriodInMonths = val;
	}


	public get contractPeriodInMonths(): string {
		return this._contractPeriodInMonths;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RemoveEmployeeRequest', moduleContext.moduleName)
export class RemoveEmployeeRequest extends BaseDataModel {
	@observable private _employee: Employee = null;

	@TypeSystem.propertyDecorator(Employee ? Employee : moduleContext.moduleName + '.Employee')
	public set employee(val: Employee) {
		this._employee = val;
	}


	public get employee(): Employee {
		return this._employee;
	}

	@observable private _contractTerminationNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNumber(val: string) {
		this._contractTerminationNumber = val;
	}


	public get contractTerminationNumber(): string {
		return this._contractTerminationNumber;
	}

	@observable private _contractTerminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set contractTerminationDate(val: moment.Moment) {
		this._contractTerminationDate = val;
	}


	public get contractTerminationDate(): moment.Moment {
		return this._contractTerminationDate;
	}

	@observable private _contractTerminationDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set contractTerminationDateSpecified(val: boolean) {
		this._contractTerminationDateSpecified = val;
	}


	public get contractTerminationDateSpecified(): boolean {
		return this._contractTerminationDateSpecified;
	}

	@observable private _contractTerminationEffectiveDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set contractTerminationEffectiveDate(val: moment.Moment) {
		this._contractTerminationEffectiveDate = val;
	}


	public get contractTerminationEffectiveDate(): moment.Moment {
		return this._contractTerminationEffectiveDate;
	}

	@observable private _contractTerminationEffectiveDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set contractTerminationEffectiveDateSpecified(val: boolean) {
		this._contractTerminationEffectiveDateSpecified = val;
	}


	public get contractTerminationEffectiveDateSpecified(): boolean {
		return this._contractTerminationEffectiveDateSpecified;
	}

	@observable private _contractTerminationNote: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNote(val: string) {
		this._contractTerminationNote = val;
	}


	public get contractTerminationNote(): string {
		return this._contractTerminationNote;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Employee', moduleContext.moduleName)
export class Employee extends BaseDataModel {
	@observable private _fullName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set fullName(val: string) {
		this._fullName = val;
	}


	public get fullName(): string {
		return this._fullName;
	}

	@observable private _identifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identifier(val: string) {
		this._identifier = val;
	}


	public get identifier(): string {
		return this._identifier;
	}

	@observable private _employeeIdentifierType: EmployeeIdentifierType = null;

	@TypeSystem.propertyDecorator(EmployeeIdentifierType ? EmployeeIdentifierType : moduleContext.moduleName + '.EmployeeIdentifierType')
	public set employeeIdentifierType(val: EmployeeIdentifierType) {
		this._employeeIdentifierType = val;
	}


	public get employeeIdentifierType(): EmployeeIdentifierType {
		return this._employeeIdentifierType;
	}

	@observable private _employeeIdentifierTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set employeeIdentifierTypeSpecified(val: boolean) {
		this._employeeIdentifierTypeSpecified = val;
	}


	public get employeeIdentifierTypeSpecified(): boolean {
		return this._employeeIdentifierTypeSpecified;
	}

	@observable private _aischodEmployeeID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodEmployeeID(val: string) {
		this._aischodEmployeeID = val;
	}


	public get aischodEmployeeID(): string {
		return this._aischodEmployeeID;
	}

	@observable private _citizenship: EmployeeCitizenshipType = null;

	@TypeSystem.propertyDecorator(EmployeeCitizenshipType ? EmployeeCitizenshipType : moduleContext.moduleName + '.EmployeeCitizenshipType')
	public set citizenship(val: EmployeeCitizenshipType) {
		this._citizenship = val;
	}


	public get citizenship(): EmployeeCitizenshipType {
		return this._citizenship;
	}

	@observable private _citizenshipSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set citizenshipSpecified(val: boolean) {
		this._citizenshipSpecified = val;
	}


	public get citizenshipSpecified(): boolean {
		return this._citizenshipSpecified;
	}

	@observable private _securityObject: string = null;

	@TypeSystem.propertyDecorator('string')
	public set securityObject(val: string) {
		this._securityObject = val;
	}


	public get securityObject(): string {
		return this._securityObject;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotificationForTakingOrRemovingFromSecurityVM', moduleContext.moduleName)
export class NotificationForTakingOrRemovingFromSecurityVM extends ApplicationFormVMBase {
	@observable private _circumstances: NotificationForTakingOrRemovingFromSecurityDataVM = null;

	@TypeSystem.propertyDecorator(NotificationForTakingOrRemovingFromSecurityDataVM ? NotificationForTakingOrRemovingFromSecurityDataVM : moduleContext.moduleName + '.NotificationForTakingOrRemovingFromSecurityDataVM')
	public set circumstances(val: NotificationForTakingOrRemovingFromSecurityDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): NotificationForTakingOrRemovingFromSecurityDataVM {
		return this._circumstances;
	}

	@observable private _securityObjectsData: SecurityObjectsDataVM = null;

	@TypeSystem.propertyDecorator(SecurityObjectsDataVM ? SecurityObjectsDataVM : moduleContext.moduleName + '.SecurityObjectsDataVM')
	public set securityObjectsData(val: SecurityObjectsDataVM) {
		this._securityObjectsData = val;
	}


	public get securityObjectsData(): SecurityObjectsDataVM {
		return this._securityObjectsData;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotificationForTakingOrRemovingFromSecurityDataVM', moduleContext.moduleName)
export class NotificationForTakingOrRemovingFromSecurityDataVM extends BaseDataModel {
	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _notificationType: NotificationType = null;

	@TypeSystem.propertyDecorator(NotificationType ? NotificationType : moduleContext.moduleName + '.NotificationType')
	public set notificationType(val: NotificationType) {
		this._notificationType = val;
	}


	public get notificationType(): NotificationType {
		return this._notificationType;
	}

	@observable private _securityContractData: SecurityContractData = null;

	@TypeSystem.propertyDecorator(SecurityContractData ? SecurityContractData : moduleContext.moduleName + '.SecurityContractData')
	public set securityContractData(val: SecurityContractData) {
		this._securityContractData = val;
	}


	public get securityContractData(): SecurityContractData {
		return this._securityContractData;
	}

	@observable private _contractAssignor: ContractAssignor = null;

	@TypeSystem.propertyDecorator(ContractAssignor ? ContractAssignor : moduleContext.moduleName + '.ContractAssignor')
	public set contractAssignor(val: ContractAssignor) {
		this._contractAssignor = val;
	}


	public get contractAssignor(): ContractAssignor {
		return this._contractAssignor;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SecurityObjectsDataVM', moduleContext.moduleName)
export class SecurityObjectsDataVM extends BaseDataModel {
	@observable private _securityObjects: SecurityObject[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityObject ? SecurityObject : moduleContext.moduleName + '.SecurityObject')
	public set securityObjects(val: SecurityObject[]) {
		this._securityObjects = val;
	}


	public get securityObjects(): SecurityObject[] {
		return this._securityObjects;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonalSecurity', moduleContext.moduleName)
export class PersonalSecurity extends BaseDataModel {
	@observable private _actualDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set actualDate(val: moment.Moment) {
		this._actualDate = val;
	}


	public get actualDate(): moment.Moment {
		return this._actualDate;
	}

	@observable private _actualDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set actualDateSpecified(val: boolean) {
		this._actualDateSpecified = val;
	}


	public get actualDateSpecified(): boolean {
		return this._actualDateSpecified;
	}

	@observable private _guardedPersonType: GuardedPersonType = null;

	@TypeSystem.propertyDecorator(GuardedPersonType ? GuardedPersonType : moduleContext.moduleName + '.GuardedPersonType')
	public set guardedPersonType(val: GuardedPersonType) {
		this._guardedPersonType = val;
	}


	public get guardedPersonType(): GuardedPersonType {
		return this._guardedPersonType;
	}

	@observable private _guardedPersonTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set guardedPersonTypeSpecified(val: boolean) {
		this._guardedPersonTypeSpecified = val;
	}


	public get guardedPersonTypeSpecified(): boolean {
		return this._guardedPersonTypeSpecified;
	}

	@observable private _guardedPerson: PersonAssignorData = null;

	@TypeSystem.propertyDecorator(PersonAssignorData ? PersonAssignorData : moduleContext.moduleName + '.PersonAssignorData')
	public set guardedPerson(val: PersonAssignorData) {
		this._guardedPerson = val;
	}


	public get guardedPerson(): PersonAssignorData {
		return this._guardedPerson;
	}

	@observable private _position: string = null;

	@TypeSystem.propertyDecorator('string')
	public set position(val: string) {
		this._position = val;
	}


	public get position(): string {
		return this._position;
	}

	@observable private _placeOfWork: string = null;

	@TypeSystem.propertyDecorator('string')
	public set placeOfWork(val: string) {
		this._placeOfWork = val;
	}


	public get placeOfWork(): string {
		return this._placeOfWork;
	}

	@observable private _address: string = null;

	@TypeSystem.propertyDecorator('string')
	public set address(val: string) {
		this._address = val;
	}


	public get address(): string {
		return this._address;
	}

	@observable private _securityType: SecurityType = null;

	@TypeSystem.propertyDecorator(SecurityType ? SecurityType : moduleContext.moduleName + '.SecurityType')
	public set securityType(val: SecurityType) {
		this._securityType = val;
	}


	public get securityType(): SecurityType {
		return this._securityType;
	}

	@observable private _securityTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set securityTypeSpecified(val: boolean) {
		this._securityTypeSpecified = val;
	}


	public get securityTypeSpecified(): boolean {
		return this._securityTypeSpecified;
	}

	@observable private _clothintType: ClothintType = null;

	@TypeSystem.propertyDecorator(ClothintType ? ClothintType : moduleContext.moduleName + '.ClothintType')
	public set clothintType(val: ClothintType) {
		this._clothintType = val;
	}


	public get clothintType(): ClothintType {
		return this._clothintType;
	}

	@observable private _clothintTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set clothintTypeSpecified(val: boolean) {
		this._clothintTypeSpecified = val;
	}


	public get clothintTypeSpecified(): boolean {
		return this._clothintTypeSpecified;
	}

	@observable private _securityTransports: SecurityTransport[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityTransport ? SecurityTransport : moduleContext.moduleName + '.SecurityTransport')
	public set securityTransports(val: SecurityTransport[]) {
		this._securityTransports = val;
	}


	public get securityTransports(): SecurityTransport[] {
		return this._securityTransports;
	}

	@observable private _terminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set terminationDate(val: moment.Moment) {
		this._terminationDate = val;
	}


	public get terminationDate(): moment.Moment {
		return this._terminationDate;
	}

	@observable private _terminationDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set terminationDateSpecified(val: boolean) {
		this._terminationDateSpecified = val;
	}


	public get terminationDateSpecified(): boolean {
		return this._terminationDateSpecified;
	}

	@observable private _aischodDistrictId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictId(val: string) {
		this._aischodDistrictId = val;
	}


	public get aischodDistrictId(): string {
		return this._aischodDistrictId;
	}

	@observable private _aischodDistrictName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictName(val: string) {
		this._aischodDistrictName = val;
	}


	public get aischodDistrictName(): string {
		return this._aischodDistrictName;
	}

	@observable private _contractTypeNumberDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTypeNumberDate(val: string) {
		this._contractTypeNumberDate = val;
	}


	public get contractTypeNumberDate(): string {
		return this._contractTypeNumberDate;
	}

	@observable private _contractTerminationNote: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNote(val: string) {
		this._contractTerminationNote = val;
	}


	public get contractTerminationNote(): string {
		return this._contractTerminationNote;
	}

	@observable private _aischodObjectID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodObjectID(val: string) {
		this._aischodObjectID = val;
	}


	public get aischodObjectID(): string {
		return this._aischodObjectID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ProtectionPersonsProperty', moduleContext.moduleName)
export class ProtectionPersonsProperty extends BaseDataModel {
	@observable private _actualDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set actualDate(val: moment.Moment) {
		this._actualDate = val;
	}


	public get actualDate(): moment.Moment {
		return this._actualDate;
	}

	@observable private _actualDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set actualDateSpecified(val: boolean) {
		this._actualDateSpecified = val;
	}


	public get actualDateSpecified(): boolean {
		return this._actualDateSpecified;
	}

	@observable private _securityObjectName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set securityObjectName(val: string) {
		this._securityObjectName = val;
	}


	public get securityObjectName(): string {
		return this._securityObjectName;
	}

	@observable private _districtCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtCode(val: string) {
		this._districtCode = val;
	}


	public get districtCode(): string {
		return this._districtCode;
	}

	@observable private _districtName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtName(val: string) {
		this._districtName = val;
	}


	public get districtName(): string {
		return this._districtName;
	}

	@observable private _address: string = null;

	@TypeSystem.propertyDecorator('string')
	public set address(val: string) {
		this._address = val;
	}


	public get address(): string {
		return this._address;
	}

	@observable private _securityType: SecurityType = null;

	@TypeSystem.propertyDecorator(SecurityType ? SecurityType : moduleContext.moduleName + '.SecurityType')
	public set securityType(val: SecurityType) {
		this._securityType = val;
	}


	public get securityType(): SecurityType {
		return this._securityType;
	}

	@observable private _securityTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set securityTypeSpecified(val: boolean) {
		this._securityTypeSpecified = val;
	}


	public get securityTypeSpecified(): boolean {
		return this._securityTypeSpecified;
	}

	@observable private _securityTransports: SecurityTransport[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityTransport ? SecurityTransport : moduleContext.moduleName + '.SecurityTransport')
	public set securityTransports(val: SecurityTransport[]) {
		this._securityTransports = val;
	}


	public get securityTransports(): SecurityTransport[] {
		return this._securityTransports;
	}

	@observable private _accessRegimeType: AccessRegimeType = null;

	@TypeSystem.propertyDecorator(AccessRegimeType ? AccessRegimeType : moduleContext.moduleName + '.AccessRegimeType')
	public set accessRegimeType(val: AccessRegimeType) {
		this._accessRegimeType = val;
	}


	public get accessRegimeType(): AccessRegimeType {
		return this._accessRegimeType;
	}

	@observable private _accessRegimeTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set accessRegimeTypeSpecified(val: boolean) {
		this._accessRegimeTypeSpecified = val;
	}


	public get accessRegimeTypeSpecified(): boolean {
		return this._accessRegimeTypeSpecified;
	}

	@observable private _controlType: ControlType = null;

	@TypeSystem.propertyDecorator(ControlType ? ControlType : moduleContext.moduleName + '.ControlType')
	public set controlType(val: ControlType) {
		this._controlType = val;
	}


	public get controlType(): ControlType {
		return this._controlType;
	}

	@observable private _controlTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set controlTypeSpecified(val: boolean) {
		this._controlTypeSpecified = val;
	}


	public get controlTypeSpecified(): boolean {
		return this._controlTypeSpecified;
	}

	@observable private _terminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set terminationDate(val: moment.Moment) {
		this._terminationDate = val;
	}


	public get terminationDate(): moment.Moment {
		return this._terminationDate;
	}

	@observable private _terminationDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set terminationDateSpecified(val: boolean) {
		this._terminationDateSpecified = val;
	}


	public get terminationDateSpecified(): boolean {
		return this._terminationDateSpecified;
	}

	@observable private _aischodDistrictId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictId(val: string) {
		this._aischodDistrictId = val;
	}


	public get aischodDistrictId(): string {
		return this._aischodDistrictId;
	}

	@observable private _aischodDistrictName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictName(val: string) {
		this._aischodDistrictName = val;
	}


	public get aischodDistrictName(): string {
		return this._aischodDistrictName;
	}

	@observable private _aischodAccessRegimeTypeId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodAccessRegimeTypeId(val: string) {
		this._aischodAccessRegimeTypeId = val;
	}


	public get aischodAccessRegimeTypeId(): string {
		return this._aischodAccessRegimeTypeId;
	}

	@observable private _aischodAccessRegimeTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodAccessRegimeTypeName(val: string) {
		this._aischodAccessRegimeTypeName = val;
	}


	public get aischodAccessRegimeTypeName(): string {
		return this._aischodAccessRegimeTypeName;
	}

	@observable private _aischodControlTypeId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodControlTypeId(val: string) {
		this._aischodControlTypeId = val;
	}


	public get aischodControlTypeId(): string {
		return this._aischodControlTypeId;
	}

	@observable private _aischodControlTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodControlTypeName(val: string) {
		this._aischodControlTypeName = val;
	}


	public get aischodControlTypeName(): string {
		return this._aischodControlTypeName;
	}

	@observable private _contractTypeNumberDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTypeNumberDate(val: string) {
		this._contractTypeNumberDate = val;
	}


	public get contractTypeNumberDate(): string {
		return this._contractTypeNumberDate;
	}

	@observable private _contractTerminationNote: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNote(val: string) {
		this._contractTerminationNote = val;
	}


	public get contractTerminationNote(): string {
		return this._contractTerminationNote;
	}

	@observable private _aischodObjectID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodObjectID(val: string) {
		this._aischodObjectID = val;
	}


	public get aischodObjectID(): string {
		return this._aischodObjectID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('AlarmAndSecurityActivity', moduleContext.moduleName)
export class AlarmAndSecurityActivity extends BaseDataModel {
	@observable private _actualDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set actualDate(val: moment.Moment) {
		this._actualDate = val;
	}


	public get actualDate(): moment.Moment {
		return this._actualDate;
	}

	@observable private _actualDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set actualDateSpecified(val: boolean) {
		this._actualDateSpecified = val;
	}


	public get actualDateSpecified(): boolean {
		return this._actualDateSpecified;
	}

	@observable private _securityObjectName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set securityObjectName(val: string) {
		this._securityObjectName = val;
	}


	public get securityObjectName(): string {
		return this._securityObjectName;
	}

	@observable private _districtCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtCode(val: string) {
		this._districtCode = val;
	}


	public get districtCode(): string {
		return this._districtCode;
	}

	@observable private _districtName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtName(val: string) {
		this._districtName = val;
	}


	public get districtName(): string {
		return this._districtName;
	}

	@observable private _address: string = null;

	@TypeSystem.propertyDecorator('string')
	public set address(val: string) {
		this._address = val;
	}


	public get address(): string {
		return this._address;
	}

	@observable private _securityType: SecurityType = null;

	@TypeSystem.propertyDecorator(SecurityType ? SecurityType : moduleContext.moduleName + '.SecurityType')
	public set securityType(val: SecurityType) {
		this._securityType = val;
	}


	public get securityType(): SecurityType {
		return this._securityType;
	}

	@observable private _securityTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set securityTypeSpecified(val: boolean) {
		this._securityTypeSpecified = val;
	}


	public get securityTypeSpecified(): boolean {
		return this._securityTypeSpecified;
	}

	@observable private _securityTransports: SecurityTransport[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityTransport ? SecurityTransport : moduleContext.moduleName + '.SecurityTransport')
	public set securityTransports(val: SecurityTransport[]) {
		this._securityTransports = val;
	}


	public get securityTransports(): SecurityTransport[] {
		return this._securityTransports;
	}

	@observable private _terminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set terminationDate(val: moment.Moment) {
		this._terminationDate = val;
	}


	public get terminationDate(): moment.Moment {
		return this._terminationDate;
	}

	@observable private _terminationDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set terminationDateSpecified(val: boolean) {
		this._terminationDateSpecified = val;
	}


	public get terminationDateSpecified(): boolean {
		return this._terminationDateSpecified;
	}

	@observable private _aischodDistrictId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictId(val: string) {
		this._aischodDistrictId = val;
	}


	public get aischodDistrictId(): string {
		return this._aischodDistrictId;
	}

	@observable private _aischodDistrictName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictName(val: string) {
		this._aischodDistrictName = val;
	}


	public get aischodDistrictName(): string {
		return this._aischodDistrictName;
	}

	@observable private _contractTypeNumberDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTypeNumberDate(val: string) {
		this._contractTypeNumberDate = val;
	}


	public get contractTypeNumberDate(): string {
		return this._contractTypeNumberDate;
	}

	@observable private _contractTerminationNote: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNote(val: string) {
		this._contractTerminationNote = val;
	}


	public get contractTerminationNote(): string {
		return this._contractTerminationNote;
	}

	@observable private _aischodObjectID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodObjectID(val: string) {
		this._aischodObjectID = val;
	}


	public get aischodObjectID(): string {
		return this._aischodObjectID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SelfProtectionPersonsProperty', moduleContext.moduleName)
export class SelfProtectionPersonsProperty extends BaseDataModel {
	@observable private _actualDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set actualDate(val: moment.Moment) {
		this._actualDate = val;
	}


	public get actualDate(): moment.Moment {
		return this._actualDate;
	}

	@observable private _actualDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set actualDateSpecified(val: boolean) {
		this._actualDateSpecified = val;
	}


	public get actualDateSpecified(): boolean {
		return this._actualDateSpecified;
	}

	@observable private _securityObjectName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set securityObjectName(val: string) {
		this._securityObjectName = val;
	}


	public get securityObjectName(): string {
		return this._securityObjectName;
	}

	@observable private _address: string = null;

	@TypeSystem.propertyDecorator('string')
	public set address(val: string) {
		this._address = val;
	}


	public get address(): string {
		return this._address;
	}

	@observable private _securityType: SecurityType = null;

	@TypeSystem.propertyDecorator(SecurityType ? SecurityType : moduleContext.moduleName + '.SecurityType')
	public set securityType(val: SecurityType) {
		this._securityType = val;
	}


	public get securityType(): SecurityType {
		return this._securityType;
	}

	@observable private _securityTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set securityTypeSpecified(val: boolean) {
		this._securityTypeSpecified = val;
	}


	public get securityTypeSpecified(): boolean {
		return this._securityTypeSpecified;
	}

	@observable private _hasTransport: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set hasTransport(val: boolean) {
		this._hasTransport = val;
	}


	public get hasTransport(): boolean {
		return this._hasTransport;
	}

	@observable private _hasTransportSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set hasTransportSpecified(val: boolean) {
		this._hasTransportSpecified = val;
	}


	public get hasTransportSpecified(): boolean {
		return this._hasTransportSpecified;
	}

	@observable private _securityTransports: SecurityTransport[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityTransport ? SecurityTransport : moduleContext.moduleName + '.SecurityTransport')
	public set securityTransports(val: SecurityTransport[]) {
		this._securityTransports = val;
	}


	public get securityTransports(): SecurityTransport[] {
		return this._securityTransports;
	}

	@observable private _accessRegimeType: AccessRegimeType = null;

	@TypeSystem.propertyDecorator(AccessRegimeType ? AccessRegimeType : moduleContext.moduleName + '.AccessRegimeType')
	public set accessRegimeType(val: AccessRegimeType) {
		this._accessRegimeType = val;
	}


	public get accessRegimeType(): AccessRegimeType {
		return this._accessRegimeType;
	}

	@observable private _accessRegimeTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set accessRegimeTypeSpecified(val: boolean) {
		this._accessRegimeTypeSpecified = val;
	}


	public get accessRegimeTypeSpecified(): boolean {
		return this._accessRegimeTypeSpecified;
	}

	@observable private _controlType: ControlType = null;

	@TypeSystem.propertyDecorator(ControlType ? ControlType : moduleContext.moduleName + '.ControlType')
	public set controlType(val: ControlType) {
		this._controlType = val;
	}


	public get controlType(): ControlType {
		return this._controlType;
	}

	@observable private _controlTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set controlTypeSpecified(val: boolean) {
		this._controlTypeSpecified = val;
	}


	public get controlTypeSpecified(): boolean {
		return this._controlTypeSpecified;
	}

	@observable private _terminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set terminationDate(val: moment.Moment) {
		this._terminationDate = val;
	}


	public get terminationDate(): moment.Moment {
		return this._terminationDate;
	}

	@observable private _terminationDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set terminationDateSpecified(val: boolean) {
		this._terminationDateSpecified = val;
	}


	public get terminationDateSpecified(): boolean {
		return this._terminationDateSpecified;
	}

	@observable private _aischodDistrictId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictId(val: string) {
		this._aischodDistrictId = val;
	}


	public get aischodDistrictId(): string {
		return this._aischodDistrictId;
	}

	@observable private _aischodDistrictName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictName(val: string) {
		this._aischodDistrictName = val;
	}


	public get aischodDistrictName(): string {
		return this._aischodDistrictName;
	}

	@observable private _aischodAccessRegimeTypeId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodAccessRegimeTypeId(val: string) {
		this._aischodAccessRegimeTypeId = val;
	}


	public get aischodAccessRegimeTypeId(): string {
		return this._aischodAccessRegimeTypeId;
	}

	@observable private _aischodAccessRegimeTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodAccessRegimeTypeName(val: string) {
		this._aischodAccessRegimeTypeName = val;
	}


	public get aischodAccessRegimeTypeName(): string {
		return this._aischodAccessRegimeTypeName;
	}

	@observable private _aischodControlTypeId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodControlTypeId(val: string) {
		this._aischodControlTypeId = val;
	}


	public get aischodControlTypeId(): string {
		return this._aischodControlTypeId;
	}

	@observable private _aischodControlTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodControlTypeName(val: string) {
		this._aischodControlTypeName = val;
	}


	public get aischodControlTypeName(): string {
		return this._aischodControlTypeName;
	}

	@observable private _contractTypeNumberDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTypeNumberDate(val: string) {
		this._contractTypeNumberDate = val;
	}


	public get contractTypeNumberDate(): string {
		return this._contractTypeNumberDate;
	}

	@observable private _contractTerminationNote: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNote(val: string) {
		this._contractTerminationNote = val;
	}


	public get contractTerminationNote(): string {
		return this._contractTerminationNote;
	}

	@observable private _aischodObjectID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodObjectID(val: string) {
		this._aischodObjectID = val;
	}


	public get aischodObjectID(): string {
		return this._aischodObjectID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SecurityOfSitesRealEstate', moduleContext.moduleName)
export class SecurityOfSitesRealEstate extends BaseDataModel {
	@observable private _actualDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set actualDate(val: moment.Moment) {
		this._actualDate = val;
	}


	public get actualDate(): moment.Moment {
		return this._actualDate;
	}

	@observable private _actualDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set actualDateSpecified(val: boolean) {
		this._actualDateSpecified = val;
	}


	public get actualDateSpecified(): boolean {
		return this._actualDateSpecified;
	}

	@observable private _securityObjectName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set securityObjectName(val: string) {
		this._securityObjectName = val;
	}


	public get securityObjectName(): string {
		return this._securityObjectName;
	}

	@observable private _districtCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtCode(val: string) {
		this._districtCode = val;
	}


	public get districtCode(): string {
		return this._districtCode;
	}

	@observable private _districtName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtName(val: string) {
		this._districtName = val;
	}


	public get districtName(): string {
		return this._districtName;
	}

	@observable private _address: string = null;

	@TypeSystem.propertyDecorator('string')
	public set address(val: string) {
		this._address = val;
	}


	public get address(): string {
		return this._address;
	}

	@observable private _securityType: SecurityType = null;

	@TypeSystem.propertyDecorator(SecurityType ? SecurityType : moduleContext.moduleName + '.SecurityType')
	public set securityType(val: SecurityType) {
		this._securityType = val;
	}


	public get securityType(): SecurityType {
		return this._securityType;
	}

	@observable private _securityTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set securityTypeSpecified(val: boolean) {
		this._securityTypeSpecified = val;
	}


	public get securityTypeSpecified(): boolean {
		return this._securityTypeSpecified;
	}

	@observable private _securityTransports: SecurityTransport[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityTransport ? SecurityTransport : moduleContext.moduleName + '.SecurityTransport')
	public set securityTransports(val: SecurityTransport[]) {
		this._securityTransports = val;
	}


	public get securityTransports(): SecurityTransport[] {
		return this._securityTransports;
	}

	@observable private _accessRegimeType: AccessRegimeType = null;

	@TypeSystem.propertyDecorator(AccessRegimeType ? AccessRegimeType : moduleContext.moduleName + '.AccessRegimeType')
	public set accessRegimeType(val: AccessRegimeType) {
		this._accessRegimeType = val;
	}


	public get accessRegimeType(): AccessRegimeType {
		return this._accessRegimeType;
	}

	@observable private _accessRegimeTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set accessRegimeTypeSpecified(val: boolean) {
		this._accessRegimeTypeSpecified = val;
	}


	public get accessRegimeTypeSpecified(): boolean {
		return this._accessRegimeTypeSpecified;
	}

	@observable private _controlType: ControlType = null;

	@TypeSystem.propertyDecorator(ControlType ? ControlType : moduleContext.moduleName + '.ControlType')
	public set controlType(val: ControlType) {
		this._controlType = val;
	}


	public get controlType(): ControlType {
		return this._controlType;
	}

	@observable private _controlTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set controlTypeSpecified(val: boolean) {
		this._controlTypeSpecified = val;
	}


	public get controlTypeSpecified(): boolean {
		return this._controlTypeSpecified;
	}

	@observable private _terminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set terminationDate(val: moment.Moment) {
		this._terminationDate = val;
	}


	public get terminationDate(): moment.Moment {
		return this._terminationDate;
	}

	@observable private _terminationDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set terminationDateSpecified(val: boolean) {
		this._terminationDateSpecified = val;
	}


	public get terminationDateSpecified(): boolean {
		return this._terminationDateSpecified;
	}

	@observable private _aischodDistrictId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictId(val: string) {
		this._aischodDistrictId = val;
	}


	public get aischodDistrictId(): string {
		return this._aischodDistrictId;
	}

	@observable private _aischodDistrictName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictName(val: string) {
		this._aischodDistrictName = val;
	}


	public get aischodDistrictName(): string {
		return this._aischodDistrictName;
	}

	@observable private _aischodAccessRegimeTypeId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodAccessRegimeTypeId(val: string) {
		this._aischodAccessRegimeTypeId = val;
	}


	public get aischodAccessRegimeTypeId(): string {
		return this._aischodAccessRegimeTypeId;
	}

	@observable private _aischodAccessRegimeTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodAccessRegimeTypeName(val: string) {
		this._aischodAccessRegimeTypeName = val;
	}


	public get aischodAccessRegimeTypeName(): string {
		return this._aischodAccessRegimeTypeName;
	}

	@observable private _aischodControlTypeId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodControlTypeId(val: string) {
		this._aischodControlTypeId = val;
	}


	public get aischodControlTypeId(): string {
		return this._aischodControlTypeId;
	}

	@observable private _aischodControlTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodControlTypeName(val: string) {
		this._aischodControlTypeName = val;
	}


	public get aischodControlTypeName(): string {
		return this._aischodControlTypeName;
	}

	@observable private _contractTypeNumberDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTypeNumberDate(val: string) {
		this._contractTypeNumberDate = val;
	}


	public get contractTypeNumberDate(): string {
		return this._contractTypeNumberDate;
	}

	@observable private _contractTerminationNote: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNote(val: string) {
		this._contractTerminationNote = val;
	}


	public get contractTerminationNote(): string {
		return this._contractTerminationNote;
	}

	@observable private _aischodObjectID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodObjectID(val: string) {
		this._aischodObjectID = val;
	}


	public get aischodObjectID(): string {
		return this._aischodObjectID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SecurityOfEvents', moduleContext.moduleName)
export class SecurityOfEvents extends BaseDataModel {
	@observable private _actualDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set actualDate(val: moment.Moment) {
		this._actualDate = val;
	}


	public get actualDate(): moment.Moment {
		return this._actualDate;
	}

	@observable private _actualDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set actualDateSpecified(val: boolean) {
		this._actualDateSpecified = val;
	}


	public get actualDateSpecified(): boolean {
		return this._actualDateSpecified;
	}

	@observable private _securityObjectName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set securityObjectName(val: string) {
		this._securityObjectName = val;
	}


	public get securityObjectName(): string {
		return this._securityObjectName;
	}

	@observable private _districtCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtCode(val: string) {
		this._districtCode = val;
	}


	public get districtCode(): string {
		return this._districtCode;
	}

	@observable private _districtName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtName(val: string) {
		this._districtName = val;
	}


	public get districtName(): string {
		return this._districtName;
	}

	@observable private _address: string = null;

	@TypeSystem.propertyDecorator('string')
	public set address(val: string) {
		this._address = val;
	}


	public get address(): string {
		return this._address;
	}

	@observable private _securityType: SecurityType = null;

	@TypeSystem.propertyDecorator(SecurityType ? SecurityType : moduleContext.moduleName + '.SecurityType')
	public set securityType(val: SecurityType) {
		this._securityType = val;
	}


	public get securityType(): SecurityType {
		return this._securityType;
	}

	@observable private _securityTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set securityTypeSpecified(val: boolean) {
		this._securityTypeSpecified = val;
	}


	public get securityTypeSpecified(): boolean {
		return this._securityTypeSpecified;
	}

	@observable private _securityTransports: SecurityTransport[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityTransport ? SecurityTransport : moduleContext.moduleName + '.SecurityTransport')
	public set securityTransports(val: SecurityTransport[]) {
		this._securityTransports = val;
	}


	public get securityTransports(): SecurityTransport[] {
		return this._securityTransports;
	}

	@observable private _accessRegimeType: AccessRegimeType = null;

	@TypeSystem.propertyDecorator(AccessRegimeType ? AccessRegimeType : moduleContext.moduleName + '.AccessRegimeType')
	public set accessRegimeType(val: AccessRegimeType) {
		this._accessRegimeType = val;
	}


	public get accessRegimeType(): AccessRegimeType {
		return this._accessRegimeType;
	}

	@observable private _accessRegimeTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set accessRegimeTypeSpecified(val: boolean) {
		this._accessRegimeTypeSpecified = val;
	}


	public get accessRegimeTypeSpecified(): boolean {
		return this._accessRegimeTypeSpecified;
	}

	@observable private _controlType: ControlType = null;

	@TypeSystem.propertyDecorator(ControlType ? ControlType : moduleContext.moduleName + '.ControlType')
	public set controlType(val: ControlType) {
		this._controlType = val;
	}


	public get controlType(): ControlType {
		return this._controlType;
	}

	@observable private _controlTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set controlTypeSpecified(val: boolean) {
		this._controlTypeSpecified = val;
	}


	public get controlTypeSpecified(): boolean {
		return this._controlTypeSpecified;
	}

	@observable private _terminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set terminationDate(val: moment.Moment) {
		this._terminationDate = val;
	}


	public get terminationDate(): moment.Moment {
		return this._terminationDate;
	}

	@observable private _terminationDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set terminationDateSpecified(val: boolean) {
		this._terminationDateSpecified = val;
	}


	public get terminationDateSpecified(): boolean {
		return this._terminationDateSpecified;
	}

	@observable private _aischodDistrictId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictId(val: string) {
		this._aischodDistrictId = val;
	}


	public get aischodDistrictId(): string {
		return this._aischodDistrictId;
	}

	@observable private _aischodDistrictName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictName(val: string) {
		this._aischodDistrictName = val;
	}


	public get aischodDistrictName(): string {
		return this._aischodDistrictName;
	}

	@observable private _aischodAccessRegimeTypeId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodAccessRegimeTypeId(val: string) {
		this._aischodAccessRegimeTypeId = val;
	}


	public get aischodAccessRegimeTypeId(): string {
		return this._aischodAccessRegimeTypeId;
	}

	@observable private _aischodAccessRegimeTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodAccessRegimeTypeName(val: string) {
		this._aischodAccessRegimeTypeName = val;
	}


	public get aischodAccessRegimeTypeName(): string {
		return this._aischodAccessRegimeTypeName;
	}

	@observable private _aischodControlTypeId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodControlTypeId(val: string) {
		this._aischodControlTypeId = val;
	}


	public get aischodControlTypeId(): string {
		return this._aischodControlTypeId;
	}

	@observable private _aischodControlTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodControlTypeName(val: string) {
		this._aischodControlTypeName = val;
	}


	public get aischodControlTypeName(): string {
		return this._aischodControlTypeName;
	}

	@observable private _contractTypeNumberDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTypeNumberDate(val: string) {
		this._contractTypeNumberDate = val;
	}


	public get contractTypeNumberDate(): string {
		return this._contractTypeNumberDate;
	}

	@observable private _contractTerminationNote: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNote(val: string) {
		this._contractTerminationNote = val;
	}


	public get contractTerminationNote(): string {
		return this._contractTerminationNote;
	}

	@observable private _aischodObjectID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodObjectID(val: string) {
		this._aischodObjectID = val;
	}


	public get aischodObjectID(): string {
		return this._aischodObjectID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SecurityObject', moduleContext.moduleName)
export class SecurityObject extends BaseDataModel {
	@observable private _pointOfPrivateSecurityServicesLaw: PointOfPrivateSecurityServicesLaw = null;

	@TypeSystem.propertyDecorator(PointOfPrivateSecurityServicesLaw ? PointOfPrivateSecurityServicesLaw : moduleContext.moduleName + '.PointOfPrivateSecurityServicesLaw')
	public set pointOfPrivateSecurityServicesLaw(val: PointOfPrivateSecurityServicesLaw) {
		this._pointOfPrivateSecurityServicesLaw = val;
	}


	public get pointOfPrivateSecurityServicesLaw(): PointOfPrivateSecurityServicesLaw {
		return this._pointOfPrivateSecurityServicesLaw;
	}

	@observable private _pointOfPrivateSecurityServicesLawSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set pointOfPrivateSecurityServicesLawSpecified(val: boolean) {
		this._pointOfPrivateSecurityServicesLawSpecified = val;
	}


	public get pointOfPrivateSecurityServicesLawSpecified(): boolean {
		return this._pointOfPrivateSecurityServicesLawSpecified;
	}

	@observable private _personalSecurity: PersonalSecurity = null;

	@TypeSystem.propertyDecorator(PersonalSecurity ? PersonalSecurity : moduleContext.moduleName + '.PersonalSecurity')
	public set personalSecurity(val: PersonalSecurity) {
		this._personalSecurity = val;
	}


	public get personalSecurity(): PersonalSecurity {
		return this._personalSecurity;
	}

	@observable private _protectionPersonsProperty: ProtectionPersonsProperty = null;

	@TypeSystem.propertyDecorator(ProtectionPersonsProperty ? ProtectionPersonsProperty : moduleContext.moduleName + '.ProtectionPersonsProperty')
	public set protectionPersonsProperty(val: ProtectionPersonsProperty) {
		this._protectionPersonsProperty = val;
	}


	public get protectionPersonsProperty(): ProtectionPersonsProperty {
		return this._protectionPersonsProperty;
	}

	@observable private _alarmAndSecurityActivity: AlarmAndSecurityActivity = null;

	@TypeSystem.propertyDecorator(AlarmAndSecurityActivity ? AlarmAndSecurityActivity : moduleContext.moduleName + '.AlarmAndSecurityActivity')
	public set alarmAndSecurityActivity(val: AlarmAndSecurityActivity) {
		this._alarmAndSecurityActivity = val;
	}


	public get alarmAndSecurityActivity(): AlarmAndSecurityActivity {
		return this._alarmAndSecurityActivity;
	}

	@observable private _selfProtectionPersonsProperty: SelfProtectionPersonsProperty = null;

	@TypeSystem.propertyDecorator(SelfProtectionPersonsProperty ? SelfProtectionPersonsProperty : moduleContext.moduleName + '.SelfProtectionPersonsProperty')
	public set selfProtectionPersonsProperty(val: SelfProtectionPersonsProperty) {
		this._selfProtectionPersonsProperty = val;
	}


	public get selfProtectionPersonsProperty(): SelfProtectionPersonsProperty {
		return this._selfProtectionPersonsProperty;
	}

	@observable private _securityOfSitesRealEstate: SecurityOfSitesRealEstate = null;

	@TypeSystem.propertyDecorator(SecurityOfSitesRealEstate ? SecurityOfSitesRealEstate : moduleContext.moduleName + '.SecurityOfSitesRealEstate')
	public set securityOfSitesRealEstate(val: SecurityOfSitesRealEstate) {
		this._securityOfSitesRealEstate = val;
	}


	public get securityOfSitesRealEstate(): SecurityOfSitesRealEstate {
		return this._securityOfSitesRealEstate;
	}

	@observable private _securityOfEvents: SecurityOfEvents = null;

	@TypeSystem.propertyDecorator(SecurityOfEvents ? SecurityOfEvents : moduleContext.moduleName + '.SecurityOfEvents')
	public set securityOfEvents(val: SecurityOfEvents) {
		this._securityOfEvents = val;
	}


	public get securityOfEvents(): SecurityOfEvents {
		return this._securityOfEvents;
	}

	@observable private _securityTransportingCargo: SecurityTransportingCargo = null;

	@TypeSystem.propertyDecorator(SecurityTransportingCargo ? SecurityTransportingCargo : moduleContext.moduleName + '.SecurityTransportingCargo')
	public set securityTransportingCargo(val: SecurityTransportingCargo) {
		this._securityTransportingCargo = val;
	}


	public get securityTransportingCargo(): SecurityTransportingCargo {
		return this._securityTransportingCargo;
	}

	@observable private _protectionOfAgriculturalProperty: ProtectionOfAgriculturalProperty = null;

	@TypeSystem.propertyDecorator(ProtectionOfAgriculturalProperty ? ProtectionOfAgriculturalProperty : moduleContext.moduleName + '.ProtectionOfAgriculturalProperty')
	public set protectionOfAgriculturalProperty(val: ProtectionOfAgriculturalProperty) {
		this._protectionOfAgriculturalProperty = val;
	}


	public get protectionOfAgriculturalProperty(): ProtectionOfAgriculturalProperty {
		return this._protectionOfAgriculturalProperty;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SecurityTransportingCargo', moduleContext.moduleName)
export class SecurityTransportingCargo extends BaseDataModel {
	@observable private _actualDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set actualDate(val: moment.Moment) {
		this._actualDate = val;
	}


	public get actualDate(): moment.Moment {
		return this._actualDate;
	}

	@observable private _actualDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set actualDateSpecified(val: boolean) {
		this._actualDateSpecified = val;
	}


	public get actualDateSpecified(): boolean {
		return this._actualDateSpecified;
	}

	@observable private _securityType: SecurityType = null;

	@TypeSystem.propertyDecorator(SecurityType ? SecurityType : moduleContext.moduleName + '.SecurityType')
	public set securityType(val: SecurityType) {
		this._securityType = val;
	}


	public get securityType(): SecurityType {
		return this._securityType;
	}

	@observable private _securityTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set securityTypeSpecified(val: boolean) {
		this._securityTypeSpecified = val;
	}


	public get securityTypeSpecified(): boolean {
		return this._securityTypeSpecified;
	}

	@observable private _securityTransports: SecurityTransport[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityTransport ? SecurityTransport : moduleContext.moduleName + '.SecurityTransport')
	public set securityTransports(val: SecurityTransport[]) {
		this._securityTransports = val;
	}


	public get securityTransports(): SecurityTransport[] {
		return this._securityTransports;
	}

	@observable private _objectTypes: string = null;

	@TypeSystem.propertyDecorator('string')
	public set objectTypes(val: string) {
		this._objectTypes = val;
	}


	public get objectTypes(): string {
		return this._objectTypes;
	}

	@observable private _territorialScopeFrom: string = null;

	@TypeSystem.propertyDecorator('string')
	public set territorialScopeFrom(val: string) {
		this._territorialScopeFrom = val;
	}


	public get territorialScopeFrom(): string {
		return this._territorialScopeFrom;
	}

	@observable private _territorialScopeTo: string = null;

	@TypeSystem.propertyDecorator('string')
	public set territorialScopeTo(val: string) {
		this._territorialScopeTo = val;
	}


	public get territorialScopeTo(): string {
		return this._territorialScopeTo;
	}

	@observable private _terminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set terminationDate(val: moment.Moment) {
		this._terminationDate = val;
	}


	public get terminationDate(): moment.Moment {
		return this._terminationDate;
	}

	@observable private _terminationDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set terminationDateSpecified(val: boolean) {
		this._terminationDateSpecified = val;
	}


	public get terminationDateSpecified(): boolean {
		return this._terminationDateSpecified;
	}

	@observable private _contractTypeNumberDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTypeNumberDate(val: string) {
		this._contractTypeNumberDate = val;
	}


	public get contractTypeNumberDate(): string {
		return this._contractTypeNumberDate;
	}

	@observable private _contractTerminationNote: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNote(val: string) {
		this._contractTerminationNote = val;
	}


	public get contractTerminationNote(): string {
		return this._contractTerminationNote;
	}

	@observable private _aischodObjectID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodObjectID(val: string) {
		this._aischodObjectID = val;
	}


	public get aischodObjectID(): string {
		return this._aischodObjectID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ProtectionOfAgriculturalProperty', moduleContext.moduleName)
export class ProtectionOfAgriculturalProperty extends BaseDataModel {
	@observable private _actualDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set actualDate(val: moment.Moment) {
		this._actualDate = val;
	}


	public get actualDate(): moment.Moment {
		return this._actualDate;
	}

	@observable private _actualDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set actualDateSpecified(val: boolean) {
		this._actualDateSpecified = val;
	}


	public get actualDateSpecified(): boolean {
		return this._actualDateSpecified;
	}

	@observable private _securityObjectName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set securityObjectName(val: string) {
		this._securityObjectName = val;
	}


	public get securityObjectName(): string {
		return this._securityObjectName;
	}

	@observable private _districtCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtCode(val: string) {
		this._districtCode = val;
	}


	public get districtCode(): string {
		return this._districtCode;
	}

	@observable private _districtName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set districtName(val: string) {
		this._districtName = val;
	}


	public get districtName(): string {
		return this._districtName;
	}

	@observable private _address: string = null;

	@TypeSystem.propertyDecorator('string')
	public set address(val: string) {
		this._address = val;
	}


	public get address(): string {
		return this._address;
	}

	@observable private _securityType: SecurityType = null;

	@TypeSystem.propertyDecorator(SecurityType ? SecurityType : moduleContext.moduleName + '.SecurityType')
	public set securityType(val: SecurityType) {
		this._securityType = val;
	}


	public get securityType(): SecurityType {
		return this._securityType;
	}

	@observable private _securityTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set securityTypeSpecified(val: boolean) {
		this._securityTypeSpecified = val;
	}


	public get securityTypeSpecified(): boolean {
		return this._securityTypeSpecified;
	}

	@observable private _securityTransports: SecurityTransport[] = null;

	@TypeSystem.propertyArrayDecorator(SecurityTransport ? SecurityTransport : moduleContext.moduleName + '.SecurityTransport')
	public set securityTransports(val: SecurityTransport[]) {
		this._securityTransports = val;
	}


	public get securityTransports(): SecurityTransport[] {
		return this._securityTransports;
	}

	@observable private _terminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set terminationDate(val: moment.Moment) {
		this._terminationDate = val;
	}


	public get terminationDate(): moment.Moment {
		return this._terminationDate;
	}

	@observable private _terminationDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set terminationDateSpecified(val: boolean) {
		this._terminationDateSpecified = val;
	}


	public get terminationDateSpecified(): boolean {
		return this._terminationDateSpecified;
	}

	@observable private _aischodDistrictId: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictId(val: string) {
		this._aischodDistrictId = val;
	}


	public get aischodDistrictId(): string {
		return this._aischodDistrictId;
	}

	@observable private _aischodDistrictName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodDistrictName(val: string) {
		this._aischodDistrictName = val;
	}


	public get aischodDistrictName(): string {
		return this._aischodDistrictName;
	}

	@observable private _contractTypeNumberDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTypeNumberDate(val: string) {
		this._contractTypeNumberDate = val;
	}


	public get contractTypeNumberDate(): string {
		return this._contractTypeNumberDate;
	}

	@observable private _contractTerminationNote: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractTerminationNote(val: string) {
		this._contractTerminationNote = val;
	}


	public get contractTerminationNote(): string {
		return this._contractTerminationNote;
	}

	@observable private _aischodObjectID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aischodObjectID(val: string) {
		this._aischodObjectID = val;
	}


	public get aischodObjectID(): string {
		return this._aischodObjectID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SecurityTransport', moduleContext.moduleName)
export class SecurityTransport extends BaseDataModel {
	@observable private _vehicleOwnershipType: VehicleOwnershipType = null;

	@TypeSystem.propertyDecorator(VehicleOwnershipType ? VehicleOwnershipType : moduleContext.moduleName + '.VehicleOwnershipType')
	public set vehicleOwnershipType(val: VehicleOwnershipType) {
		this._vehicleOwnershipType = val;
	}


	public get vehicleOwnershipType(): VehicleOwnershipType {
		return this._vehicleOwnershipType;
	}

	@observable private _vehicleOwnershipTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set vehicleOwnershipTypeSpecified(val: boolean) {
		this._vehicleOwnershipTypeSpecified = val;
	}


	public get vehicleOwnershipTypeSpecified(): boolean {
		return this._vehicleOwnershipTypeSpecified;
	}

	@observable private _registrationNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set registrationNumber(val: string) {
		this._registrationNumber = val;
	}


	public get registrationNumber(): string {
		return this._registrationNumber;
	}

	@observable private _makeAndModel: string = null;

	@TypeSystem.propertyDecorator('string')
	public set makeAndModel(val: string) {
		this._makeAndModel = val;
	}


	public get makeAndModel(): string {
		return this._makeAndModel;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SecurityContractData', moduleContext.moduleName)
export class SecurityContractData extends BaseDataModel {
	@observable private _contractNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractNumber(val: string) {
		this._contractNumber = val;
	}


	public get contractNumber(): string {
		return this._contractNumber;
	}

	@observable private _documentNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set documentNumber(val: string) {
		this._documentNumber = val;
	}


	public get documentNumber(): string {
		return this._documentNumber;
	}

	@observable private _contractDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set contractDate(val: moment.Moment) {
		this._contractDate = val;
	}


	public get contractDate(): moment.Moment {
		return this._contractDate;
	}

	@observable private _contractDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set contractDateSpecified(val: boolean) {
		this._contractDateSpecified = val;
	}


	public get contractDateSpecified(): boolean {
		return this._contractDateSpecified;
	}

	@observable private _contractType: string = null;

	@TypeSystem.propertyDecorator('string')
	public set contractType(val: string) {
		this._contractType = val;
	}


	public get contractType(): string {
		return this._contractType;
	}

	@observable private _contractIsExpired: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set contractIsExpired(val: boolean) {
		this._contractIsExpired = val;
	}


	public get contractIsExpired(): boolean {
		return this._contractIsExpired;
	}

	@observable private _contractIsExpiredSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set contractIsExpiredSpecified(val: boolean) {
		this._contractIsExpiredSpecified = val;
	}


	public get contractIsExpiredSpecified(): boolean {
		return this._contractIsExpiredSpecified;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ContractAssignor', moduleContext.moduleName)
export class ContractAssignor extends BaseDataModel {
	@observable private _assignorPersonEntityType: AssignorPersonEntityType = null;

	@TypeSystem.propertyDecorator(AssignorPersonEntityType ? AssignorPersonEntityType : moduleContext.moduleName + '.AssignorPersonEntityType')
	public set assignorPersonEntityType(val: AssignorPersonEntityType) {
		this._assignorPersonEntityType = val;
	}


	public get assignorPersonEntityType(): AssignorPersonEntityType {
		return this._assignorPersonEntityType;
	}

	@observable private _assignorPersonEntityTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set assignorPersonEntityTypeSpecified(val: boolean) {
		this._assignorPersonEntityTypeSpecified = val;
	}


	public get assignorPersonEntityTypeSpecified(): boolean {
		return this._assignorPersonEntityTypeSpecified;
	}

	@observable private _personAssignorData: PersonAssignorData = null;

	@TypeSystem.propertyDecorator(PersonAssignorData ? PersonAssignorData : moduleContext.moduleName + '.PersonAssignorData')
	public set personAssignorData(val: PersonAssignorData) {
		this._personAssignorData = val;
	}


	public get personAssignorData(): PersonAssignorData {
		return this._personAssignorData;
	}

	@observable private _entityAssignorData: EntityAssignorData = null;

	@TypeSystem.propertyDecorator(EntityAssignorData ? EntityAssignorData : moduleContext.moduleName + '.EntityAssignorData')
	public set entityAssignorData(val: EntityAssignorData) {
		this._entityAssignorData = val;
	}


	public get entityAssignorData(): EntityAssignorData {
		return this._entityAssignorData;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('EntityAssignorData', moduleContext.moduleName)
export class EntityAssignorData extends BaseDataModel {
	@observable private _identifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identifier(val: string) {
		this._identifier = val;
	}


	public get identifier(): string {
		return this._identifier;
	}

	@observable private _fullName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set fullName(val: string) {
		this._fullName = val;
	}


	public get fullName(): string {
		return this._fullName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonAssignorData', moduleContext.moduleName)
export class PersonAssignorData extends BaseDataModel {
	@observable private _identifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identifier(val: string) {
		this._identifier = val;
	}


	public get identifier(): string {
		return this._identifier;
	}

	@observable private _fullName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set fullName(val: string) {
		this._fullName = val;
	}


	public get fullName(): string {
		return this._fullName;
	}

	@observable private _guardedType: GuardedType = null;

	@TypeSystem.propertyDecorator(GuardedType ? GuardedType : moduleContext.moduleName + '.GuardedType')
	public set guardedType(val: GuardedType) {
		this._guardedType = val;
	}


	public get guardedType(): GuardedType {
		return this._guardedType;
	}

	@observable private _guardedTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set guardedTypeSpecified(val: boolean) {
		this._guardedTypeSpecified = val;
	}


	public get guardedTypeSpecified(): boolean {
		return this._guardedTypeSpecified;
	}

	@observable private _citizenship: PersonAssignorCitizenshipType = null;

	@TypeSystem.propertyDecorator(PersonAssignorCitizenshipType ? PersonAssignorCitizenshipType : moduleContext.moduleName + '.PersonAssignorCitizenshipType')
	public set citizenship(val: PersonAssignorCitizenshipType) {
		this._citizenship = val;
	}


	public get citizenship(): PersonAssignorCitizenshipType {
		return this._citizenship;
	}

	@observable private _citizenshipSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set citizenshipSpecified(val: boolean) {
		this._citizenshipSpecified = val;
	}


	public get citizenshipSpecified(): boolean {
		return this._citizenshipSpecified;
	}

	@observable private _identifierType: PersonAssignorIdentifierType = null;

	@TypeSystem.propertyDecorator(PersonAssignorIdentifierType ? PersonAssignorIdentifierType : moduleContext.moduleName + '.PersonAssignorIdentifierType')
	public set identifierType(val: PersonAssignorIdentifierType) {
		this._identifierType = val;
	}


	public get identifierType(): PersonAssignorIdentifierType {
		return this._identifierType;
	}

	@observable private _identifierTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set identifierTypeSpecified(val: boolean) {
		this._identifierTypeSpecified = val;
	}


	public get identifierTypeSpecified(): boolean {
		return this._identifierTypeSpecified;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}

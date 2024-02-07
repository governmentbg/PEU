import moment from 'moment';
import { observable } from 'mobx';
import { TypeSystem, BaseDataModel, BasePagedSearchCriteria } from 'cnsys-core';
import { moduleContext } from '../ModuleContext';
import { RegistrationDataTypes } from 'eau-core'

export enum ObligedPersonIdentTypes {
	EGN = 1,
	LNC = 2,
	BULSTAT = 3,
} 
TypeSystem.registerEnumInfo(ObligedPersonIdentTypes, 'ObligedPersonIdentTypes', moduleContext.moduleName); 

export enum ANDSourceIds {
	KAT = 1,
	BDS = 2,
} 
TypeSystem.registerEnumInfo(ANDSourceIds, 'ANDSourceIds', moduleContext.moduleName); 

export enum PaymentRequestStatuses {
	New = 1,
	Sent = 2,
	Paid = 3,
	Cancelled = 4,
	Expired = 5,
} 
TypeSystem.registerEnumInfo(PaymentRequestStatuses, 'PaymentRequestStatuses', moduleContext.moduleName); 

export enum DocumentAccessedDataTypes {
	PersonIdentifier = 1,
	PersonIdentifierAndNames = 2,
	VehicleRegNumber = 3,
} 
TypeSystem.registerEnumInfo(DocumentAccessedDataTypes, 'DocumentAccessedDataTypes', moduleContext.moduleName); 

export enum ObjectTypes {
	Document = 1,
	ServiceCaseFile = 2,
	UserProfile = 3,
	AuthenticationMeans = 4,
	User = 5,
} 
TypeSystem.registerEnumInfo(ObjectTypes, 'ObjectTypes', moduleContext.moduleName); 

export enum ActionTypes {
	Submission = 1,
	Preview = 2,
	Edit = 3,
	Login = 4,
	Add = 5,
	Delete = 6,
} 
TypeSystem.registerEnumInfo(ActionTypes, 'ActionTypes', moduleContext.moduleName); 

export enum LogActionSearchModes {
	Operational = 1,
	Archive = 2,
} 
TypeSystem.registerEnumInfo(LogActionSearchModes, 'LogActionSearchModes', moduleContext.moduleName); 

export enum DataServiceLimitStatus {
	Inactive = 0,
	Active = 1,
} 
TypeSystem.registerEnumInfo(DataServiceLimitStatus, 'DataServiceLimitStatus', moduleContext.moduleName); 

export enum UserStatuses {
	NotConfirmed = 1,
	Active = 2,
	Inactive = 3,
	Locked = 4,
	Deactivated = 5,
} 
TypeSystem.registerEnumInfo(UserStatuses, 'UserStatuses', moduleContext.moduleName); 

export enum AuthenticationTypes {
	UsernamePassword = 1,
	ActiveDirectory = 2,
	Certificate = 3,
	NRA = 4,
	EAuth = 5,
} 
TypeSystem.registerEnumInfo(AuthenticationTypes, 'AuthenticationTypes', moduleContext.moduleName); 

@TypeSystem.typeDecorator('DocumentTypeSearchCriteria', moduleContext.moduleName)
export class DocumentTypeSearchCriteria extends BasePagedSearchCriteria {
	@observable private _Ids: number[] = null;

	@TypeSystem.propertyArrayDecorator('number')
	public set Ids(val: number[]) {
		this._Ids = val;
	}


	public get Ids(): number[] {
		return this._Ids;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('LabelSearchCriteria', moduleContext.moduleName)
export class LabelSearchCriteria extends BasePagedSearchCriteria {
	@observable private _labelIDs: number[] = null;

	@TypeSystem.propertyArrayDecorator('number')
	public set labelIDs(val: number[]) {
		this._labelIDs = val;
	}


	public get labelIDs(): number[] {
		return this._labelIDs;
	}

	@observable private _languageID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set languageID(val: number) {
		this._languageID = val;
	}


	public get languageID(): number {
		return this._languageID;
	}

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
	}

	@observable private _value: string = null;

	@TypeSystem.propertyDecorator('string')
	public set value(val: string) {
		this._value = val;
	}


	public get value(): string {
		return this._value;
	}

	@observable private _loadDecsription: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set loadDecsription(val: boolean) {
		this._loadDecsription = val;
	}


	public get loadDecsription(): boolean {
		return this._loadDecsription;
	}

	@observable private _loadSeparateValueI18N: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set loadSeparateValueI18N(val: boolean) {
		this._loadSeparateValueI18N = val;
	}


	public get loadSeparateValueI18N(): boolean {
		return this._loadSeparateValueI18N;
	}

	@observable private _loadOnlyUntranslated: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set loadOnlyUntranslated(val: boolean) {
		this._loadOnlyUntranslated = val;
	}


	public get loadOnlyUntranslated(): boolean {
		return this._loadOnlyUntranslated;
	}

	@observable private _forceTranslated: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set forceTranslated(val: boolean) {
		this._forceTranslated = val;
	}


	public get forceTranslated(): boolean {
		return this._forceTranslated;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('LabelTranslation', moduleContext.moduleName)
export class LabelTranslation extends BaseDataModel {
	@observable private _labelID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set labelID(val: number) {
		this._labelID = val;
	}


	public get labelID(): number {
		return this._labelID;
	}

	@observable private _languageID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set languageID(val: number) {
		this._languageID = val;
	}


	public get languageID(): number {
		return this._languageID;
	}

	@observable private _description: string = null;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}


	public get description(): string {
		return this._description;
	}

	@observable private _value: string = null;

	@TypeSystem.propertyDecorator('string')
	public set value(val: string) {
		this._value = val;
	}


	public get value(): string {
		return this._value;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceTranslation', moduleContext.moduleName)
export class ServiceTranslation extends BaseDataModel {
	@observable private _serviceID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set serviceID(val: number) {
		this._serviceID = val;
	}


	public get serviceID(): number {
		return this._serviceID;
	}

	@observable private _languageID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set languageID(val: number) {
		this._languageID = val;
	}


	public get languageID(): number {
		return this._languageID;
	}

	@observable private _description: string = null;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}


	public get description(): string {
		return this._description;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _attachedDocumentsDescription: string = null;

	@TypeSystem.propertyDecorator('string')
	public set attachedDocumentsDescription(val: string) {
		this._attachedDocumentsDescription = val;
	}


	public get attachedDocumentsDescription(): string {
		return this._attachedDocumentsDescription;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceGroupSearchCriteria', moduleContext.moduleName)
export class ServiceGroupSearchCriteria extends BasePagedSearchCriteria {
	@observable private _Ids: number[] = null;

	@TypeSystem.propertyArrayDecorator('number')
	public set Ids(val: number[]) {
		this._Ids = val;
	}


	public get Ids(): number[] {
		return this._Ids;
	}

	@observable private _languageID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set languageID(val: number) {
		this._languageID = val;
	}


	public get languageID(): number {
		return this._languageID;
	}

	@observable private _forceTranslated: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set forceTranslated(val: boolean) {
		this._forceTranslated = val;
	}


	public get forceTranslated(): boolean {
		return this._forceTranslated;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceGroupTranslation', moduleContext.moduleName)
export class ServiceGroupTranslation extends BaseDataModel {
	@observable private _groupID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set groupID(val: number) {
		this._groupID = val;
	}


	public get groupID(): number {
		return this._groupID;
	}

	@observable private _languageID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set languageID(val: number) {
		this._languageID = val;
	}


	public get languageID(): number {
		return this._languageID;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('LDAPUserSearchCritaria', moduleContext.moduleName)
export class LDAPUserSearchCritaria extends BasePagedSearchCriteria {
	@observable private _username: string = null;

	@TypeSystem.propertyDecorator('string')
	public set username(val: string) {
		this._username = val;
	}


	public get username(): string {
		return this._username;
	}

	@observable private _firstName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set firstName(val: string) {
		this._firstName = val;
	}


	public get firstName(): string {
		return this._firstName;
	}

	@observable private _surname: string = null;

	@TypeSystem.propertyDecorator('string')
	public set surname(val: string) {
		this._surname = val;
	}


	public get surname(): string {
		return this._surname;
	}

	@observable private _lastName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set lastName(val: string) {
		this._lastName = val;
	}


	public get lastName(): string {
		return this._lastName;
	}

	@observable private _email: string = null;

	@TypeSystem.propertyDecorator('string')
	public set email(val: string) {
		this._email = val;
	}


	public get email(): string {
		return this._email;
	}

	@observable private _fullName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set fullName(val: string) {
		this._fullName = val;
	}


	public get fullName(): string {
		return this._fullName;
	}

	@observable private _displayName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set displayName(val: string) {
		this._displayName = val;
	}


	public get displayName(): string {
		return this._displayName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('LDAPUser', moduleContext.moduleName)
export class LDAPUser extends BaseDataModel {
	@observable private _accountGuid: string = null;

	@TypeSystem.propertyDecorator('string')
	public set accountGuid(val: string) {
		this._accountGuid = val;
	}


	public get accountGuid(): string {
		return this._accountGuid;
	}

	@observable private _username: string = null;

	@TypeSystem.propertyDecorator('string')
	public set username(val: string) {
		this._username = val;
	}


	public get username(): string {
		return this._username;
	}

	@observable private _firstName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set firstName(val: string) {
		this._firstName = val;
	}


	public get firstName(): string {
		return this._firstName;
	}

	@observable private _surname: string = null;

	@TypeSystem.propertyDecorator('string')
	public set surname(val: string) {
		this._surname = val;
	}


	public get surname(): string {
		return this._surname;
	}

	@observable private _lastName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set lastName(val: string) {
		this._lastName = val;
	}


	public get lastName(): string {
		return this._lastName;
	}

	@observable private _email: string = null;

	@TypeSystem.propertyDecorator('string')
	public set email(val: string) {
		this._email = val;
	}


	public get email(): string {
		return this._email;
	}

	@observable private _fullName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set fullName(val: string) {
		this._fullName = val;
	}


	public get fullName(): string {
		return this._fullName;
	}

	@observable private _displayName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set displayName(val: string) {
		this._displayName = val;
	}


	public get displayName(): string {
		return this._displayName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DataServiceLimitsSearchCriteria', moduleContext.moduleName)
export class DataServiceLimitsSearchCriteria extends BasePagedSearchCriteria {
	@observable private _serviceLimitIDs: number[] = null;

	@TypeSystem.propertyArrayDecorator('number')
	public set serviceLimitIDs(val: number[]) {
		this._serviceLimitIDs = val;
	}


	public get serviceLimitIDs(): number[] {
		return this._serviceLimitIDs;
	}

	@observable private _serviceCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serviceCode(val: string) {
		this._serviceCode = val;
	}


	public get serviceCode(): string {
		return this._serviceCode;
	}

	@observable private _serviceName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serviceName(val: string) {
		this._serviceName = val;
	}


	public get serviceName(): string {
		return this._serviceName;
	}

	@observable private _status: DataServiceLimitStatus = null;

	@TypeSystem.propertyDecorator(DataServiceLimitStatus ? DataServiceLimitStatus : moduleContext.moduleName + '.DataServiceLimitStatus')
	public set status(val: DataServiceLimitStatus) {
		this._status = val;
	}


	public get status(): DataServiceLimitStatus {
		return this._status;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DataServiceLimit', moduleContext.moduleName)
export class DataServiceLimit extends BaseDataModel {
	@observable private _serviceLimitID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set serviceLimitID(val: number) {
		this._serviceLimitID = val;
	}


	public get serviceLimitID(): number {
		return this._serviceLimitID;
	}

	@observable private _serviceCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serviceCode(val: string) {
		this._serviceCode = val;
	}


	public get serviceCode(): string {
		return this._serviceCode;
	}

	@observable private _serviceName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serviceName(val: string) {
		this._serviceName = val;
	}


	public get serviceName(): string {
		return this._serviceName;
	}

	@observable private _requestsIntervalFromStartDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set requestsIntervalFromStartDate(val: moment.Moment) {
		this._requestsIntervalFromStartDate = val;
	}


	public get requestsIntervalFromStartDate(): moment.Moment {
		return this._requestsIntervalFromStartDate;
	}

	@observable private _requestsInterval: string = null;

	@TypeSystem.propertyDecorator('string')
	public set requestsInterval(val: string) {
		this._requestsInterval = val;
	}


	public get requestsInterval(): string {
		return this._requestsInterval;
	}

	@observable private _requestsNumber: number = null;

	@TypeSystem.propertyDecorator('number')
	public set requestsNumber(val: number) {
		this._requestsNumber = val;
	}


	public get requestsNumber(): number {
		return this._requestsNumber;
	}

	@observable private _status: DataServiceLimitStatus = null;

	@TypeSystem.propertyDecorator(DataServiceLimitStatus ? DataServiceLimitStatus : moduleContext.moduleName + '.DataServiceLimitStatus')
	public set status(val: DataServiceLimitStatus) {
		this._status = val;
	}


	public get status(): DataServiceLimitStatus {
		return this._status;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RegistrationDataSearchCriteria', moduleContext.moduleName)
export class RegistrationDataSearchCriteria extends BasePagedSearchCriteria {
	@observable private _registrationDataIDs: number[] = null;

	@TypeSystem.propertyArrayDecorator('number')
	public set registrationDataIDs(val: number[]) {
		this._registrationDataIDs = val;
	}


	public get registrationDataIDs(): number[] {
		return this._registrationDataIDs;
	}

	@observable private _type: RegistrationDataTypes = null;

	@TypeSystem.propertyDecorator(RegistrationDataTypes ? RegistrationDataTypes : moduleContext.moduleName + '.RegistrationDataTypes')
	public set type(val: RegistrationDataTypes) {
		this._type = val;
	}


	public get type(): RegistrationDataTypes {
		return this._type;
	}

	@observable private _iban: string = null;

	@TypeSystem.propertyDecorator('string')
	public set iban(val: string) {
		this._iban = val;
	}


	public get iban(): string {
		return this._iban;
	}

	@observable private _cin: string = null;

	@TypeSystem.propertyDecorator('string')
	public set cin(val: string) {
		this._cin = val;
	}


	public get cin(): string {
		return this._cin;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RegistrationData', moduleContext.moduleName)
export class RegistrationData extends BaseDataModel {
	@observable private _registrationDataID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set registrationDataID(val: number) {
		this._registrationDataID = val;
	}


	public get registrationDataID(): number {
		return this._registrationDataID;
	}

	@observable private _type: RegistrationDataTypes = null;

	@TypeSystem.propertyDecorator(RegistrationDataTypes ? RegistrationDataTypes : moduleContext.moduleName + '.RegistrationDataTypes')
	public set type(val: RegistrationDataTypes) {
		this._type = val;
	}


	public get type(): RegistrationDataTypes {
		return this._type;
	}

	@observable private _description: string = null;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}


	public get description(): string {
		return this._description;
	}

	@observable private _iban: string = null;

	@TypeSystem.propertyDecorator('string')
	public set iban(val: string) {
		this._iban = val;
	}


	public get iban(): string {
		return this._iban;
	}

	@observable private _cin: string = null;

	@TypeSystem.propertyDecorator('string')
	public set cin(val: string) {
		this._cin = val;
	}


	public get cin(): string {
		return this._cin;
	}

	@observable private _email: string = null;

	@TypeSystem.propertyDecorator('string')
	public set email(val: string) {
		this._email = val;
	}


	public get email(): string {
		return this._email;
	}

	@observable private _secretWord: string = null;

	@TypeSystem.propertyDecorator('string')
	public set secretWord(val: string) {
		this._secretWord = val;
	}


	public get secretWord(): string {
		return this._secretWord;
	}

	@observable private _validityPeriod: string = null;

	@TypeSystem.propertyDecorator('string')
	public set validityPeriod(val: string) {
		this._validityPeriod = val;
	}


	public get validityPeriod(): string {
		return this._validityPeriod;
	}

	@observable private _portalUrl: string = null;

	@TypeSystem.propertyDecorator('string')
	public set portalUrl(val: string) {
		this._portalUrl = val;
	}


	public get portalUrl(): string {
		return this._portalUrl;
	}

	@observable private _notificationUrl: string = null;

	@TypeSystem.propertyDecorator('string')
	public set notificationUrl(val: string) {
		this._notificationUrl = val;
	}


	public get notificationUrl(): string {
		return this._notificationUrl;
	}

	@observable private _serviceUrl: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serviceUrl(val: string) {
		this._serviceUrl = val;
	}


	public get serviceUrl(): string {
		return this._serviceUrl;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ActionType', moduleContext.moduleName)
export class ActionType extends BaseDataModel {
	@observable private _actionTypeID: ActionTypes = null;

	@TypeSystem.propertyDecorator(ActionTypes ? ActionTypes : moduleContext.moduleName + '.ActionTypes')
	public set actionTypeID(val: ActionTypes) {
		this._actionTypeID = val;
	}


	public get actionTypeID(): ActionTypes {
		return this._actionTypeID;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _description: string = null;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}


	public get description(): string {
		return this._description;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ObjectType', moduleContext.moduleName)
export class ObjectType extends BaseDataModel {
	@observable private _objectTypeID: ObjectTypes = null;

	@TypeSystem.propertyDecorator(ObjectTypes ? ObjectTypes : moduleContext.moduleName + '.ObjectTypes')
	public set objectTypeID(val: ObjectTypes) {
		this._objectTypeID = val;
	}


	public get objectTypeID(): ObjectTypes {
		return this._objectTypeID;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _description: string = null;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}


	public get description(): string {
		return this._description;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PaymentsObligationsSearchCriteria', moduleContext.moduleName)
export class PaymentsObligationsSearchCriteria extends BasePagedSearchCriteria {
	@observable private _dateFrom: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set dateFrom(val: moment.Moment) {
		this._dateFrom = val;
	}


	public get dateFrom(): moment.Moment {
		return this._dateFrom;
	}

	@observable private _dateTo: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set dateTo(val: moment.Moment) {
		this._dateTo = val;
	}


	public get dateTo(): moment.Moment {
		return this._dateTo;
	}

	@observable private _debtorIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set debtorIdentifier(val: string) {
		this._debtorIdentifier = val;
	}


	public get debtorIdentifier(): string {
		return this._debtorIdentifier;
	}

	@observable private _debtorIdentifierType: ObligedPersonIdentTypes = null;

	@TypeSystem.propertyDecorator(ObligedPersonIdentTypes ? ObligedPersonIdentTypes : moduleContext.moduleName + '.ObligedPersonIdentTypes')
	public set debtorIdentifierType(val: ObligedPersonIdentTypes) {
		this._debtorIdentifierType = val;
	}


	public get debtorIdentifierType(): ObligedPersonIdentTypes {
		return this._debtorIdentifierType;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PaymentsObligationsData', moduleContext.moduleName)
export class PaymentsObligationsData extends BaseDataModel {
	@observable private _registrationDataType: RegistrationDataTypes = null;

	@TypeSystem.propertyDecorator(RegistrationDataTypes ? RegistrationDataTypes : moduleContext.moduleName + '.RegistrationDataTypes')
	public set registrationDataType(val: RegistrationDataTypes) {
		this._registrationDataType = val;
	}


	public get registrationDataType(): RegistrationDataTypes {
		return this._registrationDataType;
	}

	@observable private _sendDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set sendDate(val: moment.Moment) {
		this._sendDate = val;
	}


	public get sendDate(): moment.Moment {
		return this._sendDate;
	}

	@observable private _amount: number = null;

	@TypeSystem.propertyDecorator('number')
	public set amount(val: number) {
		this._amount = val;
	}


	public get amount(): number {
		return this._amount;
	}

	@observable private _paymentRequestData: any = null;

	@TypeSystem.propertyDecorator('any')
	public set paymentRequestData(val: any) {
		this._paymentRequestData = val;
	}


	public get paymentRequestData(): any {
		return this._paymentRequestData;
	}

	@observable private _externalPortalPaymentNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set externalPortalPaymentNumber(val: string) {
		this._externalPortalPaymentNumber = val;
	}


	public get externalPortalPaymentNumber(): string {
		return this._externalPortalPaymentNumber;
	}

	@observable private _status: PaymentRequestStatuses = null;

	@TypeSystem.propertyDecorator(PaymentRequestStatuses ? PaymentRequestStatuses : moduleContext.moduleName + '.PaymentRequestStatuses')
	public set status(val: PaymentRequestStatuses) {
		this._status = val;
	}


	public get status(): PaymentRequestStatuses {
		return this._status;
	}

	@observable private _payDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set payDate(val: moment.Moment) {
		this._payDate = val;
	}


	public get payDate(): moment.Moment {
		return this._payDate;
	}

	@observable private _payerIdentType: ObligedPersonIdentTypes = null;

	@TypeSystem.propertyDecorator(ObligedPersonIdentTypes ? ObligedPersonIdentTypes : moduleContext.moduleName + '.ObligedPersonIdentTypes')
	public set payerIdentType(val: ObligedPersonIdentTypes) {
		this._payerIdentType = val;
	}


	public get payerIdentType(): ObligedPersonIdentTypes {
		return this._payerIdentType;
	}

	@observable private _payerIdent: string = null;

	@TypeSystem.propertyDecorator('string')
	public set payerIdent(val: string) {
		this._payerIdent = val;
	}


	public get payerIdent(): string {
		return this._payerIdent;
	}

	@observable private _obligationData: any = null;

	@TypeSystem.propertyDecorator('any')
	public set obligationData(val: any) {
		this._obligationData = val;
	}


	public get obligationData(): any {
		return this._obligationData;
	}

	@observable private _obligedPersonIdentType: ObligedPersonIdentTypes = null;

	@TypeSystem.propertyDecorator(ObligedPersonIdentTypes ? ObligedPersonIdentTypes : moduleContext.moduleName + '.ObligedPersonIdentTypes')
	public set obligedPersonIdentType(val: ObligedPersonIdentTypes) {
		this._obligedPersonIdentType = val;
	}


	public get obligedPersonIdentType(): ObligedPersonIdentTypes {
		return this._obligedPersonIdentType;
	}

	@observable private _obligedPersonIdent: string = null;

	@TypeSystem.propertyDecorator('string')
	public set obligedPersonIdent(val: string) {
		this._obligedPersonIdent = val;
	}


	public get obligedPersonIdent(): string {
		return this._obligedPersonIdent;
	}

	@observable private _andSourceId: ANDSourceIds = null;

	@TypeSystem.propertyDecorator(ANDSourceIds ? ANDSourceIds : moduleContext.moduleName + '.ANDSourceIds')
	public set andSourceId(val: ANDSourceIds) {
		this._andSourceId = val;
	}


	public get andSourceId(): ANDSourceIds {
		return this._andSourceId;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotaryInterestsForPersonOrVehicleSearchCriteria', moduleContext.moduleName)
export class NotaryInterestsForPersonOrVehicleSearchCriteria extends BasePagedSearchCriteria {
	@observable private _dateFrom: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set dateFrom(val: moment.Moment) {
		this._dateFrom = val;
	}


	public get dateFrom(): moment.Moment {
		return this._dateFrom;
	}

	@observable private _dateTo: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set dateTo(val: moment.Moment) {
		this._dateTo = val;
	}


	public get dateTo(): moment.Moment {
		return this._dateTo;
	}

	@observable private _vehicleRegNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set vehicleRegNumber(val: string) {
		this._vehicleRegNumber = val;
	}


	public get vehicleRegNumber(): string {
		return this._vehicleRegNumber;
	}

	@observable private _personIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set personIdentifier(val: string) {
		this._personIdentifier = val;
	}


	public get personIdentifier(): string {
		return this._personIdentifier;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotaryInterestsForPersonOrVehicleReportData', moduleContext.moduleName)
export class NotaryInterestsForPersonOrVehicleReportData extends BaseDataModel {
	@observable private _notaryUserEmail: string = null;

	@TypeSystem.propertyDecorator('string')
	public set notaryUserEmail(val: string) {
		this._notaryUserEmail = val;
	}


	public get notaryUserEmail(): string {
		return this._notaryUserEmail;
	}

	@observable private _notaryUserNames: string = null;

	@TypeSystem.propertyDecorator('string')
	public set notaryUserNames(val: string) {
		this._notaryUserNames = val;
	}


	public get notaryUserNames(): string {
		return this._notaryUserNames;
	}

	@observable private _notaryUserIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set notaryUserIdentifier(val: string) {
		this._notaryUserIdentifier = val;
	}


	public get notaryUserIdentifier(): string {
		return this._notaryUserIdentifier;
	}

	@observable private _documentAccessedDataValues: DocumentAccessedDataValue[] = null;

	@TypeSystem.propertyArrayDecorator(DocumentAccessedDataValue ? DocumentAccessedDataValue : moduleContext.moduleName + '.DocumentAccessedDataValue')
	public set documentAccessedDataValues(val: DocumentAccessedDataValue[]) {
		this._documentAccessedDataValues = val;
	}


	public get documentAccessedDataValues(): DocumentAccessedDataValue[] {
		return this._documentAccessedDataValues;
	}

	@observable private _ipAddress: string = null;

	@TypeSystem.propertyDecorator('string')
	public set ipAddress(val: string) {
		this._ipAddress = val;
	}


	public get ipAddress(): string {
		return this._ipAddress;
	}

	@observable private _interestDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set interestDate(val: moment.Moment) {
		this._interestDate = val;
	}


	public get interestDate(): moment.Moment {
		return this._interestDate;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DocumentAccessedDataValue', moduleContext.moduleName)
export class DocumentAccessedDataValue extends BaseDataModel {
	@observable private _documentAccessedDataId: number = null;

	@TypeSystem.propertyDecorator('number')
	public set documentAccessedDataId(val: number) {
		this._documentAccessedDataId = val;
	}


	public get documentAccessedDataId(): number {
		return this._documentAccessedDataId;
	}

	@observable private _dataType: DocumentAccessedDataTypes = null;

	@TypeSystem.propertyDecorator(DocumentAccessedDataTypes ? DocumentAccessedDataTypes : moduleContext.moduleName + '.DocumentAccessedDataTypes')
	public set dataType(val: DocumentAccessedDataTypes) {
		this._dataType = val;
	}


	public get dataType(): DocumentAccessedDataTypes {
		return this._dataType;
	}

	@observable private _dataValue: string = null;

	@TypeSystem.propertyDecorator('string')
	public set dataValue(val: string) {
		this._dataValue = val;
	}


	public get dataValue(): string {
		return this._dataValue;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}

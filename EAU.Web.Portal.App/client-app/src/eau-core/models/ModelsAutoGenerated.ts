import moment from 'moment';
import { observable } from 'mobx';
import { TypeSystem, BaseDataModel, BasePagedSearchCriteria } from 'cnsys-core';
import { moduleContext } from '../ModuleContext';

export enum DocumentTypes {
	EDocument = 1,
	AttachableDocument = 2,
} 
TypeSystem.registerEnumInfo(DocumentTypes, 'DocumentTypes', moduleContext.moduleName); 

export enum EkatteTypes {
	Undefined = 0,
	District = 1,
	Municipality = 2,
	Settlement = 3,
	Mayoralty = 4,
	Area = 5,
} 
TypeSystem.registerEnumInfo(EkatteTypes, 'EkatteTypes', moduleContext.moduleName); 

export enum GraoTypes {
	Undefined = 0,
	District = 1,
	Municipality = 2,
	Settlement = 3,
} 
TypeSystem.registerEnumInfo(GraoTypes, 'GraoTypes', moduleContext.moduleName); 

export enum AppParameterTypes {
	DateTime = 1,
	Interval = 2,
	String = 3,
	Integer = 4,
	HourAndMinute = 5,
} 
TypeSystem.registerEnumInfo(AppParameterTypes, 'AppParameterTypes', moduleContext.moduleName); 

export enum Functionalities {
	Audit = 1,
	Users = 2,
	Signing = 3,
	SystemLimits = 4,
	Portal = 5,
} 
TypeSystem.registerEnumInfo(Functionalities, 'Functionalities', moduleContext.moduleName); 

export enum ServiceInstanceStatuses {
	InProcess = 1,
	Completed = 2,
	Rejected = 3,
} 
TypeSystem.registerEnumInfo(ServiceInstanceStatuses, 'ServiceInstanceStatuses', moduleContext.moduleName); 

export enum ANDObligationSearchMode {
	ObligedPerson = 1,
	Document = 2,
} 
TypeSystem.registerEnumInfo(ANDObligationSearchMode, 'ANDObligationSearchMode', moduleContext.moduleName); 

export enum ObligationSearchModes {
	MyPayments = 1,
	AND = 2,
	ServiceInstances = 3,
} 
TypeSystem.registerEnumInfo(ObligationSearchModes, 'ObligationSearchModes', moduleContext.moduleName); 

export enum ObligationTypes {
	ServiceInstance = 1,
	AND = 2,
} 
TypeSystem.registerEnumInfo(ObligationTypes, 'ObligationTypes', moduleContext.moduleName); 

export enum KATDocumentTypes {
	TICKET = 1,
	PENAL_DECREE = 2,
	AGREEMENT = 3,
} 
TypeSystem.registerEnumInfo(KATDocumentTypes, 'KATDocumentTypes', moduleContext.moduleName); 

export enum ObligationStatuses {
	Pending = 0,
	InProcess = 1,
	Paid = 2,
	Processed = 3,
} 
TypeSystem.registerEnumInfo(ObligationStatuses, 'ObligationStatuses', moduleContext.moduleName); 

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

export enum RegistrationDataTypes {
	ePay = 1,
	PepOfDaeu = 2,
} 
TypeSystem.registerEnumInfo(RegistrationDataTypes, 'RegistrationDataTypes', moduleContext.moduleName); 

export enum PaymentRequestStatuses {
	New = 1,
	Sent = 2,
	Paid = 3,
	Cancelled = 4,
	Expired = 5,
	Duplicate = 6,
} 
TypeSystem.registerEnumInfo(PaymentRequestStatuses, 'PaymentRequestStatuses', moduleContext.moduleName); 

export enum UserPermissions {
	ADM_USERS = 1,
	ADM_CMS = 2,
	ADM_NOM = 3,
	ADM_AUDIT = 4,
	ADM_PARAM_LIMIT = 5,
} 
TypeSystem.registerEnumInfo(UserPermissions, 'UserPermissions', moduleContext.moduleName); 

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

@TypeSystem.typeDecorator('AppParameterSearchCriteria', moduleContext.moduleName)
export class AppParameterSearchCriteria extends BasePagedSearchCriteria {
	@observable private _functionalityID: Functionalities = null;

	@TypeSystem.propertyDecorator(Functionalities ? Functionalities : moduleContext.moduleName + '.Functionalities')
	public set functionalityID(val: Functionalities) {
		this._functionalityID = val;
	}


	public get functionalityID(): Functionalities {
		return this._functionalityID;
	}

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
	}

	@observable private _codeIsExact: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set codeIsExact(val: boolean) {
		this._codeIsExact = val;
	}


	public get codeIsExact(): boolean {
		return this._codeIsExact;
	}

	@observable private _description: string = null;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}


	public get description(): string {
		return this._description;
	}

	@observable private _isSystem: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isSystem(val: boolean) {
		this._isSystem = val;
	}


	public get isSystem(): boolean {
		return this._isSystem;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DeclarationSearchCriteria', moduleContext.moduleName)
export class DeclarationSearchCriteria extends BasePagedSearchCriteria {
	@observable private _id: number = null;

	@TypeSystem.propertyDecorator('number')
	public set id(val: number) {
		this._id = val;
	}


	public get id(): number {
		return this._id;
	}

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
@TypeSystem.typeDecorator('DocumentTemplateSearchCriteria', moduleContext.moduleName)
export class DocumentTemplateSearchCriteria extends BasePagedSearchCriteria {
	@observable private _documentTypeID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set documentTypeID(val: number) {
		this._documentTypeID = val;
	}


	public get documentTypeID(): number {
		return this._documentTypeID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceSearchCriteria', moduleContext.moduleName)
export class ServiceSearchCriteria extends BasePagedSearchCriteria {
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

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
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

	@observable private _isActive: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isActive(val: boolean) {
		this._isActive = val;
	}


	public get isActive(): boolean {
		return this._isActive;
	}

	@observable private _sunauServiceUri: string = null;

	@TypeSystem.propertyDecorator('string')
	public set sunauServiceUri(val: string) {
		this._sunauServiceUri = val;
	}


	public get sunauServiceUri(): string {
		return this._sunauServiceUri;
	}

	@observable private _groupID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set groupID(val: number) {
		this._groupID = val;
	}


	public get groupID(): number {
		return this._groupID;
	}

	@observable private _attachedDocumentType: DocumentType = null;

	@TypeSystem.propertyDecorator(DocumentType ? DocumentType : moduleContext.moduleName + '.DocumentType')
	public set attachedDocumentType(val: DocumentType) {
		this._attachedDocumentType = val;
	}


	public get attachedDocumentType(): DocumentType {
		return this._attachedDocumentType;
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
@TypeSystem.typeDecorator('PageSearchCriteria', moduleContext.moduleName)
export class PageSearchCriteria extends BasePagedSearchCriteria {
	@observable private _pageIDs: number[] = null;

	@TypeSystem.propertyArrayDecorator('number')
	public set pageIDs(val: number[]) {
		this._pageIDs = val;
	}


	public get pageIDs(): number[] {
		return this._pageIDs;
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
@TypeSystem.typeDecorator('AppParameter', moduleContext.moduleName)
export class AppParameter extends BaseDataModel {
	@observable private _appParamID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set appParamID(val: number) {
		this._appParamID = val;
	}


	public get appParamID(): number {
		return this._appParamID;
	}

	@observable private _functionalityID: Functionalities = null;

	@TypeSystem.propertyDecorator(Functionalities ? Functionalities : moduleContext.moduleName + '.Functionalities')
	public set functionalityID(val: Functionalities) {
		this._functionalityID = val;
	}


	public get functionalityID(): Functionalities {
		return this._functionalityID;
	}

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
	}

	@observable private _description: string = null;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}


	public get description(): string {
		return this._description;
	}

	@observable private _isSystem: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isSystem(val: boolean) {
		this._isSystem = val;
	}


	public get isSystem(): boolean {
		return this._isSystem;
	}

	@observable private _parameterType: AppParameterTypes = null;

	@TypeSystem.propertyDecorator(AppParameterTypes ? AppParameterTypes : moduleContext.moduleName + '.AppParameterTypes')
	public set parameterType(val: AppParameterTypes) {
		this._parameterType = val;
	}


	public get parameterType(): AppParameterTypes {
		return this._parameterType;
	}

	@observable private _valueDateTime: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set valueDateTime(val: moment.Moment) {
		this._valueDateTime = val;
	}


	public get valueDateTime(): moment.Moment {
		return this._valueDateTime;
	}

	@observable private _valueIntervalFromStartDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set valueIntervalFromStartDate(val: moment.Moment) {
		this._valueIntervalFromStartDate = val;
	}


	public get valueIntervalFromStartDate(): moment.Moment {
		return this._valueIntervalFromStartDate;
	}

	@observable private _valueInterval: string = null;

	@TypeSystem.propertyDecorator('string')
	public set valueInterval(val: string) {
		this._valueInterval = val;
	}


	public get valueInterval(): string {
		return this._valueInterval;
	}

	@observable private _valueString: string = null;

	@TypeSystem.propertyDecorator('string')
	public set valueString(val: string) {
		this._valueString = val;
	}


	public get valueString(): string {
		return this._valueString;
	}

	@observable private _valueInt: number = null;

	@TypeSystem.propertyDecorator('number')
	public set valueInt(val: number) {
		this._valueInt = val;
	}


	public get valueInt(): number {
		return this._valueInt;
	}

	@observable private _valueHour: string = null;

	@TypeSystem.propertyDecorator('string')
	public set valueHour(val: string) {
		this._valueHour = val;
	}


	public get valueHour(): string {
		return this._valueHour;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DocumentTemplate', moduleContext.moduleName)
export class DocumentTemplate extends BaseDataModel {
	@observable private _docTemplateID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set docTemplateID(val: number) {
		this._docTemplateID = val;
	}


	public get docTemplateID(): number {
		return this._docTemplateID;
	}

	@observable private _documentTypeID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set documentTypeID(val: number) {
		this._documentTypeID = val;
	}


	public get documentTypeID(): number {
		return this._documentTypeID;
	}

	@observable private _content: string = null;

	@TypeSystem.propertyDecorator('string')
	public set content(val: string) {
		this._content = val;
	}


	public get content(): string {
		return this._content;
	}

	@observable private _isSubmittedAccordingToTemplate: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isSubmittedAccordingToTemplate(val: boolean) {
		this._isSubmittedAccordingToTemplate = val;
	}


	public get isSubmittedAccordingToTemplate(): boolean {
		return this._isSubmittedAccordingToTemplate;
	}

	@observable private _updatedOn: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set updatedOn(val: moment.Moment) {
		this._updatedOn = val;
	}


	public get updatedOn(): moment.Moment {
		return this._updatedOn;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DocumentTemplateField', moduleContext.moduleName)
export class DocumentTemplateField extends BaseDataModel {
	@observable private _key: string = null;

	@TypeSystem.propertyDecorator('string')
	public set key(val: string) {
		this._key = val;
	}


	public get key(): string {
		return this._key;
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
@TypeSystem.typeDecorator('DocumentType', moduleContext.moduleName)
export class DocumentType extends BaseDataModel {
	@observable private _documentTypeID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set documentTypeID(val: number) {
		this._documentTypeID = val;
	}


	public get documentTypeID(): number {
		return this._documentTypeID;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _uri: string = null;

	@TypeSystem.propertyDecorator('string')
	public set uri(val: string) {
		this._uri = val;
	}


	public get uri(): string {
		return this._uri;
	}

	@observable private _type: DocumentTypes = null;

	@TypeSystem.propertyDecorator(DocumentTypes ? DocumentTypes : moduleContext.moduleName + '.DocumentTypes')
	public set type(val: DocumentTypes) {
		this._type = val;
	}


	public get type(): DocumentTypes {
		return this._type;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Label', moduleContext.moduleName)
export class Label extends BaseDataModel {
	@observable private _labelID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set labelID(val: number) {
		this._labelID = val;
	}


	public get labelID(): number {
		return this._labelID;
	}

	@observable private _isTranslated: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isTranslated(val: boolean) {
		this._isTranslated = val;
	}


	public get isTranslated(): boolean {
		return this._isTranslated;
	}

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
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

	@observable private _languageCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set languageCode(val: string) {
		this._languageCode = val;
	}


	public get languageCode(): string {
		return this._languageCode;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Language', moduleContext.moduleName)
export class Language extends BaseDataModel {
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

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _isActive: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isActive(val: boolean) {
		this._isActive = val;
	}


	public get isActive(): boolean {
		return this._isActive;
	}

	@observable private _isDefault: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isDefault(val: boolean) {
		this._isDefault = val;
	}


	public get isDefault(): boolean {
		return this._isDefault;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceGroup', moduleContext.moduleName)
export class ServiceGroup extends BaseDataModel {
	@observable private _groupID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set groupID(val: number) {
		this._groupID = val;
	}


	public get groupID(): number {
		return this._groupID;
	}

	@observable private _isTranslated: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isTranslated(val: boolean) {
		this._isTranslated = val;
	}


	public get isTranslated(): boolean {
		return this._isTranslated;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _orderNumber: number = null;

	@TypeSystem.propertyDecorator('number')
	public set orderNumber(val: number) {
		this._orderNumber = val;
	}


	public get orderNumber(): number {
		return this._orderNumber;
	}

	@observable private _iconName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set iconName(val: string) {
		this._iconName = val;
	}


	public get iconName(): string {
		return this._iconName;
	}

	@observable private _isActive: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isActive(val: boolean) {
		this._isActive = val;
	}


	public get isActive(): boolean {
		return this._isActive;
	}

	@observable private _languageCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set languageCode(val: string) {
		this._languageCode = val;
	}


	public get languageCode(): string {
		return this._languageCode;
	}

	@observable private _updatedOn: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set updatedOn(val: moment.Moment) {
		this._updatedOn = val;
	}


	public get updatedOn(): moment.Moment {
		return this._updatedOn;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Ekatte', moduleContext.moduleName)
export class Ekatte extends BaseDataModel {
	@observable private _ekatteID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set ekatteID(val: number) {
		this._ekatteID = val;
	}


	public get ekatteID(): number {
		return this._ekatteID;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _ekatteTypeID: EkatteTypes = null;

	@TypeSystem.propertyDecorator(EkatteTypes ? EkatteTypes : moduleContext.moduleName + '.EkatteTypes')
	public set ekatteTypeID(val: EkatteTypes) {
		this._ekatteTypeID = val;
	}


	public get ekatteTypeID(): EkatteTypes {
		return this._ekatteTypeID;
	}

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
	}

	@observable private _parentID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set parentID(val: number) {
		this._parentID = val;
	}


	public get parentID(): number {
		return this._parentID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Grao', moduleContext.moduleName)
export class Grao extends BaseDataModel {
	@observable private _graoID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set graoID(val: number) {
		this._graoID = val;
	}


	public get graoID(): number {
		return this._graoID;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _graoTypeID: GraoTypes = null;

	@TypeSystem.propertyDecorator(GraoTypes ? GraoTypes : moduleContext.moduleName + '.GraoTypes')
	public set graoTypeID(val: GraoTypes) {
		this._graoTypeID = val;
	}


	public get graoTypeID(): GraoTypes {
		return this._graoTypeID;
	}

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
	}

	@observable private _parentID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set parentID(val: number) {
		this._parentID = val;
	}


	public get parentID(): number {
		return this._parentID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Page', moduleContext.moduleName)
export class Page extends BaseDataModel {
	@observable private _pageID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set pageID(val: number) {
		this._pageID = val;
	}


	public get pageID(): number {
		return this._pageID;
	}

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
	}

	@observable private _title: string = null;

	@TypeSystem.propertyDecorator('string')
	public set title(val: string) {
		this._title = val;
	}


	public get title(): string {
		return this._title;
	}

	@observable private _content: string = null;

	@TypeSystem.propertyDecorator('string')
	public set content(val: string) {
		this._content = val;
	}


	public get content(): string {
		return this._content;
	}

	@observable private _updatedOn: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set updatedOn(val: moment.Moment) {
		this._updatedOn = val;
	}


	public get updatedOn(): moment.Moment {
		return this._updatedOn;
	}

	@observable private _isTranslated: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isTranslated(val: boolean) {
		this._isTranslated = val;
	}


	public get isTranslated(): boolean {
		return this._isTranslated;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Functionality', moduleContext.moduleName)
export class Functionality extends BaseDataModel {
	@observable private _functionalityID: Functionalities = null;

	@TypeSystem.propertyDecorator(Functionalities ? Functionalities : moduleContext.moduleName + '.Functionalities')
	public set functionalityID(val: Functionalities) {
		this._functionalityID = val;
	}


	public get functionalityID(): Functionalities {
		return this._functionalityID;
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
@TypeSystem.typeDecorator('UserPermission', moduleContext.moduleName)
export class UserPermission extends BaseDataModel {
	@observable private _userID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set userID(val: number) {
		this._userID = val;
	}


	public get userID(): number {
		return this._userID;
	}

	@observable private _permissionID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set permissionID(val: number) {
		this._permissionID = val;
	}


	public get permissionID(): number {
		return this._permissionID;
	}

	@observable private _permission: UserPermissions = null;

	@TypeSystem.propertyDecorator(UserPermissions ? UserPermissions : moduleContext.moduleName + '.UserPermissions')
	public set permission(val: UserPermissions) {
		this._permission = val;
	}


	public get permission(): UserPermissions {
		return this._permission;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('UserAuthentication', moduleContext.moduleName)
export class UserAuthentication extends BaseDataModel {
	@observable private _authenticationID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set authenticationID(val: number) {
		this._authenticationID = val;
	}


	public get authenticationID(): number {
		return this._authenticationID;
	}

	@observable private _userID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set userID(val: number) {
		this._userID = val;
	}


	public get userID(): number {
		return this._userID;
	}

	@observable private _authenticationType: AuthenticationTypes = null;

	@TypeSystem.propertyDecorator(AuthenticationTypes ? AuthenticationTypes : moduleContext.moduleName + '.AuthenticationTypes')
	public set authenticationType(val: AuthenticationTypes) {
		this._authenticationType = val;
	}


	public get authenticationType(): AuthenticationTypes {
		return this._authenticationType;
	}

	@observable private _passwordHash: string = null;

	@TypeSystem.propertyDecorator('string')
	public set passwordHash(val: string) {
		this._passwordHash = val;
	}


	public get passwordHash(): string {
		return this._passwordHash;
	}

	@observable private _username: string = null;

	@TypeSystem.propertyDecorator('string')
	public set username(val: string) {
		this._username = val;
	}


	public get username(): string {
		return this._username;
	}

	@observable private _certificateID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set certificateID(val: number) {
		this._certificateID = val;
	}


	public get certificateID(): number {
		return this._certificateID;
	}

	@observable private _isLocked: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isLocked(val: boolean) {
		this._isLocked = val;
	}


	public get isLocked(): boolean {
		return this._isLocked;
	}

	@observable private _lockedUntil: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set lockedUntil(val: moment.Moment) {
		this._lockedUntil = val;
	}


	public get lockedUntil(): moment.Moment {
		return this._lockedUntil;
	}

	@observable private _isActive: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isActive(val: boolean) {
		this._isActive = val;
	}


	public get isActive(): boolean {
		return this._isActive;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Country', moduleContext.moduleName)
export class Country extends BaseDataModel {
	@observable private _countryID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set countryID(val: number) {
		this._countryID = val;
	}


	public get countryID(): number {
		return this._countryID;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceInstanceLoadOption', moduleContext.moduleName)
export class ServiceInstanceLoadOption extends BaseDataModel {
	@observable private _loadWithLock: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set loadWithLock(val: boolean) {
		this._loadWithLock = val;
	}


	public get loadWithLock(): boolean {
		return this._loadWithLock;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceInstanceSearchCriteria', moduleContext.moduleName)
export class ServiceInstanceSearchCriteria extends BasePagedSearchCriteria {
	@observable private _serviceInstanceIDs: number[] = null;

	@TypeSystem.propertyArrayDecorator('number')
	public set serviceInstanceIDs(val: number[]) {
		this._serviceInstanceIDs = val;
	}


	public get serviceInstanceIDs(): number[] {
		return this._serviceInstanceIDs;
	}

	@observable private _status: ServiceInstanceStatuses = null;

	@TypeSystem.propertyDecorator(ServiceInstanceStatuses ? ServiceInstanceStatuses : moduleContext.moduleName + '.ServiceInstanceStatuses')
	public set status(val: ServiceInstanceStatuses) {
		this._status = val;
	}


	public get status(): ServiceInstanceStatuses {
		return this._status;
	}

	@observable private _applicantID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set applicantID(val: number) {
		this._applicantID = val;
	}


	public get applicantID(): number {
		return this._applicantID;
	}

	@observable private _serviceInstanceDateFrom: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set serviceInstanceDateFrom(val: moment.Moment) {
		this._serviceInstanceDateFrom = val;
	}


	public get serviceInstanceDateFrom(): moment.Moment {
		return this._serviceInstanceDateFrom;
	}

	@observable private _serviceInstanceDateTo: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set serviceInstanceDateTo(val: moment.Moment) {
		this._serviceInstanceDateTo = val;
	}


	public get serviceInstanceDateTo(): moment.Moment {
		return this._serviceInstanceDateTo;
	}

	@observable private _serviceID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set serviceID(val: number) {
		this._serviceID = val;
	}


	public get serviceID(): number {
		return this._serviceID;
	}

	@observable private _caseFileURI: string = null;

	@TypeSystem.propertyDecorator('string')
	public set caseFileURI(val: string) {
		this._caseFileURI = val;
	}


	public get caseFileURI(): string {
		return this._caseFileURI;
	}

	@observable private _loadOption: ServiceInstanceLoadOption = null;

	@TypeSystem.propertyDecorator(ServiceInstanceLoadOption ? ServiceInstanceLoadOption : moduleContext.moduleName + '.ServiceInstanceLoadOption')
	public set loadOption(val: ServiceInstanceLoadOption) {
		this._loadOption = val;
	}


	public get loadOption(): ServiceInstanceLoadOption {
		return this._loadOption;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('UserAuthenticationInfo', moduleContext.moduleName)
export class UserAuthenticationInfo extends BaseDataModel {
	@observable private _userAuthenticationId: number = null;

	@TypeSystem.propertyDecorator('number')
	public set userAuthenticationId(val: number) {
		this._userAuthenticationId = val;
	}


	public get userAuthenticationId(): number {
		return this._userAuthenticationId;
	}

	@observable private _issuer: string = null;

	@TypeSystem.propertyDecorator('string')
	public set issuer(val: string) {
		this._issuer = val;
	}


	public get issuer(): string {
		return this._issuer;
	}

	@observable private _serialNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serialNumber(val: string) {
		this._serialNumber = val;
	}


	public get serialNumber(): string {
		return this._serialNumber;
	}

	@observable private _subject: string = null;

	@TypeSystem.propertyDecorator('string')
	public set subject(val: string) {
		this._subject = val;
	}


	public get subject(): string {
		return this._subject;
	}

	@observable private _validTo: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set validTo(val: moment.Moment) {
		this._validTo = val;
	}


	public get validTo(): moment.Moment {
		return this._validTo;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('MyPaymentsObligationSearchCriteria', moduleContext.moduleName)
export class MyPaymentsObligationSearchCriteria extends BasePagedSearchCriteria {
	@observable private _serviceInstanceID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set serviceInstanceID(val: number) {
		this._serviceInstanceID = val;
	}


	public get serviceInstanceID(): number {
		return this._serviceInstanceID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SIObligationSearchCriteria', moduleContext.moduleName)
export class SIObligationSearchCriteria extends BaseDataModel {
	@observable private _serviceInstanceID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set serviceInstanceID(val: number) {
		this._serviceInstanceID = val;
	}


	public get serviceInstanceID(): number {
		return this._serviceInstanceID;
	}

	@observable private _paymentInstructionURI: string = null;

	@TypeSystem.propertyDecorator('string')
	public set paymentInstructionURI(val: string) {
		this._paymentInstructionURI = val;
	}


	public get paymentInstructionURI(): string {
		return this._paymentInstructionURI;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ObligationRequest', moduleContext.moduleName)
export class ObligationRequest extends BaseDataModel {
	@observable private _type: ObligationTypes = null;

	@TypeSystem.propertyDecorator(ObligationTypes ? ObligationTypes : moduleContext.moduleName + '.ObligationTypes')
	public set type(val: ObligationTypes) {
		this._type = val;
	}


	public get type(): ObligationTypes {
		return this._type;
	}

	@observable private _applicantID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set applicantID(val: number) {
		this._applicantID = val;
	}


	public get applicantID(): number {
		return this._applicantID;
	}

	@observable private _obligationIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set obligationIdentifier(val: string) {
		this._obligationIdentifier = val;
	}


	public get obligationIdentifier(): string {
		return this._obligationIdentifier;
	}

	@observable private _obligationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set obligationDate(val: moment.Moment) {
		this._obligationDate = val;
	}


	public get obligationDate(): moment.Moment {
		return this._obligationDate;
	}

	@observable private _obligedPersonName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set obligedPersonName(val: string) {
		this._obligedPersonName = val;
	}


	public get obligedPersonName(): string {
		return this._obligedPersonName;
	}

	@observable private _obligedPersonIdent: string = null;

	@TypeSystem.propertyDecorator('string')
	public set obligedPersonIdent(val: string) {
		this._obligedPersonIdent = val;
	}


	public get obligedPersonIdent(): string {
		return this._obligedPersonIdent;
	}

	@observable private _obligedPersonIdentType: ObligedPersonIdentTypes = null;

	@TypeSystem.propertyDecorator(ObligedPersonIdentTypes ? ObligedPersonIdentTypes : moduleContext.moduleName + '.ObligedPersonIdentTypes')
	public set obligedPersonIdentType(val: ObligedPersonIdentTypes) {
		this._obligedPersonIdentType = val;
	}


	public get obligedPersonIdentType(): ObligedPersonIdentTypes {
		return this._obligedPersonIdentType;
	}

	@observable private _obligationSearchCriteria: ObligationSearchCriteria = null;

	@TypeSystem.propertyDecorator(ObligationSearchCriteria ? ObligationSearchCriteria : moduleContext.moduleName + '.ObligationSearchCriteria')
	public set obligationSearchCriteria(val: ObligationSearchCriteria) {
		this._obligationSearchCriteria = val;
	}


	public get obligationSearchCriteria(): ObligationSearchCriteria {
		return this._obligationSearchCriteria;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ObligationSearchCriteria', moduleContext.moduleName)
export class ObligationSearchCriteria extends BasePagedSearchCriteria {
	@observable private _mode: ObligationSearchModes = null;

	@TypeSystem.propertyDecorator(ObligationSearchModes ? ObligationSearchModes : moduleContext.moduleName + '.ObligationSearchModes')
	public set mode(val: ObligationSearchModes) {
		this._mode = val;
	}


	public get mode(): ObligationSearchModes {
		return this._mode;
	}

	@observable private _obligationIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set obligationIdentifier(val: string) {
		this._obligationIdentifier = val;
	}


	public get obligationIdentifier(): string {
		return this._obligationIdentifier;
	}

	@observable private _type: ObligationTypes = null;

	@TypeSystem.propertyDecorator(ObligationTypes ? ObligationTypes : moduleContext.moduleName + '.ObligationTypes')
	public set type(val: ObligationTypes) {
		this._type = val;
	}


	public get type(): ObligationTypes {
		return this._type;
	}

	@observable private _statuses: ObligationStatuses[] = null;

	@TypeSystem.propertyArrayDecorator(ObligationStatuses ? ObligationStatuses : moduleContext.moduleName + '.ObligationStatuses')
	public set statuses(val: ObligationStatuses[]) {
		this._statuses = val;
	}


	public get statuses(): ObligationStatuses[] {
		return this._statuses;
	}

	@observable private _applicantID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set applicantID(val: number) {
		this._applicantID = val;
	}


	public get applicantID(): number {
		return this._applicantID;
	}

	@observable private _obligedPersonIdent: string = null;

	@TypeSystem.propertyDecorator('string')
	public set obligedPersonIdent(val: string) {
		this._obligedPersonIdent = val;
	}


	public get obligedPersonIdent(): string {
		return this._obligedPersonIdent;
	}

	@observable private _drivingLicenceNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set drivingLicenceNumber(val: string) {
		this._drivingLicenceNumber = val;
	}


	public get drivingLicenceNumber(): string {
		return this._drivingLicenceNumber;
	}

	@observable private _personalDocumentNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set personalDocumentNumber(val: string) {
		this._personalDocumentNumber = val;
	}


	public get personalDocumentNumber(): string {
		return this._personalDocumentNumber;
	}

	@observable private _foreignVehicleNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set foreignVehicleNumber(val: string) {
		this._foreignVehicleNumber = val;
	}


	public get foreignVehicleNumber(): string {
		return this._foreignVehicleNumber;
	}

	@observable private _uic: string = null;

	@TypeSystem.propertyDecorator('string')
	public set uic(val: string) {
		this._uic = val;
	}


	public get uic(): string {
		return this._uic;
	}

	@observable private _andSourceId: ANDSourceIds = null;

	@TypeSystem.propertyDecorator(ANDSourceIds ? ANDSourceIds : moduleContext.moduleName + '.ANDSourceIds')
	public set andSourceId(val: ANDSourceIds) {
		this._andSourceId = val;
	}


	public get andSourceId(): ANDSourceIds {
		return this._andSourceId;
	}

	@observable private _documentType: KATDocumentTypes = null;

	@TypeSystem.propertyDecorator(KATDocumentTypes ? KATDocumentTypes : moduleContext.moduleName + '.KATDocumentTypes')
	public set documentType(val: KATDocumentTypes) {
		this._documentType = val;
	}


	public get documentType(): KATDocumentTypes {
		return this._documentType;
	}

	@observable private _documentSeries: string = null;

	@TypeSystem.propertyDecorator('string')
	public set documentSeries(val: string) {
		this._documentSeries = val;
	}


	public get documentSeries(): string {
		return this._documentSeries;
	}

	@observable private _documentNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set documentNumber(val: string) {
		this._documentNumber = val;
	}


	public get documentNumber(): string {
		return this._documentNumber;
	}

	@observable private _initialAmount: number = null;

	@TypeSystem.propertyDecorator('number')
	public set initialAmount(val: number) {
		this._initialAmount = val;
	}


	public get initialAmount(): number {
		return this._initialAmount;
	}

	@observable private _serviceInstanceID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set serviceInstanceID(val: number) {
		this._serviceInstanceID = val;
	}


	public get serviceInstanceID(): number {
		return this._serviceInstanceID;
	}

	@observable private _paymentInstructionURI: string = null;

	@TypeSystem.propertyDecorator('string')
	public set paymentInstructionURI(val: string) {
		this._paymentInstructionURI = val;
	}


	public get paymentInstructionURI(): string {
		return this._paymentInstructionURI;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PaymentRequestSearchCriteria', moduleContext.moduleName)
export class PaymentRequestSearchCriteria extends BasePagedSearchCriteria {
	@observable private _paymentRequestIDs: number[] = null;

	@TypeSystem.propertyArrayDecorator('number')
	public set paymentRequestIDs(val: number[]) {
		this._paymentRequestIDs = val;
	}


	public get paymentRequestIDs(): number[] {
		return this._paymentRequestIDs;
	}

	@observable private _obligationIDs: number[] = null;

	@TypeSystem.propertyArrayDecorator('number')
	public set obligationIDs(val: number[]) {
		this._obligationIDs = val;
	}


	public get obligationIDs(): number[] {
		return this._obligationIDs;
	}

	@observable private _paymentChannel: RegistrationDataTypes = null;

	@TypeSystem.propertyDecorator(RegistrationDataTypes ? RegistrationDataTypes : moduleContext.moduleName + '.RegistrationDataTypes')
	public set paymentChannel(val: RegistrationDataTypes) {
		this._paymentChannel = val;
	}


	public get paymentChannel(): RegistrationDataTypes {
		return this._paymentChannel;
	}

	@observable private _registrationDataID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set registrationDataID(val: number) {
		this._registrationDataID = val;
	}


	public get registrationDataID(): number {
		return this._registrationDataID;
	}

	@observable private _externalPortalNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set externalPortalNumber(val: string) {
		this._externalPortalNumber = val;
	}


	public get externalPortalNumber(): string {
		return this._externalPortalNumber;
	}

	@observable private _withLock: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set withLock(val: boolean) {
		this._withLock = val;
	}


	public get withLock(): boolean {
		return this._withLock;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('StartPaymentRequest', moduleContext.moduleName)
export class StartPaymentRequest extends BaseDataModel {
	@observable private _obligedPersonName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set obligedPersonName(val: string) {
		this._obligedPersonName = val;
	}


	public get obligedPersonName(): string {
		return this._obligedPersonName;
	}

	@observable private _payerIdent: string = null;

	@TypeSystem.propertyDecorator('string')
	public set payerIdent(val: string) {
		this._payerIdent = val;
	}


	public get payerIdent(): string {
		return this._payerIdent;
	}

	@observable private _payerIdentType: ObligedPersonIdentTypes = null;

	@TypeSystem.propertyDecorator(ObligedPersonIdentTypes ? ObligedPersonIdentTypes : moduleContext.moduleName + '.ObligedPersonIdentTypes')
	public set payerIdentType(val: ObligedPersonIdentTypes) {
		this._payerIdentType = val;
	}


	public get payerIdentType(): ObligedPersonIdentTypes {
		return this._payerIdentType;
	}

	@observable private _amount: number = null;

	@TypeSystem.propertyDecorator('number')
	public set amount(val: number) {
		this._amount = val;
	}


	public get amount(): number {
		return this._amount;
	}

	@observable private _registrationDataType: RegistrationDataTypes = null;

	@TypeSystem.propertyDecorator(RegistrationDataTypes ? RegistrationDataTypes : moduleContext.moduleName + '.RegistrationDataTypes')
	public set registrationDataType(val: RegistrationDataTypes) {
		this._registrationDataType = val;
	}


	public get registrationDataType(): RegistrationDataTypes {
		return this._registrationDataType;
	}

	@observable private _okCancelUrl: string = null;

	@TypeSystem.propertyDecorator('string')
	public set okCancelUrl(val: string) {
		this._okCancelUrl = val;
	}


	public get okCancelUrl(): string {
		return this._okCancelUrl;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}

import { BaseDataModel, TypeSystem } from "cnsys-core";
import { observable } from "mobx";
import { moduleContext } from '../ModuleContext';
import { EmployeeCitizenshipType, EmployeeIdentifierType } from "./ModelsAutoGenerated";

@TypeSystem.typeDecorator('EmployeeInfo', moduleContext.moduleName)
export class EmployeeInfo extends BaseDataModel {

	//#region employeeID
	@observable private _employeeID: number;

	@TypeSystem.propertyDecorator('number')
	public set employeeID(val: number) {
		this._employeeID = val;
	}

	public get employeeID(): number {
		return this._employeeID;
	}

	//#endregion employeeID


	//#region employeeName
	@observable private _employeeName: string;

	@TypeSystem.propertyDecorator('string')
	public set employeeName(val: string) {
		this._employeeName = val;
	}

	public get employeeName(): string {
		return this._employeeName;
	}
	//#endregion employeeName


	//#region identityType
	@observable private _identityType: EmployeeIdentifierType;

	@TypeSystem.propertyDecorator(EmployeeIdentifierType ? EmployeeIdentifierType : moduleContext.moduleName + '.IdentityType')
	public get identityType(): EmployeeIdentifierType {
		return this._identityType;
	}
	public set identityType(value: EmployeeIdentifierType) {
		this._identityType = value;
	}
	//#endregion identityType


	//#region identityValue
	@observable private _identityValue: string;
	
	@TypeSystem.propertyDecorator('string')
	public get identityValue(): string {
		return this._identityValue;
	}
	public set identityValue(value: string) {
		this._identityValue = value;
	}
	//#endregion identityValue

	//#region citizenship
	@observable private _citizenship: number;

	@TypeSystem.propertyDecorator(EmployeeCitizenshipType ? EmployeeCitizenshipType : moduleContext.moduleName + '.EmployeeCitizenshipType')
	public get citizenship(): EmployeeCitizenshipType {
		return this._citizenship;
	}
	public set citizenship(value: EmployeeCitizenshipType) {
		this._citizenship = value;
	}
	//#endregion citizenship

	constructor(obj?: any) {
		super(obj)

		this.copyFrom(obj);
	}
}

@TypeSystem.typeDecorator('FindObjectsContractInfo', moduleContext.moduleName)
export class FindObjectsContractInfo extends BaseDataModel {

	//#region companyID
	@observable private _companyID: number;

	@TypeSystem.propertyDecorator('number')
	public set companyID(val: number) {
		this._companyID = val;
	}

	public get companyID(): number {
		return this._companyID;
	}

	//#endregion companyID

	//#region contractorTypeID
	@observable private _contractorTypeID: number;

	@TypeSystem.propertyDecorator('number')
	public set contractorTypeID(val: number) {
		this._contractorTypeID = val;
	}

	public get contractorTypeID(): number {
		return this._contractorTypeID;
	}

	//#endregion contractorTypeID


	//#region personIdentityValue
	@observable private _personIdentityValue: string;

	@TypeSystem.propertyDecorator('string')
	public set personIdentityValue(val: string) {
		this._personIdentityValue = val;
	}

	public get personIdentityValue(): string {
		return this._personIdentityValue;
	}
	//#endregion personIdentityValue

	//#region personName
	@observable private _personName: string;

	@TypeSystem.propertyDecorator('string')
	public set personName(val: string) {
		this._personName = val;
	}

	public get personName(): string {
		return this._personName;
	}
	//#endregion personName

	//#region legalEntityIdentityValue
	@observable private _legalEntityIdentityValue: string;

	@TypeSystem.propertyDecorator('string')
	public set legalEntityIdentityValue(val: string) {
		this._legalEntityIdentityValue = val;
	}

	public get legalEntityIdentityValue(): string {
		return this._legalEntityIdentityValue;
	}
	//#endregion legalEntityIdentityValue

	//#region legalEntityName
	@observable private _legalEntityName: string;

	@TypeSystem.propertyDecorator('string')
	public set legalEntityName(val: string) {
		this._legalEntityName = val;
	}

	public get legalEntityName(): string {
		return this._legalEntityName;
	}
	//#endregion legalEntityName

	//#region securityObjects
	@observable private _securityObjects: FindObjectInfo[] = null;

	@TypeSystem.propertyArrayDecorator(FindObjectInfo ? FindObjectInfo : moduleContext.moduleName + '.FindObjectInfo')
	public set securityObjects(val: FindObjectInfo[]) {
		this._securityObjects = val;
	}


	public get securityObjects(): FindObjectInfo[] {
		return this._securityObjects;
	}
	//#endregion securityObjects


	constructor(obj?: any) {
		super(obj)

		this.copyFrom(obj);
	}
}

@TypeSystem.typeDecorator('FindObjectInfo', moduleContext.moduleName)
export class FindObjectInfo extends BaseDataModel {

	//#region securityObjectID
	@observable private _securityObjectID: number;

	@TypeSystem.propertyDecorator('number')
	public set securityObjectID(val: number) {
		this._securityObjectID = val;
	}

	public get securityObjectID(): number {
		return this._securityObjectID;
	}
	//#endregion securityObjectID

	//#region securityActivityTypeID
	@observable private _securityActivityTypeID: number;

	@TypeSystem.propertyDecorator('number')
	public set securityActivityTypeID(val: number) {
		this._securityActivityTypeID = val;
	}

	public get securityActivityTypeID(): number {
		return this._securityActivityTypeID;
	}
	//#endregion securityActivityTypeID


	//#region securityObjectName
	@observable private _securityObjectName: string;

	@TypeSystem.propertyDecorator('string')
	public set securityObjectName(val: string) {
		this._securityObjectName = val;
	}

	public get securityObjectName(): string {
		return this._securityObjectName;
	}
	//#endregion securityObjectName

	//#region relationshipID
	@observable private _relationshipID: number;

	@TypeSystem.propertyDecorator('number')
	public set relationshipID(val: number) {
		this._relationshipID = val;
	}

	public get relationshipID(): number {
		return this._relationshipID;
	}
	//#endregion relationshipID

	//#region genderID
	@observable private _genderID: number;

	@TypeSystem.propertyDecorator('number')
	public set genderID(val: number) {
		this._genderID = val;
	}

	public get genderID(): number {
		return this._genderID;
	}
	//#endregion genderID

	//#region identityValue
	@observable private _identityValue: string;

	@TypeSystem.propertyDecorator('string')
	public set identityValue(val: string) {
		this._identityValue = val;
	}

	public get identityValue(): string {
		return this._identityValue;
	}
	//#endregion identityValue

	//#region regionID
	@observable private _regionID: number;

	@TypeSystem.propertyDecorator('number')
	public set regionID(val: number) {
		this._regionID = val;
	}

	public get regionID(): number {
		return this._regionID;
	}
	//#endregion regionID

	//#region securityObjectAddress
	@observable private _securityObjectAddress: string;

	@TypeSystem.propertyDecorator('string')
	public set securityObjectAddress(val: string) {
		this._securityObjectAddress = val;
	}

	public get securityObjectAddress(): string {
		return this._securityObjectAddress;
	}
	//#endregion securityObjectAddress

	//#region transportEndpointsObjectKinds
	@observable private _transportEndpointsObjectKinds: string;

	@TypeSystem.propertyDecorator('string')
	public set transportEndpointsObjectKinds(val: string) {
		this._transportEndpointsObjectKinds = val;
	}

	public get transportEndpointsObjectKinds(): string {
		return this._transportEndpointsObjectKinds;
	}
	//#endregion transportEndpointsObjectKinds

	//#region transportFrom
	@observable private _transportFrom: string;

	@TypeSystem.propertyDecorator('string')
	public set transportFrom(val: string) {
		this._transportFrom = val;
	}

	public get transportFrom(): string {
		return this._transportFrom;
	}
	//#endregion transportFrom

	//#region transportTo
	@observable private _transportTo: string;

	@TypeSystem.propertyDecorator('string')
	public set transportTo(val: string) {
		this._transportTo = val;
	}

	public get transportTo(): string {
		return this._transportTo;
	}
	//#endregion transportTo

	//#region regionToID
	@observable private _regionToID: number;

	@TypeSystem.propertyDecorator('number')
	public set regionToID(val: number) {
		this._regionToID = val;
	}

	public get regionToID(): number {
		return this._regionToID;
	}
	//#endregion regionToID

	//#region addressTo
	@observable private _addressTo: string;

	@TypeSystem.propertyDecorator('string')
	public set addressTo(val: string) {
		this._addressTo = val;
	}

	public get addressTo(): string {
		return this._addressTo;
	}
	//#endregion addressTo

	//#region takeoverDate
	@observable private _takeoverDate: string;

	@TypeSystem.propertyDecorator('string')
	public set takeoverDate(val: string) {
		this._takeoverDate = val;
	}

	public get takeoverDate(): string {
		return this._takeoverDate;
	}
	//#endregion takeoverDate

	//#region terminationDate
	@observable private _terminationDate: string;

	@TypeSystem.propertyDecorator('string')
	public set terminationDate(val: string) {
		this._terminationDate = val;
	}

	public get terminationDate(): string {
		return this._terminationDate;
	}
	//#endregion terminationDate

	//#region active
	@observable private _active: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set active(val: boolean) {
		this._active = val;
	}


	public get active(): boolean {
		return this._active;
	}
	//#endregion active

	//#region note
	@observable private _note: string;

	@TypeSystem.propertyDecorator('string')
	public set note(val: string) {
		this._note = val;
	}

	public get note(): string {
		return this._note;
	}
	//#endregion note

	constructor(obj?: any) {
		super(obj)

		this.copyFrom(obj);
	}
}

@TypeSystem.typeDecorator('Region', moduleContext.moduleName)
export class Region extends BaseDataModel {

	//#region itemID
	@observable private _itemID: number;

	@TypeSystem.propertyDecorator('number')
	public set itemID(val: number) {
		this._itemID = val;
	}

	public get itemID(): number {
		return this._itemID;
	}
	//#endregion itemID

	//#region title
	@observable private _title: string;

	@TypeSystem.propertyDecorator('string')
	public set title(val: string) {
		this._title = val;
	}

	public get title(): string {
		return this._title;
	}
	//#endregion title

	//#region code
	@observable private _code: string;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}

	public get code(): string {
		return this._code;
	}
	//#endregion code

	//#region description
	@observable private _description: string;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}

	public get description(): string {
		return this._description;
	}
	//#endregion description

	//#region active
	@observable private _active: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set active(val: boolean) {
		this._active = val;
	}

	public get active(): boolean {
		return this._active;
	}
	//#endregion active

	//#region order
	@observable private _order: number;

	@TypeSystem.propertyDecorator('number')
	public set order(val: number) {
		this._order = val;
	}

	public get order(): number {
		return this._order;
	}
	//#endregion order

	constructor(obj?: any) {
		super(obj)

		this.copyFrom(obj);
	}
}

@TypeSystem.typeDecorator('SecurityTypesMode', moduleContext.moduleName)
export class SecurityTypesMode extends BaseDataModel {

	//#region itemID
	@observable private _itemID: number;

	@TypeSystem.propertyDecorator('number')
	public set itemID(val: number) {
		this._itemID = val;
	}

	public get itemID(): number {
		return this._itemID;
	}
	//#endregion itemID

	//#region title
	@observable private _title: string;

	@TypeSystem.propertyDecorator('string')
	public set title(val: string) {
		this._title = val;
	}

	public get title(): string {
		return this._title;
	}
	//#endregion title

	//#region code
	@observable private _code: string;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}

	public get code(): string {
		return this._code;
	}
	//#endregion code

	//#region description
	@observable private _description: string;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}

	public get description(): string {
		return this._description;
	}
	//#endregion description

	//#region active
	@observable private _active: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set active(val: boolean) {
		this._active = val;
	}

	public get active(): boolean {
		return this._active;
	}
	//#endregion active

	//#region order
	@observable private _order: number;

	@TypeSystem.propertyDecorator('number')
	public set order(val: number) {
		this._order = val;
	}

	public get order(): number {
		return this._order;
	}
	//#endregion order

	constructor(obj?: any) {
		super(obj)

		this.copyFrom(obj);
	}
}

@TypeSystem.typeDecorator('SecurityTypesControl', moduleContext.moduleName)
export class SecurityTypesControl extends BaseDataModel {

	//#region itemID
	@observable private _itemID: number;

	@TypeSystem.propertyDecorator('number')
	public set itemID(val: number) {
		this._itemID = val;
	}

	public get itemID(): number {
		return this._itemID;
	}
	//#endregion itemID

	//#region title
	@observable private _title: string;

	@TypeSystem.propertyDecorator('string')
	public set title(val: string) {
		this._title = val;
	}

	public get title(): string {
		return this._title;
	}
	//#endregion title

	//#region code
	@observable private _code: string;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}

	public get code(): string {
		return this._code;
	}
	//#endregion code

	//#region description
	@observable private _description: string;

	@TypeSystem.propertyDecorator('string')
	public set description(val: string) {
		this._description = val;
	}

	public get description(): string {
		return this._description;
	}
	//#endregion description

	//#region active
	@observable private _active: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set active(val: boolean) {
		this._active = val;
	}

	public get active(): boolean {
		return this._active;
	}
	//#endregion active

	//#region order
	@observable private _order: number;

	@TypeSystem.propertyDecorator('number')
	public set order(val: number) {
		this._order = val;
	}

	public get order(): number {
		return this._order;
	}
	//#endregion order

	constructor(obj?: any) {
		super(obj)

		this.copyFrom(obj);
	}
}
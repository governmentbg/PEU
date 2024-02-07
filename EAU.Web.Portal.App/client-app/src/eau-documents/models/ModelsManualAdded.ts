import { BaseDataModel, ObjectHelper, TypeSystem } from 'cnsys-core';
import { observable } from 'mobx';
import moment from 'moment';
import { moduleContext } from '../ModuleContext';

export enum RegiXEntityDataIdentifierTypes {

    UIC = 1,

    Bulstat = 2,

}
TypeSystem.registerEnumInfo(RegiXEntityDataIdentifierTypes, 'RegiXEntityDataIdentifierTypes', moduleContext.moduleName)

export enum NomenclatureType {
    Irregularity = 1
}
TypeSystem.registerEnumInfo(NomenclatureType, 'NomenclatureType', moduleContext.moduleName)

export enum DocumentModes {

    NewApplication = 1,

    RemovingIrregularitiesApplication = 2,

    EditDocument = 3,

    SignDocument = 4,

    EditAndSignDocument = 5,

    ViewDocument = 6,

    AdditionalApplication = 7,

    WithdrawService = 8
}
TypeSystem.registerEnumInfo(DocumentModes, 'DocumentModes', moduleContext.moduleName)

@TypeSystem.typeDecorator('RegisterObjectURI', moduleContext.moduleName)
export class RegisterObjectURI extends BaseDataModel {

    //#region registerIndex

    @observable private _registerIndex: number = null;

    @TypeSystem.propertyDecorator('number')
    public set registerIndex(val: number) {
        this._registerIndex = val;
    }

    public get registerIndex(): number {
        return this._registerIndex;
    }

    //#endregion registerIndex

    //#region batchNumber

    @observable private _batchNumber: number = null;

    @TypeSystem.propertyDecorator('number')
    public set batchNumber(val: number) {
        this._batchNumber = val;
    }

    public get batchNumber(): number {
        return this._batchNumber;
    }

    //#endregion batchNumber

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('DocumentTypeURI', moduleContext.moduleName)
export class DocumentTypeURI extends RegisterObjectURI {

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('DocumentURI', moduleContext.moduleName)
export class DocumentURI extends BaseDataModel {

    //#region registerIndex

    @observable private _registerIndex: number = null;

    @TypeSystem.propertyDecorator('number')
    public set registerIndex(val: number) {
        this._registerIndex = val;
    }

    public get registerIndex(): number {
        return this._registerIndex;
    }

    //#endregion registerIndex

    //#region sequenceNumber

    @observable private _sequenceNumber: number = null;

    @TypeSystem.propertyDecorator('number')
    public set sequenceNumber(val: number) {
        this._sequenceNumber = val;
    }

    public get sequenceNumber(): number {
        return this._sequenceNumber;
    }

    //#endregion sequenceNumber

    //#region receiptOrSigningDate

    @observable private _receiptOrSigningDate: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set receiptOrSigningDate(val: moment.Moment) {
        this._receiptOrSigningDate = val;
    }

    public get receiptOrSigningDate(): moment.Moment {
        return this._receiptOrSigningDate;
    }

    //#endregion receiptOrSigningDate

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('DocumentFormVMBase', moduleContext.moduleName)
export class DocumentFormVMBase extends BaseDataModel {

    //#region editStrategySelectedList

    @observable private _editStrategySelectedList: string[] = null;

    @TypeSystem.propertyArrayDecorator('string')
    public set editStrategySelectedList(val: string[]) {
        this._editStrategySelectedList = val;
    }

    public get editStrategySelectedList(): string[] {
        return this._editStrategySelectedList;
    }

    //#endregion editStrategySelectedList

    //#region documentTypeURI

    @observable private _documentTypeURI: DocumentTypeURI = null;

    @TypeSystem.propertyDecorator(moduleContext.moduleName + '.' + 'DocumentTypeURI')
    public set documentTypeURI(val: DocumentTypeURI) {
        this._documentTypeURI = val;
    }

    public get documentTypeURI(): DocumentTypeURI {
        return this._documentTypeURI;
    }

    //#endregion documentTypeURI

    //#region documentTypeName

    @observable private _documentTypeName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set documentTypeName(val: string) {
        this._documentTypeName = val;
    }

    public get documentTypeName(): string {
        return this._documentTypeName;
    }

    //#endregion documentTypeName

    //#region documentURI

    @observable private _documentURI: DocumentURI = null;

    @TypeSystem.propertyDecorator(moduleContext.moduleName + '.' + 'DocumentURI')
    public set documentURI(val: DocumentURI) {
        this._documentURI = val;
    }

    public get documentURI(): DocumentURI {
        return this._documentURI;
    }

    //#endregion documentURI

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('SigningDocumentFormVMBase<T>', moduleContext.moduleName)
export class SigningDocumentFormVMBase<T> extends DocumentFormVMBase {

    //#region digitalSignatures

    @observable private _digitalSignatures: T[] = null;

    @TypeSystem.propertyArrayDecorator('any')
    public set digitalSignatures(val: T[]) {
        this._digitalSignatures = val;
    }

    public get digitalSignatures(): T[] {
        return this._digitalSignatures;
    }

    //#endregion digitalSignatures

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('XMLDigitalSignature', moduleContext.moduleName)
export class XMLDigitalSignature extends BaseDataModel {

    //#region signature

    @observable private _signature: any = null;

    @TypeSystem.propertyDecorator('any')
    public set signature(val: any) {
        this._signature = val;
    }

    public get signature(): any {
        return this._signature;
    }

    //#endregion signature

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('AISKATNomenclatureItem', moduleContext.moduleName)
export class AISKATNomenclatureItem extends BaseDataModel {

    //#region code

    @observable private _code: number = null;

    @TypeSystem.propertyDecorator('number')
    public set code(val: number) {
        this._code = val;
    }

    public get code(): number {
        return this._code;
    }

    //#endregion code

    //#region nam

    @observable private _name: string = null;

    @TypeSystem.propertyDecorator('string')
    public set name(val: string) {
        this._name = val;
    }

    public get name(): string {
        return this._name;
    }

    //#endregion nam

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}


@TypeSystem.typeDecorator('AISKATHieraNomenclature', moduleContext.moduleName)
export class AISKATHieraNomenclature extends AISKATNomenclatureItem {
    @observable private _inner: AISKATNomenclatureItem[] = null;

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }

    @TypeSystem.propertyArrayDecorator(moduleContext.moduleName + '.' + 'AISKATNomenclatureItem')
    public set inner(val: AISKATNomenclatureItem[]) {
        this._inner = val;
    }

    public get inner(): AISKATNomenclatureItem[] {
        return this._inner;
    }
}

@TypeSystem.typeDecorator('AttachedDocument', moduleContext.moduleName)
export class AttachedDocument extends BaseDataModel {

    //#region attachedDocumentID

    @observable private _attachedDocumentID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set attachedDocumentID(val: number) {
        this._attachedDocumentID = val;
    }

    public get attachedDocumentID(): number {
        return this._attachedDocumentID;
    }

    //#endregion attachedDocumentID 

    //#region attachedDocumentGuid

    @observable private _attachedDocumentGuid: string = null;

    @TypeSystem.propertyDecorator('string')
    public set attachedDocumentGuid(val: string) {
        this._attachedDocumentGuid = val;
    }

    public get attachedDocumentGuid(): string {
        return this._attachedDocumentGuid;
    }

    //#endregion attachedDocumentGuid 

    //#region documentProcessID

    @observable private _documentProcessID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set documentProcessID(val: number) {
        this._documentProcessID = val;
    }

    public get documentProcessID(): number {
        return this._documentProcessID;
    }

    //#endregion documentProcessID 

    //#region documentProcessContentID

    @observable private _documentProcessContentID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set documentProcessContentID(val: number) {
        this._documentProcessContentID = val;
    }

    public get documentProcessContentID(): number {
        return this._documentProcessContentID;
    }

    //#endregion documentProcessContentID

    //#region documentTypeID

    @observable private _documentTypeID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set documentTypeID(val: number) {
        this._documentTypeID = val;
    }

    public get documentTypeID(): number {
        return this._documentTypeID;
    }

    //#endregion documentTypeID 

    //#region documentTypeName

    @observable private _documentTypeName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set documentTypeName(val: string) {
        this._documentTypeName = val;
    }

    public get documentTypeName(): string {
        return this._documentTypeName;
    }

    //#endregion documentTypeName

    //#region description

    @observable private _description: string = null;

    @TypeSystem.propertyDecorator('string')
    public set description(val: string) {
        this._description = val;
    }

    public get description(): string {
        return this._description;
    }

    //#endregion description 

    //#region mimeType

    @observable private _mimeType: string = null;

    @TypeSystem.propertyDecorator('string')
    public set mimeType(val: string) {
        this._mimeType = val;
    }

    public get mimeType(): string {
        return this._mimeType;
    }

    //#endregion mimeType 

    //#region fileName

    @observable private _fileName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set fileName(val: string) {
        this._fileName = val;
    }

    public get fileName(): string {
        return this._fileName;
    }

    //#endregion fileName 

    //#region htmlContent

    @observable private _htmlContent: string = null;

    @TypeSystem.propertyDecorator('string')
    public set htmlContent(val: string) {
        this._htmlContent = val;
    }

    public get htmlContent(): string {
        return this._htmlContent;
    }

    //#endregion htmlContent

    //#region signingGuid

    @observable private _signingGuid: string = null;

    @TypeSystem.propertyDecorator('string')
    public set signingGuid(val: string) {
        this._signingGuid = val;
    }

    public get signingGuid(): string {
        return this._signingGuid;
    }

    //#endregion signingGuid

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('NomenclatureItem', moduleContext.moduleName)
export class NomenclatureItem extends BaseDataModel {

    @observable private _itemID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set itemID(val: number) {
        this._itemID = val;
    }

    public get itemID(): number {
        return this._itemID;
    }

    @observable private _type: NomenclatureType = null;

    @TypeSystem.propertyDecorator(NomenclatureType)
    public set type(val: NomenclatureType) {
        this._type = val;
    }

    public get type(): NomenclatureType {
        return this._type;
    }

    @observable private _value: string = null;

    @TypeSystem.propertyDecorator('string')
    public set value(val: string) {
        this._value = val;
    }

    public get value(): string {
        return this._value;
    }

    @observable private _description: string = null;

    @TypeSystem.propertyDecorator('string')
    public set description(val: string) {
        this._description = val;
    }

    public get description(): string {
        return this._description;
    }

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('CyrilicLatinName', moduleContext.moduleName)
export class CyrilicLatinName extends BaseDataModel {

    //#region cyrillic

    @observable private _cyrillic: string = null;

    @TypeSystem.propertyDecorator('string')
    public set cyrillic(val: string) {
        this._cyrillic = val;
    }

    public get cyrillic(): string {
        return this._cyrillic;
    }

    //#endregion cyrillic

    //#region latin

    @observable private _latin: string = null;

    @TypeSystem.propertyDecorator('string')
    public set latin(val: string) {
        this._latin = val;
    }

    public get latin(): string {
        return this._latin;
    }

    //#endregion latin

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('PersonInfo', moduleContext.moduleName)
export class PersonInfo extends BaseDataModel {

    //#region pin

    @observable private _pin: string = null;

    @TypeSystem.propertyDecorator('string')
    public set pin(val: string) {
        this._pin = val;
    }

    public get pin(): string {
        return this._pin;
    }

    //#endregion pin

    //#region firstName

    @observable private _firstName: CyrilicLatinName = null;

    @TypeSystem.propertyDecorator(CyrilicLatinName)
    public set firstName(val: CyrilicLatinName) {
        this._firstName = val;
    }

    public get firstName(): CyrilicLatinName {
        return this._firstName;
    }

    //#endregion firstName

    //#region surname

    @observable private _surname: CyrilicLatinName = null;

    @TypeSystem.propertyDecorator(CyrilicLatinName)
    public set surname(val: CyrilicLatinName) {
        this._surname = val;
    }

    public get surname(): CyrilicLatinName {
        return this._surname;
    }

    //#endregion surname

    //#region family

    @observable private _family: CyrilicLatinName = null;

    @TypeSystem.propertyDecorator(CyrilicLatinName)
    public set family(val: CyrilicLatinName) {
        this._family = val;
    }

    public get family(): CyrilicLatinName {
        return this._family;
    }

    //#endregion family

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('RegiXEntityData', moduleContext.moduleName)
export class RegiXEntityData extends BaseDataModel {

    //#region identifier

    @observable private _identifier: string = null;

    @TypeSystem.propertyDecorator('string')
    public set identifier(val: string) {
        this._identifier = val;
    }

    public get identifier(): string {
        return this._identifier;
    }

    //#endregion identifier 

    //#region identifierType

    @observable private _identifierType: RegiXEntityDataIdentifierTypes = null;

    @TypeSystem.propertyDecorator(RegiXEntityDataIdentifierTypes)
    public set identifierType(val: RegiXEntityDataIdentifierTypes) {
        this._identifierType = val;
    }

    public get identifierType(): RegiXEntityDataIdentifierTypes {
        return this._identifierType;
    }

    //#endregion identifierType 

    //#region name

    @observable private _name: string = null;

    @TypeSystem.propertyDecorator('string')
    public set name(val: string) {
        this._name = val;
    }

    public get name(): string {
        return this._name;
    }

    //#endregion name 

    //#region shortName

    @observable private _shortName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set shortName(val: string) {
        this._shortName = val;
    }

    public get shortName(): string {
        return this._shortName;
    }

    //#endregion shortName 

    //#region latinName

    @observable private _latinName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set latinName(val: string) {
        this._latinName = val;
    }

    public get latinName(): string {
        return this._latinName;
    }

    //#endregion latinName 

    //#region type

    @observable private _type: string = null;

    @TypeSystem.propertyDecorator('string')
    public set type(val: string) {
        this._type = val;
    }

    public get type(): string {
        return this._type;
    }

    //#endregion type 

    //#region typeCode

    @observable private _typeCode: string = null;

    @TypeSystem.propertyDecorator('string')
    public set typeCode(val: string) {
        this._typeCode = val;
    }

    public get typeCode(): string {
        return this._typeCode;
    }

    //#endregion typeCode 

    //#region status

    @observable private _status: string = null;

    @TypeSystem.propertyDecorator('string')
    public set status(val: string) {
        this._status = val;
    }

    public get status(): string {
        return this._status;
    }

    //#endregion status 

    //#region address

    @observable private _address: AddressType = null;

    @TypeSystem.propertyDecorator(moduleContext.moduleName + '.' + 'AddressType')
    public set address(val: AddressType) {
        this._address = val;
    }

    public get address(): AddressType {
        return this._address;
    }

    //#endregion address 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('AddressType', moduleContext.moduleName)
export class AddressType extends BaseDataModel {

    //#region countryCode

    @observable private _countryCode: string = null;

    @TypeSystem.propertyDecorator('string')
    public set countryCode(val: string) {
        this._countryCode = val;
    }

    public get countryCode(): string {

        if (ObjectHelper.isStringNullOrEmpty(this._countryCode))
            return null

        return this._countryCode;
    }

    //#endregion countryCode 

    //#region country

    @observable private _country: string = null;

    @TypeSystem.propertyDecorator('string')
    public set country(val: string) {
        this._country = val;
    }

    public get country(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._country))
            return null

        return this._country;
    }

    //#endregion country 

    //#region isForeign

    @observable private _isForeign: string = null;

    @TypeSystem.propertyDecorator('string')
    public set isForeign(val: string) {
        this._isForeign = val;
    }

    public get isForeign(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._isForeign))
            return null

        return this._isForeign;
    }

    //#endregion isForeign 

    //#region districtEkatte

    @observable private _districtEkatte: string = null;

    @TypeSystem.propertyDecorator('string')
    public set districtEkatte(val: string) {
        this._districtEkatte = val;
    }

    public get districtEkatte(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._districtEkatte))
            return null

        return this._districtEkatte;
    }

    //#endregion districtEkatte 

    //#region district

    @observable private _district: string = null;

    @TypeSystem.propertyDecorator('string')
    public set district(val: string) {
        this._district = val;
    }

    public get district(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._district))
            return null

        return this._district;
    }

    //#endregion district 

    //#region municipalityEkatte

    @observable private _municipalityEkatte: string = null;

    @TypeSystem.propertyDecorator('string')
    public set municipalityEkatte(val: string) {
        this._municipalityEkatte = val;
    }

    public get municipalityEkatte(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._municipalityEkatte))
            return null

        return this._municipalityEkatte;
    }

    //#endregion municipalityEkatte 

    //#region municipality

    @observable private _municipality: string = null;

    @TypeSystem.propertyDecorator('string')
    public set municipality(val: string) {
        this._municipality = val;
    }

    public get municipality(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._municipality))
            return null

        return this._municipality;
    }

    //#endregion municipality 

    //#region settlementEKATTE

    @observable private _settlementEKATTE: string = null;

    @TypeSystem.propertyDecorator('string')
    public set settlementEKATTE(val: string) {
        this._settlementEKATTE = val;
    }

    public get settlementEKATTE(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._settlementEKATTE))
            return null

        return this._settlementEKATTE;
    }

    //#endregion settlementEKATTE 

    //#region settlement

    @observable private _settlement: string = null;

    @TypeSystem.propertyDecorator('string')
    public set settlement(val: string) {
        this._settlement = val;
    }

    public get settlement(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._settlement))
            return null

        return this._settlement;
    }

    //#endregion settlement 

    //#region area

    @observable private _area: string = null;

    @TypeSystem.propertyDecorator('string')
    public set area(val: string) {
        this._area = val;
    }

    public get area(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._area))
            return null

        return this._area;
    }

    //#endregion area 

    //#region areaEkatte

    @observable private _areaEkatte: string = null;

    @TypeSystem.propertyDecorator('string')
    public set areaEkatte(val: string) {
        this._areaEkatte = val;
    }

    public get areaEkatte(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._areaEkatte))
            return null

        return this._areaEkatte;
    }

    //#endregion areaEkatte 

    //#region postCode

    @observable private _postCode: string = null;

    @TypeSystem.propertyDecorator('string')
    public set postCode(val: string) {
        this._postCode = val;
    }

    public get postCode(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._postCode))
            return null

        return this._postCode;
    }

    //#endregion postCode 

    //#region foreignPlace

    @observable private _foreignPlace: string = null;

    @TypeSystem.propertyDecorator('string')
    public set foreignPlace(val: string) {
        this._foreignPlace = val;
    }

    public get foreignPlace(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._foreignPlace))
            return null

        return this._foreignPlace;
    }

    //#endregion foreignPlace 

    //#region housingEstate

    @observable private _housingEstate: string = null;

    @TypeSystem.propertyDecorator('string')
    public set housingEstate(val: string) {
        this._housingEstate = val;
    }

    public get housingEstate(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._housingEstate))
            return null

        return this._housingEstate;
    }

    //#endregion housingEstate 

    //#region street

    @observable private _street: string = null;

    @TypeSystem.propertyDecorator('string')
    public set street(val: string) {
        this._street = val;
    }

    public get street(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._street))
            return null

        return this._street;
    }

    //#endregion street 

    //#region streetNumber

    @observable private _streetNumber: string = null;

    @TypeSystem.propertyDecorator('string')
    public set streetNumber(val: string) {
        this._streetNumber = val;
    }

    public get streetNumber(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._streetNumber))
            return null

        return this._streetNumber;
    }

    //#endregion streetNumber 

    //#region block

    @observable private _block: string = null;

    @TypeSystem.propertyDecorator('string')
    public set block(val: string) {
        this._block = val;
    }

    public get block(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._block))
            return null

        return this._block;
    }

    //#endregion block 

    //#region entrance

    @observable private _entrance: string = null;

    @TypeSystem.propertyDecorator('string')
    public set entrance(val: string) {
        this._entrance = val;
    }

    public get entrance(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._entrance))
            return null

        return this._entrance;
    }

    //#endregion entrance 

    //#region floor

    @observable private _floor: string = null;

    @TypeSystem.propertyDecorator('string')
    public set floor(val: string) {
        this._floor = val;
    }

    public get floor(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._floor))
            return null

        return this._floor;
    }

    //#endregion floor 

    //#region apartment

    @observable private _apartment: string = null;

    @TypeSystem.propertyDecorator('string')
    public set apartment(val: string) {
        this._apartment = val;
    }

    public get apartment(): string {
        if (ObjectHelper.isStringNullOrEmpty(this._apartment))
            return null

        return this._apartment;
    }

    //#endregion apartment 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}
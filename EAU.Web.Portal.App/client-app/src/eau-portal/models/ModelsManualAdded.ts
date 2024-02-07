import { BaseDataModel, TypeSystem } from 'cnsys-core';
import { ANDObligationSearchMode, KATDocumentTypes, ObligationSearchCriteria, ObligationStatuses, ObligationTypes, ObligedPersonIdentTypes, PaymentRequestStatuses, RegistrationDataTypes, ServiceInstanceStatuses } from 'eau-core';
import { observable } from 'mobx';
import * as moment from 'moment';
import { moduleContext } from '../ModuleContext';

export enum ServiceInstanceStatus {

    NotCompleted = 1,

    WaitingResponse = 2,

    Completed = 3,

    Cancelled = 4,

    CancelIssuingAdministrativeAct = 5,

    WaitCorrectionsApplication = 6,

    Termination = 7,

    OutstandingConditions = 8,

    WaitPayment = 9,

}
TypeSystem.registerEnumInfo(ServiceInstanceStatus, 'ServiceInstanceStatus', moduleContext.moduleName)

export enum ObligatedPersonType {

    Personal = 1,

    Entity = 2,
}
TypeSystem.registerEnumInfo(ObligatedPersonType, 'ObligatedPersonType', moduleContext.moduleName)


export enum AdditinalDataForObligatedPersonType {

    DrivingLicenceNumber = 1,

    PersonalDocumentNumber = 2,

    ForeignVehicleNumber = 3,
}
TypeSystem.registerEnumInfo(AdditinalDataForObligatedPersonType, 'AdditinalDataForObligatedPersonType', moduleContext.moduleName)

export enum ObligationSearchResultUnitGroups {
    AIS_AND = 1,
    NAIF_NRBLD = 2,
}
TypeSystem.registerEnumInfo(ObligationSearchResultUnitGroups, 'ObligationSearchResultUnitGroups', moduleContext.moduleName);

@TypeSystem.typeDecorator('Obligation', moduleContext.moduleName)
export class Obligation extends BaseDataModel {
    @observable private _obligationID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set obligationID(val: number) {
        this._obligationID = val;
    }

    public get obligationID(): number {
        return this._obligationID;
    }

    @observable private _status: ObligationStatuses = null;

    @TypeSystem.propertyDecorator(ObligationStatuses ? ObligationStatuses : moduleContext.moduleName + '.ObligationStatuses')
    public set status(val: ObligationStatuses) {
        this._status = val;
    }

    public get status(): ObligationStatuses {
        return this._status;
    }

    @observable private _amount: number = null;

    @TypeSystem.propertyDecorator('number')
    public set amount(val: number) {
        this._amount = val;
    }

    public get amount(): number {
        return this._amount;
    }

    @observable private _discountAmount: number = null;

    @TypeSystem.propertyDecorator('number')
    public set discountAmount(val: number) {
        this._discountAmount = val;
    }

    public get discountAmount(): number {
        return this._discountAmount;
    }

    @observable private _bankName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set bankName(val: string) {
        this._bankName = val;
    }

    public get bankName(): string {
        return this._bankName;
    }

    @observable private _bic: string = null;

    @TypeSystem.propertyDecorator('string')
    public set bic(val: string) {
        this._bic = val;
    }

    public get bic(): string {
        return this._bic;
    }

    @observable private _iban: string = null;

    @TypeSystem.propertyDecorator('string')
    public set iban(val: string) {
        this._iban = val;
    }

    public get iban(): string {
        return this._iban;
    }

    @observable private _paymentReason: string = null;

    @TypeSystem.propertyDecorator('string')
    public set paymentReason(val: string) {
        this._paymentReason = val;
    }

    public get paymentReason(): string {
        return this._paymentReason;
    }

    @observable private _pepCin: string = null;

    @TypeSystem.propertyDecorator('string')
    public set pepCin(val: string) {
        this._pepCin = val;
    }

    public get pepCin(): string {
        return this._pepCin;
    }

    @observable private _expirationDate: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set expirationDate(val: moment.Moment) {
        this._expirationDate = val;
    }

    public get expirationDate(): moment.Moment {
        return this._expirationDate;
    }

    @observable private _applicantID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set applicantID(val: number) {
        this._applicantID = val;
    }

    public get applicantID(): number {
        return this._applicantID;
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

    @observable private _obligationDate: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set obligationDate(val: moment.Moment) {
        this._obligationDate = val;
    }

    public get obligationDate(): moment.Moment {
        return this._obligationDate;
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

    @observable private _serviceInstanceID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set serviceInstanceID(val: number) {
        this._serviceInstanceID = val;
    }

    public get serviceInstanceID(): number {
        return this._serviceInstanceID;
    }

    @observable private _serviceID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set serviceID(val: number) {
        this._serviceID = val;
    }

    public get serviceID(): number {
        return this._serviceID;
    }

    //#region additionalData

    @observable private _additionalData: any = null;

    @TypeSystem.propertyDecorator('any')
    public set additionalData(val: any) {
        this._additionalData = val;
    }

    public get additionalData(): any {
        return this._additionalData;
    }

    //#endregion additionalData 

    @observable private _paymentRequests: PaymentRequest[] = null;

    @TypeSystem.propertyArrayDecorator(PaymentRequest ? PaymentRequest : moduleContext.moduleName + '.PaymentRequest')
    public set paymentRequests(val: PaymentRequest[]) {
        this._paymentRequests = val;
    }

    public get paymentRequests(): PaymentRequest[] {
        return this._paymentRequests;
    }

    constructor(data?: any) {
        super(data);

        this.copyFrom(data);
    }
}

@TypeSystem.typeDecorator('ServiceInstanceVM', moduleContext.moduleName)
export class ServiceInstance extends BaseDataModel {

    //#region serviceInstanceID

    @observable private _serviceInstanceID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set serviceInstanceID(val: number) {
        this._serviceInstanceID = val;
    }

    public get serviceInstanceID(): number {
        return this._serviceInstanceID;
    }

    //#endregion serviceInstanceID 

    //#region status

    @observable private _status: ServiceInstanceStatuses = null;

    @TypeSystem.propertyDecorator(ServiceInstanceStatuses)
    public set status(val: ServiceInstanceStatuses) {
        this._status = val;
    }

    public get status(): ServiceInstanceStatuses {
        return this._status;
    }

    //#endregion status 

    //#region serviceInstanceDate

    @observable private _serviceInstanceDate: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set serviceInstanceDate(val: moment.Moment) {
        this._serviceInstanceDate = val;
    }

    public get serviceInstanceDate(): moment.Moment {
        return this._serviceInstanceDate;
    }

    //#endregion serviceInstanceDate 

    //#region serviceID

    @observable private _serviceID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set serviceID(val: number) {
        this._serviceID = val;
    }

    public get serviceID(): number {
        return this._serviceID;
    }

    //#endregion serviceID 

    //#region service

    @observable private _service: string = null;

    @TypeSystem.propertyDecorator('string')
    public set service(val: string) {
        this._service = val;
    }

    public get service(): string {
        return this._service;
    }

    //#endregion service 

    //#region caseFileURI

    @observable private _caseFileURI: string = null;

    @TypeSystem.propertyDecorator('string')
    public set caseFileURI(val: string) {
        this._caseFileURI = val;
    }

    public get caseFileURI(): string {
        return this._caseFileURI;
    }

    //#endregion caseFileUri 

    //#region additionalData

    @observable private _additionalData: any = null;

    @TypeSystem.propertyDecorator('any')
    public set additionalData(val: any) {
        this._additionalData = val;
    }

    public get additionalData(): any {
        return this._additionalData;
    }

    //#endregion additionalData

    //#region updatedOn

    @observable private _updatedOn: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set updatedOn(val: moment.Moment) {
        this._updatedOn = val;
    }

    public get updatedOn(): moment.Moment {
        return this._updatedOn;
    }

    //#endregion 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('SitemapSearchVM', moduleContext.moduleName)
export class SitemapSearchVM extends BaseDataModel {

    //#region searchFor

    @observable private _searchFor: string = null;

    @TypeSystem.propertyDecorator('string')
    public set searchFor(val: string) {
        this._searchFor = val;
    }

    public get searchFor(): string {
        return this._searchFor;
    }

    //#endregion searchFor

    //#region currentPage

    @observable private _currentPage: number = null;

    @TypeSystem.propertyDecorator('number')
    public set currentPage(val: number) {
        this._currentPage = val;
    }

    public get currentPage(): number {
        return this._currentPage;
    }

    //#endregion currentPage

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('URI', moduleContext.moduleName)
export class URI extends BaseDataModel {

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

    public toString() {
        return `${this.registerIndex}-${this.sequenceNumber}-${this.receiptOrSigningDate.format('dd.MM.yyyy')}`
    }

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('UserInputModel', moduleContext.moduleName)
export class UserInputModel extends BaseDataModel {

    //#region email

    @observable private _email: string = null;

    @TypeSystem.propertyDecorator('string')
    public set email(val: string) {
        this._email = val;
    }

    public get email(): string {
        return this._email;
    }

    //#endregion email

    //#region currentPassword

    @observable private _currentPassword: string = null;

    @TypeSystem.propertyDecorator('string')
    public set currentPassword(val: string) {
        this._currentPassword = val;
    }

    public get currentPassword(): string {
        return this._currentPassword;
    }

    //#endregion currentPassword

    //#region password

    @observable private _password: string = null;

    @TypeSystem.propertyDecorator('string')
    public set password(val: string) {
        this._password = val;
    }

    public get password(): string {
        return this._password;
    }

    //#endregion password

    //#region confirmPassword

    @observable private _confirmPassword: string = null;

    @TypeSystem.propertyDecorator('string')
    public set confirmPassword(val: string) {
        this._confirmPassword = val;
    }

    public get confirmPassword(): string {
        return this._confirmPassword;
    }

    //#endregion confirmPassword

    //#region acceptedTerms

    @observable private _acceptedTerms: boolean = null;

    @TypeSystem.propertyDecorator('boolean')
    public set acceptedTerms(val: boolean) {
        this._acceptedTerms = val;
    }

    public get acceptedTerms(): boolean {
        return this._acceptedTerms;
    }

    //#endregion acceptedTerms

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('UserRegistrationResult', moduleContext.moduleName)
export class UserRegistrationResult extends BaseDataModel {

    //#region emailAlreadyExists

    @observable private _emailAlreadyExists: boolean = null;

    @TypeSystem.propertyDecorator('boolean')
    public set emailAlreadyExists(val: boolean) {
        this._emailAlreadyExists = val;
    }

    public get emailAlreadyExists(): boolean {
        return this._emailAlreadyExists;
    }

    //#endregion emailAlreadyExists

    //#region emailUserStillNotActivated

    @observable private _emailUserStillNotActivated: boolean = null;

    @TypeSystem.propertyDecorator('boolean')
    public set emailUserStillNotActivated(val: boolean) {
        this._emailUserStillNotActivated = val;
    }

    public get emailUserStillNotActivated(): boolean {
        return this._emailUserStillNotActivated;
    }

    //#endregion emailUserStillNotActivated

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('UserConfirmRegistrationResult', moduleContext.moduleName)
export class UserConfirmRegistrationResult extends BaseDataModel {

    //#region isProcessExpired

    @observable private _isProcessExpired: boolean = null;

    @TypeSystem.propertyDecorator('boolean')
    public set isProcessExpired(val: boolean) {
        this._isProcessExpired = val;
    }

    public get isProcessExpired(): boolean {
        return this._isProcessExpired;
    }

    //#endregion isProcessExpired

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('CompleteForgottenPasswordModel', moduleContext.moduleName)
export class CompleteForgottenPasswordModel extends BaseDataModel {

    //#region processId

    @observable private _processId: string = null;

    @TypeSystem.propertyDecorator('string')
    public set processId(val: string) {
        this._processId = val;
    }

    public get processId(): string {
        return this._processId;
    }

    //#endregion processId

    //#region newPassword

    @observable private _newPassword: string = null;

    @TypeSystem.propertyDecorator('string')
    public set newPassword(val: string) {
        this._newPassword = val;
    }

    public get newPassword(): string {
        return this._newPassword;
    }

    //#endregion newPassword

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('PaymentRequest', moduleContext.moduleName)
export class PaymentRequest extends BaseDataModel {
    @observable private _paymentRequestID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set paymentRequestID(val: number) {
        this._paymentRequestID = val;
    }

    public get paymentRequestID(): number {
        return this._paymentRequestID;
    }

    @observable private _registrationDataID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set registrationDataID(val: number) {
        this._registrationDataID = val;
    }

    public get registrationDataID(): number {
        return this._registrationDataID;
    }

    @observable private _registrationDataType: RegistrationDataTypes = null;

    @TypeSystem.propertyDecorator(RegistrationDataTypes ? RegistrationDataTypes : moduleContext.moduleName + '.RegistrationDataTypes')
    public set registrationDataType(val: RegistrationDataTypes) {
        this._registrationDataType = val;
    }

    public get registrationDataType(): RegistrationDataTypes {
        return this._registrationDataType;
    }

    @observable private _status: PaymentRequestStatuses = null;

    @TypeSystem.propertyDecorator(PaymentRequestStatuses ? PaymentRequestStatuses : moduleContext.moduleName + '.PaymentRequestStatuses')
    public set status(val: PaymentRequestStatuses) {
        this._status = val;
    }

    public get status(): PaymentRequestStatuses {
        return this._status;
    }

    @observable private _obligationID: number = null;

    @TypeSystem.propertyDecorator('number')
    public set obligationID(val: number) {
        this._obligationID = val;
    }

    public get obligationID(): number {
        return this._obligationID;
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

    @observable private _sendDate: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set sendDate(val: moment.Moment) {
        this._sendDate = val;
    }

    public get sendDate(): moment.Moment {
        return this._sendDate;
    }

    @observable private _payDate: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set payDate(val: moment.Moment) {
        this._payDate = val;
    }

    public get payDate(): moment.Moment {
        return this._payDate;
    }

    @observable private _externalPortalPaymentNumber: string = null;

    @TypeSystem.propertyDecorator('string')
    public set externalPortalPaymentNumber(val: string) {
        this._externalPortalPaymentNumber = val;
    }

    public get externalPortalPaymentNumber(): string {
        return this._externalPortalPaymentNumber;
    }

    @observable private _amount: number = null;

    @TypeSystem.propertyDecorator('number')
    public set amount(val: number) {
        this._amount = val;
    }

    public get amount(): number {
        return this._amount;
    }

    //#region additionalData

    @observable private _additionalData: any = null;

    @TypeSystem.propertyDecorator('any')
    public set additionalData(val: any) {
        this._additionalData = val;
    }

    public get additionalData(): any {
        return this._additionalData;
    }

    //#endregion additionalData

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

    constructor(data?: any) {
        super(data);

        this.copyFrom(data);
    }
}

@TypeSystem.typeDecorator('DocumentInfo', moduleContext.moduleName)
export class DocumentInfo extends BaseDataModel {

    //#region documentURI

    @observable private _documentURI: URI = null;

    @TypeSystem.propertyDecorator(URI)
    public set documentURI(val: URI) {
        this._documentURI = val;
    }

    public get documentURI(): URI {
        return this._documentURI;
    }

    //#endregion documentURI 

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

    //#region documentTypeURI

    @observable private _documentTypeURI: string = null;

    @TypeSystem.propertyDecorator('string')
    public set documentTypeURI(val: string) {
        this._documentTypeURI = val;
    }

    public get documentTypeURI(): string {
        return this._documentTypeURI;
    }

    //#endregion documentTypeURI 

    //#region registrationTime

    @observable private _registrationTime: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set registrationTime(val: moment.Moment) {
        this._registrationTime = val;
    }

    public get registrationTime(): moment.Moment {
        return this._registrationTime;
    }

    //#endregion registrationTime 

    //#region creationTime

    @observable private _creationTime: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set creationTime(val: moment.Moment) {
        this._creationTime = val;
    }

    public get creationTime(): moment.Moment {
        return this._creationTime;
    }

    //#endregion creationTime 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('CaseFileInfo', moduleContext.moduleName)
export class CaseFileInfo extends BaseDataModel {

    //#region caseFileURI

    @observable private _caseFileURI: URI = null;

    @TypeSystem.propertyDecorator(URI)
    public set caseFileURI(val: URI) {
        this._caseFileURI = val;
    }

    public get caseFileURI(): URI {
        return this._caseFileURI;
    }

    //#endregion caseFileURI 

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

    //#region documents

    @observable private _documents: DocumentInfo[] = null;

    @TypeSystem.propertyArrayDecorator(moduleContext.moduleName + '.' + 'DocumentInfo')
    public set documents(val: DocumentInfo[]) {
        this._documents = val;
    }

    public get documents(): DocumentInfo[] {
        return this._documents;
    }

    //#endregion documents 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('StageInstanceInfo', moduleContext.moduleName)
export class StageInstanceInfo extends BaseDataModel {

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

    //#region executedByNames

    @observable private _executedByNames: string = null;

    @TypeSystem.propertyDecorator('string')
    public set executedByNames(val: string) {
        this._executedByNames = val;
    }

    public get executedByNames(): string {
        return this._executedByNames;
    }

    //#endregion executedByNames 

    //#region actualStartDate

    @observable private _actualStartDate: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set actualStartDate(val: moment.Moment) {
        this._actualStartDate = val;
    }

    public get actualStartDate(): moment.Moment {
        return this._actualStartDate;
    }

    //#endregion actualStartDate 

    //#region actualCompletionDate

    @observable private _actualCompletionDate: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set actualCompletionDate(val: moment.Moment) {
        this._actualCompletionDate = val;
    }

    public get actualCompletionDate(): moment.Moment {
        return this._actualCompletionDate;
    }

    //#endregion actualCompletionDate 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('ServiceInstanceInfo', moduleContext.moduleName)
export class ServiceInstanceInfo extends BaseDataModel {

    //#region caseFileURI

    @observable private _caseFileURI: URI = null;

    @TypeSystem.propertyDecorator(URI)
    public set caseFileURI(val: URI) {
        this._caseFileURI = val;
    }

    public get caseFileURI(): URI {
        return this._caseFileURI;
    }

    //#endregion caseFileURI 

    //#region serviceName

    @observable private _serviceName: string = null;

    @TypeSystem.propertyDecorator('string')
    public set serviceName(val: string) {
        this._serviceName = val;
    }

    public get serviceName(): string {
        return this._serviceName;
    }

    //#endregion serviceName 

    //#region serviceDescription

    @observable private _serviceDescription: string = null;

    @TypeSystem.propertyDecorator('string')
    public set serviceDescription(val: string) {
        this._serviceDescription = val;
    }

    public get serviceDescription(): string {
        return this._serviceDescription;
    }

    //#endregion serviceDescription 

    //#region status

    @observable private _status: ServiceInstanceStatus = null;

    @TypeSystem.propertyDecorator(ServiceInstanceStatus)
    public set status(val: ServiceInstanceStatus) {
        this._status = val;
    }

    public get status(): ServiceInstanceStatus {
        return this._status;
    }

    //#endregion status 

    //#region initiatingDocumentURI

    @observable private _initiatingDocumentURI: URI = null;

    @TypeSystem.propertyDecorator(URI)
    public set initiatingDocumentURI(val: URI) {
        this._initiatingDocumentURI = val;
    }

    public get initiatingDocumentURI(): URI {
        return this._initiatingDocumentURI;
    }

    //#endregion initiatingDocumentURI 

    //#region sunauServiceUri

    @observable private _sunauServiceUri: string = null;

    @TypeSystem.propertyDecorator('string')
    public set sunauServiceUri(val: string) {
        this._sunauServiceUri = val;
    }

    public get sunauServiceUri(): string {
        return this._sunauServiceUri;
    }

    //#endregion sunauServiceUri 

    //#region executedStages

    @observable private _executedStages: StageInstanceInfo[] = null;

    @TypeSystem.propertyArrayDecorator(moduleContext.moduleName + '.' + 'StageInstanceInfo')
    public set executedStages(val: StageInstanceInfo[]) {
        this._executedStages = val;
    }

    public get executedStages(): StageInstanceInfo[] {
        return this._executedStages;
    }

    //#endregion executedStages 

    //#region unexecutedStages

    @observable private _unexecutedStages: StageInstanceInfo[] = null;

    @TypeSystem.propertyArrayDecorator(moduleContext.moduleName + '.' + 'StageInstanceInfo')
    public set unexecutedStages(val: StageInstanceInfo[]) {
        this._unexecutedStages = val;
    }

    public get unexecutedStages(): StageInstanceInfo[] {
        return this._unexecutedStages;
    }

    //#endregion unexecutedStages 

    //#region cancelledStages

    @observable private _cancelledStages: StageInstanceInfo[] = null;

    @TypeSystem.propertyArrayDecorator(moduleContext.moduleName + '.' + 'StageInstanceInfo')
    public set cancelledStages(val: StageInstanceInfo[]) {
        this._cancelledStages = val;
    }

    public get cancelledStages(): StageInstanceInfo[] {
        return this._cancelledStages;
    }

    //#endregion cancelledStages 

    //#region caseFile

    @observable private _caseFile: CaseFileInfo = null;

    @TypeSystem.propertyDecorator(moduleContext.moduleName + '.' + 'CaseFileInfo')
    public set caseFile(val: CaseFileInfo) {
        this._caseFile = val;
    }

    public get caseFile(): CaseFileInfo {
        return this._caseFile;
    }

    //#endregion caseFile 

    //#region reportData

    @observable private _reportData: moment.Moment = null;

    @TypeSystem.propertyDecorator('moment')
    public set reportData(val: moment.Moment) {
        this._reportData = val;
    }

    public get reportData(): moment.Moment {
        return this._reportData;
    }

    //#endregion reportData 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('AccessCodeUIResponse', moduleContext.moduleName)
export class AccessCodeUIResponse extends BaseDataModel {
    //#region access code

    @observable private _code: string = null;

    @TypeSystem.propertyDecorator('string')
    public set code(val: string) {
        this._code = val;
    }

    public get code(): string {
        return this._code;
    }

    //#endregion name 

    constructor(obj?: any) {
        super(obj)

        this.copyFrom(obj);
    }
}

@TypeSystem.typeDecorator('ANDObligationSearchCriteria', moduleContext.moduleName)
export class ANDObligationSearchCriteria extends BaseDataModel {

    @observable private _obligatedPersonType: ObligatedPersonType = null;

    @TypeSystem.propertyDecorator(ObligatedPersonType ? ObligatedPersonType : moduleContext.moduleName + '.ObligatedPersonType')
    public set obligatedPersonType(val: ObligatedPersonType) {
        this._obligatedPersonType = val;
    }

    public get obligatedPersonType(): ObligatedPersonType {
        return this._obligatedPersonType;
    }

    @observable private _additinalDataForObligatedPersonType: AdditinalDataForObligatedPersonType = null;

    @TypeSystem.propertyDecorator(AdditinalDataForObligatedPersonType ? AdditinalDataForObligatedPersonType : moduleContext.moduleName + '.AdditinalDataForObligatedPersonType')
    public set additinalDataForObligatedPersonType(val: AdditinalDataForObligatedPersonType) {
        this._additinalDataForObligatedPersonType = val;
    }

    public get additinalDataForObligatedPersonType(): AdditinalDataForObligatedPersonType {
        return this._additinalDataForObligatedPersonType;
    }

    @observable private _mode: ANDObligationSearchMode = null;

    @TypeSystem.propertyDecorator(ANDObligationSearchMode ? ANDObligationSearchMode : moduleContext.moduleName + '.ANDObligationSearchMode')
    public set mode(val: ANDObligationSearchMode) {
        this._mode = val;
    }

    public get mode(): ANDObligationSearchMode {
        return this._mode;
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

    @observable private _amount: number = null;

    @TypeSystem.propertyDecorator('number')
    public set amount(val: number) {
        this._amount = val;
    }

    public get amount(): number {
        return this._amount;
    }

    @observable private _isCallbackUrl: boolean = null;

    @TypeSystem.propertyDecorator('boolean')
    public set isCallbackUrl(val: boolean) {
        this._isCallbackUrl = val;
    }

    public get isCallbackUrl(): boolean {
        return this._isCallbackUrl;
    }

    constructor(data?: any) {
        super(data);

        this.copyFrom(data);
    }
}

@TypeSystem.typeDecorator('ObligationSearchResult', moduleContext.moduleName)
export class ObligationSearchResult extends BaseDataModel {
    @observable private _unitGroup: ObligationSearchResultUnitGroups = null;

    @TypeSystem.propertyDecorator(ObligationSearchResultUnitGroups ? ObligationSearchResultUnitGroups : moduleContext.moduleName + '.ObligationSearchResultUnitGroups')
    public set unitGroup(val: ObligationSearchResultUnitGroups) {
        this._unitGroup = val;
    }

    public get unitGroup(): ObligationSearchResultUnitGroups {
        return this._unitGroup;
    }

    @observable private _errorNoDataFound: boolean = null;

    @TypeSystem.propertyDecorator('boolean')
    public set errorNoDataFound(val: boolean) {
        this._errorNoDataFound = val;
    }

    public get errorNoDataFound(): boolean {
        return this._errorNoDataFound;
    }

    @observable private _errorReadingData: boolean = null;

    @TypeSystem.propertyDecorator('boolean')
    public set errorReadingData(val: boolean) {
        this._errorReadingData = val;
    }

    public get errorReadingData(): boolean {
        return this._errorReadingData;
    }

    @observable private _obligations: Obligation[] = null;

    @TypeSystem.propertyArrayDecorator(Obligation ? Obligation : moduleContext.moduleName + '.Obligation')
    public set obligations(val: Obligation[]) {
        this._obligations = val;
    }

    public get obligations(): Obligation[] {
        return this._obligations;
    }

    constructor(data?: any) {
        super(data);

        this.copyFrom(data);
    }
}

@TypeSystem.typeDecorator('ANDObligationSearchResponse', moduleContext.moduleName)
export class ANDObligationSearchResponse extends BaseDataModel {
    @observable private _obligationsData: ObligationSearchResult[] = null;

    @TypeSystem.propertyArrayDecorator(ObligationSearchResult ? ObligationSearchResult : moduleContext.moduleName + '.ObligationSearchResult')
    public set obligationsData(val: ObligationSearchResult[]) {
        this._obligationsData = val;
    }

    public get obligationsData(): ObligationSearchResult[] {
        return this._obligationsData;
    }

    @observable private _hasNonHandedSlip: boolean = null;

    @TypeSystem.propertyDecorator('boolean')
    public set hasNonHandedSlip(val: boolean) {
        this._hasNonHandedSlip = val;
    }

    public get hasNonHandedSlip(): boolean {
        return this._hasNonHandedSlip;
    }

    @observable private _errorOnHasNonHandedSlip: boolean = null;

    @TypeSystem.propertyDecorator('boolean')
    public set errorOnHasNonHandedSlip(val: boolean) {
        this._errorOnHasNonHandedSlip = val;
    }

    public get errorOnHasNonHandedSlip(): boolean {
        return this._errorOnHasNonHandedSlip;
    }

    constructor(data?: any) {
        super(data);

        this.copyFrom(data);
    }
}

@TypeSystem.typeDecorator('ObligationPaymentRequest', moduleContext.moduleName)
export class ObligationPaymentRequest extends BaseDataModel {
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

    @observable private _obligedIdentType: ObligedPersonIdentTypes = null;

    @TypeSystem.propertyDecorator(ObligedPersonIdentTypes ? ObligedPersonIdentTypes : moduleContext.moduleName + '.ObligedPersonIdentTypes')
    public set obligedPersonIdentType(val: ObligedPersonIdentTypes) {
        this._obligedIdentType = val;
    }

    public get obligedPersonIdentType(): ObligedPersonIdentTypes {
        return this._obligedIdentType;
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
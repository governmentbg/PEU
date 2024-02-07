import moment from 'moment';
import { observable } from 'mobx';
import { TypeSystem, BaseDataModel } from 'cnsys-core';
import { moduleContext } from '../ModuleContext';
import { ApplicationFormVMBase, PersonAddress, PersonalInformationVM, PoliceDepartment, PersonBasicDataVM } from 'eau-documents';

export enum WeaponNoticeType {
	Acquired = 0,
	Transferred = 1,
	SaleDonation = 2,
} 
TypeSystem.registerEnumInfo(WeaponNoticeType, 'WeaponNoticeType', moduleContext.moduleName); 

@TypeSystem.typeDecorator('ApplicationByFormAnnex9VM', moduleContext.moduleName)
export class ApplicationByFormAnnex9VM extends ApplicationFormVMBase {
	@observable private _circumstances: ApplicationByFormAnnex9DataVM = null;

	@TypeSystem.propertyDecorator(ApplicationByFormAnnex9DataVM ? ApplicationByFormAnnex9DataVM : moduleContext.moduleName + '.ApplicationByFormAnnex9DataVM')
	public set circumstances(val: ApplicationByFormAnnex9DataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationByFormAnnex9DataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationByFormAnnex9DataVM', moduleContext.moduleName)
export class ApplicationByFormAnnex9DataVM extends BaseDataModel {
	@observable private _sunauServiceUri: string = null;

	@TypeSystem.propertyDecorator('string')
	public set sunauServiceUri(val: string) {
		this._sunauServiceUri = val;
	}


	public get sunauServiceUri(): string {
		return this._sunauServiceUri;
	}

	@observable private _personalInformation: PersonalInformationVM = null;

	@TypeSystem.propertyDecorator(PersonalInformationVM ? PersonalInformationVM : moduleContext.moduleName + '.PersonalInformationVM')
	public set personalInformation(val: PersonalInformationVM) {
		this._personalInformation = val;
	}


	public get personalInformation(): PersonalInformationVM {
		return this._personalInformation;
	}

	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _issuingDocument: string = null;

	@TypeSystem.propertyDecorator('string')
	public set issuingDocument(val: string) {
		this._issuingDocument = val;
	}


	public get issuingDocument(): string {
		return this._issuingDocument;
	}

	@observable private _personGrantedFromIssuingDocument: PersonBasicDataVM = null;

	@TypeSystem.propertyDecorator(PersonBasicDataVM ? PersonBasicDataVM : moduleContext.moduleName + '.PersonBasicDataVM')
	public set personGrantedFromIssuingDocument(val: PersonBasicDataVM) {
		this._personGrantedFromIssuingDocument = val;
	}


	public get personGrantedFromIssuingDocument(): PersonBasicDataVM {
		return this._personGrantedFromIssuingDocument;
	}

	@observable private _specificDataForIssuingDocumentsForKOS: string = null;

	@TypeSystem.propertyDecorator('string')
	public set specificDataForIssuingDocumentsForKOS(val: string) {
		this._specificDataForIssuingDocumentsForKOS = val;
	}


	public get specificDataForIssuingDocumentsForKOS(): string {
		return this._specificDataForIssuingDocumentsForKOS;
	}

	@observable private _servicesWithOuterDocumentForThirdPerson: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set servicesWithOuterDocumentForThirdPerson(val: boolean) {
		this._servicesWithOuterDocumentForThirdPerson = val;
	}


	public get servicesWithOuterDocumentForThirdPerson(): boolean {
		return this._servicesWithOuterDocumentForThirdPerson;
	}

	@observable private _onlyGDNPPoliceDepartment: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set onlyGDNPPoliceDepartment(val: boolean) {
		this._onlyGDNPPoliceDepartment = val;
	}


	public get onlyGDNPPoliceDepartment(): boolean {
		return this._onlyGDNPPoliceDepartment;
	}

	@observable private _isRecipientEntity: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isRecipientEntity(val: boolean) {
		this._isRecipientEntity = val;
	}


	public get isRecipientEntity(): boolean {
		return this._isRecipientEntity;
	}

	@observable private _isSpecificDataForIssuingDocumentsForKOSRequired: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isSpecificDataForIssuingDocumentsForKOSRequired(val: boolean) {
		this._isSpecificDataForIssuingDocumentsForKOSRequired = val;
	}


	public get isSpecificDataForIssuingDocumentsForKOSRequired(): boolean {
		return this._isSpecificDataForIssuingDocumentsForKOSRequired;
	}

	@observable private _agreementToReceiveERefusal: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set agreementToReceiveERefusal(val: boolean) {
		this._agreementToReceiveERefusal = val;
	}


	public get agreementToReceiveERefusal(): boolean {
		return this._agreementToReceiveERefusal;
	}

	@observable private _persistedPersonAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set persistedPersonAddress(val: PersonAddress) {
		this._persistedPersonAddress = val;
	}


	public get persistedPersonAddress(): PersonAddress {
		return this._persistedPersonAddress;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationByFormAnnex10VM', moduleContext.moduleName)
export class ApplicationByFormAnnex10VM extends ApplicationFormVMBase {
	@observable private _circumstances: ApplicationByFormAnnex10DataVM = null;

	@TypeSystem.propertyDecorator(ApplicationByFormAnnex10DataVM ? ApplicationByFormAnnex10DataVM : moduleContext.moduleName + '.ApplicationByFormAnnex10DataVM')
	public set circumstances(val: ApplicationByFormAnnex10DataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationByFormAnnex10DataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationByFormAnnex10DataVM', moduleContext.moduleName)
export class ApplicationByFormAnnex10DataVM extends BaseDataModel {
	@observable private _sunauServiceUri: string = null;

	@TypeSystem.propertyDecorator('string')
	public set sunauServiceUri(val: string) {
		this._sunauServiceUri = val;
	}


	public get sunauServiceUri(): string {
		return this._sunauServiceUri;
	}

	@observable private _personalInformation: PersonalInformationVM = null;

	@TypeSystem.propertyDecorator(PersonalInformationVM ? PersonalInformationVM : moduleContext.moduleName + '.PersonalInformationVM')
	public set personalInformation(val: PersonalInformationVM) {
		this._personalInformation = val;
	}


	public get personalInformation(): PersonalInformationVM {
		return this._personalInformation;
	}

	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _issuingDocument: string = null;

	@TypeSystem.propertyDecorator('string')
	public set issuingDocument(val: string) {
		this._issuingDocument = val;
	}


	public get issuingDocument(): string {
		return this._issuingDocument;
	}

	@observable private _personGrantedFromIssuingDocument: PersonBasicDataVM = null;

	@TypeSystem.propertyDecorator(PersonBasicDataVM ? PersonBasicDataVM : moduleContext.moduleName + '.PersonBasicDataVM')
	public set personGrantedFromIssuingDocument(val: PersonBasicDataVM) {
		this._personGrantedFromIssuingDocument = val;
	}


	public get personGrantedFromIssuingDocument(): PersonBasicDataVM {
		return this._personGrantedFromIssuingDocument;
	}

	@observable private _specificDataForIssuingDocumentsForKOS: string = null;

	@TypeSystem.propertyDecorator('string')
	public set specificDataForIssuingDocumentsForKOS(val: string) {
		this._specificDataForIssuingDocumentsForKOS = val;
	}


	public get specificDataForIssuingDocumentsForKOS(): string {
		return this._specificDataForIssuingDocumentsForKOS;
	}

	@observable private _servicesWithOuterDocumentForThirdPerson: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set servicesWithOuterDocumentForThirdPerson(val: boolean) {
		this._servicesWithOuterDocumentForThirdPerson = val;
	}


	public get servicesWithOuterDocumentForThirdPerson(): boolean {
		return this._servicesWithOuterDocumentForThirdPerson;
	}

	@observable private _isRecipientEntity: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isRecipientEntity(val: boolean) {
		this._isRecipientEntity = val;
	}


	public get isRecipientEntity(): boolean {
		return this._isRecipientEntity;
	}

	@observable private _onlyGDNPPoliceDepartment: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set onlyGDNPPoliceDepartment(val: boolean) {
		this._onlyGDNPPoliceDepartment = val;
	}


	public get onlyGDNPPoliceDepartment(): boolean {
		return this._onlyGDNPPoliceDepartment;
	}

	@observable private _isSpecificDataForIssuingDocumentsForKOSRequired: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isSpecificDataForIssuingDocumentsForKOSRequired(val: boolean) {
		this._isSpecificDataForIssuingDocumentsForKOSRequired = val;
	}


	public get isSpecificDataForIssuingDocumentsForKOSRequired(): boolean {
		return this._isSpecificDataForIssuingDocumentsForKOSRequired;
	}

	@observable private _agreementToReceiveERefusal: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set agreementToReceiveERefusal(val: boolean) {
		this._agreementToReceiveERefusal = val;
	}


	public get agreementToReceiveERefusal(): boolean {
		return this._agreementToReceiveERefusal;
	}

	@observable private _persistedPersonAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set persistedPersonAddress(val: PersonAddress) {
		this._persistedPersonAddress = val;
	}


	public get persistedPersonAddress(): PersonAddress {
		return this._persistedPersonAddress;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotificationForNonFirearmVM', moduleContext.moduleName)
export class NotificationForNonFirearmVM extends ApplicationFormVMBase {
	@observable private _circumstances: NotificationForNonFirearmDataVM = null;

	@TypeSystem.propertyDecorator(NotificationForNonFirearmDataVM ? NotificationForNonFirearmDataVM : moduleContext.moduleName + '.NotificationForNonFirearmDataVM')
	public set circumstances(val: NotificationForNonFirearmDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): NotificationForNonFirearmDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotificationForNonFirearmDataVM', moduleContext.moduleName)
export class NotificationForNonFirearmDataVM extends BaseDataModel {
	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _weaponNoticeType: WeaponNoticeType = null;

	@TypeSystem.propertyDecorator(WeaponNoticeType ? WeaponNoticeType : moduleContext.moduleName + '.WeaponNoticeType')
	public set weaponNoticeType(val: WeaponNoticeType) {
		this._weaponNoticeType = val;
	}


	public get weaponNoticeType(): WeaponNoticeType {
		return this._weaponNoticeType;
	}

	@observable private _applicantInformation: PersonalInformationVM = null;

	@TypeSystem.propertyDecorator(PersonalInformationVM ? PersonalInformationVM : moduleContext.moduleName + '.PersonalInformationVM')
	public set applicantInformation(val: PersonalInformationVM) {
		this._applicantInformation = val;
	}


	public get applicantInformation(): PersonalInformationVM {
		return this._applicantInformation;
	}

	@observable private _purchaserInformation: string = null;

	@TypeSystem.propertyDecorator('string')
	public set purchaserInformation(val: string) {
		this._purchaserInformation = val;
	}


	public get purchaserInformation(): string {
		return this._purchaserInformation;
	}

	@observable private _technicalSpecificationsOfWeapons: TechnicalSpecificationOfWeapon[] = null;

	@TypeSystem.propertyArrayDecorator(TechnicalSpecificationOfWeapon ? TechnicalSpecificationOfWeapon : moduleContext.moduleName + '.TechnicalSpecificationOfWeapon')
	public set technicalSpecificationsOfWeapons(val: TechnicalSpecificationOfWeapon[]) {
		this._technicalSpecificationsOfWeapons = val;
	}


	public get technicalSpecificationsOfWeapons(): TechnicalSpecificationOfWeapon[] {
		return this._technicalSpecificationsOfWeapons;
	}

	@observable private _persistedPersonAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set persistedPersonAddress(val: PersonAddress) {
		this._persistedPersonAddress = val;
	}


	public get persistedPersonAddress(): PersonAddress {
		return this._persistedPersonAddress;
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
@TypeSystem.typeDecorator('NotificationForFirearmVM', moduleContext.moduleName)
export class NotificationForFirearmVM extends ApplicationFormVMBase {
	@observable private _circumstances: NotificationForFirearmDataVM = null;

	@TypeSystem.propertyDecorator(NotificationForFirearmDataVM ? NotificationForFirearmDataVM : moduleContext.moduleName + '.NotificationForFirearmDataVM')
	public set circumstances(val: NotificationForFirearmDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): NotificationForFirearmDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotificationForFirearmDataVM', moduleContext.moduleName)
export class NotificationForFirearmDataVM extends BaseDataModel {
	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _applicantInformation: PersonalInformationVM = null;

	@TypeSystem.propertyDecorator(PersonalInformationVM ? PersonalInformationVM : moduleContext.moduleName + '.PersonalInformationVM')
	public set applicantInformation(val: PersonalInformationVM) {
		this._applicantInformation = val;
	}


	public get applicantInformation(): PersonalInformationVM {
		return this._applicantInformation;
	}

	@observable private _purchaserUIC: string = null;

	@TypeSystem.propertyDecorator('string')
	public set purchaserUIC(val: string) {
		this._purchaserUIC = val;
	}


	public get purchaserUIC(): string {
		return this._purchaserUIC;
	}

	@observable private _agreementToReceiveERefusal: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set agreementToReceiveERefusal(val: boolean) {
		this._agreementToReceiveERefusal = val;
	}


	public get agreementToReceiveERefusal(): boolean {
		return this._agreementToReceiveERefusal;
	}

	@observable private _technicalSpecificationsOfWeapons: TechnicalSpecificationOfWeapon[] = null;

	@TypeSystem.propertyArrayDecorator(TechnicalSpecificationOfWeapon ? TechnicalSpecificationOfWeapon : moduleContext.moduleName + '.TechnicalSpecificationOfWeapon')
	public set technicalSpecificationsOfWeapons(val: TechnicalSpecificationOfWeapon[]) {
		this._technicalSpecificationsOfWeapons = val;
	}


	public get technicalSpecificationsOfWeapons(): TechnicalSpecificationOfWeapon[] {
		return this._technicalSpecificationsOfWeapons;
	}

	@observable private _persistedPersonAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set persistedPersonAddress(val: PersonAddress) {
		this._persistedPersonAddress = val;
	}


	public get persistedPersonAddress(): PersonAddress {
		return this._persistedPersonAddress;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotificationForControlCouponVM', moduleContext.moduleName)
export class NotificationForControlCouponVM extends ApplicationFormVMBase {
	@observable private _circumstances: NotificationForControlCouponDataVM = null;

	@TypeSystem.propertyDecorator(NotificationForControlCouponDataVM ? NotificationForControlCouponDataVM : moduleContext.moduleName + '.NotificationForControlCouponDataVM')
	public set circumstances(val: NotificationForControlCouponDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): NotificationForControlCouponDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('LicenseInfo', moduleContext.moduleName)
export class LicenseInfo extends BaseDataModel {
	@observable private _permitNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set permitNumber(val: string) {
		this._permitNumber = val;
	}


	public get permitNumber(): string {
		return this._permitNumber;
	}

	@observable private _permitType: string = null;

	@TypeSystem.propertyDecorator('string')
	public set permitType(val: string) {
		this._permitType = val;
	}


	public get permitType(): string {
		return this._permitType;
	}

	@observable private _permitTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set permitTypeName(val: string) {
		this._permitTypeName = val;
	}


	public get permitTypeName(): string {
		return this._permitTypeName;
	}

	@observable private _reason: string = null;

	@TypeSystem.propertyDecorator('string')
	public set reason(val: string) {
		this._reason = val;
	}


	public get reason(): string {
		return this._reason;
	}

	@observable private _reasonName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set reasonName(val: string) {
		this._reasonName = val;
	}


	public get reasonName(): string {
		return this._reasonName;
	}

	@observable private _validityPeriodStart: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set validityPeriodStart(val: moment.Moment) {
		this._validityPeriodStart = val;
	}


	public get validityPeriodStart(): moment.Moment {
		return this._validityPeriodStart;
	}

	@observable private _validityPeriodStartSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set validityPeriodStartSpecified(val: boolean) {
		this._validityPeriodStartSpecified = val;
	}


	public get validityPeriodStartSpecified(): boolean {
		return this._validityPeriodStartSpecified;
	}

	@observable private _validityPeriodEnd: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set validityPeriodEnd(val: moment.Moment) {
		this._validityPeriodEnd = val;
	}


	public get validityPeriodEnd(): moment.Moment {
		return this._validityPeriodEnd;
	}

	@observable private _validityPeriodEndSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set validityPeriodEndSpecified(val: boolean) {
		this._validityPeriodEndSpecified = val;
	}


	public get validityPeriodEndSpecified(): boolean {
		return this._validityPeriodEndSpecified;
	}

	@observable private _holderName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set holderName(val: string) {
		this._holderName = val;
	}


	public get holderName(): string {
		return this._holderName;
	}

	@observable private _holderIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set holderIdentifier(val: string) {
		this._holderIdentifier = val;
	}


	public get holderIdentifier(): string {
		return this._holderIdentifier;
	}

	@observable private _content: string = null;

	@TypeSystem.propertyDecorator('string')
	public set content(val: string) {
		this._content = val;
	}


	public get content(): string {
		return this._content;
	}

	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Ammunition', moduleContext.moduleName)
export class Ammunition extends BaseDataModel {
	@observable private _tradeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set tradeName(val: string) {
		this._tradeName = val;
	}


	public get tradeName(): string {
		return this._tradeName;
	}

	@observable private _numberOON: string = null;

	@TypeSystem.propertyDecorator('string')
	public set numberOON(val: string) {
		this._numberOON = val;
	}


	public get numberOON(): string {
		return this._numberOON;
	}

	@observable private _caliber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set caliber(val: string) {
		this._caliber = val;
	}


	public get caliber(): string {
		return this._caliber;
	}

	@observable private _count: string = null;

	@TypeSystem.propertyDecorator('string')
	public set count(val: string) {
		this._count = val;
	}


	public get count(): string {
		return this._count;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Pyrotechnics', moduleContext.moduleName)
export class Pyrotechnics extends BaseDataModel {
	@observable private _tradeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set tradeName(val: string) {
		this._tradeName = val;
	}


	public get tradeName(): string {
		return this._tradeName;
	}

	@observable private _kind: string = null;

	@TypeSystem.propertyDecorator('string')
	public set kind(val: string) {
		this._kind = val;
	}


	public get kind(): string {
		return this._kind;
	}

	@observable private _quantity: string = null;

	@TypeSystem.propertyDecorator('string')
	public set quantity(val: string) {
		this._quantity = val;
	}


	public get quantity(): string {
		return this._quantity;
	}

	@observable private _measure: string = null;

	@TypeSystem.propertyDecorator('string')
	public set measure(val: string) {
		this._measure = val;
	}


	public get measure(): string {
		return this._measure;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Explosives', moduleContext.moduleName)
export class Explosives extends BaseDataModel {
	@observable private _tradeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set tradeName(val: string) {
		this._tradeName = val;
	}


	public get tradeName(): string {
		return this._tradeName;
	}

	@observable private _numberOON: string = null;

	@TypeSystem.propertyDecorator('string')
	public set numberOON(val: string) {
		this._numberOON = val;
	}


	public get numberOON(): string {
		return this._numberOON;
	}

	@observable private _quantity: string = null;

	@TypeSystem.propertyDecorator('string')
	public set quantity(val: string) {
		this._quantity = val;
	}


	public get quantity(): string {
		return this._quantity;
	}

	@observable private _measure: string = null;

	@TypeSystem.propertyDecorator('string')
	public set measure(val: string) {
		this._measure = val;
	}


	public get measure(): string {
		return this._measure;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Firearms', moduleContext.moduleName)
export class Firearms extends BaseDataModel {
	@observable private _brand: string = null;

	@TypeSystem.propertyDecorator('string')
	public set brand(val: string) {
		this._brand = val;
	}


	public get brand(): string {
		return this._brand;
	}

	@observable private _model: string = null;

	@TypeSystem.propertyDecorator('string')
	public set model(val: string) {
		this._model = val;
	}


	public get model(): string {
		return this._model;
	}

	@observable private _caliber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set caliber(val: string) {
		this._caliber = val;
	}


	public get caliber(): string {
		return this._caliber;
	}

	@observable private _serialNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serialNumber(val: string) {
		this._serialNumber = val;
	}


	public get serialNumber(): string {
		return this._serialNumber;
	}

	@observable private _kindCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set kindCode(val: string) {
		this._kindCode = val;
	}


	public get kindCode(): string {
		return this._kindCode;
	}

	@observable private _kindName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set kindName(val: string) {
		this._kindName = val;
	}


	public get kindName(): string {
		return this._kindName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ControlCouponDataItemVM', moduleContext.moduleName)
export class ControlCouponDataItemVM extends BaseDataModel {
	@observable private _categoryCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set categoryCode(val: string) {
		this._categoryCode = val;
	}


	public get categoryCode(): string {
		return this._categoryCode;
	}

	@observable private _categoryName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set categoryName(val: string) {
		this._categoryName = val;
	}


	public get categoryName(): string {
		return this._categoryName;
	}

	@observable private _ammunition: Ammunition = null;

	@TypeSystem.propertyDecorator(Ammunition ? Ammunition : moduleContext.moduleName + '.Ammunition')
	public set ammunition(val: Ammunition) {
		this._ammunition = val;
	}


	public get ammunition(): Ammunition {
		return this._ammunition;
	}

	@observable private _pyrotechnics: Pyrotechnics = null;

	@TypeSystem.propertyDecorator(Pyrotechnics ? Pyrotechnics : moduleContext.moduleName + '.Pyrotechnics')
	public set pyrotechnics(val: Pyrotechnics) {
		this._pyrotechnics = val;
	}


	public get pyrotechnics(): Pyrotechnics {
		return this._pyrotechnics;
	}

	@observable private _explosives: Explosives = null;

	@TypeSystem.propertyDecorator(Explosives ? Explosives : moduleContext.moduleName + '.Explosives')
	public set explosives(val: Explosives) {
		this._explosives = val;
	}


	public get explosives(): Explosives {
		return this._explosives;
	}

	@observable private _firearms: Firearms = null;

	@TypeSystem.propertyDecorator(Firearms ? Firearms : moduleContext.moduleName + '.Firearms')
	public set firearms(val: Firearms) {
		this._firearms = val;
	}


	public get firearms(): Firearms {
		return this._firearms;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('NotificationForControlCouponDataVM', moduleContext.moduleName)
export class NotificationForControlCouponDataVM extends BaseDataModel {
	@observable private _licenseInfo: LicenseInfo = null;

	@TypeSystem.propertyDecorator(LicenseInfo ? LicenseInfo : moduleContext.moduleName + '.LicenseInfo')
	public set licenseInfo(val: LicenseInfo) {
		this._licenseInfo = val;
	}


	public get licenseInfo(): LicenseInfo {
		return this._licenseInfo;
	}

	@observable private _controlCouponData: ControlCouponDataItemVM[] = null;

	@TypeSystem.propertyArrayDecorator(ControlCouponDataItemVM ? ControlCouponDataItemVM : moduleContext.moduleName + '.ControlCouponDataItemVM')
	public set controlCouponData(val: ControlCouponDataItemVM[]) {
		this._controlCouponData = val;
	}


	public get controlCouponData(): ControlCouponDataItemVM[] {
		return this._controlCouponData;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('TechnicalSpecificationOfWeapon', moduleContext.moduleName)
export class TechnicalSpecificationOfWeapon extends BaseDataModel {
	@observable private _weaponTypeCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set weaponTypeCode(val: string) {
		this._weaponTypeCode = val;
	}


	public get weaponTypeCode(): string {
		return this._weaponTypeCode;
	}

	@observable private _weaponTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set weaponTypeName(val: string) {
		this._weaponTypeName = val;
	}


	public get weaponTypeName(): string {
		return this._weaponTypeName;
	}

	@observable private _weaponPurposeCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set weaponPurposeCode(val: string) {
		this._weaponPurposeCode = val;
	}


	public get weaponPurposeCode(): string {
		return this._weaponPurposeCode;
	}

	@observable private _weaponPurposeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set weaponPurposeName(val: string) {
		this._weaponPurposeName = val;
	}


	public get weaponPurposeName(): string {
		return this._weaponPurposeName;
	}

	@observable private _make: string = null;

	@TypeSystem.propertyDecorator('string')
	public set make(val: string) {
		this._make = val;
	}


	public get make(): string {
		return this._make;
	}

	@observable private _model: string = null;

	@TypeSystem.propertyDecorator('string')
	public set model(val: string) {
		this._model = val;
	}


	public get model(): string {
		return this._model;
	}

	@observable private _caliber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set caliber(val: string) {
		this._caliber = val;
	}


	public get caliber(): string {
		return this._caliber;
	}

	@observable private _weaponNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set weaponNumber(val: string) {
		this._weaponNumber = val;
	}


	public get weaponNumber(): string {
		return this._weaponNumber;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}

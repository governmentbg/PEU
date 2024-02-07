import moment from 'moment';
import { observable } from 'mobx';
import { TypeSystem, BaseDataModel } from 'cnsys-core';
import { moduleContext } from '../ModuleContext';
import { DocumentTypeURI, DocumentURI, SigningDocumentFormVMBase } from './ModelsManualAdded';

export enum ExecutionPeriodType {
	Hours = 1,
	Days = 2,
} 
TypeSystem.registerEnumInfo(ExecutionPeriodType, 'ExecutionPeriodType', moduleContext.moduleName); 

export enum RecipientChoiceType {
	Person = 0,
	ForeignPerson = 1,
	Entity = 2,
	ForeignEntity = 3,
} 
TypeSystem.registerEnumInfo(RecipientChoiceType, 'RecipientChoiceType', moduleContext.moduleName); 

export enum ItemChoiceType {
	Person = 0,
	ForeignCitizen = 1,
} 
TypeSystem.registerEnumInfo(ItemChoiceType, 'ItemChoiceType', moduleContext.moduleName); 

export enum ElectronicServiceAuthorQualityType {
	Personal = 0,
	Representative = 1,
	LegalRepresentative = 2,
	Official = 3,
} 
TypeSystem.registerEnumInfo(ElectronicServiceAuthorQualityType, 'ElectronicServiceAuthorQualityType', moduleContext.moduleName); 

export enum ForeignEntityIdentifierChoiceType {
	ForeignEntityIdentifier = 0,
	ForeignEntityOtherData = 1,
	ForeignEntityRegister = 2,
} 
TypeSystem.registerEnumInfo(ForeignEntityIdentifierChoiceType, 'ForeignEntityIdentifierChoiceType', moduleContext.moduleName); 

export enum PersonIdentifierChoiceType {
	EGN = 0,
	LNCh = 1,
} 
TypeSystem.registerEnumInfo(PersonIdentifierChoiceType, 'PersonIdentifierChoiceType', moduleContext.moduleName); 

export enum ServiceTermType {
	Regular = 1,
	Fast = 2,
	Express = 3,
} 
TypeSystem.registerEnumInfo(ServiceTermType, 'ServiceTermType', moduleContext.moduleName); 

export enum ApplicationType {
	AppForFirstReg = 0,
	AppForChangeData = 1,
	AppForRemoveInvalidData = 2,
	AppForWithdrawService = 3,
} 
TypeSystem.registerEnumInfo(ApplicationType, 'ApplicationType', moduleContext.moduleName); 

export enum ServiceResultReceiptMethods {
	EmailOrWebApplication = 1,
	Desk = 2,
	DeskInAdministration = 3,
	CourierToAddress = 4,
	CourierToOtherAddress = 5,
	CourierToMailBox = 6,
	UnitInAdministration = 7,
} 
TypeSystem.registerEnumInfo(ServiceResultReceiptMethods, 'ServiceResultReceiptMethods', moduleContext.moduleName); 

export enum ElectronicServiceProviderType {
	Administration = 1,
	PhysicalPerson = 2,
	Company = 3,
} 
TypeSystem.registerEnumInfo(ElectronicServiceProviderType, 'ElectronicServiceProviderType', moduleContext.moduleName); 

export enum BIDPersonalIdentificationDocumentReceivePlace {
	ODMVR = 0,
	RPU = 1,
} 
TypeSystem.registerEnumInfo(BIDPersonalIdentificationDocumentReceivePlace, 'BIDPersonalIdentificationDocumentReceivePlace', moduleContext.moduleName); 

export enum BIDMaritalStatus {
	Widowed = 0,
	Single = 1,
	Maried = 2,
	Divorsed = 3,
	Separated = 4,
	Unspecified = 5,
} 
TypeSystem.registerEnumInfo(BIDMaritalStatus, 'BIDMaritalStatus', moduleContext.moduleName); 

export enum DocumentElectronicTransportType {
	Item0006000001 = 0,
	Item0006000002 = 1,
	Item0006000003 = 2,
	Item0006000004 = 3,
} 
TypeSystem.registerEnumInfo(DocumentElectronicTransportType, 'DocumentElectronicTransportType', moduleContext.moduleName); 

export enum ElectronicDocumentDiscrepancyType {
	Item0006000005 = 0,
	Item0006000006 = 1,
	Item0006000007 = 2,
	Item0006000008 = 3,
	Item0006000009 = 4,
	Item0006000010 = 5,
	Item0006000011 = 6,
} 
TypeSystem.registerEnumInfo(ElectronicDocumentDiscrepancyType, 'ElectronicDocumentDiscrepancyType', moduleContext.moduleName); 

export enum BIDEyesColor {
	Brown = 0,
	Colorful = 1,
	Blue = 2,
	Gray = 3,
	Green = 4,
	Black = 5,
	None = 6,
	Red = 7,
	Heterochromia = 8,
} 
TypeSystem.registerEnumInfo(BIDEyesColor, 'BIDEyesColor', moduleContext.moduleName); 

export enum PersonIdentificationForeignStatut {
	EUCitizen = 0,
	ForeignerPermanently = 1,
	ForeignerTemporarily = 2,
	ForeignerPermanentlyWithoutResidencePermit = 3,
	ForeignerTemporarilyWithoutResidencePermit = 4,
} 
TypeSystem.registerEnumInfo(PersonIdentificationForeignStatut, 'PersonIdentificationForeignStatut', moduleContext.moduleName); 

export enum QualifierType {
	OIDAsURI = 0,
	OIDAsURN = 1,
} 
TypeSystem.registerEnumInfo(QualifierType, 'QualifierType', moduleContext.moduleName); 

export enum IdentityDocumentType {
	PersonalCard = 0,
	Passport = 1,
	DiplomaticPassport = 2,
	OfficialPassport = 3,
	SeaManPassport = 4,
	MilitaryIDCard = 5,
	DrivingLicense = 6,
	TemporaryPassport = 7,
	OfficeOpenSheetBorderCrossing = 8,
	TemporaryPassportForLeavingTheRepublicOfBulgaria = 9,
	RefugeeCard = 10,
	MapForeignerGrantedAsylum = 11,
	MapOfForeignerWithHumanitarianStatus = 12,
	TemporaryCardOfForeigner = 13,
	CertificateForTravelingAbroadOfRefugee = 14,
	CertificateForTravelingAbroadOfAForeignerGrantedAsylum = 15,
	CertificateForTravelingAbroadOfAForeignerWithHumanitarianStatus = 16,
	CertificateForTravelingAbroadForAPersonWithoutCitizenship = 17,
	TemporaryCertificateForLeavingTheRepublicOfBulgaria = 18,
	ResidencePermitForAContinuouslyStayingForeignerInBulgaria = 19,
	ResidencePermitForPermanentResidenceInBulgariaForeigner = 20,
	ResidencePermitForResidentFamilyMemberOfEUCitizenWhoHasNotExercised = 21,
	ResidencePermitForContinuouslyStayingForeignerMarkedBeneficiaryUnderArticle3 = 22,
	ResidencePermitForPermanentResidentAlienMarkedBeneficiaryUnderArticle3 = 23,
	ResidencePermitContinuousResidentFamilyMembers = 24,
	ResidencePermitForResidentFamilyMembers = 25,
	CertificateForLongStay = 26,
	CertificateOfResidence = 27,
	DiplomaticCard = 28,
	ConsularCard = 29,
	MapOfTheAdministrativeAndTechnicalStaff = 30,
	MapOfStaff = 31,
	CertificateForReturnToTheRepublicOfBulgariaToAForeigner = 32,
	ResidencePermitContinuousResidentFamilyMemberOfAnEUCitizen = 33,
	ResidencePermit = 34,
	ResidenceCertificateForEUCitizens = 35,
} 
TypeSystem.registerEnumInfo(IdentityDocumentType, 'IdentityDocumentType', moduleContext.moduleName); 

export enum ItemChoiceType1 {
	InRepublicOfBulgariaDocumentMustServeTo = 0,
	AbroadDocumentMustServeTo = 1,
} 
TypeSystem.registerEnumInfo(ItemChoiceType1, 'ItemChoiceType1', moduleContext.moduleName); 

export enum OfficialChoiceType {
	PersonNames = 0,
	ForeignCitizenNames = 1,
} 
TypeSystem.registerEnumInfo(OfficialChoiceType, 'OfficialChoiceType', moduleContext.moduleName); 

export enum ChoiceType {
	DocumentUri = 0,
	TextUri = 1,
} 
TypeSystem.registerEnumInfo(ChoiceType, 'ChoiceType', moduleContext.moduleName); 

export enum PersonAndEntityChoiceType {
	Person = 0,
	ForeignPerson = 1,
	Entity = 2,
	ForeignEntity = 3,
} 
TypeSystem.registerEnumInfo(PersonAndEntityChoiceType, 'PersonAndEntityChoiceType', moduleContext.moduleName); 

export enum ForeignEntityChoiceType {
	RegisterData = 1,
	OtherData = 2,
} 
TypeSystem.registerEnumInfo(ForeignEntityChoiceType, 'ForeignEntityChoiceType', moduleContext.moduleName); 

@TypeSystem.typeDecorator('IdentificationPhotoAndSignatureVM', moduleContext.moduleName)
export class IdentificationPhotoAndSignatureVM extends BaseDataModel {
	@observable private _identificationSignature: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identificationSignature(val: string) {
		this._identificationSignature = val;
	}


	public get identificationSignature(): string {
		return this._identificationSignature;
	}

	@observable private _identificationPhoto: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identificationPhoto(val: string) {
		this._identificationPhoto = val;
	}


	public get identificationPhoto(): string {
		return this._identificationPhoto;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('EkatteAddress', moduleContext.moduleName)
export class EkatteAddress extends BaseDataModel {
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

	@observable private _municipalityCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set municipalityCode(val: string) {
		this._municipalityCode = val;
	}


	public get municipalityCode(): string {
		return this._municipalityCode;
	}

	@observable private _municipalityName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set municipalityName(val: string) {
		this._municipalityName = val;
	}


	public get municipalityName(): string {
		return this._municipalityName;
	}

	@observable private _settlementCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set settlementCode(val: string) {
		this._settlementCode = val;
	}


	public get settlementCode(): string {
		return this._settlementCode;
	}

	@observable private _settlementName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set settlementName(val: string) {
		this._settlementName = val;
	}


	public get settlementName(): string {
		return this._settlementName;
	}

	@observable private _areaCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set areaCode(val: string) {
		this._areaCode = val;
	}


	public get areaCode(): string {
		return this._areaCode;
	}

	@observable private _areaName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set areaName(val: string) {
		this._areaName = val;
	}


	public get areaName(): string {
		return this._areaName;
	}

	@observable private _postCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set postCode(val: string) {
		this._postCode = val;
	}


	public get postCode(): string {
		return this._postCode;
	}

	@observable private _housingEstate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set housingEstate(val: string) {
		this._housingEstate = val;
	}


	public get housingEstate(): string {
		return this._housingEstate;
	}

	@observable private _street: string = null;

	@TypeSystem.propertyDecorator('string')
	public set street(val: string) {
		this._street = val;
	}


	public get street(): string {
		return this._street;
	}

	@observable private _streetNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set streetNumber(val: string) {
		this._streetNumber = val;
	}


	public get streetNumber(): string {
		return this._streetNumber;
	}

	@observable private _block: string = null;

	@TypeSystem.propertyDecorator('string')
	public set block(val: string) {
		this._block = val;
	}


	public get block(): string {
		return this._block;
	}

	@observable private _entrance: string = null;

	@TypeSystem.propertyDecorator('string')
	public set entrance(val: string) {
		this._entrance = val;
	}


	public get entrance(): string {
		return this._entrance;
	}

	@observable private _floor: string = null;

	@TypeSystem.propertyDecorator('string')
	public set floor(val: string) {
		this._floor = val;
	}


	public get floor(): string {
		return this._floor;
	}

	@observable private _apartment: string = null;

	@TypeSystem.propertyDecorator('string')
	public set apartment(val: string) {
		this._apartment = val;
	}


	public get apartment(): string {
		return this._apartment;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('GRAOAddress', moduleContext.moduleName)
export class GRAOAddress extends BaseDataModel {
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

	@observable private _municipalityGRAOCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set municipalityGRAOCode(val: string) {
		this._municipalityGRAOCode = val;
	}


	public get municipalityGRAOCode(): string {
		return this._municipalityGRAOCode;
	}

	@observable private _municipalityGRAOName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set municipalityGRAOName(val: string) {
		this._municipalityGRAOName = val;
	}


	public get municipalityGRAOName(): string {
		return this._municipalityGRAOName;
	}

	@observable private _settlementGRAOCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set settlementGRAOCode(val: string) {
		this._settlementGRAOCode = val;
	}


	public get settlementGRAOCode(): string {
		return this._settlementGRAOCode;
	}

	@observable private _settlementGRAOName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set settlementGRAOName(val: string) {
		this._settlementGRAOName = val;
	}


	public get settlementGRAOName(): string {
		return this._settlementGRAOName;
	}

	@observable private _streetGRAOCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set streetGRAOCode(val: string) {
		this._streetGRAOCode = val;
	}


	public get streetGRAOCode(): string {
		return this._streetGRAOCode;
	}

	@observable private _streetText: string = null;

	@TypeSystem.propertyDecorator('string')
	public set streetText(val: string) {
		this._streetText = val;
	}


	public get streetText(): string {
		return this._streetText;
	}

	@observable private _buildingNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set buildingNumber(val: string) {
		this._buildingNumber = val;
	}


	public get buildingNumber(): string {
		return this._buildingNumber;
	}

	@observable private _entrance: string = null;

	@TypeSystem.propertyDecorator('string')
	public set entrance(val: string) {
		this._entrance = val;
	}


	public get entrance(): string {
		return this._entrance;
	}

	@observable private _floor: string = null;

	@TypeSystem.propertyDecorator('string')
	public set floor(val: string) {
		this._floor = val;
	}


	public get floor(): string {
		return this._floor;
	}

	@observable private _apartment: string = null;

	@TypeSystem.propertyDecorator('string')
	public set apartment(val: string) {
		this._apartment = val;
	}


	public get apartment(): string {
		return this._apartment;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('IdentifierType', moduleContext.moduleName)
export class IdentifierType extends BaseDataModel {
	@observable private _qualifier: QualifierType = null;

	@TypeSystem.propertyDecorator(QualifierType ? QualifierType : moduleContext.moduleName + '.QualifierType')
	public set qualifier(val: QualifierType) {
		this._qualifier = val;
	}


	public get qualifier(): QualifierType {
		return this._qualifier;
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
@TypeSystem.typeDecorator('EntityBasicData', moduleContext.moduleName)
export class EntityBasicData extends BaseDataModel {
	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _identifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identifier(val: string) {
		this._identifier = val;
	}


	public get identifier(): string {
		return this._identifier;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DigitalSignatureContainerVM', moduleContext.moduleName)
export class DigitalSignatureContainerVM extends BaseDataModel {
	@observable private _signature: Signature = null;

	@TypeSystem.propertyDecorator(Signature ? Signature : moduleContext.moduleName + '.Signature')
	public set signature(val: Signature) {
		this._signature = val;
	}


	public get signature(): Signature {
		return this._signature;
	}

	@observable private _signatureUniqueID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set signatureUniqueID(val: string) {
		this._signatureUniqueID = val;
	}


	public get signatureUniqueID(): string {
		return this._signatureUniqueID;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationFormVMBase', moduleContext.moduleName)
export class ApplicationFormVMBase extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _electronicServiceApplicantContactData: ElectronicServiceApplicantContactData = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantContactData ? ElectronicServiceApplicantContactData : moduleContext.moduleName + '.ElectronicServiceApplicantContactData')
	public set electronicServiceApplicantContactData(val: ElectronicServiceApplicantContactData) {
		this._electronicServiceApplicantContactData = val;
	}


	public get electronicServiceApplicantContactData(): ElectronicServiceApplicantContactData {
		return this._electronicServiceApplicantContactData;
	}

	@observable private _serviceTermTypeAndApplicantReceipt: ServiceTermTypeAndApplicantReceiptVM = null;

	@TypeSystem.propertyDecorator(ServiceTermTypeAndApplicantReceiptVM ? ServiceTermTypeAndApplicantReceiptVM : moduleContext.moduleName + '.ServiceTermTypeAndApplicantReceiptVM')
	public set serviceTermTypeAndApplicantReceipt(val: ServiceTermTypeAndApplicantReceiptVM) {
		this._serviceTermTypeAndApplicantReceipt = val;
	}


	public get serviceTermTypeAndApplicantReceipt(): ServiceTermTypeAndApplicantReceiptVM {
		return this._serviceTermTypeAndApplicantReceipt;
	}

	@observable private _declarations: DeclarationsVM = null;

	@TypeSystem.propertyDecorator(DeclarationsVM ? DeclarationsVM : moduleContext.moduleName + '.DeclarationsVM')
	public set declarations(val: DeclarationsVM) {
		this._declarations = val;
	}


	public get declarations(): DeclarationsVM {
		return this._declarations;
	}

	@observable private _electronicAdministrativeServiceHeader: ElectronicAdministrativeServiceHeaderVM = null;

	@TypeSystem.propertyDecorator(ElectronicAdministrativeServiceHeaderVM ? ElectronicAdministrativeServiceHeaderVM : moduleContext.moduleName + '.ElectronicAdministrativeServiceHeaderVM')
	public set electronicAdministrativeServiceHeader(val: ElectronicAdministrativeServiceHeaderVM) {
		this._electronicAdministrativeServiceHeader = val;
	}


	public get electronicAdministrativeServiceHeader(): ElectronicAdministrativeServiceHeaderVM {
		return this._electronicAdministrativeServiceHeader;
	}

	@observable private _authorHasNonHandedSlip: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set authorHasNonHandedSlip(val: boolean) {
		this._authorHasNonHandedSlip = val;
	}


	public get authorHasNonHandedSlip(): boolean {
		return this._authorHasNonHandedSlip;
	}

	@observable private _authorHasNonPaidSlip: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set authorHasNonPaidSlip(val: boolean) {
		this._authorHasNonPaidSlip = val;
	}


	public get authorHasNonPaidSlip(): boolean {
		return this._authorHasNonPaidSlip;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DocumentsWillBeIssuedMessageVM', moduleContext.moduleName)
export class DocumentsWillBeIssuedMessageVM extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
	}

	@observable private _documentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set documentReceiptOrSigningDate(val: moment.Moment) {
		this._documentReceiptOrSigningDate = val;
	}


	public get documentReceiptOrSigningDate(): moment.Moment {
		return this._documentReceiptOrSigningDate;
	}

	@observable private _identityDocumentsWillBeIssuedMessage: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityDocumentsWillBeIssuedMessage(val: string) {
		this._identityDocumentsWillBeIssuedMessage = val;
	}


	public get identityDocumentsWillBeIssuedMessage(): string {
		return this._identityDocumentsWillBeIssuedMessage;
	}

	@observable private _policeDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set policeDepartment(val: PoliceDepartment) {
		this._policeDepartment = val;
	}


	public get policeDepartment(): PoliceDepartment {
		return this._policeDepartment;
	}

	@observable private _deliveryDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set deliveryDate(val: moment.Moment) {
		this._deliveryDate = val;
	}


	public get deliveryDate(): moment.Moment {
		return this._deliveryDate;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('OutstandingConditionsForStartOfServiceMessageVM', moduleContext.moduleName)
export class OutstandingConditionsForStartOfServiceMessageVM extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _documentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set documentReceiptOrSigningDate(val: moment.Moment) {
		this._documentReceiptOrSigningDate = val;
	}


	public get documentReceiptOrSigningDate(): moment.Moment {
		return this._documentReceiptOrSigningDate;
	}

	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _outstandingConditionsForStartOfServiceMessageHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set outstandingConditionsForStartOfServiceMessageHeader(val: string) {
		this._outstandingConditionsForStartOfServiceMessageHeader = val;
	}


	public get outstandingConditionsForStartOfServiceMessageHeader(): string {
		return this._outstandingConditionsForStartOfServiceMessageHeader;
	}

	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
	}

	@observable private _grounds: OutstandingConditionsForStartOfServiceMessageGrounds[] = null;

	@TypeSystem.propertyArrayDecorator(OutstandingConditionsForStartOfServiceMessageGrounds ? OutstandingConditionsForStartOfServiceMessageGrounds : moduleContext.moduleName + '.OutstandingConditionsForStartOfServiceMessageGrounds')
	public set grounds(val: OutstandingConditionsForStartOfServiceMessageGrounds[]) {
		this._grounds = val;
	}


	public get grounds(): OutstandingConditionsForStartOfServiceMessageGrounds[] {
		return this._grounds;
	}

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PaymentInstructionsVM', moduleContext.moduleName)
export class PaymentInstructionsVM extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
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

	@observable private _amount: number = null;

	@TypeSystem.propertyDecorator('number')
	public set amount(val: number) {
		this._amount = val;
	}


	public get amount(): number {
		return this._amount;
	}

	@observable private _amountCurrency: string = null;

	@TypeSystem.propertyDecorator('string')
	public set amountCurrency(val: string) {
		this._amountCurrency = val;
	}


	public get amountCurrency(): string {
		return this._amountCurrency;
	}

	@observable private _deadlineForPayment: DeadlineVM = null;

	@TypeSystem.propertyDecorator(DeadlineVM ? DeadlineVM : moduleContext.moduleName + '.DeadlineVM')
	public set deadlineForPayment(val: DeadlineVM) {
		this._deadlineForPayment = val;
	}


	public get deadlineForPayment(): DeadlineVM {
		return this._deadlineForPayment;
	}

	@observable private _deadlineMessage: string = null;

	@TypeSystem.propertyDecorator('string')
	public set deadlineMessage(val: string) {
		this._deadlineMessage = val;
	}


	public get deadlineMessage(): string {
		return this._deadlineMessage;
	}

	@observable private _paymentInstructionsHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set paymentInstructionsHeader(val: string) {
		this._paymentInstructionsHeader = val;
	}


	public get paymentInstructionsHeader(): string {
		return this._paymentInstructionsHeader;
	}

	@observable private _pepCin: string = null;

	@TypeSystem.propertyDecorator('string')
	public set pepCin(val: string) {
		this._pepCin = val;
	}


	public get pepCin(): string {
		return this._pepCin;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ReceiptAcknowledgedMessageVM', moduleContext.moduleName)
export class ReceiptAcknowledgedMessageVM extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _electronicServiceProvider: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProvider(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProvider = val;
	}


	public get electronicServiceProvider(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProvider;
	}

	@observable private _transportType: DocumentElectronicTransportType = null;

	@TypeSystem.propertyDecorator(DocumentElectronicTransportType ? DocumentElectronicTransportType : moduleContext.moduleName + '.DocumentElectronicTransportType')
	public set transportType(val: DocumentElectronicTransportType) {
		this._transportType = val;
	}


	public get transportType(): DocumentElectronicTransportType {
		return this._transportType;
	}

	@observable private _receiptTime: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set receiptTime(val: moment.Moment) {
		this._receiptTime = val;
	}


	public get receiptTime(): moment.Moment {
		return this._receiptTime;
	}

	@observable private _registeredBy: ReceiptAcknowledgedMessageRegisteredByVM = null;

	@TypeSystem.propertyDecorator(ReceiptAcknowledgedMessageRegisteredByVM ? ReceiptAcknowledgedMessageRegisteredByVM : moduleContext.moduleName + '.ReceiptAcknowledgedMessageRegisteredByVM')
	public set registeredBy(val: ReceiptAcknowledgedMessageRegisteredByVM) {
		this._registeredBy = val;
	}


	public get registeredBy(): ReceiptAcknowledgedMessageRegisteredByVM {
		return this._registeredBy;
	}

	@observable private _caseAccessIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set caseAccessIdentifier(val: string) {
		this._caseAccessIdentifier = val;
	}


	public get caseAccessIdentifier(): string {
		return this._caseAccessIdentifier;
	}

	@observable private _applicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set applicant(val: ElectronicServiceApplicantVM) {
		this._applicant = val;
	}


	public get applicant(): ElectronicServiceApplicantVM {
		return this._applicant;
	}

	@observable private _applicationDocumentTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set applicationDocumentTypeName(val: string) {
		this._applicationDocumentTypeName = val;
	}


	public get applicationDocumentTypeName(): string {
		return this._applicationDocumentTypeName;
	}

	@observable private _applicationDocumentTypeURI: DocumentTypeURI = null;

	@TypeSystem.propertyDecorator(DocumentTypeURI ? DocumentTypeURI : moduleContext.moduleName + '.DocumentTypeURI')
	public set applicationDocumentTypeURI(val: DocumentTypeURI) {
		this._applicationDocumentTypeURI = val;
	}


	public get applicationDocumentTypeURI(): DocumentTypeURI {
		return this._applicationDocumentTypeURI;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ReceiptAcknowledgedPaymentForMOIVM', moduleContext.moduleName)
export class ReceiptAcknowledgedPaymentForMOIVM extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _documentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set documentReceiptOrSigningDate(val: moment.Moment) {
		this._documentReceiptOrSigningDate = val;
	}


	public get documentReceiptOrSigningDate(): moment.Moment {
		return this._documentReceiptOrSigningDate;
	}

	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
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

	@observable private _amount: number = null;

	@TypeSystem.propertyDecorator('number')
	public set amount(val: number) {
		this._amount = val;
	}


	public get amount(): number {
		return this._amount;
	}

	@observable private _amountCurrency: string = null;

	@TypeSystem.propertyDecorator('string')
	public set amountCurrency(val: string) {
		this._amountCurrency = val;
	}


	public get amountCurrency(): string {
		return this._amountCurrency;
	}

	@observable private _receiptAcknowledgedPaymentMessage: string = null;

	@TypeSystem.propertyDecorator('string')
	public set receiptAcknowledgedPaymentMessage(val: string) {
		this._receiptAcknowledgedPaymentMessage = val;
	}


	public get receiptAcknowledgedPaymentMessage(): string {
		return this._receiptAcknowledgedPaymentMessage;
	}

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ReceiptNotAcknowledgedMessageVM', moduleContext.moduleName)
export class ReceiptNotAcknowledgedMessageVM extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _messageURI: DocumentURI = null;

	@TypeSystem.propertyDecorator(DocumentURI ? DocumentURI : moduleContext.moduleName + '.DocumentURI')
	public set messageURI(val: DocumentURI) {
		this._messageURI = val;
	}


	public get messageURI(): DocumentURI {
		return this._messageURI;
	}

	@observable private _electronicServiceProvider: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProvider(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProvider = val;
	}


	public get electronicServiceProvider(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProvider;
	}

	@observable private _transportType: DocumentElectronicTransportType = null;

	@TypeSystem.propertyDecorator(DocumentElectronicTransportType ? DocumentElectronicTransportType : moduleContext.moduleName + '.DocumentElectronicTransportType')
	public set transportType(val: DocumentElectronicTransportType) {
		this._transportType = val;
	}


	public get transportType(): DocumentElectronicTransportType {
		return this._transportType;
	}

	@observable private _discrepancies: ElectronicDocumentDiscrepancyType[] = null;

	@TypeSystem.propertyArrayDecorator(ElectronicDocumentDiscrepancyType ? ElectronicDocumentDiscrepancyType : moduleContext.moduleName + '.ElectronicDocumentDiscrepancyType')
	public set discrepancies(val: ElectronicDocumentDiscrepancyType[]) {
		this._discrepancies = val;
	}


	public get discrepancies(): ElectronicDocumentDiscrepancyType[] {
		return this._discrepancies;
	}

	@observable private _applicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set applicant(val: ElectronicServiceApplicantVM) {
		this._applicant = val;
	}


	public get applicant(): ElectronicServiceApplicantVM {
		return this._applicant;
	}

	@observable private _messageCreationTime: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set messageCreationTime(val: moment.Moment) {
		this._messageCreationTime = val;
	}


	public get messageCreationTime(): moment.Moment {
		return this._messageCreationTime;
	}

	@observable private _applicationDocumentTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set applicationDocumentTypeName(val: string) {
		this._applicationDocumentTypeName = val;
	}


	public get applicationDocumentTypeName(): string {
		return this._applicationDocumentTypeName;
	}

	@observable private _applicationDocumentTypeURI: DocumentTypeURI = null;

	@TypeSystem.propertyDecorator(DocumentTypeURI ? DocumentTypeURI : moduleContext.moduleName + '.DocumentTypeURI')
	public set applicationDocumentTypeURI(val: DocumentTypeURI) {
		this._applicationDocumentTypeURI = val;
	}


	public get applicationDocumentTypeURI(): DocumentTypeURI {
		return this._applicationDocumentTypeURI;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RemovingIrregularitiesInstructionsVM', moduleContext.moduleName)
export class RemovingIrregularitiesInstructionsVM extends SigningDocumentFormVMBase<OfficialVM> {
	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _irregularityDocumentURI: DocumentURI = null;

	@TypeSystem.propertyDecorator(DocumentURI ? DocumentURI : moduleContext.moduleName + '.DocumentURI')
	public set irregularityDocumentURI(val: DocumentURI) {
		this._irregularityDocumentURI = val;
	}


	public get irregularityDocumentURI(): DocumentURI {
		return this._irregularityDocumentURI;
	}

	@observable private _removingIrregularitiesInstructionsHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set removingIrregularitiesInstructionsHeader(val: string) {
		this._removingIrregularitiesInstructionsHeader = val;
	}


	public get removingIrregularitiesInstructionsHeader(): string {
		return this._removingIrregularitiesInstructionsHeader;
	}

	@observable private _applicationDocumentURI: DocumentURI = null;

	@TypeSystem.propertyDecorator(DocumentURI ? DocumentURI : moduleContext.moduleName + '.DocumentURI')
	public set applicationDocumentURI(val: DocumentURI) {
		this._applicationDocumentURI = val;
	}


	public get applicationDocumentURI(): DocumentURI {
		return this._applicationDocumentURI;
	}

	@observable private _applicationDocumentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set applicationDocumentReceiptOrSigningDate(val: moment.Moment) {
		this._applicationDocumentReceiptOrSigningDate = val;
	}


	public get applicationDocumentReceiptOrSigningDate(): moment.Moment {
		return this._applicationDocumentReceiptOrSigningDate;
	}

	@observable private _irregularityDocumentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set irregularityDocumentReceiptOrSigningDate(val: moment.Moment) {
		this._irregularityDocumentReceiptOrSigningDate = val;
	}


	public get irregularityDocumentReceiptOrSigningDate(): moment.Moment {
		return this._irregularityDocumentReceiptOrSigningDate;
	}

	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
	}

	@observable private _irregularities: RemovingIrregularitiesInstructionsIrregularitiesVM[] = null;

	@TypeSystem.propertyArrayDecorator(RemovingIrregularitiesInstructionsIrregularitiesVM ? RemovingIrregularitiesInstructionsIrregularitiesVM : moduleContext.moduleName + '.RemovingIrregularitiesInstructionsIrregularitiesVM')
	public set irregularities(val: RemovingIrregularitiesInstructionsIrregularitiesVM[]) {
		this._irregularities = val;
	}


	public get irregularities(): RemovingIrregularitiesInstructionsIrregularitiesVM[] {
		return this._irregularities;
	}

	@observable private _deadlineCorrectionIrregularities: DeadlineVM = null;

	@TypeSystem.propertyDecorator(DeadlineVM ? DeadlineVM : moduleContext.moduleName + '.DeadlineVM')
	public set deadlineCorrectionIrregularities(val: DeadlineVM) {
		this._deadlineCorrectionIrregularities = val;
	}


	public get deadlineCorrectionIrregularities(): DeadlineVM {
		return this._deadlineCorrectionIrregularities;
	}

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('TerminationOfServiceMessageVM', moduleContext.moduleName)
export class TerminationOfServiceMessageVM extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _documentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set documentReceiptOrSigningDate(val: moment.Moment) {
		this._documentReceiptOrSigningDate = val;
	}


	public get documentReceiptOrSigningDate(): moment.Moment {
		return this._documentReceiptOrSigningDate;
	}

	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _terminationOfServiceMessageHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set terminationOfServiceMessageHeader(val: string) {
		this._terminationOfServiceMessageHeader = val;
	}


	public get terminationOfServiceMessageHeader(): string {
		return this._terminationOfServiceMessageHeader;
	}

	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
	}

	@observable private _grounds: TerminationOfServiceMessageGrounds[] = null;

	@TypeSystem.propertyArrayDecorator(TerminationOfServiceMessageGrounds ? TerminationOfServiceMessageGrounds : moduleContext.moduleName + '.TerminationOfServiceMessageGrounds')
	public set grounds(val: TerminationOfServiceMessageGrounds[]) {
		this._grounds = val;
	}


	public get grounds(): TerminationOfServiceMessageGrounds[] {
		return this._grounds;
	}

	@observable private _terminationDocumentRegistrationNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set terminationDocumentRegistrationNumber(val: string) {
		this._terminationDocumentRegistrationNumber = val;
	}


	public get terminationDocumentRegistrationNumber(): string {
		return this._terminationDocumentRegistrationNumber;
	}

	@observable private _terminationDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set terminationDate(val: moment.Moment) {
		this._terminationDate = val;
	}


	public get terminationDate(): moment.Moment {
		return this._terminationDate;
	}

	@observable private _policeDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set policeDepartment(val: PoliceDepartment) {
		this._policeDepartment = val;
	}


	public get policeDepartment(): PoliceDepartment {
		return this._policeDepartment;
	}

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicServiceApplicantVM', moduleContext.moduleName)
export class ElectronicServiceApplicantVM extends BaseDataModel {
	@observable private _recipientGroup: RecipientGroupVM = null;

	@TypeSystem.propertyDecorator(RecipientGroupVM ? RecipientGroupVM : moduleContext.moduleName + '.RecipientGroupVM')
	public set recipientGroup(val: RecipientGroupVM) {
		this._recipientGroup = val;
	}


	public get recipientGroup(): RecipientGroupVM {
		return this._recipientGroup;
	}

	@observable private _sendApplicationWithReceiptAcknowledgedMessage: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set sendApplicationWithReceiptAcknowledgedMessage(val: boolean) {
		this._sendApplicationWithReceiptAcknowledgedMessage = val;
	}


	public get sendApplicationWithReceiptAcknowledgedMessage(): boolean {
		return this._sendApplicationWithReceiptAcknowledgedMessage;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicServiceApplicantContactData', moduleContext.moduleName)
export class ElectronicServiceApplicantContactData extends BaseDataModel {
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

	@observable private _municipalityCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set municipalityCode(val: string) {
		this._municipalityCode = val;
	}


	public get municipalityCode(): string {
		return this._municipalityCode;
	}

	@observable private _municipalityName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set municipalityName(val: string) {
		this._municipalityName = val;
	}


	public get municipalityName(): string {
		return this._municipalityName;
	}

	@observable private _settlementCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set settlementCode(val: string) {
		this._settlementCode = val;
	}


	public get settlementCode(): string {
		return this._settlementCode;
	}

	@observable private _settlementName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set settlementName(val: string) {
		this._settlementName = val;
	}


	public get settlementName(): string {
		return this._settlementName;
	}

	@observable private _postCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set postCode(val: string) {
		this._postCode = val;
	}


	public get postCode(): string {
		return this._postCode;
	}

	@observable private _addressDescription: string = null;

	@TypeSystem.propertyDecorator('string')
	public set addressDescription(val: string) {
		this._addressDescription = val;
	}


	public get addressDescription(): string {
		return this._addressDescription;
	}

	@observable private _postOfficeBox: string = null;

	@TypeSystem.propertyDecorator('string')
	public set postOfficeBox(val: string) {
		this._postOfficeBox = val;
	}


	public get postOfficeBox(): string {
		return this._postOfficeBox;
	}

	@observable private _phoneNumbers: PhoneNumbers = null;

	@TypeSystem.propertyDecorator(PhoneNumbers ? PhoneNumbers : moduleContext.moduleName + '.PhoneNumbers')
	public set phoneNumbers(val: PhoneNumbers) {
		this._phoneNumbers = val;
	}


	public get phoneNumbers(): PhoneNumbers {
		return this._phoneNumbers;
	}

	@observable private _faxNumbers: FaxNumbers = null;

	@TypeSystem.propertyDecorator(FaxNumbers ? FaxNumbers : moduleContext.moduleName + '.FaxNumbers')
	public set faxNumbers(val: FaxNumbers) {
		this._faxNumbers = val;
	}


	public get faxNumbers(): FaxNumbers {
		return this._faxNumbers;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceTermTypeAndApplicantReceiptVM', moduleContext.moduleName)
export class ServiceTermTypeAndApplicantReceiptVM extends BaseDataModel {
	@observable private _serviceApplicantReceiptData: ServiceApplicantReceiptDataVM = null;

	@TypeSystem.propertyDecorator(ServiceApplicantReceiptDataVM ? ServiceApplicantReceiptDataVM : moduleContext.moduleName + '.ServiceApplicantReceiptDataVM')
	public set serviceApplicantReceiptData(val: ServiceApplicantReceiptDataVM) {
		this._serviceApplicantReceiptData = val;
	}


	public get serviceApplicantReceiptData(): ServiceApplicantReceiptDataVM {
		return this._serviceApplicantReceiptData;
	}

	@observable private _serviceTermType: ServiceTermType = null;

	@TypeSystem.propertyDecorator(ServiceTermType ? ServiceTermType : moduleContext.moduleName + '.ServiceTermType')
	public set serviceTermType(val: ServiceTermType) {
		this._serviceTermType = val;
	}


	public get serviceTermType(): ServiceTermType {
		return this._serviceTermType;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DeclarationsVM', moduleContext.moduleName)
export class DeclarationsVM extends BaseDataModel {
	@observable private _declarations: DeclarationVM[] = null;

	@TypeSystem.propertyArrayDecorator(DeclarationVM ? DeclarationVM : moduleContext.moduleName + '.DeclarationVM')
	public set declarations(val: DeclarationVM[]) {
		this._declarations = val;
	}


	public get declarations(): DeclarationVM[] {
		return this._declarations;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DeclarationVM', moduleContext.moduleName)
export class DeclarationVM extends BaseDataModel {
	@observable private _isDeclarationFilled: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isDeclarationFilled(val: boolean) {
		this._isDeclarationFilled = val;
	}


	public get isDeclarationFilled(): boolean {
		return this._isDeclarationFilled;
	}

	@observable private _code: string = null;

	@TypeSystem.propertyDecorator('string')
	public set code(val: string) {
		this._code = val;
	}


	public get code(): string {
		return this._code;
	}

	@observable private _content: string = null;

	@TypeSystem.propertyDecorator('string')
	public set content(val: string) {
		this._content = val;
	}


	public get content(): string {
		return this._content;
	}

	@observable private _furtherDescriptionFromDeclarer: string = null;

	@TypeSystem.propertyDecorator('string')
	public set furtherDescriptionFromDeclarer(val: string) {
		this._furtherDescriptionFromDeclarer = val;
	}


	public get furtherDescriptionFromDeclarer(): string {
		return this._furtherDescriptionFromDeclarer;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicAdministrativeServiceHeaderVM', moduleContext.moduleName)
export class ElectronicAdministrativeServiceHeaderVM extends BaseDataModel {
	@observable private _applicationType: ApplicationType = null;

	@TypeSystem.propertyDecorator(ApplicationType ? ApplicationType : moduleContext.moduleName + '.ApplicationType')
	public set applicationType(val: ApplicationType) {
		this._applicationType = val;
	}


	public get applicationType(): ApplicationType {
		return this._applicationType;
	}

	@observable private _documentTypeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set documentTypeName(val: string) {
		this._documentTypeName = val;
	}


	public get documentTypeName(): string {
		return this._documentTypeName;
	}

	@observable private _documentTypeURI: DocumentTypeURI = null;

	@TypeSystem.propertyDecorator(DocumentTypeURI ? DocumentTypeURI : moduleContext.moduleName + '.DocumentTypeURI')
	public set documentTypeURI(val: DocumentTypeURI) {
		this._documentTypeURI = val;
	}


	public get documentTypeURI(): DocumentTypeURI {
		return this._documentTypeURI;
	}

	@observable private _documentURI: DocumentURI = null;

	@TypeSystem.propertyDecorator(DocumentURI ? DocumentURI : moduleContext.moduleName + '.DocumentURI')
	public set documentURI(val: DocumentURI) {
		this._documentURI = val;
	}


	public get documentURI(): DocumentURI {
		return this._documentURI;
	}

	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicData = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicData ? ElectronicServiceProviderBasicData : moduleContext.moduleName + '.ElectronicServiceProviderBasicData')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicData) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicData {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _sunauServiceName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set sunauServiceName(val: string) {
		this._sunauServiceName = val;
	}


	public get sunauServiceName(): string {
		return this._sunauServiceName;
	}

	@observable private _sunauServiceURI: string = null;

	@TypeSystem.propertyDecorator('string')
	public set sunauServiceURI(val: string) {
		this._sunauServiceURI = val;
	}


	public get sunauServiceURI(): string {
		return this._sunauServiceURI;
	}

	@observable private _admStructureUnitName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set admStructureUnitName(val: string) {
		this._admStructureUnitName = val;
	}


	public get admStructureUnitName(): string {
		return this._admStructureUnitName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('Signature', moduleContext.moduleName)
export class Signature extends BaseDataModel {
	@observable private _isValid: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isValid(val: boolean) {
		this._isValid = val;
	}


	public get isValid(): boolean {
		return this._isValid;
	}

	@observable private _error: string = null;

	@TypeSystem.propertyDecorator('string')
	public set error(val: string) {
		this._error = val;
	}


	public get error(): string {
		return this._error;
	}

	@observable private _signatureTime: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set signatureTime(val: moment.Moment) {
		this._signatureTime = val;
	}


	public get signatureTime(): moment.Moment {
		return this._signatureTime;
	}

	@observable private _signatureUniqueID: string = null;

	@TypeSystem.propertyDecorator('string')
	public set signatureUniqueID(val: string) {
		this._signatureUniqueID = val;
	}


	public get signatureUniqueID(): string {
		return this._signatureUniqueID;
	}

	@observable private _signingCertificateData: SigningCertificateData = null;

	@TypeSystem.propertyDecorator(SigningCertificateData ? SigningCertificateData : moduleContext.moduleName + '.SigningCertificateData')
	public set signingCertificateData(val: SigningCertificateData) {
		this._signingCertificateData = val;
	}


	public get signingCertificateData(): SigningCertificateData {
		return this._signingCertificateData;
	}

	@observable private _timeStampInfos: TimeStampInfo[] = null;

	@TypeSystem.propertyArrayDecorator(TimeStampInfo ? TimeStampInfo : moduleContext.moduleName + '.TimeStampInfo')
	public set timeStampInfos(val: TimeStampInfo[]) {
		this._timeStampInfos = val;
	}


	public get timeStampInfos(): TimeStampInfo[] {
		return this._timeStampInfos;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RecipientGroupVM', moduleContext.moduleName)
export class RecipientGroupVM extends BaseDataModel {
	@observable private _authorWithQuality: AuthorWithQualityVM = null;

	@TypeSystem.propertyDecorator(AuthorWithQualityVM ? AuthorWithQualityVM : moduleContext.moduleName + '.AuthorWithQualityVM')
	public set authorWithQuality(val: AuthorWithQualityVM) {
		this._authorWithQuality = val;
	}


	public get authorWithQuality(): AuthorWithQualityVM {
		return this._authorWithQuality;
	}

	@observable private _recipient: ElectronicServiceRecipientVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceRecipientVM ? ElectronicServiceRecipientVM : moduleContext.moduleName + '.ElectronicServiceRecipientVM')
	public set recipient(val: ElectronicServiceRecipientVM) {
		this._recipient = val;
	}


	public get recipient(): ElectronicServiceRecipientVM {
		return this._recipient;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PhoneNumbers', moduleContext.moduleName)
export class PhoneNumbers extends BaseDataModel {
	@observable private _phoneNumberCollection: string[] = null;

	@TypeSystem.propertyArrayDecorator('string')
	public set phoneNumberCollection(val: string[]) {
		this._phoneNumberCollection = val;
	}


	public get phoneNumberCollection(): string[] {
		return this._phoneNumberCollection;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('FaxNumbers', moduleContext.moduleName)
export class FaxNumbers extends BaseDataModel {
	@observable private _faxNumberCollection: string[] = null;

	@TypeSystem.propertyArrayDecorator('string')
	public set faxNumberCollection(val: string[]) {
		this._faxNumberCollection = val;
	}


	public get faxNumberCollection(): string[] {
		return this._faxNumberCollection;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceApplicantReceiptDataVM', moduleContext.moduleName)
export class ServiceApplicantReceiptDataVM extends BaseDataModel {
	@observable private _serviceResultReceiptMethod: ServiceResultReceiptMethods = null;

	@TypeSystem.propertyDecorator(ServiceResultReceiptMethods ? ServiceResultReceiptMethods : moduleContext.moduleName + '.ServiceResultReceiptMethods')
	public set serviceResultReceiptMethod(val: ServiceResultReceiptMethods) {
		this._serviceResultReceiptMethod = val;
	}


	public get serviceResultReceiptMethod(): ServiceResultReceiptMethods {
		return this._serviceResultReceiptMethod;
	}

	@observable private _applicantAdress: ServiceApplicantReceiptDataAddress = null;

	@TypeSystem.propertyDecorator(ServiceApplicantReceiptDataAddress ? ServiceApplicantReceiptDataAddress : moduleContext.moduleName + '.ServiceApplicantReceiptDataAddress')
	public set applicantAdress(val: ServiceApplicantReceiptDataAddress) {
		this._applicantAdress = val;
	}


	public get applicantAdress(): ServiceApplicantReceiptDataAddress {
		return this._applicantAdress;
	}

	@observable private _municipalityAdministrationAdress: ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM = null;

	@TypeSystem.propertyDecorator(ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM ? ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM : moduleContext.moduleName + '.ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM')
	public set municipalityAdministrationAdress(val: ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM) {
		this._municipalityAdministrationAdress = val;
	}


	public get municipalityAdministrationAdress(): ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM {
		return this._municipalityAdministrationAdress;
	}

	@observable private _unitInAdministration: ServiceApplicantReceiptDataUnitInAdministration = null;

	@TypeSystem.propertyDecorator(ServiceApplicantReceiptDataUnitInAdministration ? ServiceApplicantReceiptDataUnitInAdministration : moduleContext.moduleName + '.ServiceApplicantReceiptDataUnitInAdministration')
	public set unitInAdministration(val: ServiceApplicantReceiptDataUnitInAdministration) {
		this._unitInAdministration = val;
	}


	public get unitInAdministration(): ServiceApplicantReceiptDataUnitInAdministration {
		return this._unitInAdministration;
	}

	@observable private _predifinedUnitInAdministration: ServiceApplicantReceiptDataUnitInAdministration = null;

	@TypeSystem.propertyDecorator(ServiceApplicantReceiptDataUnitInAdministration ? ServiceApplicantReceiptDataUnitInAdministration : moduleContext.moduleName + '.ServiceApplicantReceiptDataUnitInAdministration')
	public set predifinedUnitInAdministration(val: ServiceApplicantReceiptDataUnitInAdministration) {
		this._predifinedUnitInAdministration = val;
	}


	public get predifinedUnitInAdministration(): ServiceApplicantReceiptDataUnitInAdministration {
		return this._predifinedUnitInAdministration;
	}

	@observable private _usePredifinedUnitInAdministration: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set usePredifinedUnitInAdministration(val: boolean) {
		this._usePredifinedUnitInAdministration = val;
	}


	public get usePredifinedUnitInAdministration(): boolean {
		return this._usePredifinedUnitInAdministration;
	}

	@observable private _useFilteredUnitInAdministration: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set useFilteredUnitInAdministration(val: boolean) {
		this._useFilteredUnitInAdministration = val;
	}


	public get useFilteredUnitInAdministration(): boolean {
		return this._useFilteredUnitInAdministration;
	}

	@observable private _restrictReceiptUnitToPermanentAddress: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set restrictReceiptUnitToPermanentAddress(val: boolean) {
		this._restrictReceiptUnitToPermanentAddress = val;
	}


	public get restrictReceiptUnitToPermanentAddress(): boolean {
		return this._restrictReceiptUnitToPermanentAddress;
	}

	@observable private _postOfficeBox: string = null;

	@TypeSystem.propertyDecorator('string')
	public set postOfficeBox(val: string) {
		this._postOfficeBox = val;
	}


	public get postOfficeBox(): string {
		return this._postOfficeBox;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicServiceProviderBasicData', moduleContext.moduleName)
export class ElectronicServiceProviderBasicData extends BaseDataModel {
	@observable private _entityBasicData: EntityBasicData = null;

	@TypeSystem.propertyDecorator(EntityBasicData ? EntityBasicData : moduleContext.moduleName + '.EntityBasicData')
	public set entityBasicData(val: EntityBasicData) {
		this._entityBasicData = val;
	}


	public get entityBasicData(): EntityBasicData {
		return this._entityBasicData;
	}

	@observable private _electronicServiceProviderType: ElectronicServiceProviderType = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderType ? ElectronicServiceProviderType : moduleContext.moduleName + '.ElectronicServiceProviderType')
	public set electronicServiceProviderType(val: ElectronicServiceProviderType) {
		this._electronicServiceProviderType = val;
	}


	public get electronicServiceProviderType(): ElectronicServiceProviderType {
		return this._electronicServiceProviderType;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SigningCertificateData', moduleContext.moduleName)
export class SigningCertificateData extends BaseDataModel {
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

	@observable private _subjectAlternativeName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set subjectAlternativeName(val: string) {
		this._subjectAlternativeName = val;
	}


	public get subjectAlternativeName(): string {
		return this._subjectAlternativeName;
	}

	@observable private _validFrom: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set validFrom(val: moment.Moment) {
		this._validFrom = val;
	}


	public get validFrom(): moment.Moment {
		return this._validFrom;
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
@TypeSystem.typeDecorator('TimeStampInfo', moduleContext.moduleName)
export class TimeStampInfo extends BaseDataModel {
	@observable private _signingCertificateData: SigningCertificateData = null;

	@TypeSystem.propertyDecorator(SigningCertificateData ? SigningCertificateData : moduleContext.moduleName + '.SigningCertificateData')
	public set signingCertificateData(val: SigningCertificateData) {
		this._signingCertificateData = val;
	}


	public get signingCertificateData(): SigningCertificateData {
		return this._signingCertificateData;
	}

	@observable private _timeStampTime: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set timeStampTime(val: moment.Moment) {
		this._timeStampTime = val;
	}


	public get timeStampTime(): moment.Moment {
		return this._timeStampTime;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('AuthorWithQualityVM', moduleContext.moduleName)
export class AuthorWithQualityVM extends BaseDataModel {
	@observable private _selectedAuthorQuality: ElectronicServiceAuthorQualityType = null;

	@TypeSystem.propertyDecorator(ElectronicServiceAuthorQualityType ? ElectronicServiceAuthorQualityType : moduleContext.moduleName + '.ElectronicServiceAuthorQualityType')
	public set selectedAuthorQuality(val: ElectronicServiceAuthorQualityType) {
		this._selectedAuthorQuality = val;
	}


	public get selectedAuthorQuality(): ElectronicServiceAuthorQualityType {
		return this._selectedAuthorQuality;
	}

	@observable private _author: ElectronicStatementAuthorVM = null;

	@TypeSystem.propertyDecorator(ElectronicStatementAuthorVM ? ElectronicStatementAuthorVM : moduleContext.moduleName + '.ElectronicStatementAuthorVM')
	public set author(val: ElectronicStatementAuthorVM) {
		this._author = val;
	}


	public get author(): ElectronicStatementAuthorVM {
		return this._author;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonAndEntityBasicDataVM', moduleContext.moduleName)
export class PersonAndEntityBasicDataVM extends BaseDataModel {
	@observable private _itemPersonBasicData: PersonBasicDataVM = null;

	@TypeSystem.propertyDecorator(PersonBasicDataVM ? PersonBasicDataVM : moduleContext.moduleName + '.PersonBasicDataVM')
	public set itemPersonBasicData(val: PersonBasicDataVM) {
		this._itemPersonBasicData = val;
	}


	public get itemPersonBasicData(): PersonBasicDataVM {
		return this._itemPersonBasicData;
	}

	@observable private _itemForeignCitizenBasicData: ForeignCitizenBasicData = null;

	@TypeSystem.propertyDecorator(ForeignCitizenBasicData ? ForeignCitizenBasicData : moduleContext.moduleName + '.ForeignCitizenBasicData')
	public set itemForeignCitizenBasicData(val: ForeignCitizenBasicData) {
		this._itemForeignCitizenBasicData = val;
	}


	public get itemForeignCitizenBasicData(): ForeignCitizenBasicData {
		return this._itemForeignCitizenBasicData;
	}

	@observable private _itemEntityBasicData: EntityBasicData = null;

	@TypeSystem.propertyDecorator(EntityBasicData ? EntityBasicData : moduleContext.moduleName + '.EntityBasicData')
	public set itemEntityBasicData(val: EntityBasicData) {
		this._itemEntityBasicData = val;
	}


	public get itemEntityBasicData(): EntityBasicData {
		return this._itemEntityBasicData;
	}

	@observable private _itemForeignEntityBasicData: ForeignEntityBasicDataVM = null;

	@TypeSystem.propertyDecorator(ForeignEntityBasicDataVM ? ForeignEntityBasicDataVM : moduleContext.moduleName + '.ForeignEntityBasicDataVM')
	public set itemForeignEntityBasicData(val: ForeignEntityBasicDataVM) {
		this._itemForeignEntityBasicData = val;
	}


	public get itemForeignEntityBasicData(): ForeignEntityBasicDataVM {
		return this._itemForeignEntityBasicData;
	}

	@observable private _selectedChoiceType: PersonAndEntityChoiceType = null;

	@TypeSystem.propertyDecorator(PersonAndEntityChoiceType ? PersonAndEntityChoiceType : moduleContext.moduleName + '.PersonAndEntityChoiceType')
	public set selectedChoiceType(val: PersonAndEntityChoiceType) {
		this._selectedChoiceType = val;
	}


	public get selectedChoiceType(): PersonAndEntityChoiceType {
		return this._selectedChoiceType;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicServiceRecipientVM', moduleContext.moduleName)
export class ElectronicServiceRecipientVM extends PersonAndEntityBasicDataVM {
	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceApplicantReceiptDataAddress', moduleContext.moduleName)
export class ServiceApplicantReceiptDataAddress extends EkatteAddress {
	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM', moduleContext.moduleName)
export class ServiceApplicantReceiptDataMunicipalityAdministrationAdressVM extends BaseDataModel {
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

	@observable private _municipalityCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set municipalityCode(val: string) {
		this._municipalityCode = val;
	}


	public get municipalityCode(): string {
		return this._municipalityCode;
	}

	@observable private _municipalityName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set municipalityName(val: string) {
		this._municipalityName = val;
	}


	public get municipalityName(): string {
		return this._municipalityName;
	}

	@observable private _mayoraltyCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set mayoraltyCode(val: string) {
		this._mayoraltyCode = val;
	}


	public get mayoraltyCode(): string {
		return this._mayoraltyCode;
	}

	@observable private _mayoralty: string = null;

	@TypeSystem.propertyDecorator('string')
	public set mayoralty(val: string) {
		this._mayoralty = val;
	}


	public get mayoralty(): string {
		return this._mayoralty;
	}

	@observable private _areaCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set areaCode(val: string) {
		this._areaCode = val;
	}


	public get areaCode(): string {
		return this._areaCode;
	}

	@observable private _areaName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set areaName(val: string) {
		this._areaName = val;
	}


	public get areaName(): string {
		return this._areaName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceApplicantReceiptDataUnitInAdministration', moduleContext.moduleName)
export class ServiceApplicantReceiptDataUnitInAdministration extends BaseDataModel {
	@observable private _entityBasicData: EntityBasicData = null;

	@TypeSystem.propertyDecorator(EntityBasicData ? EntityBasicData : moduleContext.moduleName + '.EntityBasicData')
	public set entityBasicData(val: EntityBasicData) {
		this._entityBasicData = val;
	}


	public get entityBasicData(): EntityBasicData {
		return this._entityBasicData;
	}

	@observable private _administrativeDepartmentName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeDepartmentName(val: string) {
		this._administrativeDepartmentName = val;
	}


	public get administrativeDepartmentName(): string {
		return this._administrativeDepartmentName;
	}

	@observable private _administrativeDepartmentCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeDepartmentCode(val: string) {
		this._administrativeDepartmentCode = val;
	}


	public get administrativeDepartmentCode(): string {
		return this._administrativeDepartmentCode;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicStatementAuthorVM', moduleContext.moduleName)
export class ElectronicStatementAuthorVM extends PersonAndEntityBasicDataVM {
	@observable private _emailAddress: string = null;

	@TypeSystem.propertyDecorator('string')
	public set emailAddress(val: string) {
		this._emailAddress = val;
	}


	public get emailAddress(): string {
		return this._emailAddress;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonBasicDataVM', moduleContext.moduleName)
export class PersonBasicDataVM extends BaseDataModel {
	@observable private _names: PersonNames = null;

	@TypeSystem.propertyDecorator(PersonNames ? PersonNames : moduleContext.moduleName + '.PersonNames')
	public set names(val: PersonNames) {
		this._names = val;
	}


	public get names(): PersonNames {
		return this._names;
	}

	@observable private _identifier: PersonIdentifier = null;

	@TypeSystem.propertyDecorator(PersonIdentifier ? PersonIdentifier : moduleContext.moduleName + '.PersonIdentifier')
	public set identifier(val: PersonIdentifier) {
		this._identifier = val;
	}


	public get identifier(): PersonIdentifier {
		return this._identifier;
	}

	@observable private _identityDocument: IdentityDocumentBasicDataVM = null;

	@TypeSystem.propertyDecorator(IdentityDocumentBasicDataVM ? IdentityDocumentBasicDataVM : moduleContext.moduleName + '.IdentityDocumentBasicDataVM')
	public set identityDocument(val: IdentityDocumentBasicDataVM) {
		this._identityDocument = val;
	}


	public get identityDocument(): IdentityDocumentBasicDataVM {
		return this._identityDocument;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignCitizenBasicData', moduleContext.moduleName)
export class ForeignCitizenBasicData extends BaseDataModel {
	@observable private _names: ForeignCitizenNames = null;

	@TypeSystem.propertyDecorator(ForeignCitizenNames ? ForeignCitizenNames : moduleContext.moduleName + '.ForeignCitizenNames')
	public set names(val: ForeignCitizenNames) {
		this._names = val;
	}


	public get names(): ForeignCitizenNames {
		return this._names;
	}

	@observable private _birthDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set birthDateSpecified(val: boolean) {
		this._birthDateSpecified = val;
	}


	public get birthDateSpecified(): boolean {
		return this._birthDateSpecified;
	}

	@observable private _birthDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set birthDate(val: moment.Moment) {
		this._birthDate = val;
	}


	public get birthDate(): moment.Moment {
		return this._birthDate;
	}

	@observable private _placeOfBirth: ForeignCitizenPlaceOfBirth = null;

	@TypeSystem.propertyDecorator(ForeignCitizenPlaceOfBirth ? ForeignCitizenPlaceOfBirth : moduleContext.moduleName + '.ForeignCitizenPlaceOfBirth')
	public set placeOfBirth(val: ForeignCitizenPlaceOfBirth) {
		this._placeOfBirth = val;
	}


	public get placeOfBirth(): ForeignCitizenPlaceOfBirth {
		return this._placeOfBirth;
	}

	@observable private _identityDocument: ForeignCitizenIdentityDocument = null;

	@TypeSystem.propertyDecorator(ForeignCitizenIdentityDocument ? ForeignCitizenIdentityDocument : moduleContext.moduleName + '.ForeignCitizenIdentityDocument')
	public set identityDocument(val: ForeignCitizenIdentityDocument) {
		this._identityDocument = val;
	}


	public get identityDocument(): ForeignCitizenIdentityDocument {
		return this._identityDocument;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignEntityBasicDataVM', moduleContext.moduleName)
export class ForeignEntityBasicDataVM extends BaseDataModel {
	@observable private _foreignEntityName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set foreignEntityName(val: string) {
		this._foreignEntityName = val;
	}


	public get foreignEntityName(): string {
		return this._foreignEntityName;
	}

	@observable private _countryISO3166TwoLetterCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryISO3166TwoLetterCode(val: string) {
		this._countryISO3166TwoLetterCode = val;
	}


	public get countryISO3166TwoLetterCode(): string {
		return this._countryISO3166TwoLetterCode;
	}

	@observable private _countryNameCyrillic: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryNameCyrillic(val: string) {
		this._countryNameCyrillic = val;
	}


	public get countryNameCyrillic(): string {
		return this._countryNameCyrillic;
	}

	@observable private _selectedChoiceType: ForeignEntityChoiceType = null;

	@TypeSystem.propertyDecorator(ForeignEntityChoiceType ? ForeignEntityChoiceType : moduleContext.moduleName + '.ForeignEntityChoiceType')
	public set selectedChoiceType(val: ForeignEntityChoiceType) {
		this._selectedChoiceType = val;
	}


	public get selectedChoiceType(): ForeignEntityChoiceType {
		return this._selectedChoiceType;
	}

	@observable private _foreignEntityRegister: string = null;

	@TypeSystem.propertyDecorator('string')
	public set foreignEntityRegister(val: string) {
		this._foreignEntityRegister = val;
	}


	public get foreignEntityRegister(): string {
		return this._foreignEntityRegister;
	}

	@observable private _foreignEntityIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set foreignEntityIdentifier(val: string) {
		this._foreignEntityIdentifier = val;
	}


	public get foreignEntityIdentifier(): string {
		return this._foreignEntityIdentifier;
	}

	@observable private _foreignEntityOtherData: string = null;

	@TypeSystem.propertyDecorator('string')
	public set foreignEntityOtherData(val: string) {
		this._foreignEntityOtherData = val;
	}


	public get foreignEntityOtherData(): string {
		return this._foreignEntityOtherData;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonNames', moduleContext.moduleName)
export class PersonNames extends BaseDataModel {
	@observable private _first: string = null;

	@TypeSystem.propertyDecorator('string')
	public set first(val: string) {
		this._first = val;
	}


	public get first(): string {
		return this._first;
	}

	@observable private _middle: string = null;

	@TypeSystem.propertyDecorator('string')
	public set middle(val: string) {
		this._middle = val;
	}


	public get middle(): string {
		return this._middle;
	}

	@observable private _last: string = null;

	@TypeSystem.propertyDecorator('string')
	public set last(val: string) {
		this._last = val;
	}


	public get last(): string {
		return this._last;
	}

	@observable private _pseudonim: string = null;

	@TypeSystem.propertyDecorator('string')
	public set pseudonim(val: string) {
		this._pseudonim = val;
	}


	public get pseudonim(): string {
		return this._pseudonim;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonIdentifier', moduleContext.moduleName)
export class PersonIdentifier extends BaseDataModel {
	@observable private _item: string = null;

	@TypeSystem.propertyDecorator('string')
	public set item(val: string) {
		this._item = val;
	}


	public get item(): string {
		return this._item;
	}

	@observable private _itemElementName: PersonIdentifierChoiceType = null;

	@TypeSystem.propertyDecorator(PersonIdentifierChoiceType ? PersonIdentifierChoiceType : moduleContext.moduleName + '.PersonIdentifierChoiceType')
	public set itemElementName(val: PersonIdentifierChoiceType) {
		this._itemElementName = val;
	}


	public get itemElementName(): PersonIdentifierChoiceType {
		return this._itemElementName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('IdentityDocumentBasicDataVM', moduleContext.moduleName)
export class IdentityDocumentBasicDataVM extends BaseDataModel {
	@observable private _identityNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityNumber(val: string) {
		this._identityNumber = val;
	}


	public get identityNumber(): string {
		return this._identityNumber;
	}

	@observable private _identitityIssueDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set identitityIssueDate(val: moment.Moment) {
		this._identitityIssueDate = val;
	}


	public get identitityIssueDate(): moment.Moment {
		return this._identitityIssueDate;
	}

	@observable private _identityIssuer: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityIssuer(val: string) {
		this._identityIssuer = val;
	}


	public get identityIssuer(): string {
		return this._identityIssuer;
	}

	@observable private _identityDocumentType: IdentityDocumentType = null;

	@TypeSystem.propertyDecorator(IdentityDocumentType ? IdentityDocumentType : moduleContext.moduleName + '.IdentityDocumentType')
	public set identityDocumentType(val: IdentityDocumentType) {
		this._identityDocumentType = val;
	}


	public get identityDocumentType(): IdentityDocumentType {
		return this._identityDocumentType;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignCitizenNames', moduleContext.moduleName)
export class ForeignCitizenNames extends BaseDataModel {
	@observable private _firstCyrillic: string = null;

	@TypeSystem.propertyDecorator('string')
	public set firstCyrillic(val: string) {
		this._firstCyrillic = val;
	}


	public get firstCyrillic(): string {
		return this._firstCyrillic;
	}

	@observable private _lastCyrillic: string = null;

	@TypeSystem.propertyDecorator('string')
	public set lastCyrillic(val: string) {
		this._lastCyrillic = val;
	}


	public get lastCyrillic(): string {
		return this._lastCyrillic;
	}

	@observable private _otherCyrillic: string = null;

	@TypeSystem.propertyDecorator('string')
	public set otherCyrillic(val: string) {
		this._otherCyrillic = val;
	}


	public get otherCyrillic(): string {
		return this._otherCyrillic;
	}

	@observable private _pseudonimCyrillic: string = null;

	@TypeSystem.propertyDecorator('string')
	public set pseudonimCyrillic(val: string) {
		this._pseudonimCyrillic = val;
	}


	public get pseudonimCyrillic(): string {
		return this._pseudonimCyrillic;
	}

	@observable private _firstLatin: string = null;

	@TypeSystem.propertyDecorator('string')
	public set firstLatin(val: string) {
		this._firstLatin = val;
	}


	public get firstLatin(): string {
		return this._firstLatin;
	}

	@observable private _lastLatin: string = null;

	@TypeSystem.propertyDecorator('string')
	public set lastLatin(val: string) {
		this._lastLatin = val;
	}


	public get lastLatin(): string {
		return this._lastLatin;
	}

	@observable private _otherLatin: string = null;

	@TypeSystem.propertyDecorator('string')
	public set otherLatin(val: string) {
		this._otherLatin = val;
	}


	public get otherLatin(): string {
		return this._otherLatin;
	}

	@observable private _pseudonimLatin: string = null;

	@TypeSystem.propertyDecorator('string')
	public set pseudonimLatin(val: string) {
		this._pseudonimLatin = val;
	}


	public get pseudonimLatin(): string {
		return this._pseudonimLatin;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignCitizenPlaceOfBirth', moduleContext.moduleName)
export class ForeignCitizenPlaceOfBirth extends BaseDataModel {
	@observable private _countryCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryCode(val: string) {
		this._countryCode = val;
	}


	public get countryCode(): string {
		return this._countryCode;
	}

	@observable private _countryName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryName(val: string) {
		this._countryName = val;
	}


	public get countryName(): string {
		return this._countryName;
	}

	@observable private _settlementName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set settlementName(val: string) {
		this._settlementName = val;
	}


	public get settlementName(): string {
		return this._settlementName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignCitizenIdentityDocument', moduleContext.moduleName)
export class ForeignCitizenIdentityDocument extends BaseDataModel {
	@observable private _documentNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set documentNumber(val: string) {
		this._documentNumber = val;
	}


	public get documentNumber(): string {
		return this._documentNumber;
	}

	@observable private _documentType: string = null;

	@TypeSystem.propertyDecorator('string')
	public set documentType(val: string) {
		this._documentType = val;
	}


	public get documentType(): string {
		return this._documentType;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicServiceApplicant', moduleContext.moduleName)
export class ElectronicServiceApplicant extends BaseDataModel {
	@observable private _recipientGroups: RecipientGroup[] = null;

	@TypeSystem.propertyArrayDecorator(RecipientGroup ? RecipientGroup : moduleContext.moduleName + '.RecipientGroup')
	public set recipientGroups(val: RecipientGroup[]) {
		this._recipientGroups = val;
	}


	public get recipientGroups(): RecipientGroup[] {
		return this._recipientGroups;
	}

	@observable private _emailAddress: string = null;

	@TypeSystem.propertyDecorator('string')
	public set emailAddress(val: string) {
		this._emailAddress = val;
	}


	public get emailAddress(): string {
		return this._emailAddress;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RegisteredDocumentURI', moduleContext.moduleName)
export class RegisteredDocumentURI extends BaseDataModel {
	@observable private _item: any = null;

	@TypeSystem.propertyDecorator('any')
	public set item(val: any) {
		this._item = val;
	}


	public get item(): any {
		return this._item;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('AISCaseURI', moduleContext.moduleName)
export class AISCaseURI extends RegisteredDocumentURI {
	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PoliceDepartment', moduleContext.moduleName)
export class PoliceDepartment extends BaseDataModel {
	@observable private _policeDepartmentType: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set policeDepartmentType(val: boolean) {
		this._policeDepartmentType = val;
	}


	public get policeDepartmentType(): boolean {
		return this._policeDepartmentType;
	}

	@observable private _policeDepartmentTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set policeDepartmentTypeSpecified(val: boolean) {
		this._policeDepartmentTypeSpecified = val;
	}


	public get policeDepartmentTypeSpecified(): boolean {
		return this._policeDepartmentTypeSpecified;
	}

	@observable private _policeDepartmentCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set policeDepartmentCode(val: string) {
		this._policeDepartmentCode = val;
	}


	public get policeDepartmentCode(): string {
		return this._policeDepartmentCode;
	}

	@observable private _policeDepartmentName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set policeDepartmentName(val: string) {
		this._policeDepartmentName = val;
	}


	public get policeDepartmentName(): string {
		return this._policeDepartmentName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicServiceProviderBasicDataVM', moduleContext.moduleName)
export class ElectronicServiceProviderBasicDataVM extends BaseDataModel {
	@observable private _entityBasicData: EntityBasicData = null;

	@TypeSystem.propertyDecorator(EntityBasicData ? EntityBasicData : moduleContext.moduleName + '.EntityBasicData')
	public set entityBasicData(val: EntityBasicData) {
		this._entityBasicData = val;
	}


	public get entityBasicData(): EntityBasicData {
		return this._entityBasicData;
	}

	@observable private _electronicServiceProviderType: ElectronicServiceProviderType = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderType ? ElectronicServiceProviderType : moduleContext.moduleName + '.ElectronicServiceProviderType')
	public set electronicServiceProviderType(val: ElectronicServiceProviderType) {
		this._electronicServiceProviderType = val;
	}


	public get electronicServiceProviderType(): ElectronicServiceProviderType {
		return this._electronicServiceProviderType;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('AISCaseURIVM', moduleContext.moduleName)
export class AISCaseURIVM extends BaseDataModel {
	@observable private _choise: ChoiceType = null;

	@TypeSystem.propertyDecorator(ChoiceType ? ChoiceType : moduleContext.moduleName + '.ChoiceType')
	public set choise(val: ChoiceType) {
		this._choise = val;
	}


	public get choise(): ChoiceType {
		return this._choise;
	}

	@observable private _documentUri: DocumentURI = null;

	@TypeSystem.propertyDecorator(DocumentURI ? DocumentURI : moduleContext.moduleName + '.DocumentURI')
	public set documentUri(val: DocumentURI) {
		this._documentUri = val;
	}


	public get documentUri(): DocumentURI {
		return this._documentUri;
	}

	@observable private _textUri: string = null;

	@TypeSystem.propertyDecorator('string')
	public set textUri(val: string) {
		this._textUri = val;
	}


	public get textUri(): string {
		return this._textUri;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('OutstandingConditionsForStartOfServiceMessageGrounds', moduleContext.moduleName)
export class OutstandingConditionsForStartOfServiceMessageGrounds extends BaseDataModel {
	@observable private _outstandingConditionsForStartOfServiceMessageGround: string = null;

	@TypeSystem.propertyDecorator('string')
	public set outstandingConditionsForStartOfServiceMessageGround(val: string) {
		this._outstandingConditionsForStartOfServiceMessageGround = val;
	}


	public get outstandingConditionsForStartOfServiceMessageGround(): string {
		return this._outstandingConditionsForStartOfServiceMessageGround;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DeadlineVM', moduleContext.moduleName)
export class DeadlineVM extends BaseDataModel {
	@observable private _executionPeriodType: ExecutionPeriodType = null;

	@TypeSystem.propertyDecorator(ExecutionPeriodType ? ExecutionPeriodType : moduleContext.moduleName + '.ExecutionPeriodType')
	public set executionPeriodType(val: ExecutionPeriodType) {
		this._executionPeriodType = val;
	}


	public get executionPeriodType(): ExecutionPeriodType {
		return this._executionPeriodType;
	}

	@observable private _periodValue: number = null;

	@TypeSystem.propertyDecorator('number')
	public set periodValue(val: number) {
		this._periodValue = val;
	}


	public get periodValue(): number {
		return this._periodValue;
	}

	@observable private _originalDeadline: string = null;

	@TypeSystem.propertyDecorator('string')
	public set originalDeadline(val: string) {
		this._originalDeadline = val;
	}


	public get originalDeadline(): string {
		return this._originalDeadline;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ReceiptAcknowledgedMessageRegisteredByVM', moduleContext.moduleName)
export class ReceiptAcknowledgedMessageRegisteredByVM extends BaseDataModel {
	@observable private _itemAISURI: string = null;

	@TypeSystem.propertyDecorator('string')
	public set itemAISURI(val: string) {
		this._itemAISURI = val;
	}


	public get itemAISURI(): string {
		return this._itemAISURI;
	}

	@observable private _itemOfficer: ReceiptAcknowledgedMessageRegisteredByOfficerVM = null;

	@TypeSystem.propertyDecorator(ReceiptAcknowledgedMessageRegisteredByOfficerVM ? ReceiptAcknowledgedMessageRegisteredByOfficerVM : moduleContext.moduleName + '.ReceiptAcknowledgedMessageRegisteredByOfficerVM')
	public set itemOfficer(val: ReceiptAcknowledgedMessageRegisteredByOfficerVM) {
		this._itemOfficer = val;
	}


	public get itemOfficer(): ReceiptAcknowledgedMessageRegisteredByOfficerVM {
		return this._itemOfficer;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('OfficialVM', moduleContext.moduleName)
export class OfficialVM extends DigitalSignatureContainerVM {
	@observable private _hasAuthorQuality: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set hasAuthorQuality(val: boolean) {
		this._hasAuthorQuality = val;
	}


	public get hasAuthorQuality(): boolean {
		return this._hasAuthorQuality;
	}

	@observable private _electronicDocumentAuthorQuality: string = null;

	@TypeSystem.propertyDecorator('string')
	public set electronicDocumentAuthorQuality(val: string) {
		this._electronicDocumentAuthorQuality = val;
	}


	public get electronicDocumentAuthorQuality(): string {
		return this._electronicDocumentAuthorQuality;
	}

	@observable private _choise: OfficialChoiceType = null;

	@TypeSystem.propertyDecorator(OfficialChoiceType ? OfficialChoiceType : moduleContext.moduleName + '.OfficialChoiceType')
	public set choise(val: OfficialChoiceType) {
		this._choise = val;
	}


	public get choise(): OfficialChoiceType {
		return this._choise;
	}

	@observable private _position: string = null;

	@TypeSystem.propertyDecorator('string')
	public set position(val: string) {
		this._position = val;
	}


	public get position(): string {
		return this._position;
	}

	@observable private _personNames: PersonNames = null;

	@TypeSystem.propertyDecorator(PersonNames ? PersonNames : moduleContext.moduleName + '.PersonNames')
	public set personNames(val: PersonNames) {
		this._personNames = val;
	}


	public get personNames(): PersonNames {
		return this._personNames;
	}

	@observable private _foreignCitizenNames: ForeignCitizenNames = null;

	@TypeSystem.propertyDecorator(ForeignCitizenNames ? ForeignCitizenNames : moduleContext.moduleName + '.ForeignCitizenNames')
	public set foreignCitizenNames(val: ForeignCitizenNames) {
		this._foreignCitizenNames = val;
	}


	public get foreignCitizenNames(): ForeignCitizenNames {
		return this._foreignCitizenNames;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RemovingIrregularitiesInstructionsIrregularitiesVM', moduleContext.moduleName)
export class RemovingIrregularitiesInstructionsIrregularitiesVM extends BaseDataModel {
	@observable private _irregularityType: string = null;

	@TypeSystem.propertyDecorator('string')
	public set irregularityType(val: string) {
		this._irregularityType = val;
	}


	public get irregularityType(): string {
		return this._irregularityType;
	}

	@observable private _additionalInformationSpecifyingIrregularity: string = null;

	@TypeSystem.propertyDecorator('string')
	public set additionalInformationSpecifyingIrregularity(val: string) {
		this._additionalInformationSpecifyingIrregularity = val;
	}


	public get additionalInformationSpecifyingIrregularity(): string {
		return this._additionalInformationSpecifyingIrregularity;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('TerminationOfServiceMessageGrounds', moduleContext.moduleName)
export class TerminationOfServiceMessageGrounds extends BaseDataModel {
	@observable private _terminationOfServiceMessageGround: string = null;

	@TypeSystem.propertyDecorator('string')
	public set terminationOfServiceMessageGround(val: string) {
		this._terminationOfServiceMessageGround = val;
	}


	public get terminationOfServiceMessageGround(): string {
		return this._terminationOfServiceMessageGround;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RecipientGroup', moduleContext.moduleName)
export class RecipientGroup extends BaseDataModel {
	@observable private _authors: ElectronicStatementAuthor[] = null;

	@TypeSystem.propertyArrayDecorator(ElectronicStatementAuthor ? ElectronicStatementAuthor : moduleContext.moduleName + '.ElectronicStatementAuthor')
	public set authors(val: ElectronicStatementAuthor[]) {
		this._authors = val;
	}


	public get authors(): ElectronicStatementAuthor[] {
		return this._authors;
	}

	@observable private _authorQuality: string = null;

	@TypeSystem.propertyDecorator('string')
	public set authorQuality(val: string) {
		this._authorQuality = val;
	}


	public get authorQuality(): string {
		return this._authorQuality;
	}

	@observable private _recipients: ElectronicServiceRecipient[] = null;

	@TypeSystem.propertyArrayDecorator(ElectronicServiceRecipient ? ElectronicServiceRecipient : moduleContext.moduleName + '.ElectronicServiceRecipient')
	public set recipients(val: ElectronicServiceRecipient[]) {
		this._recipients = val;
	}


	public get recipients(): ElectronicServiceRecipient[] {
		return this._recipients;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ReceiptAcknowledgedMessageRegisteredByOfficerVM', moduleContext.moduleName)
export class ReceiptAcknowledgedMessageRegisteredByOfficerVM extends BaseDataModel {
	@observable private _personNames: AISUserNamesVM = null;

	@TypeSystem.propertyDecorator(AISUserNamesVM ? AISUserNamesVM : moduleContext.moduleName + '.AISUserNamesVM')
	public set personNames(val: AISUserNamesVM) {
		this._personNames = val;
	}


	public get personNames(): AISUserNamesVM {
		return this._personNames;
	}

	@observable private _aisUserIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set aisUserIdentifier(val: string) {
		this._aisUserIdentifier = val;
	}


	public get aisUserIdentifier(): string {
		return this._aisUserIdentifier;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicStatementAuthor', moduleContext.moduleName)
export class ElectronicStatementAuthor extends BaseDataModel {
	@observable private _authorQuality: any = null;

	@TypeSystem.propertyDecorator('any')
	public set authorQuality(val: any) {
		this._authorQuality = val;
	}


	public get authorQuality(): any {
		return this._authorQuality;
	}

	@observable private _item: any = null;

	@TypeSystem.propertyDecorator('any')
	public set item(val: any) {
		this._item = val;
	}


	public get item(): any {
		return this._item;
	}

	@observable private _itemPersonBasicData: PersonBasicData = null;

	@TypeSystem.propertyDecorator(PersonBasicData ? PersonBasicData : moduleContext.moduleName + '.PersonBasicData')
	public set itemPersonBasicData(val: PersonBasicData) {
		this._itemPersonBasicData = val;
	}


	public get itemPersonBasicData(): PersonBasicData {
		return this._itemPersonBasicData;
	}

	@observable private _itemForeignCitizenBasicData: ForeignCitizenBasicData = null;

	@TypeSystem.propertyDecorator(ForeignCitizenBasicData ? ForeignCitizenBasicData : moduleContext.moduleName + '.ForeignCitizenBasicData')
	public set itemForeignCitizenBasicData(val: ForeignCitizenBasicData) {
		this._itemForeignCitizenBasicData = val;
	}


	public get itemForeignCitizenBasicData(): ForeignCitizenBasicData {
		return this._itemForeignCitizenBasicData;
	}

	@observable private _itemElementName: ItemChoiceType = null;

	@TypeSystem.propertyDecorator(ItemChoiceType ? ItemChoiceType : moduleContext.moduleName + '.ItemChoiceType')
	public set itemElementName(val: ItemChoiceType) {
		this._itemElementName = val;
	}


	public get itemElementName(): ItemChoiceType {
		return this._itemElementName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ElectronicServiceRecipient', moduleContext.moduleName)
export class ElectronicServiceRecipient extends BaseDataModel {
	@observable private _item: any = null;

	@TypeSystem.propertyDecorator('any')
	public set item(val: any) {
		this._item = val;
	}


	public get item(): any {
		return this._item;
	}

	@observable private _itemPersonBasicData: PersonBasicData = null;

	@TypeSystem.propertyDecorator(PersonBasicData ? PersonBasicData : moduleContext.moduleName + '.PersonBasicData')
	public set itemPersonBasicData(val: PersonBasicData) {
		this._itemPersonBasicData = val;
	}


	public get itemPersonBasicData(): PersonBasicData {
		return this._itemPersonBasicData;
	}

	@observable private _itemForeignCitizenBasicData: ForeignCitizenBasicData = null;

	@TypeSystem.propertyDecorator(ForeignCitizenBasicData ? ForeignCitizenBasicData : moduleContext.moduleName + '.ForeignCitizenBasicData')
	public set itemForeignCitizenBasicData(val: ForeignCitizenBasicData) {
		this._itemForeignCitizenBasicData = val;
	}


	public get itemForeignCitizenBasicData(): ForeignCitizenBasicData {
		return this._itemForeignCitizenBasicData;
	}

	@observable private _itemEntityBasicData: EntityBasicData = null;

	@TypeSystem.propertyDecorator(EntityBasicData ? EntityBasicData : moduleContext.moduleName + '.EntityBasicData')
	public set itemEntityBasicData(val: EntityBasicData) {
		this._itemEntityBasicData = val;
	}


	public get itemEntityBasicData(): EntityBasicData {
		return this._itemEntityBasicData;
	}

	@observable private _itemForeignEntityBasicData: ForeignEntityBasicData = null;

	@TypeSystem.propertyDecorator(ForeignEntityBasicData ? ForeignEntityBasicData : moduleContext.moduleName + '.ForeignEntityBasicData')
	public set itemForeignEntityBasicData(val: ForeignEntityBasicData) {
		this._itemForeignEntityBasicData = val;
	}


	public get itemForeignEntityBasicData(): ForeignEntityBasicData {
		return this._itemForeignEntityBasicData;
	}

	@observable private _itemName: RecipientChoiceType = null;

	@TypeSystem.propertyDecorator(RecipientChoiceType ? RecipientChoiceType : moduleContext.moduleName + '.RecipientChoiceType')
	public set itemName(val: RecipientChoiceType) {
		this._itemName = val;
	}


	public get itemName(): RecipientChoiceType {
		return this._itemName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('AISUserNamesVM', moduleContext.moduleName)
export class AISUserNamesVM extends BaseDataModel {
	@observable private _itemPersonNames: PersonNames = null;

	@TypeSystem.propertyDecorator(PersonNames ? PersonNames : moduleContext.moduleName + '.PersonNames')
	public set itemPersonNames(val: PersonNames) {
		this._itemPersonNames = val;
	}


	public get itemPersonNames(): PersonNames {
		return this._itemPersonNames;
	}

	@observable private _itemForeignCitizenNames: ForeignCitizenNames = null;

	@TypeSystem.propertyDecorator(ForeignCitizenNames ? ForeignCitizenNames : moduleContext.moduleName + '.ForeignCitizenNames')
	public set itemForeignCitizenNames(val: ForeignCitizenNames) {
		this._itemForeignCitizenNames = val;
	}


	public get itemForeignCitizenNames(): ForeignCitizenNames {
		return this._itemForeignCitizenNames;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonBasicData', moduleContext.moduleName)
export class PersonBasicData extends BaseDataModel {
	@observable private _names: PersonNames = null;

	@TypeSystem.propertyDecorator(PersonNames ? PersonNames : moduleContext.moduleName + '.PersonNames')
	public set names(val: PersonNames) {
		this._names = val;
	}


	public get names(): PersonNames {
		return this._names;
	}

	@observable private _identifier: PersonIdentifier = null;

	@TypeSystem.propertyDecorator(PersonIdentifier ? PersonIdentifier : moduleContext.moduleName + '.PersonIdentifier')
	public set identifier(val: PersonIdentifier) {
		this._identifier = val;
	}


	public get identifier(): PersonIdentifier {
		return this._identifier;
	}

	@observable private _identityDocument: IdentityDocumentBasicData = null;

	@TypeSystem.propertyDecorator(IdentityDocumentBasicData ? IdentityDocumentBasicData : moduleContext.moduleName + '.IdentityDocumentBasicData')
	public set identityDocument(val: IdentityDocumentBasicData) {
		this._identityDocument = val;
	}


	public get identityDocument(): IdentityDocumentBasicData {
		return this._identityDocument;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignEntityBasicData', moduleContext.moduleName)
export class ForeignEntityBasicData extends BaseDataModel {
	@observable private _foreignEntityName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set foreignEntityName(val: string) {
		this._foreignEntityName = val;
	}


	public get foreignEntityName(): string {
		return this._foreignEntityName;
	}

	@observable private _countryISO3166TwoLetterCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryISO3166TwoLetterCode(val: string) {
		this._countryISO3166TwoLetterCode = val;
	}


	public get countryISO3166TwoLetterCode(): string {
		return this._countryISO3166TwoLetterCode;
	}

	@observable private _countryNameCyrillic: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryNameCyrillic(val: string) {
		this._countryNameCyrillic = val;
	}


	public get countryNameCyrillic(): string {
		return this._countryNameCyrillic;
	}

	@observable private _items: string[] = null;

	@TypeSystem.propertyArrayDecorator('string')
	public set items(val: string[]) {
		this._items = val;
	}


	public get items(): string[] {
		return this._items;
	}

	@observable private _itemsElementName: ForeignEntityIdentifierChoiceType[] = null;

	@TypeSystem.propertyArrayDecorator(ForeignEntityIdentifierChoiceType ? ForeignEntityIdentifierChoiceType : moduleContext.moduleName + '.ForeignEntityIdentifierChoiceType')
	public set itemsElementName(val: ForeignEntityIdentifierChoiceType[]) {
		this._itemsElementName = val;
	}


	public get itemsElementName(): ForeignEntityIdentifierChoiceType[] {
		return this._itemsElementName;
	}

	@observable private _foreignEntityRegister: string = null;

	@TypeSystem.propertyDecorator('string')
	public set foreignEntityRegister(val: string) {
		this._foreignEntityRegister = val;
	}


	public get foreignEntityRegister(): string {
		return this._foreignEntityRegister;
	}

	@observable private _foreignEntityIdentifier: string = null;

	@TypeSystem.propertyDecorator('string')
	public set foreignEntityIdentifier(val: string) {
		this._foreignEntityIdentifier = val;
	}


	public get foreignEntityIdentifier(): string {
		return this._foreignEntityIdentifier;
	}

	@observable private _foreignEntityOtherData: string = null;

	@TypeSystem.propertyDecorator('string')
	public set foreignEntityOtherData(val: string) {
		this._foreignEntityOtherData = val;
	}


	public get foreignEntityOtherData(): string {
		return this._foreignEntityOtherData;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('IdentityDocumentBasicData', moduleContext.moduleName)
export class IdentityDocumentBasicData extends BaseDataModel {
	@observable private _identityNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityNumber(val: string) {
		this._identityNumber = val;
	}


	public get identityNumber(): string {
		return this._identityNumber;
	}

	@observable private _identitityIssueDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set identitityIssueDate(val: moment.Moment) {
		this._identitityIssueDate = val;
	}


	public get identitityIssueDate(): moment.Moment {
		return this._identitityIssueDate;
	}

	@observable private _identityIssuer: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityIssuer(val: string) {
		this._identityIssuer = val;
	}


	public get identityIssuer(): string {
		return this._identityIssuer;
	}

	@observable private _identityDocumentType: IdentityDocumentType = null;

	@TypeSystem.propertyDecorator(IdentityDocumentType ? IdentityDocumentType : moduleContext.moduleName + '.IdentityDocumentType')
	public set identityDocumentType(val: IdentityDocumentType) {
		this._identityDocumentType = val;
	}


	public get identityDocumentType(): IdentityDocumentType {
		return this._identityDocumentType;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonAddress', moduleContext.moduleName)
export class PersonAddress extends GRAOAddress {
	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('UnitInfo', moduleContext.moduleName)
export class UnitInfo extends BaseDataModel {
	@observable private _unitID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set unitID(val: number) {
		this._unitID = val;
	}


	public get unitID(): number {
		return this._unitID;
	}

	@observable private _name: string = null;

	@TypeSystem.propertyDecorator('string')
	public set name(val: string) {
		this._name = val;
	}


	public get name(): string {
		return this._name;
	}

	@observable private _parentUnitID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set parentUnitID(val: number) {
		this._parentUnitID = val;
	}


	public get parentUnitID(): number {
		return this._parentUnitID;
	}

	@observable private _hasChildUnits: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set hasChildUnits(val: boolean) {
		this._hasChildUnits = val;
	}


	public get hasChildUnits(): boolean {
		return this._hasChildUnits;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DocumentMustServeToVM', moduleContext.moduleName)
export class DocumentMustServeToVM extends BaseDataModel {
	@observable private _itemInRepublicOfBulgariaDocumentMustServeTo: string = null;

	@TypeSystem.propertyDecorator('string')
	public set itemInRepublicOfBulgariaDocumentMustServeTo(val: string) {
		this._itemInRepublicOfBulgariaDocumentMustServeTo = val;
	}


	public get itemInRepublicOfBulgariaDocumentMustServeTo(): string {
		return this._itemInRepublicOfBulgariaDocumentMustServeTo;
	}

	@observable private _itemAbroadDocumentMustServeTo: string = null;

	@TypeSystem.propertyDecorator('string')
	public set itemAbroadDocumentMustServeTo(val: string) {
		this._itemAbroadDocumentMustServeTo = val;
	}


	public get itemAbroadDocumentMustServeTo(): string {
		return this._itemAbroadDocumentMustServeTo;
	}

	@observable private _itemElementName: ItemChoiceType1 = null;

	@TypeSystem.propertyDecorator(ItemChoiceType1 ? ItemChoiceType1 : moduleContext.moduleName + '.ItemChoiceType1')
	public set itemElementName(val: ItemChoiceType1) {
		this._itemElementName = val;
	}


	public get itemElementName(): ItemChoiceType1 {
		return this._itemElementName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('CitizenshipVM', moduleContext.moduleName)
export class CitizenshipVM extends BaseDataModel {
	@observable private _countryGRAOCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryGRAOCode(val: string) {
		this._countryGRAOCode = val;
	}


	public get countryGRAOCode(): string {
		return this._countryGRAOCode;
	}

	@observable private _countryName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryName(val: string) {
		this._countryName = val;
	}


	public get countryName(): string {
		return this._countryName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonalInformationVM', moduleContext.moduleName)
export class PersonalInformationVM extends BaseDataModel {
	@observable private _personAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set personAddress(val: PersonAddress) {
		this._personAddress = val;
	}


	public get personAddress(): PersonAddress {
		return this._personAddress;
	}

	@observable private _mobilePhone: string = null;

	@TypeSystem.propertyDecorator('string')
	public set mobilePhone(val: string) {
		this._mobilePhone = val;
	}


	public get mobilePhone(): string {
		return this._mobilePhone;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('EntityAddress', moduleContext.moduleName)
export class EntityAddress extends EkatteAddress {
	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('IdentityDocumentForeignCitizenBasicData', moduleContext.moduleName)
export class IdentityDocumentForeignCitizenBasicData extends BaseDataModel {
	@observable private _identityNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityNumber(val: string) {
		this._identityNumber = val;
	}


	public get identityNumber(): string {
		return this._identityNumber;
	}

	@observable private _identitityIssueDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set identitityIssueDate(val: moment.Moment) {
		this._identitityIssueDate = val;
	}


	public get identitityIssueDate(): moment.Moment {
		return this._identitityIssueDate;
	}

	@observable private _identitityIssueDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set identitityIssueDateSpecified(val: boolean) {
		this._identitityIssueDateSpecified = val;
	}


	public get identitityIssueDateSpecified(): boolean {
		return this._identitityIssueDateSpecified;
	}

	@observable private _identitityExpireDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set identitityExpireDate(val: moment.Moment) {
		this._identitityExpireDate = val;
	}


	public get identitityExpireDate(): moment.Moment {
		return this._identitityExpireDate;
	}

	@observable private _identitityExpireDateSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set identitityExpireDateSpecified(val: boolean) {
		this._identitityExpireDateSpecified = val;
	}


	public get identitityExpireDateSpecified(): boolean {
		return this._identitityExpireDateSpecified;
	}

	@observable private _identityIssuer: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityIssuer(val: string) {
		this._identityIssuer = val;
	}


	public get identityIssuer(): string {
		return this._identityIssuer;
	}

	@observable private _identityDocumentType: IdentityDocumentType = null;

	@TypeSystem.propertyDecorator(IdentityDocumentType ? IdentityDocumentType : moduleContext.moduleName + '.IdentityDocumentType')
	public set identityDocumentType(val: IdentityDocumentType) {
		this._identityDocumentType = val;
	}


	public get identityDocumentType(): IdentityDocumentType {
		return this._identityDocumentType;
	}

	@observable private _identityDocumentTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set identityDocumentTypeSpecified(val: boolean) {
		this._identityDocumentTypeSpecified = val;
	}


	public get identityDocumentTypeSpecified(): boolean {
		return this._identityDocumentTypeSpecified;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('GenderDataGender', moduleContext.moduleName)
export class GenderDataGender extends BaseDataModel {
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

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('GenderData', moduleContext.moduleName)
export class GenderData extends BaseDataModel {
	@observable private _genders: GenderDataGender[] = null;

	@TypeSystem.propertyArrayDecorator(GenderDataGender ? GenderDataGender : moduleContext.moduleName + '.GenderDataGender')
	public set genders(val: GenderDataGender[]) {
		this._genders = val;
	}


	public get genders(): GenderDataGender[] {
		return this._genders;
	}

	@observable private _versionDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set versionDate(val: moment.Moment) {
		this._versionDate = val;
	}


	public get versionDate(): moment.Moment {
		return this._versionDate;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PlaceOfBirthAbroad', moduleContext.moduleName)
export class PlaceOfBirthAbroad extends BaseDataModel {
	@observable private _countryGRAOCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryGRAOCode(val: string) {
		this._countryGRAOCode = val;
	}


	public get countryGRAOCode(): string {
		return this._countryGRAOCode;
	}

	@observable private _countryName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryName(val: string) {
		this._countryName = val;
	}


	public get countryName(): string {
		return this._countryName;
	}

	@observable private _settlementAbroadName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set settlementAbroadName(val: string) {
		this._settlementAbroadName = val;
	}


	public get settlementAbroadName(): string {
		return this._settlementAbroadName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ServiceIrregularity', moduleContext.moduleName)
export class ServiceIrregularity extends BaseDataModel {
	@observable private _serviceIrregularityID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set serviceIrregularityID(val: number) {
		this._serviceIrregularityID = val;
	}


	public get serviceIrregularityID(): number {
		return this._serviceIrregularityID;
	}

	@observable private _serviceID: number = null;

	@TypeSystem.propertyDecorator('number')
	public set serviceID(val: number) {
		this._serviceID = val;
	}


	public get serviceID(): number {
		return this._serviceID;
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
@TypeSystem.typeDecorator('RefusalVM', moduleContext.moduleName)
export class RefusalVM extends SigningDocumentFormVMBase<OfficialVM> {
	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
	}

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
	}

	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _refusalHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set refusalHeader(val: string) {
		this._refusalHeader = val;
	}


	public get refusalHeader(): string {
		return this._refusalHeader;
	}

	@observable private _refusalContent: string = null;

	@TypeSystem.propertyDecorator('string')
	public set refusalContent(val: string) {
		this._refusalContent = val;
	}


	public get refusalContent(): string {
		return this._refusalContent;
	}

	@observable private _documentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set documentReceiptOrSigningDate(val: moment.Moment) {
		this._documentReceiptOrSigningDate = val;
	}


	public get documentReceiptOrSigningDate(): moment.Moment {
		return this._documentReceiptOrSigningDate;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ActionsTakenMessageVM', moduleContext.moduleName)
export class ActionsTakenMessageVM extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
	}

	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
	}

	@observable private _actionsTakenMessageHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set actionsTakenMessageHeader(val: string) {
		this._actionsTakenMessageHeader = val;
	}


	public get actionsTakenMessageHeader(): string {
		return this._actionsTakenMessageHeader;
	}

	@observable private _actionsTakenMessageMessage: string = null;

	@TypeSystem.propertyDecorator('string')
	public set actionsTakenMessageMessage(val: string) {
		this._actionsTakenMessageMessage = val;
	}


	public get actionsTakenMessageMessage(): string {
		return this._actionsTakenMessageMessage;
	}

	@observable private _policeDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set policeDepartment(val: PoliceDepartment) {
		this._policeDepartment = val;
	}


	public get policeDepartment(): PoliceDepartment {
		return this._policeDepartment;
	}

	@observable private _documentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set documentReceiptOrSigningDate(val: moment.Moment) {
		this._documentReceiptOrSigningDate = val;
	}


	public get documentReceiptOrSigningDate(): moment.Moment {
		return this._documentReceiptOrSigningDate;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonNamesLatin', moduleContext.moduleName)
export class PersonNamesLatin extends BaseDataModel {
	@observable private _first: string = null;

	@TypeSystem.propertyDecorator('string')
	public set first(val: string) {
		this._first = val;
	}


	public get first(): string {
		return this._first;
	}

	@observable private _middle: string = null;

	@TypeSystem.propertyDecorator('string')
	public set middle(val: string) {
		this._middle = val;
	}


	public get middle(): string {
		return this._middle;
	}

	@observable private _last: string = null;

	@TypeSystem.propertyDecorator('string')
	public set last(val: string) {
		this._last = val;
	}


	public get last(): string {
		return this._last;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonIdentificationData', moduleContext.moduleName)
export class PersonIdentificationData extends BaseDataModel {
	@observable private _names: PersonNames = null;

	@TypeSystem.propertyDecorator(PersonNames ? PersonNames : moduleContext.moduleName + '.PersonNames')
	public set names(val: PersonNames) {
		this._names = val;
	}


	public get names(): PersonNames {
		return this._names;
	}

	@observable private _namesLatin: PersonNamesLatin = null;

	@TypeSystem.propertyDecorator(PersonNamesLatin ? PersonNamesLatin : moduleContext.moduleName + '.PersonNamesLatin')
	public set namesLatin(val: PersonNamesLatin) {
		this._namesLatin = val;
	}


	public get namesLatin(): PersonNamesLatin {
		return this._namesLatin;
	}

	@observable private _identifier: PersonIdentifier = null;

	@TypeSystem.propertyDecorator(PersonIdentifier ? PersonIdentifier : moduleContext.moduleName + '.PersonIdentifier')
	public set identifier(val: PersonIdentifier) {
		this._identifier = val;
	}


	public get identifier(): PersonIdentifier {
		return this._identifier;
	}

	@observable private _birthDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set birthDate(val: moment.Moment) {
		this._birthDate = val;
	}


	public get birthDate(): moment.Moment {
		return this._birthDate;
	}

	@observable private _gender: GenderData = null;

	@TypeSystem.propertyDecorator(GenderData ? GenderData : moduleContext.moduleName + '.GenderData')
	public set gender(val: GenderData) {
		this._gender = val;
	}


	public get gender(): GenderData {
		return this._gender;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('PersonDataExtendedVM', moduleContext.moduleName)
export class PersonDataExtendedVM extends BaseDataModel {
	@observable private _identificationDocuments: IdentityDocumentType[] = null;

	@TypeSystem.propertyArrayDecorator(IdentityDocumentType ? IdentityDocumentType : moduleContext.moduleName + '.IdentityDocumentType')
	public set identificationDocuments(val: IdentityDocumentType[]) {
		this._identificationDocuments = val;
	}


	public get identificationDocuments(): IdentityDocumentType[] {
		return this._identificationDocuments;
	}

	@observable private _personIdentification: PersonIdentificationData = null;

	@TypeSystem.propertyDecorator(PersonIdentificationData ? PersonIdentificationData : moduleContext.moduleName + '.PersonIdentificationData')
	public set personIdentification(val: PersonIdentificationData) {
		this._personIdentification = val;
	}


	public get personIdentification(): PersonIdentificationData {
		return this._personIdentification;
	}

	@observable private _placeOfBirth: string = null;

	@TypeSystem.propertyDecorator('string')
	public set placeOfBirth(val: string) {
		this._placeOfBirth = val;
	}


	public get placeOfBirth(): string {
		return this._placeOfBirth;
	}

	@observable private _eyesColor: BIDEyesColor = null;

	@TypeSystem.propertyDecorator(BIDEyesColor ? BIDEyesColor : moduleContext.moduleName + '.BIDEyesColor')
	public set eyesColor(val: BIDEyesColor) {
		this._eyesColor = val;
	}


	public get eyesColor(): BIDEyesColor {
		return this._eyesColor;
	}

	@observable private _maritalStatus: BIDMaritalStatus = null;

	@TypeSystem.propertyDecorator(BIDMaritalStatus ? BIDMaritalStatus : moduleContext.moduleName + '.BIDMaritalStatus')
	public set maritalStatus(val: BIDMaritalStatus) {
		this._maritalStatus = val;
	}


	public get maritalStatus(): BIDMaritalStatus {
		return this._maritalStatus;
	}

	@observable private _height: number = null;

	@TypeSystem.propertyDecorator('number')
	public set height(val: number) {
		this._height = val;
	}


	public get height(): number {
		return this._height;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('IssuerCountryVM', moduleContext.moduleName)
export class IssuerCountryVM extends BaseDataModel {
	@observable private _countryGRAOCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryGRAOCode(val: string) {
		this._countryGRAOCode = val;
	}


	public get countryGRAOCode(): string {
		return this._countryGRAOCode;
	}

	@observable private _countryName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set countryName(val: string) {
		this._countryName = val;
	}


	public get countryName(): string {
		return this._countryName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('TravelDocumentVM', moduleContext.moduleName)
export class TravelDocumentVM extends BaseDataModel {
	@observable private _identityNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityNumber(val: string) {
		this._identityNumber = val;
	}


	public get identityNumber(): string {
		return this._identityNumber;
	}

	@observable private _identitityIssueDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set identitityIssueDate(val: moment.Moment) {
		this._identitityIssueDate = val;
	}


	public get identitityIssueDate(): moment.Moment {
		return this._identitityIssueDate;
	}

	@observable private _identitityExpireDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set identitityExpireDate(val: moment.Moment) {
		this._identitityExpireDate = val;
	}


	public get identitityExpireDate(): moment.Moment {
		return this._identitityExpireDate;
	}

	@observable private _identityIssuer: IssuerCountryVM = null;

	@TypeSystem.propertyDecorator(IssuerCountryVM ? IssuerCountryVM : moduleContext.moduleName + '.IssuerCountryVM')
	public set identityIssuer(val: IssuerCountryVM) {
		this._identityIssuer = val;
	}


	public get identityIssuer(): IssuerCountryVM {
		return this._identityIssuer;
	}

	@observable private _identityDocumentSeries: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityDocumentSeries(val: string) {
		this._identityDocumentSeries = val;
	}


	public get identityDocumentSeries(): string {
		return this._identityDocumentSeries;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForWithdrawServiceVM', moduleContext.moduleName)
export class ApplicationForWithdrawServiceVM extends ApplicationFormVMBase {
	@observable private _circumstances: ApplicationForWithdrawServiceDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForWithdrawServiceDataVM ? ApplicationForWithdrawServiceDataVM : moduleContext.moduleName + '.ApplicationForWithdrawServiceDataVM')
	public set circumstances(val: ApplicationForWithdrawServiceDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationForWithdrawServiceDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForWithdrawServiceDataVM', moduleContext.moduleName)
export class ApplicationForWithdrawServiceDataVM extends BaseDataModel {
	@observable private _caseFileURI: string = null;

	@TypeSystem.propertyDecorator('string')
	public set caseFileURI(val: string) {
		this._caseFileURI = val;
	}


	public get caseFileURI(): string {
		return this._caseFileURI;
	}

	@observable private _policeDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set policeDepartment(val: PoliceDepartment) {
		this._policeDepartment = val;
	}


	public get policeDepartment(): PoliceDepartment {
		return this._policeDepartment;
	}

	@observable private _issuingDocument: string = null;

	@TypeSystem.propertyDecorator('string')
	public set issuingDocument(val: string) {
		this._issuingDocument = val;
	}


	public get issuingDocument(): string {
		return this._issuingDocument;
	}

	@observable private _refusalReasons: string = null;

	@TypeSystem.propertyDecorator('string')
	public set refusalReasons(val: string) {
		this._refusalReasons = val;
	}


	public get refusalReasons(): string {
		return this._refusalReasons;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('OutstandingConditionsForWithdrawServiceMessageGrounds', moduleContext.moduleName)
export class OutstandingConditionsForWithdrawServiceMessageGrounds extends BaseDataModel {
	@observable private _outstandingConditionsForWithdrawServiceMessageGround: string = null;

	@TypeSystem.propertyDecorator('string')
	public set outstandingConditionsForWithdrawServiceMessageGround(val: string) {
		this._outstandingConditionsForWithdrawServiceMessageGround = val;
	}


	public get outstandingConditionsForWithdrawServiceMessageGround(): string {
		return this._outstandingConditionsForWithdrawServiceMessageGround;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('OutstandingConditionsForWithdrawServiceMessageVM', moduleContext.moduleName)
export class OutstandingConditionsForWithdrawServiceMessageVM extends SigningDocumentFormVMBase<DigitalSignatureContainerVM> {
	@observable private _documentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set documentReceiptOrSigningDate(val: moment.Moment) {
		this._documentReceiptOrSigningDate = val;
	}


	public get documentReceiptOrSigningDate(): moment.Moment {
		return this._documentReceiptOrSigningDate;
	}

	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _electronicServiceApplicant: ElectronicServiceApplicantVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceApplicantVM ? ElectronicServiceApplicantVM : moduleContext.moduleName + '.ElectronicServiceApplicantVM')
	public set electronicServiceApplicant(val: ElectronicServiceApplicantVM) {
		this._electronicServiceApplicant = val;
	}


	public get electronicServiceApplicant(): ElectronicServiceApplicantVM {
		return this._electronicServiceApplicant;
	}

	@observable private _outstandingConditionsForWithdrawServiceMessageHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set outstandingConditionsForWithdrawServiceMessageHeader(val: string) {
		this._outstandingConditionsForWithdrawServiceMessageHeader = val;
	}


	public get outstandingConditionsForWithdrawServiceMessageHeader(): string {
		return this._outstandingConditionsForWithdrawServiceMessageHeader;
	}

	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
	}

	@observable private _grounds: OutstandingConditionsForWithdrawServiceMessageGrounds[] = null;

	@TypeSystem.propertyArrayDecorator(OutstandingConditionsForWithdrawServiceMessageGrounds ? OutstandingConditionsForWithdrawServiceMessageGrounds : moduleContext.moduleName + '.OutstandingConditionsForWithdrawServiceMessageGrounds')
	public set grounds(val: OutstandingConditionsForWithdrawServiceMessageGrounds[]) {
		this._grounds = val;
	}


	public get grounds(): OutstandingConditionsForWithdrawServiceMessageGrounds[] {
		return this._grounds;
	}

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}

import moment from 'moment';
import { observable } from 'mobx';
import { TypeSystem, BaseDataModel } from 'cnsys-core';
import { moduleContext } from '../ModuleContext';
import { ApplicationFormVMBase, PlaceOfBirthAbroad, ForeignCitizenNames, PersonNames, DocumentMustServeToVM, PersonalInformationVM, CitizenshipVM, BIDPersonalIdentificationDocumentReceivePlace, IdentityDocumentType, PersonAddress, IdentityDocumentForeignCitizenBasicData, PoliceDepartment, BIDMaritalStatus, PersonIdentifier, SigningDocumentFormVMBase, OfficialVM,  DocumentURI, AISCaseURIVM, ElectronicServiceProviderBasicDataVM, ElectronicServiceApplicantVM, BIDEyesColor, IdentificationPhotoAndSignatureVM, PersonNamesLatin, PersonIdentificationData, PersonDataExtendedVM, TravelDocumentVM } from 'eau-documents';

export enum AddressForIssuing {
	PermanentAddress = 0,
	CurrentAddress = 1,
} 
TypeSystem.registerEnumInfo(AddressForIssuing, 'AddressForIssuing', moduleContext.moduleName); 

export enum DataContainsInCertificateNomenclature {
	PermanentAddress = 0,
	CyrillicNames = 1,
	LatinNames = 2,
	IssuingDate = 3,
	ExpiryDate = 4,
	CurrentDocStatusAndPublicationDate = 5,
} 
TypeSystem.registerEnumInfo(DataContainsInCertificateNomenclature, 'DataContainsInCertificateNomenclature', moduleContext.moduleName); 

export enum BulgarianIdentityDocumentTypes {
	IDCard = 0,
	Passport = 1,
	DrivingLicense = 2,
} 
TypeSystem.registerEnumInfo(BulgarianIdentityDocumentTypes, 'BulgarianIdentityDocumentTypes', moduleContext.moduleName); 

export enum IssuingBgPersonalDocumentReasonNomenclature {
	Loss = 0,
	Stealing = 1,
	Damage = 2,
	Destruction = 3,
} 
TypeSystem.registerEnumInfo(IssuingBgPersonalDocumentReasonNomenclature, 'IssuingBgPersonalDocumentReasonNomenclature', moduleContext.moduleName); 

export enum IssueDocumentFor {
	IssuedBulgarianIdentityDocumentsInPeriod = 1,
	OtherInformationConnectedWithIssuedBulgarianIdentityDocuments = 2,
} 
TypeSystem.registerEnumInfo(IssueDocumentFor, 'IssueDocumentFor', moduleContext.moduleName); 

@TypeSystem.typeDecorator('ApplicationForIssuingDocumentVM', moduleContext.moduleName)
export class ApplicationForIssuingDocumentVM extends ApplicationFormVMBase {
	@observable private _circumstances: ApplicationForIssuingDocumentDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuingDocumentDataVM ? ApplicationForIssuingDocumentDataVM : moduleContext.moduleName + '.ApplicationForIssuingDocumentDataVM')
	public set circumstances(val: ApplicationForIssuingDocumentDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationForIssuingDocumentDataVM {
		return this._circumstances;
	}

	@observable private _personalInformation: PersonalInformationVM = null;

	@TypeSystem.propertyDecorator(PersonalInformationVM ? PersonalInformationVM : moduleContext.moduleName + '.PersonalInformationVM')
	public set personalInformation(val: PersonalInformationVM) {
		this._personalInformation = val;
	}


	public get personalInformation(): PersonalInformationVM {
		return this._personalInformation;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuingDocumentDataVM', moduleContext.moduleName)
export class ApplicationForIssuingDocumentDataVM extends BaseDataModel {
	@observable private _documentToBeIssuedFor: DocumentToBeIssuedForVM = null;

	@TypeSystem.propertyDecorator(DocumentToBeIssuedForVM ? DocumentToBeIssuedForVM : moduleContext.moduleName + '.DocumentToBeIssuedForVM')
	public set documentToBeIssuedFor(val: DocumentToBeIssuedForVM) {
		this._documentToBeIssuedFor = val;
	}


	public get documentToBeIssuedFor(): DocumentToBeIssuedForVM {
		return this._documentToBeIssuedFor;
	}

	@observable private _addressForIssuing: AddressForIssuing = null;

	@TypeSystem.propertyDecorator(AddressForIssuing ? AddressForIssuing : moduleContext.moduleName + '.AddressForIssuing')
	public set addressForIssuing(val: AddressForIssuing) {
		this._addressForIssuing = val;
	}


	public get addressForIssuing(): AddressForIssuing {
		return this._addressForIssuing;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignIdentityBasicDataVM', moduleContext.moduleName)
export class ForeignIdentityBasicDataVM extends BaseDataModel {
	@observable private _foreignCitizenData: ForeignCitizenDataVM = null;

	@TypeSystem.propertyDecorator(ForeignCitizenDataVM ? ForeignCitizenDataVM : moduleContext.moduleName + '.ForeignCitizenDataVM')
	public set foreignCitizenData(val: ForeignCitizenDataVM) {
		this._foreignCitizenData = val;
	}


	public get foreignCitizenData(): ForeignCitizenDataVM {
		return this._foreignCitizenData;
	}

	@observable private _egn: string = null;

	@TypeSystem.propertyDecorator('string')
	public set egn(val: string) {
		this._egn = val;
	}


	public get egn(): string {
		return this._egn;
	}

	@observable private _lnCh: string = null;

	@TypeSystem.propertyDecorator('string')
	public set lnCh(val: string) {
		this._lnCh = val;
	}


	public get lnCh(): string {
		return this._lnCh;
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

	@observable private _phone: string = null;

	@TypeSystem.propertyDecorator('string')
	public set phone(val: string) {
		this._phone = val;
	}


	public get phone(): string {
		return this._phone;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignerParentDataVM', moduleContext.moduleName)
export class ForeignerParentDataVM extends BaseDataModel {
	@observable private _unknownParent: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set unknownParent(val: boolean) {
		this._unknownParent = val;
	}


	public get unknownParent(): boolean {
		return this._unknownParent;
	}

	@observable private _names: ForeignCitizenNames = null;

	@TypeSystem.propertyDecorator(ForeignCitizenNames ? ForeignCitizenNames : moduleContext.moduleName + '.ForeignCitizenNames')
	public set names(val: ForeignCitizenNames) {
		this._names = val;
	}


	public get names(): ForeignCitizenNames {
		return this._names;
	}

	@observable private _birthDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set birthDate(val: string) {
		this._birthDate = val;
	}


	public get birthDate(): string {
		return this._birthDate;
	}

	@observable private _egn: string = null;

	@TypeSystem.propertyDecorator('string')
	public set egn(val: string) {
		this._egn = val;
	}


	public get egn(): string {
		return this._egn;
	}

	@observable private _lnCh: string = null;

	@TypeSystem.propertyDecorator('string')
	public set lnCh(val: string) {
		this._lnCh = val;
	}


	public get lnCh(): string {
		return this._lnCh;
	}

	@observable private _citizenship: CitizenshipVM = null;

	@TypeSystem.propertyDecorator(CitizenshipVM ? CitizenshipVM : moduleContext.moduleName + '.CitizenshipVM')
	public set citizenship(val: CitizenshipVM) {
		this._citizenship = val;
	}


	public get citizenship(): CitizenshipVM {
		return this._citizenship;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignerSpouseDataVM', moduleContext.moduleName)
export class ForeignerSpouseDataVM extends BaseDataModel {
	@observable private _names: ForeignCitizenNames = null;

	@TypeSystem.propertyDecorator(ForeignCitizenNames ? ForeignCitizenNames : moduleContext.moduleName + '.ForeignCitizenNames')
	public set names(val: ForeignCitizenNames) {
		this._names = val;
	}


	public get names(): ForeignCitizenNames {
		return this._names;
	}

	@observable private _birthDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set birthDate(val: string) {
		this._birthDate = val;
	}


	public get birthDate(): string {
		return this._birthDate;
	}

	@observable private _egn: string = null;

	@TypeSystem.propertyDecorator('string')
	public set egn(val: string) {
		this._egn = val;
	}


	public get egn(): string {
		return this._egn;
	}

	@observable private _lnCh: string = null;

	@TypeSystem.propertyDecorator('string')
	public set lnCh(val: string) {
		this._lnCh = val;
	}


	public get lnCh(): string {
		return this._lnCh;
	}

	@observable private _citizenship: CitizenshipVM = null;

	@TypeSystem.propertyDecorator(CitizenshipVM ? CitizenshipVM : moduleContext.moduleName + '.CitizenshipVM')
	public set citizenship(val: CitizenshipVM) {
		this._citizenship = val;
	}


	public get citizenship(): CitizenshipVM {
		return this._citizenship;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ParentDataVM', moduleContext.moduleName)
export class ParentDataVM extends BaseDataModel {
	@observable private _unknownParent: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set unknownParent(val: boolean) {
		this._unknownParent = val;
	}


	public get unknownParent(): boolean {
		return this._unknownParent;
	}

	@observable private _names: PersonNames = null;

	@TypeSystem.propertyDecorator(PersonNames ? PersonNames : moduleContext.moduleName + '.PersonNames')
	public set names(val: PersonNames) {
		this._names = val;
	}


	public get names(): PersonNames {
		return this._names;
	}

	@observable private _birthDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set birthDate(val: moment.Moment) {
		this._birthDate = val;
	}


	public get birthDate(): moment.Moment {
		return this._birthDate;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('SpouseDataVM', moduleContext.moduleName)
export class SpouseDataVM extends BaseDataModel {
	@observable private _names: PersonNames = null;

	@TypeSystem.propertyDecorator(PersonNames ? PersonNames : moduleContext.moduleName + '.PersonNames')
	public set names(val: PersonNames) {
		this._names = val;
	}


	public get names(): PersonNames {
		return this._names;
	}

	@observable private _pid: string = null;

	@TypeSystem.propertyDecorator('string')
	public set pid(val: string) {
		this._pid = val;
	}


	public get pid(): string {
		return this._pid;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaVM', moduleContext.moduleName)
export class ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaVM extends ApplicationFormVMBase {
	@observable private _identificationPhotoAndSignature: IdentificationPhotoAndSignatureVM = null;

	@TypeSystem.propertyDecorator(IdentificationPhotoAndSignatureVM ? IdentificationPhotoAndSignatureVM : moduleContext.moduleName + '.IdentificationPhotoAndSignatureVM')
	public set identificationPhotoAndSignature(val: IdentificationPhotoAndSignatureVM) {
		this._identificationPhotoAndSignature = val;
	}


	public get identificationPhotoAndSignature(): IdentificationPhotoAndSignatureVM {
		return this._identificationPhotoAndSignature;
	}

	@observable private _circumstances: ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM ? ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM : moduleContext.moduleName + '.ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM')
	public set circumstances(val: ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM', moduleContext.moduleName)
export class ApplicationForIssuanceOfIdentityDocumentsAndRPOFInTheRepublicOfBulgariaDataVM extends BaseDataModel {
	@observable private _identityDocumentsType: IdentityDocumentType[] = null;

	@TypeSystem.propertyArrayDecorator(IdentityDocumentType ? IdentityDocumentType : moduleContext.moduleName + '.IdentityDocumentType')
	public set identityDocumentsType(val: IdentityDocumentType[]) {
		this._identityDocumentsType = val;
	}


	public get identityDocumentsType(): IdentityDocumentType[] {
		return this._identityDocumentsType;
	}

	@observable private _foreignIdentityBasicData: ForeignIdentityBasicDataVM = null;

	@TypeSystem.propertyDecorator(ForeignIdentityBasicDataVM ? ForeignIdentityBasicDataVM : moduleContext.moduleName + '.ForeignIdentityBasicDataVM')
	public set foreignIdentityBasicData(val: ForeignIdentityBasicDataVM) {
		this._foreignIdentityBasicData = val;
	}


	public get foreignIdentityBasicData(): ForeignIdentityBasicDataVM {
		return this._foreignIdentityBasicData;
	}

	@observable private _permanentAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set permanentAddress(val: PersonAddress) {
		this._permanentAddress = val;
	}


	public get permanentAddress(): PersonAddress {
		return this._permanentAddress;
	}

	@observable private _presentAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set presentAddress(val: PersonAddress) {
		this._presentAddress = val;
	}


	public get presentAddress(): PersonAddress {
		return this._presentAddress;
	}

	@observable private _previousIdentityDocument: IdentityDocumentForeignCitizenBasicData = null;

	@TypeSystem.propertyDecorator(IdentityDocumentForeignCitizenBasicData ? IdentityDocumentForeignCitizenBasicData : moduleContext.moduleName + '.IdentityDocumentForeignCitizenBasicData')
	public set previousIdentityDocument(val: IdentityDocumentForeignCitizenBasicData) {
		this._previousIdentityDocument = val;
	}


	public get previousIdentityDocument(): IdentityDocumentForeignCitizenBasicData {
		return this._previousIdentityDocument;
	}

	@observable private _travelDocument: TravelDocumentVM = null;

	@TypeSystem.propertyDecorator(TravelDocumentVM ? TravelDocumentVM : moduleContext.moduleName + '.TravelDocumentVM')
	public set travelDocument(val: TravelDocumentVM) {
		this._travelDocument = val;
	}


	public get travelDocument(): TravelDocumentVM {
		return this._travelDocument;
	}

	@observable private _newTravelDocument: TravelDocumentVM = null;

	@TypeSystem.propertyDecorator(TravelDocumentVM ? TravelDocumentVM : moduleContext.moduleName + '.TravelDocumentVM')
	public set newTravelDocument(val: TravelDocumentVM) {
		this._newTravelDocument = val;
	}


	public get newTravelDocument(): TravelDocumentVM {
		return this._newTravelDocument;
	}

	@observable private _abroadAddress: string = null;

	@TypeSystem.propertyDecorator('string')
	public set abroadAddress(val: string) {
		this._abroadAddress = val;
	}


	public get abroadAddress(): string {
		return this._abroadAddress;
	}

	@observable private _motherData: ForeignerParentDataVM = null;

	@TypeSystem.propertyDecorator(ForeignerParentDataVM ? ForeignerParentDataVM : moduleContext.moduleName + '.ForeignerParentDataVM')
	public set motherData(val: ForeignerParentDataVM) {
		this._motherData = val;
	}


	public get motherData(): ForeignerParentDataVM {
		return this._motherData;
	}

	@observable private _fatherData: ForeignerParentDataVM = null;

	@TypeSystem.propertyDecorator(ForeignerParentDataVM ? ForeignerParentDataVM : moduleContext.moduleName + '.ForeignerParentDataVM')
	public set fatherData(val: ForeignerParentDataVM) {
		this._fatherData = val;
	}


	public get fatherData(): ForeignerParentDataVM {
		return this._fatherData;
	}

	@observable private _spouseData: ForeignerSpouseDataVM = null;

	@TypeSystem.propertyDecorator(ForeignerSpouseDataVM ? ForeignerSpouseDataVM : moduleContext.moduleName + '.ForeignerSpouseDataVM')
	public set spouseData(val: ForeignerSpouseDataVM) {
		this._spouseData = val;
	}


	public get spouseData(): ForeignerSpouseDataVM {
		return this._spouseData;
	}

	@observable private _policeDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set policeDepartment(val: PoliceDepartment) {
		this._policeDepartment = val;
	}


	public get policeDepartment(): PoliceDepartment {
		return this._policeDepartment;
	}

	@observable private _hasDocumentForDisabilities: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set hasDocumentForDisabilities(val: boolean) {
		this._hasDocumentForDisabilities = val;
	}


	public get hasDocumentForDisabilities(): boolean {
		return this._hasDocumentForDisabilities;
	}

	@observable private _serviceCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serviceCode(val: string) {
		this._serviceCode = val;
	}


	public get serviceCode(): string {
		return this._serviceCode;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM', moduleContext.moduleName)
export class ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensVM extends ApplicationFormVMBase {
	@observable private _identificationPhotoAndSignature: IdentificationPhotoAndSignatureVM = null;

	@TypeSystem.propertyDecorator(IdentificationPhotoAndSignatureVM ? IdentificationPhotoAndSignatureVM : moduleContext.moduleName + '.IdentificationPhotoAndSignatureVM')
	public set identificationPhotoAndSignature(val: IdentificationPhotoAndSignatureVM) {
		this._identificationPhotoAndSignature = val;
	}


	public get identificationPhotoAndSignature(): IdentificationPhotoAndSignatureVM {
		return this._identificationPhotoAndSignature;
	}

	@observable private _circumstances: ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM ? ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM : moduleContext.moduleName + '.ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM')
	public set circumstances(val: ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM', moduleContext.moduleName)
export class ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensDataVM extends BaseDataModel {
	@observable private _identificationDocuments: IdentityDocumentType[] = null;

	@TypeSystem.propertyArrayDecorator(IdentityDocumentType ? IdentityDocumentType : moduleContext.moduleName + '.IdentityDocumentType')
	public set identificationDocuments(val: IdentityDocumentType[]) {
		this._identificationDocuments = val;
	}


	public get identificationDocuments(): IdentityDocumentType[] {
		return this._identificationDocuments;
	}

	@observable private _person: PersonDataExtendedVM = null;

	@TypeSystem.propertyDecorator(PersonDataExtendedVM ? PersonDataExtendedVM : moduleContext.moduleName + '.PersonDataExtendedVM')
	public set person(val: PersonDataExtendedVM) {
		this._person = val;
	}


	public get person(): PersonDataExtendedVM {
		return this._person;
	}

	@observable private _permanentAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set permanentAddress(val: PersonAddress) {
		this._permanentAddress = val;
	}


	public get permanentAddress(): PersonAddress {
		return this._permanentAddress;
	}

	@observable private _policeDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set policeDepartment(val: PoliceDepartment) {
		this._policeDepartment = val;
	}


	public get policeDepartment(): PoliceDepartment {
		return this._policeDepartment;
	}

	@observable private _receivePlace: BIDPersonalIdentificationDocumentReceivePlace = null;

	@TypeSystem.propertyDecorator(BIDPersonalIdentificationDocumentReceivePlace ? BIDPersonalIdentificationDocumentReceivePlace : moduleContext.moduleName + '.BIDPersonalIdentificationDocumentReceivePlace')
	public set receivePlace(val: BIDPersonalIdentificationDocumentReceivePlace) {
		this._receivePlace = val;
	}


	public get receivePlace(): BIDPersonalIdentificationDocumentReceivePlace {
		return this._receivePlace;
	}

	@observable private _motherData: ParentDataVM = null;

	@TypeSystem.propertyDecorator(ParentDataVM ? ParentDataVM : moduleContext.moduleName + '.ParentDataVM')
	public set motherData(val: ParentDataVM) {
		this._motherData = val;
	}


	public get motherData(): ParentDataVM {
		return this._motherData;
	}

	@observable private _fatherData: ParentDataVM = null;

	@TypeSystem.propertyDecorator(ParentDataVM ? ParentDataVM : moduleContext.moduleName + '.ParentDataVM')
	public set fatherData(val: ParentDataVM) {
		this._fatherData = val;
	}


	public get fatherData(): ParentDataVM {
		return this._fatherData;
	}

	@observable private _spouseData: SpouseDataVM = null;

	@TypeSystem.propertyDecorator(SpouseDataVM ? SpouseDataVM : moduleContext.moduleName + '.SpouseDataVM')
	public set spouseData(val: SpouseDataVM) {
		this._spouseData = val;
	}


	public get spouseData(): SpouseDataVM {
		return this._spouseData;
	}

	@observable private _abroadAddress: string = null;

	@TypeSystem.propertyDecorator('string')
	public set abroadAddress(val: string) {
		this._abroadAddress = val;
	}


	public get abroadAddress(): string {
		return this._abroadAddress;
	}

	@observable private _hasDocumentForDisabilities: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set hasDocumentForDisabilities(val: boolean) {
		this._hasDocumentForDisabilities = val;
	}


	public get hasDocumentForDisabilities(): boolean {
		return this._hasDocumentForDisabilities;
	}

	@observable private _phone: string = null;

	@TypeSystem.propertyDecorator('string')
	public set phone(val: string) {
		this._phone = val;
	}


	public get phone(): string {
		return this._phone;
	}

	@observable private _serviceCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serviceCode(val: string) {
		this._serviceCode = val;
	}


	public get serviceCode(): string {
		return this._serviceCode;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM', moduleContext.moduleName)
export class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensVM extends ApplicationFormVMBase {
	@observable private _circumstances: ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM ? ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM : moduleContext.moduleName + '.ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM')
	public set circumstances(val: ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM {
		return this._circumstances;
	}

	@observable private _identificationPhotoAndSignature: IdentificationPhotoAndSignatureVM = null;

	@TypeSystem.propertyDecorator(IdentificationPhotoAndSignatureVM ? IdentificationPhotoAndSignatureVM : moduleContext.moduleName + '.IdentificationPhotoAndSignatureVM')
	public set identificationPhotoAndSignature(val: IdentificationPhotoAndSignatureVM) {
		this._identificationPhotoAndSignature = val;
	}


	public get identificationPhotoAndSignature(): IdentificationPhotoAndSignatureVM {
		return this._identificationPhotoAndSignature;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM', moduleContext.moduleName)
export class ApplicationForIssuingResidencePermitAndDrivingLicenseForEuropeanCitizensDataVM extends BaseDataModel {
	@observable private _identityDocumentsType: IdentityDocumentType[] = null;

	@TypeSystem.propertyArrayDecorator(IdentityDocumentType ? IdentityDocumentType : moduleContext.moduleName + '.IdentityDocumentType')
	public set identityDocumentsType(val: IdentityDocumentType[]) {
		this._identityDocumentsType = val;
	}


	public get identityDocumentsType(): IdentityDocumentType[] {
		return this._identityDocumentsType;
	}

	@observable private _foreignIdentityBasicData: ForeignIdentityBasicDataVM = null;

	@TypeSystem.propertyDecorator(ForeignIdentityBasicDataVM ? ForeignIdentityBasicDataVM : moduleContext.moduleName + '.ForeignIdentityBasicDataVM')
	public set foreignIdentityBasicData(val: ForeignIdentityBasicDataVM) {
		this._foreignIdentityBasicData = val;
	}


	public get foreignIdentityBasicData(): ForeignIdentityBasicDataVM {
		return this._foreignIdentityBasicData;
	}

	@observable private _address: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set address(val: PersonAddress) {
		this._address = val;
	}


	public get address(): PersonAddress {
		return this._address;
	}

	@observable private _policeDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set policeDepartment(val: PoliceDepartment) {
		this._policeDepartment = val;
	}


	public get policeDepartment(): PoliceDepartment {
		return this._policeDepartment;
	}

	@observable private _travelDocument: TravelDocumentVM = null;

	@TypeSystem.propertyDecorator(TravelDocumentVM ? TravelDocumentVM : moduleContext.moduleName + '.TravelDocumentVM')
	public set travelDocument(val: TravelDocumentVM) {
		this._travelDocument = val;
	}


	public get travelDocument(): TravelDocumentVM {
		return this._travelDocument;
	}

	@observable private _newTravelDocument: TravelDocumentVM = null;

	@TypeSystem.propertyDecorator(TravelDocumentVM ? TravelDocumentVM : moduleContext.moduleName + '.TravelDocumentVM')
	public set newTravelDocument(val: TravelDocumentVM) {
		this._newTravelDocument = val;
	}


	public get newTravelDocument(): TravelDocumentVM {
		return this._newTravelDocument;
	}

	@observable private _otherCitizenship: CitizenshipVM = null;

	@TypeSystem.propertyDecorator(CitizenshipVM ? CitizenshipVM : moduleContext.moduleName + '.CitizenshipVM')
	public set otherCitizenship(val: CitizenshipVM) {
		this._otherCitizenship = val;
	}


	public get otherCitizenship(): CitizenshipVM {
		return this._otherCitizenship;
	}

	@observable private _hasDocumentForDisabilities: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set hasDocumentForDisabilities(val: boolean) {
		this._hasDocumentForDisabilities = val;
	}


	public get hasDocumentForDisabilities(): boolean {
		return this._hasDocumentForDisabilities;
	}

	@observable private _serviceCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set serviceCode(val: string) {
		this._serviceCode = val;
	}


	public get serviceCode(): string {
		return this._serviceCode;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportVM', moduleContext.moduleName)
export class RequestForApplyingForServiceForIssuingOfBulgarianCitizensIdentityCardOrPassportVM extends ApplicationFormVMBase {
	@observable private _circumstances: RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM = null;

	@TypeSystem.propertyDecorator(RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM ? RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM : moduleContext.moduleName + '.RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM')
	public set circumstances(val: RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM', moduleContext.moduleName)
export class RequestForApplyingForServiceForIssuingOfBCICardOrPassportDataVM extends BaseDataModel {
	@observable private _identificationDocuments: IdentityDocumentType[] = null;

	@TypeSystem.propertyArrayDecorator(IdentityDocumentType ? IdentityDocumentType : moduleContext.moduleName + '.IdentityDocumentType')
	public set identificationDocuments(val: IdentityDocumentType[]) {
		this._identificationDocuments = val;
	}


	public get identificationDocuments(): IdentityDocumentType[] {
		return this._identificationDocuments;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DocumentToBeIssuedForVM', moduleContext.moduleName)
export class DocumentToBeIssuedForVM extends BaseDataModel {
	@observable private _chooseIssuingDocument: IssueDocumentFor = null;

	@TypeSystem.propertyDecorator(IssueDocumentFor ? IssueDocumentFor : moduleContext.moduleName + '.IssueDocumentFor')
	public set chooseIssuingDocument(val: IssueDocumentFor) {
		this._chooseIssuingDocument = val;
	}


	public get chooseIssuingDocument(): IssueDocumentFor {
		return this._chooseIssuingDocument;
	}

	@observable private _issuedBulgarianIdentityDocumentsInPeriod: IssuedBulgarianIdentityDocumentsInPeriodVM = null;

	@TypeSystem.propertyDecorator(IssuedBulgarianIdentityDocumentsInPeriodVM ? IssuedBulgarianIdentityDocumentsInPeriodVM : moduleContext.moduleName + '.IssuedBulgarianIdentityDocumentsInPeriodVM')
	public set issuedBulgarianIdentityDocumentsInPeriod(val: IssuedBulgarianIdentityDocumentsInPeriodVM) {
		this._issuedBulgarianIdentityDocumentsInPeriod = val;
	}


	public get issuedBulgarianIdentityDocumentsInPeriod(): IssuedBulgarianIdentityDocumentsInPeriodVM {
		return this._issuedBulgarianIdentityDocumentsInPeriod;
	}

	@observable private _otherInformationConnectedWithIssuedBulgarianIdentityDocuments: OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM = null;

	@TypeSystem.propertyDecorator(OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM ? OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM : moduleContext.moduleName + '.OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM')
	public set otherInformationConnectedWithIssuedBulgarianIdentityDocuments(val: OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM) {
		this._otherInformationConnectedWithIssuedBulgarianIdentityDocuments = val;
	}


	public get otherInformationConnectedWithIssuedBulgarianIdentityDocuments(): OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM {
		return this._otherInformationConnectedWithIssuedBulgarianIdentityDocuments;
	}

	@observable private _documentMustServeTo: DocumentMustServeToVM = null;

	@TypeSystem.propertyDecorator(DocumentMustServeToVM ? DocumentMustServeToVM : moduleContext.moduleName + '.DocumentMustServeToVM')
	public set documentMustServeTo(val: DocumentMustServeToVM) {
		this._documentMustServeTo = val;
	}


	public get documentMustServeTo(): DocumentMustServeToVM {
		return this._documentMustServeTo;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('IssuedBulgarianIdentityDocumentsInPeriodVM', moduleContext.moduleName)
export class IssuedBulgarianIdentityDocumentsInPeriodVM extends BaseDataModel {
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

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM', moduleContext.moduleName)
export class OtherInformationConnectedWithIssuedBulgarianIdentityDocumentsVM extends BaseDataModel {
	@observable private _nessesaryInformation: string = null;

	@TypeSystem.propertyDecorator('string')
	public set nessesaryInformation(val: string) {
		this._nessesaryInformation = val;
	}


	public get nessesaryInformation(): string {
		return this._nessesaryInformation;
	}

	@observable private _documentNumbers: DocumentNumber[] = null;

	@TypeSystem.propertyArrayDecorator(DocumentNumber ? DocumentNumber : moduleContext.moduleName + '.DocumentNumber')
	public set documentNumbers(val: DocumentNumber[]) {
		this._documentNumbers = val;
	}


	public get documentNumbers(): DocumentNumber[] {
		return this._documentNumbers;
	}

	@observable private _documentsInfos: IssuedBulgarianIdentityDocumentInfo[] = null;

	@TypeSystem.propertyArrayDecorator(IssuedBulgarianIdentityDocumentInfo ? IssuedBulgarianIdentityDocumentInfo : moduleContext.moduleName + '.IssuedBulgarianIdentityDocumentInfo')
	public set documentsInfos(val: IssuedBulgarianIdentityDocumentInfo[]) {
		this._documentsInfos = val;
	}


	public get documentsInfos(): IssuedBulgarianIdentityDocumentInfo[] {
		return this._documentsInfos;
	}

	@observable private _includsDataInCertificate: DataContainsInCertificateNomenclature[] = null;

	@TypeSystem.propertyArrayDecorator(DataContainsInCertificateNomenclature ? DataContainsInCertificateNomenclature : moduleContext.moduleName + '.DataContainsInCertificateNomenclature')
	public set includsDataInCertificate(val: DataContainsInCertificateNomenclature[]) {
		this._includsDataInCertificate = val;
	}


	public get includsDataInCertificate(): DataContainsInCertificateNomenclature[] {
		return this._includsDataInCertificate;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ForeignCitizenDataVM', moduleContext.moduleName)
export class ForeignCitizenDataVM extends BaseDataModel {
	@observable private _foreignCitizenNames: ForeignCitizenNames = null;

	@TypeSystem.propertyDecorator(ForeignCitizenNames ? ForeignCitizenNames : moduleContext.moduleName + '.ForeignCitizenNames')
	public set foreignCitizenNames(val: ForeignCitizenNames) {
		this._foreignCitizenNames = val;
	}


	public get foreignCitizenNames(): ForeignCitizenNames {
		return this._foreignCitizenNames;
	}

	@observable private _genderName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set genderName(val: string) {
		this._genderName = val;
	}


	public get genderName(): string {
		return this._genderName;
	}

	@observable private _genderCode: string = null;

	@TypeSystem.propertyDecorator('string')
	public set genderCode(val: string) {
		this._genderCode = val;
	}


	public get genderCode(): string {
		return this._genderCode;
	}

	@observable private _birthDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set birthDate(val: string) {
		this._birthDate = val;
	}


	public get birthDate(): string {
		return this._birthDate;
	}

	@observable private _placeOfBirthAbroad: PlaceOfBirthAbroad = null;

	@TypeSystem.propertyDecorator(PlaceOfBirthAbroad ? PlaceOfBirthAbroad : moduleContext.moduleName + '.PlaceOfBirthAbroad')
	public set placeOfBirthAbroad(val: PlaceOfBirthAbroad) {
		this._placeOfBirthAbroad = val;
	}


	public get placeOfBirthAbroad(): PlaceOfBirthAbroad {
		return this._placeOfBirthAbroad;
	}

	@observable private _citizenship: CitizenshipVM = null;

	@TypeSystem.propertyDecorator(CitizenshipVM ? CitizenshipVM : moduleContext.moduleName + '.CitizenshipVM')
	public set citizenship(val: CitizenshipVM) {
		this._citizenship = val;
	}


	public get citizenship(): CitizenshipVM {
		return this._citizenship;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM', moduleContext.moduleName)
export class CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDVM extends SigningDocumentFormVMBase<OfficialVM> {
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

	@observable private _certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeader(val: string) {
		this._certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeader = val;
	}


	public get certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeader(): string {
		return this._certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDHeader;
	}

	@observable private _certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDData: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDData(val: string) {
		this._certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDData = val;
	}


	public get certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDData(): string {
		return this._certificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDData;
	}

	@observable private _documentMustServeTo: DocumentMustServeToVM = null;

	@TypeSystem.propertyDecorator(DocumentMustServeToVM ? DocumentMustServeToVM : moduleContext.moduleName + '.DocumentMustServeToVM')
	public set documentMustServeTo(val: DocumentMustServeToVM) {
		this._documentMustServeTo = val;
	}


	public get documentMustServeTo(): DocumentMustServeToVM {
		return this._documentMustServeTo;
	}

	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
	}

	@observable private _reportDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set reportDate(val: moment.Moment) {
		this._reportDate = val;
	}


	public get reportDate(): moment.Moment {
		return this._reportDate;
	}

	@observable private _identityDocuments: IdentityDocumentData[] = null;

	@TypeSystem.propertyArrayDecorator(IdentityDocumentData ? IdentityDocumentData : moduleContext.moduleName + '.IdentityDocumentData')
	public set identityDocuments(val: IdentityDocumentData[]) {
		this._identityDocuments = val;
	}


	public get identityDocuments(): IdentityDocumentData[] {
		return this._identityDocuments;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('IdentityDocumentData', moduleContext.moduleName)
export class IdentityDocumentData extends BaseDataModel {
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

	@observable private _identityDocumentStatus: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identityDocumentStatus(val: string) {
		this._identityDocumentStatus = val;
	}


	public get identityDocumentStatus(): string {
		return this._identityDocumentStatus;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DocumentNumber', moduleContext.moduleName)
export class DocumentNumber extends BaseDataModel {
	@observable private _number: string = null;

	@TypeSystem.propertyDecorator('string')
	public set number(val: string) {
		this._number = val;
	}


	public get number(): string {
		return this._number;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('IssuedBulgarianIdentityDocumentInfo', moduleContext.moduleName)
export class IssuedBulgarianIdentityDocumentInfo extends BaseDataModel {
	@observable private _issuingYear: number = null;

	@TypeSystem.propertyDecorator('number')
	public set issuingYear(val: number) {
		this._issuingYear = val;
	}


	public get issuingYear(): number {
		return this._issuingYear;
	}

	@observable private _issuingYearSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set issuingYearSpecified(val: boolean) {
		this._issuingYearSpecified = val;
	}


	public get issuingYearSpecified(): boolean {
		return this._issuingYearSpecified;
	}

	@observable private _docType: BulgarianIdentityDocumentTypes = null;

	@TypeSystem.propertyDecorator(BulgarianIdentityDocumentTypes ? BulgarianIdentityDocumentTypes : moduleContext.moduleName + '.BulgarianIdentityDocumentTypes')
	public set docType(val: BulgarianIdentityDocumentTypes) {
		this._docType = val;
	}


	public get docType(): BulgarianIdentityDocumentTypes {
		return this._docType;
	}

	@observable private _docTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set docTypeSpecified(val: boolean) {
		this._docTypeSpecified = val;
	}


	public get docTypeSpecified(): boolean {
		return this._docTypeSpecified;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DeclarationUndurArticle17VM', moduleContext.moduleName)
export class DeclarationUndurArticle17VM extends ApplicationFormVMBase {
	@observable private _circumstances: DeclarationUndurArticle17Data = null;

	@TypeSystem.propertyDecorator(DeclarationUndurArticle17Data ? DeclarationUndurArticle17Data : moduleContext.moduleName + '.DeclarationUndurArticle17Data')
	public set circumstances(val: DeclarationUndurArticle17Data) {
		this._circumstances = val;
	}


	public get circumstances(): DeclarationUndurArticle17Data {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('DeclarationUndurArticle17Data', moduleContext.moduleName)
export class DeclarationUndurArticle17Data extends BaseDataModel {
	@observable private _docType: BulgarianIdentityDocumentTypes = null;

	@TypeSystem.propertyDecorator(BulgarianIdentityDocumentTypes ? BulgarianIdentityDocumentTypes : moduleContext.moduleName + '.BulgarianIdentityDocumentTypes')
	public set docType(val: BulgarianIdentityDocumentTypes) {
		this._docType = val;
	}


	public get docType(): BulgarianIdentityDocumentTypes {
		return this._docType;
	}

	@observable private _docTypeSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set docTypeSpecified(val: boolean) {
		this._docTypeSpecified = val;
	}


	public get docTypeSpecified(): boolean {
		return this._docTypeSpecified;
	}

	@observable private _permanentAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set permanentAddress(val: PersonAddress) {
		this._permanentAddress = val;
	}


	public get permanentAddress(): PersonAddress {
		return this._permanentAddress;
	}

	@observable private _presentAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set presentAddress(val: PersonAddress) {
		this._presentAddress = val;
	}


	public get presentAddress(): PersonAddress {
		return this._presentAddress;
	}

	@observable private _reasonData: IssuingBgPersonalDocumentReasonData = null;

	@TypeSystem.propertyDecorator(IssuingBgPersonalDocumentReasonData ? IssuingBgPersonalDocumentReasonData : moduleContext.moduleName + '.IssuingBgPersonalDocumentReasonData')
	public set reasonData(val: IssuingBgPersonalDocumentReasonData) {
		this._reasonData = val;
	}


	public get reasonData(): IssuingBgPersonalDocumentReasonData {
		return this._reasonData;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('IssuingBgPersonalDocumentReasonData', moduleContext.moduleName)
export class IssuingBgPersonalDocumentReasonData extends BaseDataModel {
	@observable private _reason: IssuingBgPersonalDocumentReasonNomenclature = null;

	@TypeSystem.propertyDecorator(IssuingBgPersonalDocumentReasonNomenclature ? IssuingBgPersonalDocumentReasonNomenclature : moduleContext.moduleName + '.IssuingBgPersonalDocumentReasonNomenclature')
	public set reason(val: IssuingBgPersonalDocumentReasonNomenclature) {
		this._reason = val;
	}


	public get reason(): IssuingBgPersonalDocumentReasonNomenclature {
		return this._reason;
	}

	@observable private _reasonSpecified: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set reasonSpecified(val: boolean) {
		this._reasonSpecified = val;
	}


	public get reasonSpecified(): boolean {
		return this._reasonSpecified;
	}

	@observable private _factsAndCircumstances: string = null;

	@TypeSystem.propertyDecorator('string')
	public set factsAndCircumstances(val: string) {
		this._factsAndCircumstances = val;
	}


	public get factsAndCircumstances(): string {
		return this._factsAndCircumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('InvitationToDrawUpAUANVM', moduleContext.moduleName)
export class InvitationToDrawUpAUANVM extends SigningDocumentFormVMBase<OfficialVM> {
	@observable private _electronicServiceProviderBasicData: ElectronicServiceProviderBasicDataVM = null;

	@TypeSystem.propertyDecorator(ElectronicServiceProviderBasicDataVM ? ElectronicServiceProviderBasicDataVM : moduleContext.moduleName + '.ElectronicServiceProviderBasicDataVM')
	public set electronicServiceProviderBasicData(val: ElectronicServiceProviderBasicDataVM) {
		this._electronicServiceProviderBasicData = val;
	}


	public get electronicServiceProviderBasicData(): ElectronicServiceProviderBasicDataVM {
		return this._electronicServiceProviderBasicData;
	}

	@observable private _aisCaseURI: AISCaseURIVM = null;

	@TypeSystem.propertyDecorator(AISCaseURIVM ? AISCaseURIVM : moduleContext.moduleName + '.AISCaseURIVM')
	public set aisCaseURI(val: AISCaseURIVM) {
		this._aisCaseURI = val;
	}


	public get aisCaseURI(): AISCaseURIVM {
		return this._aisCaseURI;
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

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
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

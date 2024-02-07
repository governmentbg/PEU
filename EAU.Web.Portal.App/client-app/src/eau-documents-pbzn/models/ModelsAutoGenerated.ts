import moment from 'moment';
import { observable } from 'mobx';
import { TypeSystem, BaseDataModel } from 'cnsys-core';
import { moduleContext } from '../ModuleContext';
import {ApplicationFormVMBase, PoliceDepartment, EntityAddress, PersonAddress, DocumentMustServeToVM,  ElectronicServiceApplicantVM, OfficialVM, DocumentURI, AISCaseURIVM, SigningDocumentFormVMBase, ElectronicServiceProviderBasicDataVM } from 'eau-documents';

export enum CertificateType {
	Issuing = 0,
	Renewing = 1,
} 
TypeSystem.registerEnumInfo(CertificateType, 'CertificateType', moduleContext.moduleName); 

export enum EntityOrPerson {
	Entity = 0,
	Person = 1,
} 
TypeSystem.registerEnumInfo(EntityOrPerson, 'EntityOrPerson', moduleContext.moduleName); 

@TypeSystem.typeDecorator('ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM', moduleContext.moduleName)
export class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesVM extends ApplicationFormVMBase {
	@observable private _circumstances: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM ? ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM : moduleContext.moduleName + '.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM')
	public set circumstances(val: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM', moduleContext.moduleName)
export class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesDataVM extends BaseDataModel {
	@observable private _sunauServiceUri: string = null;

	@TypeSystem.propertyDecorator('string')
	public set sunauServiceUri(val: string) {
		this._sunauServiceUri = val;
	}


	public get sunauServiceUri(): string {
		return this._sunauServiceUri;
	}

	@observable private _entityOrPerson: EntityOrPerson = null;

	@TypeSystem.propertyDecorator(EntityOrPerson ? EntityOrPerson : moduleContext.moduleName + '.EntityOrPerson')
	public set entityOrPerson(val: EntityOrPerson) {
		this._entityOrPerson = val;
	}


	public get entityOrPerson(): EntityOrPerson {
		return this._entityOrPerson;
	}

	@observable private _applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM ? ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM : moduleContext.moduleName + '.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM')
	public set applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData(val: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM) {
		this._applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData = val;
	}


	public get applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData(): ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM {
		return this._applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData;
	}

	@observable private _applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM ? ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM : moduleContext.moduleName + '.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM')
	public set applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData(val: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM) {
		this._applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData = val;
	}


	public get applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData(): ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM {
		return this._applicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonData;
	}

	@observable private _workPhone: string = null;

	@TypeSystem.propertyDecorator('string')
	public set workPhone(val: string) {
		this._workPhone = val;
	}


	public get workPhone(): string {
		return this._workPhone;
	}

	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _personDataPermanentAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set personDataPermanentAddress(val: PersonAddress) {
		this._personDataPermanentAddress = val;
	}


	public get personDataPermanentAddress(): PersonAddress {
		return this._personDataPermanentAddress;
	}

	@observable private _personDataCurrentAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set personDataCurrentAddress(val: PersonAddress) {
		this._personDataCurrentAddress = val;
	}


	public get personDataCurrentAddress(): PersonAddress {
		return this._personDataCurrentAddress;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM', moduleContext.moduleName)
export class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM extends BaseDataModel {
	@observable private _permanentAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set permanentAddress(val: PersonAddress) {
		this._permanentAddress = val;
	}


	public get permanentAddress(): PersonAddress {
		return this._permanentAddress;
	}

	@observable private _currentAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set currentAddress(val: PersonAddress) {
		this._currentAddress = val;
	}


	public get currentAddress(): PersonAddress {
		return this._currentAddress;
	}

	@observable private _certificateNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateNumber(val: string) {
		this._certificateNumber = val;
	}


	public get certificateNumber(): string {
		return this._certificateNumber;
	}

	@observable private _certificateType: CertificateType = null;

	@TypeSystem.propertyDecorator(CertificateType ? CertificateType : moduleContext.moduleName + '.CertificateType')
	public set certificateType(val: CertificateType) {
		this._certificateType = val;
	}


	public get certificateType(): CertificateType {
		return this._certificateType;
	}

	@observable private _diplomaNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set diplomaNumber(val: string) {
		this._diplomaNumber = val;
	}


	public get diplomaNumber(): string {
		return this._diplomaNumber;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM', moduleContext.moduleName)
export class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM extends BaseDataModel {
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

	@observable private _declaredScopeOfCertification: string = null;

	@TypeSystem.propertyDecorator('string')
	public set declaredScopeOfCertification(val: string) {
		this._declaredScopeOfCertification = val;
	}


	public get declaredScopeOfCertification(): string {
		return this._declaredScopeOfCertification;
	}

	@observable private _availableCertifiedPersonnel: CertifiedPersonelVM[] = null;

	@TypeSystem.propertyArrayDecorator(CertifiedPersonelVM ? CertifiedPersonelVM : moduleContext.moduleName + '.CertifiedPersonelVM')
	public set availableCertifiedPersonnel(val: CertifiedPersonelVM[]) {
		this._availableCertifiedPersonnel = val;
	}


	public get availableCertifiedPersonnel(): CertifiedPersonelVM[] {
		return this._availableCertifiedPersonnel;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('CertifiedPersonelVM', moduleContext.moduleName)
export class CertifiedPersonelVM extends BaseDataModel {
	@observable private _personFirstName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set personFirstName(val: string) {
		this._personFirstName = val;
	}


	public get personFirstName(): string {
		return this._personFirstName;
	}

	@observable private _personLastName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set personLastName(val: string) {
		this._personLastName = val;
	}


	public get personLastName(): string {
		return this._personLastName;
	}

	@observable private _certificateNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateNumber(val: string) {
		this._certificateNumber = val;
	}


	public get certificateNumber(): string {
		return this._certificateNumber;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutVM', moduleContext.moduleName)
export class ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutVM extends ApplicationFormVMBase {
	@observable private _circumstances: ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM ? ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM : moduleContext.moduleName + '.ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM')
	public set circumstances(val: ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM', moduleContext.moduleName)
export class ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM extends BaseDataModel {
	@observable private _entityManagementAddress: EntityAddress = null;

	@TypeSystem.propertyDecorator(EntityAddress ? EntityAddress : moduleContext.moduleName + '.EntityAddress')
	public set entityManagementAddress(val: EntityAddress) {
		this._entityManagementAddress = val;
	}


	public get entityManagementAddress(): EntityAddress {
		return this._entityManagementAddress;
	}

	@observable private _corespondingAddress: EntityAddress = null;

	@TypeSystem.propertyDecorator(EntityAddress ? EntityAddress : moduleContext.moduleName + '.EntityAddress')
	public set corespondingAddress(val: EntityAddress) {
		this._corespondingAddress = val;
	}


	public get corespondingAddress(): EntityAddress {
		return this._corespondingAddress;
	}

	@observable private _phoneNumber: string = null;

	@TypeSystem.propertyDecorator('string')
	public set phoneNumber(val: string) {
		this._phoneNumber = val;
	}


	public get phoneNumber(): string {
		return this._phoneNumber;
	}

	@observable private _documentCertifyingTheAccidentOccurredOrOtherInformation: string = null;

	@TypeSystem.propertyDecorator('string')
	public set documentCertifyingTheAccidentOccurredOrOtherInformation(val: string) {
		this._documentCertifyingTheAccidentOccurredOrOtherInformation = val;
	}


	public get documentCertifyingTheAccidentOccurredOrOtherInformation(): string {
		return this._documentCertifyingTheAccidentOccurredOrOtherInformation;
	}

	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _documentMustServeTo: DocumentMustServeToVM = null;

	@TypeSystem.propertyDecorator(DocumentMustServeToVM ? DocumentMustServeToVM : moduleContext.moduleName + '.DocumentMustServeToVM')
	public set documentMustServeTo(val: DocumentMustServeToVM) {
		this._documentMustServeTo = val;
	}


	public get documentMustServeTo(): DocumentMustServeToVM {
		return this._documentMustServeTo;
	}

	@observable private _sunauServiceUri: string = null;

	@TypeSystem.propertyDecorator('string')
	public set sunauServiceUri(val: string) {
		this._sunauServiceUri = val;
	}


	public get sunauServiceUri(): string {
		return this._sunauServiceUri;
	}

	@observable private _isRecipientEntity: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set isRecipientEntity(val: boolean) {
		this._isRecipientEntity = val;
	}


	public get isRecipientEntity(): boolean {
		return this._isRecipientEntity;
	}

	@observable private _includeInformation107: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set includeInformation107(val: boolean) {
		this._includeInformation107 = val;
	}


	public get includeInformation107(): boolean {
		return this._includeInformation107;
	}

	@observable private _includeInformation133: boolean = null;

	@TypeSystem.propertyDecorator('boolean')
	public set includeInformation133(val: boolean) {
		this._includeInformation133 = val;
	}


	public get includeInformation133(): boolean {
		return this._includeInformation133;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('CertificateForAccidentVM', moduleContext.moduleName)
export class CertificateForAccidentVM extends SigningDocumentFormVMBase<OfficialVM> {
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

	@observable private _certificateForAccidentHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateForAccidentHeader(val: string) {
		this._certificateForAccidentHeader = val;
	}


	public get certificateForAccidentHeader(): string {
		return this._certificateForAccidentHeader;
	}

	@observable private _certificateForAccidentData: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateForAccidentData(val: string) {
		this._certificateForAccidentData = val;
	}


	public get certificateForAccidentData(): string {
		return this._certificateForAccidentData;
	}

	@observable private _issuingPoliceDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set issuingPoliceDepartment(val: PoliceDepartment) {
		this._issuingPoliceDepartment = val;
	}


	public get issuingPoliceDepartment(): PoliceDepartment {
		return this._issuingPoliceDepartment;
	}

	@observable private _documentMustServeTo: DocumentMustServeToVM = null;

	@TypeSystem.propertyDecorator(DocumentMustServeToVM ? DocumentMustServeToVM : moduleContext.moduleName + '.DocumentMustServeToVM')
	public set documentMustServeTo(val: DocumentMustServeToVM) {
		this._documentMustServeTo = val;
	}


	public get documentMustServeTo(): DocumentMustServeToVM {
		return this._documentMustServeTo;
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
@TypeSystem.typeDecorator('CertificateToWorkWithFluorinatedGreenhouseGassesVM', moduleContext.moduleName)
export class CertificateToWorkWithFluorinatedGreenhouseGassesVM extends SigningDocumentFormVMBase<OfficialVM> {
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

	@observable private _certificateToWorkWithFluorinatedGreenhouseGassesHeader: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateToWorkWithFluorinatedGreenhouseGassesHeader(val: string) {
		this._certificateToWorkWithFluorinatedGreenhouseGassesHeader = val;
	}


	public get certificateToWorkWithFluorinatedGreenhouseGassesHeader(): string {
		return this._certificateToWorkWithFluorinatedGreenhouseGassesHeader;
	}

	@observable private _certificateValidity: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateValidity(val: string) {
		this._certificateValidity = val;
	}


	public get certificateValidity(): string {
		return this._certificateValidity;
	}

	@observable private _certificateToWorkWithFluorinatedGreenhouseGassesGround: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateToWorkWithFluorinatedGreenhouseGassesGround(val: string) {
		this._certificateToWorkWithFluorinatedGreenhouseGassesGround = val;
	}


	public get certificateToWorkWithFluorinatedGreenhouseGassesGround(): string {
		return this._certificateToWorkWithFluorinatedGreenhouseGassesGround;
	}

	@observable private _certificateToWorkWithFluorinatedGreenhouseGassesActivities: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateToWorkWithFluorinatedGreenhouseGassesActivities(val: string) {
		this._certificateToWorkWithFluorinatedGreenhouseGassesActivities = val;
	}


	public get certificateToWorkWithFluorinatedGreenhouseGassesActivities(): string {
		return this._certificateToWorkWithFluorinatedGreenhouseGassesActivities;
	}

	@observable private _entityData: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM ? ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM : moduleContext.moduleName + '.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM')
	public set entityData(val: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM) {
		this._entityData = val;
	}


	public get entityData(): ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataVM {
		return this._entityData;
	}

	@observable private _personData: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM ? ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM : moduleContext.moduleName + '.ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM')
	public set personData(val: ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM) {
		this._personData = val;
	}


	public get personData(): ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM {
		return this._personData;
	}

	@observable private _certificateToWorkWithFluorinatedGreenhouseGassesPersonsGround: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateToWorkWithFluorinatedGreenhouseGassesPersonsGround(val: string) {
		this._certificateToWorkWithFluorinatedGreenhouseGassesPersonsGround = val;
	}


	public get certificateToWorkWithFluorinatedGreenhouseGassesPersonsGround(): string {
		return this._certificateToWorkWithFluorinatedGreenhouseGassesPersonsGround;
	}

	@observable private _identificationPhoto: string = null;

	@TypeSystem.propertyDecorator('string')
	public set identificationPhoto(val: string) {
		this._identificationPhoto = val;
	}


	public get identificationPhoto(): string {
		return this._identificationPhoto;
	}

	@observable private _documentReceiptOrSigningDate: moment.Moment = null;

	@TypeSystem.propertyDecorator('moment')
	public set documentReceiptOrSigningDate(val: moment.Moment) {
		this._documentReceiptOrSigningDate = val;
	}


	public get documentReceiptOrSigningDate(): moment.Moment {
		return this._documentReceiptOrSigningDate;
	}

	@observable private _administrativeBodyName: string = null;

	@TypeSystem.propertyDecorator('string')
	public set administrativeBodyName(val: string) {
		this._administrativeBodyName = val;
	}


	public get administrativeBodyName(): string {
		return this._administrativeBodyName;
	}

	@observable private _policeDepartment: PoliceDepartment = null;

	@TypeSystem.propertyDecorator(PoliceDepartment ? PoliceDepartment : moduleContext.moduleName + '.PoliceDepartment')
	public set policeDepartment(val: PoliceDepartment) {
		this._policeDepartment = val;
	}


	public get policeDepartment(): PoliceDepartment {
		return this._policeDepartment;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}

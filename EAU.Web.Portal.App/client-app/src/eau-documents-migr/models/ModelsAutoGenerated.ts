import moment from 'moment';
import { observable } from 'mobx';
import { TypeSystem, BaseDataModel } from 'cnsys-core';
import { moduleContext } from '../ModuleContext';
import { ApplicationFormVMBase, PersonAddress, CitizenshipVM, DocumentMustServeToVM} from 'eau-documents';

@TypeSystem.typeDecorator('ApplicationForIssuingDocumentForForeignersVM', moduleContext.moduleName)
export class ApplicationForIssuingDocumentForForeignersVM extends ApplicationFormVMBase {
	@observable private _circumstances: ApplicationForIssuingDocumentForForeignersDataVM = null;

	@TypeSystem.propertyDecorator(ApplicationForIssuingDocumentForForeignersDataVM ? ApplicationForIssuingDocumentForForeignersDataVM : moduleContext.moduleName + '.ApplicationForIssuingDocumentForForeignersDataVM')
	public set circumstances(val: ApplicationForIssuingDocumentForForeignersDataVM) {
		this._circumstances = val;
	}


	public get circumstances(): ApplicationForIssuingDocumentForForeignersDataVM {
		return this._circumstances;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}
@TypeSystem.typeDecorator('ApplicationForIssuingDocumentForForeignersDataVM', moduleContext.moduleName)
export class ApplicationForIssuingDocumentForForeignersDataVM extends BaseDataModel {
	@observable private _birthDate: string = null;

	@TypeSystem.propertyDecorator('string')
	public set birthDate(val: string) {
		this._birthDate = val;
	}


	public get birthDate(): string {
		return this._birthDate;
	}

	@observable private _address: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set address(val: PersonAddress) {
		this._address = val;
	}


	public get address(): PersonAddress {
		return this._address;
	}

	@observable private _certificateFor: string = null;

	@TypeSystem.propertyDecorator('string')
	public set certificateFor(val: string) {
		this._certificateFor = val;
	}


	public get certificateFor(): string {
		return this._certificateFor;
	}

	@observable private _citizenship: CitizenshipVM = null;

	@TypeSystem.propertyDecorator(CitizenshipVM ? CitizenshipVM : moduleContext.moduleName + '.CitizenshipVM')
	public set citizenship(val: CitizenshipVM) {
		this._citizenship = val;
	}


	public get citizenship(): CitizenshipVM {
		return this._citizenship;
	}

	@observable private _documentMustServeTo: DocumentMustServeToVM = null;

	@TypeSystem.propertyDecorator(DocumentMustServeToVM ? DocumentMustServeToVM : moduleContext.moduleName + '.DocumentMustServeToVM')
	public set documentMustServeTo(val: DocumentMustServeToVM) {
		this._documentMustServeTo = val;
	}


	public get documentMustServeTo(): DocumentMustServeToVM {
		return this._documentMustServeTo;
	}

	@observable private _persistedAddress: PersonAddress = null;

	@TypeSystem.propertyDecorator(PersonAddress ? PersonAddress : moduleContext.moduleName + '.PersonAddress')
	public set persistedAddress(val: PersonAddress) {
		this._persistedAddress = val;
	}


	public get persistedAddress(): PersonAddress {
		return this._persistedAddress;
	}

	constructor(data?: any) {
		super(data);

		this.copyFrom(data);
	}
}

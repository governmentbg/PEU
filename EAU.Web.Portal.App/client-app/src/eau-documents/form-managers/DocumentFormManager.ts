import { BaseDataModel, ErrorHelper, ErrorInfo, ObjectHelper } from "cnsys-core";
import { DocumentType, EAUBaseValidator, Service } from "eau-core";
import { action, observable } from "mobx";
import { AttachedDocument, DocumentFormVMBase, DocumentModes, NomenclatureItem, NomenclatureType } from "../models";
import { IDocumentProvider } from "../providers/DocumentProvider";
import { DocumentFormValidationContext } from "../validations";

export class Section {
    code: string;
    additionalWebKeyCode?: string;
    form: BaseDataModel | BaseDataModel[];
    formUICmp: any;
    title: string;
    validate: () => boolean;
    validator?: EAUBaseValidator<BaseDataModel, DocumentFormValidationContext>
    @observable errors?: ErrorInfo[];
}

export interface DocumentFormManagerInitParams {
    mode: DocumentModes,
    attachedDocuments: AttachedDocument[],
    additionalData: any,
    service: Service,
    documentType: DocumentType,
    getNomenclatures: () => Promise<NomenclatureItem[]>
}

export interface IDocumentFormManager {
    documentForm: DocumentFormVMBase,
    attachedDocuments: AttachedDocument[],
    mode: DocumentModes,
    additionalData: any,
    service: Service,
    documentType: DocumentType,
    sections: Section[],
    hasPrintPreview: boolean,
    getNomenclatures: (nomenclatureType:NomenclatureType) => Promise<NomenclatureItem[]>

    validate: () => boolean;

    init: (formJson: any, initParams: DocumentFormManagerInitParams, provider: IDocumentProvider) => void;
}

export abstract class DocumentFormManagerBase<TDoc extends DocumentFormVMBase> implements IDocumentFormManager {

    private _documentForm: TDoc;
    private _attachedDocuments: AttachedDocument[];
    private _sections: Section[];

    private validator: EAUBaseValidator<DocumentFormVMBase, DocumentFormValidationContext>;
    private initParams: DocumentFormManagerInitParams;
    private validationContext: DocumentFormValidationContext;
    private nomeclatures: NomenclatureItem[];

    protected provider: IDocumentProvider;


    //#region IDocumentFormManager props

    get documentForm(): TDoc {
        return this._documentForm;
    }

    get attachedDocuments(): AttachedDocument[] {
        return this._attachedDocuments;
    }

    get mode(): DocumentModes {
        return this.initParams.mode;
    }

    get additionalData(): any {
        return this.initParams.additionalData;
    }

    get service(): Service {
        return this.initParams.service;
    }

    get documentType(): DocumentType {
        return this.initParams.documentType;
    }

    get sections(): Section[] {
        return this._sections;
    }

    get hasPrintPreview(): boolean {
        return this.initParams.additionalData.hasPrintPreview &&
            (this.initParams.additionalData.hasPrintPreview == "1" || this.initParams.additionalData.hasPrintPreview == "true" || this.initParams.additionalData.hasPrintPreview == "True")
    }

    getNomenclatures(nomenclatureType: NomenclatureType): Promise<NomenclatureItem[]> {
        let that = this;

        if (!this.nomeclatures) {

            return this.initParams.getNomenclatures().then((noms: NomenclatureItem[]) => {

                if (noms && noms.length > 0) {
                    that.nomeclatures = noms;
                    let filteredNoms = noms.filter(x => x.type == nomenclatureType)

                    return Promise.resolve(filteredNoms)
                }

                return Promise.resolve([]);
            })
        }else {

            let filteredNoms = this.nomeclatures.filter(x => x.type == nomenclatureType)

            return Promise.resolve(filteredNoms)
        }
    }

    //#endregion

    //#region IDocumentFormManager

    validate(): boolean {
        var result = this.validator.validate(this.documentForm);

        for (var section of this.sections) {
            this.initSectionErrors(section);
        }

        return result;
    }

    init(formJson: any, initParams: DocumentFormManagerInitParams, provider: IDocumentProvider) {
        this.provider = provider;
        this.initParams = initParams;
        this._attachedDocuments = this.initParams.attachedDocuments ? this.initParams.attachedDocuments : [];

        if (formJson) {
            this._documentForm = this.createDocument(formJson);

            this.initDocumentForm();

            this.validationContext = this.createValidationContext();
            this.validator = this.provider.getValidator();

            this.validator.setValidationContext(this.validationContext);

            this._sections = this.createSections(this.validationContext);
        }        
    }

    //#endregion 

    //#region Abstract

    protected abstract createDocument(obj: any): TDoc;

    //#endregion

    //#region Virtual functions

    protected initDocumentForm() {
    }

    protected createValidationContext(): DocumentFormValidationContext {
        return {
            documentFormManager: this
        };
    }

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        return [];
    }

    //endregion

    @action protected validateSection(section: Section): boolean {
        if (section.validator) {
            var isValid = section.validator.validate(section.form as BaseDataModel);

            if (isValid) {
                section.errors = [];
            }
            else {
                this.initSectionErrors(section);
            }

            return isValid;
        }
        else {
            return true;
        }
    }

    private initSectionErrors(section: Section) {
        var errors: ErrorInfo[]

        if (!ObjectHelper.isArray(section.form)) {
            errors = ErrorHelper.getErrorsRecursive(section.form);
        }
        else {
            errors = [];

            for (var form of section.form) {
                errors = errors.concat(ErrorHelper.getErrorsRecursive(form))
            }
        }

        section.errors = errors;
    }
}
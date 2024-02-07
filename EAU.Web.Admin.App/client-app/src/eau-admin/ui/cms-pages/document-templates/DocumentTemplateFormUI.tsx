import { ArrayHelper, ObjectHelper, SelectListItem } from 'cnsys-core';
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { attributeClassRequiredLabel, Constants as ConstantsEAU, DocumentTemplate, DocumentTemplateSearchCriteria, DocumentType, EAUBaseComponent, Nomenclatures, TextEditorUI, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Alert } from 'reactstrap';
import { Constants } from '../../../Constants';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';
import { DocumentTemplatesValidator } from '../../../validations/DocumentTemplatesValidator';
import BtnGroupFormUI from '../../common/BtnGroupFormUI';

interface DocumentTemplateFormtRouteProps extends BaseRouteParams {
    documentTemplateID?: number
}

interface DocumentTemplateFormProps extends BaseRouteProps<DocumentTemplateFormtRouteProps>, AsyncUIProps, BaseRoutePropsExt {
}

@observer export class DocumentTemplateFormImplUI extends EAUBaseComponent<DocumentTemplateFormProps, DocumentTemplate>{

    @observable private notification: any;
    @observable private isDocumentTemplateLoaded: boolean;
    @observable private isDocTemplateFieldsLoaded: boolean;
    @observable private documentTypes: SelectListItem[];
    @observable private docTypes: DocumentType[];
    @observable private docTemplateFields: SelectListItem[];
    @observable private usedActiveFields: string;

    private nomenclaturesDataService: NomenclaturesDataService;
    private documentTemplatesDataService: NomenclaturesDataService;

    constructor(props: DocumentTemplateFormProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    render(): JSX.Element {

        if (ObjectHelper.isNullOrUndefined(this.props.match.params.documentTemplateID) || (this.props.match.params.documentTemplateID && this.isDocumentTemplateLoaded)) {

            return (
                <div className="card">
                    <div className="card-body">
                        {this.notification}
                        <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                        <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                        <div className="row">
                            <div className="form-group col-md-6">
                                {this.labelFor(m => m.documentTypeID, "GL_DECLARATION_L", attributeClassRequiredLabel)}
                                {this.dropDownListFor(m => m.documentTypeID, this.documentTypes, null, null, true, this.getResource("GL_CHOICE_L"))}
                            </div>
                            <div className="form-group col-md-3">
                                <label className="d-none d-md-inline-block"></label>
                                <div className="custom-control custom-checkbox">
                                    {this.checkBoxFor(x => x.isSubmittedAccordingToTemplate, "GL_ONLY_BY_TEMPLATE_L", { className: "custom-control-input" })}
                                </div>
                            </div>
                            {
                                this.props.match.params.documentTemplateID
                                    ? <div className="form-group col-md-3">
                                        {this.labelFor(x => x.updatedOn, "GL_CREATE_UPDATE_DATE_L")}
                                        {<input id="input_updatedOn" className="form-control" disabled value={this.model.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime).toString()} />}
                                    </div>
                                    : null
                            }
                        </div>
                        <div className="row">
                            <div className="form-group col-md-6">
                                <label htmlFor="ACTIVE_FIELDS">{this.getResource("GL_ACTIVE_FIELDS_L")}</label>
                                <textarea id="Identificators" value={this.usedActiveFields} className="form-control" disabled></textarea>
                            </div>
                        </div>
                        <div className="row form-group">
                            <div className="col-sm-12">
                                {this.labelFor(x => x.content, "GL_CONTENT_L", { className: "required-field" })}
                                <div>{this.isDocTemplateFieldsLoaded && <TextEditorUI {...this.bind(x => x.content)} activeFields={this.docTemplateFields} />}</div>
                            </div>
                        </div>
                    </div>
                    <BtnGroupFormUI refuseLink={Constants.PATHS.DocumentTemplates} onSave={this.save} />
                </div>
            )
        }

        return null;
    }

    componentDidMount() {
        this.getDocumentTemplateFields();

        if (this.props.match.params.documentTemplateID)
            this.initModel();
    }

    @action private save() {
        if (ObjectHelper.isNullOrUndefined(this.model.isSubmittedAccordingToTemplate)) {
            this.model.isSubmittedAccordingToTemplate = false;
        }

        if (this.props.location.pathname == Constants.PATHS.AddDocumentTemplate) {

            if (this.validators[0].validate(this.model)) {

                this.props.registerAsyncOperation(this.nomenclaturesDataService.createDocumentTemplates(this.model).then(() => {
                    runInAction.bind(this)(() => {

                        this.notification = <Alert color="success">{this.getResource("GL_SAVE_OK_I")}</Alert>
                        this.model.content = null;
                        this.documentTypes.splice(1, this.documentTypes.findIndex(x => x.value == this.model.documentTypeID.toString()))
                        this.model = new DocumentTemplate();
                    })
                }))
            }
        } else {
            this.props.registerAsyncOperation(this.nomenclaturesDataService.updateDocumentTemplates(this.model).then(() => {
                this.notification = <Alert color="success">{this.getResource("GL_UPDATE_OK_I")}</Alert>
                this.defineUsedActiveFileds();
            }))
        }
    }

    //#region Main funcs

    private funcBinds() {
        this.save = this.save.bind(this);
        this.getDocumentTemplateFields = this.getDocumentTemplateFields.bind(this);
        this.getDocTypeName = this.getDocTypeName.bind(this);
    }

    @action public getDocumentTemplateFields() {

        this.docTemplateFields = [];

        this.props.registerAsyncOperation(this.nomenclaturesDataService.getDocumentTemplateFields().then(result => {

            runInAction.bind(this)(() => {

                for (var i = 0; i < result.length; i++) {
                    let selectListItem = new SelectListItem;
                    selectListItem.text = result[i].description;
                    selectListItem.value = result[i].key;

                    this.docTemplateFields.push(selectListItem);
                }
            })
        }).finally(() => {
            this.isDocTemplateFieldsLoaded = true;
        }));
    }

    @action private searchDocTypes() {
        let that = this;
        let documentTemplateSearchCriteria = new DocumentTemplateSearchCriteria();
        documentTemplateSearchCriteria.pageSize = Number.MAX_SAFE_INTEGER;

        return this.documentTemplatesDataService.searchDocumentTemplates(documentTemplateSearchCriteria).then((documentTemplates: DocumentTemplate[]) => {

            let usedDocTypesIds = ArrayHelper.queryable.from(documentTemplates).select(el => el.documentTypeID).toArray().splice(that.props.match.params.documentTemplateID);

            return Nomenclatures.getDocumentTypes().then(documentTypes => {

                runInAction(() => {
                    that.documentTypes = [];

                    documentTypes.forEach(documentType => {

                        if (usedDocTypesIds.indexOf(documentType.documentTypeID) == -1 && (documentType.type == 2)) {
                            that.documentTypes.push(new SelectListItem({ selected: false, text: documentType.name, value: documentType.documentTypeID }))
                        }

                    });
                })
            })
        })
    }

    @action private init() {
        this.docTypes = [];
        this.documentTypes = [];
        this.model = new DocumentTemplate();
        this.validators = [new DocumentTemplatesValidator()]

        this.nomenclaturesDataService = new NomenclaturesDataService();
        this.documentTemplatesDataService = new NomenclaturesDataService();

        this.props.registerAsyncOperation(this.searchDocTypes());
    }

    private initModel() {
        this.props.registerAsyncOperation(this.nomenclaturesDataService.getDocumentTemplateById(this.props.match.params.documentTemplateID).then((docTemplate) => {
            runInAction.bind(this)(() => {

                if (!docTemplate)
                    this.notification = <Alert color="error">{this.getResource("GL_DECLARATION_NOT_EXIST_E")}</Alert>

                this.model = docTemplate;
                this.defineUsedActiveFileds();
            })
        }).finally(() => {
            this.isDocumentTemplateLoaded = true;
        }))
    }

    private getDocTypeName(id: number): string {

        var currentDocType = this.docTypes.filter(docType => docType.documentTypeID == id)[0];
        return currentDocType.name;
    }

    @action private defineUsedActiveFileds() {
        this.usedActiveFields = "";

        if (this.props.location.pathname != Constants.PATHS.AddDocumentTemplate) {
            let regex = /{([^}]+)\}/g;
            let result;

            while ((result = regex.exec(this.model.content)) !== null) {
                this.usedActiveFields += result + '\r\n';
            }
        }
    }

    //#endregion
}

export const DocumentTemplateFormUI = withRouter(withAsyncFrame(DocumentTemplateFormImplUI, false));
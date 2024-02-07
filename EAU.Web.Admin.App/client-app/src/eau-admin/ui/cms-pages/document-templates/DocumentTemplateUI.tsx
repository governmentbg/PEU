import { AsyncUIProps, BaseRouteProps, BaseRoutePropsExt, ConfirmationModal, RawHTML, withAsyncFrame } from 'cnsys-ui-react';
import { DocumentTemplate, DocumentTemplateSearchCriteria, DocumentType, EAUBaseComponent, Pagination, ValidationSummaryErrors, Constants as ConstantsEAU } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Link, withRouter } from 'react-router-dom';
import { Alert, Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import { Constants } from '../../../Constants';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';

interface DocumentTemplateProps extends BaseRouteProps<any>, AsyncUIProps, BaseRoutePropsExt {
}

@observer export class DocumentTemplateImplUI extends EAUBaseComponent<DocumentTemplateProps, DocumentTemplateSearchCriteria>{
    @observable isOpen: boolean;
    @observable private documentTemplate: DocumentTemplate[];
    @observable private documentTypes: DocumentType[] = [];
    @observable private loadedDocTypes: boolean;
    @observable private previewDocumentTemplate: DocumentTemplate;
    @observable private notification: any;

    private documentTemplatesDataService: NomenclaturesDataService;
    private nomenclaturesDataService: NomenclaturesDataService;

    constructor(props: DocumentTemplateProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    render() {

        if (this.documentTemplate && this.documentTemplate.length > 0 && this.loadedDocTypes) {
            return <div className="card">
                <div className="card-body">
                    {this.notification}
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                    <div id="request-messages"></div>
                    <div className="card-navbar">
                        <div className="button-bar">
                            <div className="right-side">
                                <Link to={Constants.PATHS.AddDocumentTemplate} className="btn btn-primary"><i className="ui-icon ui-icon-plus-white"></i> {this.getResource("GL_ADD_DOCUMENT_TEMPLATES_L")}</Link>
                            </div>
                            <div className="left-side"></div>
                        </div>
                    </div>
                    <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                    <div className="table-responsive">
                        <table className="table table-bordered table-striped table-hover">
                            <thead>
                                <tr>
                                    <th>{this.getResource("GL_DECLARATION_L")}</th>
                                    <th>{this.getResource("GL_CREATE_UPDATE_DATE_L")}</th>
                                    <th>{this.getResource("GL_ACTIONS_L")}</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    this.documentTemplate.map((documentTemplate, index) => {

                                        let foundDocType = this.documentTypes.find(m => m.documentTypeID == documentTemplate.documentTypeID);
                                        let docTypeName = foundDocType ? foundDocType.name : "";

                                        return <tr key={index}>
                                            <td>{docTypeName}<span className="param-value"></span></td>
                                            <td>{documentTemplate.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime).toString()}<span className="param-value"></span></td>
                                            <td className="buttons-td">

                                                <button className="btn btn-secondary" title={this.getResource("GL_VIEW_L")} onClick={() => { this.toggle(); this.setPreviewDocumentTemplate(documentTemplate) }}><i
                                                    className="ui-icon ui-icon-eye"></i></button>

                                                <Modal isOpen={this.isOpen} toggle={this.toggle}>
                                                    <ModalHeader toggle={this.toggle}>{this.getResource("GL_VIEW_DECLARATION_TEMPLATE_L")}</ModalHeader>
                                                    <ModalBody>
                                                        <div className="row">
                                                            <div className="col-6">
                                                                <strong>{this.getResource("GL_DECLARATION_L")}</strong>
                                                                <p>{docTypeName}</p>
                                                            </div>
                                                            <div className="col-6">
                                                                <p><strong>{this.getResource("GL_ACTIVE_FIELDS_L")}</strong></p>
                                                                <textarea id="textarea-ACTIVE-FIELDS" value={this.defineUsedActiveFileds(this.previewDocumentTemplate.content)} className="form-control form-control-plaintext" rows={3} readOnly={true} />
                                                            </div>
                                                            <div className="col-12">
                                                                <p><strong>{this.getResource("GL_CONTENT_L")}</strong></p>
                                                                <RawHTML rawHtmlText={this.previewDocumentTemplate.content}></RawHTML>
                                                            </div>
                                                        </div>
                                                    </ModalBody>
                                                    <ModalFooter>
                                                        <div className="button-bar">
                                                            <button type="button" className="btn btn-secondary close-preview-modal" onClick={this.toggle}>{this.getResource("GL_CLOSE_L")}</button>
                                                        </div>
                                                    </ModalFooter>
                                                </Modal>

                                                <Link to={Constants.PATHS.EditDocumentTemplate.replace(':documentTemplateID', documentTemplate.docTemplateID.toString())} className="btn btn-secondary"><i className="ui-icon ui-icon-edit" title={this.getResource("GL_EDIT_L")}></i></Link>
                                                <ConfirmationModal
                                                    modalTitleKey={"GL_DELETE_DOCUMENT_TEMPLATE_I"}
                                                    modalTextKeys={["GL_DEL_CONFIRM_DECLARATION_TEMPLATE_I"]}
                                                    noTextKey="GL_CANCEL_L"
                                                    yesTextKey="GL_CONFIRM_L"
                                                    onSuccess={() => this.deleteValue(documentTemplate.docTemplateID)}>
                                                    <button type="button" className="btn btn-secondary" title={this.getResource("GL_DELETE_L")}>
                                                        <i className="ui-icon ui-icon-trash"></i>
                                                    </button>
                                                </ConfirmationModal>
                                            </td>

                                        </tr>
                                    })
                                }
                            </tbody>
                        </table>
                    </div>
                    <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} />
                </div>
            </div>
        }
        else {
            return <Link to={Constants.PATHS.AddDocumentTemplate} className="btn btn-primary"><i className="ui-icon ui-icon-plus-white"></i> {this.getResource("GL_ADD_DOCUMENT_TEMPLATES_L")}</Link>

        }

    };

    private setPreviewDocumentTemplate(documentTemplate: DocumentTemplate) {
        this.previewDocumentTemplate = documentTemplate;
    }

    @action private onPageChange(page: any): void {
        this.model.page = page;
        this.props.registerAsyncOperation(this.searchDocumentTemplates());
    }

    private toggle() {
        this.isOpen = !this.isOpen;
    }

    private defineUsedActiveFileds(contentString: string) {
        let usedActiveFields = "";

        if (this.props.location.pathname != Constants.PATHS.AddDocumentTemplate) {
            let regex = /{([^}]+)\}/g;
            let result;

            while ((result = regex.exec(contentString)) !== null) {
                usedActiveFields += result + '\r\n';
            }
        }

        return usedActiveFields;
    }

    private searchDocumentTemplates() {

        return this.documentTemplatesDataService.searchDocumentTemplates(this.model).then(result => {
            this.documentTemplate = result;
        });
    }

    private deleteValue(documentTypeId: number) {
        this.props.registerAsyncOperation(this.documentTemplatesDataService.deleteDocumentTemplate(documentTypeId).then(() => {
            return this.searchDocumentTemplates().then(() => {
                this.notification = <Alert color="success">{this.getResource('GL_DELETED_TIME_I')}</Alert>
            })
        }));
    }

    private searchDocumentTypes() {
        let that = this;

        this.nomenclaturesDataService.getDocumentTypes().then((docTypes) => {
            that.documentTypes = docTypes;
        }).finally(() => {
            that.loadedDocTypes = true;
        });
    }

    //#region main functions

    private funcBinds() {
        this.onPageChange = this.onPageChange.bind(this);
        this.deleteValue = this.deleteValue.bind(this);
        this.searchDocumentTypes = this.searchDocumentTypes.bind(this);
        this.toggle = this.toggle.bind(this);
        this.setPreviewDocumentTemplate = this.setPreviewDocumentTemplate.bind(this);
    }

    private init() {
        this.documentTemplatesDataService = new NomenclaturesDataService();
        this.nomenclaturesDataService = new NomenclaturesDataService();

        this.model = new DocumentTemplateSearchCriteria();
        this.previewDocumentTemplate = new DocumentTemplate();
        this.props.registerAsyncOperation(this.searchDocumentTemplates());
        this.searchDocumentTypes();
    }

    //#endregion
}

export const DocumentTemplateUI = withRouter(withAsyncFrame(DocumentTemplateImplUI, false));
import { BaseDataModel, ErrorInfo, ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, ConfirmationModal, ViewMode, withAsyncFrame } from "cnsys-ui-react";
import { appConfig, Button, eauAuthenticationService, EAUBaseComponent, Nomenclatures, SigningProcessUI, ValidationSummaryErrors } from "eau-core";
import { ApplicationFormManagerBase, ApplicationFormVMBase, AttachedDocumentsUI, DocumentFormManagerProvider, DocumentFormPreviewUI, DocumentModes, Section, SectionTitleUI } from 'eau-documents';
import { observable } from 'mobx';
import { observer } from "mobx-react";
import React from 'react';
import { Alert } from 'reactstrap';
import { ProcessStatuses } from '../models/ModelsManualAdded';
import { IDocumentProcessContext } from "../process-contexts/IDocumentProcessContext";
import { MenuUI } from './MenuUI';
import { SendingUI } from './SendingUI';

interface DocumentProcessUIProps extends BaseProps, AsyncUIProps {
    context: IDocumentProcessContext
    currentSectionCode?: string
    onChangeSection: (code: string) => void
    onDeleted?: (caseFileURI: string) => void;
    showRejectButtonInsteadOfNewDocButton?: boolean;
    skipAuthorizationForAccessToProcess?: boolean;
}

var deleteDocProcessMsgKeys = ['GL_REJECT_MSG_1_L', 'GL_REJECT_MSG_2_L'];
var cancelDocSingingMsgKeys = ['GL_REJECT_APPLICATION_DOCUMENT_L', 'GL_REJECT_MSG_SIGNING_DOCUMENT_I'];

@observer class DocumentProcessUIImpl extends EAUBaseComponent<DocumentProcessUIProps, any> {

    @observable currentSection: Section;

    saveDocTimeout: any;

    @observable private documentName: string;

    @observable hasUserAccessToProcess?: boolean;

    constructor(props?: DocumentProcessUIProps) {
        super(props);

        //Bind
        this.componentWillUpdate = this.componentWillUpdate.bind(this);
        this.componentDidUpdate = this.componentDidUpdate.bind(this);
        this.componentWillUnmount = this.componentWillUnmount.bind(this);
        this.scrollTopOrFocusFirstErrElement = this.scrollTopOrFocusFirstErrElement.bind(this);

        this.changeSection = this.changeSection.bind(this);
        this.delete = this.delete.bind(this);
        this.refresh = this.refresh.bind(this);
        this.startSigning = this.startSigning.bind(this);
        this.startSending = this.startSending.bind(this);
        this.goToNextSection = this.goToNextSection.bind(this);
        this.goToPreviewSection = this.goToPreviewSection.bind(this);
        this.toggleShowMenu = this.toggleShowMenu.bind(this);
        this.printDocument = this.printDocument.bind(this);
        this.downloadXmlDocument = this.downloadXmlDocument.bind(this);
        this.newPreviewDocument = this.newPreviewDocument.bind(this);

        if (this.props.skipAuthorizationForAccessToProcess) {
            this.hasUserAccessToProcess = true;
        }
        else {
            this.props.registerAsyncOperation(eauAuthenticationService.getCurrentUser().bind(this).then(usr => {
                if (usr) {
                    this.hasUserAccessToProcess = true;
                }
                else {
                    this.hasUserAccessToProcess = false;
                }
            }))
        }

        //Init
        this.props.registerAsyncOperation(this.setCurrentSection(this.props.currentSectionCode));

        if ((this.props.context.formManager.mode != DocumentModes.ViewDocument) && (this.props.context.formManager.mode != DocumentModes.SignDocument)) {
            this.saveDocumentPeriodically();
        }

        this.initDocumentName();
    }

    render(): JSX.Element {
        let content = null;

        if (this.props.context.status == ProcessStatuses.InProcess)
            content = this.renderInProcess();
        else if (this.props.context.status == ProcessStatuses.Signing)
            content = this.renderSigning();
        else if (this.props.context.status == ProcessStatuses.Sending || this.props.context.status == ProcessStatuses.ReadyForSending)
            content = this.renderSending();
        else if (this.props.context.status == ProcessStatuses.Accepted)
            content = this.renderAccepted();

        return <>
            {this.renderPageHeader()}
            {content}
        </>
    }

    //#region Renders

    renderInProcess() {
        if (this.props.context.hasChangedApplicant) {
            return (
                <div className="page-wrapper" id="ARTICLE-CONTENT">
                    <Alert color="warning">{this.getResource("GL_00002_E")}</Alert>
                    <div className="button-bar button-bar--form button-bar--responsive">
                        <div className="left-side">
                            <ConfirmationModal modalTitleKey='GL_REJECT_APPLICATION_L' modalTextKeys={deleteDocProcessMsgKeys} onSuccess={this.delete} yesTextKey='GL_OK_L' noTextKey='GL_NO_L'>
                                <Button type="button" className="btn btn-secondary" lableTextKey="GL_REJECT_L"></Button>
                            </ConfirmationModal>
                        </div>
                    </div>
                </div>);
        }

        if (this.props.context.hasChangesInApplicationsNomenclature) {
            return (
                <div className="page-wrapper" id="ARTICLE-CONTENT">
                    <Alert color="warning">{this.getResource("GL_00005_E")}</Alert>
                    <div className="button-bar button-bar--form button-bar--responsive">
                        <div className="left-side">
                            <ConfirmationModal modalTitleKey='GL_REJECT_APPLICATION_L' modalTextKeys={deleteDocProcessMsgKeys} onSuccess={this.delete} yesTextKey='GL_OK_L' noTextKey='GL_NO_L'>
                                <Button type="button" className="btn btn-secondary" lableTextKey="GL_REJECT_L"></Button>
                            </ConfirmationModal>
                        </div>
                    </div>
                </div>);
        }

        return (
            <>
                {
                    ObjectHelper.isSubClassOf(this.props.context.formManager, ApplicationFormManagerBase) && (this.props.context.formManager.documentForm as ApplicationFormVMBase).authorHasNonPaidSlip
                        ? <div className="general-message-wrapper">
                            {
                                (this.props.context.formManager.documentForm as ApplicationFormVMBase).authorHasNonPaidSlip
                                    ? <div className="alert alert-warning" role="alert"> <p>{this.getResource('GL_00029_I')}</p></div>
                                    : null
                            }
                        </div>
                        : null
                }
                {this.props.context.formManager.additionalData && this.props.context.formManager.additionalData.removingIrregularitiesInstructionURI &&
                    <div className="general-message-wrapper">
                        <div className="alert alert-warning" role="alert">
                            <p>{this.getResource('GL_00003_I')} {this.props.context.caseFileUri}</p>

                        </div>
                    </div>
                }
                {this.props.context.formManager.sections && this.props.context.formManager.sections.length > 1 && this.props.context.formManager.mode != DocumentModes.SignDocument && this.props.context.formManager.mode != DocumentModes.ViewDocument &&
                    <MenuUI onChangeSection={this.changeSection} sections={this.props.context.formManager.sections} currentSectionCode={this.currentSection ? this.currentSection.code : null} collapsed={false} />
                }
                <div className="page-wrapper" id="ARTICLE-CONTENT">
                    {this.currentSection && this.props.context.formManager.mode != DocumentModes.SignDocument && this.props.context.formManager.mode != DocumentModes.ViewDocument ?
                        this.renderCurrentSection() :
                        this.renderDocumentFormPreview()}
                </div>
            </>);
    }

    renderSigning() {
        if (this.props.context.hasChangedApplicant) {
            return (
                <div className="page-wrapper" id="ARTICLE-CONTENT">
                    <Alert color="warning">{this.getResource("GL_00002_E")}</Alert>
                    <div className="button-bar button-bar--form button-bar--responsive">
                        <div className="left-side">
                            <ConfirmationModal modalTitleKey='GL_REJECT_APPLICATION_L' modalTextKeys={deleteDocProcessMsgKeys} onSuccess={this.delete} yesTextKey='GL_OK_L' noTextKey='GL_NO_L'>
                                <Button type="button" className="btn btn-secondary" lableTextKey="GL_REJECT_L"></Button>
                            </ConfirmationModal>
                        </div>
                    </div>
                </div>);
        }

        if (this.props.context.hasChangesInApplicationsNomenclature) {
            return (
                <div className="page-wrapper" id="ARTICLE-CONTENT">
                    <Alert color="warning">{this.getResource("GL_00005_E")}</Alert>
                    <div className="button-bar button-bar--form button-bar--responsive">
                        <div className="left-side">
                            <ConfirmationModal modalTitleKey='GL_REJECT_APPLICATION_L' modalTextKeys={deleteDocProcessMsgKeys} onSuccess={this.delete} yesTextKey='GL_OK_L' noTextKey='GL_NO_L'>
                                <Button type="button" className="btn btn-secondary" lableTextKey="GL_REJECT_L"></Button>
                            </ConfirmationModal>
                        </div>
                    </div>
                </div>);
        }

        return (
            <div className="page-wrapper" id="ARTICLE-CONTENT">
                <div className="ui-form ui-form--preview">
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                    <DocumentFormManagerProvider documentFormManager={this.props.context.formManager}>
                        <DocumentFormPreviewUI documentFormManager={this.props.context.formManager} errors={this.props.asyncErrors} onChangeSection={null} getAttachedDocDounloadUrl={this.props.context.getAttachedDocumentDownloadURL} />
                    </DocumentFormManagerProvider>
                    <SigningProcessUI
                        signingProcessGuid={this.props.context.signingGuid}
                        onCompleted={this.refresh}
                        onRejected={this.refresh}
                    />
                </div>
            </div>
        );
    }

    renderPageHeader() {
        return <div className="page-header" id="PAGE-CONTENT">
            <div className="row">
                <div className="col">
                    <h1 className="page-title">{this.documentName ?? null}</h1>
                </div>
                {this.showMenuButton() ?
                    <div className="col-auto">
                        <div className="button-menu-container">
                            <button id="show-menu" type="button" className="button-menu d-md-none collapsed" aria-expanded="false" onClick={this.toggleShowMenu}>
                                <i className="ui-icon ui-icon-sub-menu" aria-hidden="true"></i>
                                <span className="sr-only">{this.getResource("GL_SHOW_APP_NAV_SECTION_L")}</span>
                            </button>
                        </div>
                    </div>
                    : null}
            </div>
        </div>
    }

    renderSending() {
        return <SendingUI processStatus={this.props.context.status} />
    }

    renderAccepted() {
        return <SendingUI processStatus={this.props.context.status} />
    }

    renderCurrentSection() {
        if (this.currentSection.code == "attachedDocuments") {
            let sectionErrors: string[] = undefined;

            if (this.currentSection.errors && this.currentSection.errors.length > 0) {
                sectionErrors = [];
                for (let i: number = 0; i < this.currentSection.errors.length; i++) {
                    sectionErrors.push(this.currentSection.errors[i].error);
                }
            }

            return (
                <div key={this.currentSection.code} className="ui-form ui-form--input">
                    <SectionTitleUI title={this.currentSection.title} sectionCode={this.currentSection.code} additionalWebKeyCode={this.currentSection.additionalWebKeyCode} />
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                    <DocumentFormManagerProvider documentFormManager={this.props.context.formManager}>
                        <AttachedDocumentsUI
                            viewMode={ViewMode.Edit}
                            documentUploadURL={this.props.context.getAttachedDocumentUploadURL()}
                            getDocumentDownloadUrl={this.props.context.getAttachedDocumentDownloadURL}
                            onDeleteDocument={this.props.context.deleteAttachedDocument}
                            onSaveAttachedDocumentTemplate={this.props.context.saveAttachedDocumentTemplate}
                            onStartSigningAttachedDocumentTemplate={this.props.context.startSigningAttachedDocumentTemplate}
                            onRefreshAttachedDocuments={this.props.context.refreshAttachedDocuments}
                            sectionErrors={sectionErrors}
                        />
                    </DocumentFormManagerProvider>
                    {this.renderButtonBar()}
                </div>
            );
        }
        else {
            var props = this.bind(this.currentSection.form as BaseDataModel, ViewMode.Edit, this.currentSection.code, [this.currentSection.validator]);

            return (
                <div key={this.currentSection.code} className="ui-form ui-form--input">
                    {this.currentSection.code == "main" && this.props.context.formManager.sections.length == 1 ? null :
                        <SectionTitleUI title={this.currentSection.title} sectionCode={this.currentSection.code} additionalWebKeyCode={this.currentSection.additionalWebKeyCode} />
                    }
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                    <DocumentFormManagerProvider documentFormManager={this.props.context.formManager}>
                        {React.createElement(this.currentSection.formUICmp, props)}
                    </DocumentFormManagerProvider>
                    {this.renderButtonBar()}
                </div>
            );
        }
    }

    renderDocumentFormPreview() {
        return (
            <div className="ui-form ui-form--preview">
                <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                <DocumentFormManagerProvider documentFormManager={this.props.context.formManager}>
                    <DocumentFormPreviewUI documentFormManager={this.props.context.formManager} errors={this.props.asyncErrors} onChangeSection={this.props.context.formManager.mode != DocumentModes.SignDocument && this.props.context.formManager.mode != DocumentModes.ViewDocument ? this.changeSection : null} getAttachedDocDounloadUrl={this.props.context.getAttachedDocumentDownloadURL} />
                </DocumentFormManagerProvider>
                {this.renderButtonBar()}
            </div>
        );
    }

    renderButtonBar() {
        return (
            <div className="button-bar button-bar--form button-bar--responsive">
                <div className="right-side">
                    {
                        this.currentSection && this.props.context.formManager.mode != DocumentModes.SignDocument && this.props.context.formManager.mode != DocumentModes.ViewDocument
                            ? <>
                                <Button type="button" className="btn btn-primary" lableTextKey="GL_CONTINUE_L" onClick={this.goToNextSection}></Button>
                                {this.props.context.formManager.sections.length > 1 && <Button type="button" className="btn btn-secondary" lableTextKey="GL_PREVIEW_L" onClick={this.goToPreviewSection}></Button>}
                            </>
                            : <>
                                {(this.props.context.formManager.mode == DocumentModes.SignDocument || this.props.context.formManager.mode == DocumentModes.NewApplication || this.props.context.formManager.mode == DocumentModes.RemovingIrregularitiesApplication || this.props.context.formManager.mode == DocumentModes.EditAndSignDocument || this.props.context.formManager.mode == DocumentModes.AdditionalApplication || this.props.context.formManager.mode == DocumentModes.WithdrawService) &&
                                    <Button type="button" className="btn btn-primary" lableTextKey="GL_SIGN_L" onClick={this.startSigning} disabled={this.hasClientErrorsOnSection()}></Button>}
                                {this.props.context.formManager.mode == DocumentModes.ViewDocument && this.hasUserAccessToProcess &&
                                    <Button type="button" className="btn btn-secondary" lableTextKey="GL_DOWNLOAD_DOC_AS_FILE_L" onClick={this.downloadXmlDocument}></Button>}
                                {this.props.context.formManager.mode == DocumentModes.EditDocument &&
                                    <Button type="button" className="btn btn-primary" lableTextKey="GL_SAVE_L" onClick={this.startSending}></Button>}
                                {this.props.context.formManager.hasPrintPreview && this.hasUserAccessToProcess ? <iframe id="ifmcontentstoprint" style={{ display: 'none' }}></iframe> : null}
                                {this.props.context.formManager.hasPrintPreview && this.hasUserAccessToProcess &&
                                    <Button type="button" className="btn btn-secondary" lableTextKey="GL_PRINT_L" onClick={this.printDocument}></Button>}
                            </>
                    }
                </div>
                <div className="left-side">
                    {
                        this.props.context.formManager.mode != DocumentModes.ViewDocument
                            ? <ConfirmationModal modalTitleKey='GL_REJECT_APPLICATION_L' modalTextKeys={(this.props.context.formManager.mode == DocumentModes.SignDocument ? cancelDocSingingMsgKeys : deleteDocProcessMsgKeys)} onSuccess={this.delete} yesTextKey='GL_OK_L' noTextKey='GL_NO_L'>
                                <Button type="button" className="btn btn-secondary" lableTextKey="GL_REJECT_L"></Button>
                            </ConfirmationModal>
                            : this.props.showRejectButtonInsteadOfNewDocButton
                                ? <Button type="button" className="btn btn-secondary" lableTextKey="GL_CLOSE_L" onClick={this.delete}></Button>
                                : ObjectHelper.isStringNullOrEmpty(this.props.context.caseFileUri) && ObjectHelper.isStringNullOrEmpty(this.props.context.formManager.additionalData.NotAcknowledgedMessageURI)
                                    ? <Button type="button" className="btn btn-secondary" lableTextKey="GL_CHOICE_NEWDOC_L" onClick={this.newPreviewDocument}></Button>
                                    : null
                    }
                </div>
            </div >);
    }

    //#endregion

    //#region Livecycle Events

    componentWillUpdate(nextProps: DocumentProcessUIProps, nextState: any, nextContext: any): void {
        super.componentWillUpdate(nextProps, nextState, nextContext);

        if ((this.currentSection && nextProps.currentSectionCode && nextProps.currentSectionCode != this.currentSection.code) ||
            (!this.currentSection && nextProps.currentSectionCode && nextProps.currentSectionCode != "preview")) {
            this.props.registerAsyncOperation(this.setCurrentSection(nextProps.currentSectionCode));
        }
    }

    componentDidUpdate(): void {
        if (!this.currentSection) {
            if (this.props.context.isContextInitialized === true && this.props.context.status == ProcessStatuses.Signing) {
                window.setTimeout(() => { window.scrollTo(0, $('.footer-wrapper').first().offset().top); }, 500);
            } else {
                window.scrollTo(0, 0);
            }
        }
        else {
            window.setTimeout(this.scrollTopOrFocusFirstErrElement, 500);
        }
    }

    componentWillUnmount() {
        //Зачистваме периодичното записване на черновата.
        clearTimeout(this.saveDocTimeout);

        this.props.context.disposeRefresh();
    }

    //#endregion

    //#region Events

    refresh() {
        return this.props.context.refresh();
    }

    changeSection(code: string) {

        if (this.currentSection) {
            var tmpCurrentErrors = this.currentSection.errors;
            var isValid = this.currentSection.validate();

            if (isValid || code != this.currentSection.code && this.compareErrors(tmpCurrentErrors, this.currentSection.errors)) {
                this.props.registerAsyncOperation(this.props.context.saveDocumentForm());
                this.props.onChangeSection(code);
            }

            if (!isValid) {
                window.setTimeout(this.scrollTopOrFocusFirstErrElement, 500);
            }
        } else {
            this.props.registerAsyncOperation(this.props.context.saveDocumentForm());
            this.props.onChangeSection(code);
        }
    }

    delete() {
        let caseFileURI = this.props.context.caseFileUri;
        this.props.registerAsyncOperation(this.props.context.deleteDocumentProcess().then(() => {
            if (this.props.onDeleted) {
                this.props.onDeleted(caseFileURI);
            }
        }))
    }

    goToNextSection() {
        var nextSectionIdx = this.props.context.formManager.sections.indexOf(this.currentSection) + 1;

        if (nextSectionIdx < this.props.context.formManager.sections.length) {
            this.changeSection(this.props.context.formManager.sections[nextSectionIdx].code)
        }
        else {
            this.changeSection("preview");
        }
    }

    goToPreviewSection() {
        this.changeSection("preview");
    }

    startSigning() {
        this.props.registerAsyncOperation(this.props.context.startSigning())
    }

    startSending() {
        this.props.registerAsyncOperation(this.props.context.startSending().then(() => {
            this.refresh();
        }))
    }

    //#endregion

    //#region Helpers

    toggleShowMenu() {

        $('#PAGE-NAV').toggleClass('show');
        $('#show-menu').toggleClass('collapsed');

    }

    setCurrentSection(sectionCode?: string): Promise<void> {

        if (sectionCode == "preview" && this.props.context.formManager.sections) {
            this.props.context.formManager.validate();
            this.currentSection = null;

        } else if (sectionCode && this.props.context.formManager.sections) {
            this.currentSection = this.props.context.formManager.sections.filter(s => s.code == sectionCode)[0];

        } else {
            this.currentSection = this.props.context.formManager.sections ? this.props.context.formManager.sections[0] : null;
        }

        return Promise.resolve();
    }

    compareErrors(errs1: ErrorInfo[], errs2: ErrorInfo[]): boolean {
        if ((!errs1 || errs1.length == 0) &&
            (!errs2 || errs2.length == 0)) {
            return true;
        }

        if (!errs1 || errs1.length == 0) {
            return false;
        }

        if (!errs2 || errs2.length == 0) {
            return false;
        }

        if (errs1.length != errs2.length) {
            return false;
        }

        var flag: boolean = false;
        for (var i = 0; i < errs1.length; i++) {
            for (var j = 0; j < errs2.length; j++) {
                if (errs1[i].error == errs2[j].error && errs1[i].propertyName == errs2[j].propertyName) {
                    flag = true;
                    break;
                }
                else
                    flag = false;
            }
            if (!flag) {
                return false;
            }
        }

        return true;
    }

    saveDocumentPeriodically() {

        //Стартираме периодичното записване на черновата.
        this.saveDocTimeout = setTimeout(() => {

            this.props.registerAsyncOperation(this.props.context.saveDocumentForm().then(() => {
                this.saveDocumentPeriodically()
            }))

        }, appConfig.docSaveIntervalInMs);
    }

    initDocumentName() {
        this.props.registerAsyncOperation(
            Nomenclatures.getDocumentTypes(d => d.documentTypeID == this.props.context.getDocumentTypeId()).then((docs) => {
                if (this.props.context.formManager &&
                    this.props.context.formManager.documentForm &&
                    this.props.context.formManager.documentForm.documentTypeName &&
                    docs[0].uri != '0010-000002' &&
                    docs[0].uri != '0010-000001') {
                    this.documentName = this.props.context.formManager.documentForm.documentTypeName
                }
                else {
                    this.documentName = docs[0].name
                }
            }));
    }

    printDocument(data) {
        if (this.props.context.formManager.hasPrintPreview) {

            this.props.registerAsyncOperation(this.props.context.getDocumentHtml().then((html: string) => {
                var ua = window.navigator.userAgent;
                var isIE = /MSIE|Trident/.test(ua);

                if (isIE) {
                    var newWin = window.open("");
                    newWin.document.write(html);
                    newWin.document.execCommand('print', false, null);
                    newWin.focus();
                    newWin.close();
                } else {
                    var pri = (document.getElementById("ifmcontentstoprint") as HTMLIFrameElement).contentWindow;

                    pri.document.open();
                    pri.document.write(html);
                    pri.document.close();
                    pri.focus();

                    //Слагаме delay, защото когато има картинки, първия път когато отворим печатимата форма не се зареждат достатъчно бързо.
                    new Promise(resolve => setTimeout(resolve, 1)).then(
                        t => {

                            var result = pri.document.execCommand('print', false, null);

                            if (!result)
                                pri.print();

                            pri.document.clear();
                        })
                }
            }));
        }
    }

    downloadXmlDocument() {

        //IF this.props.context.formManager.mode == DocumentModes.ViewDocument

        var xmlDownloadUrl = this.props.context.getXmlDownloadUrl();

        var pom = document.createElement('a');
        pom.setAttribute('href', xmlDownloadUrl);
        pom.setAttribute('target', '_blank');

        document.body.appendChild(pom);

        pom.click();

        document.body.removeChild(pom);

        //this.props.registerAsyncOperation(this.props.context.getDocumentXml().then())
    }

    showMenuButton(): boolean {

        if (this.props.context.formManager.sections &&
            this.props.context.formManager.sections.length > 1 &&
            !(this.props.context.formManager.mode == DocumentModes.ViewDocument) &&
            this.props.context.status != ProcessStatuses.Sending &&
            this.props.context.status != ProcessStatuses.ReadyForSending &&
            this.props.context.status != ProcessStatuses.Accepted &&
            this.props.context.status != ProcessStatuses.Signing
        )
            return true;

        return false;
    }

    private hasClientErrorsOnSection() {
        if (this.props.context && this.props.context.formManager && this.props.context.formManager.sections && this.props.context.formManager.sections.length > 0) {
            for (var i = 0; i < this.props.context.formManager.sections.length; i++) {
                let currentSection = this.props.context.formManager.sections[i];

                if (currentSection.errors && currentSection.errors.length > 0)
                    return true;
            }
        }

        return false;
    }

    private newPreviewDocument() {
        this.props.context.clearContext();
    }

    private scrollTopOrFocusFirstErrElement(): void {
        let firstErrorUl = $("div.ui-form ul.invalid-feedback").first();

        if (firstErrorUl.length == 1) {
            //Има грешки и фокусира първия елемент с грешка.
            let firstElementForFocuse = firstErrorUl.prev(".input-error");

            if (firstElementForFocuse.length == 1)
                firstElementForFocuse.focus();
            else {
                //Когато първи с грешка е autocomplete контрола.
                firstErrorUl.prev("div.auto-complete-container").find(":input").focus();
            }
        } else {
            //Няма грешки и скролва до начало на страницата.
            this.scrollTopAndFocusTabContent();
        }
    }

    private scrollTopAndFocusTabContent(): void {
        let tabContent = document.querySelector('#ARTICLE-CONTENT');
        $(tabContent).attr('tabIndex', -1);
        // tabContent.scrollIntoView();
        window.scrollTo(0, 0);
        $(tabContent).focus();
    }

    //#endregion
}

export const DocumentProcessUI = withAsyncFrame(DocumentProcessUIImpl, false);
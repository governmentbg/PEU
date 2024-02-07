import { ObjectHelper, UrlHelper, UserInfo } from 'cnsys-core';
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, RawHTML, withAsyncFrame, withRouter } from "cnsys-ui-react";
import { Constants, eauAuthenticationService, EAUBaseComponent, Nomenclatures, Service, ValidationSummaryErrors } from "eau-core";
import { observable } from 'mobx';
import { observer } from "mobx-react";
import React from 'react';
import { Link } from 'react-router-dom';
import { Alert } from 'reactstrap';
import { ProcessStatuses } from '../models/ModelsManualAdded';
import { ApplicationProcessContext } from "../process-contexts/ApplicationProcessContext";
import { DocumentProcessUI } from "./DocumentProcessUI";

interface ApplicationProcessUIRouteParams extends BaseRouteParams {
    serviceID: number,
    sectionCode?: string
}

interface ApplicationProcessUIProps extends BaseRouteProps<ApplicationProcessUIRouteParams>, AsyncUIProps, BaseRoutePropsExt {
}

@observer class ApplicationProcessUIImpl extends EAUBaseComponent<ApplicationProcessUIProps, any> {
    processContext: ApplicationProcessContext;
    instructionURI: string;
    caseFileURI: string;
    withdrawService?: boolean;
    additionalApplicationURI: string;
    @observable service: Service
    user: UserInfo;

    constructor(props?: ApplicationProcessUIProps) {
        super(props);

        this.changeSection = this.changeSection.bind(this);
        this.startApplicationProcess = this.startApplicationProcess.bind(this);
        this.signIn = this.signIn.bind(this);
        this.delete = this.delete.bind(this);
        this.goToApplication = this.goToApplication.bind(this);
        this.onProcessDeleted = this.onProcessDeleted.bind(this);
        this.goToServiceName = this.goToServiceName.bind(this);
        this.startNewApplicationProcessInsteadOfAnother = this.startNewApplicationProcessInsteadOfAnother.bind(this);

        this.instructionURI = UrlHelper.getUrlParameter("instructionURI");
        this.caseFileURI = UrlHelper.getUrlParameter("caseFileURI");
        this.withdrawService = UrlHelper.getUrlParameter("withdrawService") === 'true';
        this.additionalApplicationURI = UrlHelper.getUrlParameter("additionalApplicationURI");

        Nomenclatures.getServices(s => s.serviceID == this.props.match.params.serviceID).bind(this).then(s => this.service = s[0]);

        this.props.registerAsyncOperation(eauAuthenticationService.getCurrentUser().bind(this).then(usr => {
            if (usr) {
                this.user = usr;
                this.processContext = new ApplicationProcessContext();

                return this.processContext.tryLoadApplicationProcess(this.props.match.params.serviceID,
                    this.instructionURI,
                    this.additionalApplicationURI,
                    this.caseFileURI,
                    this.withdrawService).then(isLoaded => {
                        if (!isLoaded) {
                            return this.processContext.createApplicationProcess(this.props.match.params.serviceID, this.instructionURI, this.additionalApplicationURI, this.caseFileURI, this.withdrawService);

                        } else {
                            if ((this.instructionURI || this.additionalApplicationURI) &&
                                (this.processContext.status == ProcessStatuses.NotRegistered || this.processContext.status == ProcessStatuses.Registered)) {
                                return this.processContext.deleteDocumentProcess().bind(this).then(() => {
                                    return this.processContext.createApplicationProcess(this.props.match.params.serviceID, this.instructionURI, this.additionalApplicationURI, this.caseFileURI, this.withdrawService);
                                })
                            }
                        }
                    });
            }
        }))
    }

    render() {
        if (this.processContext && this.processContext.isContextInitialized) {
            if (this.processContext.status == ProcessStatuses.Registered) {
                return (
                    <>
                        <div className="page-header" id="PAGE-CONTENT">
                            <div className="row">
                                <div className="col">
                                    <h1 className="page-title">{this.processContext.formManager.documentForm.documentTypeName}</h1>
                                </div>
                            </div>
                        </div>
                        <div className="page-wrapper" id="ARTICLE-CONTENT">
                            <div className="alert alert-warning">
                                <p>
                                    {
                                        this.getResource(!ObjectHelper.isStringNullOrEmpty(this.processContext.removingIrregularitiesInstructionURI)
                                            ? 'GL_00007_L'
                                            : !ObjectHelper.isStringNullOrEmpty(this.processContext.formManager.additionalData.additionalApplicationURI)
                                                ? 'GL_REGISTERED_DOCUMENT_L'
                                                : this.withdrawService ? "GL_REGISTERED_WITHDRAW_APPLICATION_E" : "GL_REGISTERED_APPLICATION_E")
                                    }
                                    <br />
                                    <button type="button" className="btn btn-link alert-link" onClick={() => this.goToApplication(true)}>{<b>{this.getResource("GL_URI_CASE_L") + " " + this.processContext.caseFileURI}</b>}</button>
                                </p>
                            </div>
                            <div className="button-bar button-bar--form button-bar--responsive">
                                <div className="right-side">
                                    <button type="button" className="btn btn-primary" onClick={() => this.goToApplication(true)}>{this.getResource("GL_GO_TO_THE_APPLICATION_E")}</button>
                                </div>
                                <div className="left-side">
                                    <button type="button" className="btn btn-secondary" onClick={this.delete}>{this.getResource("GL_SUBMIT_NEW_APPLICATION_L")}</button>
                                </div>
                            </div>
                        </div>
                    </>
                )
            } else if (this.processContext.status == ProcessStatuses.NotRegistered) {
                return (
                    <>
                        <div className="page-header" id="PAGE-CONTENT">
                            <div className="row">
                                <div className="col">
                                    <h1 className="page-title">{this.processContext.formManager.documentForm.documentTypeName}</h1>
                                </div>
                            </div>
                        </div>
                        <div className="page-wrapper" id="ARTICLE-CONTENT">
                            <div className="alert alert-danger" role="alert">
                                <p>{this.getResource("GL_NOT_REGISTERED_APPLICATION_E")}
                                    <Link to={Constants.PATHS.NotAcknowledgedMessage
                                        .replace(':notAcknowledgedMessageURI', this.processContext.notAcknowledgedMessageURI.toString())
                                        .replace(':documentProcessId', this.processContext.documentProcessID.toString())}
                                        className="alert-link">
                                        <b> „{this.getResource("DOC_GL_ReceiptNotAcknowledgedMessage_L")}“</b>
                                    </Link>.
                                </p>
                            </div>
                            <div className="button-bar button-bar--form button-bar--responsive">
                                <div className="right-side">
                                </div>
                                <div className="left-side">
                                    <button type="button" className="btn btn-secondary" onClick={this.delete}>{this.getResource("GL_SUBMIT_NEW_APPLICATION_L")}</button>
                                </div>
                            </div>
                        </div>
                    </>
                )
            } else {
                if (this.instructionURI && this.instructionURI != this.processContext.removingIrregularitiesInstructionURI) {
                    return (
                        <div>
                            {this.renderProcessExistsInfo(this.processContext.status)}
                            <div className="button-bar button-bar--service button-bar--responsive">
                                <div className="right-side">
                                </div>
                                <div className="left-side">
                                    {
                                        this.processContext.status != ProcessStatuses.Sending
                                            ? <button className="btn btn-primary" onClick={this.startNewApplicationProcessInsteadOfAnother}>{this.getResource("GL_SUBMIT_NEW_APPLICATION_L")}</button>
                                            : null
                                    }
                                </div>
                            </div>
                        </div>)

                } else if (this.additionalApplicationURI && this.additionalApplicationURI != this.processContext.formManager.additionalData.additionalApplicationURI) {
                    return (
                        <div>
                            {this.renderProcessExistsInfo(this.processContext.status)}
                            <div className="button-bar button-bar--service button-bar--responsive">
                                <div className="right-side">
                                </div>
                                <div className="left-side">
                                    <button className="btn btn-primary" onClick={this.startNewApplicationProcessInsteadOfAnother}>{this.getResource("GL_SUBMIT_NEW_APPLICATION_L")}</button>
                                </div>
                            </div>
                        </div>)
                } else if (this.caseFileURI && this.caseFileURI != this.processContext.formManager.additionalData.caseFileURI) {
                    return (
                        <div>
                            {this.renderProcessExistsInfo(this.processContext.status)}
                            <div className="button-bar button-bar--service button-bar--responsive">
                                <div className="right-side">
                                </div>
                                <div className="left-side">
                                    <button className="btn btn-primary" onClick={this.startNewApplicationProcessInsteadOfAnother}>{this.getResource("GL_SUBMIT_NEW_APPLICATION_L")}</button>
                                </div>
                            </div>
                        </div>)
                } else {
                    return <DocumentProcessUI context={this.processContext} currentSectionCode={this.props.match.params.sectionCode} onChangeSection={this.changeSection} onDeleted={this.onProcessDeleted} />
                }
            }
        }
        else {
            return this.renderStartUI();
        }
    }

    renderProcessExistsInfo(processStatus: ProcessStatuses) {

        switch (processStatus) {
            case ProcessStatuses.InProcess:
            case ProcessStatuses.Signing:
                return <Alert color="warning">
                    {this.getResource(!ObjectHelper.isStringNullOrEmpty(this.processContext.caseFileURI) ? "GL_00007_E" : "GL_00004_E")}
                    {
                        !ObjectHelper.isStringNullOrEmpty(this.processContext.caseFileURI)
                            ? <><br /><br /><button type="button" className="btn btn-link alert-link" onClick={() => this.goToApplication(false)}>{<b>{this.getResource("GL_URI_CASE_L") + " " + this.processContext.caseFileURI}</b>}</button></>
                            : null
                    }
                </Alert>
            case ProcessStatuses.Sending:
                return <Alert color="warning">{this.getResource("GL_00006_E")}</Alert>
            default: return ""
        }
    }

    renderStartUI() {
        if (this.service && this.service.isActive) {
            return (
                <>
                    <div className="page-header" id="PAGE-CONTENT">
                        <div className="row">
                            <div id="servicename" className="col">
                                <h1 className="page-title">{this.service.name ?? null}</h1>
                            </div>
                        </div>
                    </div>
                    <div className="page-wrapper" id="ARTICLE-CONTENT">
                        <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                        {this.user ?
                            <div className="button-bar button-bar--service button-bar--responsive">
                                <div className="right-side">
                                </div>
                                <div className="left-side">
                                    <button className="btn btn-primary" onClick={this.startApplicationProcess}>{this.getResource("GL_START_SERVICE_L")}</button>
                                </div>
                            </div> :
                            <div className="alert alert-info" role="alert">
                                {this.getResource("GL_ENTRANCE_IN_PORTAL_I")}
                                <button className="btn btn-link alert-link font-weight-bold" onClick={this.signIn}>"{this.getResource("GL_PORTAL_ENTRANCE_L")}"</button>
                            </div>}
                        <RawHTML rawHtmlText={this.service.description} />
                        {this.user &&
                            <div className="button-bar button-bar--service button-bar--responsive">
                                <div className="right-side">
                                </div>
                                <div className="left-side">
                                    <button className="btn btn-primary" onClick={this.startApplicationProcess}>{this.getResource("GL_START_SERVICE_L")}</button>
                                </div>
                            </div>}
                    </div>
                </>);
        } else {

            return <div className="page-wrapper" id="ARTICLE-CONTENT">
                <div className="row">
                    <div className="col-12">
                        <Alert color="warning" >{this.getResource("GL_PAGE_NOT_FOUND_L")}</Alert>
                    </div>
                </div>
            </div>
        }
    }

    goToServiceName() {
        document.getElementById("servicename").focus();
        document.getElementById('servicename').scrollIntoView()
    }

    startApplicationProcess() {
        this.goToServiceName();

        this.props.registerAsyncOperation(this.processContext.createApplicationProcess(this.props.match.params.serviceID, this.instructionURI, this.additionalApplicationURI, this.caseFileURI, this.withdrawService).then(() => {
            if (this.props.match.params.sectionCode) {
                this.changeSection(null);
            }
        }))
    }

    changeSection(code: string) {
        var path = Constants.PATHS.APPLICATION_PROCESSES.replace(':serviceID', this.props.match.params.serviceID.toString());

        if (code) {
            path = path.replace(":sectionCode?", code);
        } else {
            path = path.replace("/:sectionCode?", "");
        }

        var params: any = {};

        if (this.instructionURI) {
            params.instructionURI = this.instructionURI;
        }

        if (this.caseFileURI) {
            params.caseFileURI = this.caseFileURI;
        }

        if (this.additionalApplicationURI) {
            params.additionalApplicationURI = this.additionalApplicationURI;
        }

        if (this.withdrawService) {
            params.withdrawService = this.withdrawService;
        }

        this.props.routerExt.goTo(path, params);
    }

    signIn() {
        eauAuthenticationService.userLogin();
    }

    onProcessDeleted(caseFileURI: string) {
        if (!ObjectHelper.isStringNullOrEmpty(caseFileURI)) {
            this.props.routerExt.goTo(Constants.PATHS.ServiceInstance.replace(':caseFileURI', caseFileURI), null);
        } else {
            this.props.routerExt.goTo(Constants.PATHS.APPLICATION_PROCESSES_START.replace(':serviceID', this.service.serviceID.toString()).replace('/:sectionCode?', ''), null);
        }
        this.instructionURI = null;
        this.caseFileURI = null;
        this.additionalApplicationURI = null;
    }

    delete() {
        this.props.registerAsyncOperation(this.processContext.deleteDocumentProcess().bind(this).then(() => {
            this.onProcessDeleted(null)
        }))
    }

    startNewApplicationProcessInsteadOfAnother() {
        var that = this;

        this.processContext.deleteDocumentProcess().then(() => {
            that.startApplicationProcess()
        })
    }

    goToApplication(deleleApplication: boolean) {
        const applicationUrl = Constants.PATHS.ServiceInstance.replace(':caseFileURI', this.processContext.caseFileURI.toString());

        if (deleleApplication) {
            this.props.registerAsyncOperation(this.processContext.deleteDocumentProcess().bind(this).then(() => {
                this.props.routerExt.goTo(applicationUrl, null);
            }))
        } else {
            this.props.routerExt.goTo(applicationUrl, null);
        }
    }
}

export const ApplicationProcessUI = withAsyncFrame(withRouter(ApplicationProcessUIImpl), false);
import { UserInfo, ObjectHelper, UrlHelper } from 'cnsys-core';
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter, ConfirmationModal, RawHTML } from "cnsys-ui-react";
import { Constants, eauAuthenticationService, EAUBaseComponent, Nomenclatures, Service, Button, ValidationSummaryErrors } from "eau-core";
import { observable } from 'mobx';
import { observer } from "mobx-react";
import React from 'react';
import { Alert } from 'reactstrap';
import { ProcessStatuses } from '../models/ModelsManualAdded';
import { ApplicationProcessContext } from "../process-contexts/ApplicationProcessContext";

interface ApplicationProcessStartUIRouteParams extends BaseRouteParams {
    serviceID: number,
    sectionCode?: string
}

interface ApplicationProcessStartUIProps extends BaseRouteProps<ApplicationProcessStartUIRouteParams>, AsyncUIProps, BaseRoutePropsExt {
}

var deleteDocProcessMsgKeys = ['GL_REJECT_MSG_1_L', 'GL_REJECT_MSG_2_L'];

@observer class ApplicationProcessStartUIImpl extends EAUBaseComponent<ApplicationProcessStartUIProps, any> {
    processContext: ApplicationProcessContext;
    @observable service: Service
    user: UserInfo;
    @observable userHasDraftWork: boolean;
    instructionURI: string;
    caseFileURI: string;
    withdrawService?: boolean;
    additionalApplicationURI: string;
    @observable processCreatedOnFormatted: string;

    constructor(props?: ApplicationProcessStartUIProps) {
        super(props);

        this.continueProcess = this.continueProcess.bind(this);
        this.delete = this.delete.bind(this);
        this.goToServiceName = this.goToServiceName.bind(this);
        this.startApplicationProcess = this.startApplicationProcess.bind(this);
        this.goToApplication = this.goToApplication.bind(this);
        this.goToApplicationProcessPage = this.goToApplicationProcessPage.bind(this);
        this.goToApplicationProcessStartPage = this.goToApplicationProcessStartPage.bind(this);
        this.userHasDraftWork = false;

        this.instructionURI = UrlHelper.getUrlParameter("instructionURI");
        this.caseFileURI = UrlHelper.getUrlParameter("caseFileURI");
        this.additionalApplicationURI = UrlHelper.getUrlParameter("additionalApplicationURI");
        this.withdrawService = UrlHelper.getUrlParameter("withdrawService") === 'true';

        Nomenclatures.getServices(s => s.serviceID == this.props.match.params.serviceID).bind(this).then(s => this.service = s[0]);

        this.props.registerAsyncOperation(eauAuthenticationService.getCurrentUser().bind(this).then(usr => {
            if (usr) {
                this.user = usr;
                this.processContext = new ApplicationProcessContext();

                return this.processContext.tryLoadApplicationProcess(this.props.match.params.serviceID,
                    this.instructionURI,
                    this.additionalApplicationURI,
                    this.caseFileURI,
                    this.withdrawService).then(hasProcess => {
                    if (hasProcess) {
                        this.userHasDraftWork = true;
                        this.processCreatedOnFormatted = this.processContext.createdOn.format(Constants.DATE_FORMATS.dateTime).toString();
                    }
                });
            }
        }))
    }

    render() {
        if (this.service) {
            if (this.userHasDraftWork) {

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
                                                    : "GL_REGISTERED_APPLICATION_E")
                                        }
                                        <br />
                                        <button type="button" className="btn btn-link alert-link" onClick={this.goToApplication}>{<b>{this.getResource("GL_URI_CASE_L") + " " + this.processContext.caseFileURI}</b>}</button>
                                    </p>
                                </div>
                                <div className="button-bar button-bar--form button-bar--responsive">
                                    <div className="right-side">
                                        <button type="button" className="btn btn-primary" onClick={this.goToApplication}>{this.getResource("GL_GO_TO_THE_APPLICATION_E")}</button>
                                    </div>
                                    <div className="left-side">
                                        <button type="button" className="btn btn-secondary" onClick={this.delete}>{this.getResource("GL_SUBMIT_NEW_APPLICATION_L")}</button>
                                    </div>
                                </div>
                            </div>
                        </>
                    )
                }
                else if (this.processContext.status == ProcessStatuses.Sending || this.processContext.status == ProcessStatuses.Accepted)
                    return <Alert color="warning">{this.getResource("GL_00006_E")}</Alert>
                else {
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
                                <Alert color="warning">{this.getResource("GL_00030_I").replace("<дата и час>", this.processCreatedOnFormatted)}</Alert>
                                <div className="button-bar button-bar--form button-bar--responsive">
                                    <div className="right-side">
                                        <Button type="button" className="btn btn-primary" lableTextKey="GL_CONTINUE_L" onClick={this.continueProcess}></Button>
                                    </div>
                                    <div className="left-side">
                                        <ConfirmationModal modalTitleKey='GL_REJECT_APPLICATION_L' modalTextKeys={deleteDocProcessMsgKeys} onSuccess={this.delete} yesTextKey='GL_OK_L' noTextKey='GL_NO_L'>
                                            <Button type="button" className="btn btn-secondary" lableTextKey="GL_REJECT_L"></Button>
                                        </ConfirmationModal>
                                    </div>
                                </div>
                            </div>
                        </>);
                }
            }
            else {
                return this.renderStartUI();
            }
        }
        else
            return null;
    }

    renderStartUI() {
        if (this.service) {
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
        }
        else
            return null;
    }

    startApplicationProcess() {
        this.goToServiceName();
        this.props.registerAsyncOperation(this.processContext.createApplicationProcess(this.props.match.params.serviceID, this.instructionURI, this.additionalApplicationURI, this.caseFileURI)
            .bind(this).then(() => {
                this.goToApplicationProcessPage(this.props.match.params.sectionCode);
            }))
    }

    signIn() {
        eauAuthenticationService.userLogin();
    }

    goToServiceName() {
        document.getElementById("servicename").focus();
        document.getElementById('servicename').scrollIntoView()
    }

    continueProcess() {
        this.goToApplicationProcessPage(this.props.match.params.sectionCode);
    }

    delete() {
        this.props.registerAsyncOperation(this.processContext.deleteDocumentProcess().bind(this).then(() => {
            this.userHasDraftWork = false;
        }))
    }

    goToApplicationProcessPage(code: string) {
        var path = Constants.PATHS.APPLICATION_PROCESSES.replace(':serviceID', this.props.match.params.serviceID.toString());

        if (code) {
            path = path.replace(":sectionCode?", code);
        }
        else {
            path = path.replace("/:sectionCode?", "");
        }

        var params: any = {};

        if (this.instructionURI) {
            params.instructionURI = this.instructionURI;
        }

        if (this.caseFileURI) {
            params.caseFileURI = this.caseFileURI;
        }

        this.props.routerExt.goTo(path, params);
    }

    goToApplicationProcessStartPage(code: string) {
        var path = Constants.PATHS.APPLICATION_PROCESSES_START.replace(':serviceID', this.props.match.params.serviceID.toString());

        if (code) {
            path = path.replace(":sectionCode?", code);
        }
        else {
            path = path.replace("/:sectionCode?", "");
        }

        this.props.routerExt.goTo(path, null);
    }

    goToApplication() {
        let applicationUrl = Constants.PATHS.ServiceInstance.replace(':caseFileURI', this.processContext.caseFileURI.toString());

        this.props.registerAsyncOperation(this.processContext.deleteDocumentProcess().bind(this).then(() => {
            this.props.routerExt.goTo(applicationUrl, null);
        }))
    }
}

export const ApplicationProcessStartUI = withAsyncFrame(withRouter(ApplicationProcessStartUIImpl), false);
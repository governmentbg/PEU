import { ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, ConfirmationModal, withAsyncFrame, withRouter } from "cnsys-ui-react";
import { Button, EAUBaseComponent } from "eau-core";
import { DocumentProcessUI, ProcessStatuses } from 'eau-services-document-processes';
import { observer } from "mobx-react";
import React from 'react';
import { moduleContext } from '../';
import { DocumentModes } from '../../eau-documents';
import { Constants } from '../Constants';
import { DocumentProcessContext } from '../process-contexts/DocumentProcessContext';

interface EDocViewUIRouteParams extends BaseRouteParams {
    sectionCode?: string
    documentProcessID: string
}

interface EDocViewUIProps extends BaseRouteProps<EDocViewUIRouteParams>, AsyncUIProps, BaseRoutePropsExt {
}

interface EDocViewUIProps extends BaseProps, AsyncUIProps {
}

var deleteDocProcessMsgKeys = ['GL_REJECT_MSG_1_L', 'GL_REJECT_MSG_2_L'];

@observer class EDocViewUIImpl extends EAUBaseComponent<EDocViewUIProps, any> {
    requestMetadataUrl: string;
    requestID: string;
    processContext: DocumentProcessContext;
    deleteIsPending: boolean = false;

    constructor(props?: EDocViewUIProps) {
        super(props);
        this.changeSection = this.changeSection.bind(this);
        this.delete = this.delete.bind(this);
        this.returnToInProcessStatus = this.returnToInProcessStatus.bind(this);

        this.processContext = new DocumentProcessContext();

        this.props.registerAsyncOperation(this.processContext.tryLoadApplicationProcess(Number(this.props.match.params.documentProcessID)).bind(this));
    }

    render() {
        if (this.processContext && this.processContext.isContextInitialized) {
            if (this.processContext.status == ProcessStatuses.Accepted) {
                this.delete();
            } else if (this.processContext.status == ProcessStatuses.ErrorInAccepting) {
                return (
                    <div className='page-wrapper'>
                        <div className='alert alert-danger'>{moduleContext.resourceManager.getResourceByKey('GL_ERROR_PROCESSING_DOCUMENT_E')}{this.processContext.errorMessage ? (": " + this.processContext.errorMessage) : ""}</div>
                        <div className="button-bar button-bar--form button-bar--responsive">
                            <div className="right-side">
                                <Button type="button" className="btn btn-primary" lableTextKey="GL_BACK_TO_EDIT_SIGN_L" onClick={this.returnToInProcessStatus}></Button>
                            </div>
                            <div className="left-side">
                                <ConfirmationModal modalTitleKey='GL_REJECT_APPLICATION_L' modalTextKeys={deleteDocProcessMsgKeys} onSuccess={this.delete} yesTextKey='GL_OK_L' noTextKey='GL_NO_L'>
                                    <Button type="button" className="btn btn-secondary" lableTextKey="GL_REJECT_L"></Button>
                                </ConfirmationModal>
                            </div>
                        </div>
                    </div>);
            } else {
                return <DocumentProcessUI
                    context={this.processContext}
                    currentSectionCode={this.props.match.params.sectionCode}
                    onChangeSection={this.changeSection}
                    onDeleted={this.closeWindowAndRefreshParent}
                    showRejectButtonInsteadOfNewDocButton={(this.processContext.formManager.mode == DocumentModes.ViewDocument) && ObjectHelper.isStringNullOrEmpty(this.processContext.caseFileUri)}
                    skipAuthorizationForAccessToProcess={true}
                />;
            }
        }

        return null
    }

    changeSection(code: string) {
        var path = Constants.PATHS.DOCUMENT_PROCESSES;

        if (code) {
            path = path.replace(":sectionCode?", code).replace(":documentProcessID", this.processContext.documentProcessID.toString()) + '/' + window.location.search;
        }
        else {
            path = path.replace("/:sectionCode?", "").replace(":documentProcessID", this.processContext.documentProcessID.toString()) + window.location.search;
        }

        this.props.routerExt.goTo(path, {});
    }

    delete() {
        if (this.deleteIsPending == false) {
            this.deleteIsPending = true;
            this.props.registerAsyncOperation(this.processContext.deleteDocumentProcess().then(() => {
                this.deleteIsPending = false;
                this.closeWindowAndRefreshParent();
            }));
        }
    }

    closeWindowAndRefreshParent(): void {
        try {
            if (window.opener != null && typeof (window.opener.DoPageRefresh) !== 'undefined') {
                window.opener.DoPageRefresh();
            }
        } catch (exception) {
        } finally {
            window.close();
        }
    }

    returnToInProcessStatus(): void {
        this.props.registerAsyncOperation(this.processContext.returnToInProcessStatus());
    }
}

export const EDocViewUI = withAsyncFrame(withRouter(EDocViewUIImpl), true);
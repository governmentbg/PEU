import { UrlHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import { observer } from "mobx-react";
import React from 'react';
import { Redirect } from 'react-router-dom';
import { Constants } from '../Constants';
import { DocumentProcessContext } from '../process-contexts/DocumentProcessContext';

interface EDocLandingPageUIPropsRouteParams extends BaseRouteParams {
    sectionCode?: string
}

interface EDocLandingPageUIProps extends BaseRouteProps<EDocLandingPageUIPropsRouteParams>, AsyncUIProps, BaseRoutePropsExt {
}

interface EDocLandingPageUIProps extends BaseProps, AsyncUIProps {
}

@observer class EDocLandingPageUIImpl extends EAUBaseComponent<EDocLandingPageUIProps, any> {
    requestMetadataUrl: string;
    requestID: string;
    processContext: DocumentProcessContext;
    deleteIsPending: boolean = false;

    constructor(props?: EDocLandingPageUIProps) {
        super(props);

        this.requestMetadataUrl = decodeURIComponent(window.location.search.substring(window.location.search.indexOf('requestMetadataUrl=') + 'requestMetadataUrl='.length));
        this.requestID = UrlHelper.getUrlParameter("requestID");
        this.processContext = new DocumentProcessContext();

        this.props.registerAsyncOperation(this.processContext.createApplicationProcess(this.requestID, this.requestMetadataUrl));
    }

    render() {
        return (
            <>
                {
                    this.processContext.isContextInitialized &&
                    <Redirect to={Constants.PATHS.DOCUMENT_PROCESSES_BASE + this.processContext.documentProcessID} />
                }
            </>)
    }
}

export const EDocLandingPageUI = withAsyncFrame(withRouter(EDocLandingPageUIImpl), true);
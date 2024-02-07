import React from 'react';
import { ProcessStatuses } from '../models/ModelsManualAdded';
import { moduleContext } from '../ModuleContext';

interface MenuUIProps {
    processStatus: ProcessStatuses;
}

export function SendingUI(props: MenuUIProps): JSX.Element {
    var styleCSS: any;
    var valuenow: number;

    if (props.processStatus == ProcessStatuses.ReadyForSending ||
        props.processStatus == ProcessStatuses.Sending) {
        styleCSS = { width: "30%" };
        valuenow = 30;
    }
    else {
        styleCSS = { width: "60%" }
        valuenow = 60;
    }

    return (
        <div className="page-wrapper" id="ARTICLE-CONTENT">
            <h3 className="form-control-label ml-2">{moduleContext.resourceManager.getResourceByKey("GL_REGISTER_APPLICATION_L")}</h3>
            <div className="progress progress--form">
                <div className="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" aria-valuenow={valuenow} aria-valuemin={0} aria-valuemax={100} style={styleCSS}></div>
            </div>
        </div>
    );
}
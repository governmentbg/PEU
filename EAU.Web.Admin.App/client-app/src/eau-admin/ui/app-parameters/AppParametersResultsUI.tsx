import { BindableReference } from 'cnsys-core';
import { BaseProps } from 'cnsys-ui-react';
import { AppParameter, resourceManager } from 'eau-core';
import React from 'react';
import AppParameterResultUI from './AppParameterResultUI';

interface AppParametersResultsProps extends BaseProps {
    appParameters: AppParameter[];
}

const AppParametersResultsUI: React.FC<AppParametersResultsProps> = ({ appParameters }) => {
    return <div className="table-responsive">
        <table className="table table-bordered table-striped table-hover">
            <thead>
                <tr>
                    <th>{resourceManager.getResourceByKey("GL_MODULE_FUNCTIONALITY_L")}</th>
                    <th className="w-30">{resourceManager.getResourceByKey("GL_PARAM_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_TYPE_L")}</th>
                    <th className="w-30">{resourceManager.getResourceByKey("GL_VALUE_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_SYS_PARAM_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_ACTIONS_L")}</th>
                </tr>
            </thead>
            <tbody>
                {
                    appParameters.map((appParameter, index) => <tr key={appParameter.appParamID + "_" + index}>
                        <AppParameterResultUI modelReference={new BindableReference(appParameter)} />
                    </tr>)
                }
            </tbody>
        </table>
    </div>
}

export default AppParametersResultsUI;
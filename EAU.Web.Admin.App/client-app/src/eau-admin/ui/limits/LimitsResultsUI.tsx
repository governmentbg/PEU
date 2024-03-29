﻿import { BindableReference } from 'cnsys-core';
import { BaseProps } from 'cnsys-ui-react';
import { resourceManager } from 'eau-core';
import React from 'react';
import { DataServiceLimit } from '../../models/ModelsAutoGenerated';
import LimitResultUI from './LimitResultUI';

interface LimitsResultsProps extends BaseProps {
    limits: DataServiceLimit[],
    onChangeStatus,
    onSave,
    onCancelChanges
}

const LimitsResultsUI: React.FC<LimitsResultsProps> = ({ limits, onChangeStatus, onSave, onCancelChanges }) => {
    return <div className="table-responsive">
        <table className="table table-bordered table-striped table-hover">
            <thead>
                <tr>
                    <th>{resourceManager.getResourceByKey("GL_LIMIT_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_PERIOD_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_NOM_MAX_REQUESTS_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_STATUS_L")}</th>
                    <th>{resourceManager.getResourceByKey("GL_ACTIONS_L")}</th>
                </tr>
            </thead>
            <tbody>
                {
                    limits.map((limit, index) => <tr key={limit.serviceLimitID + "_" + index}>
                        <LimitResultUI modelReference={new BindableReference(limit)} onChangeStatus={onChangeStatus} onSave={onSave} onCancelChanges={onCancelChanges}/>
                    </tr>)
                }
            </tbody>
        </table>
    </div>
}

export default LimitsResultsUI;
import { ObjectHelper } from "cnsys-core";
import { AsyncUIProps, BaseProps, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { Constants, EAUBaseComponent, ServiceInstanceStatuses } from 'eau-core';
import { observer } from 'mobx-react';
import React from 'react';
import { ServiceInstanceInfo } from '../../models/ModelsManualAdded';

interface MyEServiceInstancePreviousStagesUIProps extends BaseProps, AsyncUIProps {
    eServicesInstanceInfo: ServiceInstanceInfo;
    eServicesInstanceStatus: number;
}
 
@observer class MyEServiceInstancePreviousStagesUI extends EAUBaseComponent<MyEServiceInstancePreviousStagesUIProps, ServiceInstanceInfo>{
    
    render() {
       
        let dataResultRow: any = null;

        if (!ObjectHelper.isNullOrUndefined(this.props.eServicesInstanceInfo)) {

            dataResultRow = <>

                {  this.props.eServicesInstanceInfo.executedStages.length > 0 ? 

                    this.props.eServicesInstanceInfo.executedStages.map((executedStage, indexExecutedStage) => 
                        
                        <li className="stage-list-item" key={indexExecutedStage}>

                            <div className={`stage-status ${(indexExecutedStage+1) == this.props.eServicesInstanceInfo.executedStages.length ? "first" : ''}`}>
                            <div className={`item-status ${this.props.eServicesInstanceStatus == ServiceInstanceStatuses.Rejected && indexExecutedStage == 0 ? "danger" : 'success'}`} title={this.props.eServicesInstanceStatus == ServiceInstanceStatuses.Rejected && indexExecutedStage == 0 ? this.getResource('GL_SERVICE_INSTANCE_UNEXECUTED_STAGE_L') : this.getResource('GL_SERVICE_INSTANCE_EXECUTED_STAGE_L')}>
                                    <div className="item-status-content">                                            
                                    <i className={`ui-icon ui-icon-${this.props.eServicesInstanceStatus == ServiceInstanceStatuses.Rejected && indexExecutedStage == 0 ? "minus" : 'check'}`} aria-hidden="true"></i>
                                    </div>
                                </div>
                            </div>

                            <div className="stage-info arrow arrow-left">
                                <div className="stage-info-date">{!ObjectHelper.isNullOrUndefined(executedStage.actualCompletionDate) ? executedStage.actualCompletionDate.format(Constants.DATE_FORMATS.date) : ''}</div>        
                                <div className="stage-info-title">{executedStage.name}</div>                                        
                            </div>

                        </li>
                    )
                    : null
                }
            </>
        }
       
        return <>
          {dataResultRow}
        </>
    }
}

export default withRouter(withAsyncFrame(MyEServiceInstancePreviousStagesUI, false));
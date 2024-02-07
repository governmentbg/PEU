import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { Constants, EAUBaseComponent, resourceManager, ServiceInstanceStatuses } from "eau-core";
import { observer } from "mobx-react";
import * as React from "react";
import { Link } from 'react-router-dom';
import { ServiceInstance } from "../../models/ModelsManualAdded";

interface EServicesRowUIProps extends BaseProps, AsyncUIProps {
    updateEServiceFunc
}

@observer
class EServicesRowUIImpl extends EAUBaseComponent<EServicesRowUIProps, ServiceInstance> {

    constructor(props: EServicesRowUIProps) {
        super(props);

        //Init
        this.getResourceByStatus = this.getResourceByStatus.bind(this);
        this.getResourceBySubStatus = this.getResourceBySubStatus.bind(this);

    }

    render() {
        return (
            <>
                <tr>
                    <th>
                        <p className="td-text"><b>{this.model.service}</b></p>
                    </th>
                    <td>
                        <p className="td-text">{this.model.caseFileURI}</p>
                    </td>
                    <td>
                        <p className="td-text text-nowrap">{this.dateDisplayFor(this.model.serviceInstanceDate)}</p>
                    </td>
                    <td>
                        <p className="td-text text-nowrap">
                            {(this.model.status == ServiceInstanceStatuses.InProcess) ?
                                <i className="ui-icon ui-icon-state-waiting mr-1" aria-hidden="true"></i>
                                : (this.model.status == ServiceInstanceStatuses.Completed) ?
                                    <i className="ui-icon ui-icon-state-active mr-1" aria-hidden="true"></i> :
                                    <i className="ui-icon ui-icon-state-canceled mr-1" aria-hidden="true"></i>
                            }
                            {this.getResourceByStatus(this.model.status)}
                        </p>

                        {(this.model.additionalData["subStatus"]) && this.model.status != ServiceInstanceStatuses.Rejected ?
                            <p className="td-text">{this.getResourceBySubStatus(this.model.additionalData["subStatus"])}</p> : null}
                    </td>
                    <td className="actions-td">
                        <Link to={Constants.PATHS.ServiceInstance.replace(':caseFileURI', this.model.caseFileURI.toString())} title={resourceManager.getResourceByKey("GL_PAGE_OPEN_IN_NEW_TAB_L")} target="_blank" >
                            {this.getResource("GL_PREVIEW_L")}
                        </Link>
                    </td>
                </tr>
            </>
        );
    }

    private getResourceByStatus(status: ServiceInstanceStatuses): string {
        switch (status) {
            case ServiceInstanceStatuses.Completed:
                return this.getResource("GL_ServiceInstanceStatuses_Completed_L");
            case ServiceInstanceStatuses.InProcess:
                return this.getResource("GL_ServiceInstanceStatuses_InProcess_L");
            case ServiceInstanceStatuses.Rejected:
                return this.getResource("GL_ServiceInstanceStatuses_Rejected_L");
            default:
                return "";
        }
    }

    private getResourceBySubStatus(subStatus: string): string {
        switch (subStatus) {
            case "WaitCorrectionsApplication":
                return this.getResource("GL_ServiceInstanceStatus_WaitCorrectionsApplication_L");
            case "WaitPayment":
                return this.getResource("GL_ServiceInstanceStatus_WaitPayment_L");
            case "NotCompleted":
                return this.getResource("GL_ServiceInstanceStatus_NotCompleted_L");
            case "WaitingResponse":
                return this.getResource("GL_ServiceInstanceStatus_WaitingResponse_L");
            case "Cancelled":
            case "CancelIssuingAdministrativeAct":
                return this.getResource("GL_REFUSAL_PROVIDE_SERVICE_L");
            case "Termination":
                return this.getResource("GL_EXPIRED_APPLICATION_DEADLINE_L");
            case "OutstandingConditions":
                return this.getResource("GL_UNFULFILLED_CONDITIONS_L");
            default:
                return "";
        }
    }
}

export const MyEServicesSearchResultRowUI = withAsyncFrame(EServicesRowUIImpl);
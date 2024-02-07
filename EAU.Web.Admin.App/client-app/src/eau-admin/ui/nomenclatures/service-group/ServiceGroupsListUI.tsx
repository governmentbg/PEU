import { AsyncUIProps, BaseProps, ConfirmationModal, withAsyncFrame } from 'cnsys-ui-react';
import { EAUBaseComponent , Constants as ConstantsEAU} from 'eau-core';
import { observer } from 'mobx-react';
import React from 'react';
import { Link } from 'react-router-dom';
import { Constants } from '../../../Constants';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';

interface ServiceGroupsProps extends BaseProps, AsyncUIProps {
    onChangeStatusCallback: any
}

@observer 
class ServiceGroupsListUI extends EAUBaseComponent<ServiceGroupsProps, any>{

    private nomenclatureDataService: NomenclaturesDataService;

    constructor(props: ServiceGroupsProps) {
        super(props);
        this.nomenclatureDataService = new NomenclaturesDataService();
    }

    render() {
        return <>
            <td>
                {this.model.orderNumber}
            </td>
            <td>{this.model.name}</td>
            <td>
                <i className={`ui-icon ui-icon-state-${this.model.isActive ? "active" : "inactive"}`} aria-hidden="true"></i>
                {this.model.isActive ? this.getResource("GL_ACTIVE_L") : this.getResource("GL_INACTIVE_L")}
            </td>

            <td>{this.model.iconName}</td>

            <td>{this.model.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime)}</td>
            <td className="buttons-td">
                <Link to={Constants.PATHS.NomEditServiceGroup.replace(':serviceGroupID', this.model.groupID.toString())}>
                    <button className="btn btn-secondary" title={this.getResource("GL_EDIT_L")}>
                        <i className="ui-icon ui-icon-edit"></i>
                    </button>
                </Link>

                <ConfirmationModal 
                    modalTitleKey={this.model.isActive ? "GL_DEACTIVE_CONFIRM_I" : "GL_ACTIVE_CONFIRM_I"}  
                    modalTextKeys={[this.model.isActive ? "GL_DEACTIVE_CONFIRM_I" : "GL_ACTIVE_CONFIRM_I"]}  
                    noTextKey="GL_NO_L" 
                    yesTextKey="GL_YES_L" 
                    onSuccess={() => this.props.onChangeStatusCallback(this.model)}>
                    <button className="btn btn-secondary" title={this.model.isActive ? this.getResource("GL_DEACTIVATION_L") : this.getResource("GL_ACTIVATION_L")}>
                        <i className={this.model.isActive ? "ui-icon ui-icon-deactivate" : "ui-icon ui-icon-activate"}  aria-hidden="true"></i>
                    </button>
                </ConfirmationModal>
            </td>    
        </>
    }
}

export default withAsyncFrame(ServiceGroupsListUI, false);
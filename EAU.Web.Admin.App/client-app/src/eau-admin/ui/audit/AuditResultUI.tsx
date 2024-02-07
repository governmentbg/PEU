import { Constants, EAUBaseComponent } from 'eau-core';
import { observer } from 'mobx-react';
import React from 'react';
import { AsyncUIProps, BaseProps } from 'cnsys-ui-react';
import { LogAction } from '../../models/LogAction';
import JsonToList from './JsonToList';
import { LogActionSearchCriteria } from 'eau-admin/models/LogActionSearchCriteria';
import { observable } from 'mobx';
import { ModalTypes } from './AuditSearchUI';

interface AuditResultProps extends BaseProps, AsyncUIProps {
	audit: LogAction,
	onOpenModalCallback: (event, type, data) => any,
	actionTypesDescriptionValue: any,
	objectTypesDescriptionValue: any
}


@observer class AuditResultUI extends EAUBaseComponent<AuditResultProps, LogActionSearchCriteria> {
	
	@observable private modal: boolean;
	
	constructor(props: AuditResultProps) {
		super(props);
	}
	
	render() {

		let test = <JsonToList json={this.props.audit.additionalData}/>;

		return (
				<>
				<td>{this.props.audit.logActionDate.format(Constants.DATE_FORMATS.dateTime).toString()}</td>
				<td>{this.props.objectTypesDescriptionValue[this.props.audit.objectType]}</td>
				<td>{this.props.audit.key}</td>
				<td>{this.props.actionTypesDescriptionValue[this.props.audit.actionType]}</td>
				<td><a  href="#" title="Преглед" onClick={(еvent) => this.props.onOpenModalCallback(еvent, ModalTypes.userPreview, this.props.audit.userID)}>{this.props.audit.userEmail}</a> </td>
				<td>{this.props.audit.ipAddress}</td>
				<td><a href="#" title="Преглед" onClick={(еvent) => this.props.onOpenModalCallback(еvent, ModalTypes.loginSessionPreview, this.props.audit.loginSessionID)}>{this.props.audit.loginSessionID}</a></td>
				<td className="buttons-td"> 
				<button className="btn btn-secondary" title="Преглед" onClick={(еvent) => this.props.onOpenModalCallback(еvent, ModalTypes.additionalDataPreview, this.props.audit.additionalData)}>
					<i className="ui-icon ui-icon-eye"></i> 
				</button>

				</td>
				</>
				) 
			}
	
}

export default AuditResultUI;
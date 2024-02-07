import React from 'react';
import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { EAUBaseComponent } from 'eau-core';
import { observable } from 'mobx';
import { UsersDataService } from '../../services/UsersDataService';
import { observer } from 'mobx-react';
import JsonToList from './JsonToList';

interface LoginSessionProps extends BaseProps, AsyncUIProps {
	loginSessionId: string
}

@observer class LoginSessionPreviewImpl extends EAUBaseComponent<LoginSessionProps, null> {

	@observable loginSessionDetails;
	@observable isLoaded;

	constructor(props: LoginSessionProps) {
        super(props);

		this.init = this.init.bind(this);
        this.init();
	}
	
	init() {

		let dataManager = new UsersDataService;
		
		this.props.registerAsyncOperation(
			dataManager.getLoginSession(this.props.loginSessionId)
			.then(result => {
				this.loginSessionDetails = result;
				this.isLoaded = true;
			})
		);
	}

	render() {

		let result = null;

		if (this.isLoaded)
			result = <JsonToList json={this.loginSessionDetails} />

		return <>	
				{result}
			</>
		}
	}

export const LoginSessionPreview  = withAsyncFrame(LoginSessionPreviewImpl, false);
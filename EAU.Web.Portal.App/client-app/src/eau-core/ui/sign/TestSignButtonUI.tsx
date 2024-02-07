import * as React from 'react';
import { EAUBaseComponent } from '../EAUBaseComponent';
import { BaseProps } from 'cnsys-ui-react';
import { appConfig } from 'eau-core';

export interface TestSignButtonUIProps extends BaseProps {   
    onTestSign: () => void;
}

export class TestSignButtonUI extends EAUBaseComponent<TestSignButtonUIProps, any> {
    constructor(props: TestSignButtonUIProps) {
        super(props);
    }

    render(): JSX.Element {
        if (appConfig.allowTestSign === true) {
            return <button type="button" className="btn btn-primary" onClick={this.props.onTestSign}>{this.getResource('GL_TEST_SIGNATURE_L')}</button>
        }

        return null;
    }
}
import { ObjectHelper, UIHelper } from "cnsys-core";
import { observer } from "mobx-react";
import * as React from "react";
import { Progress } from "reactstrap";
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface PasswordStrengthMeterProps extends BaseFieldProps {
    password: string;
    helpText?: string;
}

@observer export class PasswordStrengthMeter extends BaseFieldComponent<PasswordStrengthMeterProps> {

    private STRENGTH_WIDTHS = [5, 20, 40, 60, 80, 100];
    private STRENGTH_COLORS = ['danger', 'danger', 'danger', 'warning', 'success', 'success'];

    private currentWidth;
    private currentStrengthColor;

    constructor(props: PasswordStrengthMeterProps) {
        super(props)

        this.currentWidth = 0;
        this.currentStrengthColor = this.STRENGTH_COLORS[0];
    }

    componentWillReceiveProps(nextProps: PasswordStrengthMeterProps, nextContext: any) {
        let passwordStrengthScore = UIHelper.calcPasswordStrength(nextProps.password);
        this.currentWidth = ObjectHelper.isStringNullOrEmpty(nextProps.password) ? 0 : this.STRENGTH_WIDTHS[passwordStrengthScore];
        this.currentStrengthColor = this.STRENGTH_COLORS[passwordStrengthScore];
    }

    render() {
        return <><Progress className="progress--password" color={this.currentStrengthColor} value={this.currentWidth} />
            {!ObjectHelper.isStringNullOrEmpty(this.props.helpText) && <div className="help-text-inline" id={`${this.getId()}_HELP`}>{this.props.helpText}</div>}
        </>
    }

    renderInternal(): JSX.Element {
        throw "Not Implemented";
    }
}
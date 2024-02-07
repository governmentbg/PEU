import { ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { AppParameter, AppParameterTypes, EAUBaseComponent, Functionalities, Constants, ResourceHelpers } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import moment from 'moment';
import React from 'react';
import { AppParametersDataService } from '../../services/AppParametersDataService';
import { AppParameterResultValidator } from '../../validations/AppParametersValidator';

interface AppParameterResultProps extends BaseProps, AsyncUIProps {
}

@observer class AppParameterResultUI extends EAUBaseComponent<AppParameterResultProps, AppParameter>{

    private appParametersService: AppParametersDataService;
    private initialAppParameter: AppParameter;

    @observable private editMode: boolean;

    constructor(props: AppParameterResultProps) {
        super(props);

        this.init();
        this.funcBinds();
    }

    render() {

        return <>
            <td>{this.showFunctionalityModule()}</td>
            <td>
                <span className="word-break"><b>{this.model.code}</b></span><br />
                {this.model.description}</td>
            <td>{this.showParamType()}</td>
            <td className="buttons-tdA">{this.editMode ? this.editValue() : this.showValue()}</td>
            {
                this.model.isSystem === true
                    ? <td className="icons-td"><i className="ui-icon ui-icon-state-active" aria-hidden="true"></i>{this.getResource("GL_YES_L")}</td>
                    : <td className="icons-td"><i className="ui-icon ui-icon-state-inactive" aria-hidden="true"></i>{this.getResource("GL_NO_L")}</td>
            }
            <td className="buttons-td">
                {
                    this.editMode
                        ? <span className="btn-group">
                            <button type="button" className="btn btn-secondary" title={this.getResource("GL_CONFIRM_L")} onClick={this.saveChanges}>
                                <i className="ui-icon ui-icon-check"></i>
                            </button>
                            <button type="button" className="btn btn-secondary" title={this.getResource("GL_CANCEL_L")} onClick={this.cancelChanges}>
                                <i className="ui-icon ui-icon-ban"></i>
                            </button>
                        </span>
                        : <span className="preview">
                            <button type="button" className="btn btn-secondary" title={this.getResource("GL_EDIT_L")} onClick={this.markForEdit}>
                                <i className="ui-icon ui-icon-edit"></i>
                            </button>
                        </span>
                }
            </td>
        </>
    }

    private saveChanges() {

        if (this.validators[0].validate(this.model)) {
            this.props.registerAsyncOperation(this.appParametersService.editAppParameter(this.model).then(() => {
                this.editMode = !this.editMode;
            }));
        }
    }

    @action private cancelChanges() {

        this.model = this.initialAppParameter;
        this.editMode = !this.editMode;
    }

    private markForEdit() {

        this.editMode = !this.editMode;
    }

    private editValue() {

        switch (this.model.parameterType) {
            case AppParameterTypes.Integer: return this.textBoxFor(x => x.valueInt)
            case AppParameterTypes.String: return this.textAreaFor(x => x.valueString)
            case AppParameterTypes.Interval: return this.durationFor(x => x.valueInterval)
            case AppParameterTypes.DateTime: return <div className="form-row">
                <div className="form-group col-7 date-control">
                    {this.dateFor(x => x.valueDateTime, null, null, null, null, null, true)}
                </div>
                <div className="form-group col-5 time-control">
                    {this.timeFor(x => x.valueDateTime, null, null, Constants.DATE_FORMATS.TimeHms)}
                </div>
            </div>
            case AppParameterTypes.HourAndMinute: return this.textBoxFor(x => x.valueHour)

            default: return "Unsupported parameterType"
        }
    }

    private showFunctionalityModule() {
        var functionalityStr = Functionalities[this.model.functionalityID];

        if (!ObjectHelper.isNullOrUndefined(this.model.functionalityID) && !ObjectHelper.isStringNullOrEmpty(functionalityStr))
            return ResourceHelpers.getResourceByEmun(this.model.functionalityID, Functionalities)

        return "";
    }

    private showParamType() {

        switch (this.model.parameterType) {
            case AppParameterTypes.Integer: return this.getResource("GL_INTEGER_L");
            case AppParameterTypes.String: return this.getResource("GL_STRING_L");
            case AppParameterTypes.Interval: return this.getResource("GL_TIME_INTERVAL_L");
            case AppParameterTypes.DateTime: return this.getResource("GL_DATE_HOURS_MINUTES_L");
            case AppParameterTypes.HourAndMinute: return this.getResource("GL_HOURS_MINUTES_L");

            default: return "Unsupported parameterType"
        }
    }

    private showValue() {

        switch (this.model.parameterType) {
            case AppParameterTypes.Integer: return this.model.valueInt;
            case AppParameterTypes.String: return this.model.valueString;
            case AppParameterTypes.Interval: return this.durationDisplayFor(moment.duration(this.model.valueInterval), false);
            case AppParameterTypes.DateTime: return this.dateDisplayFor(this.model.valueDateTime, Constants.DATE_FORMATS.dateTime);
            case AppParameterTypes.HourAndMinute: return this.model.valueHour;

            default: return "Unsupported parameterType"
        }
    }

    //#region main functions

    private funcBinds() {

        this.markForEdit = this.markForEdit.bind(this);
        this.saveChanges = this.saveChanges.bind(this);
        this.cancelChanges = this.cancelChanges.bind(this);
    }

    @action private init() {

        this.appParametersService = new AppParametersDataService();
        this.validators = [new AppParameterResultValidator()];
        this.initialAppParameter = JSON.parse(JSON.stringify(this.model));
    }

    //#endregion
}

export default withAsyncFrame(AppParameterResultUI, false);
import { ObjectHelper } from 'cnsys-core';
import { BaseProps } from 'cnsys-ui-react';
import { classNameMr2ColFormLabel, EAUBaseComponent, resourceManager, UserSearchCriteria, UserStatuses } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { InternalUsersSearchValidator } from '../../../validations/InternalUsersValidator';

interface AppParametersSearchProps extends BaseProps {
    onSearchCallback: () => void;
    onClearCallback: () => void;
    previewMode: boolean
}

@observer class UserProfileSearchUI extends EAUBaseComponent<AppParametersSearchProps, UserSearchCriteria>{

    @observable searchByStatus: "all" | "active" | "inactive";

    private groupName: string;

    constructor(props: AppParametersSearchProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    render() {
        let serchUserByEmail = !this.props.previewMode ? null : <div className="form-group  col-sm-6 col-xl-3 col-lg-5 col-md-4">
            {this.labelFor(x => x.email, "GL_EMAIL_L")}
            {this.textBoxFor(x => x.email)}
        </div>
        return <>
            <div className="card-header">
                <h3>{this.getResource("GL_SEARCH_USER_PROFILE_L")}</h3>
            </div>

            <div className="card-body">
                <div className="row">
                    <div className="col-xl-6 col-lg-7 col-md-8">
                        <label htmlFor="PERIOD-LAST-REGISTRATION-CHANGE">{resourceManager.getResourceByKey("GL_PERIOD_LAST_REGISTRATION_CHANGE_L")}</label>
                        <div className="row">
                            <div className="form-group col-sm-6">
                                <div className="d-flex">
                                    {this.labelFor(x => x.dateFrom, "GL_FROM_L", classNameMr2ColFormLabel)}
                                    {this.dateFor(x => x.dateFrom, null, null, null, null, null, true)}
                                </div>
                            </div>
                            <div className="form-group col-sm-6">
                                <div className="d-flex">
                                    {this.labelFor(x => x.dateTo, "GL_TO_L", classNameMr2ColFormLabel)}
                                    {this.dateFor(x => x.dateTo, null, null, null, null, null, true)}
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="form-group  col-sm-6 col-xl-3 col-lg-5 col-md-4">
                        {this.labelFor(x => x.username, "GL_USERNAME_L")}
                        {this.textBoxFor(x => x.username)}
                    </div>
                    {serchUserByEmail}
                    <div className="form-group col-xl-3 col-lg-5  col-md-6">
                        <label htmlFor="STATUS">{this.getResource('GL_STATUS_L')}</label>
                        <div className="form-inline">
                            <div className="custom-control-inline custom-control custom-radio">
                                <input className="custom-control-input" type="radio" onChange={this.handleRadioButtonListChange} name={this.groupName}
                                    id={this.groupName + '_all'} value={'all'} checked={this.searchByStatus === "all"} />
                                <label className="custom-control-label" htmlFor={this.groupName + '_all'}>{this.getResource("GL_ALL_L")}</label>
                            </div>
                            <div className="custom-control-inline custom-control custom-radio">
                                <input className="custom-control-input" type="radio" onChange={this.handleRadioButtonListChange} name={this.groupName}
                                    id={this.groupName + '_active'} value={'active'} checked={this.searchByStatus === "active"} />
                                <label className="custom-control-label" htmlFor={this.groupName + '_active'}>{this.getResource("GL_ACTIVE_L")}</label>
                            </div>
                            <div className="custom-control-inline custom-control custom-radio">
                                <input className="custom-control-input" type="radio" onChange={this.handleRadioButtonListChange} name={this.groupName}
                                    id={this.groupName + '_inactive'} value={'inactive'} checked={this.searchByStatus === "inactive"} />
                                <label className="custom-control-label" htmlFor={this.groupName + '_inactive'}>{this.getResource("GL_INACTIVE_L")}</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </>
    }

    //#region handlers

    @action private handleRadioButtonListChange(e: any) {

        this.searchByStatus = e.target.value;

        if (e.target.value == 'active') {
            this.model.userStatuses = [UserStatuses.Active];
        } else if (e.target.value == 'inactive') {
            this.model.userStatuses = [UserStatuses.Inactive];
        } else if (e.target.value == 'all') {
            this.model.userStatuses = [];
        }
    }

    @action private onClear() {
        this.searchByStatus = 'all';
        this.props.onClearCallback();
    }

    @action private onSearch() {

        if (this.validators[0].validate(this.model))
            this.props.onSearchCallback();
    }

    //#endregion

    //#region main functions

    private init() {
        this.searchByStatus = 'all';
        this.groupName = ObjectHelper.newGuid();
        this.validators = this.props.previewMode ? [] : [new InternalUsersSearchValidator()];
    }

    private funcBinds() {
        this.onClear = this.onClear.bind(this);
        this.onSearch = this.onSearch.bind(this);
        this.handleRadioButtonListChange = this.handleRadioButtonListChange.bind(this);
    }

    //#endregion
}

export default UserProfileSearchUI;
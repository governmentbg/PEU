import { ArrayHelper, ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame } from 'cnsys-ui-react';
import { EAUBaseComponent, UserPermissions, UserSearchCriteria, UserStatuses, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import moment from 'moment';
import React from 'react';
import { withRouter } from 'react-router-dom';
import { Alert } from 'reactstrap';
import { Constants } from '../../../Constants';
import { InternalUserVM } from '../../../models/InternalUserModels';
import { UsersDataService } from '../../../services/UsersDataService';
import { InternalUsersEditValidator } from '../../../validations/InternalUsersValidator';
import BtnGroupFormUI from '../../common/BtnGroupFormUI';

interface UserProfileFormProps extends BaseRouteProps<any>, AsyncUIProps, BaseRoutePropsExt {
    userId?,
    previewMode?: boolean
}

@observer class UserProfileFormImpl extends EAUBaseComponent<UserProfileFormProps, InternalUserVM>{

    @observable private notification: any;
    @observable private isUserLoaded: boolean;

    private usersDataService: UsersDataService;
    private userId: string;
    private groupName: string;
    private enumValues: number[];

    constructor(props: UserProfileFormProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    render(): JSX.Element {

        let inputAttributes: any  = false;
        let checkboxAttributes: any = false;

        if (!ObjectHelper.isNullOrUndefined(this.props.previewMode)) {
            inputAttributes = {'className': 'form-control test', 'disabled': true};
            checkboxAttributes = {'disabled': true}
        }

        if (this.model && this.isUserLoaded) {

            return <div className="card">
                <div className="card-body">
                    {this.notification}
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                    <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                    <div className="row">
                        <div className="form-group col-md-4 col-sm-6">
                            <label htmlFor="cin">{this.getResource("GL_CIN_L")}</label>
                            <input type="text" name="cin" id="cin" className="form-control" disabled value={this.model.cin} />
                        </div>
                        <div className="form-group col-md-4 col-sm-6">
                            <label htmlFor="username">{this.getResource("GL_USERNAME_L")}</label>
                            <input type="text" name="username" id="username" className="form-control" disabled value={this.model.username} />
                        </div>
                        <div className="form-group col-lg-4  col-md-8 col-sm-6">
                            {this.labelFor(x => x.updatedOn, "GL_DATE_LAST_UPDATE_L")}
                            <input id="INPUT_DATE_LAST_UPDATE" value={this.model.updatedOn.format('DD.MM.YYYY г. HH:mm ч.')} className="form-control" disabled />
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-md-4 col-sm-6">
                            {this.labelFor(x => x.email, "GL_EMAIL_L")}
                            {this.textBoxFor(x => x.email, inputAttributes)}
                        </div>
                        <div className="form-group col-md-4 col-sm-6">
                            <label htmlFor="STATUS">{this.getResource("GL_STATUS_L")}</label>
                            <div className="form-inline">
                                <div className="custom-control-inline custom-control custom-radio">
                                    <input className="custom-control-input" type="radio" onChange={this.handleRadioButtonListChange} name={this.groupName}
                                        id={this.groupName + '_active'} value={'active'} checked={this.model.isActive} {...checkboxAttributes}/>
                                    <label className="custom-control-label" htmlFor={this.groupName + '_active'}>{this.getResource("GL_ACTIVE_L")}</label>
                                </div>
                                <div className="custom-control-inline custom-control custom-radio">
                                    <input className="custom-control-input" type="radio" onChange={this.handleRadioButtonListChange} name={this.groupName}
                                        id={this.groupName + '_inactive'} value={'inactive'} checked={!this.model.isActive} {...checkboxAttributes}/>
                                    <label className="custom-control-label" htmlFor={this.groupName + '_inactive'}>{this.getResource("GL_INACTIVE_L")}</label>
                                </div>
                            </div>
                        </div>
                    </div>
                    
                    <div className={`row ${this.model.username ? '' : 'd-none'}`}>
                        <div className="form-group col-sm-12">
                            <fieldset className="">
                                <legend>{this.getResource("GL_ACCESS_ROLES_L")}</legend>

                                <div className="custom-control custom-checkbox">
                                    <input className="custom-control-input check-all" name="all_roles" id="all_roles" type="checkbox" onChange={this.handleMarkAllRoles}
                                        checked={this.enumValues.length == this.model.userPermisions.length} {...checkboxAttributes}/>
                                    <label className="custom-control-label font-weight-bold" htmlFor="all_roles">{this.getResource("GL_ALL_L")}</label>
                                </div>
                                {
                                    this.enumValues.map((enumValue, index) => {
                                        let isChecked = this.model.userPermisions.findIndex(index => index == enumValue) != -1;
                                        let strEnumValue = UserPermissions[enumValue];

                                        return <div key={strEnumValue} className="custom-control custom-checkbox">
                                            <input className="custom-control-input check-single" name={strEnumValue} id={strEnumValue}
                                                value={enumValue} type="checkbox" onChange={this.handleRoleChange} checked={isChecked} {...checkboxAttributes}/>
                                            <label className="custom-control-label" htmlFor={strEnumValue}>{this.getResource(`GL_${strEnumValue}_L`)}</label>
                                        </div>
                                    })
                                }
                            </fieldset>
                        </div>
                    </div>


                </div>
                {!this.props.previewMode && <BtnGroupFormUI refuseLink={Constants.PATHS.InternalUsersProfiles} onSave={this.update} /> }
            </div>
        }

        return null;
    }

    //#region handlers

    @action private handleRadioButtonListChange(e: any) {

        this.model.isActive = e.target.value == 'active';
    }

    @action private update() {

        if (this.validators[0].validate(this.model)) {

            this.props.registerAsyncOperation(this.usersDataService.updateUserProfile(this.model).then(() => {
                this.notification = <Alert color="success">{this.getResource("GL_UPDATE_OK_I")}</Alert>
            }))
        }
    }

    private handleMarkAllRoles() {
        this.model.userPermisions = this.model.userPermisions.length != this.enumValues.length ? this.enumValues : [];
    }

    private handleRoleChange(e: any) {
        let roleIndex = this.model.userPermisions.findIndex(index => index == +e.target.value);

        if (roleIndex != -1)
            this.model.userPermisions.splice(roleIndex, 1);
        else
            this.model.userPermisions.push(+e.target.value);
    }

    //#endregion

    //#region Main funcs

    private funcBinds() {
        this.update = this.update.bind(this);
        this.handleRoleChange = this.handleRoleChange.bind(this);
        this.handleMarkAllRoles = this.handleMarkAllRoles.bind(this);
        this.handleRadioButtonListChange = this.handleRadioButtonListChange.bind(this);
    }

    private init() {
        this.groupName = ObjectHelper.newGuid();
        this.userId = !ObjectHelper.isNullOrUndefined(this.props.match.params.userId) ? this.props.match.params.userId : this.props.userId;
        this.validators = [new InternalUsersEditValidator()]
        this.usersDataService = new UsersDataService();
        this.enumValues = ObjectHelper.getEnumValues(UserPermissions);

        this.initModel();
    }

    @action private initModel() {
        let that = this;
        let userSearchCriteria = new UserSearchCriteria();
        userSearchCriteria.loadUserPermissions = true;
        userSearchCriteria.userIDs = [+this.userId];
        userSearchCriteria.userStatuses = [];

        this.props.registerAsyncOperation(this.usersDataService.getUsers(userSearchCriteria).then((users) => {

            runInAction.bind(this)(() => {

                if (!users || users.length == 0)
                    this.notification = <Alert color="error">{this.getResource("GL_USER_NOT_FOUND_E")}</Alert>
                else {
                    let currentUser = users[0];
                    this.model = new InternalUserVM();
                    this.model.userID = currentUser.userID
                    this.model.cin = currentUser.cin;
                    this.model.username = currentUser.username;
                    this.model.updatedOn = moment(currentUser.updatedOn);
                    this.model.email = currentUser.email;
                    this.model.isActive = currentUser.status == UserStatuses.Active;
                    this.model.userPermisions = ArrayHelper.queryable.from(users[0].userPermissions).select(x => x.permission).toArray();
                }
            })
        }).finally(() => {
            that.isUserLoaded = true;
        }))
    }

    //#endregion
}

export const UserProfileFormUI = withRouter(withAsyncFrame(UserProfileFormImpl, false));
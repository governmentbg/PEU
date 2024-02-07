import { AsyncUIProps, BaseProps, ConfirmationModal, RawHTML, withAsyncFrame } from 'cnsys-ui-react';
import { attributesClassFormControlRequiredLabel, eauAuthenticationService, EAUBaseComponent, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy, attributesClassFormControlReqired } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Link } from 'react-router-dom';
import { Alert, Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import { Constants } from '../../Constants';
import { UserInputModel } from '../../models/ModelsManualAdded';
import { UserService } from '../../services/UserService';
import { UserProfileValidator } from '../../validations/UsersValidator';

const modalTextKeys = ['GL_FORGETING_USER_PROFILE_SUBTITLE_I', 'GL_FORGETING_USER_PROFILE_DESCRIPTION_I'];

interface UserProfileProps extends BaseProps, AsyncUIProps {
}

@observer class UserProfileImpl extends EAUBaseComponent<UserProfileProps, UserInputModel>{

    @observable notification: any;
    @observable hasAuthentications: boolean;
    @observable isOpen: boolean;

    private userService: UserService;

    constructor(props: UserProfileProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    render() {

        return <div className="page-wrapper" id="ARTICLE-CONTENT">
            <div className="ui-form ui-form--input">
                {this.notification || null}
                <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                <div className="row">
                    <div className="form-group col-md-6 col-lg-4">
                        {this.labelFor(x => x.email, "GL_EMAIL_L", attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(x => x.email, attributesClassFormControlReqired)}
                    </div>
                </div>
            </div>
            <div className="button-bar button-bar--form button-bar--responsive">
                <div className="right-side">
                    <button className="btn btn-primary" onClick={this.updateUserProfile}>{this.getResource("GL_CONFIRM_L")}</button>
                    {
                        this.hasAuthentications
                            ? <>
                                <button className="btn btn-outline-danger" onClick={this.onModalToggle}>{this.getResource("GL_FORGET_ME_L")}</button>
                                <Modal isOpen={this.isOpen} toggle={this.onModalToggle} centered>
                                    <ModalHeader toggle={this.onModalToggle}>{this.getResource("GL_FORGETING_USER_PROFILE_TITLE_I")}</ModalHeader>
                                    <ModalBody>
                                        <div className="alert alert-danger" role="alert">
                                            <b><RawHTML rawHtmlText={this.getResource("GL_PROFILE_CANNOT_BE_FORGOTTEN_ERROR_I")} /></b>
                                            <RawHTML rawHtmlText={this.getResource("GL_SHOULD_DELETE_ADDED_AUTHENTICATION_I")} />
                                        </div>
                                    </ModalBody>
                                    <ModalFooter>
                                        <div className="button-bar button-bar--responsive">
                                            <div className="right-side">
                                                <Link to={Constants.PATHS.UserAuthentications} className="btn btn-secondary" >{this.getResource("GL_TO_USER_AUTHENTICATIONS_PAGE_L")}</Link>
                                            </div>
                                            <div className="left-side">
                                                <button type="button" className="btn btn-secondary" data-dismiss="modal" onClick={this.onModalToggle}>{this.getResource("GL_CANCEL_L")}</button>
                                            </div>
                                        </div>
                                    </ModalFooter>
                                </Modal>
                            </>
                            : <ConfirmationModal modalTitleKey='GL_FORGETING_USER_PROFILE_TITLE_I' modalTextKeys={modalTextKeys} onSuccess={this.deactivateUser} yesTextKey='GL_CONFIRM_L' noTextKey='GL_CANCEL_L'>
                                <button className="btn btn-outline-danger">{this.getResource("GL_FORGET_ME_L")}</button>
                            </ConfirmationModal>
                    }
                </div>
                <div className="left-side">
                    <Link to={Constants.PATHS.Home} className="btn btn-secondary" >{this.getResource("GL_CANCEL_L")}</Link>
                </div>
            </div>
        </div>
    }

    //#region handlers

    private onModalToggle() {
        this.isOpen = !this.isOpen;
    }

    //#endregion

    //#region user profile manipulations

    private updateUserProfile() {

        if (this.validators[0].validate(this.model)) {

            this.props.registerAsyncOperation(new UserService().updateUserProfile(this.model.email).then(result => {
                this.notification = <Alert color="info">{this.getResource("GL_CHANGE_WILL_BE_SEEN_LAST_ENTER_L")}</Alert>
            }))
        }
    }

    private deactivateUser() {
        this.props.registerAsyncOperation(new UserService().deactivateUserProfile().then(result => {

            eauAuthenticationService.userLogout();
        }))
    }

    //#endregion

    //#region main functions 

    @action private init() {
        this.userService = new UserService();
        this.model = new UserInputModel();
        this.validators = [new UserProfileValidator()];

        let authenticationTypes = this.userService.getUsersAuthTypes().then(authentications => {
            this.hasAuthentications = authentications && authentications.length > 0
        })

        let loadUserPromise = eauAuthenticationService.loadCurrentUser().then(u => {
            this.model.email = u.email;
        })

        this.props.registerAsyncOperation(Promise.all([authenticationTypes, loadUserPromise]));
    }

    private funcBinds() {
        this.updateUserProfile = this.updateUserProfile.bind(this);
        this.deactivateUser = this.deactivateUser.bind(this);
        this.onModalToggle = this.onModalToggle.bind(this);
    }

    //#endregion
}

export const UserProfileUI = withAsyncFrame(UserProfileImpl, false)
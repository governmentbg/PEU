﻿import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { EAUBaseComponent, NotificationPanel, NotificationType, Pagination, resourceManager, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { LDAPUser, LDAPUserSearchCritaria } from '../../../models/ModelsAutoGenerated';
import { UsersDataService } from '../../../services/UsersDataService';
import { LDAPUserSearchValidator } from '../../../validations/InternalUsersValidator';
import CardFooterUI from '../../common/CardFooterUI';
import LDAPUsersProfilesResultsUI from './LDAPUsersProfilesResultsUI';
import LDAPUsersProfilesSearchUI from './LDAPUsersProfilesSearchUI';

interface LDAPUsersProfilesProps extends BaseProps, AsyncUIProps {
    onGetUser?: (user) => void;
}

@observer class LDAPUsersProfilesImpl extends EAUBaseComponent<LDAPUsersProfilesProps, LDAPUserSearchCritaria>{

    @observable private ldapUsers: LDAPUser[];
    @observable isLoaded: boolean;

    private userDataService: UsersDataService;

    constructor(props: LDAPUsersProfilesProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    render() {

        let dataResult: any = null;

        if (this.isLoaded) {

            if (this.ldapUsers && this.ldapUsers.length > 0) {
                dataResult = <>
                    <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                    <LDAPUsersProfilesResultsUI userProfiles={this.ldapUsers} onGetUser={this.props.onGetUser} />
                    <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} />
                </>
            } else {

                dataResult = (
                    <div className="col-12">
                        <NotificationPanel notificationType={NotificationType.Info} text={resourceManager.getResourceByKey("GL_NO_RESULTS_I")} />
                    </div>
                );
            }
        }

        return <>
            <div className="card card--search">
                <div className="col-12">
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                    <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                </div>
                <LDAPUsersProfilesSearchUI {...this.bind(x => x, "LDAPUserSearchCritaria")} />
                <CardFooterUI onClear={this.onClear} onSearch={this.onSearch} />
            </div>
            <div className="card card--borderless">
                <div className="card-body">
                    {dataResult}
                </div>
            </div>
        </>
    }

    //#region handlers

    @action private onPageChange(page: any): void {

        if (this.validators[0].validate(this.model)) {
            this.model.page = page;
            this.props.registerAsyncOperation(this.searchUsers());
        }
    }

    @action private onSearch() {

        if (this.validators[0].validate(this.model)) {
            this.model.page = 1;
            this.props.registerAsyncOperation(this.searchUsers());
        }
    }

    private onClear() {
        this.init();
    }

    //#endregion

    @action private searchUsers() {

        this.isLoaded = false;

        return this.userDataService.getLDAPUsers(this.model).then(result => {
            this.ldapUsers = result;

        }).finally(() => {
            this.isLoaded = true;
        })
    }

    //#region main functions

    private funcBinds() {
        this.onSearch = this.onSearch.bind(this);
        this.onClear = this.onClear.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
    }

    @action private init() {
        this.userDataService = new UsersDataService();
        this.isLoaded = false;
        this.initModel();
    }

    @action private initModel() {
        this.model = new LDAPUserSearchCritaria();
        this.validators = [new LDAPUserSearchValidator()]
    }

    //#endregion
}

export const LDAPUsersProfilesUI = withAsyncFrame(LDAPUsersProfilesImpl, false);
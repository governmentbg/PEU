import { ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { AuthenticationTypes, EAUBaseComponent, NotificationPanel, NotificationType, Pagination, UserSearchCriteria, UserStatuses, UserVM, ValidationSummaryErrors } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { UsersDataService } from '../../../services/UsersDataService';
import CardFooterUI from '../../common/CardFooterUI';
import UserProfileResultsUI from './UsersProfilesResultsUI';
import UserProfileSearchUI from './UsersProfilesSearchUI';

interface UsersProfilesProps extends BaseProps, AsyncUIProps {
    previewMode?: boolean;
    onGetUser?: (user) => void;
}

@observer class UsersProfilesImpl extends EAUBaseComponent<UsersProfilesProps, UserSearchCriteria>{

    @observable private users: UserVM[];
    @observable isLoaded: boolean;

    previewMode = false;

    private userDataService: UsersDataService;

    constructor(props: UsersProfilesProps) {
        super(props);

        this.previewMode = ObjectHelper.isNullOrUndefined(this.props.previewMode) ? false : this.props.previewMode;
        
        this.funcBinds();
        this.init();


    }

    render() {

        let dataResult: any = null;

        if (this.isLoaded) {

            if (this.users && this.users.length > 0) {
                dataResult = <div className="card">
                    <div className="card-body">
                    <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                    <UserProfileResultsUI userProfiles={this.users} previewMode={this.previewMode} onGetUser={this.props.onGetUser}/>
                    <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} />
                </div>
                </div>
            } else {

                dataResult = (<div className="card">
                    <div className="col-12">
                        <NotificationPanel notificationType={NotificationType.Info} text={this.getResource('GL_NO_RESULTS_I')} />
                    </div>
                </div>);
            }
        }

        return <>
            <div className="card">
                <div className="col-12">
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                </div>
                <UserProfileSearchUI onSearchCallback={this.onSearch} onClearCallback={this.onClear} previewMode={this.previewMode} {...this.bind(x => x, "UserSearchCriteria")} />
                <CardFooterUI onClear={this.onClear} onSearch={this.onSearch} />
            </div>
            {dataResult}
        </>
    }

    //#region handlers

    @action private onPageChange(page: any): void {
        this.model.page = page;
        this.props.registerAsyncOperation(this.searchUsers());
    }

    @action private onSearch() {
        this.model.page = 1;
        this.props.registerAsyncOperation(this.searchUsers());
    }

    private onClear() {
        this.init();
    }

    //#endregion

    @action private searchUsers() {
        this.isLoaded = false;

        return this.userDataService.getUsers(this.model).then(result => {
            this.users = result;

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
        this.props.registerAsyncOperation(this.searchUsers());
    }

    @action private initModel() {
        this.model = new UserSearchCriteria();
        this.model.userIDs = [];
        
        this.model.userStatuses = this.previewMode ? [UserStatuses.Active, UserStatuses.Deactivated, UserStatuses.Locked, UserStatuses.Inactive] : [];
        
        this.model.loadUserPermissions = false;
        this.model.authenticationType = this.previewMode ? null:AuthenticationTypes.ActiveDirectory;
    }

    //#endregion
}

export const UsersProfilesUI = withAsyncFrame(UsersProfilesImpl, false);
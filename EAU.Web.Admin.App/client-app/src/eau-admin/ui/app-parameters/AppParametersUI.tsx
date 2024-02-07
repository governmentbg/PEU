import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { AppParameter, AppParameterSearchCriteria, EAUBaseComponent, NotificationPanel, NotificationType, Pagination, ValidationSummaryErrors, ValidationSummary, ValidationSummaryStrategy } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { AppParametersDataService } from '../../services/AppParametersDataService';
import AppParametersResultsUI from './AppParametersResultsUI';
import AppParametersSearchUI from './AppParametersSearchUI';
import CardFooterUI from '../common/CardFooterUI';

interface AppParametersProps extends BaseProps, AsyncUIProps {
}

@observer class AppParametersImpl extends EAUBaseComponent<AppParametersProps, AppParameterSearchCriteria>{

    private appParametersService: AppParametersDataService;
    @observable private appParameters: AppParameter[];

    @observable isLoaded: boolean;

    constructor(props: AppParametersProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    render() {
        let dataResult: any = null;

        if (this.isLoaded) {

            if (this.appParameters && this.appParameters.length > 0) {
                dataResult = <div className="card">
                    <div className="card-body">
                    <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                    <AppParametersResultsUI appParameters={this.appParameters} />
                    <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} />
                </div>
                </div>
            } else {

                dataResult = (<div className="card">
                    <div className="col-12">
                        <NotificationPanel notificationType={NotificationType.Info} text={this.getResource("GL_NO_RESULTS_I")} />
                    </div>
                </div>);
            }
        }

        return <>
            <div className="card">
                <div className="col-12">
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                </div>
                <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                <AppParametersSearchUI  {...this.bind(x => x, "AppParameterSearchCriteria")} />
                <CardFooterUI onClear={this.onClear} onSearch={this.onSearch} />
            </div>
            {dataResult}

        </>
    }

    //#region handlers

    @action private onPageChange(page: any): void {
        this.model.page = page;
        this.props.registerAsyncOperation(this.searchAppParameters());
    }

    @action private onSearch() {
        this.model.page = 1;
        this.props.registerAsyncOperation(this.searchAppParameters());
    }

    private onClear() {
        this.init();
    }

    //#endregion

    @action private searchAppParameters() {
        this.isLoaded = false;

        return this.appParametersService.getAppParameters(this.model).then(result => {
            this.appParameters = result;

        }).finally(() => {
            this.isLoaded = true;
        })
    }

    //#region main functions

    private funcBinds() {
        this.onSearch = this.onSearch.bind(this);
        this.onClear = this.onClear.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
        this.searchAppParameters = this.searchAppParameters.bind(this);
    }

    @action private init() {
        this.appParametersService = new AppParametersDataService();
        this.isLoaded = false;
        this.model = new AppParameterSearchCriteria();
        this.model.isSystem = false;
    }

    //#endregion
}

export const AppParametersUI = withAsyncFrame(AppParametersImpl, false);
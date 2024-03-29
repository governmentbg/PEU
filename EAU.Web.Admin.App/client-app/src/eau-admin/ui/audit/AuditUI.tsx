﻿import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { LogAction } from '../../models/LogAction';
import { LogActionSearchCriteria } from '../../models/LogActionSearchCriteria';
import { ActionType, LogActionSearchModes, ObjectType } from '../../models/ModelsAutoGenerated';
import { AuditDataService } from '../../services/AuditDataService';
import { EAUBaseComponent, NotificationPanel, NotificationType, Pagination, ValidationSummaryErrors } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import CardFooterUI from '../common/CardFooterUI';
import AuditResultsUI from './AuditResultsUI';
import { AuditSearchUI } from './AuditSearchUI';
import { AuditValidator } from '../../validations/AuditValidator';
import { Cache } from '../../cache/Cache';


interface AuditProps extends BaseProps, AsyncUIProps {
}

@observer class AuditUIImpl extends EAUBaseComponent<AuditProps, LogActionSearchCriteria>{

    // @observable private audit: DataServiceLimit[];
    @observable isLoaded: boolean;
    @observable auditList: LogAction[] = [];
    @observable actionTypes: ActionType[] = [];
    @observable objectTypes: ObjectType[] = [];
    @observable infoMessage: string = null;


    private auditDataService: AuditDataService;

    constructor(props: AuditProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    @action private init() {

        this.validators = [new AuditValidator()];
        let cache = Cache;

        this.props.registerAsyncOperation(
            cache.getActionTypesDataCache().then(result => {
                this.actionTypes = result;
            })
        );

        this.props.registerAsyncOperation(
            cache.getObjectTypesDataCache().then(result => {
                this.objectTypes = result;
            })
        );

        this.model = new LogActionSearchCriteria();
        this.auditDataService = new AuditDataService();
    }

    private funcBinds() {
        this.onSearch = this.onSearch.bind(this);
        this.onClear = this.onClear.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
        this.onChangeMode = this.onChangeMode.bind(this);

    }

    @action private onSearch() {
        this.model.page = 1;

        if (this.validators[0].validate(this.model)) {
            this.props.registerAsyncOperation(
                this.auditDataService.getServiceLimits(this.model)
                    .then(
                        result => {
                            this.isLoaded = true;
                            this.auditList = result ? result : []
                        })
            );
        }
    }

    @action private onPageChange(page: any): void {
        this.model.page = page;
        this.props.registerAsyncOperation(
            this.auditDataService.getServiceLimits(this.model)
                .then(
                    result => {
                        this.isLoaded = true;
                        this.auditList = result;
                    })
                .finally(() => this.isLoaded = true)
        );
    }

    private onClear() {
        this.auditList = [];
        this.isLoaded = false;
        this.infoMessage = null;
        this.init();
    }

    onChangeMode() {

        switch (Number(this.model.mode)) {
            case LogActionSearchModes.Archive:
                this.infoMessage = this.getResource("GL_ODIT_SEARCH_AFTER_L");
                break;

            case LogActionSearchModes.Operational:
                this.infoMessage = this.getResource("GL_ODIT_SEARCH_ТО_L");
                break;

            default:
                this.infoMessage = null;
        }
    }

    render() {


        let searchSection = <></>;
        let resultsSection = <></>;

        if (this.objectTypes.length > 0 && this.actionTypes.length > 0) {
            searchSection =
                <div className="card">

                    <div className="card-header">
                        <h3>{this.getResource("GL_ODIT_SEARCH_L")}</h3>
                    </div>

                    <div id="collapsable-content" className="collapse show">
                        <form method="get" className="needs-validation" action="/audit-list">
                            <div className="card-body">

                                <div className={`alert alert-info alert-dismissible ${this.infoMessage ? '' : 'd-none'}`} id="audit-info-message" role="alert">
                                    {this.infoMessage}
                                </div>

                                <AuditSearchUI
                                    onSearchCallback={this.onSearch}
                                    onClearCallback={this.onClear}
                                    objectTypes={this.objectTypes}
                                    actionTypes={this.actionTypes}
                                    onChangeMode={this.onChangeMode}
                                    {...this.bind(x => x, "LogActionSearchCriteria")}
                                />
                                
                            </div>
                        </form>
                    </div>

                    <CardFooterUI onClear={this.onClear} onSearch={this.onSearch} />
                </div>

            if (this.isLoaded) {

                if (this.auditList.length > 0) {
                    resultsSection =
                        <div className="card">
                            <div className="card-body">
                                <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                                <AuditResultsUI auditList={this.auditList} objectTypes={this.objectTypes} actionTypes={this.actionTypes} />
                                <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                            </div>
                        </div>
                }
                else {
                    resultsSection = <div className="card">
                        <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                        <div className="col-12">
                            <NotificationPanel notificationType={NotificationType.Info} text={this.getResource("GL_NO_DATA_FOUND_L")} />
                        </div>
                    </div>;
                }
            }
        }

        return <>
            {searchSection}
            {resultsSection}
        </>
    }
}

export const AuditUI = withAsyncFrame(AuditUIImpl, false);
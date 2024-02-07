﻿import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { Constants, EAUBaseComponent, NotificationPanel, NotificationType, Pagination, ValidationSummaryErrors } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { UrlHelper } from 'cnsys-core';
import { formatDate } from '../../../AdminUIHelpers';
import { DocumentAccessedDataTypes, NotaryInterestsForPersonOrVehicleReportData, NotaryInterestsForPersonOrVehicleSearchCriteria } from '../../../models/ModelsAutoGenerated';
import { ReportsDataService } from '../../../services/ReportsDataService';

interface NotaryInterestsForPersonOrVehicleResultsUIProps extends BaseProps, AsyncUIProps { }

@observer export class NotaryInterestsForPersonOrVehicleResultsUIImpl extends EAUBaseComponent<NotaryInterestsForPersonOrVehicleResultsUIProps, NotaryInterestsForPersonOrVehicleSearchCriteria>{

    private reportsDataService: ReportsDataService;
    @observable private results: NotaryInterestsForPersonOrVehicleReportData[];

    constructor(props: NotaryInterestsForPersonOrVehicleResultsUIProps) {
        super(props);

        this.reportsDataService = new ReportsDataService();
        this.onPageChange = this.onPageChange.bind(this);
        this.exportDataToPDF = this.exportDataToPDF.bind(this);
    }

    @action componentDidMount() {
        if (this.model) {
            if (this.model.dateFrom) {
                this.model.dateFrom = this.model.dateFrom.startOf('day');
            }

            if (this.model.dateTo) {
                this.model.dateTo = this.model.dateTo.endOf('day');
            }

            this.props.registerAsyncOperation(this.reportsDataService.getNotaryInterestsForPersonOrVehicle(this.model).then(response => {
                this.results = response || [];
            }))
        }
    }

    @action private onPageChange(page: any): void {
        this.model.page = page;

        if (this.model.dateFrom) {
            this.model.dateFrom = this.model.dateFrom.startOf('day');
        }

        if (this.model.dateTo) {
            this.model.dateTo = this.model.dateTo.endOf('day');
        }

        this.props.registerAsyncOperation(this.reportsDataService.getNotaryInterestsForPersonOrVehicle(this.model).then(response => {
            this.results = response || [];
        }))
    }

    private exportDataToPDF() {
        const downloadURL = this.reportsDataService.getNotaryInterestsForPersonOrVehicleAsPDFDownloadURL(this.model);
        UrlHelper.downloadFile(downloadURL);
    }

    render() {

        if (!this.model || !this.results) {
            return null;
        }

        return <div className="card">
            {
                this.results?.length > 0
                    ? <div className="card-body">
                        <div className="card-navbar">
                            <div className="button-bar">
                                <div className="right-side">
                                    <button type="button" className="btn btn-secondary" onClick={this.exportDataToPDF}>
                                        <i className="ui-icon ui-icon-download mr-1"></i>{this.getResource('GL_EXPORT_TABLE_TO_PDF_L')}
                                    </button>
                                </div>
                                <div className="left-side"></div>
                            </div>
                        </div>
                        <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                        <div className="table-responsive">
                            <table className="table table-bordered table-striped table-hover">
                                <thead>
                                    <tr>
                                        <th colSpan={3} className="text-center align-middle">{this.getResource('GL_NOTARY_REVIEWED_THE_REFERENCE_L')}</th>
                                        <th className="text-center align-middle">{this.getResource('GL_PERSON_OF_INTEREST_L')}</th>
                                        <th className="text-center align-middle">{this.getResource('GL_VEHICLE_OF_INTEREST_L')}</th>
                                        <th rowSpan={2}>{this.getResource('GL_WORKPLACE_IP ADDRESS_L')}</th>
                                        <th rowSpan={2}>{this.getResource('GL_REFERENCE_TIME_L')}</th>
                                    </tr>
                                    <tr>
                                        <th>{this.getResource('GL_PERSON_ID_L')}</th>
                                        <th>{this.getResource('GL_NAME_L')}</th>
                                        <th>{this.getResource('GL_EMAIL_ADDRESS_L')}</th>
                                        <th>{this.getResource('GL_PERSON_ID_AND_NAME_L')}</th>
                                        <th>{this.getResource('GL_VEHICLE_REG_NUMBER_SHORT_L')}</th>
                                    </tr>
                                </thead>
                                <tbody id="content">
                                    {
                                        this.results.map((currentResult, key) => {

                                            return <tr key={`${currentResult.notaryUserEmail}_${key}`}>
                                                <td>{currentResult.notaryUserIdentifier}</td>
                                                <td>{currentResult.notaryUserNames}</td>
                                                <td>{currentResult.notaryUserEmail}</td>
                                                <td>
                                                    {
                                                        currentResult.documentAccessedDataValues?.filter(x => x.dataType == DocumentAccessedDataTypes.PersonIdentifierAndNames).map((personData, key) => {
                                                            return <p key={`${personData.dataValue}_${key}`}>{personData.dataValue}</p>
                                                        })
                                                    }
                                                </td>
                                                <td>
                                                    {
                                                        currentResult.documentAccessedDataValues?.filter(x => x.dataType == DocumentAccessedDataTypes.VehicleRegNumber).map((vehicleData, key) => {
                                                            return <p key={`${vehicleData.dataValue}_${key}`}>{vehicleData.dataValue}</p>
                                                        })
                                                    }
                                                </td>
                                                <td>{currentResult.ipAddress}</td>
                                                <td>{formatDate(currentResult.interestDate, Constants.DATE_FORMATS.dateTime)}</td>
                                            </tr>
                                        })
                                    }
                                </tbody>
                            </table>
                        </div>
                        <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} />
                    </div>
                    : <div className="col-12">
                        {
                            this.props.asyncErrors?.length > 0
                                ? <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                                : <NotificationPanel notificationType={NotificationType.Info} text={this.getResource("GL_NO_RESULTS_I")} />
                        }
                    </div>
            }
        </div>
    }
}

export const NotaryInterestsForPersonOrVehicleResultsUI = withAsyncFrame(NotaryInterestsForPersonOrVehicleResultsUIImpl, false)
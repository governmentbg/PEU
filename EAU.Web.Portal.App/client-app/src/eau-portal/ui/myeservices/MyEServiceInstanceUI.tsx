import { BindableReference, ObjectHelper } from "cnsys-core";
import { AsyncUIProps, BaseProps, BaseRouteProps, RawHTML, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { EAUBaseComponent, Nomenclatures, Service, ValidationSummary, ValidationSummaryStrategy, ValidationSummaryErrors, ServiceInstanceSearchCriteria, ObligationSearchCriteria, ServiceInstanceStatuses } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import moment from "moment";
import * as React from 'react';
import { CaseFileInfo, Obligation, ServiceInstance, ServiceInstanceInfo } from "../../models/ModelsManualAdded";
import { ObligationsDataService } from '../../services/ObligationsDataService';
import { ServiceInstancesDataService } from "../../services/ServiceInstancesDataService";
import MyEServiceInstanceCurrentStageUI from './MyEServiceInstanceCurrentStageUI';
import MyEServiceInstanceDocumentsUI from './MyEServiceInstanceDocumentsUI';
import MyEServiceInstancePaymentsUI from './MyEServiceInstancePaymentsUI';

interface MyEServiceInstanceUIProps extends BaseRouteProps<any>, BaseProps, AsyncUIProps {
}

@observer class MyEServiceInstanceUIImpl extends EAUBaseComponent<MyEServiceInstanceUIProps, ServiceInstanceSearchCriteria>{

    @observable private eServicesInstanceItem: ServiceInstance[] = [];

    @observable private eService: Service;

    @observable private obligationList: Obligation[] = [];

    @observable private eServicesInstanceDocuments: CaseFileInfo;

    @observable private eServicesInstanceInfo: ServiceInstanceInfo = null;

    @observable private isLoaded: boolean;
    @observable private isServiceLoaded: boolean;
    @observable private isLoadedObligations: boolean;
    @observable private isLoadedDocuments: boolean;

    @observable private pepError: string;

    @observable private showPreviousStages: boolean = false;

    private refreshInerval: any;

    private serviceInstancesDataService: ServiceInstancesDataService;
    private obligationsDataService: ObligationsDataService;

    constructor(props: MyEServiceInstanceUIProps) {
        super(props);

        this.toggleStages = this.toggleStages.bind(this);
        this.refreshSecondary = this.refreshSecondary.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
        this.componentWillUnmount = this.componentWillUnmount.bind(this);

        this.serviceInstancesDataService = new ServiceInstancesDataService();
        this.obligationsDataService = new ObligationsDataService();

        this.init = this.init.bind(this);
        this.init();
    }

    componentDidMount() {
        this.refreshInerval = setInterval(this.refreshSecondary, 10000);
    }

    componentWillUnmount() {
        clearInterval(this.refreshInerval);
        this.refreshInerval = null;
    }

    render() {

        let dataResult: any = null;

        if (this.isLoaded) {

            if (!ObjectHelper.isNullOrUndefined(this.eServicesInstanceItem) && this.eServicesInstanceItem.length > 0) {

                if (this.isServiceLoaded && this.isLoadedObligations) {

                    dataResult =
                        <div className="ui-form ui-form--preview">
                            <fieldset className="fields-group fields-group--no-border">
                                <h2 className="page-subtitle">
                                    <span className="d-inline-block pb-1 pt-1">{this.getResource("GL_URI_L") + ' ' + this.eServicesInstanceItem[0].caseFileURI}</span> <br></br>
                                    <span className="d-inline-block mb-3">{!ObjectHelper.isNullOrUndefined(this.eService.name) ? this.eService.name : ''}</span>
                                </h2>
                                {!ObjectHelper.isNullOrUndefined(this.eService.explanatoryTextService) && this.eService.explanatoryTextService != '' ?
                                    <div className="alert alert-info white-space-normal"><RawHTML rawHtmlText={this.eService.explanatoryTextService} /></div> : ''
                                }
                            </fieldset>

                            <fieldset className="fields-group fields-group--no-border">

                                <legend>
                                    <h3 className="section-title">{this.getResource("GL_SERVICE_CURRENT_STAGE_L")}</h3>
                                </legend>

                                <div className={`stage-view ${this.showPreviousStages ? "" : "collapsed"}`}>

                                    <MyEServiceInstanceCurrentStageUI
                                        modelReference={new BindableReference(this.eServicesInstanceItem[0])}
                                        eService={this.eService}
                                        showPreviousStages={this.showPreviousStages}
                                        obligationList={this.obligationList}
                                        eServicesInstanceInfo={this.eServicesInstanceInfo}
                                        pepErrorCallback={this.pepErrorProcess} />

                                    <button id="TOGGLE-STAGES" className="btn btn-link item-status-button" onClick={this.toggleStages}>
                                        <i className="ui-icon ui-icon-chevron-up mr-1" aria-hidden="true"></i>{this.showPreviousStages ? this.getResource("GL_SHOW_LAST_STAGE_L") : this.getResource("GL_SHOW_ALL_STAGES_L")}
                                    </button>
                                </div>

                            </fieldset>

                            {this.isLoadedDocuments
                                ? <MyEServiceInstanceDocumentsUI eServicesInstanceDocuments={this.eServicesInstanceDocuments} caseFileURI={this.eServicesInstanceItem[0].caseFileURI} serviceInstanceId={this.eServicesInstanceItem[0].serviceInstanceID} />
                                : null
                            }

                            {!ObjectHelper.isNullOrUndefined(this.obligationList) && this.obligationList.length > 0
                                ? <MyEServiceInstancePaymentsUI paymentList={this.obligationList} />
                                : null
                            }

                        </div>
                }

            }

            else {
                dataResult = <div className="alert alert-warning mt-0 mb-4"><p>{this.getResource("GL_00022_E")}</p></div>
            }
        }

        return <>
            <div className="page-wrapper" id="ARTICLE-CONTENT">

                <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                <ValidationSummary model={this.model} propNames={["errServiceInstance"]} strategy={ValidationSummaryStrategy.excludeAllExcept} />

                {!ObjectHelper.isStringNullOrEmpty(this.pepError) &&
                    <div className="alert alert-warning mt-0 mb-4" role="alert">
                        <p>{this.pepError}</p>
                    </div>}

                {dataResult}
            </div>
        </>
    }

    @action private toggleStages() {

        if (this.showPreviousStages) {
            this.showPreviousStages = false;
        }
        else {
            this.loadPreviousStages();
        }

    }

    @action private init() {

        this.isLoaded = false;
        this.isServiceLoaded = false;
        this.isLoadedObligations = false;
        this.isLoadedDocuments = false;

        this.model = new ServiceInstanceSearchCriteria();
        this.model.caseFileURI = this.props.match.params.caseFileURI;
        this.model.page = 1;

        let obligationCriteria = new ObligationSearchCriteria();
        obligationCriteria.page = 1;

        if (this.model.caseFileURI) {

            runInAction.bind(this)(() => {
                this.props.registerAsyncOperation(

                    // инстанция на услуга
                    this.serviceInstancesDataService.searchServiceInstances(this.model)
                        .then((result) => {
                            this.eServicesInstanceItem = result;
                        })

                        // списък услуги от номенклатура
                        .then(() => {
                            if (!ObjectHelper.isNullOrUndefined(this.eServicesInstanceItem) && !ObjectHelper.isNullOrUndefined(this.eServicesInstanceItem[0].serviceID)) {
                                Nomenclatures.getServices()
                                    .then(res => {
                                        if (res && res.length > 0) {
                                            this.eService = res.filter(s => s.serviceID == this.eServicesInstanceItem[0].serviceID)[0];
                                        }
                                    })
                                    .finally(() => this.isServiceLoaded = true)
                            }
                        })

                        // списък плащания
                        .then(() => {
                            if (!ObjectHelper.isNullOrUndefined(this.eServicesInstanceItem) && !ObjectHelper.isNullOrUndefined(this.eServicesInstanceItem[0].serviceInstanceID)) {

                                obligationCriteria.serviceInstanceID = this.eServicesInstanceItem[0].serviceInstanceID;
                                obligationCriteria.pageSize = 1000000;
                                this.props.registerAsyncOperation(
                                    this.obligationsDataService.searchObligations(obligationCriteria)
                                        .then(resultObligation => {
                                            runInAction(() => {
                                                this.obligationList = resultObligation;
                                                this.isLoadedObligations = true
                                            })
                                        })
                                )
                            }
                        })

                        // списък документи
                        .then(() => {

                            if (!ObjectHelper.isNullOrUndefined(this.eServicesInstanceItem) && !ObjectHelper.isNullOrUndefined(this.eServicesInstanceItem[0].serviceInstanceID)) {
                                this.props.registerAsyncOperation(
                                    this.serviceInstancesDataService.getServiceInstancesDocuments(this.eServicesInstanceItem[0].serviceInstanceID)
                                        .then(result => {
                                            runInAction(() => {
                                                this.eServicesInstanceDocuments = result
                                                this.isLoadedDocuments = true
                                            })
                                        })

                                )
                            }
                        })

                        .finally(() => this.isLoaded = true)
                );
            })
        }
    }

    private loadPreviousStages() {
        if (this.eServicesInstanceItem[0].serviceInstanceID) {

            this.props.registerAsyncOperation(this.serviceInstancesDataService.getServiceInstancesInfo(this.eServicesInstanceItem[0].serviceInstanceID).bind(this)
                .then(result => {
                    runInAction(() => {

                        this.eServicesInstanceInfo = result;

                        let sortArray = [];

                        if (result.executedStages.length > 0) {

                            // Сортиране по дата
                            sortArray = result.executedStages.sort((el1, el2) => {
                                if (el1.actualCompletionDate > el2.actualCompletionDate)
                                    return -1;
                                else if (el1.actualCompletionDate < el2.actualCompletionDate)
                                    return 1;
                                else
                                    return 0;
                            });

                            if (this.eServicesInstanceItem[0].status == ServiceInstanceStatuses.Completed || this.eServicesInstanceItem[0].status == ServiceInstanceStatuses.Rejected) {
                                sortArray.shift();
                            }

                            this.eServicesInstanceInfo.executedStages = sortArray;
                        }

                        this.showPreviousStages = true;
                    })
                }));
        }
    }

    private refreshSecondary() {

        if (!ObjectHelper.isNullOrUndefined(this.eServicesInstanceItem) && !ObjectHelper.isNullOrUndefined(this.eServicesInstanceItem[0]) && this.eServicesInstanceItem[0].status == ServiceInstanceStatuses.InProcess) {

            // инстанция на услуга
            this.serviceInstancesDataService.searchServiceInstances(this.model)
                .then((result) => {
                    runInAction(() => {
                        if (!moment(this.eServicesInstanceItem[0].updatedOn).isSame(result[0].updatedOn)) {
                            this.eServicesInstanceItem = result;

                            if (this.eServicesInstanceItem[0].serviceInstanceID) {

                                if (this.showPreviousStages) {
                                    this.loadPreviousStages();
                                }

                                this.isLoadedObligations = false;
                                this.isLoadedDocuments = false;

                                let obligationCriteria = new ObligationSearchCriteria();
                                obligationCriteria.page = 1;
                                obligationCriteria.serviceInstanceID = this.eServicesInstanceItem[0].serviceInstanceID;
                                obligationCriteria.pageSize = 1000000;

                                this.props.registerAsyncOperation(
                                    // списък плащания
                                    this.obligationsDataService.searchObligations(obligationCriteria)
                                        .then(resultObligation => { this.obligationList = resultObligation; })
                                        .finally(() => this.isLoadedObligations = true)
                                )

                                // списък документи
                                this.props.registerAsyncOperation(
                                    this.serviceInstancesDataService.getServiceInstancesDocuments(this.eServicesInstanceItem[0].serviceInstanceID)
                                        .then(resultDocuments => {
                                            runInAction(() => {
                                                this.eServicesInstanceDocuments = resultDocuments
                                                this.isLoadedDocuments = true
                                            })
                                        })
                                )
                            }
                        }
                    })
                })
                .catch((err) => {
                    if (this.model.getPropertyErrors("errServiceInstance").length == 0)
                        this.model.addError("errServiceInstance", err.message)
                })
        }
    }

    @action private pepErrorProcess(errMsg: string): void {
        this.pepError = errMsg;
    }
}

export const MyEServiceInstanceUI = withRouter(withAsyncFrame(MyEServiceInstanceUIImpl, false));
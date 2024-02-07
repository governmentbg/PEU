import { BindableReference, ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { ANDObligationSearchMode, EAUBaseComponent, Nomenclatures, Service, ValidationSummaryErrors } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Link } from 'react-router-dom';
import { Constants } from '../../Constants';
import { ANDObligationSearchCriteria, ANDObligationSearchResponse, ObligatedPersonType, ObligationSearchResultUnitGroups } from '../../models/ModelsManualAdded';
import { ObligationsDataService } from '../../services/ObligationsDataService';
import { ANDObligationSearchCriteriaValidator } from '../../validations/ANDObligationSearchCriteriaValidator';
import { BDSObligationsResultsUI } from './BDSObligationsResultsUI';
import { KATObligationsResultsUI } from './KATObligationsResultsUI';
import { ObligationsFormUI } from './ObligationsFormUI';
import { showUsedSearchCriteria } from './ObligationsHelpers';

interface ObligationsUIProps extends BaseProps, AsyncUIProps, BaseRoutePropsExt { }

@observer class ObligationsUIImpl extends EAUBaseComponent<ObligationsUIProps, ANDObligationSearchCriteria>{

    @observable private searchResult: ANDObligationSearchResponse;
    @observable private currentService: Service;
    @observable private searchHasBeenMade: boolean;
    @observable private isLoaded: boolean;

    private obligationDataService: ObligationsDataService;

    constructor(props: ObligationsUIProps) {
        super(props);

        //Init
        this.validators = [new ANDObligationSearchCriteriaValidator()];
        this.model = new ANDObligationSearchCriteria();
        this.model.mode = ANDObligationSearchMode.ObligedPerson;
        this.model.obligatedPersonType = ObligatedPersonType.Personal;
        this.obligationDataService = new ObligationsDataService();

        // binds
        this.onNewCheck = this.onNewCheck.bind(this);
        this.onSearch = this.onSearch.bind(this);
    }

    private onSearch() {
        if (this.validators[0].validate(this.model)) {

            this.props.registerAsyncOperation(this.obligationDataService.searchObligationsAND(this.model).then(result => {
                this.searchResult = result;
            }).catch(() => {
            }).finally(() => {
                this.searchHasBeenMade = true;
            }))
        }
    }

    public componentDidMount() {

        this.props.registerAsyncOperation(Nomenclatures.getServices().then(res => {

            runInAction(() => {

                this.isLoaded = true;

                if (res && res.length > 0) {
                    this.currentService = res.find(service => service.serviceUrl == window.location.pathname)
                }

                let params = new URLSearchParams(window.location.search);

                if ((params as any).size > 0) {
                    params.forEach((value, key) => {

                        if (this.model.hasOwnProperty('_' + key)) {
                            this.model['_' + key] = value;
                        }
                    });

                    this.onSearch();
                }
            })
        }))
    }

    @action private onNewCheck = () => {
        this.searchResult = null;
        this.searchHasBeenMade = false;
        this.props.routerExt.goTo(Constants.PATHS.Obligations, {})
    }

    private get getAISANDObligations() {
        if (this.searchHasBeenMade && this.searchResult?.obligationsData?.length > 0) {
            return this.searchResult?.obligationsData?.find(x => x.unitGroup == ObligationSearchResultUnitGroups.AIS_AND)
        }

        return null;
    }

    private get getNAIFNRBLDObligations() {
        if (this.searchHasBeenMade && this.searchResult?.obligationsData?.length > 0) {
            return this.searchResult?.obligationsData?.find(x => x.unitGroup == ObligationSearchResultUnitGroups.NAIF_NRBLD)
        }

        return null;
    }

    render() {

        if (!this.isLoaded) {
            return null
        }

        return <div className="page-wrapper" id="ARTICLE-CONTENT">
            <div className="ui-form ui-form--input">
                <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                {
                    this.currentService && !ObjectHelper.isStringNullOrEmpty(this.currentService.description)
                        ? <div className="w-100" dangerouslySetInnerHTML={{ __html: this.currentService.description }}></div>
                        : null
                }
                {
                    this.searchHasBeenMade
                        ? <>
                            <div className="section-title">
                                <h2 className="mt-0 mb-1">{this.getResource('GL_ObligationCheckResults_L')}</h2>
                                <p className="small font-weight-bold">{showUsedSearchCriteria(this.model)}</p>
                            </div>
                            {
                                ObjectHelper.isNullOrUndefined(this.searchResult)
                                    || (ObjectHelper.isNullOrUndefined(this.getAISANDObligations) && ObjectHelper.isNullOrUndefined(this.getNAIFNRBLDObligations))
                                    || (this.getAISANDObligations?.errorReadingData && this.getNAIFNRBLDObligations?.errorReadingData)
                                    ? <div className="alert alert-danger mt-4" role="alert">
                                        <p>{this.getResource('GL_UNPAID_ALL_DEBTS_E')}</p>
                                    </div>
                                    : <>
                                        <KATObligationsResultsUI searchCriteria={this.model} result={this.getAISANDObligations} />
                                        <BDSObligationsResultsUI searchCriteria={this.model} result={this.getNAIFNRBLDObligations} />
                                    </>
                            }
                        </>
                        : <ObligationsFormUI modelReference={new BindableReference(this.model, "", this.validators)} />
                }
                <div className="button-bar button-bar--form button-bar--responsive">
                    <div className="right-side">
                        {
                            this.searchHasBeenMade
                                ? <button type="button" className="btn btn-primary" onClick={this.onNewCheck}>{this.getResource('GL_NEW_CHECK_L')}</button>
                                : <button type="button" onClick={this.onSearch} className="btn btn-primary">{this.getResource("GL_CHECK_L")}</button>
                        }
                    </div>
                    <div className="left-side">
                        <Link className="btn btn-secondary" to={Constants.PATHS.Services}>{this.getResource("GL_REFUSE_L")}</Link>
                    </div>
                </div>
            </div>
        </div>
    }
}

export const ObligationsUI = withAsyncFrame(withRouter(ObligationsUIImpl), false)
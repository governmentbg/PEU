import { BindableReference } from "cnsys-core";
import { AsyncUIProps, BaseProps, BaseRouteProps, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { EAUBaseComponent, Nomenclatures, ObligationSearchCriteria, Pagination, Service } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import * as React from 'react';
import { Obligation } from '../../models/ModelsManualAdded';
import { ObligationsDataService } from '../../services/ObligationsDataService';
import MyEPaymentsResultRowUI from './MyEPaymentsResultRowUI';

interface MyEPaymentsProps extends BaseRouteProps<any>, BaseProps, AsyncUIProps {
}

@observer class MyEPaymentsUIImpl extends EAUBaseComponent<MyEPaymentsProps, ObligationSearchCriteria>{

    @observable private ePaymentsItems: Obligation[];
    @observable private isLoaded: boolean;
    @observable private isServicesLoaded: boolean;

    @observable private services: Service[];

    private obligationsDataService: ObligationsDataService;

    constructor(props: MyEPaymentsProps) {

        super(props);

        this.onPageChange = this.onPageChange.bind(this);
        this.init = this.init.bind(this);
        this.init();
    }

    render() {
       
        let dataResult: any = null;

        if (this.isLoaded && this.isServicesLoaded) {

            if (this.ePaymentsItems && this.ePaymentsItems.length > 0) {
                dataResult = <div className="page-wrapper" id="ARTICLE-CONTENT">

                    <Pagination activePage={this.model.page} count={this.model.count}
                        pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm"
                        onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />

                    <div className="table-responsive-block">
                        <table className="table table-hover" aria-label={this.getResource("GL_SERVICE_PAYMENTS_LIST_I")}>
                            <thead>
                                <tr>
                                    <th className="w-40">{this.getResource('GL_APPLICATION_L')}</th>
                                    <th>{this.getResource('GL_DESCRIPTION_L')}</th>
                                    <th>{this.getResource('GL_PAYMENT_PORTAL_L')}</th>
                                    <th>{this.getResource('GL_PAYMENT_DATE_L')}</th>
                                    <th className="text-right">{this.getResource('GL_AMOUNT_L')}</th>
                                </tr>
                            </thead>
                            <tbody>
                                {
                                    this.ePaymentsItems.map((ePaymentsItem, index) => 
                                        <MyEPaymentsResultRowUI modelReference={new BindableReference(ePaymentsItem)} services={this.services} key={index}/>
                                    )
                                }
                            </tbody>
                        </table>
                    </div>

                    <Pagination activePage={this.model.page} count={this.model.count}
                        pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm"
                        onSelect={this.onPageChange} />
                    
                </div>
            } else {
                dataResult = (<div className="page-wrapper" id="ARTICLE-CONTENT">
                                <div className="alert alert-dismissible alert-warning fade show">
                                    <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                                </div>
                            </div>);
            }
        }

        return <>
            {dataResult}
        </>
    }

    @action private onPageChange(page: any): void {
        this.model.page = page;
        this.props.registerAsyncOperation(this.searchMyEPaymentFunc());
    }

    private searchMyEPaymentFunc() {

        this.isLoaded = false;

        return this.obligationsDataService.searchObligations(this.model)
            .then(result => {this.ePaymentsItems = result;})
            .finally(() => {this.isLoaded = true; })
    }

    @action private init() {

        this.isLoaded = false;
        this.isServicesLoaded = false;
        this.ePaymentsItems = [];
        this.obligationsDataService = new ObligationsDataService();
        this.model = new ObligationSearchCriteria();
        this.model.page = 1;

        runInAction.bind(this)(() => {
            this.props.registerAsyncOperation(

                this.obligationsDataService.searchObligations(this.model)
                .then(result => { this.ePaymentsItems = result;})
                .then(() => {
                    Nomenclatures.getServices()
                        .then(res => {
                            if (res && res.length > 0) {
                                this.services = res;
                            }
                        })
                    .finally(() => this.isServicesLoaded = true)
                })
                .finally(() => this.isLoaded = true)
            )
        })
    }
}

export const MyEPaymentsUI = withRouter(withAsyncFrame(MyEPaymentsUIImpl))
import { AsyncUIProps, BaseProps, BaseRouteProps, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { EAUBaseComponent, ServiceInstanceSearchCriteria, ValidationSummaryErrors } from 'eau-core';
import { observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { ServiceInstancesDataService } from "../../services/ServiceInstancesDataService";
import { MyEServicesSearchFormUI } from './MyEServicesSearchFormUI';
import { MyEServicesSearchResultUI } from './MyEServicesSearchResultUI';

interface MyEServicesUIProps extends BaseRouteProps<any>, BaseProps, AsyncUIProps {

}

@observer class MyEServicesUIImpl extends EAUBaseComponent<MyEServicesUIProps, ServiceInstanceSearchCriteria>{
    @observable private eServicesItems = [];
    @observable private isAlreadySearched: boolean;
    @observable private currentSearchCriteria: ServiceInstanceSearchCriteria;
    @observable private searchCriteria: ServiceInstanceSearchCriteria;
    private serviceInstancesDataService: ServiceInstancesDataService;


    constructor(props: MyEServicesUIProps) {
        super(props);

        //Bind
        this.searchEMyServices = this.searchEMyServices.bind(this);
        this.updateEMyService = this.updateEMyService.bind(this);

        //Init
        this.serviceInstancesDataService = new ServiceInstancesDataService();
        this.currentSearchCriteria = undefined;

        this.searchCriteria = new ServiceInstanceSearchCriteria();
        this.model = this.searchCriteria;

        this.searchEMyServices(this.model);
        this.isAlreadySearched = false;
    }

    render() {
        return (<div className="page-wrapper" id="ARTICLE-CONTENT">
            <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
            <MyEServicesSearchFormUI {...this.bind(model => model, "ServiceInstanceSearchCriteria")} searchEServicesFunc={this.searchEMyServices} />

            {(this.eServicesItems && this.eServicesItems.length > 0)
                ? <MyEServicesSearchResultUI
                    {...this.bind(this.currentSearchCriteria, "ServiceInstanceSearchCriteria")}
                    eServiceList={this.eServicesItems}
                    searchEServicesFunc={this.searchEMyServices}
                    updateEServiceFunc={this.updateEMyService} />
                : (this.isAlreadySearched === true)
                    ? <div className="alert alert-dismissible alert-warning fade show">
                        <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                    </div>
                    : null
            }
        </div>);
    }

    private searchEMyServices(searchCriteria: ServiceInstanceSearchCriteria, newSearch: boolean = true, searchFromButton: boolean = false) {
        let that = this;

        if (searchFromButton == true && this.serviceInstancesDataService) {
            this.props.registerAsyncOperation(this.serviceInstancesDataService.searchServiceInstances(searchCriteria).then((eservices) => {
                runInAction(() => {
                    that.isAlreadySearched = true;
                    this.eServicesItems = eservices;
                    if (newSearch) {
                        that.currentSearchCriteria = searchCriteria.clone();
                    }
                });
            }));
        }
    }

    private updateEMyService(eServicesItem: any): any {
        //if (this.validator.validate(eServicesItem)) {
        //    return this.nomenclatureDataService.updateLabel(eServicesItem);
        //}
        return null;
    }
}

export const MyEServicesUI = withRouter(withAsyncFrame(MyEServicesUIImpl, false));
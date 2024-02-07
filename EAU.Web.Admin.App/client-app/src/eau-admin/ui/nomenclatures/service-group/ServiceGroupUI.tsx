import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { ServiceGroup, EAUBaseComponent, ServiceGroupSearchCriteria, Pagination, ValidationSummaryErrors} from 'eau-core';
import { action, observable  } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Link } from 'react-router-dom';
import {ObjectHelper, BindableReference} from "cnsys-core";
import { Table } from 'reactstrap';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';
import {Constants} from '../../../Constants';
import ServiceGroupsListUi from './ServiceGroupsListUI';


interface GroupListProps extends BaseProps, AsyncUIProps {
}

@observer 
class ServiceGroupImpl extends EAUBaseComponent<GroupListProps, ServiceGroupSearchCriteria>{

    @observable private serviceGroup: ServiceGroup[] = [];
    @observable  isChangedActivity = null;
    @observable isLoaded: boolean;

    private nomenclatureDataService: NomenclaturesDataService;
    
    constructor(props) {
        super(props);
        this.nomenclatureDataService = new NomenclaturesDataService();

        this.onPageChange = this.onPageChange.bind(this);
        this.searchServiceGroups = this.searchServiceGroups.bind(this);
        this.onChangeStatusCallback = this.onChangeStatusCallback.bind(this);
        this.init();
    }

    render() {
        
        let dataResult: any = null;

        if (this.isLoaded) {
            
            if (!ObjectHelper.isNullOrUndefined(this.serviceGroup)) {
                dataResult = <div className="card">
                    <div className="card-body">
                        <ValidationSummaryErrors {...this.props} />
                    {!ObjectHelper.isNullOrUndefined(this.isChangedActivity) && <div className="alert alert-success"><p>{this.isChangedActivity ? this.getResource("GL_DEACTIVE_OK_I") : this.getResource("GL_ACTIVE_OK_I")}</p></div> }
                        
                        <div className="card-navbar">
                            <div className="button-bar">
                                <div className="right-side">
                                    <Link to={Constants.PATHS.NomAddServiceGroup}>
                                        <button className="btn btn-primary"><i className="ui-icon ui-icon-plus-white"></i> {this.getResource("GL_ADD_SERVICE_GROUP_L")}</button> 
                                    </Link>
                                </div>
                                <div className="left-side"></div>
                            </div>
                        </div>

                        <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                        
                        <div className="table-responsive">
                            <Table bordered striped hover>
                                <thead>
                                    <tr>
                                        <th>{this.getResource("GL_NUMBER_IN_GROUP_L")}</th>
                                        <th>{this.getResource("GL_NAME_SERVICE_GROUP_L")}</th>
                                        <th>{this.getResource("GL_STATUS_L")}</th>
                                        <th>{this.getResource("GL_IMAGE_L")}</th>
                                        <th>{this.getResource("GL_CREATE_UPDATE_DATE_L")}</th>
                                        <th>{this.getResource("GL_ACTIONS_L")}</th>
                                    </tr>
                                </thead>
                                <tbody>
                                
                                {this.serviceGroup.map((serviceGroup, index) => <tr key={serviceGroup.groupID + "_" + index}>
                                    <ServiceGroupsListUi modelReference={new BindableReference(serviceGroup)} onChangeStatusCallback={this.onChangeStatusCallback} />
                                </tr>)}
                               
                                </tbody>
                            </Table>
                         </div>
                         <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} />
                    </div>
                </div>
            } 
            else {
                dataResult = <div className="alert alert-dismissible alert-warning fade show">
                    <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                </div>
            }
        }

        return <>
            {dataResult}
        </>
    }

    onChangeStatusCallback(serviceGroup: ServiceGroup) {
            this.isChangedActivity = null;
            
            serviceGroup.isActive ? 
            this.props.registerAsyncOperation(this.nomenclatureDataService.deactivateServiceGroup(serviceGroup)
                    .then(() => {
                        this.isChangedActivity = !serviceGroup.isActive;
                    }))
                : 
                this.props.registerAsyncOperation(this.nomenclatureDataService.activateServiceGroup(serviceGroup)
                    .then(() => {
                        this.isChangedActivity = !serviceGroup.isActive;
                    }))
    }

    @action private onPageChange(page: any): void {
        this.isChangedActivity = null;
        this.model.page = page;
        this.props.registerAsyncOperation(this.searchServiceGroups());
    }

    @action private searchServiceGroups() {
        this.isLoaded = false;
        this.isChangedActivity = null;
        
        return this.nomenclatureDataService.searchServiceGroups(this.model).then(result => {
            this.serviceGroup = result;
        })
        .finally(() => {
            this.isLoaded = true;
        })
    }

    @action private init() { 
        this.isLoaded = false;
        this.model = new ServiceGroupSearchCriteria();

        this.props.registerAsyncOperation(
            this.nomenclatureDataService.searchServiceGroups(this.model)
            .then(result => { this.serviceGroup = result})
            .finally(() => {this.isLoaded = true})
        );
    }
}

export const ServiceGroupUI = withAsyncFrame(ServiceGroupImpl, false); 

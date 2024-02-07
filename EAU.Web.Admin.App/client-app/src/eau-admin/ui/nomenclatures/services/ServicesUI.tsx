import * as React from "react";
import { observer } from "mobx-react";
import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { DocumentType, EAUBaseComponent, Nomenclatures, Pagination, Service, ValidationSummaryErrors } from "eau-core";
import { ServicesSearchFormUI } from "./ServicesSearchFormUI";
import { ServicesResultsUI } from "./ServicesResultsUI";
import { action, observable, runInAction } from "mobx";
import { NomenclaturesDataService } from "../../../services/NomenclaturesDataService";
import { ServiceSearchCriteria } from "eau-core";
import { Link } from "react-router-dom";
import { Constants } from "../../../Constants";
import { SelectListItem } from "cnsys-core";


interface ServicesUIProps extends BaseProps, AsyncUIProps {
}

@observer
class ServicesUIImpl extends EAUBaseComponent<ServicesUIProps, ServiceSearchCriteria> {

    @observable private services: Service[];
    @observable private selectedGroupItems: SelectListItem[];
    @observable private isLoaded: boolean;

    private nomenclatureDataService: NomenclaturesDataService;

    constructor(props: ServicesUIProps) {
        super(props);

        this.searchServices = this.searchServices.bind(this);
        this.onPageChange = this.onPageChange.bind(this);

        this.init();
    }

    render() {
        return <>
            <div className="card">
                <div className="col-12">
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                </div>
                    <ServicesSearchFormUI {...this.bind(model => model, "ServiceSearchCriteria")} searchServices={this.searchServices} selectedGroupItems={this.selectedGroupItems} />
            </div>
            <div className="card"> 
                <div className="card-body">
                    <div className="card-navbar">
                        <div className="button-bar">
                            <div className="right-side">
                                <Link to={Constants.PATHS.NomAddService} className="btn btn-primary"><i className="ui-icon ui-icon-plus-white"></i>&nbsp;
                                    {this.getResource("GL_ADD_SERVICE_L")}</Link>
                            </div>
                            <div className="left-side">
                            </div>
                        </div>
                    </div>

                    <Pagination activePage={this.model.page} count={this.model.count}
                        pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm"
                        onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                    <ServicesResultsUI services={this.services} isLoaded={this.isLoaded} />
                    <Pagination activePage={this.model.page} count={this.model.count}
                        pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm"
                        onSelect={this.onPageChange} />
                </div>
            </div>
        </>
    }

    @action
    private init() {
        this.services = [];
        this.nomenclatureDataService = new NomenclaturesDataService();
        this.model = new ServiceSearchCriteria();
        this.model.attachedDocumentType = new DocumentType();
        this.getSelectedGroupItems();

        this.searchServices(this.model);
    }


    private searchServices(serviceSearchCriteria: ServiceSearchCriteria) {
        this.props.registerAsyncOperation(this.nomenclatureDataService.searchServices(serviceSearchCriteria).then(services => {
            runInAction.bind(this)(() => {
                this.services = services;
                this.isLoaded = true;
            })
        }))
    }

    @action
    private onPageChange(page: any): void {
        this.model.page = page;
        this.searchServices(this.model);
    }

    private getSelectedGroupItems() {
        this.props.registerAsyncOperation(Nomenclatures.getServicesGroups().bind(this).then(serviceGroups => {
            runInAction.bind(this)(() => {
                this.selectedGroupItems = [];
                serviceGroups.forEach(group => {
                    this.selectedGroupItems.push(new SelectListItem({ selected: false, text: group.name, value: group.groupID }))
                });
            })

        }))
    }
}

export const ServicesUI = withAsyncFrame(ServicesUIImpl, false);
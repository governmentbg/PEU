import { AsyncUIProps, withAsyncFrame, withRouter, BaseRoutePropsExt, BaseRouteProps } from 'cnsys-ui-react';
import { DocumentType, EAUBaseComponent, ServiceSearchCriteria, Service, Language, Pagination, ValidationSummaryErrors} from 'eau-core';
import { action, observable, runInAction  } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import {ObjectHelper} from "cnsys-core";
import { Table } from 'reactstrap';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';
import TranslationLangUI from '../../../ui/common/TranslationLangUI';
import { ServiceI18nVM } from '../../../models/ServiceI18nVM';
import { ServicesListI18nUI } from './ServicesI18nListUI';

interface ServicesProps extends BaseRouteProps<any>, AsyncUIProps, BaseRoutePropsExt {
}

@observer 
class ServicesI18nUIImpl extends EAUBaseComponent<ServicesProps, ServiceSearchCriteria>{

    @observable isLoaded: boolean;

    @observable public langCode: string;
    @observable public langName: string;

    private nomenclaturesDataService: NomenclaturesDataService;
    @observable private services:Service[];
    @observable private servicesi18n: Service[] = [];
    @observable private servicesI18nVMArr: ServiceI18nVM[] = [];

    constructor(props) {
        super(props)
        this.nomenclaturesDataService = new NomenclaturesDataService();
        this.funcBinds();
    }

    funcBinds() {
        this.loadCurrentLanguage = this.loadCurrentLanguage.bind(this);
        this.onLangSelect = this.onLangSelect.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
    }
    
    render() {

        let dataResult: any  = null;

        if (this.isLoaded) {
            if ((!ObjectHelper.isArrayNullOrEmpty(this.servicesI18nVMArr))) {
                dataResult = <>
                        {/*<ValidationSummary {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                        {!ObjectHelper.isNullOrUndefined(this.isChangedActivity) && <div className="alert alert-success"><p>{this.isChangedActivity ? this.getResource("GL_ACTIVE_OK_I") : this.getResource("GL_DEACTIVE_OK_I")}</p></div> }*/}
                            <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                            <ServicesListI18nUI services={this.servicesI18nVMArr} langName={this.langName} />
                            <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} />
                    </>
            }
            else {
                dataResult = <div className="alert alert-dismissible alert-warning fade show">
                        {this.getResource("GL_NO_DATA_FOUND_L")}
                    </div>
            }
        }
        
        return <> 
            <TranslationLangUI onLangSelect={this.onLangSelect} loadCurrentLanguage={this.loadCurrentLanguage} />
            <div className="card">
                <div className="col-12">
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                </div>

                <div className="card-body">
                    {dataResult}
                    </div>
            </div>  
        </>
    }

    @action private searchServices() {

        this.isLoaded = false;
        
        return this.nomenclaturesDataService.searchServices(this.model).then(result => {
            this.services = result;
        })
        .finally(() => {
            this.isLoaded = true;
        })
    }

    /**
     * Генерира нов ViewModel за списък с езици
     */
    @action createList() {
        
        this.servicesI18nVMArr = [];
        
        this.services.forEach((service) => {
            let serviceI18nVM = new ServiceI18nVM;
            serviceI18nVM.serviceID = service.serviceID;
            serviceI18nVM.bgName = service.name;
            serviceI18nVM.bgDescription = service.description;
            serviceI18nVM.bgAttachedDocumentsDescription = service.attachedDocumentsDescription;
            serviceI18nVM.langCode = this.langCode;
            
            let i18nObj = this.servicesi18n.filter(d => d.serviceID == service.serviceID);
            serviceI18nVM.isTranslated = (!ObjectHelper.isNullOrUndefined(i18nObj[0].name) && !ObjectHelper.isNullOrUndefined(i18nObj[0].description)) ? true : false;
            serviceI18nVM.name = i18nObj[0].name;
            serviceI18nVM.description = i18nObj[0].description;
            this.servicesI18nVMArr.push(serviceI18nVM);
        })
    }

    @action private onPageChange(page: any): void {
        this.model.page = page;
        this.props.registerAsyncOperation(
            this.nomenclaturesDataService.searchServices(this.model, this.langCode, true)
            .then(result => {this.servicesi18n = result})
            .then(() => 
                this.nomenclaturesDataService.searchServices(this.model, "bg")
                .then(result => {this.services = result}))
                .then(() => {this.createList()})
                .finally(() => this.isLoaded = true)
        );
    }


    @action private onLangSelect(lang:Language) {
       
        this.langCode = lang.code;
        this.langName = lang.name;

        let searchCriteria = new ServiceSearchCriteria();
        searchCriteria.pageSize = Number.MAX_SAFE_INTEGER;
        searchCriteria.page = 1;

        searchCriteria.attachedDocumentType = new DocumentType();

        this.props.registerAsyncOperation (
            this.nomenclaturesDataService.searchServices(searchCriteria, this.langCode).then(
                (result) => this.servicesi18n = result
            )
            .then(() => {this.createList()})
            .finally(() => this.isLoaded = true)
        )
    }

    @action loadCurrentLanguage(langCode: Language) {
        this.langCode = langCode.code;
        this.langName = langCode.name;
        this.init();
    }

    @action private init() { 

        this.isLoaded = false;
        this.model = new ServiceSearchCriteria();
        this.model.forceTranslated = true;

        this.model.attachedDocumentType = new DocumentType();

        this.props.registerAsyncOperation(
            this.nomenclaturesDataService.searchServices(this.model, this.langCode, true)
            .then(result => {this.servicesi18n = result})
            .then(() => 
                this.nomenclaturesDataService.searchServices(this.model, "bg")
                .then(result => {this.services = result}))
                .then(() => {this.createList()})
                .finally(() => this.isLoaded = true)
        );
    }
}

export const ServicesI18nUI = withRouter(withAsyncFrame(ServicesI18nUIImpl, false)); 
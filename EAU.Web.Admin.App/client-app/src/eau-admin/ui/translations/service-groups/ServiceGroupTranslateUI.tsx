import { AsyncUIProps, withAsyncFrame, BaseProps } from 'cnsys-ui-react';
import { ServiceGroup, EAUBaseComponent, ServiceGroupSearchCriteria, Language, Pagination, ValidationSummaryErrors} from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { ObjectHelper, BindableReference } from 'cnsys-core';
import { Table } from 'reactstrap';
import ServiceGroupsListTranslateUI from './ServiceGroupsListTranslateUI';
import { ServiceGroupI18nVM } from '../../../models/ServiceGroupI18nVM';
import TranslationLangUI from '../../../ui/common/TranslationLangUI';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';
import { ServiceGroupI18nValidator } from '../../../validations/ServiceGroupI18nValidator';

interface GroupListProps extends BaseProps, AsyncUIProps {
}

@observer 
class ServiceGroupTranslateUIImpl extends EAUBaseComponent<GroupListProps, ServiceGroupSearchCriteria>{

    @observable private serviceGroup: ServiceGroup[] = [];
    @observable private serviceGroupi18n: ServiceGroup[] = [];
    @observable private serviceGroupI18nVMArr: ServiceGroupI18nVM[] = [];

    @observable public langCode: string;
    @observable public langName: string;

    @observable isLoaded: boolean;
    @observable isUpdated: boolean;
    
  
    private nomenclatureDataService: NomenclaturesDataService;
    
    constructor(props) {
        super(props);
        this.nomenclatureDataService = new NomenclaturesDataService();
        
        this.validators = [new ServiceGroupI18nValidator()];
        
        this.isUpdated = false;
        this.funcBinds();
       
    }

    /**
     * Добавя/обнвява превод на група с услуги
     * @param serviceGroupI18n 
     */
    onSave(serviceGroupI18n: ServiceGroupI18nVM) {
        if (this.validators[0].validate(serviceGroupI18n)) {
            if (serviceGroupI18n.isTranslated) {
                this.props.registerAsyncOperation(this.nomenclatureDataService.updateServiceGroupi18n(serviceGroupI18n)
                .then(() => {this.isUpdated = true}))
            }
            else {
                this.props.registerAsyncOperation(this.nomenclatureDataService.addServiceGroupi18n(serviceGroupI18n)
                .then(() => {this.isUpdated = true}))
            }
        }
    }

    @action private init() { 

        this.isLoaded = false;
        this.model = new ServiceGroupSearchCriteria();
        this.model.forceTranslated = true;

        this.props.registerAsyncOperation(
            this.nomenclatureDataService.searchServiceGroups(this.model, this.langCode, true)
            .then(result => {this.serviceGroupi18n = result})
            .then(() => 
                this.nomenclatureDataService.searchServiceGroups(this.model, "bg")
                .then(result => {this.serviceGroup = result}))
                .then(() => {this.createList()})
                .finally(() => this.isLoaded = true)
        );
    }

    funcBinds() {
        this.loadCurrentLanguage = this.loadCurrentLanguage.bind(this);
        this.onLangSelect = this.onLangSelect.bind(this);
        this.onSave = this.onSave.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
        this.switchEditMode = this.switchEditMode.bind(this);
    }

    @action loadCurrentLanguage(langCode: Language) {
        this.langCode = langCode.code;
        this.langName = langCode.name;
        this.init();
    }

    /**
     * Обновява списъка с езици при смяна на език
     * @param lang 
     */
    @action private onLangSelect(lang:Language) {
       
        this.langCode = lang.code;
        this.langName = lang.name;
        
        this.isLoaded = false;
        this.isUpdated = false;
       
        this.model.forceTranslated = true;
        this.serviceGroupI18nVMArr = [];
        this.props.registerAsyncOperation (
            this.nomenclatureDataService.searchServiceGroups(this.model, this.langCode, true).then(
                (result) => this.serviceGroupi18n = result
            )
            .then(() => {this.createList()})
            .finally(() => this.isLoaded = true)
        )
    }
   

    /**
     * Генерира нов ViewModel за списък с езици
     */
    @action createList() {
        
        this.serviceGroupI18nVMArr = [];

        this.serviceGroup.forEach((serviceGroup) => {
           
            let serviceGroupI18nVM = new ServiceGroupI18nVM;
            serviceGroupI18nVM.groupID = serviceGroup.groupID;
            serviceGroupI18nVM.bgName = serviceGroup.name;
            serviceGroupI18nVM.languageCode = this.langCode;

            let nameI18nObj = this.serviceGroupi18n.filter(d => d.groupID == serviceGroup.groupID);
            serviceGroupI18nVM.name = !ObjectHelper.isArrayNullOrEmpty(nameI18nObj) ? nameI18nObj[0].name : "";
            serviceGroupI18nVM.isTranslated = nameI18nObj[0].isTranslated;
            this.serviceGroupI18nVMArr.push(serviceGroupI18nVM);
        })

    }

    @action private onPageChange(page: any): void {
        this.model.page = page;
        this.props.registerAsyncOperation(
            this.nomenclatureDataService.searchServiceGroups(this.model, this.langCode, true)
            .then(result => {this.serviceGroupi18n = result})
            .then(() => 
                this.nomenclatureDataService.searchServiceGroups(this.model, "bg")
                .then(result => {this.serviceGroup = result}))
                .then(() => {this.createList()})
                .finally(() => {
                    runInAction.bind(this)(() => {
                        this.isLoaded = true; 
                        this.isUpdated = false
                    });
                   
                })
        );
    }

    private switchEditMode(editMode) {
        if (!editMode)
            this.isUpdated = false;
        
        return !editMode;
    }

    render() {

        let dataResult: any = null;
        
        if (this.isLoaded) {

            if ((!ObjectHelper.isArrayNullOrEmpty(this.serviceGroupI18nVMArr))) {
                dataResult = <>
                    <div className="table-responsive">
                        <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                        <Table bordered striped hover>
                            <thead>
                                <tr>
                                    <th>{this.getResource("GL_NAME_SERVICE_GROUP_L")}</th>
                                    <th>{this.getResource("GL_NAME_SERVICE_GROUP_L")} <span className="label-description">({this.langName})</span></th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.serviceGroupI18nVMArr.map((serviceGroup) => 
                                <ServiceGroupsListTranslateUI  modelReference={new BindableReference(serviceGroup)} switchEditMode={this.switchEditMode} key={serviceGroup.groupID} onSave={this.onSave} />)}
                            </tbody>
                        </Table>
                    </div>
                </>
            }
            else {
                dataResult = <div className="alert alert-dismissible alert-warning fade show">
                        <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                    </div>
            }
        }
        
        return <>
            <TranslationLangUI onLangSelect={this.onLangSelect} loadCurrentLanguage={this.loadCurrentLanguage} />
            <div className="card">
                <div className="card-body">
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                    {this.isUpdated && <div className="alert alert-success"><p>{this.getResource("GL_UPDATE_OK_I")}</p></div> }
                    {dataResult}
                </div>
            </div>
        </>
    }
}


export const ServiceGroupTranslateUI = withAsyncFrame(ServiceGroupTranslateUIImpl, false); 

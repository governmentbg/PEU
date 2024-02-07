import { ObjectHelper } from "cnsys-core";
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { EAUBaseComponent, Language, Service, TextEditorUI, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Constants } from '../../../Constants';
import { ServiceI18nVM } from '../../../models/ServiceI18nVM';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';
import TranslationLangUI from '../../../ui/common/TranslationLangUI';
import { ServiceI18nValidator } from '../../../validations/ServiceI18nValidator';
import BtnGroupFormUI from '../../common/BtnGroupFormUI';


interface ServicePagesProps extends BaseRouteParams {
    serviceID: number
}

interface ServicesProps extends BaseRouteProps<ServicePagesProps>, AsyncUIProps, BaseRoutePropsExt {
}

@observer class ServicesI18nForm extends EAUBaseComponent<ServicesProps, ServiceI18nVM> {

    private serviceID;
    @observable isFormSubmited: boolean = false;
    @observable isLoaded: boolean;

    @observable public langCode: string;
    @observable public langName: string;

    @observable private service: Service;
    @observable private servicei18n: Service;

    private nomenclaturesDataService: NomenclaturesDataService;

    constructor(props?: ServicesProps) {
        super(props);

        this.serviceID = this.props.match.params.serviceID;
        this.nomenclaturesDataService = new NomenclaturesDataService();
        this.isLoaded = false;

        this.funcBinds();
    }

    @action private init() {

        this.model = new ServiceI18nVM();

        if (this.serviceID) {

            runInAction.bind(this)(() => {
                this.props.registerAsyncOperation(this.nomenclaturesDataService.getServiceById(this.serviceID, this.langCode)
                    .then((result) => {
                        this.servicei18n = result
                    })
                    .then(() => this.nomenclaturesDataService.getServiceById(this.serviceID, "bg"))
                    .then((result) => this.service = result)
                    .then(() => { this.createI18nVM() })
                    .finally(() => this.isLoaded = true));
            })
        }

        this.validators = [new ServiceI18nValidator()];
    }

    /**
     * Генерира нов ViewModel за списък с езици
     */
    @action createI18nVM() {

        if (ObjectHelper.isArrayNullOrEmpty(this.service))
            this.model = null;

        else {
            let serviceI18nVM = new ServiceI18nVM;
            serviceI18nVM.serviceID = this.service.serviceID;
            serviceI18nVM.bgName = this.service.name;
            serviceI18nVM.bgDescription = this.service.description;
            serviceI18nVM.bgAttachedDocumentsDescription = this.service.attachedDocumentsDescription;
            serviceI18nVM.langCode = this.langCode;
            serviceI18nVM.name = this.servicei18n.name;
            serviceI18nVM.description = this.servicei18n.description;
            serviceI18nVM.attachedDocumentsDescription = this.servicei18n.attachedDocumentsDescription;
            serviceI18nVM.isTranslated = this.servicei18n.isTranslated;
            this.model = serviceI18nVM;
        }
    }

    funcBinds() {
        this.loadCurrentLanguage = this.loadCurrentLanguage.bind(this);
        this.onLangSelect = this.onLangSelect.bind(this);
        this.onSave = this.onSave.bind(this);
    }

    @action loadCurrentLanguage(langCode: Language) {
        this.langCode = langCode.code;
        this.langName = langCode.name;
        this.init();
    }

    /**
     * Обновява списъка със страници при смяна на език
     * @param lang 
     */
    @action private onLangSelect(lang: Language) {

        this.langCode = lang.code;
        this.langName = lang.name;
        this.isLoaded = false;

        this.props.registerAsyncOperation(
            this.nomenclaturesDataService.getServiceById(this.serviceID, this.langCode).then(
                (result) => this.servicei18n = result
            )
                .then(() => { this.createI18nVM() })
                .finally(() => this.isLoaded = true)
        )
    }

    render() {

        let dataResult: any = null;

        if (this.isLoaded) {

            if (this.model) {

                dataResult =
                    <div className="card">
                        <div className="card-body">
                            {this.isFormSubmited && <div className="alert alert-success"><p>{this.getResource("GL_SAVE_OK_I")}</p></div>}
                            <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                            <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                            <div className="row">
                            <div className="col-sm-12 form-group">
                                <label htmlFor="SERVICE_NAME">{this.getResource("GL_SERVICE_NAME_L")}</label>
                                    <div>{this.model.bgName}</div>
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-sm-12 col-md-6 form-group">
                                    {this.labelFor(x => x.name, "GL_SERVICE_NAME_L")} <span className="label-description">({this.langName})</span>
                                    {this.textAreaFor(x => x.name, null, 3,
                                        {
                                            name: "name",
                                            className: "form-control",
                                            maxLength: 1000,
                                            id: 'name'
                                        })}
                                </div>
                            </div>
                            <div className="row">
                                <div className="form-group col-12">
                                    <label htmlFor="DESCRIPTION">{this.getResource("GL_DESCRIPTION_L")}</label>
                                    <div className="form-text-scrollable-large public-portal-css">
                                        <div className="card card-page">
                                            <div className="card-body card-page__body">
                                                {<div className="form-text-srollable-large" dangerouslySetInnerHTML={{ __html: this.model.bgDescription }} />}
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <br />
                            <div className="row">
                                <div className="form-group col-12">
                                {this.labelFor(x => x.description, "GL_DESCRIPTION_L")}<span className="label-description"> ({this.langName})</span>
                                <div><TextEditorUI {...this.bind(m => m.description)} /></div>
                                </div>
                            </div>
                            <div className="row">
                            <div className="col-sm-12 form-group">
                                <label htmlFor="DESCRIPTION_DOC_ATTACHED">{this.getResource("GL_DESCRIPTION_DOC_ATTACHED_L")}</label>
                                    {<div className="form-text-srollable-large" dangerouslySetInnerHTML={{ __html: this.model.bgAttachedDocumentsDescription }} />}
                                </div>
                            </div>
                            <div className="row">
                                <div className="form-group col-12">
                                {this.labelFor(x => x.attachedDocumentsDescription, "GL_DESCRIPTION_DOC_ATTACHED_L")}<span className="label-description"> ({this.langName})</span>
                                <div><TextEditorUI {...this.bind(m => m.attachedDocumentsDescription)} /></div>
                                </div>
                            </div>
                        </div>
                        <BtnGroupFormUI refuseLink={Constants.PATHS.TranslationsServices} onSave={this.onSave} />
                    </div>
            }
            else {
                dataResult = <div className="alert alert-dismissible alert-warning fade show">
                    <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                </div>
            }
        }

        return <>
            <TranslationLangUI onLangSelect={this.onLangSelect} loadCurrentLanguage={this.loadCurrentLanguage} />
            {dataResult}
        </>
    }

    private onSave() {

        this.isFormSubmited = false;

        if (this.validators[0].validate(this.model)) {

            if (this.model.isTranslated) {
                this.props.registerAsyncOperation(
                    this.nomenclaturesDataService.updateI18nService
                        (this.model)
                        .then(() => {
                            runInAction.bind(this)(() => {
                                this.isFormSubmited = true;
                            })
                        })
                );
            }
            else {
                this.props.registerAsyncOperation(
                    this.nomenclaturesDataService.addI18nService(this.model)
                        .then(() => {
                            runInAction.bind(this)(() => {
                                this.isFormSubmited = true;
                                this.model.isTranslated = true;
                            })
                        })
                );
            }
        }
    }

}

export const ServicesI18nFormUI = withRouter(withAsyncFrame(ServicesI18nForm)); 

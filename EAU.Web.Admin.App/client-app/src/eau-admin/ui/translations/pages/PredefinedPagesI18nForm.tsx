import { BindableReference, ObjectHelper } from "cnsys-core";
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { EAUBaseComponent, Language, Page, TextEditorUI, ValidationSummary, ValidationSummaryStrategy } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Constants } from '../../../Constants';
import { PageI18nVM } from '../../../models/PageI18nVM';
import { CmsDataService } from '../../../services/CmsDataService';
import TranslationLangUI from '../../../ui/common/TranslationLangUI';
import BtnGroupFormUI from '../../common/BtnGroupFormUI';

interface CmsPagesProps extends BaseRouteParams {
    pageID: number
}

interface PagesProps extends BaseRouteProps<CmsPagesProps>, AsyncUIProps, BaseRoutePropsExt {
}

@observer class PredefinedPagesI18nForm extends EAUBaseComponent<PagesProps, PageI18nVM> {

    private pageID;
    @observable isFormSubmited: boolean = false;
    @observable isLoaded: boolean;

    @observable public langCode: string;
    @observable public langName: string;

    @observable private page: Page;
    @observable private pagei18n: Page;

    private cmsDataService: CmsDataService;

    constructor(props?: PagesProps) {
        super(props);

        this.pageID = this.props.match.params.pageID;
        this.cmsDataService = new CmsDataService();
        this.isLoaded = false;

        this.funcBinds();
    }

    @action private init() {

        this.model = new PageI18nVM();

        if (this.pageID) {

            runInAction.bind(this)(() => {
                this.props.registerAsyncOperation(this.cmsDataService.getPageById(this.pageID, this.langCode)
                    .then((result) => {
                        this.pagei18n = result
                    })
                    .then(() => this.cmsDataService.getPageById(this.pageID, "bg"))
                    .then((result) => this.page = result)
                    .then(() => { this.createI18nVM() })
                    .finally(() => this.isLoaded = true));
            })
        }
    }

    /**
     * Генерира нов ViewModel за списък с езици
     */
    @action createI18nVM() {

        if (ObjectHelper.isArrayNullOrEmpty(this.page))
            this.model = null;

        else {
            let pageI18nVM = new PageI18nVM;
            pageI18nVM.pageID = this.page.pageID;
            pageI18nVM.bgTitle = this.page.title;
            pageI18nVM.bgContent = this.page.content;
            pageI18nVM.languageCode = this.langCode;
            pageI18nVM.title = this.pagei18n.title;
            pageI18nVM.content = this.pagei18n.content;
            pageI18nVM.isTranslated = this.pagei18n.isTranslated;
            this.model = pageI18nVM;
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
            this.cmsDataService.getPageById(this.pageID, this.langCode).then(
                (result) => this.pagei18n = result
            )
                .then(() => { this.createI18nVM() })
                .finally(() => this.isLoaded = true)
        )
    }

    render() {

        let dataResult: any = null;

        if (this.isLoaded) {

            if (this.model) {

                dataResult = <div className="card">
                    <form id="groupListForm">
                        <div className="card-body">
                            {this.isFormSubmited && <div className="alert alert-success"><p>{this.getResource("GL_SAVE_OK_I")}</p></div>}
                            <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeAllExcept} />
                            <div className="row">
                                <div className="col-sm-12 form-group">
                                    <label htmlFor="HTML_PAGE_TITLE">{this.getResource("GL_HTML_PAGE_TITLE_L")}</label>
                                    <div>{this.model.bgTitle}</div>
                                </div>
                            </div>
                            <div className="row">
                                <div className="form-group col-sm-9">
                                    {this.labelFor(x => x.title, "GL_HTML_PAGE_TITLE_L")} <span className="label-description">({this.langName})</span>
                                    {this.textBoxFor(x => x.title)}
                                </div>
                            </div>
                            <label htmlFor="CONTENT">{this.getResource("GL_CONTENT_L")}</label>
                            <div className="form-text-scrollable-large public-portal-css">
                                <div className="card card-page">
                                    <div className="card-body card-page__body">
                                        <div className="form-text-srollable-large" dangerouslySetInnerHTML={{ __html: this.model.bgContent }} />
                                    </div>
                                </div>
                            </div>
                            <br />
                            <div className="row">
                                <div className="form-group col-sm-12">
                                    {this.labelFor(x => x.content, "GL_CONTENT_L")}<span className="label-description"> ({this.langName})</span>
                                    <div><TextEditorUI {...this.bind(x => x.content)} /></div>
                                </div>
                            </div>
                        </div>
                        <BtnGroupFormUI refuseLink={Constants.PATHS.TranslationsPages} onSave={this.onSave} />
                    </form>
                </div>
            } else {
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

        if (this.model.isTranslated) {
            this.props.registerAsyncOperation(
                this.cmsDataService.updateI18nPage(this.model)
                    .then(() => {
                        runInAction.bind(this)(() => {
                            this.isFormSubmited = true;
                        })
                    })
            );
        } else {
            this.props.registerAsyncOperation(
                this.cmsDataService.addI18nPage(this.model)
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

export const PredefinedPagesI18nFormUI = withRouter(withAsyncFrame(PredefinedPagesI18nForm)); 
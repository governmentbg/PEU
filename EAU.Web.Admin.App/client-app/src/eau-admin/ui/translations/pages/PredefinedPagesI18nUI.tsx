import { AsyncUIProps, withAsyncFrame, withRouter, BaseRoutePropsExt, BaseRouteProps } from 'cnsys-ui-react';
import { ServiceGroup, EAUBaseComponent, PageSearchCriteria, Page, Language, ValidationSummaryErrors } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { ObjectHelper } from "cnsys-core";
import { Table } from 'reactstrap';
import { CmsDataService } from '../../../services/CmsDataService';
import TranslationLangUI from '../../../ui/common/TranslationLangUI';
import { PageI18nVM } from '../../../models/PageI18nVM';
import { PredefinedPagesListI18nUI } from './PredefinedPagesI18nListUI';

interface PredefinedPagesProps extends BaseRouteProps<any>, AsyncUIProps, BaseRoutePropsExt {
}

@observer
class PredefinedPagesI18nUIImpl extends EAUBaseComponent<PredefinedPagesProps, any>{

    @observable private serviceGroup: ServiceGroup[] = [];
    @observable isLoaded: boolean;

    @observable public langCode: string;
    @observable public langName: string;

    private cmsDataService: CmsDataService;
    @observable private cmsPages: Page[];
    @observable private cmsPagesi18n: Page[] = [];
    @observable private cmsPagesI18nVMArr: PageI18nVM[] = [];

    constructor(props) {
        super(props)
        this.cmsDataService = new CmsDataService();
        this.funcBinds();
    }

    funcBinds() {
        this.loadCurrentLanguage = this.loadCurrentLanguage.bind(this);
        this.onLangSelect = this.onLangSelect.bind(this);
    }

    render() {

        let dataResult: any = null;

        if (this.isLoaded) {
            if (!ObjectHelper.isNullOrUndefined(this.serviceGroup)) {
                dataResult =
                    <div className="table-responsive">
                        <Table bordered striped hover>
                            <thead>
                                <tr>
                                    <th>{this.getResource("GL_HTML_PAGE_TITLE_L")}</th>
                                    <th>{this.getResource("GL_HTML_PAGE_TITLE_L")} <span className="label-description">({this.langName})</span></th>
                                    <th>{this.getResource("GL_ACTIONS_L")}</th>
                                </tr>
                            </thead>
                            <tbody>
                                {this.cmsPagesI18nVMArr.map((cmsPage) => <PredefinedPagesListI18nUI cmsPage={cmsPage} key={cmsPage.pageID} />)}
                            </tbody>
                        </Table>
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

    /**
     * Генерира нов ViewModel за списък с езици
     */
    @action createList() {

        this.cmsPagesI18nVMArr = [];

        this.cmsPages.forEach((cmsPage) => {
            let pageI18nVM = new PageI18nVM;
            pageI18nVM.pageID = cmsPage.pageID;
            pageI18nVM.bgTitle = cmsPage.title;
            pageI18nVM.bgContent = cmsPage.content;
            pageI18nVM.languageCode = this.langCode;

            let i18nObj = this.cmsPagesi18n.filter(d => d.pageID == cmsPage.pageID);
            pageI18nVM.isTranslated = (!ObjectHelper.isNullOrUndefined(i18nObj[0].title) && !ObjectHelper.isNullOrUndefined(i18nObj[0].content)) ? true : false;
            pageI18nVM.title = i18nObj[0].title;
            pageI18nVM.content = i18nObj[0].content;
            this.cmsPagesI18nVMArr.push(pageI18nVM);
        })
    }

    @action private onLangSelect(lang: Language) {

        this.langCode = lang.code;
        this.langName = lang.name;

        let searchCriteria = new PageSearchCriteria();
        searchCriteria.pageSize = Number.MAX_SAFE_INTEGER;
        searchCriteria.page = 1;

        this.props.registerAsyncOperation(
            this.cmsDataService.searchCmsPages(searchCriteria, this.langCode).then(
                (result) => this.cmsPagesi18n = result
            )
                .then(() => { this.createList() })
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

        let searchCriteria = new PageSearchCriteria();
        searchCriteria.pageSize = Number.MAX_SAFE_INTEGER;
        searchCriteria.page = 1;

        this.props.registerAsyncOperation(this.cmsDataService.searchCmsPages(searchCriteria, this.langCode)
            .then((result) => { this.cmsPagesi18n = result })
            .then(() => this.cmsDataService.searchCmsPages(searchCriteria, "bg"))
            .then((result) => this.cmsPages = result)
            .then(() => { this.createList() })
            .finally(() => this.isLoaded = true));
    }
}

export const PredefinedPagesI18nUI = withRouter(withAsyncFrame(PredefinedPagesI18nUIImpl, false)); 
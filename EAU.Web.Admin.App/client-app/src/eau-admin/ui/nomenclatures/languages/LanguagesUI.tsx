import React from 'react';
import {AsyncUIProps, BaseProps, ConfirmationModal, withAsyncFrame} from 'cnsys-ui-react';
import {observer} from "mobx-react";
import {Language, EAUBaseComponent, Pagination, Constants} from 'eau-core';
import {action, observable, runInAction} from "mobx";
import {BasePagedSearchCriteria, ObjectHelper} from "cnsys-core";
import {NomenclaturesDataService} from "../../../services/NomenclaturesDataService";

interface LanguagesProps extends BaseProps, AsyncUIProps {
}

@observer
class LanguagesImpl extends EAUBaseComponent<LanguagesProps, any> {

    @observable private currentLanguages: Language[];
    @observable private isChangedActivity: boolean;

    private pageSearchCriteria: BasePagedSearchCriteria;
    private nomenclatureDataService: NomenclaturesDataService;

    constructor(props: LanguagesProps) {
        super(props);
        this.onPageChange = this.onPageChange.bind(this);
        this.changeLanguageActivity = this.changeLanguageActivity.bind(this);

        this.nomenclatureDataService = new NomenclaturesDataService();
        this.init();
    }

    render() {
        return <>
            {!ObjectHelper.isArrayNullOrEmpty(this.currentLanguages) && <div className="card edit-language">
                    <div className="card-body">
                        {!ObjectHelper.isNullOrUndefined(this.isChangedActivity) && <div className="alert alert-success">
                            <p>{this.isChangedActivity ? this.getResource("GL_ACTIVE_OK_I") : this.getResource("GL_INACTIVE_OK_I")}</p>
                        </div>}
                        <Pagination activePage={this.pageSearchCriteria.page} count={this.pageSearchCriteria.count}
                                    pagesCount={this.pageSearchCriteria.getPagesCount()} maxVisiblePage={10} size="sm"
                                    onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top"/>
                        <table className="table table-bordered table-striped table-hover">
                            <thead>
                            <tr>
                                <th>{this.getResource("GL_ABBREVIATION_L")}</th>
                                <th>{this.getResource("GL_DESIGNATION_L")}</th>
                                <th>{this.getResource("GL_DOC_KAT_PERSONDATA_STATUS_L")}</th>
                                <th>{this.getResource("GL_ACTIONS_L")}</th>
                            </tr>
                            </thead>
                            <tbody id="content">
                            {this.currentLanguages.map((language) =>
                                <tr key={language.languageID}>
                                    <td>
                                        {language.code}
                                    </td>
                                    <td>{language.name}</td>
                                    <td className="icons-td">
			<span id="status-117">
				<i className={`ui-icon ui-icon-state-${language.isActive ? "active" : "inactive"}`}
                   aria-hidden="true"></i>
                {language.isActive ? this.getResource("GL_ACTIVE_L") : this.getResource("GL_INACTIVE_L")}</span>
                                    </td>
                                    <td className="buttons-td">

                                        <ConfirmationModal
                                            modalTitleKey={language.isActive ? "GL_DEACTIVATION_L" : "GL_ACTIVATION_L"}
                                            modalTextKeys={[language.isActive ? "GL_INACTIVE_CONFIRM_I" : "GL_ACTIVE_CONFIRM_I"]}
                                            noTextKey="GL_REFUSE_L" yesTextKey="GL_CONFIRM_L"
                                            onSuccess={() => this.changeLanguageActivity(language)}>

                                            {language.languageID !== Constants.BG_LANG_ID ? <a className="btn btn-secondary"
                                               title={language.isActive ? this.getResource("GL_DEACTIVATION_L") : this.getResource("GL_ACTIVATION_L")}>
                                                <i className={`ui-icon ui-icon-${language.isActive ? "deactivate" : "activate"}`}></i>
                                            </a> : null}
                                        </ConfirmationModal>
                                    </td>
                                </tr>)
                            }
                            </tbody>
                        </table>
                        <Pagination activePage={this.pageSearchCriteria.page} count={this.pageSearchCriteria.count}
                                    pagesCount={this.pageSearchCriteria.getPagesCount()} maxVisiblePage={10} size="sm"
                                    onSelect={this.onPageChange}/>
                    </div>
                </div>}
        </>
    }

    @action
    private init() {
        this.currentLanguages = [];
        if (!this.pageSearchCriteria) {
            this.pageSearchCriteria = new BasePagedSearchCriteria();
            this.pageSearchCriteria.page = 1;
            this.pageSearchCriteria.pageSize = 4;
        }
        this.searchLanguages();
    }

    changeLanguageActivity(language: Language) {
        if (language.isActive) {
            this.props.registerAsyncOperation(this.nomenclatureDataService.deactivateLanguage(language.languageID).then(() => {

                runInAction.bind(this)(() => {
                    language.isActive = !language.isActive;
                    this.isChangedActivity = language.isActive;
                    this.pageSearchCriteria.page = 1;
                    this.searchLanguages();
                })
            }));
        } else {
            this.props.registerAsyncOperation(this.nomenclatureDataService.activateLanguage(language.languageID).then(() => {

                runInAction.bind(this)(() => {
                    language.isActive = !language.isActive;
                    this.isChangedActivity = language.isActive;
                    this.pageSearchCriteria.page = 1;
                    this.searchLanguages();
                })
            }));
        }
    }

    @action
    private onPageChange(page: any): void {
        if (page > 1) {
            this.isChangedActivity = null;
        }
        this.pageSearchCriteria.page = page;

        this.searchLanguages();
    }

    searchLanguages() {
        this.props.registerAsyncOperation(this.nomenclatureDataService.searchLanguages(this.pageSearchCriteria).then((languages) => {
            this.currentLanguages = languages;
        }))
    }
}

export const LanguagesUI = withAsyncFrame(LanguagesImpl);
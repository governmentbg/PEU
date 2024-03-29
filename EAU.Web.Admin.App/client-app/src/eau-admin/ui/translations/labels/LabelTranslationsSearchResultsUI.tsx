import * as React from "react";
import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { observer } from "mobx-react";
import { EAUBaseComponent, Pagination, ValidationSummaryErrors } from "eau-core";
import { LabelSearchCriteria } from "../../../models/ModelsAutoGenerated";
import { BindableReference } from "cnsys-core";
import { LabelTranslationRowUI } from "./LabelTranslationRowUI";

interface LabelTranslationsSearchResultsUIProps extends BaseProps, AsyncUIProps {
    selectedLanguage
    labelsList
    searchLabelsFunc
    saveTranslationFunc
}

@observer
class LabelTranslationsSearchResultsUIImpl extends EAUBaseComponent<LabelTranslationsSearchResultsUIProps, LabelSearchCriteria> {

    constructor(props: LabelTranslationsSearchResultsUIProps) {
        super(props);
        this.onPageChange = this.onPageChange.bind(this);
    }

    render() {
        return <>
            <div className="card">
                <div className="card-body">
                    <div id="request-messages"></div>

                    <Pagination activePage={this.model.page} count={this.model.count}
                        pagesCount={this.model.getPagesCount()}
                        maxVisiblePage={10} size="sm"
                        onSelect={this.onPageChange}
                        aditionalCssClass="pagination-container--page-top" />

                    <div className="table-responsive">
                        <table className="table table-bordered table-striped table-hover">
                            <thead>
                                <tr>
                                    <th className="w-10">{this.getResource("GL_CODE_L")}</th>
                                    <th className="w-30">{this.getResource("GL_TEXT_BG_L")}</th>
                                    <th className="w-40">
                                        {this.getResource("GL_TEXT_L")}
                                        <span className="label-description"> ({this.props.selectedLanguage.name})</span>
                                    </th>
                                    <th>{this.getResource("GL_DESCRIPTION_L")}</th>
                                </tr>
                            </thead>
                            <tbody id="content">
                                {this.props.labelsList.map((labelItem => {
                                    return (

                                        <LabelTranslationRowUI key={labelItem.labelID}
                                            modelReference={new BindableReference(labelItem)}
                                            saveTranslationFunc={this.props.saveTranslationFunc}
                                        />
                                    )
                                }))}

                            </tbody>
                        </table>
                    </div>

                    <Pagination activePage={this.model.page} count={this.model.count}
                        pagesCount={this.model.getPagesCount()}
                        maxVisiblePage={10} size="sm"
                        onSelect={this.onPageChange} />

                </div>
            </div>
        </>
    }

    private onPageChange(page: any): void {
        this.model.page = page;
        this.props.searchLabelsFunc(this.model);
    }

}

export const LabelTranslationSearchResultsUI = withAsyncFrame(LabelTranslationsSearchResultsUIImpl, false);
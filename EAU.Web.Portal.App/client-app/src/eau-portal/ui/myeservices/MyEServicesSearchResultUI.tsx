import { BindableReference } from "cnsys-core";
import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { EAUBaseComponent, Pagination, ServiceInstanceSearchCriteria } from "eau-core";
import { observer } from "mobx-react";
import * as React from "react";
import { MyEServicesSearchResultRowUI } from "./MyEServicesSearchResultRowUI";

interface MyEServicesSearchResultProps extends BaseProps, AsyncUIProps {
    eServiceList: any[]
    searchEServicesFunc
    updateEServiceFunc
}

@observer class MyEServicesSearchResultUIImpl extends EAUBaseComponent<MyEServicesSearchResultProps, ServiceInstanceSearchCriteria> {
    constructor(props: MyEServicesSearchResultProps) {
        super(props);
        this.onPageChange = this.onPageChange.bind(this);
    }

    componentDidMount() {
        if (this.props.eServiceList && this.props.eServiceList.length > 0) {
            document.getElementById("SEARCH-RESULTS").focus();
            document.getElementById('SEARCH-RESULTS').scrollIntoView()
        }
    }

    componentWillReceiveProps() {
        if (this.props.eServiceList && this.props.eServiceList.length > 0) {
            document.getElementById("SEARCH-RESULTS").focus();
            document.getElementById('SEARCH-RESULTS').scrollIntoView()
        }
    }

    render() {
        return <>
            <Pagination activePage={this.model.page} count={this.model.count}
                pagesCount={this.model.getPagesCount()}
                maxVisiblePage={10} size="sm"
                onSelect={this.onPageChange}
                aditionalCssClass="pagination-container--page-top" />
            <div className="table-responsive-block">
                <table id="SEARCH-RESULTS" tabIndex={-1} className="table table-hover" aria-label={this.getResource("GL_REQUESTED_SERVICES_LIST_I")}>
                    <thead>
                        <tr>
                            <th>{this.getResource("GL_APPLICATION_L")}</th>
                            <th>{this.getResource("GL_URI_L")}</th>
                            <th>{this.getResource("GL_APPLICATION_DATE_L")}</th>
                            <th>{this.getResource("GL_STATUS_L")}</th>
                            <th>{this.getResource("GL_TO_CASE_L")}</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.eServiceList.map((item => {
                            return (
                                <MyEServicesSearchResultRowUI key={item.serviceInstanceID}
                                    modelReference={new BindableReference(item)}
                                    updateEServiceFunc={this.props.updateEServiceFunc} />
                            )
                        }))}
                    </tbody>
                </table>
            </div>
            <Pagination activePage={this.model.page} count={this.model.count}
                pagesCount={this.model.getPagesCount()}
                maxVisiblePage={10} size="sm"
                onSelect={this.onPageChange} />

        </>
    }


    private onPageChange(page: any): void {
        this.model.page = page;
        this.props.searchEServicesFunc(this.model, false, true);
    }
}


export const MyEServicesSearchResultUI = withAsyncFrame(MyEServicesSearchResultUIImpl);
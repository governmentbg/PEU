﻿import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { EAUBaseComponent, Pagination, appConfig } from "eau-core";
import { observable } from "mobx";
import React from "react";
import { NotificationForConcludingOrTerminatingEmploymentContractDataVM } from "../../../models/ModelsAutoGenerated";
import { observer } from "mobx-react";
import { EmployeeInfo } from "../../../models/ModelsManualAdded";

interface IEmployeSearchResultsProps extends BaseProps, AsyncUIProps {
    result: EmployeeInfo[];
    onSelectUserCallback(user: EmployeeInfo, event);
    selectedUsers: EmployeeInfo[];
    addedEmployeesIds: number[]
}

@observer
class EmployeeSearchResultsIMPL extends EAUBaseComponent<IEmployeSearchResultsProps, NotificationForConcludingOrTerminatingEmploymentContractDataVM>{

    private itemsPerPage = appConfig.defaultPageSize ? appConfig.defaultPageSize : 10;

    @observable private page: number = 1;

    constructor(props?: IEmployeSearchResultsProps) {
        super(props);

        this.onPageChange = this.onPageChange.bind(this);
        //Bind
    }

    private onPageChange(page) {
        this.page = page;
    }

    private isChecked(employeeID) {
        return this.props.selectedUsers.filter(x => x.employeeID == employeeID).length > 0
    }

    render() {

        if (this.props.result.length === 0)
            return <div className="alert alert-info">{this.getResource('GL_NO_DATA_FOUND_L')}</div>;

        return (
            <>
                <div className="alert alert-info" id="INFO_1">
                    <p>{this.getResource("DOC_COD_ADD_EMPLOYEE_INFO_MSG_L")}</p>
                </div>

                <div className="table-responsive-block">
                    <table className="table table-striped table-hover" aria-label={this.getResource("DOC_COD_EMPLOYEE_LIST_L")}>
                        <thead>
                            <tr>
                                <th>{this.getResource("DOC_COD_Employee_fullName_L")}</th>
                                <th>{this.getResource("DOC_COD_Employee_identifier_L")}</th>
                            </tr>
                        </thead>
                        <tbody>

                            {this.props.result.slice(this.itemsPerPage * (this.page - 1), this.itemsPerPage * this.page).map(x => {

                                return <tr key={x.employeeID}>
                                    <td data-label={this.getResource("DOC_COD_Employee_fullName_L")} key={x.employeeID}>
                                        <div className="custom-control custom-checkbox">
                                            <input
                                                disabled={this.props.addedEmployeesIds.includes(Number(x.employeeID))}
                                                checked={(this.isChecked(x.employeeID) || this.props.addedEmployeesIds.includes(Number(x.employeeID)))}
                                                onChange={(e) => this.props.onSelectUserCallback(x, e)}
                                                className="custom-control-input" name="exampleRadiosAHA"
                                                id={x.employeeID.toString()}
                                                type="checkbox"
                                            />
                                            <label className="custom-control-label" htmlFor={x.employeeID.toString()}>{x.employeeName}</label>
                                        </div>
                                    </td>
                                    <td data-label={this.getResource("DOC_COD_Employee_identifier_L")}>{x.identityValue}</td>
                                </tr>
                            })}
                        </tbody>
                    </table>
                </div>

                <Pagination activePage={this.page} count={this.props.result.length}
                    pagesCount={Math.ceil(this.props.result.length / this.itemsPerPage)} maxVisiblePage={10} size="sm"
                    onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
            </>
        );
    };
}

export const EmployeeSearchResultsUI = withAsyncFrame(EmployeeSearchResultsIMPL, false);




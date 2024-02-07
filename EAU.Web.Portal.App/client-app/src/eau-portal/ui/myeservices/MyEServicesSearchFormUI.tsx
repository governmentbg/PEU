import { ArrayHelper, BindableReference, ObjectHelper, SelectListItem } from "cnsys-core";
import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { attributesClassFormControl, AutoCompleteUI, Button, EAUBaseComponent, IAutoCompleteItem, Nomenclatures, Period, resourceManager, Service, attributesClassFormControlLabel, ServiceInstanceSearchCriteria, ServiceInstanceStatuses } from "eau-core";
import { action } from "mobx";
import { observer } from "mobx-react";
import React from 'react';

interface MyEServicesSearchFormProps extends BaseProps, AsyncUIProps {
    searchEServicesFunc
}

@observer class MyEServicesSearchFormUIImpl extends EAUBaseComponent<MyEServicesSearchFormProps, ServiceInstanceSearchCriteria> {
    private selectedItems: SelectListItem[];

    constructor(props: MyEServicesSearchFormProps) {
        super(props);

        //Init
        this.selectedItems = [
            new SelectListItem({
                value: ServiceInstanceStatuses.Completed.toString(),  //Изпълнени
                text: this.getResource("GL_ServiceInstanceStatuses_Completed_L"),
                selected: this.model.status === ServiceInstanceStatuses.Completed
            }),
            new SelectListItem({
                value: ServiceInstanceStatuses.InProcess.toString(),  //текущи
                text: this.getResource("GL_ServiceInstanceStatuses_InProcess_L"),
                selected: this.model.status === ServiceInstanceStatuses.InProcess
            }),
            new SelectListItem({
                value: ServiceInstanceStatuses.Rejected.toString(),  //прекратени
                text: this.getResource("GL_ServiceInstanceStatuses_Rejected_L"),
                selected: this.model.status === ServiceInstanceStatuses.Rejected
            })
        ];

        //Bind
        this.onSearchServices = this.onSearchServices.bind(this);
        this.search = this.search.bind(this);
        this.clear = this.clear.bind(this);
    }

    render() {
        return <>
            <div className="search-box search-box--report">
                <fieldset className="card card--box">
                    <legend className="card-header">
                        <h2 className="card-header__title">{resourceManager.getResourceByKey('GL_SEARCH_CRITERIA_L')}</h2>
                    </legend>
                    <div className="card-body">
                        <div className="row">
                            <div className="form-group col-sm col-md-4">
                                {this.labelFor(m => m.caseFileURI, "GL_URI_CASE_L", attributesClassFormControlLabel)}
                                {this.textBoxFor(m => m.caseFileURI)}
                            </div>
                            <div className="col-md-auto">
                                <fieldset>
                                    <legend className="form-control-label">
                                        {resourceManager.getResourceByKey('GL_SEARCH_CRITERIA_APP_PERIOD_L')}
                                    </legend>

                                    <Period modelReferenceOfFirstDate={new BindableReference(this.model, 'serviceInstanceDateFrom')}
                                        modelReferenceOfSecondDate={new BindableReference(this.model, 'serviceInstanceDateTo')} />
                                </fieldset>
                            </div>

                            <div className="form-group col-md-4 col-lg">
                                {this.labelFor(m => m.status, "GL_SEARCH_CRITERIA_EXECUTION_STATUS_L", attributesClassFormControlLabel)}
                                {this.dropDownListFor(m => m.status, this.selectedItems, null, null, true, this.getResource('GL_ALL_L'))}
                            </div>
                        </div>
                        <div className="row">
                            <div className="col-sm-12 form-group">
                                {this.labelFor(m => m.serviceID, "GL_SEARCH_CRITERIA_APP_TYPE_L", attributesClassFormControlLabel)}
                                <AutoCompleteUI
                                    dataSourceSearchDelegat={this.onSearchServices}
                                    placeholder=''
                                    triggerLength={1}
                                    {...this.bind(m => m.serviceID)}
                                    attributes={attributesClassFormControl} />
                            </div>
                        </div>
                    </div>
                    <div id="services_search_footer" className="card-footer">
                        <div className="button-bar card__button-bar button-bar--responsive">
                            <div className="right-side">
                                <Button className="btn btn-primary" type="submit" onClick={this.search}>{resourceManager.getResourceByKey('GL_SEARCH_L')}</Button>
                            </div>
                            <div className="left-side">
                                <Button className="btn btn-secondary" type="button" onClick={this.clear}>{resourceManager.getResourceByKey('GL_CLEAR_L')}</Button>
                            </div>
                        </div>
                    </div>
                </fieldset>
            </div>
        </>
    }

    private search() {
        if (this.isValidCriteria()) {
            this.model.page = 1;
            this.props.searchEServicesFunc(this.model, true, true);
        }
  
    }

    @action
    private clear() {
        this.model.caseFileURI = "";
        this.model.serviceInstanceDateFrom = null;
        this.model.serviceInstanceDateTo = null;
        this.model.status = null;
        this.model.serviceID = null;
        this.model.page = 1;
        this.model.clearErrors();
    }

    onSearchServices(text: string): Promise<IAutoCompleteItem[]> {
        let that = this;
        return Nomenclatures.getServices(el => el.isActive == true).then(nom => {
            let res: IAutoCompleteItem[] = [];
            let services: Service[] = [];

            if (ObjectHelper.isStringNullOrEmpty(text)) {
                services = nom
            } else {
                services = ArrayHelper.queryable.from(nom)
                    .where(el => el.name.toUpperCase().indexOf(text.toUpperCase()) >= 0)
                    .toArray();
            }

            for (let i: number = 0; i < services.length; i++) {
                let currService = services[i];

                let autoCompleteItem: IAutoCompleteItem = {
                    id: currService.serviceID,
                    text: currService.name
                };

                res.push(autoCompleteItem);
            }

            res.sort((el1, el2) => {
                if (el1.text < el2.text)
                    return -1;
                else if (el1.text > el2.text)
                    return 1;
                else
                    return 0;
            });

            return res;


        });
    }

    isValidCriteria(): boolean {

        this.model.clearErrors();
        if (this.model.serviceInstanceDateFrom && this.model.serviceInstanceDateTo)
            if (this.model.serviceInstanceDateFrom > this.model.serviceInstanceDateTo) {
                this.model.addError("serviceInstanceDateFrom", this.getResource("GL_PERIOD_ISNOTVALID_E"));
                return false;
            }
        return true;
    }

}

export const MyEServicesSearchFormUI = withAsyncFrame(MyEServicesSearchFormUIImpl);
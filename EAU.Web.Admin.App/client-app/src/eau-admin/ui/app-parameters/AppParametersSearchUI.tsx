import { ObjectHelper, SelectListItem } from 'cnsys-core';
import { AsyncUIProps, BaseProps, withAsyncFrame } from 'cnsys-ui-react';
import { AppParameterSearchCriteria, EAUBaseComponent, Nomenclatures } from 'eau-core';
import { observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { AppParameterSearchCriteriaValidator } from '../../validations/AppParametersValidator';

interface AppParametersSearchProps extends BaseProps, AsyncUIProps {
}

@observer class AppParametersSearchUI extends EAUBaseComponent<AppParametersSearchProps, AppParameterSearchCriteria>{

    @observable private functionalitiesListItems: SelectListItem[];
    private groupName: string;

    constructor(props: AppParametersSearchProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    render() {
        return <>
            <div className="card-header">
                <h3>{this.getResource("GL_SEARCH_TITLE_L")}</h3>
            </div>
            <div className="card-body">
                <div className="row">
                    <div className="form-group col-lg-6 col-xl-3">
                        {this.labelFor(x => x.functionalityID, "GL_MODULE_FUNCTIONALITY_L")}
                        {this.dropDownListFor(x => x.functionalityID, this.functionalitiesListItems, null, null, true, this.getResource("GL_ALL_L"))}
                    </div>
                    <div className="form-group col-lg-6 col-xl-3">
                        {this.labelFor(x => x.code, "GL_CODE_L")}
                        {this.textBoxFor(x => x.code)}
                    </div>
                    <div className="form-group col-lg-6 col-xl-3">
                        {this.labelFor(x => x.code, "GL_DESCRIPTION_L")}
                        {this.textBoxFor(x => x.description)}
                    </div>

                    <div className="form-group col-md-6 col-lg-auto col-xl-3">
                        <label htmlFor="SYS_PARAM" className="control-label ">{this.getResource("GL_SYS_PARAM_L")}</label>
                        <div className="form-inline">
                            <div className="custom-control-inline custom-control custom-radio">
                                <input className="custom-control-input" type="radio" onChange={this.handleRadioButtonListChange} name={this.groupName}
                                    id={this.groupName + '_all'} value={'all'} checked={ObjectHelper.isNullOrUndefined(this.model.isSystem)} />
                                <label className="custom-control-label" htmlFor={this.groupName + '_all'}>{this.getResource("GL_ALL_L")}</label>
                            </div>
                            <div className="custom-control-inline custom-control custom-radio">
                                <input className="custom-control-input" type="radio" onChange={this.handleRadioButtonListChange} name={this.groupName}
                                    id={this.groupName + '_isSystem'} value={'isSystem'} checked={this.model.isSystem === true} />
                                <label className="custom-control-label" htmlFor={this.groupName + '_isSystem'}>{this.getResource("GL_YES_L")}</label>
                            </div>
                            <div className="custom-control-inline custom-control custom-radio">
                                <input className="custom-control-input" type="radio" onChange={this.handleRadioButtonListChange} name={this.groupName}
                                    id={this.groupName + '_isNotSystem'} value={'isNotSystem'} checked={this.model.isSystem === false} />
                                <label className="custom-control-label" htmlFor={this.groupName + '_isNotSystem'}>{this.getResource("GL_NO_L")}</label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </>
    }

    //#region handlers

    private handleRadioButtonListChange(e: any) {
        switch (e.target.value) {
            case "all": this.model.isSystem = undefined;
                break;
            case "isSystem": this.model.isSystem = true;
                break;
            case "isNotSystem": this.model.isSystem = false;
                break;

            default: this.model.isSystem = undefined
        }
    }

    //#endregion

    //#region main functions

    private init() {

        this.groupName = ObjectHelper.newGuid();
        this.validators = [new AppParameterSearchCriteriaValidator()];

        this.props.registerAsyncOperation(Nomenclatures.getFunctionalities().then(functionalities => {

            runInAction.bind(this)(() => {
                this.functionalitiesListItems = [];

                functionalities.forEach((functionality) => {
                    this.functionalitiesListItems.push(new SelectListItem({ selected: false, text: functionality.name, value: functionality.functionalityID }))
                })
            })
        }))
    }

    private funcBinds() {
        this.handleRadioButtonListChange = this.handleRadioButtonListChange.bind(this);
    }

    //#endregion
}

export default withAsyncFrame(AppParametersSearchUI, false);

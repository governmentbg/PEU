import { SelectListItem } from "cnsys-core";
import { AsyncUIProps, BaseProps, withAsyncFrame } from "cnsys-ui-react";
import { attributesClassFormControlRequiredLabel, EAUBaseComponent } from "eau-core";
import { Nomenclatures } from "eau-documents";
import { action, observable, runInAction } from "mobx";
import { observer } from "mobx-react";
import * as React from "react";
import { SpecialNumberSearchCriteria } from "../models/ModelsManualAdded";

const attributesClassFormControlMaxL6 = { className: 'form-control', maxLength: 6 };

interface VehicleSixLettersOrDigitsRegNumberSearchUIProps extends BaseProps, AsyncUIProps {
    policeDepartmentId: number;
    vehicleTypeCode: number;
    onSearchCallback: () => void;
}

@observer class VehicleSixLettersOrDigitsRegNumberSearchUIImpl extends EAUBaseComponent<VehicleSixLettersOrDigitsRegNumberSearchUIProps, SpecialNumberSearchCriteria> {
    private searchBtn: HTMLButtonElement;
    @observable private provinceCodes: SelectListItem[];

    constructor(props: VehicleSixLettersOrDigitsRegNumberSearchUIProps) {
        super(props);

        //Bind
        this.onSearch = this.onSearch.bind(this);
        this.onClear = this.onClear.bind(this);
        this.searchBtnSetter = this.searchBtnSetter.bind(this);
        this.documentKeyPress = this.documentKeyPress.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
        this.componentWillUnmount = this.componentWillUnmount.bind(this);

        //Init
        let that = this;
        this.searchBtn = undefined;
        this.provinceCodes = [];

        this.props.registerAsyncOperation(Nomenclatures.getProvinceCodes(this.props.policeDepartmentId, this.props.vehicleTypeCode).then(codes => {
            if (codes && codes.length > 0) {
                runInAction(() => { 
                    for (var code of codes) {
                        that.provinceCodes.push(new SelectListItem({ value: code, text: code, selected: false }))
                    }
                });
            }
        }));
    }

    componentDidMount() {
        document.addEventListener('keypress', this.documentKeyPress, true);
    }

    render(): JSX.Element {
        return (
            <>
                <div className="alert alert-info" id="INFO-22">
                    <p>{this.getResource('DOC_KAT_SpecialNumberSearchCriteria_I')}</p>
                </div>

                <div className="search-box search-box--report">
                    <fieldset className="card card--box">
                        <legend className="card-header">
                            <h2 className="card-header__title">{this.getResource('GL_SEARCH_CRITERIA_L')}</h2>
                        </legend>

                        <div className="card-body">
                            <div className="row">
                                <div className="form-group col-sm-5 col-lg-4">
                                    {this.labelFor(m => m.provinceCode, null, attributesClassFormControlRequiredLabel)}
                                    {this.provinceCodes && this.provinceCodes.length > 0 && this.dropDownListFor(m => m.provinceCode, this.provinceCodes, null, null, true, this.getResource('GL_CHOICE_L'))}
                                </div>
                                <div className="form-group col-sm-7 col-lg-6">
                                    {/** Комбинация от 6 букви и/или цифри */}
                                    {this.labelFor(m => m.number, null, attributesClassFormControlRequiredLabel)}
                                    {this.textBoxFor(m => m.number, attributesClassFormControlMaxL6)}
                                </div>
                            </div>
                        </div>

                        <div className="card-footer">
                            <div className="button-bar card__button-bar button-bar--responsive">
                                <div className="right-side">
                                    <button type="submit" className="btn btn-primary" ref={this.searchBtnSetter} onClick={this.onSearch}>{this.getResource('GL_SEARCH_L')}</button>
                                </div>
                                <div className="left-side">
                                    <button type="button" className="btn btn-secondary" onClick={this.onClear}>{this.getResource('GL_CLEAR_L')}</button>
                                </div>
                            </div>
                        </div>

                    </fieldset>
                </div>
            </>);
    }

    componentWillUnmount() {
        document.removeEventListener('keypress', this.documentKeyPress, true);
    }

    @action onClear(): void {
        this.model.provinceCode = undefined;
        this.model.number = undefined;
        this.model.clearErrors();
    }

    private onSearch(): void {
        if (this.props.onSearchCallback) {
            this.props.onSearchCallback();
        }
    }

    private documentKeyPress(e: any): void {
        if (e.keyCode === 13 && $(e.target).parents('.card-body').length == 1) {
            this.searchBtn.click();
        }
    }

    private searchBtnSetter(btn: HTMLButtonElement): void {
        this.searchBtn = btn;
    }
}

export const VehicleSixLettersOrDigitsRegNumberSearchUI = withAsyncFrame(VehicleSixLettersOrDigitsRegNumberSearchUIImpl);

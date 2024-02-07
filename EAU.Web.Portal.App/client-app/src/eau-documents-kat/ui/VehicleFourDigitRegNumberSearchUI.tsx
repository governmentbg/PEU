import * as React from "react";
import { observer } from "mobx-react";
import { action } from "mobx";
import { BaseProps } from "cnsys-ui-react";
import { EAUBaseComponent, ResourceHelpers, attributesClassFormControlRequiredLabel, attributesClassFormControlLabel } from "eau-core";
import { FourDigitSearchCriteria, FourDigitSearchTypes, NumberFormat } from '../models/ModelsManualAdded';

interface VehicleFourDigitRegNumberSearchUIProps extends BaseProps {
    onSearchCallback: () => void;
    onClearCallback: () => void;
}

@observer export class VehicleFourDigitRegNumberSearchUI extends EAUBaseComponent<VehicleFourDigitRegNumberSearchUIProps, FourDigitSearchCriteria> {
    private searchBtn: HTMLButtonElement;

    constructor(props: VehicleFourDigitRegNumberSearchUIProps) {
        super(props);

        //Bind
        this.onSearch = this.onSearch.bind(this);
        this.onClear = this.onClear.bind(this);
        this.searchBtnSetter = this.searchBtnSetter.bind(this);
        this.documentKeyPress = this.documentKeyPress.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
        this.componentWillUnmount = this.componentWillUnmount.bind(this);

        //Init
        this.searchBtn = undefined;
    }

    componentDidMount() {
        document.addEventListener('keypress', this.documentKeyPress, true);
    }

    render(): JSX.Element {
        return (
            <>
                <div className="alert alert-info" id="INFO-2">
                    <p>{this.getResource('DOC_KAT_FourDigitSearchCriteria_I')}</p>
                </div>

                <div className="search-box search-box--report">
                    <fieldset className="card card--box">
                        <legend className="card-header">
                            <h2 className="card-header__title">{this.getResource('GL_SEARCH_CRITERIA_L')}</h2>
                        </legend>

                        <div className="card-body">
                            <div className="row">
                                <div className="form-group col-12">
                                    <fieldset>
                                        <legend className="form-control-label">{this.getResource('GL_SearchType_L')}</legend>
                                        {this.radioButtonListFor(m => m.fourDigitSearchType, ResourceHelpers.getSelectListItemsForEnum(FourDigitSearchTypes))}
                                    </fieldset>
                                </div>
                            </div>

                            {this.model.fourDigitSearchType == FourDigitSearchTypes.ByInterval
                                &&
                                <div className="row">
                                    <div className="form-group col-sm-9 col-md-7">
                                        {/** Вид комбинация от 4 цифри в избрания интервал */}
                                        {this.labelFor(m => m.numberFormat, null, attributesClassFormControlLabel)}
                                        {this.dropDownListFor(m => m.numberFormat, ResourceHelpers.getSelectListItemsForEnum(NumberFormat), null, null, true, this.getResource('GL_CHOICE_L'))}
                                    </div>
                                    <div className="col-sm-9 col-md">
                                        <fieldset>
                                            <legend className="form-control-label">{this.getResource('DOC_KAT_FourDigitSearchCriteria_Interval_I')}</legend>
                                            <div className="row">
                                                <div className="form-group col-sm">
                                                    <div className="row form-row">
                                                        <div className="col-auto">
                                                            {this.labelFor(m => m.fromRegNumber, null, { className: 'col-form-label' })}
                                                        </div>
                                                        <div className="col">
                                                            {this.textBoxFor(m => m.fromRegNumber, { className: 'form-control', maxLength: 4 })}
                                                        </div>
                                                    </div>
                                                </div>
                                                <div className="form-group col-sm">
                                                    <div className="row form-row">
                                                        <div className="col-auto">
                                                            {this.labelFor(m => m.toRegNumber, null, { className: 'col-form-label' })}
                                                        </div>
                                                        <div className="col">
                                                            {this.textBoxFor(m => m.toRegNumber, { className: 'form-control', maxLength: 4 })}
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>}
                            {this.model.fourDigitSearchType == FourDigitSearchTypes.ByRegNumber
                                &&
                                <div className="row">
                                    <div className="form-group col-sm-9 col-md-7">
                                        {/** Номер с 4 цифри */}
                                        {this.labelFor(m => m.specificRegNumber, null, attributesClassFormControlRequiredLabel)}
                                        {this.textBoxFor(m => m.specificRegNumber, { className: 'form-control', maxLength: 4 })}
                                    </div>
                                </div>}
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
        if (this.model.fourDigitSearchType == FourDigitSearchTypes.ByInterval) {
            this.model.fromRegNumber = undefined;
            this.model.toRegNumber = undefined;
            this.model.numberFormat = undefined;
        } else {
            this.model.specificRegNumber = undefined;
        }
        if (this.props.onClearCallback) {
            this.props.onClearCallback();
        }
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

import * as React from "react";
import { observer } from "mobx-react";
import { observable, runInAction, action } from "mobx";
import { Modal, ModalBody, ModalHeader, ModalFooter } from "reactstrap";
import { ObjectHelper } from "cnsys-core";
import { BaseProps, AsyncUIProps, withAsyncFrame } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import { SPRKRTCODataService } from '../services/SPRKRTCODataService';
import { FourDigitSearchCriteriaValidator } from '../validations/FourDigitSearchCriteriaValidator';
import { VehicleFourDigitRegNumberSearchUI } from "./VehicleFourDigitRegNumberSearchUI";
import { FourDigitSearchCriteria, FourDigitSearchTypes, IFourDigitsSearchResult, IPlateStatusResult, PlateStatus, VehicleTypeCode } from "../models/ModelsManualAdded";

interface VehicleFourDigitRegNumberChoiceUIProps extends BaseProps, AsyncUIProps {
    policeDepartmentId: number;
    vehicleTypeCode: VehicleTypeCode;
    type1PlatesCount?: number;
    type2PlatesCount?: number;
    callbackOnChoose: (choice: string) => void;
}

const REG_NUMBERS_GROUP_NAME: string = 'freeFourDigitsRegNums';

@observer class VehicleFourDigitRegNumberChoiceUIImpl extends EAUBaseComponent<VehicleFourDigitRegNumberChoiceUIProps, null> {
    @observable private isAlreadySearched: boolean;
    private searchCriteriaValidator: FourDigitSearchCriteriaValidator;
    private searchCriteria: FourDigitSearchCriteria;
    @observable private data: IFourDigitsSearchResult;
    @observable private isOpen: boolean;

    constructor(props?: VehicleFourDigitRegNumberChoiceUIProps, context?: any) {
        super(props, context);

        //Bind
        this.closeModal = this.closeModal.bind(this);
        this.openModal = this.openModal.bind(this);
        this.onSave = this.onSave.bind(this);
        this.onSearchResult = this.onSearchResult.bind(this);
        this.onClear = this.onClear.bind(this);
        this.drawResult = this.drawResult.bind(this);
        this.componentDidUpdate = this.componentDidUpdate.bind(this);

        //Init
        this.isOpen = false;
        this.searchCriteriaValidator = new FourDigitSearchCriteriaValidator();
    }

    render(): JSX.Element {
        return (
            <>
                <button type="button" className="btn btn-light" onClick={this.openModal}>
                    <i className="ui-icon ui-icon-import mr-1" aria-hidden="true"></i>{this.getResource('GL_CHOICE_L')}
                </button>
                <Modal centered={true} backdrop='static' autoFocus={true} isOpen={this.isOpen} onExit={this.closeModal}>
                    <ModalHeader closeAriaLabel={this.getResource("GL_CLOSE_L")} toggle={this.closeModal} >
                        {this.getResource('DOC_KAT_REGNUMBER_CHOICE_L')}
                    </ModalHeader>
                    <ModalBody>
                        {this.props.asyncErrors && this.props.asyncErrors.length > 0 ? <>{this.props.drawErrors()}{this.props.drawWarnings()}</> : null}
                        <VehicleFourDigitRegNumberSearchUI {...this.bind(this.searchCriteria, 'fourDigitsCriteria', [this.searchCriteriaValidator])} onSearchCallback={this.onSearchResult} onClearCallback={this.onClear} />
                        {this.drawResult()}
                    </ModalBody>
                    <ModalFooter>
                        <div className="button-bar">
                            <div className="right-side">
                                < button type="button" className="btn btn-primary" onClick={this.onSave} data-dismiss="modal" disabled={!this.data || !this.data.result || this.data.result.length == 0}>{this.getResource('GL_CONFIRM_L')}</button>
                            </div>
                            <div className="left-side">
                                <button type="button" className="btn btn-secondary" onClick={this.closeModal} data-dismiss="modal">{this.getResource('GL_CANCEL_L')}</button>
                            </div>
                        </div>
                    </ModalFooter>
                </Modal>
            </>);
    }

    drawResult(): JSX.Element {
        if (this.data && this.data.result && this.data.result.length > 0) {
            return (
                <>
                    {!ObjectHelper.isStringNullOrEmpty(this.data.exceedResultLimiteWarnning)
                        &&
                        <div className="alert alert-warning" role="alert">
                            <p>{this.data.exceedResultLimiteWarnning}</p>
                        </div>}
                    <fieldset>
                        <legend className="form-control-label">{'Свободни регистрационни номера'}</legend>
                        <div className="form-group checkbox-columns">
                            {this.data.result.map((el: IPlateStatusResult, idx: number) => {
                                return (
                                    <div className="custom-control custom-radio" key={idx}>
                                        <input type="radio" name={REG_NUMBERS_GROUP_NAME} id={`regNum${idx}`} value={el.number} className="custom-control-input" />
                                        <label className="custom-control-label" htmlFor={`regNum${idx}`}>{el.number}</label>
                                    </div>);
                            })}
                        </div>
                    </fieldset>
                </>);
        } else {
            if (this.isAlreadySearched && (!this.props.asyncErrorMessages || this.props.asyncErrorMessages.length == 0)) {
                return (<div className="alert alert-info">{this.getResource('GL_NO_DATA_FOUND_L')}</div>);
            } else {
                return null;
            }
        }
    }

    componentDidUpdate(): void {
        if (this.isAlreadySearched == true && this.data && this.data.result && this.data.result.length > 0 && this.isOpen == true) {
            let firstRadioBtn = $('#regNum0');

            if (firstRadioBtn.length == 1) {
                firstRadioBtn.prop("checked", true);
                firstRadioBtn.focus();
            }
        }
    }

    @action private closeModal(): void {
        this.isOpen = false;
    }

    @action private openModal(): void {
        this.isOpen = true;

        this.isAlreadySearched = false;
        this.searchCriteria = new FourDigitSearchCriteria();
        this.searchCriteria.fourDigitSearchType = FourDigitSearchTypes.ByRegNumber;
        this.searchCriteria.policeDepartment = this.props.policeDepartmentId;
        this.searchCriteria.plateStatus = PlateStatus.Available;
        this.searchCriteria.vehicleTypeCode = this.props.vehicleTypeCode;
        this.searchCriteria.type1PlatesCount = this.props.type1PlatesCount;
        this.searchCriteria.type2PlatesCount = this.props.type2PlatesCount;
        this.data = undefined;
    }

    @action private onSave(e: any): void {
        let checkedRadioBtn = $("input[name='" + REG_NUMBERS_GROUP_NAME + "']:checked");
        let selectedRegNum: string = checkedRadioBtn && checkedRadioBtn.length == 1 ? checkedRadioBtn.val().toString() : null;

        if (ObjectHelper.isStringNullOrEmpty(selectedRegNum)) return;

        if (this.props.callbackOnChoose) {
            this.data = undefined;
            this.props.callbackOnChoose(selectedRegNum);
        }

        this.isOpen = false;
    }

    @action private onSearchResult(): void {
        if (this.searchCriteriaValidator.validate(this.searchCriteria)) {
            let srv = new SPRKRTCODataService();
            let that = this;

            that.data = undefined;

            this.props.registerAsyncOperation(srv.getFreeFourDigitsPlates(this.searchCriteria).then(res => {
                runInAction(() => {
                    that.isAlreadySearched = true;

                    if (res && res.result && res.result.length > 0) {
                        that.data = res;
                    }
                });
            }));
        }
    }

    @action private onClear(): void {
        this.data = undefined;
        this.isAlreadySearched = false;
    }
}

export const VehicleFourDigitRegNumberChoiceUI = withAsyncFrame(VehicleFourDigitRegNumberChoiceUIImpl, false);
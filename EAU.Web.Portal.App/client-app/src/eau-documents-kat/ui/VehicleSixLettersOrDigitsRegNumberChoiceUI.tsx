import * as React from "react";
import { observer } from "mobx-react";
import { observable, runInAction, action } from "mobx";
import { Modal, ModalBody, ModalHeader, ModalFooter } from "reactstrap";
import { ObjectHelper } from "cnsys-core";
import { BaseProps, AsyncUIProps, withAsyncFrame } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import { SpecialNumberSearchCriteriaValidator } from "../validations/SpecialNumberSearchCriteriaValidator";
import { SPRKRTCODataService } from '../services/SPRKRTCODataService';
import { VehicleSixLettersOrDigitsRegNumberSearchUI } from "./VehicleSixLettersOrDigitsRegNumberSearchUI";
import { SpecialNumberSearchCriteria, VehicleTypeCode } from "../models/ModelsManualAdded";

interface VehicleSixLettersOrDigitsRegNumberChoiceUIProps extends BaseProps, AsyncUIProps {
    policeDepartmentId: number;
    vehicleTypeCode: VehicleTypeCode;
    callbackOnChoose: (provinceCode: string, number: string) => void;
}

@observer class VehicleSixLettersOrDigitsRegNumberChoiceUIImpl extends EAUBaseComponent<VehicleSixLettersOrDigitsRegNumberChoiceUIProps, null> {
    private confirmBtn: HTMLButtonElement;
    private searchCriteriaValidator: SpecialNumberSearchCriteriaValidator;
    private searchCriteria: SpecialNumberSearchCriteria;
    @observable private data: number;
    @observable private lastSearchedFreeRegNumber: string;
    @observable private lastSearchedProvinceCode: string;
    @observable private isOpen: boolean;

    constructor(props: VehicleSixLettersOrDigitsRegNumberChoiceUIProps) {
        super(props);

        //Bind
        this.closeModal = this.closeModal.bind(this);
        this.openModal = this.openModal.bind(this);
        this.onSave = this.onSave.bind(this);
        this.onSearchResult = this.onSearchResult.bind(this);
        this.confirmBtnSetter = this.confirmBtnSetter.bind(this);

        //Init
        this.isOpen = false;
        this.confirmBtn = undefined;
        this.searchCriteriaValidator = new SpecialNumberSearchCriteriaValidator();
    }

    render(): JSX.Element {
        let dataResult: any = null;

        if (this.data == 0) {
            dataResult = (<div className="alert alert-success" role="alert"><p>{this.getResource('DOC_KAT_REGNUMBER_IS_FREE_I').replace('{regNum}', (this.lastSearchedProvinceCode + this.lastSearchedFreeRegNumber))}</p></div>);
        }

        if (this.data == 1) {
            dataResult = (<div className="alert alert-danger" role="alert"><p>{this.getResource('DOC_KAT_REGNUMBER_IS_NOT_FREE_I')}</p></div>);
        }

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
                        <VehicleSixLettersOrDigitsRegNumberSearchUI
                            {...this.bind(this.searchCriteria, 'SpecialNumberSearchCriteria', [this.searchCriteriaValidator])}
                            policeDepartmentId={this.props.policeDepartmentId}
                            vehicleTypeCode={this.props.vehicleTypeCode}
                            onSearchCallback={this.onSearchResult} />
                        {dataResult}
                    </ModalBody>
                    <ModalFooter>
                        <div className="button-bar">
                            <div className="right-side">
                                <button type="button" className="btn btn-primary" ref={this.confirmBtnSetter} onClick={this.onSave} data-dismiss="modal" disabled={ObjectHelper.isStringNullOrEmpty(this.lastSearchedFreeRegNumber)}>{this.getResource('GL_CONFIRM_L')}</button>
                            </div>
                            <div className="left-side">
                                <button type="button" className="btn btn-secondary" onClick={this.closeModal} data-dismiss="modal">{this.getResource('GL_CANCEL_L')}</button>
                            </div>
                        </div>
                    </ModalFooter>
                </Modal>
            </>);
    }

    componentDidUpdate(): void {
        if (this.data == 0 && !ObjectHelper.isStringNullOrEmpty(this.lastSearchedFreeRegNumber) && this.isOpen == true) {
            this.confirmBtn.focus();
        }
    }

    @action private closeModal(): void {
        this.isOpen = false;
    }

    @action private openModal(): void {
        this.isOpen = true;
        this.searchCriteria = new SpecialNumberSearchCriteria();
        this.searchCriteria.vehicleTypeCode = this.props.vehicleTypeCode;
        this.data = undefined;
        this.lastSearchedFreeRegNumber = undefined;
        this.lastSearchedProvinceCode = undefined;
    }

    private onSave(e: any): void {
        if (this.data == 0 && !ObjectHelper.isStringNullOrEmpty(this.lastSearchedFreeRegNumber)) {
            if (this.props.callbackOnChoose) {
                this.props.callbackOnChoose(this.lastSearchedProvinceCode, this.lastSearchedFreeRegNumber);
            }

            this.isOpen = false;
        }
    }

    @action private onSearchResult(): void {
        this.data = undefined;
        this.lastSearchedFreeRegNumber = undefined;
        this.lastSearchedProvinceCode = undefined;

        if (this.searchCriteriaValidator.validate(this.searchCriteria)) {
            let srv = new SPRKRTCODataService();
            let that = this;

            this.props.registerAsyncOperation(srv.getFreeSpecialNumbers(this.searchCriteria).then(res => {
                runInAction(() => {
                    if (!ObjectHelper.isNullOrUndefined(res)) {
                        that.data = res;
                        that.lastSearchedFreeRegNumber = res == 0 ? that.searchCriteria.number : undefined;
                        this.lastSearchedProvinceCode = res == 0 ? that.searchCriteria.provinceCode : undefined;
                    }
                });
            }));
        }
    }

    private confirmBtnSetter(btn: HTMLButtonElement): void {
        this.confirmBtn = btn;
    }
}

export const VehicleSixLettersOrDigitsRegNumberChoiceUI = withAsyncFrame(VehicleSixLettersOrDigitsRegNumberChoiceUIImpl, false);
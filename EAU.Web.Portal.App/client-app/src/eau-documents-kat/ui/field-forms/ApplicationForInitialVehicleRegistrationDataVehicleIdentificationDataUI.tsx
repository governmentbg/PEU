﻿import React from "react";
import { action, observable, runInAction } from "mobx";
import { observer } from "mobx-react";
import { SelectListItem, ArrayHelper, ObjectHelper } from "cnsys-core";
import { BaseProps, AsyncUIProps, withAsyncFrame } from "cnsys-ui-react";
import { ApplicationForInitialVehicleRegistrationDataVehicleIdentificationData } from "../../models/ModelsAutoGenerated"
import { EAUBaseComponent, attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, AutoCompleteUI, IAutoCompleteItem, attributesClassFormControl, attributesClassFormControlLabel } from "eau-core";
import { AISKATNomenclatureItem, Nomenclatures } from "eau-documents";

interface ApplicationForInitialVehicleRegistrationDataVehicleIdentificationDataUIProps extends BaseProps, AsyncUIProps {
}

@observer class ApplicationForInitialVehicleRegistrationDataVehicleIdentificationDataUIImpl extends EAUBaseComponent<ApplicationForInitialVehicleRegistrationDataVehicleIdentificationDataUIProps, ApplicationForInitialVehicleRegistrationDataVehicleIdentificationData> {
    @observable private vehicleBaseColors: SelectListItem[];

    constructor(props?: ApplicationForInitialVehicleRegistrationDataVehicleIdentificationDataUIProps, context?: any) {
        super(props, context);

        //bind
        this.onImportCountryChange = this.onImportCountryChange.bind(this);
        this.onColorCodeChange = this.onColorCodeChange.bind(this);

        //Init
        this.vehicleBaseColors = [];
        let that = this;
        this.props.registerAsyncOperation(Nomenclatures.getVehicleBaseColors().then(colors => {
            if (colors && colors.length > 0) {
                runInAction(() => {
                    for (var i: number = 0; i < colors.length; i++) {
                        let currColor = colors[i];
                        that.vehicleBaseColors.push(new SelectListItem({ value: currColor.code.toString(), text: currColor.name, selected: false }));
                    }
                });
            }
        }));
    }

    renderEdit(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {/** Рама (VIN) на ППС */}
                        {this.labelFor(m => m.identificationNumber, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(m => m.identificationNumber, attributesClassFormControlReqired)}
                    </div>
                    <div className="form-group col-sm-6">
                        {/** Държава на внос */}
                        {this.labelFor(m => m.importCountryCode, null, attributesClassFormControlRequiredLabel)}
                        <AutoCompleteUI
                            dataSourceSearchDelegat={this.searchImportCountrySource}
                            placeholder=''
                            triggerLength={1}
                            {...this.bind(m => m.importCountryCode)}
                            attributes={attributesClassFormControl}
                            onChangeCallback={this.onImportCountryChange} />
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {/** Знак за държава, издала типовото одобрение */}
                        {this.labelFor(m => m.approvalCountryCode, null, attributesClassFormControlRequiredLabel)}
                        {this.textBoxFor(m => m.approvalCountryCode, attributesClassFormControlReqired)}
                        {this.inlineHelpFor(m => m.approvalCountryCode, 'DOC_KAT_ApplicationForInitialVehicleRegistrationDataVehicleIdentificationData_approvalCountryCode_I')}
                    </div>
                    <div className="form-group col-sm-6">
                        {/** Цвят (основен) на ППС */}
                        {this.labelFor(m => m.colorCode, null, attributesClassFormControlLabel)}
                        {this.dropDownListFor(m => m.colorCode, this.vehicleBaseColors, attributesClassFormControl, this.onColorCodeChange, true, this.getResource('GL_CHOICE_L'))}
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-12">
                        {/** Допълнителна информация */}
                        {this.labelFor(m => m.additionalInfo, null, attributesClassFormControlLabel)}
                        {this.textBoxFor(m => m.additionalInfo)}
                        {this.inlineHelpFor(m => m.additionalInfo, 'DOC_KAT_ADDITIONAL_INFORMATION_I')}
                    </div>
                </div>
            </>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-sm-6">
                        {/** Рама (VIN) на ППС */}
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.identificationNumber)}</h4>
                        {this.textDisplayFor(m => m.identificationNumber)}
                    </div>
                    <div className="form-group col-sm-6">
                        {/** Държава на внос */}
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.importCountryCode)}</h4>
                        <p className="field-text">{this.model.importCountryName}</p>
                        {this.propertyErrorsDispleyFor(m => m.importCountryCode)}
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.approvalCountryCode)}</h4>
                        {this.textDisplayFor(m => m.approvalCountryCode)}
                    </div>
                    {!ObjectHelper.isStringNullOrEmpty(this.model.colorCode) && < div className="form-group col-sm-6">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.colorCode)}</h4>
                        {this.textDisplayFor(m => m.colorName)}
                    </div>}
                </div>
                {!ObjectHelper.isStringNullOrEmpty(this.model.additionalInfo)
                    &&
                    <div className="row" >
                        <div className="col-12 form-group">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.additionalInfo)}</h4>
                            {this.textDisplayFor(m => m.additionalInfo)}
                        </div>
                    </div>}
            </>
        );
    }

    private searchImportCountrySource(val: string): Promise<IAutoCompleteItem[]> {
        if (!val || val.trim() === '') {
            return Nomenclatures.getCountries().then(nom => {
                return nom.map((item: AISKATNomenclatureItem) => {
                    let tmp: IAutoCompleteItem = { id: item.code.toString(), text: item.name };

                    return tmp;
                });
            });
        } else {
            return Nomenclatures.getCountries().then(nom => {
                return nom.filter(el => (el.name as string).toLowerCase().indexOf(val.toLowerCase()) >= 0).map((el, idx) => {
                    let tmp: IAutoCompleteItem = { id: el.code.toString(), text: el.name };

                    return tmp;
                });
            });
        }
    }

    @action private onImportCountryChange(selectedItem: IAutoCompleteItem): void {
        this.model.importCountryName = selectedItem ? selectedItem.text : undefined;
    }

    @action private onColorCodeChange(e: any): void {
        if (ObjectHelper.isStringNullOrEmpty(this.model.colorCode)) {
            this.model.colorName = undefined;
        } else {
            let selectedColor = ArrayHelper.queryable.from(this.vehicleBaseColors).first(c => c.value == this.model.colorCode);

            this.model.colorName = selectedColor.text;
        }
    }
}

export const ApplicationForInitialVehicleRegistrationDataVehicleIdentificationDataUI = withAsyncFrame(ApplicationForInitialVehicleRegistrationDataVehicleIdentificationDataUIImpl);
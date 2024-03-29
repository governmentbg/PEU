﻿import { SelectListItem, ObjectHelper } from "cnsys-core";
import { ViewMode, BaseProps } from "cnsys-ui-react";
import { EAUBaseComponent, ValidationSummary, ValidationSummaryStrategy, attributesClassFormControlRequiredLabel, attributesClassFormControlReqired, attributesClassFormControlDisabled } from "eau-core";
import { action } from "mobx";
import React from "react";
import { ApplicationFormManagerProps, withDocumentFormManager, withCollectionItems } from "eau-documents";
import { ControlCouponDataItemVM, Firearms, Ammunition, Pyrotechnics, Explosives } from "../../models/ModelsAutoGenerated";
import { FirearmUI } from "./FirearmUI";
import { AmmunitionUI } from "./AmmunitionUI";
import { ExplosiveUI } from "./ExplosiveUI";
import { PyrotechnicsUI } from "./PyrotechnicsUI";


interface ControlCouponDataItemsProps extends BaseProps, ApplicationFormManagerProps {
    licenseType: string;
}

class ControlCouponDataItemsUIImpl extends EAUBaseComponent<ControlCouponDataItemsProps, ControlCouponDataItemVM> {

    private categorySelectList: SelectListItem[] = [];
    private categoryKeyMap: Map<string, string>;

    constructor(props: ControlCouponDataItemsProps) {
        super(props);

        if (this.props.viewMode == ViewMode.Edit) {

            this.categoryKeyMap = new Map();

            if (this.props.documentFormManager.service.additionalConfiguration.controlCouponItemCategory) {

                let arr = JSON.parse(this.props.documentFormManager.service.additionalConfiguration.controlCouponItemCategory);

                if (ObjectHelper.isArray(arr)) {
                    arr.forEach(category => {
                        if (category.licenseTypes.includes(this.props.licenseType)) {
                            this.categorySelectList.push(new SelectListItem(category));
                            this.categoryKeyMap.set(category.value, category.key);
                        }
                    });

                    if (this.categorySelectList.length == 1) {
                        this.model.categoryCode = this.categorySelectList[0].value;
                        this.model.categoryName = this.categorySelectList[0].text;
                    }
                }
            }

            this.onCategoryChange = this.onCategoryChange.bind(this);
        }
    }

    @action private onCategoryChange(e: any) {

        this.model.categoryCode = null;
        this.model.categoryName = null;
        this.model.firearms = null;
        this.model.ammunition = null;
        this.model.pyrotechnics = null;
        this.model.explosives = null;

        let currentCategory = this.categorySelectList.find(x => x.value == e.target.value);

        if (currentCategory) {
            this.model.categoryCode = currentCategory.value;
            this.model.categoryName = currentCategory.text;

            let categoryKey = this.categoryKeyMap.get(this.model.categoryCode);

            switch (categoryKey) {
                case 'Firearm':
                    this.model.firearms = new Firearms();
                    break;
                case 'Ammunition':
                    this.model.ammunition = new Ammunition();
                    break;
                case 'Pyrotechnics':
                    this.model.pyrotechnics = new Pyrotechnics();
                    break;
                case 'Explosive':
                    this.model.explosives = new Explosives();
                    break;
            }
        }
    }

    renderEdit(): JSX.Element {

        let categoryKey = this.categoryKeyMap.get(this.model.categoryCode);

        return (
            <>
                {categoryKey == 'Ammunition' ? <ValidationSummary model={this.model.ammunition} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} /> : null}
                <div className="row">
                    <div className="form-group col-sm-6">
                        {this.labelFor(x => x.categoryCode, null, attributesClassFormControlRequiredLabel)}
                        {this.categorySelectList.length == 1
                            ? this.dropDownListFor(x => x.categoryCode, this.categorySelectList, attributesClassFormControlDisabled)
                            : this.dropDownListFor(x => x.categoryCode, this.categorySelectList, attributesClassFormControlReqired, this.onCategoryChange, true, this.getResource("GL_CHOICE_L"))
                        }
                    </div>
                </div>
                {categoryKey == 'Firearm' ? <FirearmUI {...this.bind(m => m.firearms)} /> : null}
                {categoryKey == 'Ammunition' ? <AmmunitionUI {...this.bind(m => m.ammunition)} /> : null}
                {categoryKey == 'Pyrotechnics' ? <PyrotechnicsUI {...this.bind(m => m.pyrotechnics)} /> : null}
                {categoryKey == 'Explosive' ? <ExplosiveUI {...this.bind(m => m.explosives)} /> : null}
            </>
        )
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                {this.model.ammunition ? <ValidationSummary model={this.model.ammunition} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} /> : null}
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(x => x.categoryCode)}</h4>
                        {this.textDisplayFor(x => x.categoryName)}
                        {this.propertyErrorsDispleyFor(x => x.categoryCode)}
                    </div>
                </div>
                {this.model.firearms ? <FirearmUI {...this.bind(m => m.firearms)} /> : null}
                {this.model.ammunition ? <AmmunitionUI {...this.bind(m => m.ammunition)} /> : null}
                {this.model.pyrotechnics ? <PyrotechnicsUI {...this.bind(m => m.pyrotechnics)} /> : null}
                {this.model.explosives ? <ExplosiveUI {...this.bind(m => m.explosives)} /> : null}
            </>
        )
    }
}

export const ControlCouponDataItemsUI = withDocumentFormManager(withCollectionItems(ControlCouponDataItemsUIImpl, {
    initItem: () => new ControlCouponDataItemVM(),
    addButtonLabelKey: 'DOC_KOS_ADD_ARMS_L',
    showDeleteButtonOnSingleElement: true
}));
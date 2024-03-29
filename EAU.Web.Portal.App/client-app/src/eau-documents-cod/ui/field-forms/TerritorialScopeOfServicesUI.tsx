﻿import * as React from "react";
import { observer } from "mobx-react";
import { BaseProps } from "cnsys-ui-react";
import { EAUBaseComponent, ResourceHelpers } from "eau-core";
import { observable, action } from "mobx";
import { TerritorialScopeOfServicesVM, ScopeOfCertification, TerritorialScopeOfServicesDistrictsVM } from "../../models/ModelsAutoGenerated";
import { TerritorialScopeOfServicesDistrictsCollectionUI } from ".";
import { FieldFormUI } from "eau-documents";

@observer export class TerritorialScopeOfServicesUI extends EAUBaseComponent<BaseProps, TerritorialScopeOfServicesVM>{

    @observable public showScopeOfServicesDistrictsSelection: boolean;

    constructor(props?: any, context?: any) {
        super(props, context);

        this.showScopeOfServicesDistrictsSelection = this.model != null && this.model.scopeOfCertification == ScopeOfCertification.SelectedDistricts;

        this.onChangeScopeOfCertification = this.onChangeScopeOfCertification.bind(this);
    }

    renderEdit() {
        return (
            <>
                <div className="ml-4">
                    <FieldFormUI title={this.getResourceByProperty(m => m)} headerType={"h4"}>                        
                        <div className="row">
                            <div className="form-group col-12">
                                {this.radioButtonListFor(m => m.scopeOfCertification, ResourceHelpers.getSelectListItemsForEnum(ScopeOfCertification), null, this.onChangeScopeOfCertification)}
                            </div>
                        </div>
                        {this.showScopeOfServicesDistrictsSelection == true ? <TerritorialScopeOfServicesDistrictsCollectionUI {...this.bind(m => m)} /> : ""}
                    </FieldFormUI>
                </div>
            </>
        );
    }

    renderDisplay() {
        return (
            <>
                {ResourceHelpers.getResourceByEmun(this.model.scopeOfCertification, ScopeOfCertification)}
                {this.showScopeOfServicesDistrictsSelection == true ? <TerritorialScopeOfServicesDistrictsCollectionUI {...this.bind(m => m)} /> : ""}
            </>
        );
    };

    @action public onChangeScopeOfCertification(event?: any, value?: string) {
        if (value == ScopeOfCertification.SelectedDistricts.toString()) {
            
            this.showScopeOfServicesDistrictsSelection = true;
        }
        else {
            this.showScopeOfServicesDistrictsSelection = false;
        }
    }
}
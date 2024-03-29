﻿import * as React from "react";
import { observer } from "mobx-react";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import { ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM } from "../../models/ModelsAutoGenerated";
import { GraoAddressUI } from "eau-documents";

interface PersonDataUIProps extends BaseProps {
}

@observer
export class PersonDataUI
    extends EAUBaseComponent<PersonDataUIProps, ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataVM> {

    constructor(props: PersonDataUIProps) {
        super(props);

        //Bind       

        //Init

    }
    renderEdit() {
        return (
            this.renderInternal()
        );
    };

    renderDisplay() {
        return (
            this.renderInternal()
        );
    }

    private renderInternal() {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.permanentAddress)}</h4>
                        <GraoAddressUI  {...this.bind(m => m.permanentAddress, ViewMode.Display)} />
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-12">
                        <h4 className="form-control-label">{this.getResourceByProperty(m => m.currentAddress)}</h4>
                        <GraoAddressUI  {...this.bind(m => m.currentAddress, ViewMode.Display)} />
                    </div>
                </div>
            </>
        );
    }
}

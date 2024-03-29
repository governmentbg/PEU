﻿import * as React from "react";
import { action } from "mobx";
import { observer } from "mobx-react";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent, ResourceHelpers } from "eau-core";
import { FieldFormUI, GraoAddressUI } from "eau-documents";
import { ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataVM, ANDCertificateReason } from "../../models/ModelsAutoGenerated";

interface ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataUIProps extends BaseProps {
}


@observer
export class ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataUI
    extends EAUBaseComponent<ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataUIProps, ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataVM> {

    constructor(props: ApplicationForIssuingDocumentForRightsAndImposedPenaltiesOfVehicleDriverDataUIProps) {
        super(props);

        //Bind       

        //Init

    }

    renderEdit(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m)}>
                    <FieldFormUI title={this.getResourceByProperty(m => m.permanentAddress)} headerType={"h4"}>
                        {/** Постоянен адрес */}
                        <GraoAddressUI {...this.bind(m => m.permanentAddress)} viewMode={ViewMode.Display} />
                    </FieldFormUI>
                    <FieldFormUI title={this.getResourceByProperty(m => m.currentAddress)} headerType={"h4"}>
                        {/** Настоящ адрес */}
                        <GraoAddressUI {...this.bind(m => m.currentAddress)} viewMode={ViewMode.Display} />
                    </FieldFormUI>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.andCertificateReason)} required>
                    {/** Удостоверението да послужи */}
                    <div className="row">
                        <div className="form-group col-sm-6">
                            {this.dropDownListFor(m => m.andCertificateReason
                                , ResourceHelpers.getSelectListItemsForEnum(ANDCertificateReason)
                                , null
                                , null
                                , true
                                , this.getResource('GL_DDL_CHOICE_L'))}
                        </div>
                    </div>
                </FieldFormUI>
            </>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m)}>
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.permanentAddress)}</h4>
                            <GraoAddressUI {...this.bind(m => m.permanentAddress)} />
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{this.getResourceByProperty(m => m.currentAddress)}</h4>
                            <GraoAddressUI {...this.bind(m => m.currentAddress)} />
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.andCertificateReason)} >
                    <div className="row">
                        <div className="col-12 form-group">
                            <p className="field-text">{ResourceHelpers.getResourceByEmun(this.model.andCertificateReason, ANDCertificateReason)}</p>
                            {this.propertyErrorsDispleyFor(m => m.andCertificateReason)}
                        </div>
                    </div>
                </FieldFormUI>
            </>
        );
    }
}
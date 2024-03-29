﻿import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent, resourceManager } from "eau-core";
import { ApplicationFormManagerProps, withDocumentFormManager, FieldFormUI, GraoAddressUI, PoliceDepartmentUI } from "eau-documents";
import { observer } from "mobx-react";
import React from "react";
import { ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataVM } from "../../models/ModelsAutoGenerated";

interface ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataProps extends BaseProps, ApplicationFormManagerProps {
}

@observer export class ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataImpl
    extends EAUBaseComponent<ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataProps, ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataVM> {

    renderEdit(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.issuingPoliceDepartment)}>
                    <PoliceDepartmentUI  {...this.bind(m => m.issuingPoliceDepartment, ViewMode.Display)} />
                </FieldFormUI>
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
                <fieldset className="fields-group">
                    <legend>
                        <h3 className="field-title">{resourceManager.getResourceByKey("DOC_GL_ReceiveERefusal_L")}</h3>
                    </legend>
                    <div className="custom-control custom-checkbox">
                        {/** желая да получа отказа като електронен документ */}
                        {this.checkBoxFor(m => m.agreementToReceiveERefusal)}
                    </div>
                </fieldset>
            </>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.issuingPoliceDepartment)}>
                    <PoliceDepartmentUI  {...this.bind(m => m.issuingPoliceDepartment)} />
                </FieldFormUI>
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
                {this.model.agreementToReceiveERefusal === true
                    &&
                    < FieldFormUI title={this.getResource('DOC_GL_ProvidingRefusalDocument_L')}>
                        <p className="field-text check-item check-success">{this.getResourceByProperty(m => m.agreementToReceiveERefusal)}</p>
                    </FieldFormUI>}
            </>
        );
    }
}

export const ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataUI = withDocumentFormManager(ApplicationForIssuingOfControlCouponForDriverWithNoPenaltiesDataImpl)
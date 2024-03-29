﻿import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent, ResourceHelpers, attributesClassFormControlLabel } from "eau-core";
import { ApplicationType, DocumentMustServeToUI, EkatteAddressUI, FieldFormUI, PoliceDepartmentUI, withDocumentFormManager, ApplicationFormManagerProps } from "eau-documents";
import { observer } from "mobx-react";
import React from "react";
import { ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM } from "../../models/ModelsAutoGenerated";

interface AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutDataUIProps extends BaseProps, ApplicationFormManagerProps {
}

@observer class AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutDataImpl
    extends EAUBaseComponent<AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutDataUIProps, ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataVM> {

    renderEdit(): JSX.Element {
        return (
            <>
                {
                    this.props.documentFormManager.applicationType != ApplicationType.AppForFirstReg
                        ? <FieldFormUI title={this.getResourceByProperty(m => m.issuingPoliceDepartment)}>
                            <PoliceDepartmentUI {...this.bind(m => m.issuingPoliceDepartment, ViewMode.Display)} />
                        </FieldFormUI>
                        : <FieldFormUI title={this.getResourceByProperty(m => m.issuingPoliceDepartment)} required>
                            <PoliceDepartmentUI {...this.bind(m => m.issuingPoliceDepartment)} inlineHelpKey={'GL_DOC_issuingPoliceDepartment_Help_L'} />
                        </FieldFormUI>
                }
                <FieldFormUI title={this.getResource("DOC_PBZN_AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutData_L")} required >
                    {this.model.isRecipientEntity ?
                        <FieldFormUI title={ResourceHelpers.getResourceByProperty('entityManagementAddress', this.model)} required headerType={"h4"}>
                            <EkatteAddressUI {...this.bind(m => m.entityManagementAddress)} />
                        </FieldFormUI>
                        : null}
                    <FieldFormUI title={ResourceHelpers.getResourceByProperty('corespondingAddress', this.model)} required headerType={"h4"}>
                        <EkatteAddressUI {...this.bind(m => m.corespondingAddress)} />
                        <div className="row">
                            <div className="form-group col-sm-6">
                                {this.labelFor(m => m.phoneNumber, null, attributesClassFormControlLabel)}
                                {this.textBoxFor(m => m.phoneNumber)}
                            </div>
                        </div>
                    </FieldFormUI>
                </FieldFormUI>
                <FieldFormUI title={ResourceHelpers.getResourceByProperty('documentMustServeTo', this.model)} required>
                    <DocumentMustServeToUI {...this.bind(m => m.documentMustServeTo)} />
                </FieldFormUI>
                <FieldFormUI title={ResourceHelpers.getResourceByProperty('documentCertifyingTheAccidentOccurredOrOtherInformation', this.model)} required>
                    <div className="row">
                        <div className="form-group col-12">
                            {this.textAreaFor(m => m.documentCertifyingTheAccidentOccurredOrOtherInformation)}
                            {this.inlineHelpFor(m => m.documentCertifyingTheAccidentOccurredOrOtherInformation, 'GL_PBZN_DocumentCertifyingTheAccidentOccurredOrOtherInformationHelp_L')}
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI>
                    <div className="row mt-2">
                        <div className="col-12">
                            <div className="custom-control custom-checkbox">
                                {this.checkBoxFor(m => m.includeInformation107)}
                            </div>
                            <div className="custom-control custom-checkbox">
                                {this.checkBoxFor(m => m.includeInformation133)}
                            </div>
                        </div>
                    </div>
                </FieldFormUI>
            </>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.issuingPoliceDepartment)}>
                    <PoliceDepartmentUI {...this.bind(m => m.issuingPoliceDepartment)} />
                </FieldFormUI>
                <fieldset className="fields-group">
                    <legend><h3 className="field-title">{this.getResource("DOC_PBZN_AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutData_L")}</h3></legend>
                    {this.model.isRecipientEntity ?
                        <div className="row">
                            <div className="form-group col-12">
                                <h4 className="form-control-label">{ResourceHelpers.getResourceByProperty('entityManagementAddress', this.model)}</h4>
                                <EkatteAddressUI {...this.bind(m => m.entityManagementAddress)} />
                            </div>
                        </div>
                        : null}
                    <div className="row">
                        <div className="form-group col-12">
                            <h4 className="form-control-label">{ResourceHelpers.getResourceByProperty('corespondingAddress', this.model)}</h4>
                            <EkatteAddressUI {...this.bind(m => m.corespondingAddress)} />
                            {this.model.phoneNumber ?
                                <div className="row">
                                    <div className="form-group col-sm-6">
                                        {this.labelFor(m => m.phoneNumber, null, attributesClassFormControlLabel)}
                                        {this.textDisplayFor(m => m.phoneNumber)}
                                    </div>
                                </div>
                                : null}
                        </div>
                    </div>
                </fieldset>
                <fieldset className="fields-group">
                    <legend><h3 className="field-title">{ResourceHelpers.getResourceByProperty('documentMustServeTo', this.model)}</h3></legend>
                    <DocumentMustServeToUI {...this.bind(m => m.documentMustServeTo)} />
                </fieldset>
                <fieldset className="fields-group">
                    <legend><h3 className="field-title">{ResourceHelpers.getResourceByProperty("documentCertifyingTheAccidentOccurredOrOtherInformation", this.model)} </h3></legend>
                    {this.textDisplayFor(m => m.documentCertifyingTheAccidentOccurredOrOtherInformation)}
                </fieldset>
                {this.model.includeInformation107 === true || this.model.includeInformation133 === true ?
                    <fieldset className="fields-group">
                        <div className="row mt-2">
                            {
                                this.model.includeInformation107 === true && <div className="form-group col-12">
                                    <p className="field-text check-item check-success">
                                        {ResourceHelpers.getResourceByProperty('IncludeInformation107', this.model)}
                                    </p>
                                </div>
                            }
                            {
                                this.model.includeInformation133 === true && <div className="form-group col-12">
                                    <p className="field-text check-item check-success">
                                        {ResourceHelpers.getResourceByProperty('IncludeInformation133', this.model)}
                                    </p>
                                </div>
                            }
                        </div>
                    </fieldset>
                    : null
                }
            </>
        );
    }
}

export const AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutDataUI = withDocumentFormManager(AppForIssuingDocsOfIncurredAndOtherAccidentsAndProvideInfoOnTheActCarriedOutDataImpl)
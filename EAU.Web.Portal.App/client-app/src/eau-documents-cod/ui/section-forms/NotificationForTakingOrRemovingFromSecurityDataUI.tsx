﻿import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent, ResourceHelpers } from "eau-core";
import { ApplicationFormManagerProps, withDocumentFormManager, FieldFormUI, PoliceDepartmentUI } from "eau-documents";
import { observer } from "mobx-react";
import React from "react";
import { NotificationForTakingOrRemovingFromSecurityDataVM, NotificationType, ContractAssignor, AssignorPersonEntityType, PersonAssignorIdentifierType, PersonAssignorData } from "../../models/ModelsAutoGenerated";
import { ObjectHelper, SelectListItem } from "cnsys-core";
import { SecurityContractDataUI, AssignorUI } from "../field-forms";
import { action, observable } from "mobx";
import { isNotificationForTakingOrRemovingFromSecurityManager } from "../../form-managers/NotificationForTakingOrRemovingFromSecurityManager";

interface NotificationForTakingOrRemovingFromSecurityDataProps extends BaseProps, ApplicationFormManagerProps {
}

@observer export class NotificationForTakingOrRemovingFromSecurityDataImpl
    extends EAUBaseComponent<NotificationForTakingOrRemovingFromSecurityDataProps, NotificationForTakingOrRemovingFromSecurityDataVM> {

    private notificationTypesNew: SelectListItem[];
    private notificationTypesTermination: SelectListItem[];

    constructor(props: NotificationForTakingOrRemovingFromSecurityDataProps) {
        super(props);

        //Bind
        this.onRadioChange = this.onRadioChange.bind(this);

        //Init
        this.notificationTypesNew = [
            new SelectListItem({
                value: NotificationType.NewSecurityContr235789.toString(),
                text: ResourceHelpers.getResourceByEmun(NotificationType.NewSecurityContr235789, NotificationType),
                selected: this.model.notificationType === NotificationType.NewSecurityContr235789
            }),
            new SelectListItem({
                value: NotificationType.NewSecurityContr4.toString(),
                text: ResourceHelpers.getResourceByEmun(NotificationType.NewSecurityContr4, NotificationType),
                selected: this.model.notificationType === NotificationType.NewSecurityContr4
            })
        ];

        this.notificationTypesTermination = [
            new SelectListItem({
                value: NotificationType.TerminationSecurityContr235789.toString(),
                text: ResourceHelpers.getResourceByEmun(NotificationType.TerminationSecurityContr235789, NotificationType),
                selected: this.model.notificationType === NotificationType.TerminationSecurityContr235789
            }),
            new SelectListItem({
                value: NotificationType.TerminationSecurityContr4.toString(),
                text: ResourceHelpers.getResourceByEmun(NotificationType.TerminationSecurityContr4, NotificationType),
                selected: this.model.notificationType === NotificationType.TerminationSecurityContr4
            })
        ];
    }

    renderEdit(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.issuingPoliceDepartment)} required>
                    <div className="row">
                        <div className="form-group col-12">
                            <PoliceDepartmentUI  {...this.bind(m => m.issuingPoliceDepartment)} />
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.notificationType)} required>
                    <div className="row">
                        <div className="form-group col-12">
                            <fieldset>
                                <legend className="form-control-label">{this.getResource("DOC_COD_NotificationType_New")}</legend>
                                {this.radioButtonListFor(m => m.notificationType, this.notificationTypesNew, null, this.onRadioChange)}
                            </fieldset>
                        </div>
                        <div className="form-group col-12">
                            <fieldset>
                                <legend className="form-control-label">{this.getResource("DOC_COD_NotificationType_Termination")}</legend>
                                {this.radioButtonListFor(m => m.notificationType, this.notificationTypesTermination, null, this.onRadioChange)}
                            </fieldset>
                        </div>
                    </div>
                </FieldFormUI>
                {this.model.notificationType == NotificationType.NewSecurityContr235789 || this.model.notificationType == NotificationType.NewSecurityContr4 ?
                    <FieldFormUI title={this.getResourceByProperty(m => m.securityContractData)}>
                        <SecurityContractDataUI  {...this.bind(m => m.securityContractData)} mode={this.model.notificationType == NotificationType.NewSecurityContr235789 || this.model.notificationType == NotificationType.NewSecurityContr4 ? "new" : this.model.notificationType == NotificationType.TerminationSecurityContr4 ? "term4" : "term235789"} />
                    </FieldFormUI>
                    : null}
                {this.model.notificationType == NotificationType.NewSecurityContr235789 || this.model.notificationType == NotificationType.TerminationSecurityContr235789 ?
                    <FieldFormUI title={this.getResourceByProperty(m => m.contractAssignor)}>
                        <AssignorUI {...this.bind(m => m.contractAssignor)} notificationType={this.model.notificationType}/>
                    </FieldFormUI>
                    : null}
            </>
        );
    }


    renderDisplay(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.issuingPoliceDepartment)}>
                    <div className="row">
                        <div className="form-group col-12">
                            <PoliceDepartmentUI  {...this.bind(m => m.issuingPoliceDepartment)} />
                        </div>
                    </div>
                </FieldFormUI>
                <FieldFormUI title={this.getResourceByProperty(m => m.notificationType)}>
                    <div className="row">
                        <div className="col-12 form-group">
                            {ResourceHelpers.getResourceByEmun(this.model.notificationType, NotificationType)}
                        </div>
                    </div>
                </FieldFormUI>
                {this.model.notificationType == NotificationType.NewSecurityContr235789 || this.model.notificationType == NotificationType.NewSecurityContr4 ?
                    <FieldFormUI title={this.getResourceByProperty(m => m.securityContractData)}>
                        <SecurityContractDataUI  {...this.bind(m => m.securityContractData)} mode={this.model.notificationType == NotificationType.NewSecurityContr235789 || this.model.notificationType == NotificationType.NewSecurityContr4 ? "new" : this.model.notificationType == NotificationType.TerminationSecurityContr4 ? "term4" : "term235789"} />
                    </FieldFormUI>
                : null}
                {this.model.notificationType == NotificationType.NewSecurityContr235789 || this.model.notificationType == NotificationType.TerminationSecurityContr235789 ?
                    <FieldFormUI title={this.getResourceByProperty(m => m.contractAssignor)}>
                        <AssignorUI {...this.bind(m => m.contractAssignor)} notificationType={this.model.notificationType}/>
                    </FieldFormUI>
                    : null}
            </>
        );
    }

    @action private onRadioChange(e: any): void {

        this.model.securityContractData = undefined;
        this.model.contractAssignor = undefined;
       
        if (isNotificationForTakingOrRemovingFromSecurityManager(this.props.documentFormManager)) {
            this.props.documentFormManager.resetSecurityObjectsData();
            if (this.model.notificationType == NotificationType.NewSecurityContr235789 ||
                this.model.notificationType == NotificationType.TerminationSecurityContr235789) {
                this.model.contractAssignor = new ContractAssignor();
                this.model.contractAssignor.assignorPersonEntityType = AssignorPersonEntityType.Person;

                this.model.contractAssignor.personAssignorData = new PersonAssignorData(); 
                if (ObjectHelper.isNullOrUndefined(this.model.contractAssignor.personAssignorData.identifierType))
                    this.model.contractAssignor.personAssignorData.identifierType = PersonAssignorIdentifierType.EGN;
            }
        }
    }
}

export const NotificationForTakingOrRemovingFromSecurityDataUI = withDocumentFormManager(NotificationForTakingOrRemovingFromSecurityDataImpl);
﻿import { BaseProps } from "cnsys-ui-react";
import { EAUBaseComponent, ValidationSummary, ValidationSummaryStrategy } from "eau-core";
import { ApplicationFormManagerProps, withDocumentFormManager, FieldFormUI, ApplicationFormVMBase, EntityBasicData } from "eau-documents";
import { observer } from "mobx-react";
import React from "react";
import { SecurityObjectsDataVM } from "../../models/ModelsAutoGenerated";
import { SecurityObjectDataUI } from "../field-forms";
import { isNotificationForTakingOrRemovingFromSecurityManager } from "../../form-managers/NotificationForTakingOrRemovingFromSecurityManager";
import { TerminationSecurityObjectsWrapperUI } from "../field-forms/TerminationSecurityObjectsWrapperUI";

interface SecurityObjectsDataProps extends BaseProps, ApplicationFormManagerProps {
}

@observer export class SecurityObjectsDataImpl
    extends EAUBaseComponent<SecurityObjectsDataProps, SecurityObjectsDataVM> {

    private isForTermination: boolean;
    private applicant: EntityBasicData;
    
    constructor(props: SecurityObjectsDataProps) {
        super(props);

        if (isNotificationForTakingOrRemovingFromSecurityManager(this.props.documentFormManager))
            this.isForTermination = this.props.documentFormManager.isTerminationNotification();

            let app = this.props.documentFormManager.documentForm as ApplicationFormVMBase;
            this.applicant = app.electronicServiceApplicant.recipientGroup.recipient.itemEntityBasicData;
    }

    renderEdit(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.securityObjects)}>
                    {!this.isForTermination ? 
                        <SecurityObjectDataUI {...this.bind(m => m.securityObjects)} /> 
                    : 
                        <>
                            {this.model.securityObjects.length === 0 && <ValidationSummary model={this.model} strategy={ValidationSummaryStrategy.excludeAllExcept} propNames={["securityObjects"]} />}
                            <TerminationSecurityObjectsWrapperUI {...this.bind(m => m)} applicant={this.applicant}/>
                        </>
                    }
                </FieldFormUI>
            </>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                <FieldFormUI title={this.getResourceByProperty(m => m.securityObjects)}>
                    {this.model.securityObjects.length === 0 && <ValidationSummary model={this.model} strategy={ValidationSummaryStrategy.excludeAllExcept} propNames={["securityObjects"]} />}
                    <SecurityObjectDataUI {...this.bind(m => m.securityObjects)} />
                </FieldFormUI>
            </>
        );
    }
}

export const SecurityObjectsDataUI = withDocumentFormManager(SecurityObjectsDataImpl);
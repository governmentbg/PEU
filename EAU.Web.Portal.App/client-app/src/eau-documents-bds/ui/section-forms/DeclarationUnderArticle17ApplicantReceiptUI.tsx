import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import React from "react";
import { FieldFormUI, ServiceApplicantReceiptDataUI, ServiceTermTypeAndApplicantReceiptVM, ApplicationType, ApplicationFormManagerProps, withDocumentFormManager } from "eau-documents";

interface DeclarationUnderArticle17ApplicantReceiptUIProps extends BaseProps, ApplicationFormManagerProps {
}

class DeclarationUnderArticle17ApplicantReceiptUIImpl extends EAUBaseComponent<DeclarationUnderArticle17ApplicantReceiptUIProps, ServiceTermTypeAndApplicantReceiptVM> {

    constructor(props: DeclarationUnderArticle17ApplicantReceiptUIProps) {
        super(props);
    }

    renderEdit(): JSX.Element {
        if (this.model) {
            if (this.props.documentFormManager.applicationType == ApplicationType.AppForFirstReg) {
                return (
                    <FieldFormUI title={this.getResource("DOC_GL_ApplicationForDeclaration_ServiceApplicantReceiptData_L")} >
                        <ServiceApplicantReceiptDataUI {...this.bind(m => m.serviceApplicantReceiptData)} />
                    </FieldFormUI>
                );
            } else {
                return (
                    <FieldFormUI title={this.getResource("DOC_GL_ApplicationForDeclaration_ServiceApplicantReceiptData_L")} >
                        <ServiceApplicantReceiptDataUI {...this.bind(m => m.serviceApplicantReceiptData, ViewMode.Display)} actualViewMode={ViewMode.Edit} />
                    </FieldFormUI>
                );
            }
        }

        return null;
    }

    renderDisplay(): JSX.Element {
        if (this.model) {
            return (
                <FieldFormUI title={this.getResource("DOC_GL_ApplicationForDeclaration_ServiceApplicantReceiptData_L")} >
                    <ServiceApplicantReceiptDataUI {...this.bind(m => m.serviceApplicantReceiptData, ViewMode.Display)} />
                </FieldFormUI>
            );
        }

        return null;
    }
}

export const DeclarationUnderArticle17ApplicantReceiptUI = withDocumentFormManager(DeclarationUnderArticle17ApplicantReceiptUIImpl);
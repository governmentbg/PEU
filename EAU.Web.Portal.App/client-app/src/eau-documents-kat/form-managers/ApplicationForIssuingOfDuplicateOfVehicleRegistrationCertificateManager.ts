﻿import { ResourceHelpers } from 'eau-core';
import { ApplicationFormManagerBase, DocumentFormValidationContext, ElectronicServiceAuthorQualityType, PersonIdentifier, PersonIdentifierChoiceType, Section } from 'eau-documents';
import { action } from 'mobx';
import { ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateVM, OwnerVM, PersonEntityChoiceType } from '../models/ModelsAutoGenerated';
import { VehicleDataRequestUI } from '../ui/section-forms/VehicleDataRequestUI';
import { VehicleDataRequestValidator } from '../validations/VehicleDataRequestValidator';

export class ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateManager extends ApplicationFormManagerBase<ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateVM>{

    //#region ApplicationFormManagerBase

    createDocument(obj: any): ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateVM {
        return new ApplicationForIssuingOfDuplicateOfVehicleRegistrationCertificateVM(obj);
    }

    //#endregion

    protected initDocumentForm() {
        super.initDocumentForm();

        this.additionalData.showWarningForNonPaidSlip = true;
    }

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var circumstances = new Section();
        circumstances.code = "circumstances";
        circumstances.title = ResourceHelpers.getResourceByProperty(m => m.circumstances, this.documentForm);
        circumstances.form = this.documentForm.circumstances;
        circumstances.formUICmp = VehicleDataRequestUI;
        circumstances.validator = new VehicleDataRequestValidator();
        circumstances.validator.setValidationContext(validationContext);
        circumstances.validate = () => this.validateSection(circumstances);

        sections.splice(1, 0, circumstances);

        return sections;
    }

    @action public changeAuthorQuality(qualityType: ElectronicServiceAuthorQualityType) {
        var recipientGroup = this.documentForm.electronicServiceApplicant.recipientGroup;

        recipientGroup.authorWithQuality.selectedAuthorQuality = qualityType;

        let owner = new OwnerVM();
        this.documentForm.circumstances.ownersCollection.owners = [];

        if (qualityType == ElectronicServiceAuthorQualityType.Personal) {
            let author = this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author;
            owner.type = PersonEntityChoiceType.Person;
            owner.personIdentifier = new PersonIdentifier();
            owner.personIdentifier.item = author.itemPersonBasicData.identifier.item;
            owner.personIdentifier.itemElementName = author.itemPersonBasicData.identifier.itemElementName;
        } else if (qualityType == ElectronicServiceAuthorQualityType.LegalRepresentative) {
            owner.type = PersonEntityChoiceType.Entity;
        } else {
            owner.type = PersonEntityChoiceType.Person;
            owner.personIdentifier = new PersonIdentifier();
            owner.personIdentifier.item = null;
            owner.personIdentifier.itemElementName = PersonIdentifierChoiceType.EGN;
        }

        this.documentForm.circumstances.ownersCollection.owners.push(owner);
        this.documentForm.circumstances.ownersCollection.clearErrors();
    }
}
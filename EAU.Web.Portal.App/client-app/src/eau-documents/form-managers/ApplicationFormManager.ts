import { ArrayHelper, ErrorInfo, ErrorLevels, ObjectHelper } from "cnsys-core";
import { DeliveryChannel, eauAuthenticationService, ResourceHelpers, ErrMsgCodesConstants } from "eau-core";
import { action, runInAction } from "mobx";
import moment from 'moment';
import { Nomenclatures } from "../cache/Nomenclatures";
import { ApplicationFormVMBase, ApplicationType, DocumentModes, ElectronicServiceAuthorQualityType, EntityBasicData, IdentityDocumentBasicDataVM, IdentityDocumentType, PersonAndEntityChoiceType, PersonBasicDataVM, PersonIdentifier, PersonIdentifierChoiceType, PersonNames, ServiceApplicantReceiptDataAddress, ServiceApplicantReceiptDataUnitInAdministration, ServiceResultReceiptMethods, ServiceTermType, ItemChoiceType1, UnitInfo } from "../models";
import { moduleContext } from '../ModuleContext';
import { RegiXDataService } from "../services/RegiXDataService";
import { AttachedDocumentsUI } from "../ui/section-forms/AttachedDocumentsUI";
import { DeclarationsUI } from "../ui/section-forms/DeclarationsUI";
import { ElectronicServiceApplicantUI } from '../ui/section-forms/ElectronicServiceApplicantUI';
import { ServiceTermTypeAndApplicantReceiptUI } from "../ui/section-forms/ServiceTermTypeAndApplicantReceiptUI";
import { DeclarationsVMValidator } from "../validations/DeclarationVMValidator";
import { DocumentFormValidationContext } from "../validations/DocumentFormValidationContext";
import { ElectronicServiceApplicantVMValidator } from "../validations/ElectronicServiceApplicantVMValidator";
import { ServiceTermTypeAndApplicantReceiptVMValidator } from "../validations/ServiceTermTypeAndApplicantReceiptVMValidator";
import { DocumentFormManagerBase, IDocumentFormManager, Section } from "./DocumentFormManager";

export interface IApplicationFormManager extends IDocumentFormManager {
    applicationType: ApplicationType;
    getPossibleAuthorQualities: () => ElectronicServiceAuthorQualityType[];
    getPossibleRecipientTypes: () => PersonAndEntityChoiceType[];
    getPossibleRecipientIdentityDocumentTypes?: () => IdentityDocumentType[];

    initRecipientEntityData: () => Promise<void>;

    changeRecipientTypes: (recipientType: PersonAndEntityChoiceType) => void;
    changeAuthorQuality: (qualityType: ElectronicServiceAuthorQualityType) => void;
    changeDocumentMustServeTo?: (mustServeTo: ItemChoiceType1) => void;

    changeServiceTermType: (termType: ServiceTermType) => Promise<void>;
    getServiceTermType: ServiceTermType;
    changeServiceResultReceiptMethod: (ReceiptMethod: ServiceResultReceiptMethods) => Promise<void>;
    getUnitsInfo: (unitInfoType: "delivery" | "serving") => Promise<UnitInfo[]>;
    loadRegiXEnityData(uic: string, entityBasicData: EntityBasicData): Promise<void>;
    deliveryChannels: DeliveryChannel[];

    getTemplateFieldData(templateFieldKey: string): string;
    getTemplateFieldSize(templateFieldKey: string): string;
    declarationMapper(declarationCode: string, isSelected: boolean): void;

    getSelectedAuthorQuality: ElectronicServiceAuthorQualityType;
}

export abstract class ApplicationFormManagerBase<TApp extends ApplicationFormVMBase> extends DocumentFormManagerBase<TApp> implements IApplicationFormManager {

    //#region DocumentFormManagerBase

    protected initDocumentForm() {
        super.initDocumentForm();

        if (this.deliveryChannels && this.deliveryChannels.length == 1 && this.documentForm.serviceTermTypeAndApplicantReceipt) {
            //Ако има само един начин на получаване, го избираме.
            this.setServiceResultReceiptMethod(this.deliveryChannels[0].deliveryChannelID);
        }
    }

    protected createSections(validationContext: DocumentFormValidationContext): Section[] {
        var sections = super.createSections(validationContext);

        var applicant = new Section();
        applicant.code = "applicant";
        applicant.title = ResourceHelpers.getResourceByProperty("electronicServiceApplicant", ApplicationFormVMBase);
        applicant.form = this.documentForm.electronicServiceApplicant;
        applicant.formUICmp = ElectronicServiceApplicantUI;
        applicant.validator = new ElectronicServiceApplicantVMValidator();
        applicant.validator.setValidationContext(validationContext);
        applicant.validate = () => this.validateSection(applicant);

        sections.push(applicant);

        //Създаваме секция "Начин на получаване".
        var wayOfDeliverySection = new Section();
        wayOfDeliverySection.code = "wayOfDelivery";
        wayOfDeliverySection.title = ResourceHelpers.getResourceByProperty("serviceTermTypeAndApplicantReceipt", ApplicationFormVMBase);
        wayOfDeliverySection.form = this.documentForm.serviceTermTypeAndApplicantReceipt;
        wayOfDeliverySection.formUICmp = ServiceTermTypeAndApplicantReceiptUI;
        wayOfDeliverySection.validator = new ServiceTermTypeAndApplicantReceiptVMValidator();
        wayOfDeliverySection.validator.setValidationContext(validationContext);
        wayOfDeliverySection.validate = () => this.validateSection(wayOfDeliverySection);

        sections.push(wayOfDeliverySection);

        if (this.mode != DocumentModes.SignDocument && this.mode != DocumentModes.ViewDocument) {

            if (this.documentForm.declarations && this.documentForm.declarations.declarations && this.documentForm.declarations.declarations.length > 0) {
                var sDeclarations = new Section();
                sDeclarations.code = "declarations";
                sDeclarations.title = ResourceHelpers.getResourceByProperty("declarations", ApplicationFormVMBase);
                sDeclarations.form = this.documentForm.declarations;
                sDeclarations.formUICmp = DeclarationsUI;
                sDeclarations.validator = new DeclarationsVMValidator();
                sDeclarations.validator.setValidationContext(validationContext);
                sDeclarations.validate = () => this.validateSection(sDeclarations);

                sections.push(sDeclarations);
            }

            if (this.service.attachedDocumentTypes && this.service.attachedDocumentTypes.length > 0) {
                var documents = new Section();
                documents.code = "attachedDocuments";
                documents.title = ResourceHelpers.getResourceByProperty("attachedDocuments", ApplicationFormVMBase);
                documents.form = this.attachedDocuments;
                documents.formUICmp = AttachedDocumentsUI;
                documents.validate = this.validateAttachedDocs.bind(this);
                sections.push(documents);
            }
        } else {
            if (this.documentForm.declarations && this.documentForm.declarations.declarations && this.documentForm.declarations.declarations.length > 0) {
                var sDeclarations = new Section();
                sDeclarations.code = "declarations";
                sDeclarations.title = ResourceHelpers.getResourceByProperty("declarations", ApplicationFormVMBase);
                sDeclarations.form = this.documentForm.declarations;
                sDeclarations.formUICmp = DeclarationsUI;
                sDeclarations.validator = new DeclarationsVMValidator();
                sDeclarations.validator.setValidationContext(validationContext);
                sDeclarations.validate = () => this.validateSection(sDeclarations);

                sections.push(sDeclarations);
            }

            if (this.attachedDocuments && this.attachedDocuments.length > 0) {
                var documents = new Section();
                documents.code = "attachedDocuments";
                documents.title = ResourceHelpers.getResourceByProperty("attachedDocuments", ApplicationFormVMBase);
                documents.form = this.attachedDocuments;
                documents.formUICmp = AttachedDocumentsUI;
                documents.validate = this.validateAttachedDocs.bind(this);
                sections.push(documents);
            }
        }

        return sections;
    }

    //#endregion

    //#region IApplicationFormManager

    get applicationType() {
        return this.documentForm.electronicAdministrativeServiceHeader.applicationType;
    }

    get deliveryChannels() {
        return this.service?.deliveryChannels;
    }

    //#region ElectronicServiceApplicant

    getPossibleAuthorQualities(): ElectronicServiceAuthorQualityType[] {
        if (!this.additionalData.possibleAuthorQualities) {
            return null;
        }

        var authorQualities: ElectronicServiceAuthorQualityType[] = this.additionalData.possibleAuthorQualities.split(",").map(item => Number(item));

        if (this.documentForm.electronicAdministrativeServiceHeader.applicationType != ApplicationType.AppForFirstReg) {

            if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.selectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal) {
                authorQualities = [ElectronicServiceAuthorQualityType.Personal];
            } else {
                if (authorQualities.indexOf(ElectronicServiceAuthorQualityType.Personal) >= 0) {
                    authorQualities.splice(authorQualities.indexOf(ElectronicServiceAuthorQualityType.Personal), 1)
                }

                if (this.documentForm.electronicServiceApplicant.recipientGroup.recipient.itemPersonBasicData && authorQualities.indexOf(ElectronicServiceAuthorQualityType.LegalRepresentative) >= 0) {
                    authorQualities.splice(authorQualities.indexOf(ElectronicServiceAuthorQualityType.LegalRepresentative), 1)
                }
            }
        }

        return authorQualities;
    }

    getPossibleRecipientTypes(): PersonAndEntityChoiceType[] {
        if (this.additionalData.possibleRecipientTypes) {

            if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.selectedAuthorQuality == ElectronicServiceAuthorQualityType.LegalRepresentative)
                return [PersonAndEntityChoiceType.Entity]

            return this.additionalData.possibleRecipientTypes.split(",").map(item => Number(item));
        }

        return null;
    }

    getPossibleRecipientIdentityDocumentTypes(): IdentityDocumentType[] {
        if (this.additionalData.possibleRecipientIdentityDocumentTypes) {
            return this.additionalData.possibleRecipientIdentityDocumentTypes.split(",").map(item => Number(item));
        }

        return null;
    }

    @action initRecipientEntityData(): Promise<void> {
        if (this.documentForm.electronicServiceApplicant.recipientGroup.recipient.selectedChoiceType == PersonAndEntityChoiceType.Entity) {
            let that = this;
            let identifier: string = this.documentForm.electronicServiceApplicant.recipientGroup.recipient.itemEntityBasicData.identifier;
            let eikErrors = this.documentForm.electronicServiceApplicant.recipientGroup.recipient.itemEntityBasicData.getPropertyErrors('identifier');

            if (ObjectHelper.isStringNullOrEmpty(identifier) || (eikErrors && eikErrors.length > 0)) {
                return Promise.resolve().then(() => {
                    runInAction(() => {
                        if (ObjectHelper.isStringNullOrEmpty(identifier) && (ObjectHelper.isNullOrUndefined(eikErrors) || eikErrors.length == 0)) {
                            that.documentForm.electronicServiceApplicant.recipientGroup.recipient.itemEntityBasicData.addError('identifier', ResourceHelpers.formatErrorMessage(ErrMsgCodesConstants.DefaultNotEmptyErrorMessage, new EntityBasicData(), 'identifier'));
                        }
                    });
                });
            } else {

                return this.loadRegiXEnityData(identifier, that.documentForm.electronicServiceApplicant.recipientGroup.recipient.itemEntityBasicData)
            }
        } else {
            throw new Error('Not supported method call.');
        }
    }

    @action changeRecipientTypes(recipientType: PersonAndEntityChoiceType) {

        var recipient = this.documentForm.electronicServiceApplicant.recipientGroup.recipient;

        if (recipientType == PersonAndEntityChoiceType.Person) {
            recipient.selectedChoiceType = recipientType;
            recipient.itemEntityBasicData = undefined;
            recipient.itemForeignCitizenBasicData = undefined;
            recipient.itemForeignEntityBasicData = undefined;

            if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.selectedAuthorQuality == ElectronicServiceAuthorQualityType.Personal) {
                recipient.itemPersonBasicData = new PersonBasicDataVM(JSON.parse(JSON.stringify(this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.itemPersonBasicData)));

            } else {
                if (!recipient.itemPersonBasicData || this.applicationType == ApplicationType.AppForFirstReg) {
                    recipient.itemPersonBasicData = new PersonBasicDataVM();
                    recipient.itemPersonBasicData.identifier = new PersonIdentifier();
                    recipient.itemPersonBasicData.identifier.itemElementName = PersonIdentifierChoiceType.EGN;
                    recipient.itemPersonBasicData.identityDocument = new IdentityDocumentBasicDataVM();
                    recipient.itemPersonBasicData.names = new PersonNames();
                }
            }
        } else if (recipientType == PersonAndEntityChoiceType.Entity) {
            recipient.selectedChoiceType = recipientType;
            recipient.itemPersonBasicData = undefined;
            recipient.itemForeignCitizenBasicData = undefined;
            recipient.itemForeignEntityBasicData = undefined;

            if (!recipient.itemEntityBasicData || ObjectHelper.isStringNullOrEmpty(recipient.itemEntityBasicData.name)) {

                recipient.itemEntityBasicData = new EntityBasicData();

                eauAuthenticationService.getCurrentUser().then(user => {

                    if (user && !ObjectHelper.isStringNullOrEmpty(user.uic))
                        this.loadRegiXEnityData(user.uic, recipient.itemEntityBasicData);
                })
            }
        } else {
            throw "Not supported recipientType."
        }
    }

    @action public changeAuthorQuality(qualityType: ElectronicServiceAuthorQualityType) {

        var recipientGroup = this.documentForm.electronicServiceApplicant.recipientGroup;

        recipientGroup.authorWithQuality.selectedAuthorQuality = qualityType;

        if (this.documentForm.electronicAdministrativeServiceHeader.applicationType == ApplicationType.AppForFirstReg) {

            if (qualityType == ElectronicServiceAuthorQualityType.Personal) {
                this.changeRecipientTypes(PersonAndEntityChoiceType.Person);

            } else if (qualityType == ElectronicServiceAuthorQualityType.LegalRepresentative) {
                this.changeRecipientTypes(PersonAndEntityChoiceType.Entity);

            } else {
                if (this.getPossibleRecipientTypes().indexOf(PersonAndEntityChoiceType.Person) >= 0) {
                    this.changeRecipientTypes(PersonAndEntityChoiceType.Person);
                } else {
                    this.changeRecipientTypes(PersonAndEntityChoiceType.Entity);
                }
            }
        }
    }

    get getSelectedAuthorQuality(): ElectronicServiceAuthorQualityType {
        return this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.selectedAuthorQuality;
    }

    //#endregion

    //#region ServiceTermTypeAndApplicantReceipt

    @action public changeServiceTermType(termType: ServiceTermType): Promise<void> {
        this.documentForm.serviceTermTypeAndApplicantReceipt.serviceTermType = termType;

        return Promise.resolve();
    }

    get getServiceTermType(): ServiceTermType {
        return this.documentForm.serviceTermTypeAndApplicantReceipt.serviceTermType;
    }

    @action public changeServiceResultReceiptMethod(ReceiptMethod: ServiceResultReceiptMethods): Promise<void> {
        this.setServiceResultReceiptMethod(ReceiptMethod);

        if (ReceiptMethod == ServiceResultReceiptMethods.UnitInAdministration) {
            return this.getUnitsInfo("delivery").bind(this).then(unitsInfo => {
                if (unitsInfo.length == 1) {
                    let unit = this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.unitInAdministration;

                    runInAction(() => {
                        unit.administrativeDepartmentCode = unitsInfo[0].unitID.toString();
                        unit.administrativeDepartmentName = unitsInfo[0].name;
                    });
                }
            });
        } else {
            return Promise.resolve();
        }
    }

    getUnitsInfo(unitInfoType: "delivery" | "serving"): Promise<UnitInfo[]> {
        if (unitInfoType == "delivery")
            return Nomenclatures.getDeliveryUnitsInfo(this.service.serviceID);
        else
            return Nomenclatures.getServingUnitsInfo(this.service.serviceID);
    }

    //#endregion

    public loadRegiXEnityData(uic: string, entityBasicData: EntityBasicData): Promise<void> {
        return new RegiXDataService().getEntityData(uic).then((entityData) => {
            if (entityData) {
                runInAction(() => {
                    entityBasicData.clearErrors(true);
                    entityBasicData.name = entityData.name;
                    entityBasicData.identifier = entityData.identifier;

                    return Promise.resolve();
                })
            } else {
                entityBasicData.name = null;
            }

            return Promise.resolve();
        });
    }

    public getTemplateFieldData(templateFieldKey: string): string {
        if (ObjectHelper.isStringNullOrEmpty(templateFieldKey))
            return null;

        switch (templateFieldKey) {
            case '{APPLICANT_EGN_LNCH}': {
                if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.selectedChoiceType == PersonAndEntityChoiceType.Person)
                    return this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.itemPersonBasicData.identifier.item;
                else
                    return null;
            }
            case '{APPLICANT_PERSON_NAME}': {
                if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.selectedChoiceType == PersonAndEntityChoiceType.Person) {
                    let personNames = this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.itemPersonBasicData.names;

                    return `${personNames.first}${ObjectHelper.isStringNullOrEmpty(personNames.middle) ? '' : ' ' + personNames.middle} ${personNames.last}`;
                } else
                    return null;
            }
            case '{APPLICANT_AUTHORITY}': {
                if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.selectedChoiceType == PersonAndEntityChoiceType.Person) {
                    return this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality
                        .author.itemPersonBasicData.identityDocument.identityIssuer;
                } else
                    return null;
            }
            case '{APPLICANT_DATE_OF_ISSUE}': {
                if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.selectedChoiceType == PersonAndEntityChoiceType.Person) {
                    return this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality
                        .author.itemPersonBasicData.identityDocument.identitityIssueDate.format('l');
                } else
                    return null;
            }
            case '{APPLICANT_DOCUMENT_NUMBER}': {
                if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.selectedChoiceType == PersonAndEntityChoiceType.Person) {
                    return this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality
                        .author.itemPersonBasicData.identityDocument.identityNumber;
                } else
                    return null;
            }
            case '{APPLICANT_DOCUMENT_TYPE}': {
                if (this.documentForm.electronicServiceApplicant.recipientGroup.recipient.selectedChoiceType == PersonAndEntityChoiceType.Person) {
                    let docTypeId: IdentityDocumentType = this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality
                        .author.itemPersonBasicData.identityDocument.identityDocumentType;

                    return ResourceHelpers.getResourceByEmun(docTypeId, IdentityDocumentType);
                }
                else
                    return null;
            }
            case '{COMPANY_NAME}': {
                if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.selectedChoiceType == PersonAndEntityChoiceType.Entity)
                    return this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.itemEntityBasicData.name;
                else
                    return null;
            }
            case '{EIK_BULSTAT_PIK}': {
                if (this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.selectedChoiceType == PersonAndEntityChoiceType.Entity)
                    return this.documentForm.electronicServiceApplicant.recipientGroup.authorWithQuality.author.itemEntityBasicData.identifier;
                else
                    return null;
            }
            case '{CURRENT_DATE}': {
                let dateNow: moment.Moment = moment();
                return dateNow.format('l');
            }
            default:
                return '';
        }
    }

    getTemplateFieldSize(templateFieldKey: string): string {
        if (ObjectHelper.isStringNullOrEmpty(templateFieldKey))
            return null;

        switch (templateFieldKey) {
            case '{APPLICANT_EGN_LNCH}':
            case '{EIK_BULSTAT_PIK}':
            case '{CURRENT_DATE}':
            case '{APPLICANT_DATE_OF_ISSUE}':
            case '{APPLICANT_DOCUMENT_NUMBER}':
                return '10';
            case '{APPLICANT_PERSON_NAME}':
            case '{COMPANY_NAME}':
            case '{APPLICANT_AUTHORITY}':
            case '{APPLICANT_DOCUMENT_TYPE}':
            case '{APPLICANT_PERMANENT_ADDRESS}':
                return '45';
            default:
                return '40';
        }
    }

    public declarationMapper(declarationCode: string, isSelected: boolean) {

    }

    //#endregion

    @action private setServiceResultReceiptMethod(ReceiptMethod: ServiceResultReceiptMethods): void {
        this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.serviceResultReceiptMethod = ReceiptMethod;

        if (ReceiptMethod == ServiceResultReceiptMethods.CourierToOtherAddress) {
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.applicantAdress = new ServiceApplicantReceiptDataAddress();
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.unitInAdministration = undefined;
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.postOfficeBox = undefined;
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.municipalityAdministrationAdress = undefined;
        } else if (ReceiptMethod == ServiceResultReceiptMethods.UnitInAdministration) {
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.applicantAdress = undefined;
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.postOfficeBox = undefined;
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.municipalityAdministrationAdress = undefined;

            if (!this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.unitInAdministration)
                this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.unitInAdministration = new ServiceApplicantReceiptDataUnitInAdministration();

            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.unitInAdministration.entityBasicData = this.documentForm.electronicAdministrativeServiceHeader.electronicServiceProviderBasicData.entityBasicData;
        } else {
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.applicantAdress = undefined;
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.unitInAdministration = undefined;
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.postOfficeBox = undefined;
            this.documentForm.serviceTermTypeAndApplicantReceipt.serviceApplicantReceiptData.municipalityAdministrationAdress = undefined;
        }
    }

    @action private validateAttachedDocs(): boolean {
        if (!this.attachedDocuments || this.attachedDocuments.length == 0)
            return true;

        let attachedDocSection = ArrayHelper.queryable.from(this.sections).singleOrDefault(s => s.code == "attachedDocuments");
        let attachedDocWithErr = ArrayHelper.queryable.from(this.attachedDocuments).firstOrDefault(ad => ObjectHelper.isNullOrUndefined(ad.documentProcessContentID));

        if (attachedDocWithErr) {
            attachedDocSection.errors = [];

            let err: ErrorInfo = {
                error: moduleContext.resourceManager.getResourceByKey('GL_UNSIGNED_ATTACHED_DOCUMENT_GENERATED_BY_TEMPLATE_E'),
                level: ErrorLevels.Error,
                errorContainer: 'attachedDocuments',
                propertyName: 'attachedDocuments'
            };

            attachedDocSection.errors.push(err);

            if (!attachedDocWithErr.hasErrors()) {
                attachedDocWithErr.addError(err.error, err.level);
            }

            return false;
        } else {
            attachedDocSection.errors = [];

            for (let i: number = 0; i < this.attachedDocuments.length; i++) {
                this.attachedDocuments[i].clearErrors();
            }

            return true;
        }
    }
}

export function isApplicationFormManager(manager: IApplicationFormManager | any): manager is IApplicationFormManager {
    return manager && ObjectHelper.isSubClassOf(manager, ApplicationFormManagerBase);
}
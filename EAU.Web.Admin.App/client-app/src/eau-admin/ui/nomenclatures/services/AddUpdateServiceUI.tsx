import { ObjectHelper, SelectListItem } from "cnsys-core";
import { AsyncUIProps, BaseProps, BaseRouteProps, IMultipleSelectListItem, withAsyncFrame } from "cnsys-ui-react";
import {
    AdmServiceTermType,
    attributeClassRequiredLabel,
    attributesClassFormControlMaxL450,
    Constants as ConstantsEAU,
    Declaration,
    DeliveryChannel,
    DocumentType,
    EAUBaseComponent,
    Nomenclatures,
    ResourceHelpers,
    Service,
    ServiceTerm,
    TextEditorUI, ValidationSummary, ValidationSummaryStrategy,
    WaysToStartService
} from "eau-core";
import { action, observable, runInAction } from "mobx";
import { observer } from "mobx-react";
import moment from "moment";
import React from "react";
import { withRouter } from "react-router";
import { Link } from "react-router-dom";
import { Button } from "reactstrap";
import { Constants } from "../../../Constants";
import { NomenclaturesDataService } from "../../../services/NomenclaturesDataService";
import { AddUpdateServiceValidator } from "../../../validations/ServiceValidator";

interface AddUpdateServiceProps extends BaseProps, BaseRouteProps<any>, AsyncUIProps {
}

@observer
class AddUpdateServiceUIImpl extends EAUBaseComponent<AddUpdateServiceProps, Service> {

    private serviceId: any;
    private defaultSunauServiceUri: string;

    @observable isLoaded: boolean;
    @observable private notification: any;
    @observable private sunauUriWarning;
    @observable private reRender: boolean;
    @observable private selectedServiceTerms: SelectListItem[];

    @observable private documentTypes: DocumentType[];
    @observable private selectedDocumentTypes: IMultipleSelectListItem[];
    @observable private selectedGroupItems: SelectListItem[];

    @observable private deliveryChannels: DeliveryChannel[];
    @observable private declarations: Declaration[];

    @observable private selectedDeliveryChannels: IMultipleSelectListItem[];
    @observable private selectedDocumentTypesAttachableDocuments: IMultipleSelectListItem[];
    @observable private selectedDeclarations: IMultipleSelectListItem[];

    private nomenclatureDataService: NomenclaturesDataService;

    constructor(props: AddUpdateServiceProps) {
        super(props);

        this.onRadioChange = this.onRadioChange.bind(this);
        this.handleDocumentTypesChange = this.handleDocumentTypesChange.bind(this);
        this.handleDeclarationsChange = this.handleDeclarationsChange.bind(this);
        this.getModelDocumentTypes = this.getModelDocumentTypes.bind(this);
        this.getModelDeclarations = this.getModelDeclarations.bind(this);
        this.handleDeliveryChannelsChange = this.handleDeliveryChannelsChange.bind(this);
        this.getModelDeliveryChannels = this.getModelDeliveryChannels.bind(this);
        this.getModelServiceTerms = this.getModelServiceTerms.bind(this);
        this.addUpdateService = this.addUpdateService.bind(this);
        this.clearUnusedFields = this.clearUnusedFields.bind(this);
        this.onSunauServiceUriChange = this.onSunauServiceUriChange.bind(this);
        this.handleInitiationTypeIdChange = this.handleInitiationTypeIdChange.bind(this);
        this.checkForValidOrderNumber = this.checkForValidOrderNumber.bind(this);
        this.handleSelectedDocumentTypesChange = this.handleSelectedDocumentTypesChange.bind(this);
        this.getSelectedDocumentType = this.getSelectedDocumentType.bind(this);

        this.init();
    }

    render() {
        let reRender = this.reRender;

        return !this.serviceId || (this.serviceId && this.isLoaded) ? <div className="card">
            <div className="card-body">
                {this.notification}
                <div className="row">
                    <div className="form-group col-md-4">
                        {this.labelFor(m => m.isActive, "GL_STATUS_L")}
                        <div className="form-inline">
                            <div className="custom-control-inline custom-control custom-radio">
                                <input disabled={!!this.serviceId} className="custom-control-input" type="radio" name="active" id="active" checked={this.model.isActive} onChange={this.onRadioChange} />
                                <label className="custom-control-label"
                                    htmlFor="active">{this.getResource("GL_ACTIVE_L")}</label>
                            </div>
                            <div className="custom-control-inline custom-control custom-radio">
                                <input disabled={!!this.serviceId} className="custom-control-input" type="radio" name="inactive" id="inactive" checked={!this.model.isActive} onChange={this.onRadioChange} />
                                <label className="custom-control-label"
                                    htmlFor="inactive">{this.getResource("GL_INACTIVE_L")}</label>
                            </div>
                        </div>
                    </div>
                    {this.serviceId && <div className="col-sm-6 col-lg-4">
                        <div className="row">
                            <div className="col">
                                {this.labelFor(m => m.updatedOn, "GL_CREATE_UPDATE_DATE_L")}
                            </div>
                        </div>
                        <div className="row">
                            <div className="form-group col-12 col-md-8 col-xl-6">
                                <input id="Input_updatedOn" type="text" className="form-control" disabled value={this.model.updatedOn && this.model.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime)} />
                            </div>
                        </div>
                    </div>}
                </div>
                <ValidationSummary model={this.model} propNames={["emptyServiceTermsError"]} strategy={ValidationSummaryStrategy.excludeAllExcept}/>
                {this.sunauUriWarning}
                <div className="row">
                    <div className="form-group col-12">
                        {this.labelFor(m => m.name, "GL_DESIGNATION_L", attributeClassRequiredLabel)}
                        {this.textBoxFor(m => m.name, attributesClassFormControlMaxL450)}
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-md-4">
                        {this.labelFor(m => m.groupID, "GL_SERVICE_GROUP_NAME_L", attributeClassRequiredLabel)}
                        {this.dropDownListFor(m => m.groupID, this.selectedGroupItems, null, null, true, this.getResource("GL_CHOICE_L"))}
                    </div>
                    <div className="form-group col-12 col-md-4">
                        <div className="row">
                            <div className="col">
                                {this.labelFor(m => m.orderNumber, "GL_ORDER_NUMBER_L", attributeClassRequiredLabel)}
                            </div>
                        </div>
                        <div className="row">
                            <div className="form-group col-12 col-md-8 col-xl-6">
                                {this.textBoxFor(m => m.orderNumber, null, this.checkForValidOrderNumber)}
                            </div>
                        </div>
                    </div>
                    <div className="form-group col-12 col-md-4">
                        <div className="row">
                            <div className="col">
                                {this.labelFor(m => m.sunauServiceUri, "GL_URI_ADM_SERVICE_L", attributeClassRequiredLabel)}
                            </div>
                        </div>
                        <div className="row">
                            <div className="form-group col-12 col-md-8 col-xl-6">
                                {this.textBoxFor(m => m.sunauServiceUri, attributesClassFormControlMaxL450, this.onSunauServiceUriChange )}
                            </div>
                        </div>
                    </div>
                </div>
                <div className="row">
                    <div className="form-group col-md-4">
                        {this.labelFor(m => m.initiationTypeID, "GL_SERVICE_INITIATION_METHOD_L", attributeClassRequiredLabel)}
                        {this.dropDownListFor(m => m.initiationTypeID, ResourceHelpers.getSelectListItemsForEnum(WaysToStartService), null, this.handleInitiationTypeIdChange, true, this.getResource("GL_CHOICE_L"))}
                    </div>

                    {this.isInitiationByApplication() && <>
                        <div className="form-group col-12 col-md-4">
                            {this.labelFor(m => m.documentTypeID, "GL_APPLICATION_INITIATION_SERVICE_L", attributeClassRequiredLabel)}
                            {this.selectFor(m => m.documentTypeID, this.selectedDocumentTypes, this.getSelectedDocumentType(), null, this.getResource("GL_ALL_L"), this.handleSelectedDocumentTypesChange, false)}
                        </div>

                        <div className="form-group col-md-4">
                            {this.labelFor(m => m.seviceTerms, "GL_TYPE_SERVICES_L", attributeClassRequiredLabel)}
                            <div className="form-inline">
                                <div className="custom-control-inline custom-control custom-checkbox">
                                    <input className="custom-control-input" id="regular" type="checkbox" checked={this.isCheckedServiceTerm(AdmServiceTermType.Regular)} onChange={() => this.toggleServiceTermCheckbox(AdmServiceTermType.Regular)} />
                                    <label className="custom-control-label"
                                        htmlFor="regular">{this.getResource("GL_AdmServiceTermType_Regular_L")}</label>
                                </div>
                                <div className="custom-control-inline custom-control custom-checkbox">
                                    <input className="custom-control-input" id="fast" type="checkbox" checked={this.isCheckedServiceTerm(AdmServiceTermType.Fast)} onChange={() => this.toggleServiceTermCheckbox(AdmServiceTermType.Fast)} />
                                    <label className="custom-control-label"
                                        htmlFor="fast">{this.getResource("GL_AdmServiceTermType_Fast_L")}</label>
                                </div>
                                <div className="custom-control-inline custom-control custom-checkbox">
                                    <input className="custom-control-input" id="express" type="checkbox" checked={this.isCheckedServiceTerm(AdmServiceTermType.Express)} onChange={() => this.toggleServiceTermCheckbox(AdmServiceTermType.Express)} />
                                    <label className="custom-control-label"
                                        htmlFor="express">{this.getResource("GL_AdmServiceTermType_Express_L")}</label>
                                </div>
                            </div>
                        </div>
                    </>}
                    {this.isInitiationByRedirectToWebPage() && <>
                        <div className="form-group col-12 col-md-4">
                            {this.labelFor(m => m.serviceUrl, "GL_SERVICE_ADDRESS_L", attributeClassRequiredLabel)}
                            {this.textBoxFor(m => m.serviceUrl)}
                        </div>
                    </>}
                </div>

                {this.isInitiationByApplication() && <>
                    <div className="row">
                        <div className="form-group col-12 col-md-4">
                            {this.labelFor(m => m.deliveryChannels, "GL_DELIVERY_METHOD_L", attributeClassRequiredLabel)}
                            {this.selectFor(m => m.deliveryChannels, this.selectedDeliveryChannels, this.getModelDeliveryChannels(), null, this.getResource("GL_CHOICE_L"), this.handleDeliveryChannelsChange)}
                        </div>
                        <div className="form-group col-12 col-md-4">
                            {this.labelFor(m => m.admStructureUnitName, "GL_NAME_STRUCTURAL_UNIT_L", attributeClassRequiredLabel)}
                            {this.textBoxFor(m => m.admStructureUnitName, attributesClassFormControlMaxL450)}
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-12 col-md-8">
                            {this.labelFor(m => m.resultDocumentName, "GL_Service_resultDocumentName_L")}
                            {this.textBoxFor(m => m.resultDocumentName)}
                        </div>
                    </div>
                </>}
                {(this.isInitiationByApplication() || this.isInitiationByRedirectToWebPage()) &&
                    <div className="row">
                        <div className="form-group col-sm-12">
                        {this.labelFor(m => m.description, "GL_DESCRIPTION_L")}
                        <TextEditorUI {...this.bind(m => m.description)}  />
                        </div>
                    </div>}
                {this.isInitiationByApplication() && <>
                    <div className="row">
                        <div className="form-group col-sm-12 col-md-6">
                            {this.labelFor(m => m.explanatoryTextService, "GL_DESCRIPTION_SERVICE_L")}
                            <TextEditorUI {...this.bind(m => m.explanatoryTextService)} />
                        </div>

                        <div className="form-group col-sm-12 col-md-6">
                            {this.labelFor(m => m.explanatoryTextRefusedOrTerminatedService, "GL_DESCRIPTION_DENIAL_SERVICE_L")}
                            <TextEditorUI {...this.bind(m => m.explanatoryTextRefusedOrTerminatedService)} />
                        </div>
                    </div>

                    <div className="row">
                        <div className="form-group col-sm-12 col-md-6">
                            {this.labelFor(m => m.explanatoryTextFulfilledService, "GL_DESCRIPTION_COMPLETED_SERVICE_L")}
                            <TextEditorUI {...this.bind(m => m.explanatoryTextFulfilledService)} />
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-sm-12 ">
                            {this.labelFor(m => m.attachedDocumentsDescription, "GL_DESCRIPTION_DOC_ATTACHED_L")}
                            <TextEditorUI {...this.bind(m => m.attachedDocumentsDescription)}  />
                        </div>
                    </div>

                    <div className="row">
                        <div className="form-group col-md-12">
                            {this.labelFor(m => m.attachedDocumentTypes, "GL_Service_attachedDocumentTypes_L")}
                            {this.selectFor(m => m.attachedDocumentTypes, this.selectedDocumentTypesAttachableDocuments, this.getModelDocumentTypes(), null, this.getResource("GL_CHOICE_L"), this.handleDocumentTypesChange)}
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-md-12">
                            {this.labelFor(m => m.declarations, "GL_Service_declarations_L")}
                            {this.selectFor(m => m.declarations, this.selectedDeclarations, this.getModelDeclarations(), null, this.getResource("GL_CHOICE_L"), this.handleDeclarationsChange)}
                        </div>
                    </div>
                </>}
                {
                    (this.isInitiationByApplication() || this.isInitiationByRedirectToWebPage()) && <div className="row">
                        <div className="form-group col-md-12">
                            {this.labelFor(m => m.additionalDataAsString, "GL_CONFIGURATION_DATA_L")}
                            {this.textAreaFor(m => m.additionalDataAsString)}
                        </div>
                    </div>
                }
            </div>
            <div className="card-footer">
                <div className="button-bar">
                    <div className="right-side">
                        <Button type="button" color="primary"
                            onClick={this.addUpdateService}>{this.getResource("GL_SAVE_L")}</Button>
                    </div>
                    <div className="left-side">
                        <Link to={Constants.PATHS.NomServices}
                            className="btn btn-secondary">{this.getResource("GL_REFUSE_L")}</Link>
                    </div>
                </div>
            </div>
        </div> : null;
    }

    private getSelectedDocumentType(): IMultipleSelectListItem[] {
        if (!ObjectHelper.isNullOrUndefined(this.model.documentTypeID)) {
            return this.documentTypes.filter(d => this.model.documentTypeID == d.documentTypeID).map(d => {
                return { label: d.name, value: d.documentTypeID.toString() };
            })
        }
    }

    private handleSelectedDocumentTypesChange(documentType: IMultipleSelectListItem) {
        if (documentType) {
            this.model.documentTypeID = this.documentTypes.filter(d => documentType.value == d.documentTypeID.toString())[0].documentTypeID;
        }
    }

    @action
    private init() {
        this.serviceId = this.props.match.params.serviceID;

        this.isLoaded = false;
        this.nomenclatureDataService = new NomenclaturesDataService();

        this.model = new Service();
        this.model.isActive = false;
        this.model.seviceTerms = [];

        this.validators = [new AddUpdateServiceValidator()];

        this.props.registerAsyncOperation(Promise.all([this.getSelectedGroupItems(), this.getDocumentTypes(), this.getDeliveryChannels(), this.getDeclarations()]).then(() => {
            if (this.serviceId) {
                this.getServiceById();
            }
        }));

        this.selectedServiceTerms = ResourceHelpers.getSelectListItemsForEnum(AdmServiceTermType);
    }

    private checkForValidOrderNumber() {
        if(!Number.isInteger(this.model.orderNumber)) {
            this.model.orderNumber = null;
        }
    }

    private onSunauServiceUriChange() {
        if(this.serviceId) {
                if (this.model.sunauServiceUri !== this.defaultSunauServiceUri) {
                    this.sunauUriWarning = <div className="alert alert-dismissible alert-warning fade show">
                        <p>{this.getResource("GL_CANNOT_REVIEW_APPL_IF_CHANGE_URI_ADM_SERVICE_E")}</p>
                    </div>
                }
                else  {
                    this.sunauUriWarning = null;
                }
        }
    }

    private isInitiationByApplication(): boolean {
        return this.model.initiationTypeID == WaysToStartService.ByAplication;
    }

    private isInitiationByRedirectToWebPage(): boolean {
        return this.model.initiationTypeID == WaysToStartService.ByRedirectToWebPage;
    }

    @action
    private getModelServiceTerms() {
        if (!ObjectHelper.isArrayNullOrEmpty(this.model.seviceTerms)) {
            this.model.seviceTerms.map(s => s.serviceTermType).forEach(serviceType => {
                this.selectedServiceTerms.forEach(selectedServiceTermType => {
                    if (selectedServiceTermType.value == serviceType.toString()) {
                        selectedServiceTermType.selected = true;
                    }
                })
            });
            this.reRender = !this.reRender;
        }
    }

    private isCheckedServiceTerm(serviceTermType: AdmServiceTermType): boolean {
        let currentServiceTerm = this.selectedServiceTerms.filter(serviceTerm => serviceTerm.value == serviceTermType.toString())[0];

        if (currentServiceTerm.selected) {
            return true;
        }
        return false;
    }

    @action
    private toggleServiceTermCheckbox(serviceTermType: AdmServiceTermType) {
        let currentServiceTerm = this.selectedServiceTerms.filter(serviceTerm => serviceTerm.value == serviceTermType.toString())[0];
        currentServiceTerm.selected = !currentServiceTerm.selected;
        this.reRender = !this.reRender;

        if (currentServiceTerm.selected) {
            let serviceTerm = new ServiceTerm();
            serviceTerm.serviceTermType = serviceTermType;
            serviceTerm.isActive = true;
            serviceTerm.description = currentServiceTerm.text;

            this.model.seviceTerms.push(serviceTerm);
        } else {
            this.model.seviceTerms = this.model.seviceTerms.filter(serviceTerm => serviceTerm.serviceTermType != serviceTermType);
        }
    }

    private handleDeliveryChannelsChange(deliveryChannels: IMultipleSelectListItem[]) {
        if (deliveryChannels) {
            let currentIDs = deliveryChannels.map(e => e.value);
            this.model.deliveryChannels = this.deliveryChannels.filter(d => currentIDs.includes(d.deliveryChannelID.toString()));
        } else {
            this.model.deliveryChannels = [];
        }
    }

    private getModelDeliveryChannels(): IMultipleSelectListItem[] {
        if (!ObjectHelper.isArrayNullOrEmpty(this.model.deliveryChannels)) {
            let ids = this.model.deliveryChannels.map(d => d.deliveryChannelID);
            return this.deliveryChannels.filter(d => ids.includes(d.deliveryChannelID)).map(d => {
                return { label: d.name, value: d.deliveryChannelID.toString() };
            })
        }
    }

    private handleDocumentTypesChange(documentTypes: IMultipleSelectListItem[]) {
        if (documentTypes) {
            let currentIDs = documentTypes.map(e => e.value);
            this.model.attachedDocumentTypes = this.documentTypes.filter(d => currentIDs.includes(d.documentTypeID.toString()));
        } else {
            this.model.attachedDocumentTypes = [];
        }
    }

    private handleDeclarationsChange(declarations: IMultipleSelectListItem[]) {
        if (declarations) {
            let currentIDs = declarations.map(e => e.value);
            this.model.declarations = this.declarations.filter(d => currentIDs.includes(d.declarationID.toString()));
        } else {
            this.model.declarations = [];
        }
    }

    private getModelDocumentTypes(): IMultipleSelectListItem[] {
        if (!ObjectHelper.isArrayNullOrEmpty(this.model.attachedDocumentTypes)) {
            let ids = this.model.attachedDocumentTypes.map(d => d.documentTypeID);
            return this.documentTypes.filter(d => ids.includes(d.documentTypeID)).map(d => {
                return { label: d.name, value: d.documentTypeID.toString() };
            })
        }
    }

    private getModelDeclarations(): IMultipleSelectListItem[] {
        if (!ObjectHelper.isArrayNullOrEmpty(this.model.declarations)) {
            let ids = this.model.declarations.map(d => d.declarationID);
            return this.declarations.filter(d => ids.includes(d.declarationID)).map(d => {
                return { label: d.description, value: d.declarationID.toString() };
            })
        }
    }

    private onRadioChange(): void {
        this.model.isActive = !this.model.isActive
    }

    private getDocumentTypes(): Promise<void> {
        return Nomenclatures.getDocumentTypes().bind(this).then(documentTypes => {
            runInAction.bind(this)(() => {
                this.selectedDocumentTypes = [];
                this.selectedDocumentTypesAttachableDocuments = [];
                this.documentTypes = documentTypes;

                documentTypes.forEach(documentType => {
                    if (documentType.type == ConstantsEAU.E_DOCUMENT) {
                        this.selectedDocumentTypes.push({ label: documentType.name, value: documentType.documentTypeID.toString() });

                    } else if (documentType.type == ConstantsEAU.ATTACHABLE_DOCUMENT) {
                        this.selectedDocumentTypesAttachableDocuments.push({ label: documentType.name, value: documentType.documentTypeID.toString() });
                    }
                });
            })
        })
    }

    private getSelectedGroupItems(): Promise<void> {
        return Nomenclatures.getServicesGroups().bind(this).then(serviceGroups => {
            runInAction.bind(this)(() => {
                this.selectedGroupItems = [];
                serviceGroups.forEach(group => {
                    if (group.isActive) {
                        this.selectedGroupItems.push(new SelectListItem({ selected: false, text: group.name, value: group.groupID }))
                    }
                });
            })
        })
    }

    private getDeliveryChannels(): Promise<void> {
        return Nomenclatures.getDeliveryChannels().bind(this).then(deliveryChannels => {
            runInAction.bind(this)(() => {
                this.selectedDeliveryChannels = [];
                this.deliveryChannels = deliveryChannels;

                deliveryChannels.forEach(deliveryChannel => {
                    this.selectedDeliveryChannels.push({ label: deliveryChannel.name, value: deliveryChannel.deliveryChannelID.toString() });
                });
            })
        })
    }

    private getDeclarations(): Promise<void> {
        return this.nomenclatureDataService.getDeclarations().bind(this).then(declarations => {
            runInAction.bind(this)(() => {
                this.selectedDeclarations = [];
                this.declarations = declarations;

                declarations.forEach(declaration => {
                    this.selectedDeclarations.push({ label: declaration.description, value: declaration.declarationID.toString() });
                });
            })
        })
    }

    private getServiceById() {
        this.props.registerAsyncOperation(this.nomenclatureDataService.getServiceById(this.serviceId).bind(this).then(service => {
            this.model = service;
            this.defaultSunauServiceUri = this.model.sunauServiceUri;

            if (!ObjectHelper.isNullOrUndefined(this.model.additionalConfiguration))
                this.model.additionalDataAsString = JSON.stringify(this.model.additionalConfiguration);

            runInAction.bind(this)(() => {
                this.getModelDocumentTypes();
                this.getModelDeclarations();
                this.getModelDeliveryChannels();
                this.getModelServiceTerms();
            })
        }).finally(() => {
            this.isLoaded = true;
        }));
    }

    private addUpdateService() {
        this.model.initiationTypeID = +this.model.initiationTypeID;

        if (this.validators[0].validate(this.model)) {

            if (!ObjectHelper.isStringNullOrEmpty(this.model.additionalDataAsString)) {
                this.model.additionalConfiguration = JSON.parse(this.model.additionalDataAsString);
            } else {
                this.model.additionalConfiguration = undefined;
             }

            if (this.serviceId) {
                this.props.registerAsyncOperation(this.nomenclatureDataService.updateService(this.model).then(() => {
                    this.getServiceById();
                    this.notification = <div className="alert alert-success">
                        <p>{this.getResource("GL_UPDATE_OK_I")}</p>
                    </div>
                }));
            } else {
                this.props.registerAsyncOperation(this.nomenclatureDataService.addService(this.model).then(() => {
                    this.clearAll();
                    this.notification = <div className="alert alert-success">
                        <p>{this.getResource("GL_SAVE_OK_I")}</p>
                    </div>
                }))
            }
            window.scrollTo(0, 0);
        }
    }

    private handleInitiationTypeIdChange() {
         this.clearUnusedFields();
         this.model.seviceTerms = [];
         this.setDefaultServiceTerms();
    }

    private setDefaultServiceTerms() {
        if(this.isInitiationByApplication()) {
            this.selectedServiceTerms.forEach(serviceTermType => serviceTermType.selected = false);
            let currentServiceTerm = this.selectedServiceTerms.filter(serviceTerm => serviceTerm.value == AdmServiceTermType.Regular.toString())[0];

            let serviceTerm = new ServiceTerm();
            serviceTerm.serviceTermType = AdmServiceTermType.Regular;
            serviceTerm.isActive = true;
            serviceTerm.description = currentServiceTerm.text;

            this.model.seviceTerms.push(serviceTerm);
            currentServiceTerm.selected = true;
        }
    }

    private clearUnusedFields() {
        if (this.isInitiationByApplication()) {
            this.clearAllNotUsedForInitiationByApplication();
        } else if (this.isInitiationByRedirectToWebPage()) {
            this.clearAllNotUsedForInitiationByRedirectToWebPage();
        } else {
            this.clearAllNotUsedForInitiationByApplication();
            this.clearAllNotUsedForInitiationByRedirectToWebPage();
        }
        this.model.description = null;
    }

    @action
    private clearAllBeforeInitiationType() {
        this.model.initiationTypeID = null;
        this.model.name = null;
        this.model.orderNumber = null;
        this.model.sunauServiceUri = null;
        this.model.groupID = null;
        this.model.updatedOn = moment();
        this.model.isActive = false;
        this.model.description = null;
    }

    private clearAllNotUsedForInitiationByApplication() {
        this.model.serviceUrl = null;
    }

    @action
    private clearAllNotUsedForInitiationByRedirectToWebPage() {
        this.model.declarations = [];
        this.model.attachedDocumentTypes = [];
        this.model.documentTypeID = null;
        this.model.deliveryChannels = [];
        this.model.seviceTerms = [];
        this.model.resultDocumentName = null;
        this.model.admStructureUnitName = null;
        this.model.attachedDocumentsDescription = null;
        this.model.explanatoryTextRefusedOrTerminatedService = null;
        this.model.explanatoryTextFulfilledService = null;
        this.model.explanatoryTextService = null;
    }

    private clearAll() {
        this.clearAllBeforeInitiationType();
        this.clearAllNotUsedForInitiationByApplication();
        this.clearAllNotUsedForInitiationByRedirectToWebPage();
    }
}

export const AddUpdateServiceUI = withAsyncFrame(withRouter(AddUpdateServiceUIImpl));
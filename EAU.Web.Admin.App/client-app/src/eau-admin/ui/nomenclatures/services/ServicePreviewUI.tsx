import {AsyncUIProps, BaseProps, withAsyncFrame} from "cnsys-ui-react";
import {
    DocumentType,
    EAUBaseComponent,
    Nomenclatures,
    ResourceHelpers,
    Service,
    ServiceGroup,
    WaysToStartService,
    Constants as ConstantsEAU
} from "eau-core";
import React from "react";
import {observer} from "mobx-react";
import {ObjectHelper} from "cnsys-core";
import {action, observable} from "mobx";

interface ServicePreviewProps extends BaseProps, AsyncUIProps {
    service: Service;
}

@observer
class ServicePreviewUIImpl extends EAUBaseComponent<ServicePreviewProps, any> {

    @observable serviceGroup: ServiceGroup;
    @observable documentType: DocumentType;

    constructor(props: ServicePreviewProps) {
        super(props);

        this.getServiceGroup();
        this.getDocumentType();
    }

    render() {
        return <>
            <div className="field-container">
                <h3 className="field-title field-title--preview">{this.getResource('GL_DESIGNATION_L')}</h3>
                <p className="field-text">
                    {this.props.service.name}
                </p>
            </div>
            <div className="field-container">
                <h3 className="field-title field-title--preview">{this.getResource('GL_STATUS_L')}</h3>
                <p className="field-text">
                    {this.props.service.isActive ? this.getResource("GL_ACTIVE_L") : this.getResource("GL_INACTIVE_L")}
                </p>
            </div>
            <div className="field-container">
                <h3 className="field-title field-title--preview">{this.getResource('GL_SERVICE_GROUP_NAME_L')}</h3>
                <p className="field-text">
                    {this.serviceGroup.name}
                </p>
            </div>
            <div className="field-container">
                <h3 className="field-title field-title--preview">{this.getResource('GL_ORDER_NUMBER_L')}</h3>
                <p className="field-text">
                    {this.props.service.orderNumber}
                </p>
            </div>
            <div className="field-container">
                <h3 className="field-title field-title--preview">{this.getResource('GL_CREATE_UPDATE_DATE_L')}</h3>
                <p className="field-text">
                    {this.props.service.updatedOn && this.props.service.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime)}
                </p>
            </div>
            <div className="field-container">
                <h3 className="field-title field-title--preview">{this.getResource('GL_URI_ADM_SERVICE_L')}</h3>
                <p className="field-text">
                    {this.props.service.sunauServiceUri}
                </p>
            </div>
            <div className="field-container">
                <h3 className="field-title field-title--preview">{this.getResource('GL_SERVICE_INITIATION_METHOD_L')}</h3>
                <p className="field-text">
                    {this.getInitiationType()}
                </p>
            </div>
            {this.props.service.initiationTypeID == WaysToStartService.ByAplication ? <>
                <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_APPLICATION_INITIATION_SERVICE_L')}</h3>
                    <p className="field-text">
                        {this.documentType.name}
                    </p>
                </div>
                {!ObjectHelper.isArrayNullOrEmpty(this.props.service.seviceTerms) && <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_TYPE_SERVICES_L')}</h3>
                    <div className="field-text">
                        {this.props.service.seviceTerms.map((serviceTerm) =>
                            <p key={serviceTerm.serviceTermID}>{serviceTerm.description}</p>
                        )}
                    </div>
                </div>}
                <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_DELIVERY_METHOD_L')}</h3>
                    <div className="field-text">
                        {this.props.service.deliveryChannels.map((deliveryChannel) =>
                            <p key={deliveryChannel.deliveryChannelID}>{deliveryChannel.name}</p>
                        )}
                    </div>
                </div>
                {!ObjectHelper.isArrayNullOrEmpty(this.props.service.admStructureUnitName) &&
                <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_NAME_STRUCTURAL_UNIT_L')}</h3>
                    <p className="field-text">
                        {this.props.service.admStructureUnitName}
                    </p>
                </div>}
                {this.props.service.resultDocumentName && <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_Service_resultDocumentName_L')}</h3>
                    <p className="field-text">
                        {this.props.service.resultDocumentName}
                    </p>
                </div>}
                {this.props.service.description && <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_DESCRIPTION_L')}</h3>
                    <p className="field-text" dangerouslySetInnerHTML={{ __html: this.props.service.description }}/>
                </div>}
                {this.props.service.explanatoryTextService && <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_DESCRIPTION_SERVICE_L')}</h3>
                    <p className="field-text" dangerouslySetInnerHTML={{ __html: this.props.service.explanatoryTextService }}/>
                </div>}
                {this.props.service.explanatoryTextRefusedOrTerminatedService && <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_DESCRIPTION_DENIAL_SERVICE_L')}</h3>
                    <p className="field-text" dangerouslySetInnerHTML={{ __html: this.props.service.explanatoryTextRefusedOrTerminatedService }}/>
                </div>}
                {this.props.service.explanatoryTextFulfilledService && <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_DESCRIPTION_COMPLETED_SERVICE_L')}</h3>
                    <p className="field-text" dangerouslySetInnerHTML={{ __html: this.props.service.explanatoryTextFulfilledService }}/>
                </div>}
                {this.props.service.attachedDocumentsDescription && <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_DESCRIPTION_DOC_ATTACHED_L')}</h3>
                    <p className="field-text" dangerouslySetInnerHTML={{ __html: this.props.service.attachedDocumentsDescription }}/>
                </div>}
                {!ObjectHelper.isArrayNullOrEmpty(this.props.service.attachedDocumentTypes) &&
                <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_DOCUMENT_KIND_L')}</h3>
                    <div className="field-text">
                        <span className="field-text">
                            {this.props.service.attachedDocumentTypes.map((attachedDocumentType) =>
                                <p key={attachedDocumentType.documentTypeID}>{attachedDocumentType.name}</p>
                            )}
                        </span>
                    </div>
                </div>}
                {!ObjectHelper.isArrayNullOrEmpty(this.props.service.declarations) && <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_SERVICES_DECLARATIONS_L')}</h3>
                    <div className="field-text">
                        <span className="field-text">
                            {this.props.service.declarations.map((declaration) =>
                                <p key={declaration.declarationID}>{declaration.description}</p>
                            )}
                        </span>
                    </div>
                </div>}
            </> : <div className="field-container">
                <h3 className="field-title field-title--preview">{this.getResource('GL_SERVICE_ADDRESS_L')}</h3>
                <p className="field-text">
                    {this.props.service.serviceUrl}
                </p>
                {this.props.service.description && <div className="field-container">
                    <h3 className="field-title field-title--preview">{this.getResource('GL_DESCRIPTION_L')}</h3>
                    <p className="field-text" dangerouslySetInnerHTML={{ __html: this.props.service.description }}/>
                </div>}
            </div>}
        </>;
    }

    private getInitiationType() {
        let initiationTypes = ResourceHelpers.getSelectListItemsForEnum(WaysToStartService);

       let initiationType = initiationTypes.filter(i => i.value == this.props.service.initiationTypeID.toString());

       if(initiationType && initiationType.length > 0 ) {
           return initiationType[0].text;
       }
       return null;
    }

    @action
    private getServiceGroup() {
        this.serviceGroup = new ServiceGroup();

        this.props.registerAsyncOperation(Nomenclatures.getServicesGroups().bind(this).then(serviceGroups => {
             let serviceGroup = serviceGroups.filter(serviceGroup => serviceGroup.groupID == this.props.service.groupID);

             if (serviceGroup && serviceGroup.length > 0) {
                 this.serviceGroup = serviceGroup[0];
             }
        }))
    }

    @action
    private getDocumentType() {
       this.documentType = new DocumentType();

       return this.props.registerAsyncOperation(Nomenclatures.getDocumentTypes().bind(this).then(documentTypes => {
            let documentType = documentTypes.filter(documentType => documentType.documentTypeID == this.props.service.documentTypeID);

            if (documentType && documentType.length > 0) {
                this.documentType = documentType[0];
            }
        }))
    }
}

export const ServicePreviewUI = withAsyncFrame(ServicePreviewUIImpl, false);
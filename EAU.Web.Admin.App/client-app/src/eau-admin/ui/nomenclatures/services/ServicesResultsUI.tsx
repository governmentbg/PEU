import { AsyncUIProps, BaseProps, ConfirmationModal, withAsyncFrame } from "cnsys-ui-react";
import { Constants as ConstantsEAU, EAUBaseComponent, Nomenclatures, Service, ServiceGroup } from "eau-core";
import { observable } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { Link } from "react-router-dom";
import { Button, Modal, ModalBody, ModalFooter, ModalHeader } from "reactstrap";
import { NomenclaturesDataService } from "../../../services/NomenclaturesDataService";
import { ServicePreviewUI } from "./ServicePreviewUI";

interface ServicesResultsProps extends BaseProps, AsyncUIProps {
    services: Service[];
    isLoaded: boolean;
}

@observer
class ServicesResultsUIImpl extends EAUBaseComponent<ServicesResultsProps, any> {

    @observable private modal: boolean;
    @observable private serviceGroups: ServiceGroup[];
    @observable private previewService: Service;

    private nomenclatureDataService: NomenclaturesDataService;

    constructor(props: ServicesResultsProps) {
        super(props);

        this.toggle = this.toggle.bind(this);

        this.nomenclatureDataService = new NomenclaturesDataService();
        this.previewService = new Service();

        this.modal = false;

        this.props.registerAsyncOperation(Nomenclatures.getServicesGroups().then(serviceGroups => {
            this.serviceGroups = serviceGroups;
        }));
    }

    render() {
        return <>
            {this.props.services && this.props.services.length > 0 ?
                <table className="table table-bordered table-striped table-hover">
                    <thead>
                        <tr>
                            <th>{this.getResource("GL_DESIGNATION_L")}</th>
                            <th>{this.getResource("GL_STATUS_L")}</th>
                            <th>{this.getResource("GL_URI_ADM_SERVICE_L")}</th>
                            <th>{this.getResource("GL_SERVICE_GROUP_NAME_L")}</th>
                            <th>{this.getResource("GL_CREATE_UPDATE_DATE_L")}</th>
                            <th>{this.getResource("GL_ACTIONS_L")}</th>
                        </tr>
                    </thead>
                    <tbody id="content">
                        {this.props.services.map((service) => <tr key={service.serviceID}>
                            <td>{service.name}</td>
                            <td className="icons-td">
                                <i className={`ui-icon ui-icon-state-${service.isActive ? "active" : "inactive"}`} aria-hidden="true"></i>
                                {service.isActive ? this.getResource("GL_ACTIVE_L") : this.getResource("GL_INACTIVE_L")}
                            </td>
                            <td>{service.sunauServiceUri}</td>
                            <td>{this.serviceGroups && this.getServiceGroupName(service.groupID)}</td>
                            <td>
                                {service.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime)}
                            </td>
                            <td className="buttons-td">
                                <button className="btn btn-secondary" title={this.getResource("GL_VIEW_L")} onClick={() => { this.toggle(); this.setPreviewService(service); }}>
                                    <i className="ui-icon ui-icon-eye"></i></button>

                                <Link className="btn btn-secondary" title={this.getResource("GL_EDIT_L")} to={`/nomenclatures/services/${service.serviceID}`}>
                                    <i className="ui-icon ui-icon-edit"></i>
                                </Link>

                                <ConfirmationModal
                                    modalTitleKey={service.isActive ? "GL_DEACTIVATION_L" : "GL_ACTIVATION_L"}
                                    modalTextKeys={[service.isActive ? "GL_DEACTIVE_CONFIRM_I" : "GL_ACTIVE_CONFIRM_I"]}
                                    noTextKey="GL_REFUSE_L" yesTextKey="GL_CONFIRM_L"
                                    onSuccess={() => this.changeServiceActivity(service)}>

                                    <button className="btn btn-secondary" title={service.isActive ? this.getResource("GL_DEACTIVATION_L") : this.getResource("GL_ACTIVATION_L")}>
                                        <i className={`ui-icon ui-icon-${service.isActive ? "deactivate" : "activate"}`}></i></button>
                                </ConfirmationModal>
                            </td>
                        </tr>)}
                        <Modal isOpen={this.modal} toggle={this.toggle}>
                            <ModalHeader toggle={this.toggle}>{this.getResource("GL_APPLICATION_L")}</ModalHeader>
                            <ModalBody>
                                <ServicePreviewUI service={this.previewService} />
                            </ModalBody>
                            <ModalFooter>
                                <Button color="secondary" onClick={this.toggle}>{this.getResource("GL_CLOSE_L")}</Button>
                            </ModalFooter>
                        </Modal>
                    </tbody>
                </table>
                : this.props.isLoaded && <div className="alert alert-dismissible alert-warning fade show">
                    <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                </div>}
        </>;
    }

    toggle() {
        this.modal = !this.modal;
    }

    setPreviewService(service: Service) {
        this.previewService = service;
    }

    getServiceGroupName(id: number): string {
        let currentServiceGroup = this.serviceGroups.filter(serviceGroup => serviceGroup.groupID == id)[0];

        if (currentServiceGroup)
            return currentServiceGroup.name;

        return null;
    }

    private changeServiceActivity(service: Service) {
        if (service.isActive) {
            this.props.registerAsyncOperation(this.nomenclatureDataService.deactivateService(service.serviceID).then(() => {
                service.isActive = !service.isActive;
            }));
        } else {
            this.props.registerAsyncOperation(this.nomenclatureDataService.activateService(service.serviceID).then(() => {
                service.isActive = !service.isActive;
            }));
        }
    }
}

export const ServicesResultsUI = withAsyncFrame(ServicesResultsUIImpl,false);
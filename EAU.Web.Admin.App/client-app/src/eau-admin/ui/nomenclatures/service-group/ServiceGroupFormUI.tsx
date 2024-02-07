import { ObjectHelper } from "cnsys-core";
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame } from 'cnsys-ui-react';
import { attributeClassRequiredLabel, Constants as ConstantsEAU, EAUBaseComponent, ServiceGroup, ServiceGroupSearchCriteria, ValidationSummaryErrors } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Constants } from '../../../Constants';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';
import { ServiceGroupValidator } from '../../../validations/ServiceGroupValidator';
import BtnGroupFormUI from '../../common/BtnGroupFormUI';


interface GroupListRouteProps extends BaseRouteParams {
    serviceGroupID?: number
}

interface GroupListProps extends BaseRouteProps<GroupListRouteProps>, AsyncUIProps, BaseRoutePropsExt {
}

@observer class ServiceGroupForm extends EAUBaseComponent<GroupListProps, ServiceGroup> {

    private serviceGroupId: number;

    @observable isFormSubmited: boolean;
    @observable isLoaded: boolean = true;

    private nomenclatureDataService: NomenclaturesDataService;

    constructor(props?: GroupListProps) {
        super(props);

        this.serviceGroupId = this.props.match.params.serviceGroupID;
        this.nomenclatureDataService = new NomenclaturesDataService();
        this.init();
    }

    @action private init() {
        this.model = new ServiceGroup();
        this.onSave = this.onSave.bind(this);

        if (this.serviceGroupId) {

            this.isLoaded = false;

            let searchCriteria = new ServiceGroupSearchCriteria();
            searchCriteria.Ids = [this.serviceGroupId];

            this.props.registerAsyncOperation(
                this.nomenclatureDataService.getServiceGroupById(this.serviceGroupId)
                    .then((result) => {
                        if (!ObjectHelper.isNullOrUndefined(result))
                            this.model = result
                    })
                    .finally(() => { this.isLoaded = true })
            )
        }
        this.validators = [new ServiceGroupValidator()];
    }

    private onSave() {
        if (this.validators[0].validate(this.model)) {

            if (this.serviceGroupId)
                this.props.registerAsyncOperation(this.nomenclatureDataService.updateServiceGroup(this.model)
                    .then(() => {
                        this.isFormSubmited = true;
                    }))

            else {
                this.model.isActive = false;
                this.props.registerAsyncOperation(this.nomenclatureDataService.addServiceGroup(this.model)
                    .then(() => {
                        runInAction.bind(this)(() => {
                            this.isFormSubmited = true;
                            this.model.name = this.model.iconName = this.model.orderNumber = null;
                        })
                    }))
            }
        }
    }

    render() {

        let dataNotFound = false;

        if (this.serviceGroupId && this.model.groupID == null) {
            dataNotFound = true;
        }

        let dataResult = null;

        if (this.isLoaded) {

            if (!dataNotFound) {
                dataResult = <div className="card">
                    <form id="groupListForm">
                        <div className="card-body">
                            <ValidationSummaryErrors {...this.props} />

                            {!ObjectHelper.isNullOrUndefined(this.isFormSubmited) && <div className="alert alert-success"><p>{this.serviceGroupId ? this.getResource("GL_UPDATE_OK_I") : this.getResource("GL_SAVE_OK_I")}</p></div>}

                            <div className="row">
                                <div className="form-group col-12 col-md-6">
                                    {this.labelFor(x => x.name, "GL_DESIGNATION_L", attributeClassRequiredLabel)}
                                    {this.textBoxFor(x => x.name)}
                                </div>

                                <div className="form-group col-md-3">
                                    <label htmlFor="exampleInputPassword1">{this.getResource("GL_STATUS_L")}</label>
                                    <div className="form-inline">
                                        <div className="custom-control-inline custom-control custom-radio">
                                            <input disabled={true} className="custom-control-input" type="radio" name="exampleRadiosA" id="exampleRadiosA1" value="option2" checked={this.model.isActive} />
                                            <label className="custom-control-label" htmlFor="exampleRadiosA1">{this.getResource("GL_ACTIVE_L")}</label>
                                        </div>
                                        <div className="custom-control-inline custom-control custom-radio">
                                            <input disabled={true} className="custom-control-input" type="radio" name="exampleRadiosA" id="exampleRadiosA1" value="option1" checked={!this.model.isActive} />
                                            <label className="custom-control-label" htmlFor="exampleRadiosA1">{this.getResource("GL_INACTIVE_L")}</label>
                                        </div>
                                    </div>
                                </div>

                                <div className={`form-group col-md-3 ${this.serviceGroupId ? '' : 'd-none'}`}>
                                    <label htmlFor="CREATE_UPDATE_DATE">{this.getResource("GL_CREATE_UPDATE_DATE_L")}</label>
                                    <input id="input_updatedOn" className="form-control" disabled={true} value={this.model.updatedOn ? this.model.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime) : ''} />
                                </div>
                            </div>

                            <div className="row">
                                <div className="form-group  col-md-4">
                                    {this.labelFor(x => x.iconName, "GL_IMAGE_L", attributeClassRequiredLabel)}
                                    {this.textBoxFor(x => x.iconName)}
                                </div>
                                <div className="form-group col-md-2">
                                    {this.labelFor(x => x.orderNumber, "GL_ORDER_NUMBER_L", attributeClassRequiredLabel)}
                                    {this.textBoxFor(x => x.orderNumber)}
                                </div>
                            </div>
                        </div>
                        <BtnGroupFormUI refuseLink={Constants.PATHS.NomServiceGroups} onSave={this.onSave} />
                    </form>
                </div>
            }
            else {
                dataResult = <div className="card">
                    <div className="card-body">
                        <div className="alert alert-dismissible alert-warning fade show">
                            <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                        </div>
                    </div>
                </div>
            }
        }

        return <>
            {dataResult}
        </>
    }
}

export const ServiceGroupFormUI = withAsyncFrame(ServiceGroupForm, false); 

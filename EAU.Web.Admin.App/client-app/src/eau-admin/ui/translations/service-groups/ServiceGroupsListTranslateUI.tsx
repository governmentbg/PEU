import { AsyncUIProps, BaseProps } from 'cnsys-ui-react';
import { EAUBaseComponent } from 'eau-core';
import { observer } from 'mobx-react';
import React from 'react';
import { action, observable } from 'mobx';
import { ServiceGroupI18nVM } from '../../../models/ServiceGroupI18nVM';
import { ServiceGroupI18nValidator } from '../../..//validations/ServiceGroupI18nValidator';


interface ServiceGroupsProps extends  BaseProps, AsyncUIProps {
    onSave: (serviceGroupI18n: ServiceGroupI18nVM) => any;
    switchEditMode: (editMode: boolean) => any;
}

@observer 
class ServiceGroupsListTranslateUI extends EAUBaseComponent<ServiceGroupsProps, ServiceGroupI18nVM>{

    @observable editMode: boolean;
    private initialLimit: ServiceGroupI18nVM;

    constructor(props: ServiceGroupsProps) {
        super(props);
        this.editMode = false;
        this.validators = [new ServiceGroupI18nValidator()];
        this.initialLimit = JSON.parse(JSON.stringify(this.model));
    }

    render() {

        return <>
                <tr key={`${this.model.groupID}`}>
                    <td>{this.model.bgName}</td>
                    
                    <td id={"editTranslationName-" + this.model.groupID}>
                        
                        <div className={this.editMode ? 'd-none' : ''}>
                            <button 
                                className={`btn btn-secondary float-right`}
                                onClick={() => {this.switchEditMode()}}
                                title={this.getResource("GL_EDIT_L")}><i className="ui-icon ui-icon-edit"></i>
                            </button>
                            <span className="label-value">{this.model.name}</span>
                        </div>

                        <div className={!this.editMode ? 'd-none' : ''}>
                            <div className="form-group" >
                                    {this.textAreaFor(x => x.name)}
                            </div>

                            <div className="btn-group float-right">
                                <button className="btn btn-secondary"
                                        title={this.getResource("GL_SAVE_L")}
                                        onClick={() => {this.props.onSave(this.model); this.switchEditMode()}}
                                >
                                    <i className="ui-icon ui-icon-check"></i>
                                </button>
                                <button className="btn btn-secondary"
                                        title={this.getResource("GL_REFUSE_L")}
                                        onClick={() => {this.onCancelChanges()}}
                                >
                                    <i className="ui-icon ui-icon-ban"></i>
                                </button>    
                            </div>
                        </div>
                    </td> 
                </tr>
                      
        </>
    }

    private switchEditMode() {
        this.editMode = this.props.switchEditMode(this.editMode )
    }

    @action private onCancelChanges() {
        this.model.copyFrom(this.initialLimit);
        this.editMode = !this.editMode;
    }


}

export default ServiceGroupsListTranslateUI; 
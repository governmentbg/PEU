import { AsyncUIProps, withAsyncFrame, BaseRoutePropsExt, BaseProps } from 'cnsys-ui-react';
import { EAUBaseComponent, Service} from 'eau-core';
import React from 'react';
import { Link } from 'react-router-dom';
import {Constants} from '../../../Constants';
import { ServiceI18nVM } from '../../../models/ServiceI18nVM';

interface ServiceProps extends BaseProps, AsyncUIProps, BaseRoutePropsExt {
    services: ServiceI18nVM[],
    langName:string
}

class ServicesListI18nUIImpl extends EAUBaseComponent<ServiceProps, any>{

    constructor(props) {
        super(props);
    }

    render() {
        return <>
            <div className="table-responsive">
                <table className="table table-bordered table-striped table-hover">
                    <thead>
                        <tr>
                            <th>{this.getResource("GL_SERVICE_NAME_L")}</th>
                            <th>{this.getResource("GL_SERVICE_NAME_L")} <span className="label-description">({this.props.langName})</span></th>
                            <th>{this.getResource("GL_ACTIONS_L")}</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.props.services.map((service, index) => 
                            <tr key={service.serviceID + "_" + index}>
                                <td>{service.bgName}</td>
                                <td>{service.name}</td>
                                <td className="buttons-td">
                                    <Link to={Constants.PATHS.TranslationsEditService.replace(':serviceID', service.serviceID.toString())}>
                                        <button className="btn btn-secondary" title={this.getResource("GL_EDIT_L")}>
                                            <i className="ui-icon ui-icon-edit"></i>
                                        </button>
                                    </Link>
                                </td>
                            </tr>
                        )}
                    </tbody>
                </table>
            </div>
        </>
    }
}

export const ServicesListI18nUI = withAsyncFrame(ServicesListI18nUIImpl); 
import React, { useEffect, useState } from 'react';
import { ObjectHelper } from 'cnsys-core';
import { Nomenclatures, ServiceGroup } from 'eau-core';
import { ServicesByGroupUI } from './ServicesByGroupUI';
import { moduleContext } from '../../ModuleContext';

interface ServicesUIState {
    servicesGroups: ServiceGroup[];
    error: string
}

export const ServicesUI = () => {
    const [servicesState, setServicesState] = useState<ServicesUIState>({ servicesGroups: undefined, error: undefined })

    useEffect(() => {
        Nomenclatures.getServicesGroups().then(res => {
            if (res && res.length > 0) {
                res = res.filter(el => el.isActive == true);

                setServicesState({
                    servicesGroups: res.sort((el1, el2) => {
                        if (el1.orderNumber < el2.orderNumber) return -1;
                        else if (el1.orderNumber > el2.orderNumber) return 1;
                        else return 0;
                    }),
                    error: undefined
                });
            }
        }).catch(e => {
            setServicesState({ servicesGroups: undefined, error: moduleContext.resourceManager.getResourceByKey('GL_ERROR_L') });
        })
    }, [])

    if (servicesState.servicesGroups && servicesState.servicesGroups.length > 0) {
        return (
            <div className="page-wrapper" id="ARTICLE-CONTENT">
                <ul className="nav-services">
                    {servicesState.servicesGroups.map(serviceGroup => <li key={serviceGroup.groupID}>
                        <ServicesByGroupUI serviceGroup={serviceGroup} />
                    </li>)}
                </ul>
            </div>);
    } else {
        if (ObjectHelper.isStringNullOrEmpty(servicesState.error)) {
            return (<div className="loader-overlay load"></div>);
        } else {
            return (<div className="alert alert-danger" role="alert"><p>{servicesState.error}</p></div>);
        }
    }
}
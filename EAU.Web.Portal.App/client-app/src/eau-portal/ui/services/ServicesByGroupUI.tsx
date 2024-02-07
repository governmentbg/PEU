import defaultImg from 'assets/images/services/icon-service.svg';
import { Button, Constants, Nomenclatures, Service, ServiceGroup, WaysToStartService, resourceManager } from 'eau-core';
import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { Collapse } from 'reactstrap';

interface ServicesByGroupProps {
    serviceGroup: ServiceGroup;
}

export const ServicesByGroupUI: React.FC<ServicesByGroupProps> = (props) => {

    const [services, setServices] = useState<Service[]>();
    const [isOpen, setIsOpen] = useState<boolean>();
    const [imgSrc, setImgSrc] = useState<any>();
    const [showClearBtn, setShowClearBtn] = useState<boolean>(false);
    const [servicesByGroup, setServicesByGroup] = useState<Service[]>();
    const [searchFieldValue, setSearchFieldValue] = useState('');

    useEffect(() => {
        import(/* webpackMode: "eager" */ `assets/images/services/${props.serviceGroup.iconName}`)
            .then(imgSrc => imgSrc && setImgSrc(imgSrc.default))
            .catch(() => setImgSrc(defaultImg));
    }, []);

    return (
        <>
            <Button className={isOpen ? "service service-toggle" : "service service-toggle collapsed"} aria-expanded={isOpen ? isOpen : false} type="button" data-toggle="collapse" onClick={toggle}>
                {imgSrc ? <img className="service-image" src={imgSrc} alt="" /> : null}
                <h2 className="service-title">{props.serviceGroup.name}</h2>
            </Button>
            <Collapse isOpen={isOpen} >

            <div className="service-subnav">
                <div className="service-subnav-search">
                    <div className="input-group form-group input-group-sm">
                        <div className="input-group-prepend">
                            <div className="input-group-text">
                                <i className="ui-icon ui-icon-search-color" aria-hidden="true"></i>
                            </div>
                        </div>

                        <input 
                            type="text" 
                            className="form-control form-control-sm" 
                            title={resourceManager.getResourceByKey("GL_SERVICE_SEARCH_CRITERIA_L")}
                            onChange={serviceByGroupSearch} 
                            placeholder={resourceManager.getResourceByKey("GL_SEARCH_TITLE_L")} 
                            value={searchFieldValue} />

                        {showClearBtn && <div className="input-group-append">
                            <Button type="button" className="btn btn-light btn-sm clear-button" title={resourceManager.getResourceByKey("GL_CLEAR_L")} onClick={clearServicesSearchField}>
                                <i className="ui-icon ui-icon-clear"></i>
                            </Button>
                        </div>}

                    </div>
                </div>

                <ul>

                    {services && services.length > 0 && services.map((item: Service) => {
                        if (item.initiationTypeID == WaysToStartService.ByAplication) {
                            return (
                                <li key={item.serviceID}>
                                    <Link to={Constants.PATHS.APPLICATION_PROCESSES_START.replace(':serviceID', item.serviceID.toString())} >
                                        {item.name}
                                    </Link>
                                </li>
                            );
                        }
                        else if (item.initiationTypeID == WaysToStartService.ByRedirectToWebPage) {
                            if (item.serviceUrl.indexOf("http") == 0) {
                                return (
                                    <li key={item.serviceID}>
                                        <a href={item.serviceUrl}>{item.name}</a>
                                    </li>
                                );
                            }
                            else {
                                return (
                                    <li key={item.serviceID}>
                                        <Link to={item.serviceUrl} >
                                            {item.name}
                                        </Link>
                                    </li>
                                );
                            }
                        }
                        else {
                            return null;
                        }
                    })}

                </ul>
            </div>
            </Collapse>
        </>);

    function toggle() {

        if (!services) {
            Nomenclatures.getServices(el => el.groupID == props.serviceGroup.groupID).then(res => {

                if (res && res.length > 0) {
                    setServices(res.filter(m => m.isActive == true));
                    setServicesByGroup(res.filter(m => m.isActive == true));
                }

                setIsOpen(!isOpen);
            });
        } else
            setIsOpen(!isOpen);
    }

    function serviceByGroupSearch(e: any): void {

        let text: string = e.target.value;
        setSearchFieldValue(text);

        if (text.length >= 1) {
            setShowClearBtn(true);
        }
        else {
            setShowClearBtn(false);
        }

        if (servicesByGroup && servicesByGroup.length > 0) {

            if (services.length != servicesByGroup.length)
                setServices(servicesByGroup);
            
            if (text.length >= 1) {
                    
                let filteredServices = servicesByGroup.filter(m => m.name.toLowerCase().includes(text.toLowerCase()));
                setServices(filteredServices);
            }
        }
    }

    function clearServicesSearchField() {
        setSearchFieldValue('');
        setServices(servicesByGroup);
    }
}
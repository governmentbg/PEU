import { resourceManager } from 'eau-core';
import React from 'react';

export const HomeUI = () => {

    return (
        <div id="container-fluid">
            <div id="page-title">
                <h2>{resourceManager.getResourceByKey("GL_ADMIN_TITLE_L")}</h2>
            </div>
            <div className="card">
                <div className="card-body">
                    <div className="plain-text mt-2">
                        <b>{resourceManager.getResourceByKey("GL_ADMIN_SUB_TITLE_L")}</b>
                        <div>
                            <ul>
                                <li>{resourceManager.getResourceByKey("GL_ADMINISTER_USER_PROFILES_L")}</li>
                                <li>{resourceManager.getResourceByKey("GL_PREVIEW_ODIT_INFO_L")}</li>
                                <li>{resourceManager.getResourceByKey("GL_CONFIGURE_DECLARATION_TEMPLATES_L")}</li>
                                <li>{resourceManager.getResourceByKey("GL_MANAGE_PAGES_CONTENT_PORTAL_L")}</li>
                                <li>{resourceManager.getResourceByKey("GL_NOMENCLATURES_ADMINISTRATION_L")}</li>
                                <li>{resourceManager.getResourceByKey("GL_MANAGE_MULTILANGUAGE_INTERFACE_L")}</li>
                                <li>{resourceManager.getResourceByKey("GL_ADMINISTRATE_PARAMETERS_AND_LIMITS_L")}</li>
                                <li>{resourceManager.getResourceByKey("GL_ADMINISTRATE_REGISTRATION_DATA_MVR_L")}</li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};
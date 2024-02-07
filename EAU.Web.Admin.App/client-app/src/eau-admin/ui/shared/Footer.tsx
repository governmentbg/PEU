import React from 'react';
import logoEU from 'assets/images/footer-logo-eu.svg'
import logoOPDU from 'assets/images/footer-logo-opdu.svg'
import { resourceManager, appConfig } from 'eau-core';

const Footer = function () {
    return <div className="footer-wrapper">
        <footer className="footer">
            <div className="software-version">{resourceManager.getResourceByKey("GL_VERSION_L")}: {appConfig.version}</div>
            <div className="brand-line"></div>
            <div className="container-fluid footer-containter">
                <div className="row">
                    <div className="col-md-8 col-xl-10 order-md-2 text-center">
                        <p>{resourceManager.getResourceByKey("GL_OPERATION_PROGRAM_GOOD_MANAGEMENT_EU_L")}</p>
                    </div>
                    <div className="col-6 col-md-2 col-xl-1 order-md-1 text-center">
                        <img src={logoEU} alt="logo-eu" />
                    </div>
                    <div className="col-6 col-md-2 col-xl-1 order-md-3 text-center">
                        <img src={logoOPDU} alt="logo-opdu" />
                    </div>
                </div>
            </div>
        </footer>
    </div>
}

export default Footer;
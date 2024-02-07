import { AsyncUIProps, withAsyncFrame } from 'cnsys-ui-react';
import { resourceManager } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import logo from '../../../assets/images/logo.png';

interface HeaderProps extends AsyncUIProps {
}

@observer class HeaderImpl extends React.Component<HeaderProps, any> {
    @observable private isMobileMenuOpen: boolean;

    constructor(props: HeaderProps) {
        super(props);

        //Bind
        this.toggle = this.toggle.bind(this);

        //Init
        this.isMobileMenuOpen = false;
    }

    render() {
        return (
            <div className="header-wrapper">
                <header className="header header--popup">
                    <div className="header-bg">
                        <div className="section-wrapper fixed-content-width header-gerb-bg">
                            <div className="form-row header-container ">
                                <div className="col-auto">
                                    <img className="mvr-logo" src={logo} alt="" />
                                </div>
                                <div className="col header-title">
                                    <p>{resourceManager.getResourceByKey("GL_MVR_L")}</p>
                                    <p className='site-name'>{resourceManager.getResourceByKey("GL_PEAU_L")}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </header>
            </div>);
    }

    @action toggle(e: any): void {
        this.isMobileMenuOpen = !this.isMobileMenuOpen;
    }

    @action blureMobileMenu(e: any): void {
        if (this.isMobileMenuOpen) {
            this.isMobileMenuOpen = false;
        }
    }
}

export const Header = withAsyncFrame(HeaderImpl);
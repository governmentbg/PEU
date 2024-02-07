import { ObjectHelper } from "cnsys-core";
import { observable } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { Modal, ModalBody, ModalFooter, ModalHeader } from 'reactstrap';
import { BaseComponent, BaseProps } from "./BaseComponent";
import { BaseRoutePropsExt, withRouter } from "./WithRouter";

export class ModalTrigger extends BaseComponent<BaseProps, any> {
    constructor(props?: BaseProps, context?: any) {
        super(props, context);
    }
}

export class ModalContent extends BaseComponent<BaseProps, any> {
    constructor(props?: BaseProps, context?: any) {
        super(props, context);
    }
}

export interface InformationModalProps extends BaseProps, BaseRoutePropsExt {
    /** Заглавие на popup-а */
    title?: string;
    modalUniqueName?: string;
}

@observer class InformationModalImpl extends BaseComponent<InformationModalProps, any> {
    @observable showModal: boolean;
    uid?: string;

    constructor(props?: InformationModalProps, context?: any) {
        super(props, context);

        this.showModal = false;
        this.render = this.render.bind(this);
        this.open = this.open.bind(this);
        this.close = this.close.bind(this);

        if (props.modalUniqueName)
            this.uid = props.modalUniqueName;
        else
            this.uid = ObjectHelper.newGuid();

        this.ensureOpen = this.ensureOpen.bind(this);
        this.ensureClose = this.ensureClose.bind(this);
        this.modalToggleEvent = this.modalToggleEvent.bind(this);
    }

    render() {
        return (
            <div >
                <span onClick={this.open}>
                    {React.Children.map(this.props.children, (child: any, idx: number) => {
                        return child.type.prototype instanceof ModalTrigger ? child.props.children : null;
                    })}
                </span>
                <Modal size="lg" backdrop='static' autoFocus={true} isOpen={this.showModal} onExit={this.close}>
                    <ModalHeader>
                        {this.props.title}
                    </ModalHeader>
                    <ModalBody>
                        {React.Children.map(this.props.children, (child: any, idx: number) => {
                            return child.type.prototype instanceof ModalContent ? child.props.children : null;
                        })}
                    </ModalBody>
                    <ModalFooter>
                        <button onClick={this.close}>{"TODO: resources"}</button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }

    ensureOpen() {
        if (this.showModal == false) {
            this.open();
        }
    }

    ensureClose() {
        if (this.showModal == true) {
            this.close();
        }
    }

    open() {
        this.showModal = true;
        this.ensureAddModalUidInUrl(this.uid);
    }

    close() {
        this.showModal = false;
        this.ensureRemoveModalUidInUrl(this.uid)
    }

    isModalUidInUrl(modalUrl: string): boolean {
        let res: boolean = false;
        let urlParams = this.props.routerExt.getParams();

        for (let prop in urlParams) {
            if (prop === modalUrl) {
                res = true;
                break;
            }
        }

        return res;
    }

    ensureAddModalUidInUrl(modalUrl: string) {
        if (this.isModalUidInUrl(modalUrl) == false) {

            let params: any = this.props.routerExt.getParams();
            params[modalUrl] = "open";
            this.props.routerExt.changeParams(params);
        }
    }

    ensureRemoveModalUidInUrl(modalUrl: string) {
        if (this.isModalUidInUrl(modalUrl) == true) {
            
            let params: any = this.props.routerExt.getParams();
            params[modalUrl] = undefined

            this.props.routerExt.changeParams(params);
        }
    }

    detachModalToggleEvents() {
        window.removeEventListener(this.props.routerExt.getOnBackEventName(), this.modalToggleEvent, false);

    }

    attachModalToggleEvents() {
        window.addEventListener(this.props.routerExt.getOnBackEventName(), this.modalToggleEvent, false);
    }

    modalToggleEvent() {
        if (this.isModalUidInUrl(this.uid))
            this.ensureOpen();
        else
            this.ensureClose();
    }

    componentDidMount() {
        this.attachModalToggleEvents();
    }

    componentWillUnmount() {
        this.detachModalToggleEvents();
    }
}

export const InformationModal = withRouter(InformationModalImpl);
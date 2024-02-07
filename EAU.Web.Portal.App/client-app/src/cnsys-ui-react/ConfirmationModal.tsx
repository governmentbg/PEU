import { moduleContext } from "cnsys-core";
import { resourceManager } from "eau-core";
import React from "react";
import Modal from "reactstrap/lib/Modal";
import ModalBody from "reactstrap/lib/ModalBody";
import ModalFooter from "reactstrap/lib/ModalFooter";
import ModalHeader from "reactstrap/lib/ModalHeader";

interface ConfirmationModalProps {
    modalTitleKey: string;
    modalTextKeys: Array<string>;
    onSuccess: () => void;
    yesTextKey: string;
    noTextKey: string;
    children: React.ReactNode;
}

export const ConfirmationModal = (props: ConfirmationModalProps) => {
    const [isOpen, setIsOpen] = React.useState(false);
    const childrenWithProps = React.Children.map(props.children, (child: any) =>
        React.cloneElement(child, { onClick: () => setIsOpen(true) })
    );

    const onSuccess = () => {
        setIsOpen(false);
        props.onSuccess();
    }

    return (
        <>
            {childrenWithProps}
            <Modal centered={true} backdrop='static' autoFocus={true} isOpen={isOpen} onExit={() => setIsOpen(false)}>
                <ModalHeader toggle={() => setIsOpen(false)} closeAriaLabel={resourceManager.getResourceByKey("GL_CLOSE_L")}>
                    {moduleContext.resourceManager.getResourceByKey(props.modalTitleKey)}
                </ModalHeader>
                <ModalBody>
                    <div className="alert alert-warning" role="alert">
                        {props.modalTextKeys && props.modalTextKeys.length > 0 ?
                            (<>
                                {
                                    props.modalTextKeys.map((textItemKey: string, idx: number) => {
                                        if (idx == 0)
                                            return <React.Fragment key={idx} ><b>{moduleContext.resourceManager.getResourceByKey(textItemKey)}</b></React.Fragment>
                                        else
                                            return <React.Fragment key={idx}> <br /> {moduleContext.resourceManager.getResourceByKey(textItemKey)}</React.Fragment>
                                    })
                                }
                            </>) : null}
                    </div>
                </ModalBody>
                <ModalFooter>
                    <div className="button-bar">
                        <div className="right-side">
                            <button id="BTN_MODAL_OK" type="button" className="btn btn-primary" onClick={onSuccess} data-dismiss="modal">{moduleContext.resourceManager.getResourceByKey(props.yesTextKey)}</button>
                        </div>
                        <div className="left-side">
                            <button type="button" className="btn btn-secondary" onClick={() => setIsOpen(false)} data-dismiss="modal">{moduleContext.resourceManager.getResourceByKey(props.noTextKey)}</button>
                        </div>
                    </div>
                </ModalFooter>
            </Modal>
        </>);
}
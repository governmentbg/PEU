import * as React from "react";
import {
    Button,
    DropdownItem,
    DropdownMenu,
    DropdownToggle, Modal,
    ModalBody,
    ModalFooter, ModalHeader,
    UncontrolledDropdown
} from "reactstrap";
import { resourceManager } from "eau-core";
import { Link } from "react-router-dom";
import { Constants } from "../../Constants";
import { observer } from "mobx-react";
import { observable } from "mobx";
import { UIHelper } from "cnsys-core";

interface AccessibilityOptionsProps {

}

@observer
export class AccessibilityOptionsUI extends React.Component<AccessibilityOptionsProps, any> {

    @observable private modal: boolean;

    constructor(props: AccessibilityOptionsProps) {
        super(props);

        this.modal = false;
        this.onHideCSS = this.onHideCSS.bind(this);
        this.toggle = this.toggle.bind(this);
    }

    render() {
        return <><UncontrolledDropdown a11y={true}>
            <DropdownToggle id="dropdownMenuAccessibility" aria-controls="dropdownMenuAccessibilityMenu" type="button" caret className="navbar-top-item" data-boundary="window" tag="button">
                <span title={resourceManager.getResourceByKey("GL_ACCESSIBILITY_L")}>
                    <i className="ui-icon nav-icon-accessibility" aria-hidden="true"></i>
                    <span className="sr-only">{resourceManager.getResourceByKey("GL_ACCESSIBILITY_L")}</span>
                </span>
            </DropdownToggle>
            <DropdownMenu id="dropdownMenuAccessibilityMenu" aria-labelledby="dropdownMenuAccessibility" positionFixed={true}>
                <DropdownItem key={0} tag={Link}
                    to={Constants.PATHS.AccessFunction}>{resourceManager.getResourceByKey(Constants.RESOURCES.AccessFunction)}</DropdownItem>

                <DropdownItem key={1}
                    onClick={this.toggle}>{resourceManager.getResourceByKey("GL_TEXT_VERSION_L")}</DropdownItem>
            </DropdownMenu>
        </UncontrolledDropdown>

            <Modal isOpen={this.modal} toggle={this.toggle}>
                <ModalHeader
                    toggle={this.toggle}>{resourceManager.getResourceByKey("GL_TEXT_VERSION_L")}</ModalHeader>
                <ModalBody>
                    {"Визуалните елементи на страницата не се показват при преминаване към изглед само за текст. За да излезете от текстовия изглед, можете да използвате бутона „Към нормален изглед“ в горната част на страницата, да обновите страницата или да затворите и отворите браузъра си."}
                </ModalBody>
                <ModalFooter>
                    <div className="button-bar">
                        <div className="right-side">
                            <Button color="primary"
                                onClick={this.onHideCSS}>{resourceManager.getResourceByKey("GL_CONFIRM_L")}</Button>
                        </div>
                        <div className="left-side">
                            <Button color="secondary"
                                onClick={this.toggle}>{resourceManager.getResourceByKey("GL_REFUSE_L")}</Button>
                        </div>
                    </div>
                </ModalFooter>
            </Modal>

            
        </>;
    }

    toggle() {
        this.modal = !this.modal;
    }

    onHideCSS() {
        this.toggle();
        UIHelper.HideSiteCss();
    }
}
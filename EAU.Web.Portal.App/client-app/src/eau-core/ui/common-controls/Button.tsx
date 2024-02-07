import React from "react";
import { moduleContext } from '../../ModuleContext';

export interface ButtonProps extends React.ButtonHTMLAttributes<HTMLButtonElement> {
    lableTextKey?: string
    titlekey?: string
}

export class Button extends React.Component<ButtonProps, any> {

    render() {
        //Взивамем всички пропс освен labelTextKey и се подават на button. Това се прави така, защото дава warning, когато подаваме на HTMLButtonElement custum проп, за който не знае.
        var { lableTextKey, title, ...other } = this.props;
        var title = this.props.titlekey ? moduleContext.resourceManager.getResourceByKey(this.props.titlekey) : this.props.title;

        if (this.props.lableTextKey) {
            return <button {...other} title={title}>{this.props.children}{moduleContext.resourceManager.getResourceByKey(this.props.lableTextKey)}</button>
        }
        else {
            return <button {...other} title={title}>{this.props.children}</button>
        }
    }
}
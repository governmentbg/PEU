import { observable } from 'mobx';
import { observer } from "mobx-react";
import * as React from "react";
import { BaseFieldComponent, BaseFieldProps } from '../BaseFieldComponent';

interface HintProps extends BaseFieldProps {
    contentFunc: (propName: string) => Promise<string>
}

@observer export class Hint extends BaseFieldComponent<HintProps> {
    @observable private isShown: boolean = false;
    private text: string;

    constructor(props?: HintProps, context?: any) {
        super(props, context);

        this.showHide = this.showHide.bind(this);
        this.hide = this.hide.bind(this);
    }

    render() {

        return (<ul onBlur={this.hide} className="panel-tool-options inline">
            <li className={this.isShown ? "dropdown open horizontalList" : "dropdown horizontalList"}>
                <a onClick={this.showHide} href="javascript:;" data-toggle="dropdown" className="dropdown-toggle" aria-expanded={this.isShown}>
                    <i className="fa fa-question-circle"></i>
                </a>
                <ul className="dropdown-menu dropdown-menu-center">
                    <div className="dropdown-text">
                        <p dangerouslySetInnerHTML={{ __html: this.text }}></p>
                    </div>
                </ul>
            </li>
        </ul>)
    }

    renderInternal(): JSX.Element {
        throw "Not Implemented";
    }

    showHide() {
        if (this.isShown) {
            this.isShown = false;
        }
        else {
            if (this.text) {
                this.isShown = true;
            }
            else {
                this.props.contentFunc(this.props.fullHtmlName).bind(this).then(text => {
                    this.text = text;
                    this.isShown = true;
                })
            }
        }
    }

    hide() {
        this.isShown = false;
    }
}
import { AsyncUIProps, BaseProps, RawHTML, withAsyncFrame } from "cnsys-ui-react";
import { EAUBaseComponent, Label, TextEditorUI } from "eau-core";
import { observable, runInAction, action } from "mobx";
import { observer } from "mobx-react";
import React from "react";
import { ObjectHelper } from "cnsys-core";

interface LabelRowUIProps extends BaseProps, AsyncUIProps {
    updateLabelFunc: (label: Label) => any;
}

@observer class LabelRowUIImpl extends EAUBaseComponent<LabelRowUIProps, Label> {

    private initialLabelInfo: Label; //Used to restore label information to previous state
    @observable private isEditMode: boolean = false;

    constructor(props: LabelRowUIProps) {
        super(props);

        this.initialLabelInfo = JSON.parse(JSON.stringify(this.model));
        this.updateLabel = this.updateLabel.bind(this);
        this.cancelChanges = this.cancelChanges.bind(this);
    }

    render() {

        if (this.isEditMode) {
            return this.renderEditMode();
        } else {
            return this.renderDisplayMode();
        }
    }

    private renderEditMode() {
        return <tr key={`${this.model.labelID}_${this.isEditMode ? "edit" : "display"}`}>
            <td className="word-break">{this.model.code}</td>
            <td>
                <div>
                    <span>
                        {this.textAreaFor(m => m.value, null, 3, { name: "labelValue", className: "form-control", maxLength: 1000 })}
                    </span>
                </div>
            </td>
            <td>
                <div className="w-100 col-12">
                    <TextEditorUI {...this.bind(m => m.description)} />
                </div>
            </td>
            <td className="buttons-td">
                <span>
                    <button className="btn btn-secondary" title={this.getResource("GL_SAVE_L")} onClick={this.updateLabel}>
                        <i className="ui-icon ui-icon-check"></i>
                    </button>
                    <button className="btn btn-secondary" title={this.getResource("GL_REFUSE_L")} onClick={this.cancelChanges}>
                        <i className="ui-icon ui-icon-ban"></i>
                    </button>
                </span>
            </td>
        </tr>
    }

    private renderDisplayMode() {
        return (<tr key={ObjectHelper.newGuid()}>
            <td className="word-break">{this.model.code}</td>
            <td>
                <div>
                    <span className="label-value">{this.model.value}</span>
                </div>
            </td>
            <td>
                <div>
                    <span className="label-value"><RawHTML rawHtmlText={this.model.description} /></span>
                </div>
            </td>
            <td className="buttons-td">
                <button className="btn btn-secondary" onClick={() => { this.switchEditMode(true) }} title={this.getResource("GL_EDIT_L")}><i className="ui-icon ui-icon-edit"></i></button>
            </td>
        </tr>);
    }

    private switchEditMode(editModeEnabled: boolean) {
        this.isEditMode = editModeEnabled;
    }

    private updateLabel() {
        let that = this;

        this.props.registerAsyncOperation(this.props.updateLabelFunc(this.model).then(() => {
            if (this.model.getErrors().length == 0) {

                runInAction(() => {
                    that.initialLabelInfo = JSON.parse(JSON.stringify(that.model));
                    that.switchEditMode(false);
                })
            }
        }));
    }

    @action private cancelChanges() {
        this.model = new Label(this.initialLabelInfo);
        this.switchEditMode(false)
    }
}

export const LabelRowUI = withAsyncFrame(LabelRowUIImpl);
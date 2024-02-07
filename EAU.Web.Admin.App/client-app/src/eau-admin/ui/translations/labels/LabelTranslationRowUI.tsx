import * as React from "react";
import {AsyncUIProps, BaseProps, RawHTML, withAsyncFrame} from "cnsys-ui-react";
import {observer} from "mobx-react";
import {EAUBaseComponent} from "eau-core";
import {LabelTranslationI18nVM} from "../../../models/LabelTranslationI18nVM";

interface LabelTranslationRowUIProps extends BaseProps, AsyncUIProps {
    saveTranslationFunc
}

@observer
class LabelTranslationRowUIImpl extends EAUBaseComponent<LabelTranslationRowUIProps, LabelTranslationI18nVM> {

    private initialLabelTranslationInfo : LabelTranslationI18nVM;

    constructor(props: LabelTranslationRowUIProps){
        super(props);
    }

    render(){
        return (
            <>
                <tr id={"edit-row-"+this.model.labelID}>
                    <td className="word-break">{this.model.labelCode}</td>

                    <td>
                        <span className="label-value">{this.model.bgValue}</span>
                    </td>


                    <td className="w-25">
                        <div>
                            <div className={"preview-" + this.model.labelID}>
                                <button className="btn btn-secondary float-right ml-2" onClick={()=>{this.switchEditMode(true)}} title={this.getResource("GL_EDIT_L")}>
                                    <i className="ui-icon ui-icon-edit"></i>
                                </button>
                                <span className="label-value">{this.model.value}</span>
                            </div>

                        </div>

                        <div>

                            <div className={"d-none edit-" + this.model.labelID}>
                                <div className="row">
                                    <div className="form-group col-12">
                                        {this.textAreaFor(m => m.value, null, 3,
                                            { name:"labelValue",
                                                className:"form-control",
                                                maxLength:1000,
                                                id:'label-'+this.model.labelID+'-translation'})}
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="col-12">
                                        <div className="btn-group float-right">
                                            <button   className="btn btn-secondary"
                                                      title={this.getResource("GL_SAVE_L")}
                                                      onClick={() => {this.updateTranslation()}}
                                            >
                                                <i className="ui-icon ui-icon-check"></i>
                                            </button>
                                            <button className="btn btn-secondary"
                                                    title={this.getResource("GL_REFUSE_L")}
                                                    onClick={() => {this.cancelChanges()}}
                                            >
                                                <i className="ui-icon ui-icon-ban"></i>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </td>

                    <td>
                        <span className="label-value"><RawHTML rawHtmlText={this.model.description} /></span>
                    </td>
                </tr>
            </>
        );
    }

    private switchEditMode(editModeEnabled : boolean){
        this.initialLabelTranslationInfo = JSON.parse(JSON.stringify(this.model));
        if(editModeEnabled){
            $(".preview-" + this.model.labelID).addClass("d-none");
            $(".edit-" + this.model.labelID).removeClass("d-none");
        }else{
            $(".preview-" + this.model.labelID).removeClass("d-none");
            $(".edit-" + this.model.labelID).addClass("d-none");
        }
    }

    private updateTranslation(){
        this.props.registerAsyncOperation(
            this.props.saveTranslationFunc(this.model).then(() => {
                if(this.model.getErrors().length == 0) {
                    this.initialLabelTranslationInfo = JSON.parse(JSON.stringify(this.model));
                    this.switchEditMode(false);
                }
            })
        );
    }

    private cancelChanges() {
        this.model = new LabelTranslationI18nVM(this.initialLabelTranslationInfo);
        this.switchEditMode(false)
    }
}

export const LabelTranslationRowUI = withAsyncFrame(LabelTranslationRowUIImpl);
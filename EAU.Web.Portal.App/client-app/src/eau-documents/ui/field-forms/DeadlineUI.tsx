import React from "react";
import { EAUBaseComponent, Constants } from "eau-core";
import { BaseProps } from "cnsys-ui-react";
import { DeadlineVM } from "../..";

export class DeadlineUI extends EAUBaseComponent<BaseProps, DeadlineVM> {

    render(): JSX.Element {
        return (
            <>
                <div className="row">
                    <div className="form-group col-12">
                        <p className="field-text"> {this.getDeadlineText()}</p>
                    </div>
                </div>
            </>
        )
    }
    getDeadlineText(): string {       

        var executionPeriodTypeText = "";

        if (this.model.executionPeriodType.toString() == "1") {
            if (this.model.periodValue && this.model.periodValue > 1)
                executionPeriodTypeText = this.getResource("DOC_GL_Hours_L");
            else
                executionPeriodTypeText = this.getResource("DOC_GL_Hour_L");
        }
        else {
            if (this.model.executionPeriodType.toString() == "2") {
                if (this.model.periodValue && this.model.periodValue > 1)
                    executionPeriodTypeText = this.getResource("DOC_GL_Days_L");
                else
                    executionPeriodTypeText = this.getResource("DOC_GL_Day_L");
            }
        }

        return this.model.periodValue.toString() + " " + executionPeriodTypeText;

        
    }
}
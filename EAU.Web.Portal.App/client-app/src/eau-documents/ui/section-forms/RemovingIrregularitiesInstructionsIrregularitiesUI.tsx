import * as React from "react";
import { BaseProps, ViewMode } from "cnsys-ui-react";
import { EAUBaseComponent } from "eau-core";
import { RemovingIrregularitiesInstructionsVM, RemovingIrregularitiesInstructionsIrregularitiesVM, RemovingIrregularitiesInstructionsIrregularityUI } from "../..";
import { IrregularitiesInstructionsCollectionsUI } from "../field-forms/IrregularitiesInstructionsCollectionsUI";

export class RemovingIrregularitiesInstructionsIrregularitiesUI extends EAUBaseComponent<BaseProps, RemovingIrregularitiesInstructionsVM>{
    constructor(props: BaseProps) {
        super(props);

        //Bind       

        //Init        

    }

    renderEdit(): JSX.Element {
        return (
            <>
                <IrregularitiesInstructionsCollectionsUI {...this.bind(m => m.irregularities)} />        
            </>
        )
    }


    renderDisplay(): JSX.Element {
        return (
            <>
                <IrregularitiesInstructionsCollectionsUI {...this.bind(m => m.irregularities)} />                
            </>
        )
    }    
}
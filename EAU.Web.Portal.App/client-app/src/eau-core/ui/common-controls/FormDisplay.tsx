import { Helper } from "cnsys-core";
import { RawHTML } from "cnsys-ui-react";
import React from "react";

interface FormDisplayProps {
    displayHTML: any
}

export function FormDisplayUI(props: FormDisplayProps) {

    if (props.displayHTML) {
        Helper.ensurePolyfilForReplaceAll();

        let displayHTMLContent = props.displayHTML
            .replaceAll('contenteditable="true"', '')
            .replaceAll('class="editable"', '')

        return <RawHTML rawHtmlText={displayHTMLContent}/>
    }

    return null
}
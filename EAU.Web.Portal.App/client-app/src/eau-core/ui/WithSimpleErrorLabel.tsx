import React from "react";
import { BaseFieldProps } from 'cnsys-ui-react'
import { observer } from "mobx-react";

const SimpleErrorLabelUI = observer((props: BaseFieldProps) =>
    (<>
        {
            props.modelReference && props.modelReference.hasErrors() ?
                <ul className="invalid-feedback" id={`${props.fullHtmlName.replace(".", "_")}_ERRORS`}>
                    {props.modelReference.getErrors().map((err, idx) => { return (<li key={idx}><i className="ui-icon ui-icon-error mr-1"></i>{err}</li>); })}
                </ul>
                : null
        }
    </>)
);

export function withSimpleErrorLabel<C extends React.ComponentClass<BaseFieldProps>>(Component: C): C {

    var ret = function (props: any) {
        return (
            <>
                <Component {...props} >{props.children}</Component>
                <SimpleErrorLabelUI {...props}> </SimpleErrorLabelUI>
            </>);
    };

    return ret as any;
}

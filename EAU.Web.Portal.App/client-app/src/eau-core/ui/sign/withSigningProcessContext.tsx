import * as React from "react";
import { ISigningProcessContextProps, SigningProcessContext } from './SigningProcessContext';

export function withSigningProcessContext<C extends React.ComponentClass<ISigningProcessContextProps>>(Component: C): any {
    return function (props: any) {
        return (
            <SigningProcessContext.Consumer>
                {value =>
                    <Component {...props}
                        signerID={value.signerID}
                        processID={value.processID}
                        signerChooseChannel={value.signerChooseChannel}
                        signerCompletSigning={value.signerCompletSigning}
                        signerRejectChannel={value.signerRejectChannel}>
                        {props.children}
                    </Component>}
            </SigningProcessContext.Consumer>);
    }
}
import * as React from "react";
import { SigningChannels } from "../../models/sign/SignProcessEnums";

export interface ISigningProcessContextProps {
    signerID: number;
    processID: string;
    signerChooseChannel: (signerID: number, channel: SigningChannels) => void;
    signerCompletSigning: () => void;
    signerRejectChannel: (rejectFromPortal: boolean, rejectReson?: string) => void;
}

export const SigningProcessContext = React.createContext<ISigningProcessContextProps>({
    signerID: undefined,
    processID: undefined,
    signerChooseChannel: null,
    signerCompletSigning: null,
    signerRejectChannel: null
});
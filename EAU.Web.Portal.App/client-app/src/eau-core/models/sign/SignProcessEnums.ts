import { TypeSystem } from 'cnsys-core';
import { moduleContext } from '../../ModuleContext';

export enum SigningRequestStatuses {
    InProcess = 1,
    Rejecting = 2,
    Completing = 3,
}
TypeSystem.registerEnumInfo(SigningRequestStatuses, 'SigningRequestStatuses', moduleContext.moduleName);

export enum SignerSigningStatuses {
    Waiting = 0,
    StartSigning = 1,
    Signed = 2
}
TypeSystem.registerEnumInfo(SignerSigningStatuses, 'SignerSigningStatuses', moduleContext.moduleName);

export enum SigningChannels {
    BtrustLocal = 0,
    BtrustRemote = 1,
    EvrotrustRemote = 2
}
TypeSystem.registerEnumInfo(SigningChannels, 'SigningChannels', moduleContext.moduleName);
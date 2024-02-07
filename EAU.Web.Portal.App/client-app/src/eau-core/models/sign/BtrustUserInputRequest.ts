import { TypeSystem } from 'cnsys-core';
import { moduleContext } from '../../ModuleContext';

export interface BtrustUserInputRequest {
    input: string;
    inputType?: BtrustUserInputTypes;
    otp?: string;
}

export enum BtrustUserInputTypes {
    EGN = 0,
    LNCH = 1,
    PROFILE = 2,
    PHONE = 3,
    EMAIL = 4
}
TypeSystem.registerEnumInfo(BtrustUserInputTypes, 'BtrustUserInputTypes', moduleContext.moduleName);
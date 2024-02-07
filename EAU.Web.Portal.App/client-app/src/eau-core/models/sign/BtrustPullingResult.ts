import { TypeSystem } from 'cnsys-core';
import { moduleContext } from '../../ModuleContext';

export interface BtrustPullingResult {
    code: string;
    status?: BtrustDocStatus;
    rejectReson: string;
}

export enum BtrustDocStatus {
    SIGNED = 0,
    REJECTED = 1
}
TypeSystem.registerEnumInfo(BtrustDocStatus, 'BtrustDocStatus', moduleContext.moduleName);
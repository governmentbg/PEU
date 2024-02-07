import { EAUBaseDataService } from 'eau-core';
import { EmployeeInfo, FindObjectsContractInfo, Region, SecurityTypesMode, SecurityTypesControl } from '../models/ModelsManualAdded';

export class MOIDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "MOI";
    }
    
    public getEmployees(structureCode: string, identityValue?: string, active?: boolean): Promise<EmployeeInfo[]> {
        return this.get<EmployeeInfo[]>(`/CHOD/employees/`, EmployeeInfo, { structureCode: structureCode, identityValue: identityValue, active: active });
    }

    public getObjects(structureCode?: string, identityValue?: string, legalEntityIdentityValue?: string, personIdentityTypeID?: number, personIdentityValue?: string, securityActivityTypeID?: number, active?: boolean): Promise<FindObjectsContractInfo[]> {
        return this.get<FindObjectsContractInfo[]>(`/CHOD/objects/`, FindObjectsContractInfo, {structureCode: structureCode, identityValue: identityValue, legalEntityIdentityValue:legalEntityIdentityValue, personIdentityTypeID:personIdentityTypeID, personIdentityValue: personIdentityValue, securityActivityTypeID:securityActivityTypeID, active:active});
    }

    public getRegions(): Promise<Region[]> {
        return this.get<Region[]>(`/CHOD/regions/`, Region, null).then(districts => {
            if (districts && districts.length > 0) 
                return districts.filter((district) => district.itemID >= 0);
        });
    }

    public getSecurityTypesModes(): Promise<SecurityTypesMode[]> {
        return this.get<SecurityTypesMode[]>(`/CHOD/securityTypesMode`, SecurityTypesMode, null);
    }

    public getSecurityTypesControl(): Promise<SecurityTypesControl[]> {
        return this.get<SecurityTypesControl[]>(`/CHOD/securityTypesControl`, SecurityTypesControl, null);
    }
}
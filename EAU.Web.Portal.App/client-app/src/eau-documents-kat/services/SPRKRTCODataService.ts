import { EAUBaseDataService } from 'eau-core';
import { FourDigitSearchCriteria, IFourDigitsSearchResult, SpecialNumberSearchCriteria } from '../models/ModelsManualAdded';

export class SPRKRTCODataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "MOI";
    }

    public getFreeFourDigitsPlates(criteria: FourDigitSearchCriteria): Promise<IFourDigitsSearchResult> {
        return this.get<IFourDigitsSearchResult>("/SPRKRTCO/FreeFourDigitsRegNumbers", null, criteria);
    }

    public getFreeSpecialNumbers(criteria: SpecialNumberSearchCriteria): Promise<number> {
        return this.get<number>("/SPRKRTCO/FreeSpecialNumbers", null, criteria);
    }
}
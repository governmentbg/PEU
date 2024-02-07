import { Functionality } from '../models';
import { EAUBaseDataService } from './EAUBaseDataService';

export class FunctionalitiesDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "Functionalities";
    }

    public getFunctionalities(): Promise<Functionality[]> {
        return this.get<Functionality[]>(``, Functionality, null);
    }
}
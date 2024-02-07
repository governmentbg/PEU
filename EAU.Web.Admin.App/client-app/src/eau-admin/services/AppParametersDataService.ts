import { AppParameter, AppParameterSearchCriteria, ResourceHelpers, Functionalities, EAUBaseDataService } from 'eau-core';

export class AppParametersDataService extends EAUBaseDataService {

    protected baseUrl(): string {
        return super.baseUrl() + "AppParameters";
    }

    public editAppParameter(appParameter: AppParameter): Promise<void> {
        return this.put<void>(`/${appParameter.code}`, AppParameter, appParameter);
    }

    public getAppParameters(searchCriteria: AppParameterSearchCriteria): Promise<AppParameter[]> {

        return this.doSearch(searchCriteria, () => this.get<AppParameter[]>(``, AppParameter, searchCriteria), (data, criteria) => {

            return data.sort((a: AppParameter, b: AppParameter) => {

                //Сортираме първо по Модул/Функционалност
                if (ResourceHelpers.getResourceByEmun(a.functionalityID, Functionalities) > ResourceHelpers.getResourceByEmun(b.functionalityID, Functionalities)) {
                    return 1;
                } else if (ResourceHelpers.getResourceByEmun(a.functionalityID, Functionalities) < ResourceHelpers.getResourceByEmun(b.functionalityID, Functionalities)) {
                    return -1
                }

                //След това сортираме по код
                if (a.code > b.code) {
                    return 1;
                } else if (a.code < b.code) {
                    return -1
                }

                return 0;
            })
        })
    }
}
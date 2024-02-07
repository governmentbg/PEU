import { ArrayHelper, ObjectHelper } from "cnsys-core";
import { BaseProps } from "cnsys-ui-react";
import { attributesClassFormControl, AutoCompleteUI, Country, EAUBaseComponent, IAutoCompleteItem, Nomenclatures } from "eau-core";
import { action } from "mobx";
import * as React from 'react';
import { CitizenshipVM } from '../../models';

export class CitizenshipUI extends EAUBaseComponent<BaseProps, CitizenshipVM> {

    constructor(props: BaseProps) {
        super(props);

        this.onSearchCoutries = this.onSearchCoutries.bind(this);
        this.onAutoCompleteChange = this.onAutoCompleteChange.bind(this);
    }

    renderEdit(): JSX.Element {
        return (
            <>                
                <AutoCompleteUI
                    dataSourceSearchDelegat={this.onSearchCoutries}
                    placeholder=''
                    triggerLength={1}
                    onChangeCallback={this.onAutoCompleteChange}
                    {...this.bind(m => m.countryGRAOCode)}
                    attributes={attributesClassFormControl} />
            </>
        );
    }

    renderDisplay(): JSX.Element {
        return (
            <>
                {this.textDisplayFor(m => m.countryName)}
            </>
        );
    }

    onSearchCoutries(text: string): Promise<IAutoCompleteItem[]> {

        return Nomenclatures.getCountries(el => !ObjectHelper.isStringNullOrEmpty(el.code)).then(nom => {
            let res: IAutoCompleteItem[] = [];
            let country: Country[] = [];

            if (ObjectHelper.isStringNullOrEmpty(text)) {
                country = nom
            } else {
                country = ArrayHelper.queryable.from(nom).where(el => el.name.toUpperCase().indexOf(text.toUpperCase()) >= 0).toArray();
            }

            for (let i: number = 0; i < country.length; i++) {
                let currCounty = country[i];

                let autoCompleteItem: IAutoCompleteItem = {                    
                    id: currCounty.code,
                    text: currCounty.name,
                    callbackData: {
                        countryName: currCounty.name,
                        countryGRAOCode: currCounty.code
                    }
                };

                res.push(autoCompleteItem);
            }

            res.sort((el1, el2) => {
                if (el1.text < el2.text)
                    return -1;
                else if (el1.text > el2.text)
                    return 1;
                else
                    return 0;
            });

            return res;
        });
    }

    @action onAutoCompleteChange(selectedOpt: IAutoCompleteItem): void {
        if (ObjectHelper.isNullOrUndefined(selectedOpt)) {            
            this.model.countryGRAOCode = undefined;
            this.model.countryName = undefined;

        } else {            
            this.model.countryName = selectedOpt.callbackData.countryName;
            this.model.countryGRAOCode = selectedOpt.callbackData.countryGRAOCode;           
        }
    }
}
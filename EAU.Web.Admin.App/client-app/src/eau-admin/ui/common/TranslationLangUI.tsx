import { AsyncUIProps, withAsyncFrame } from 'cnsys-ui-react';
import { Language, resourceManager } from 'eau-core';
import { NomenclaturesDataService } from '../../services/NomenclaturesDataService';
import React, { useEffect, useState } from 'react';
import { BasePagedSearchCriteria } from 'cnsys-core';

interface TranslationLangProps extends AsyncUIProps {
    onLangSelect: (lang: Language) => any;
    loadCurrentLanguage: (e) => void;
}

const TranslationLangUI: React.FC<TranslationLangProps> = ({ onLangSelect, loadCurrentLanguage, registerAsyncOperation }) => {
    const [langs, setLangs] = useState([])
    const [selectedLang, setSelectedLang] = useState<string>()

    useEffect(() => {
        registerAsyncOperation(loadLangs())
    }, [])

    return <div className="form-row">
        <div className="col-auto">
            <label htmlFor="LANGUAGE" className="form-text">{resourceManager.getResourceByKey("GL_LANGUAGE_L")}</label>
        </div>
        <div className="col-md-8 col-lg-6 col-xl-4 form-group">
            <select id="select-selectedLang" className="form-control" value={selectedLang ? selectedLang : ""} onChange={onChange} >
                <optgroup label={resourceManager.getResourceByKey("GL_ACTIVE_L")}>
                    {
                        langs && langs.filter(l => l.isActive == true).map((lang: Language, index: any) => <option value={lang.code} key={index}>{`${lang.code} - ${lang.name}`}</option>)
                    }
                </optgroup>
                <optgroup label={resourceManager.getResourceByKey("GL_UNACTIVE_L")}>
                    {
                        langs && langs.filter(l => l.isActive == false).map((lang: Language, index: any) => <option value={lang.code} key={index}>{`${lang.code} - ${lang.name}`}</option>)
                    }
                </optgroup>
            </select>
        </div>
    </div>

    function onChange(e: any) {
        let selectedLang = langs.filter(l => l.code == e.target.value)[0];

        sessionStorage.setItem('lang', '{"code":"' + selectedLang.code + '","name":"' + selectedLang.name + '"}');
        setSelectedLang(selectedLang.code);

        onLangSelect(selectedLang);
    }

  

    function loadLangs(): Promise<any> {
        let nomSrv: NomenclaturesDataService = new NomenclaturesDataService();
        let criteria: BasePagedSearchCriteria = new BasePagedSearchCriteria();

        criteria.page = 1;
        criteria.pageSize = Number.MAX_SAFE_INTEGER;

        return nomSrv.searchLanguages(criteria).then(langs => {
            setLangs(langs.filter(x => x.code != "bg"));

            langs = langs.filter(x => x.code != "bg");
            loadCurrentLanguage(langs[0])
        });
    }
}

export default withAsyncFrame(TranslationLangUI)
import * as React from "react";
import * as JsCookies from "js-cookie";
import { Nomenclatures, Language, appConfig, resourceManager } from "eau-core";
import { UncontrolledDropdown, DropdownItem, DropdownMenu, DropdownToggle } from "reactstrap";


export function LanguageSelectorUI(props: any) {
    const [languages, setLanguages] = React.useState<Language[]>([]);

    React.useEffect(() => {
        Nomenclatures.getLanguages(l => l.isActive).then(langs => {
            if (langs && langs.length > 0) {
                setLanguages(langs);
            }
        });
    }, []);

    let onLangChange = function (e: any): void {
        e.preventDefault();

        let lang: string = $(e.target).data('lang');

        if (lang != appConfig.clientLanguage) {
            JsCookies.set("currentLang", lang, { path: "/", expires: new Date(2033, 12), domain: `${appConfig.commonCookieDomain}`, secure: true});

            if (/\/[a-zA-Z]{2}\//.test(window.location.href)) {
                window.location.href = window.location.href.replace(/\/[a-zA-Z]{2}\//, lang.toLocaleLowerCase() == 'bg' ? '/' : `/${lang}/`);
            } else {
                let baseUrl = document.getElementsByTagName('base')[0].getAttribute('href')!;
                let currHref = window.location.href;


                let idx = baseUrl === '/' ? currHref.indexOf(window.location.origin) : currHref.indexOf(baseUrl);

                let hrefTillBaseUrl = baseUrl === '/' ? currHref.substring(0, idx + window.location.origin.length + 1) : currHref.substring(0, idx + baseUrl.length);
                let hrefAfterBaseUrl = baseUrl === '/' ? currHref.substring((idx + window.location.origin.length + 1)) : currHref.substring(idx + baseUrl.length);
                let newUrl = `${hrefTillBaseUrl}${lang.toLocaleLowerCase() == 'bg' ? '' : `${lang}/`}${hrefAfterBaseUrl}`;

                window.location.href = newUrl;
            }
        }
    }

    if (languages && languages.length > 0) {
        return (
            <UncontrolledDropdown a11y={true}>
                <DropdownToggle id="dropdownMenuLanguage" aria-controls="dropdownMenuLanguageMenu" type="button" caret className="navbar-top-item" tag="button">
                    <span title={resourceManager.getResourceByKey("GL_CHOOSE_LANG_I")}><i className="ui-icon nav-icon-globe mr-1" aria-hidden="true"></i>
                        <span className="sr-only">{resourceManager.getResourceByKey("GL_CHOOSE_LANG_I")}</span>
                        <span aria-hidden="true">{appConfig.clientLanguage}</span>
                    </span>

                </DropdownToggle>
                <DropdownMenu id="dropdownMenuLanguageMenu" aria-labelledby="dropdownMenuLanguage" positionFixed={true}>
                    {languages.map((lang, idx) => {
                        if (appConfig.clientLanguage == lang.code)
                            return (<DropdownItem key={lang.languageID} tag="button" type="button" active aria-current="true">{lang.name}</DropdownItem>);
                        else
                            return (
                                <DropdownItem
                                    tag="button"
                                    type="button"
                                    key={lang.languageID}
                                    data-lang={lang.code}                                    
                                    onClick={onLangChange}>
                                    {lang.name}
                                </DropdownItem>);
                    })}
                </DropdownMenu>
            </UncontrolledDropdown>);
    } else {
        return null;
    }
}
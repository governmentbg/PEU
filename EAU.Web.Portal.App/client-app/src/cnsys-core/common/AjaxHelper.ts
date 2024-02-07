import { UrlHelper } from './Helper';
import { compressToEncodedURIComponent } from 'lz-string';

export namespace AjaxHelper {

    export function get<T>(url: string, data?: any | string): Promise<T> {
        var settings: JQueryAjaxSettings;

        settings = { method: "GET", type: "GET", url: url, data: data }

        return ajax<T>(settings);
    }

    export function post<T>(url: string, data?: any | string): Promise<T> {
        var settings: JQueryAjaxSettings;

        settings = { method: "POST", type: "POST", url: url, data: data }

        return ajax<T>(settings);
    }

    export function ajax<T>(settings: JQueryAjaxSettings): Promise<T> {
        if (settings.headers == null)
            settings.headers = { "Content-Type": "application/json" };

        //Това се прави заради MVC bindinga на Array
        if (settings.method == 'GET' && settings.data != undefined && $.isPlainObject(settings.data)) {
            let urlParams: any = UrlHelper.createUrlQueryByObject(settings.data);
            let urlParamsString = $.param(urlParams);

            settings.data = null;
            let newUrl = `${settings.url}?${urlParamsString}`;

            //TODO: да се вземе от конфигурация 1200.
            if (newUrl.length > 1200) {
                //компресираме Query-то, за да не превишим лимита на браузърите.
                settings.url += '?lzQuery=' + compressToEncodedURIComponent(urlParamsString);
            } else {
                settings.url = newUrl;
            }
        }

        var jqXHRResult = $.ajax(settings);
        
        var resThis = {
            jqXHR: jqXHRResult
        };

        return Promise.resolve(jqXHRResult).bind(resThis).then(function (result: any) {
            return result;
        });
    }

    export function doAjaxSetup(settings: JQueryAjaxSettings) {
        $.ajaxSetup(settings);
    }
}
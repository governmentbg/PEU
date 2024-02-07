import { AjaxHelper, ApiError, BaseDataService, ClientError, moduleContext, ObjectHelper, TypeSystem } from 'cnsys-core';

export abstract class EAUBaseDataService extends BaseDataService {

    //#region overrides

    public ajax<T>(settings: JQueryAjaxSettings, type: any): Promise<T> {

        var promiseBind = {};

        /*Не се използват arrow функции, за да имаме достъп до this.*/
        var result = this.prepareAjaxSettings(settings).bind(promiseBind).then(function (setts) {
            var promiseBind = this;

            if (type) {
                var constructor = TypeSystem.getTypeInfo(type).ctor;

                return AjaxHelper.ajax<any>(settings).then(function (result) {

                    /*препредаваме данните от this, на ресултата. */
                    ObjectHelper.assign(promiseBind, this);

                    if (Array.isArray(result)) {
                        var elems = <any[]>result;
                        return elems.map(elem => new constructor(elem))
                    }
                    else {
                        if (result) {
                            return new constructor(result);
                        }
                        else {
                            return null;
                        }
                    }
                });
            }
            else {
                return AjaxHelper.ajax<T>(settings).then(function (result) {

                    /*препредаваме данните от this, на ресултата. */
                    ObjectHelper.assign(promiseBind, this);

                    return result;
                });
            }
        });

        result = result.catch((ex: any): any => {
            var error: ApiError;
            var xhr: JQueryXHR = ex;

            //Ако услугата връща 404 NotFound web методите връщат null
            if (xhr.status == 404) {
                /*препредаваме данните от xhr, на ресултата. */
                ObjectHelper.assign(promiseBind, { jqXHR: xhr });

                error = new ApiError(xhr.responseJSON);
                error.httpStatusCode = xhr.status;

            } else if (xhr.status == 429) {
                error = new ApiError(moduleContext.resourceManager.getResourceByKey('GL_TOO_MANY_REQUESTS_E'), xhr.status);
                error.treatAsWarning = true;
            } else if (xhr.status == 401) {
                error = new ApiError(moduleContext.resourceManager.getResourceByKey('GL_USR_SESSION_TIMEOUT_E'), xhr.status);
                error.treatAsWarning = true;

            } else if (ex.responseJSON && ex.responseText) {
                error = new ApiError(xhr.responseJSON);
                error.httpStatusCode = xhr.status;
            } else if (ex.readyState == 0 && ex.statusText == "error") {
                error = new ApiError(moduleContext.resourceManager.getResourceByKey('GL_SYSTEM_UNAVAILABLE_E'), 500);

            } else {
                throw new ClientError(ex);
            }

            throw error;
        });

        return result;
    }

    //#endregion

    //#region helpers

    public userLogout(redirectUri?: string) {
        window.localStorage.setItem("active-user", "off")

        var input = document.createElement('input');
        input.name = "redirectUri";
        input.value = !ObjectHelper.isStringNullOrEmpty(redirectUri) ? redirectUri : "";
        input.hidden = true;

        var form = document.createElement('form');
        form.method = "POST";
        form.action = `api/Users/logout`;
        form.appendChild(input);
        document.body.appendChild(form);

        form.submit();
        document.body.removeChild(form);
    }

    public userLogin() {
        let form = document.createElement('form');
        form.method = 'POST';
        form.action = 'api/Users/Login';
        document.body.appendChild(form);

        form.submit();
        document.body.removeChild(form);
    }

    //#endregion
}
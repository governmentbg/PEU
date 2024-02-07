import { appConfig, moduleContext } from "cnsys-core";
import React from "react";

interface LoadingProps {
    delay?: number;
    isGlobal?: boolean;
}

export function LoadingUI(props: LoadingProps): any {
    // тук се прави проверката, защото ползваме LoadingUI в boot-a (още преди да сме заредили ресурсите в bootstrap-a)
    const text = (moduleContext && moduleContext.resourceManager) ? moduleContext.resourceManager.getResourceByKey('GL_LOADING_I') : appConfig.loadingUIText;

    return (
        <div className="loader-overlay-local load">
            <i className="ui-icon ui-icon-loading ui-icon-spin"></i>
            <span className="sr-only">
                {
                    // това никога не се показва. Идеята е да го има като текст, 
                    // за да може слепите хора да го прочетат (като ползват специални програми, които четат съдържанието на html-a)
                    text
                }
            </span>
        </div>);
}
import { ArrayHelper } from 'cnsys-core';
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, RawHTML, withAsyncFrame } from 'cnsys-ui-react';
import { CmsDataService, Page, resourceManager } from 'eau-core';
import React, { useEffect, useState } from 'react';
import { withRouter } from 'react-router';

interface CmsUIRouteProps extends BaseRouteParams {
}

interface CmsProps extends BaseRouteProps<CmsUIRouteProps>, AsyncUIProps, BaseRoutePropsExt {
}

const CmsImpl: React.FC<CmsProps> = (props) => {

    const [cmsPages, loadCMSPages] = useState<Page[]>();

    useEffect(() => {
        props.registerAsyncOperation(new CmsDataService().getPages().then(pages => {
            if (pages && pages.length > 0)
                loadCMSPages(pages);
        }))
    }, [])

    if (cmsPages && cmsPages.length > 0) {
        let page: Page = ArrayHelper.queryable.from(cmsPages).singleOrDefault(el => el.code.toLowerCase() == props.location.pathname.toLowerCase().substring(1));

        if (page) {
            return (
                <div className="page-wrapper" id="ARTICLE-CONTENT">
                    <RawHTML rawHtmlText={page.content}></RawHTML>
                </div>);
        } else
            return (
                <div className="page-wrapper" id="ARTICLE-CONTENT">
                    <div className="alert alert-danger" role="alert"><p>{resourceManager.getResourceByKey('GL_PAGE_NOT_FOUND_L')}</p></div>
                </div>);
    }
    else
        return null;
}

export const CmsUI = withRouter(withAsyncFrame(CmsImpl));
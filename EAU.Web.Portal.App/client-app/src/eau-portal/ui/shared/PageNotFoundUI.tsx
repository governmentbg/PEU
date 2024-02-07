import { resourceManager } from 'eau-core';
import React from 'react';
import { Alert } from 'reactstrap';

export const PageNotFoundUI = function () {
    return (
        <div className="page-wrapper" id="ARTICLE-CONTENT">
            <div className="row">
                <div className="col-12">
                    <Alert color="warning" >{resourceManager.getResourceByKey("GL_PAGE_NOT_FOUND_L")}</Alert>
                </div>
            </div>
        </div>);
}
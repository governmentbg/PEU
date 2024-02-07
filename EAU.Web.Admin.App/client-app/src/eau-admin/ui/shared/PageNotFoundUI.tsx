import { resourceManager } from 'eau-core';
import React from 'react';
import { Alert } from 'reactstrap';

export const PageNotFoundUI = function () {
    return <div className="card">
        <div className="card-body">
            <Alert color="warning" >{resourceManager.getResourceByKey("GL_PAGE_NOT_FOUND_L")}</Alert>
        </div>
    </div>
}
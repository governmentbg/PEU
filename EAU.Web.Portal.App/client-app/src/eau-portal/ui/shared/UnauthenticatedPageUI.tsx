import { resourceManager } from "eau-core";
import React from "react";
import { Alert } from "reactstrap";

export function UnauthenticatedPageUI() {

    return <div className="page-wrapper" id="ARTICLE-CONTENT">
        <Alert color="warning">{resourceManager.getResourceByKey("GL_UNAUTHENTICATED_USER_I")}</Alert>
    </div>
}
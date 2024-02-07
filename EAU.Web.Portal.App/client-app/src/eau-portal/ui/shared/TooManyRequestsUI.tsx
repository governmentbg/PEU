import { resourceManager } from "eau-core";
import React from "react";
import { Alert } from "reactstrap";

export function TooManyRequestsUI() {

    return <div className="page-wrapper" id="ARTICLE-CONTENT">
        <Alert color="warning">{resourceManager.getResourceByKey("GL_TOO_MANY_REQUESTS_E")}</Alert>
    </div>
}
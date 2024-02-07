import { resourceManager } from "eau-core";
import React from "react";
import { Alert } from "reactstrap";

export function SessionTimeoutUI() {

    return <div className="page-wrapper" id="ARTICLE-CONTENT">
        <Alert color="warning">{resourceManager.getResourceByKey("GL_USR_SESSION_TIMEOUT_E")}</Alert>
    </div>
}
import { resourceManager } from "eau-core";
import React from "react";
import { Alert } from "reactstrap";

export function SessionTimeoutUI() {
    return (
        <div className="card">
            <div className="card-body">
                <Alert color="warning">{resourceManager.getResourceByKey("GL_USR_SESSION_TIMEOUT_E")}</Alert>
            </div>
        </div>);
}
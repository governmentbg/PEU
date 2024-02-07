import { resourceManager } from "eau-core";
import React from "react";

export function SystemUnavailablePageUI() {

    return (<div id="main-wrapper">
        <div id="content-wrapper">
            <div id="page-wrapper">
                <div className="container-fluid">
                    <div className="card">
                        <div className="card-body">
                            <div className="alert alert-danger null">{resourceManager.getResourceByKey("GL_SYSTEM_UNAVAILABLE_E")}</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    );
}
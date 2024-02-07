import { ObjectHelper } from "cnsys-core";
import React from "react";
import { resourceManager } from "../common";

interface CollapsableContentContainerProps {
    titleKey: string;
}

export function withCollapsableContent(Component: React.ComponentClass | React.FC, containerProps: CollapsableContentContainerProps) {

    var ret = function (props: any) {

        var cmpUniqueId = ObjectHelper.newGuid();

        return (
            <div className={`card ${props.grayBackground ? 'card--search' : ""}`}>
                <div className="card-header">
                    <h3>
                        {containerProps.titleKey ? resourceManager.getResourceByKey(containerProps.titleKey) : null}
                        <button id={`collapsable-trigger-${cmpUniqueId}`} className="system-button toggle-collapse" onClick={() => { onCollapseCriteria(`collapsable-content-${cmpUniqueId}`) }}>
                            <i className="ui-icon ui-icon-chevron-up" aria-hidden="true"></i>
                        </button>
                    </h3>
                </div>
                <div id={`collapsable-content-${cmpUniqueId}`} className="collapse" style={{ display: 'block' }}>
                        <Component {...props}></Component>
                </div>
            </div>);

        function onCollapseCriteria(targetId: string): void {
            let triger = $(`#collapsable-trigger-${cmpUniqueId}`);
            triger.toggleClass('collapsed');

            $('#' + targetId).slideToggle();
        }
    };

    return ret as any;
}
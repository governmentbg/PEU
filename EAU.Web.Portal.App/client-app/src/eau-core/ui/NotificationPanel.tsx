import { observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';

export enum NotificationType {
    Success = 1,
    Info = 2,
    Warning = 3,
    Error = 4
}

interface NotificationPanelProps {
    text: string;
    notificationType: NotificationType;
    isDismissible?: boolean;
}

@observer export class NotificationPanel extends React.Component<NotificationPanelProps, any> {

    private notificationTypeClass = "";
    @observable isVisiblePanel = true;

    constructor(props: NotificationPanelProps) {
        super(props)

        if (this.props.notificationType == NotificationType.Success)
            this.notificationTypeClass = "alert-success";
        else if (this.props.notificationType == NotificationType.Info)
            this.notificationTypeClass = "alert-info";
        else if (this.props.notificationType == NotificationType.Warning)
            this.notificationTypeClass = "alert-warning";
        else if (this.props.notificationType == NotificationType.Error)
            this.notificationTypeClass = "alert-danger";

        this.dissmisNotificationPanel = this.dissmisNotificationPanel.bind(this);
    }

    render() {
        return this.isVisiblePanel
            ? <div className={`alert ${this.notificationTypeClass} ${this.props.isDismissible ? "alert-dismissible fade show" : null}`} role="alert">
                {this.props.isDismissible ? <button type="button" className="close" onClick={this.dissmisNotificationPanel} data-dismiss="alert">×</button> : null}
                {this.props.text}
            </div>
            : null
    }

    dissmisNotificationPanel() {
        this.isVisiblePanel = false;
    }
}
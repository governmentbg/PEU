import React from "react";
import { RouteComponentProps } from "react-router-dom";

export interface AsyncComponentLoaderProps extends RouteComponentProps<any> {
    load: Promise<React.ComponentType<any>>
}

export class AsyncComponentLoader extends React.Component<AsyncComponentLoaderProps, any> {
    constructor(props: AsyncComponentLoaderProps) {
        super(props);

        this.state = { component: null };
    }

    componentDidMount() {
        let that = this;

        this.props.load.then((component) => {
            that.setState({ component: component });
        });
    }

    render() {
        if (this.state.component) {
            return React.createElement(this.state.component, this.props);
        } else {
            return (<div id="loader" className="loader-overlay load"></div>);
        }
    }
}
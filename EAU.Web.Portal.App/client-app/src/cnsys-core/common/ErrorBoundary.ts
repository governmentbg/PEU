import React from "react";
import { handleErrorLog } from "./ErrorHandler";

export class ErrorBoundary extends React.Component {
    constructor(props: any) {
        super(props);
        this.state = { hasError: false };
    }

    componentDidCatch(error: any, info: any) {
        handleErrorLog(error);
    }

    render() {
        return this.props.children;
    }
}
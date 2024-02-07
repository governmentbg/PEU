import React from "react";
import { TagData } from './ClosingTag';
import { OpeningTag } from './OpeningTag';

export interface OpeningTagLinkProps {
    data: TagData;
    onLinkClick: () => void;
}

export class OpeningTagLink extends React.Component<OpeningTagLinkProps, any> {
    constructor(props?: OpeningTagLinkProps) {
        super(props);

        this.handleClick = this.handleClick.bind(this);
    }

    render() {
        var data = this.props.data;
        var style = {
            cursor: "pointer"
        };

        if (data.nodes) {
            return (<span style={style} onClick={this.handleClick}><OpeningTag data={data} /></span>);
        } else {
            return (<OpeningTag data={data} />);
        }
    }

    handleClick(event: any): void {
        event.preventDefault();
        this.props.onLinkClick();
    }
}

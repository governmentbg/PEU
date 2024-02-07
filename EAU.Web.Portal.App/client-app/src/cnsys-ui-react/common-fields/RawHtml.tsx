import * as React from "react";
import { BaseFieldProps } from '../BaseFieldComponent';

interface RawHTMLProps extends BaseFieldProps {
    rawHtmlText: string;
    attributes?: React.DetailedHTMLProps<React.HTMLAttributes<HTMLDivElement>, HTMLDivElement>;
}

export const RawHTML: React.FC<RawHTMLProps> = ({ rawHtmlText, attributes }) => {

    return <div dangerouslySetInnerHTML={{ __html: rawHtmlText}} {...attributes}></div>
}
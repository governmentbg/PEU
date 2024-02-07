import React from 'react';

interface FieldFormUIProps {
    title?: string;
    headerType?: "h3" | "h4"
    required?: boolean;
    ariaDescribedby?: string
}

export const FieldFormUI: React.FC<FieldFormUIProps> = ({ title, required, children, headerType, ariaDescribedby }) => {

    return (
        <fieldset className="fields-group" aria-describedby={ariaDescribedby}>
            {fieldFormHeader()}
            {children}
        </fieldset>
    );

    function fieldFormHeader() {
        if (title) {

            switch (headerType) {
                case "h4": return <legend><h4 className={`field-title ${required ? "required-field" : ""}`}>{title}</h4></legend>
                default: return <legend><h3 className={`field-title ${required ? "required-field" : ""}`}>{title}</h3></legend>
            }
        }

        return null
    }
}
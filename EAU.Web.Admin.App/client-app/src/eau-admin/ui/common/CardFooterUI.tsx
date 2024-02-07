import { BaseProps } from 'cnsys-ui-react';
import { resourceManager } from 'eau-core';
import React from 'react';

interface CardFooterProps extends BaseProps {
    onClear: () => void;
    onSearch: () => void;
}

const CardFooterUI: React.FC<CardFooterProps> = ({ onClear, onSearch }) => {

    return <div className="card-footer">
        <div className="button-bar">
            <div className="right-side">
                <button type="button" className="btn btn-primary" onClick={onSearch}><i className="ui-icon ui-icon-search ci-btn"></i>&nbsp;{resourceManager.getResourceByKey("GL_SEARCH_L")}</button>
            </div>
            <div className="left-side">
                <button type="button" className="btn btn-secondary" onClick={onClear}>{resourceManager.getResourceByKey("GL_CLEAR_L")}</button>
            </div>
        </div>
    </div>
}

export default CardFooterUI;
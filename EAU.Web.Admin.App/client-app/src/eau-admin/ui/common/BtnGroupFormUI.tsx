import { BaseProps } from 'cnsys-ui-react';
import { resourceManager } from 'eau-core';
import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from 'reactstrap';

interface BtnGroupFormUIProps extends BaseProps {
    onSave: () => any;
    refuseLink: string;
}

const BtnGroupFormUI: React.FC<BtnGroupFormUIProps> = ({ onSave, refuseLink }) => {

    return <div className="card-footer">
        <div className="button-bar">
            <div className="right-side">
                <Button type="button" color="primary" onClick={onSave}>{resourceManager.getResourceByKey("GL_SAVE_L")}</Button>
            </div>
            <div className="left-side">
                <Link to={refuseLink} className="btn btn-secondary" >{resourceManager.getResourceByKey("GL_REFUSE_L")}</Link>
            </div>
        </div>
    </div>
}

export default BtnGroupFormUI;
import { Constants as ConstantsEAU, Page, resourceManager } from 'eau-core';
import React from 'react';
import { Link } from 'react-router-dom';
import { Constants } from '../../../Constants';

interface PredefinedPagesProps {
    cmsPage: Page,
}

export const PredefinedPagesListUI: React.FC<PredefinedPagesProps> = ({ cmsPage }) => {
    return <tr>
        <td>{cmsPage.title}</td>
        <td>{cmsPage.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime)}</td>
        <td className="buttons-td">
            <Link to={Constants.PATHS.PagesEdit.replace(':pageID', cmsPage.pageID.toString())}>
                <button className="btn btn-secondary" title={resourceManager.getResourceByKey("GL_EDIT_L")}>
                    <i className="ui-icon ui-icon-edit"></i>
                </button>
            </Link>
        </td>
    </tr>
}
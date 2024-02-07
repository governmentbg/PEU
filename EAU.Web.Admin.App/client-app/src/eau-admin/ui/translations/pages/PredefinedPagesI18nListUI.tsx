import { AsyncUIProps, withAsyncFrame, BaseRoutePropsExt, BaseProps } from 'cnsys-ui-react';
import { EAUBaseComponent} from 'eau-core';
import React from 'react';
import { Link } from 'react-router-dom';
import {Constants} from '../../../Constants';
import { PageI18nVM } from '../../../models/PageI18nVM';

interface PredefinedPagesProps extends BaseProps, AsyncUIProps, BaseRoutePropsExt {
    cmsPage: PageI18nVM
}

class PredefinedPagesListI18nUIImpl extends EAUBaseComponent<PredefinedPagesProps, any>{

    constructor(props) {
        super(props);
    }

    render() {
        return <>
            <tr>
                <td>{this.props.cmsPage.bgTitle}</td>
                <td>{this.props.cmsPage.title}</td>
                <td className="buttons-td">
                    <Link to={Constants.PATHS.TranslationsPagesTranslate.replace(':pageID', this.props.cmsPage.pageID.toString())}>
                        <button className="btn btn-secondary" title={this.getResource("GL_EDIT_L")}>
                            <i className="ui-icon ui-icon-edit"></i>
                        </button>
                    </Link>
                </td>
            </tr>
        </>
    }
}

export const PredefinedPagesListI18nUI = withAsyncFrame(PredefinedPagesListI18nUIImpl); 
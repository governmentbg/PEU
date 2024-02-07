import { BindableReference } from "cnsys-core";
import { AsyncUIProps, BaseRouteParams, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { attributeClassRequiredLabel, Constants as ConstantsEAU, EAUBaseComponent, Page, TextEditorUI, ValidationSummaryErrors } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Constants } from '../../../Constants';
import { CmsDataService } from '../../../services/CmsDataService';
import { PageValidator } from '../../../validations/PageValidator';
import BtnGroupFormUI from '../../common/BtnGroupFormUI';

interface CmsPagesProps extends BaseRouteParams {
    pageID: number
}

interface PagesProps extends BaseRouteProps<CmsPagesProps>, AsyncUIProps, BaseRoutePropsExt {
}

@observer class PredefinedPagesForm extends EAUBaseComponent<PagesProps, Page> {

    private pageID: number;
    @observable isFormSubmited: boolean = false;
    @observable isLoaded: boolean;

    private cmsDataService: CmsDataService;

    constructor(props?: PagesProps) {
        super(props);

        this.pageID = this.props.match.params.pageID;
        this.cmsDataService = new CmsDataService();

        this.init();

        if (this.pageID) {
            this.props.registerAsyncOperation(this.cmsDataService.getPageById(this.pageID, "bg")
                .then((result) => { this.model = result })
                .finally(() => this.isLoaded = true));
        }

        this.validators = [new PageValidator()];
    }

    private onSave() {
        if (this.validators[0].validate(this.model)) {
            this.props.registerAsyncOperation(this.cmsDataService.updateCmsPage(this.model)
                .then(() => { this.isFormSubmited = true }))
        }
        else
            this.isFormSubmited = null;
    }

    @action private init() {
        this.isLoaded = false;
        this.model = new Page();
        this.onSave = this.onSave.bind(this);
    }

    render() {

        let dataResult: any = null;

        if (this.isLoaded) {

            if (this.model) {

                dataResult = <div className="card">
                    <form id="groupListForm">
                        <div className="card-body">

                            {this.isFormSubmited && <div className="alert alert-success"><p>{this.getResource("GL_UPDATE_OK_I")}</p></div>}

                            <ValidationSummaryErrors {...this.props} />

                            <div className="row">

                                <div className="form-group col-xl-6 col-lg-12 col-md-12 ">
                                    {this.labelFor(x => x.title, "GL_HTML_PAGE_TITLE_L", attributeClassRequiredLabel)}
                                    {this.textBoxFor(x => x.title)}
                                </div>

                                <div className="form-group  col-xl-3 col-lg-6 col-md-6 col-sm-12">
                                    <div className="row">
                                        <div className="col">
                                            {this.labelFor(x => x.updatedOn, "GL_CREATE_UPDATE_DATE_L")}
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="form-group col-12 col-sm-7 col-md-12">
                                            <input id="input_updatedOn" type="text" className="form-control" disabled value={this.model.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime)} />
                                        </div>
                                    </div>
                                </div>

                                <div className="form-group col-xl-3 col-lg-6 col-md-6 col-sm-12">
                                    <div className="row">
                                        <div className="col">
                                            {this.labelFor(x => x.code, "GL_HTML_PAGE_CODE_L")}
                                        </div>
                                    </div>
                                    <div className="row">
                                        <div className="form-group col-12 col-sm-7 col-md-12">
                                            {this.textBoxFor(x => x.code, { className: "form-control", disabled: true })}
                                        </div>
                                    </div>
                                </div>

                            </div>

                            <div className="row">
                                <div className="form-group col-sm-12">
                                    {this.labelFor(x => x.content, "GL_CONTENT_L", attributeClassRequiredLabel)}
                                    <div><TextEditorUI {...this.bind(m => m.content)} /></div>
                                </div>
                            </div>
                        </div>
                        <BtnGroupFormUI refuseLink={Constants.PATHS.Pages} onSave={this.onSave} />
                    </form>
                </div>
            }
            else {
                dataResult =
                    <div className="card">
                        <div className="card-body">
                            <div className="alert alert-dismissible alert-warning fade show">
                                <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                            </div>
                        </div>
                    </div>
            }
        }

        return <>
            {dataResult}
        </>
    }
}

export const PredefinedPagesFormUI = withRouter(withAsyncFrame(PredefinedPagesForm, false)); 

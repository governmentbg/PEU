import { ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame } from 'cnsys-ui-react';
import { Constants as ConstantsEAU, Declaration, EAUBaseComponent, TextEditorUI, ValidationSummary, ValidationSummaryErrors, ValidationSummaryStrategy } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { withRouter } from 'react-router-dom';
import { Alert } from 'reactstrap';
import { Constants } from '../../../Constants';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';
import { DeclarationsValidator } from '../../../validations/DeclarationsValidator';
import BtnGroupFormUI from '../../common/BtnGroupFormUI';

interface DeclarationFormProps extends BaseRouteProps<any>, AsyncUIProps, BaseRoutePropsExt {
}

@observer class DeclarationFormImplUI extends EAUBaseComponent<DeclarationFormProps, Declaration>{
    @observable notification: any;
    @observable isDeclarationLoaded: boolean;

    private declarationsDataService: NomenclaturesDataService;

    constructor(props: DeclarationFormProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    componentDidMount() {
        if (this.props.match.params.declarationID)
            this.initModelForEdit();
    }

    render(): JSX.Element {

        if (ObjectHelper.isNullOrUndefined(this.props.match.params.declarationID)
            || (this.props.match.params.declarationID && this.isDeclarationLoaded)) {

            return <div className="card">
                <div className="card-body">
                    {this.notification}
                    <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                    <ValidationSummary model={this.model} {...this.props} strategy={ValidationSummaryStrategy.includeOnlyModelErrors} />
                    <div className="row">
                        <div className="form-group col-md-8">
                            {this.labelFor(x => x.description, "GL_DESCRIPTION_L", { className: "required-field" })}
                            {this.textBoxFor(x => x.description)}
                        </div>
                        <div className="form-group col-md-3 col-sm-6  col-xl-2">
                            {this.labelFor(x => x.code, "GL_CODE_L", this.props.location.pathname == Constants.PATHS.NomAddDeclaration ? { className: "required-field" } : null)}
                            {this.props.location.pathname == Constants.PATHS.NomAddDeclaration ? this.textBoxFor(x => x.code, { className: "form-control" }) : this.textBoxFor(x => x.code, { className: 'form-control', disabled: true })}
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-md-4">
                            <label className="d-none d-md-inline-block" />
                            <div className="custom-control custom-checkbox">
                                {this.checkBoxFor(x => x.isRquired, "GL_REQUIRE_MARK_IN_APPLICATION_L", { className: "custom-control-input" })}
                            </div>
                        </div>
                        <div className="form-group col-md-4">
                            <label className="d-none d-md-inline-block" />
                            <div className="custom-control custom-checkbox">
                                {this.checkBoxFor(x => x.isAdditionalDescriptionRequired, "GL_REQUIRE_DECLARER_DESC_L", { className: "custom-control-input" })}
                            </div>
                        </div>
                        {this.props.match.params.declarationID ?
                            <div className="form-group col-md-4">
                                <div className="row">
                                    <div className="col">
                                        {this.labelFor(x => x.updatedOn, "GL_CREATE_UPDATE_DATE_L")}
                                    </div>
                                </div>
                                <div className="row">
                                    <div className="form-group col-sm-6  col-xl-6 col-md-8">
                                        <input id="input_updatedOn" className="form-control" disabled value={this.model.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime).toString()} />
                                    </div>
                                </div>
                            </div> : null}
                    </div>
                    <div className="row form-group">
                        <div className="col-sm-12">
                            {this.labelFor(x => x.content, "GL_CONTENT_L", { className: "required-field" })}
                            <div><TextEditorUI {...this.bind(m => m.content)} /></div>
                        </div>
                    </div>
                </div>
                <BtnGroupFormUI onSave={this.save} refuseLink={Constants.PATHS.NomDeclarations} />
            </div>
        }

        return null;
    }

    private save() {

        if (this.props.location.pathname == Constants.PATHS.NomAddDeclaration) {
            //Adding new declartion
            if (this.validators[0].validate(this.model)) {
                this.props.registerAsyncOperation(this.declarationsDataService.createDeclarations(this.model).then(() => {
                    runInAction.bind(this)(() => {
                        this.notification = <Alert color="success">{this.getResource("GL_SAVE_OK_I")}</Alert>
                        this.model = new Declaration();
                    })
                }))
            }
        } else {
            //updating current declaration
            this.props.registerAsyncOperation(this.declarationsDataService.updateDeclarations(this.model).then(() => {
                this.notification = <Alert color="success">{this.getResource("GL_UPDATE_OK_I")}</Alert>
            }))
        }
    }

    //#region Main funcs

    private funcBinds() {
        this.save = this.save.bind(this);
    }

    @action private init() {

        this.model = new Declaration();
        this.model.isRquired = false;
        this.model.isAdditionalDescriptionRequired = false;

        this.validators = [new DeclarationsValidator()]
        this.declarationsDataService = new NomenclaturesDataService();
    }

    private initModelForEdit() {
        this.props.registerAsyncOperation(this.declarationsDataService.getDeclarationById(this.props.match.params.declarationID).then((declaration) => {

            runInAction.bind(this)(() => {

                if (!declaration)
                    this.notification = <Alert color="error">{this.getResource("GL_DECLARATION_NOT_EXIST_E")}</Alert>

                this.model = declaration;
            })
        }).finally(() => {
            this.isDeclarationLoaded = true;
        }))
    }

    //#endregion
}

export const DeclarationFormUI = withRouter(withAsyncFrame(DeclarationFormImplUI, false));
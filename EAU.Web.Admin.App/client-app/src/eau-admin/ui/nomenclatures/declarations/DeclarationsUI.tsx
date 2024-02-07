import { ArrayHelper, ObjectHelper } from 'cnsys-core';
import { AsyncUIProps, BaseRouteProps, BaseRoutePropsExt, ConfirmationModal, withAsyncFrame } from 'cnsys-ui-react';
import { attributesClassFormControl, AutoCompleteUI, Declaration, DeclarationSearchCriteria, EAUBaseComponent, IAutoCompleteItem, Pagination, ValidationSummaryErrors, Constants as ConstantsEAU } from 'eau-core';
import { action, observable, runInAction } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { withRouter } from 'react-router';
import { Link } from 'react-router-dom';
import { Alert } from 'reactstrap';
import { Constants } from '../../../Constants';
import { NomenclaturesDataService } from '../../../services/NomenclaturesDataService';
import CardFooterUI from '../../common/CardFooterUI';

interface DeclarationsProps extends BaseRouteProps<any>, AsyncUIProps, BaseRoutePropsExt {
}

@observer class DeclarationsImplUI extends EAUBaseComponent<DeclarationsProps, DeclarationSearchCriteria>{
    @observable private pageLoaded: boolean = false;
    @observable private declarationsResult: Declaration[] = [];
    @observable private allDeclarations: Declaration[] = [];
    @observable private notification: any;
    private declarationsDataService: NomenclaturesDataService;

    constructor(props: DeclarationsProps) {
        super(props);

        this.funcBinds();
        this.init();
    }

    render() {

        if (this.declarationsResult && this.declarationsResult.length > 0) {
            return (
                <>
                    <div className="card">
                        <div className="card-header">
                            <h3>{this.getResource("GL_SEARCH_TITLE_L")}</h3>
                        </div>
                        <div className="card-body">
                            <div className="row">
                                <div className="form-group col-md-8">
                                    <label htmlFor="DESCRIPTION">{this.getResource("GL_DESCRIPTION_L")}</label>
                                    <AutoCompleteUI
                                        {...this.bind(m => m.id)}
                                        attributes={attributesClassFormControl}
                                        dataSourceSearchDelegat={this.onAutoCompleteSearch}
                                    />
                                </div>
                            </div>
                        </div>
                        <CardFooterUI onClear={this.clear} onSearch={this.searchDeclarationById} />
                    </div>
                    <div className="card">
                        <div className="card-body">
                            {this.notification}
                            <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                            <div id="request-messages"></div>
                            <div className="card-navbar">
                                <div className="button-bar">
                                    <div className="right-side">
                                        <Link to={Constants.PATHS.NomAddDeclaration} className="btn btn-primary"><i className="ui-icon ui-icon-plus-white"></i> {this.getResource("GL_ADD_DECLARATION_L")}</Link>
                                    </div>
                                </div>
                            </div>
                            <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} aditionalCssClass="pagination-container--page-top" />
                            <div className="table-responsive">
                                <table className="table table-bordered table-striped table-hover">
                                    <thead>
                                        <tr>

                                            <th>{this.getResource("GL_DESCRIPTION_L")}</th>
                                            <th>{this.getResource("GL_CREATE_UPDATE_DATE_L")}</th>
                                            <th>{this.getResource("GL_ACTIONS_L")}</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {
                                            this.declarationsResult.map((declaration, index) => <tr key={index}>

                                                <td>{declaration.description}<span className="param-value"></span></td>
                                                <td>{declaration.updatedOn.format(ConstantsEAU.DATE_FORMATS.dateTime).toString()}<span className="param-value"></span></td>
                                                <td className="buttons-td">


                                                    <Link to={Constants.PATHS.NomEditDeclaration.replace(':declarationID', declaration.declarationID.toString())} className="btn btn-secondary"><i className="ui-icon ui-icon-edit" title={this.getResource("GL_EDIT_L")}></i></Link>

                                                    <ConfirmationModal
                                                        modalTitleKey={"GL_DELETE_RECORD_I"}
                                                        modalTextKeys={["GL_DELETE_DECLARATION_MSG_L"]}
                                                        noTextKey="GL_CANCEL_L"
                                                        yesTextKey="GL_CONFIRM_L"
                                                        onSuccess={() => this.deleteValue(declaration.declarationID)}>
                                                        <button type="button" className="btn btn-secondary" title={this.getResource("GL_DELETE_L")}>
                                                            <i className="ui-icon ui-icon-trash"></i>
                                                        </button>
                                                    </ConfirmationModal>
                                                </td>

                                            </tr>)
                                        }
                                    </tbody>
                                </table>
                            </div>
                            <Pagination activePage={this.model.page} count={this.model.count} pagesCount={this.model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={this.onPageChange} />
                        </div>
                    </div>
                </>);
        }
        else if (this.pageLoaded) {
            return (<>
                <div className="card">
                    <div className="card-header">
                        <h3>{this.getResource("GL_SEARCH_TITLE_L")}</h3>
                    </div>
                    <div className="card-body">
                        <div className="row">
                            <div className="form-group col-md-8">
                                <label htmlFor="DESCRIPTION">{this.getResource("GL_DESCRIPTION_L")}</label>
                                <AutoCompleteUI
                                    {...this.bind(m => m.id)}
                                    attributes={attributesClassFormControl}
                                    dataSourceSearchDelegat={this.onAutoCompleteSearch}
                                />
                            </div>
                        </div>
                    </div>
                    <CardFooterUI onClear={this.clear} onSearch={this.searchDeclarationById} />
                </div>
                <div className="card">
                    <div className="card-body">
                        {this.notification}
                        <ValidationSummaryErrors asyncErrors={this.props.asyncErrors} />
                        <div id="request-messages"></div>
                        <div className="card-navbar">
                            <div className="button-bar">
                                <div className="right-side">
                                    <Link to={Constants.PATHS.NomAddDeclaration} className="btn btn-primary"><i className="ui-icon ui-icon-plus-white"></i> {this.getResource("GL_ADD_DECLARATION_L")}</Link>
                                </div>
                            </div>
                            <div className="alert alert-dismissible alert-warning fade show">
                                <p>{this.getResource("GL_NO_DATA_FOUND_L")}</p>
                            </div>
                        </div>
                    </div>
                </div>
            </>);
        }
        else {
            return <Link to={Constants.PATHS.NomAddDeclaration} className="btn btn-primary"><i className="ui-icon ui-icon-plus-white"></i> {this.getResource("GL_ADD_DECLARATION_L")}</Link>
        }

        return null;
    }



    private onAutoCompleteSearch(text: string): Promise<IAutoCompleteItem[]> {

        let res: IAutoCompleteItem[] = [];
        let filteredData = ArrayHelper.queryable.from(this.allDeclarations).where(el => el.description.toUpperCase().indexOf(text.toUpperCase()) >= 0).toArray();
        let autoCompleteItem: IAutoCompleteItem;

        filteredData.forEach((filtered) => {
            autoCompleteItem = { id: filtered.declarationID, text: filtered.description };
            res.push(autoCompleteItem);
        })

        return Promise.resolve(res);
    }

    @action private onPageChange(page: any): void {
        this.model.page = page;
        this.props.registerAsyncOperation(this.searchDeclarations());
    }

    private searchDeclarations() {

        return this.declarationsDataService.searchDeclarations(this.model).then(result => {
            runInAction.bind(this)(() => {
                this.declarationsResult = result;
            })
        });
    }

    private searchAllDeclarations() {
        let declarationCriteria = new DeclarationSearchCriteria;
        declarationCriteria.pageSize = Number.MAX_SAFE_INTEGER;
        

        return this.declarationsDataService.searchDeclarations(declarationCriteria).then(result => {
            runInAction.bind(this)(() => {
                this.allDeclarations = result;
            })
        });
    }

    @action private clear() {
        this.model.id = null;
        this.searchDeclarations();
        this.searchAllDeclarations();

    }

    private searchDeclarationById() {
        this.pageLoaded = true;
        this.declarationsResult = [];

        if (!ObjectHelper.isNullOrUndefined(this.model.id)) {
            this.props.registerAsyncOperation(this.declarationsDataService.getDeclarationById(this.model.id).then(result => {
                this.declarationsResult = [result]
                this.model.count = this.declarationsResult.length;
            }));
        }
        else {
            //this.model.getPagesCount()
            this.model.count = this.declarationsResult.length;
          //  this.props.registerAsyncOperation(this.searchDeclarations())
            }
    }

    private deleteValue(declarationID: number) {
        this.props.registerAsyncOperation(this.declarationsDataService.deleteDeclarations(declarationID).then(() => {
            this.searchDeclarations().then(() => {
                this.notification = <Alert color="success">{this.getResource('GL_DELETED_TIME_I')}</Alert>
            })
        }));
    }

    //#region main functions

    private funcBinds() {
        this.onPageChange = this.onPageChange.bind(this);
        this.deleteValue = this.deleteValue.bind(this);
        this.searchDeclarationById = this.searchDeclarationById.bind(this);
        this.onAutoCompleteSearch = this.onAutoCompleteSearch.bind(this);
        this.clear = this.clear.bind(this);
    }

    private init() {
        this.declarationsDataService = new NomenclaturesDataService();
        this.model = new DeclarationSearchCriteria();
        this.props.registerAsyncOperation(this.searchDeclarations());
        this.props.registerAsyncOperation(this.searchAllDeclarations());
    }

    //#endregion
}

export const DeclarationsUI = withRouter(withAsyncFrame(DeclarationsImplUI, false));



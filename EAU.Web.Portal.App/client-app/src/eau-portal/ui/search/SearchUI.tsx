import { ArrayHelper, ObjectHelper, PagingCollection } from 'cnsys-core';
import { AsyncUIProps, BaseRouteProps, BaseRoutePropsExt, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { appConfig, Button, EAUBaseComponent, IPageRouteNode, NotificationPanel, NotificationType, pageRoute, Pagination } from 'eau-core';
import { action, observable } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { Link } from 'react-router-dom';
import { SitemapSearchVM } from '../../models/ModelsManualAdded';

interface SiteMapPreviewItem {
    pathPattern: string;
    text: string;
}

interface SearchsUIProps extends BaseRouteProps<any>, BaseRoutePropsExt, AsyncUIProps {
}

export const attributeClassRequiredLabel = { className: 'form-control-label' };

@observer class SearchsUIImpl extends EAUBaseComponent<SearchsUIProps, SitemapSearchVM>{
    @observable private pageRouteItems: IPageRouteNode[];
    @observable private pagedCollectionItems: PagingCollection<IPageRouteNode>;
    @observable private countSearchResults: number;
    @observable private afterSearch: boolean;
    @observable private currentSearchFor: string;

    constructor(props: SearchsUIProps) {
        super(props);

        //Bind
        this.onSearch = this.onSearch.bind(this);
        this.componentDidMount = this.componentDidMount.bind(this);
        this.getResults = this.getResults.bind(this);
        this.onPageChange = this.onPageChange.bind(this);
        this.onSearchForChange = this.onSearchForChange.bind(this);
        this.handleKeyPress = this.handleKeyPress.bind(this);

        //Init
        this.pageRouteItems = undefined;
        this.model = new SitemapSearchVM();
        //this.props.routerExt.mergeParamsTo(this.model);
        this.countSearchResults = 0;
        this.afterSearch = false;
        this.currentSearchFor = "";
    }

    componentDidMount() {
        this.pageRouteItems = pageRoute.getAllPageRouteNodes.filter(i => !(i.disabled === true));
    }

    render() {
        return (
            <div className="page-wrapper" id="ARTICLE-CONTENT">
                <div className="search-page">
                    <fieldset className="fields-group">
                        <div className="row mb-3">
                            <div className="col-12">
                                {this.labelFor(x => x.searchFor, "GL_SEARCH_CRITERIA_L", attributeClassRequiredLabel)}
                            </div>
                            <div className="form-group col-sm col-md-5">
                                {this.textBoxFor(x => x.searchFor, { type: "text", className: "form-control", onKeyPress: this.handleKeyPress, required: true }, this.onSearchForChange)}
                            </div>
                            <div className="form-group col-sm-auto">
                                <Button type="submit" className="btn btn-primary search-button" lableTextKey="GL_SEARCH_L" onClick={this.onSearch}>
                                    <i className="ui-icon ui-icon-search mr-1"></i>
                                </Button>
                            </div>
                        </div>
                    </fieldset>
                    {this.getPagination("pagination-container--page-top")}
                    {this.getResults()}
                    {this.getPagination("pagination-container--page-bottom")}
                </div>
            </div>);
    }

    getPagination(className: string) {

        if (this.pagedCollectionItems && this.pagedCollectionItems.getPagesCount() > 0) {
            return (
                <Pagination activePage={this.pagedCollectionItems.currentPage}
                    count={this.countSearchResults}
                    pagesCount={this.pagedCollectionItems.getPagesCount()}
                    maxVisiblePage={10}
                    size="sm"
                    onSelect={this.onPageChange}
                    aditionalCssClass={className} />);
        }
        return null;
    }

    handleKeyPress(e: any) {
        if (e.key === 'Enter') {
            this.onSearch(null);

        }else
            this.afterSearch = false;
    }

    onSearchForChange(e: any) {
        if (this.afterSearch == true)
            this.afterSearch = false;
    }

    onSearch(e: any) {
        //this.props.routerExt.changeParams(this.model);
        this.model.currentPage = 1;
        this.search();
    }

    search(): any {

        this.model.clearErrors();

        var isValidCriteria: boolean;

        if (this.model.searchFor && this.model.searchFor != "" && this.model.searchFor.length > 2) {
            this.currentSearchFor = this.model.searchFor;
            isValidCriteria = true;
        }
        else
            isValidCriteria = false;

        if (isValidCriteria) {
            this.afterSearch = true;
            var allFiltereditems: IPageRouteNode[] = [];
            for (let item of this.pageRouteItems) {
                var filteredItems = this.filterItems(item, this.currentSearchFor);

                for (let fitem of filteredItems) {
                    allFiltereditems.push(fitem);
                }
            }

            allFiltereditems.sort((el1, el2) => {
                if (el1.text < el2.text)
                    return -1;
                else if (el1.text > el2.text)
                    return 1;
                else
                    return 0;
            })

            if (allFiltereditems && allFiltereditems.length > 0) {
                this.pagedCollectionItems = new PagingCollection(allFiltereditems.length, allFiltereditems, this.model.currentPage, appConfig.defaultPageSize);
            }
            else {
                this.pagedCollectionItems = null;
            }
            this.countSearchResults = allFiltereditems.length;
        }
        else {
            this.model.addError("searchFor", this.getResource("GL_SEARCHCRITERIA_MIN3SYMBOLS"));
        }
    }

    filterItems(item: IPageRouteNode, search: string): IPageRouteNode[] {

        var filtereditems: IPageRouteNode[] = [];

        if (!ObjectHelper.isStringNullOrEmpty(search)) {
            if (item.text.toString().toLocaleLowerCase().indexOf(search.toLocaleLowerCase()) != -1
                && !ArrayHelper.queryable.from(filtereditems).singleOrDefault(el => el.text == item.text)) {

                if (item.pathRegEx != item.pathPattern) {
                    var dynamicParams = item.pathPattern.match(/\/:[^/]*/gi);

                    if (dynamicParams && dynamicParams.length > 0) {
                        item.pathRegEx = item.pathPattern;
                        item.pathParamsNames = [];

                        for (var dynamicParam of dynamicParams) {
                            if (dynamicParam.indexOf("?") > 0) {
                                item.pathPattern = item.pathRegEx.replace(dynamicParam, "")
                            }
                            else {
                                item.pathPattern = item.pathRegEx.replace(dynamicParam, "")
                            }
                        }
                    }
                    //else {
                    //    item.pathPattern = item.pathRegEx;
                    //}
                }
                //item.text = this.addKeywordColorToText(item.text.toString(), search); 

                filtereditems.push(item);
            }
        }

        return filtereditems;
    }

    getResults() {
        if (this.pagedCollectionItems) {
            return (
                <div className="search-results">
                    {this.pagedCollectionItems.getCurrentItems.map((item: SiteMapPreviewItem, index: number) => {
                        return (
                            <div className="search-result__item" key={item.text}>
                                <div className="num">{(index + 1) + ((this.pagedCollectionItems.currentPage - 1) * this.pagedCollectionItems.pagedDataState.itemsPerPage)}</div>
                                <Link to={item.pathPattern}>
                                    {this.rawHtml(this.addKeywordColorToText(item.text, this.currentSearchFor))}
                                </Link>
                            </div>
                        );
                    })}
                </div>
            );
        }
        else {
            return (
                <>
                    {this.afterSearch == true ?
                        <NotificationPanel notificationType={NotificationType.Info}  text={this.getResource("GL_NO_RESULTS_I")} />
                        : null}
                </>);
        }

    }

    @action private onPageChange(page: any): void {
        //this.props.routerExt.changeParams(this.model);
        this.pagedCollectionItems.changeCurrentPage(page);
        this.model.currentPage = page;
        this.search();
    }

    addKeywordColorToText(text: string, keyword: string): string {

        const keywordRxInsensitive = new RegExp(keyword, 'gi');
        const matchedKeywords = text.match(keywordRxInsensitive);

        if (matchedKeywords && matchedKeywords.length > 0) {

            matchedKeywords.forEach(matchedKeyword => {
                text = text.replace(matchedKeyword, "<strong class=\"keyword\">" + matchedKeyword + "</strong>");
            })
        }

        return text;
    }

}


export const SearchsUI = withRouter(withAsyncFrame(SearchsUIImpl))
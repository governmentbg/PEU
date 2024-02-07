import { ObjectHelper } from "cnsys-core";
import { AsyncUIProps, withAsyncFrame, BaseProps } from 'cnsys-ui-react';
import { Page, PageSearchCriteria, Pagination, resourceManager, ValidationSummaryErrors, ValidationSummary, ValidationSummaryStrategy } from 'eau-core';
import React, { useEffect, useState } from 'react';
import { Table } from 'reactstrap';
import { CmsDataService } from '../../../services/CmsDataService';
import { PredefinedPagesListUI } from './PredefinedPagesListUI';

interface PredefinedPagesProps extends BaseProps, AsyncUIProps {
}

const PredefinedPagesImlp: React.FC<PredefinedPagesProps> = (props) => {

    const [cmsPages, setCMSPages] = useState<Page[]>([]);
    const [isLoaded, setIsLoaded] = useState<boolean>(false);
    const [model, setModel] = useState<PageSearchCriteria>(new PageSearchCriteria());

    const cmsDataService: CmsDataService = new CmsDataService();

    useEffect(() => {
        props.registerAsyncOperation(cmsDataService.searchCmsPages(model, "bg").then(result => { setCMSPages(result) }).finally(() => setIsLoaded(true)));
    }, [])

    var dataResult: any = null;

    if (isLoaded) {
        if (!ObjectHelper.isNullOrUndefined(cmsPages)) {
            dataResult = <>
                <Pagination activePage={model.page} count={model.count} pagesCount={model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={onPageChange} aditionalCssClass="pagination-container--page-top" />
                <div className="table-responsive">
                    <Table bordered striped hover>
                        <thead>
                            <tr>
                                <th>{resourceManager.getResourceByKey("GL_HTML_PAGE_TITLE_L")}</th>
                                <th>{resourceManager.getResourceByKey("GL_CREATE_UPDATE_DATE_L")}</th>
                                <th>{resourceManager.getResourceByKey("GL_ACTIONS_L")}</th>
                            </tr>
                        </thead>
                        <tbody>
                            {cmsPages.map((cmsPage) => <PredefinedPagesListUI cmsPage={cmsPage} key={cmsPage.pageID} />)}
                        </tbody>
                    </Table>
                </div>
                <Pagination activePage={model.page} count={model.count} pagesCount={model.getPagesCount()} maxVisiblePage={10} size="sm" onSelect={onPageChange} />
            </>
        }
        else {
            dataResult = <div className="alert alert-dismissible alert-warning fade show">
                <p>{resourceManager.getResourceByKey("GL_NO_DATA_FOUND_L")}</p>
            </div>
        }
    }

    return <>
        <div className="card">
            <div className="col-12">
                <ValidationSummaryErrors asyncErrors={props.asyncErrors} />
            </div>
            <div className="card-body">
                {dataResult}
            </div>
        </div>
    </>

    function onPageChange(page: any): void {
        model.page = page;
        setModel(model);
        props.registerAsyncOperation(cmsDataService.searchCmsPages(model, "bg").then(result => { setCMSPages(result) }));
    }
}

export const PredefinedPagesUI = withAsyncFrame(PredefinedPagesImlp,false);
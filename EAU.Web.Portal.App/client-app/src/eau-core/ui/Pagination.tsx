import { ObjectHelper } from 'cnsys-core';
import { BaseComponent, BaseProps } from 'cnsys-ui-react';
import React from 'react';
import { resourceManager } from '../common/ResourceManager';

interface PaginationProps extends BaseProps {
    activePage: number;
    pagesCount: number;
    count: number;
    maxVisiblePage: number;
    onSelect: (page: number) => void;
    size?: string;
    aditionalCssClass?: string;
}

export class Pagination extends BaseComponent<PaginationProps, any> {
    private firstVisiblePage: number;
    private lastVisiblePage: number;
    private activeElement: any;
    private navElementId: string;

    constructor(props?: PaginationProps) {
        super(props);

        this.activeElement = undefined;
        this.navElementId = ObjectHelper.newGuid();

        this.calcPageWindowLimits = this.calcPageWindowLimits.bind(this);
        this.onClickPaginationLink = this.onClickPaginationLink.bind(this);
        this.componentDidUpdate = this.componentDidUpdate.bind(this);
        this.drawPages = this.drawPages.bind(this);
    }

    render(): JSX.Element {
        if (this.props.pagesCount == 0) return null;

        return (
            <nav id={this.navElementId} className={ObjectHelper.isNullOrUndefined(this.props.aditionalCssClass) ? "pagination-container pagination-container--page-bottom" : "pagination-container " + this.props.aditionalCssClass} aria-label={resourceManager.getResourceByKey("GL_NAVIGATION_PAGE_CHOISE_I")} >
                <div className="result-count">
                    {resourceManager.getResourceByKey("GL_TOTAL_L")}:&nbsp;{this.props.count}
                </div>
                {this.props.pagesCount > 1 ?
                    <ul className="pagination" >
                        {this.drawPages()}
                    </ul>
                    : null}
            </nav>
        );
    }

    componentDidUpdate(prevProps: any) {
        if (this.activeElement) {
            let mustFocuseActiveElement: boolean = !($('#' + this.navElementId).hasClass("pagination-container--page-bottom"));

            if (mustFocuseActiveElement) {
                this.activeElement.focus();
            }
        }
    }

    private onClickPaginationLink(e: any): void {
        let toPage = Number(e.currentTarget.getAttribute('data-go-to-page'));
        this.props.onSelect(toPage);
    }

    private drawPages(): any[] {
        let pageItems: any[] = [];
        let key: number = 0;

        this.calcPageWindowLimits();

        if (this.props.activePage > 1) {
            pageItems.push(
                <li className="page-item first" key={++key}>
                    <button type="button" className="page-link" aria-label={resourceManager.getResourceByKey("GL_SHOW_FIRST_PAGE_I")} data-go-to-page="1" onClick={this.onClickPaginationLink}>
                        <span aria-hidden="true">«</span>
                        <span className="sr-only">{resourceManager.getResourceByKey("GL_SHOW_FIRST_PAGE_I")}</span>
                    </button>
                </li>);

            pageItems.push(
                <li className="page-item prev" key={++key}>
                    <button type="button" className="page-link" aria-label={resourceManager.getResourceByKey("GL_SHOW_PREVIOUS_PAGE_I")} data-go-to-page={(this.props.activePage - 1)} onClick={this.onClickPaginationLink}>
                        <span aria-hidden="true">‹</span>
                        <span className="sr-only">{resourceManager.getResourceByKey("GL_SHOW_PREVIOUS_PAGE_I")}</span>
                    </button>
                </li>);
        } else {
            pageItems.push(
                <li className="page-item first disabled" key={++key}>
                    <button disabled type="button" className="page-link" aria-label={resourceManager.getResourceByKey("GL_SHOW_FIRST_PAGE_I")}>
                        <span aria-hidden="true">«</span>
                        <span className="sr-only">{resourceManager.getResourceByKey("GL_SHOW_FIRST_PAGE_I")}</span>
                    </button>
                </li>);

            pageItems.push(
                <li className="page-item prev disabled" key={++key}>
                    <button disabled type="button" className="page-link" aria-label={resourceManager.getResourceByKey("GL_SHOW_PREVIOUS_PAGE_I")}>
                        <span aria-hidden="true">‹</span>
                        <span className="sr-only">{resourceManager.getResourceByKey("GL_SHOW_PREVIOUS_PAGE_I")}</span>
                    </button>
                </li>);
        }

        for (let i: number = this.firstVisiblePage; i <= this.lastVisiblePage; i++) {
            if (i == this.props.activePage) {
                pageItems.push(
                    <li className="page-item active" key={++key}>
                        <button type="button" className="page-link" ref={el => { this.activeElement = el; }}>
                            <span className="sr-only">{resourceManager.getResourceByKey("GL_CURRENT_PAGE_I")}</span>
                            {i}
                        </button>
                    </li>
                );
            } else {
                pageItems.push(
                    <li className="page-item" key={++key}>
                        <button type="button" className="page-link" data-go-to-page={i} onClick={this.onClickPaginationLink}>
                            <span className="sr-only">{resourceManager.getResourceByKey("GL_SHOW_PAGE_I")}</span>
                            {i}
                        </button>
                        
                    </li>);
            }

        }

        if (this.props.activePage < this.props.pagesCount) {
            pageItems.push(
                <li className="page-item next" key={++key}>
                    <button type="button" className="page-link" aria-label={resourceManager.getResourceByKey("GL_SHOW_NEXT_PAGE_I")} data-go-to-page={(this.props.activePage + 1)} onClick={this.onClickPaginationLink}>
                        <span aria-hidden="true">›</span>
                        <span className="sr-only">{resourceManager.getResourceByKey("GL_SHOW_NEXT_PAGE_I")}</span>
                    </button>
                </li>);

            pageItems.push(
                <li className="page-item last" key={++key}>
                    <button type="button" className="page-link" aria-label={resourceManager.getResourceByKey("GL_SHOW_LAST_PAGE_I")} data-go-to-page={(this.props.pagesCount)} onClick={this.onClickPaginationLink}>
                        <span aria-hidden="true">»</span>
                        <span className="sr-only">{resourceManager.getResourceByKey("GL_SHOW_LAST_PAGE_I")}</span>
                    </button>
                </li>);
        } else {
            pageItems.push(
                <li className="page-item next disabled" key={++key}>
                    <button disabled type="button" className="page-link" aria-label={resourceManager.getResourceByKey("GL_SHOW_NEXT_PAGE_I")}>
                        <span aria-hidden="true">›</span>
                        <span className="sr-only">{resourceManager.getResourceByKey("GL_SHOW_NEXT_PAGE_I")}</span>
                    </button>
                </li>);

            pageItems.push(
                <li className="page-item last disabled" key={++key}>
                    <button disabled type="button" className="page-link" aria-label={resourceManager.getResourceByKey("GL_SHOW_LAST_PAGE_I")}>
                        <span aria-hidden="true">»</span>
                        <span className="sr-only">{resourceManager.getResourceByKey("GL_SHOW_LAST_PAGE_I")}</span>
                    </button>
                </li>);
        }

        return pageItems;
    }

    private calcPageWindowLimits(): void {
        let centerPageWindow: number = (this.props.maxVisiblePage % 2) == 0 ? (this.props.maxVisiblePage / 2) : Math.ceil(this.props.maxVisiblePage / 2);

        if (this.props.activePage <= centerPageWindow) {
            this.firstVisiblePage = 1;
            this.lastVisiblePage = this.props.pagesCount < this.props.maxVisiblePage ? this.props.pagesCount : this.props.maxVisiblePage;
        } else {
            if (this.props.activePage >= this.props.pagesCount - centerPageWindow) {
                this.firstVisiblePage = 1 + (this.props.activePage - centerPageWindow);
                this.lastVisiblePage = this.props.pagesCount;
            } else {
                this.firstVisiblePage = 1 + (this.props.activePage - centerPageWindow);
                this.lastVisiblePage = this.props.pagesCount < this.props.maxVisiblePage ? this.props.pagesCount : this.props.maxVisiblePage + (this.props.activePage - centerPageWindow);
            }
        }
    }
}
import { computed } from 'mobx';
import { PagedDataStateVM } from './PagedDataStateVM';

export class PagedCollection<T>{

    protected items: T[] = [];
    public pagedDataState: PagedDataStateVM;

    constructor(countOfItems: number, items: T[], currentPage?: number, itemsPerPage?: number) {
        this.pagedDataState = new PagedDataStateVM(countOfItems, currentPage, itemsPerPage);
        this.items = items;
    }

    public changeCurrentPage(newPage: number): void {
        this.pagedDataState.currentPage = newPage;
    }

    @computed get currentPage(): number {
        return this.pagedDataState.currentPage;
    }
}
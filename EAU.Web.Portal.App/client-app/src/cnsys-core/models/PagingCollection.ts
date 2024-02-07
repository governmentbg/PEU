import { computed } from 'mobx';
import { PagedCollection } from './PagedCollection';

export class PagingCollection<T> extends PagedCollection<T>{

    public getPagesCount(): number {
        return Math.floor((this.pagedDataState.countOfItems + this.pagedDataState.itemsPerPage - 1) / this.pagedDataState.itemsPerPage);
    }

    @computed get getCurrentItems(): T[] {
        let countOfSkippedItems = ((this.pagedDataState.currentPage - 1) * this.pagedDataState.itemsPerPage);
        let items: T[] = [];
        this.items.forEach((item) => items.push(item));

        return items.splice(countOfSkippedItems, this.pagedDataState.itemsPerPage);
    }
}
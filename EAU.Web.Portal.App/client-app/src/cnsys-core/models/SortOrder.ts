import { TypeSystem } from '../common/TypeSystem';
import { moduleContext } from '../ModuleContext';

export enum SortOrderVM {

    //Неподреден
    Unordered = 1,

    //Възходящ
    Asc = 2,

    //Низходящ
    Desc = 3
}
TypeSystem.registerEnumInfo(SortOrderVM, 'SortOrderVM', moduleContext.moduleName)
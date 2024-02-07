import { moduleContext, TypeSystem } from 'cnsys-core';

export enum TreeViewModes {
    DisplayTree = 0,
    MultiSelectTree = 1,
    SingleSelectTree = 2,
}
TypeSystem.registerEnumInfo(TreeViewModes, 'TreeViewModes', moduleContext.moduleName);
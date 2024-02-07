import { TypeSystem } from '../common/TypeSystem';
import { moduleContext } from '../ModuleContext';
import { BaseDataModel } from './BaseDataModel';

@TypeSystem.typeDecorator("SelectListItem", moduleContext.moduleName)
export class SelectListItem extends BaseDataModel {
    private _selected: boolean = false;
    private _text?: string = undefined;
    private _value?: string = undefined;

    @TypeSystem.propertyDecorator('boolean')
    get selected(): boolean {
        return this._selected;
    }
    set selected(value: boolean) {
        this._selected = value;
    }

    @TypeSystem.propertyDecorator('string')
    get text(): string | undefined {
        return this._text;
    }
    set text(value: string | undefined) {
        this._text = value;
    }

    @TypeSystem.propertyDecorator('string')
    get value(): string | undefined {
        return this._value;
    }
    set value(value: string | undefined) {
        this._value = value;
    }

    constructor(obj?: any) {
        super(obj);

        if (obj) {
            this.selected = obj['selected'];
            this.text = obj['text'];
            this.value = obj['value'];
        }
    }

}
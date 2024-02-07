import * as React from "react";
import { action, observable, runInAction } from 'mobx';
import { observer } from "mobx-react";
import "jquery";
import { ObjectHelper, ArrayHelper } from 'cnsys-core';
import { BaseFieldComponent, BaseFieldProps } from 'cnsys-ui-react';
import { moduleContext } from "../../ModuleContext";
import { withSimpleErrorLabel } from "../WithSimpleErrorLabel";

export interface IAutoCompleteItem {
    id: (string | number);
    text: string;
    optionText?: string;
    callbackData?: any;
}

interface AutoCompleteUIProps extends BaseFieldProps {
    triggerLength?: number;
    placeholder?: string;
    dataSourceSearchDelegat: (val: string) => Promise<IAutoCompleteItem[]>;
    onChangeCallback?: (selectedItem: IAutoCompleteItem) => void;
}

@observer class AutoCompleteUIImpl extends BaseFieldComponent<AutoCompleteUIProps> {
    @observable private selectOptions: IAutoCompleteItem[];
    @observable private tbValue: string;
    @observable private isModelSet: boolean;
    @observable private newSelectedOptIdx: number;

    private mustUpdateScroll: boolean;
    private autoCompleteOptLst: HTMLUListElement;
    private id: string;
    private ulId: string;

    constructor(props: AutoCompleteUIProps, context?: any) {
        super(props, context);

        //Bind
        this.onTextChange = this.onTextChange.bind(this);
        this.onTbFocus = this.onTbFocus.bind(this);
        this.onAutoCompleteBlure = this.onAutoCompleteBlure.bind(this);
        this.onAutoCompleteItemClick = this.onAutoCompleteItemClick.bind(this);
        this.onKayHandle = this.onKayHandle.bind(this);
        this.autoCompletOptLstSetter = this.autoCompletOptLstSetter.bind(this);
        this.setModel = this.setModel.bind(this);
        this.autoCompleteMouseOver = this.autoCompleteMouseOver.bind(this);
        this.componentDidUpdate = this.componentDidUpdate.bind(this);

        //Init
        let modelVal = this.props.modelReference.getValue();
        this.id = ObjectHelper.newGuid();
        this.ulId = `ul_${this.id}`;
        this.isModelSet = ObjectHelper.isStringNullOrEmpty(modelVal) ? false : true;
        this.newSelectedOptIdx = 0;
        this.selectOptions = [];
        this.tbValue = '';
        this.mustUpdateScroll = false;


        if (this.isModelSet) {
            this.props.dataSourceSearchDelegat('').then(items => {
                if (items && items.length > 0) {
                    let selectedItem = ArrayHelper.queryable.from(items).single(item => item.id.toString() == (modelVal as string));

                    this.tbValue = selectedItem.text;
                }
            });
        }
    }

    componentDidMount() {
        document.addEventListener('click', this.onAutoCompleteBlure, true);
    }

    componentWillUnmount() {
        document.removeEventListener('click', this.onAutoCompleteBlure, true);
    }

    componentDidUpdate() {
        //super.componentDidUpdate();

        if (this.selectOptions && this.selectOptions.length > 0 && this.mustUpdateScroll == true)
            $(this.autoCompleteOptLst).find('li.auto-complete-active-option').first().get(0).scrollIntoView(false);
    }

    renderInternal(): JSX.Element {
        let that = this;
        let activeDecendant = this.newSelectedOptIdx >= 0 && this.selectOptions && this.selectOptions.length > 0 ? this.selectOptions[this.newSelectedOptIdx].id.toString() : null;
        return (
            <div className="auto-complete-container" id={this.id}>
                <input
                    type="text"
                    placeholder={this.props.placeholder ? this.props.placeholder : ""}
                    onKeyDown={this.onKayHandle}
                    onChange={this.onTextChange}
                    onFocus={this.onTbFocus}
                    value={this.isModelSet === true && ObjectHelper.isNullOrUndefined(this.props.modelReference.getValue()) ? '' : this.tbValue}
                    name={this.props.modelReference ? this.getName() : ""}
                    id={this.getId()}
                    autoComplete="off"
                    aria-autocomplete="list"
                    aria-controls={this.ulId}
                    role="combobox"
                    aria-expanded={this.selectOptions && this.selectOptions.length > 0}
                    aria-owns={this.ulId}
                    aria-haspopup="listbox"
                    aria-activedescendant={activeDecendant}
                    {...this.fieldAttributes} />
                {this.selectOptions && this.selectOptions.length > 0 ?
                    <>
                        <ul id={this.ulId} className="auto-complete-options" ref={this.autoCompletOptLstSetter} role="listbox">
                            {this.selectOptions.map((item, i: number) => {
                                let optCss: string = this.newSelectedOptIdx == i ? "auto-complete-active-option" : "";

                                return (
                                    <li className={optCss}
                                        id={item.id.toString()}
                                        key={item.id}
                                        data-key={item.id}
                                        onMouseOver={that.autoCompleteMouseOver}
                                        onClick={that.onAutoCompleteItemClick}
                                        role="option"
                                        aria-selected={this.newSelectedOptIdx == i}>
                                        {ObjectHelper.isStringNullOrEmpty(item.optionText) ? item.text : item.optionText}
                                    </li>);
                            })}
                        </ul>
                        <div aria-live="polite" aria-atomic="true" role="status" className="sr-only">
                            {moduleContext.resourceManager.getResourceByKey('GL_AUTOCOMPLETE_SRONLY_RESULT_I').replace('<Param1>', this.selectOptions.length.toString())}
                        </div>
                    </>
                    : null}

            </div>);
    }

    @action onKayHandle(e: any): void {
        let key = e.keyCode || e.charCode;

        if (this.selectOptions && this.selectOptions.length > 0) {
            if (key == 9) {
                /* ====== tab click ====== */
                if (!ObjectHelper.isNullOrUndefined(this.newSelectedOptIdx)) {
                    if (!this.isModelSet) {
                        this.tbValue = '';
                    }
                    this.newSelectedOptIdx = undefined;
                    this.selectOptions = [];
                }
                return;
            }

            if (key == '13') {
                /* ====== enter click ====== */
                if (!ObjectHelper.isNullOrUndefined(this.newSelectedOptIdx)) {
                    let selectedOpt: IAutoCompleteItem = this.selectOptions[this.newSelectedOptIdx];

                    this.setModel(selectedOpt);
                }
                return;
            }

            if (key == 38 || key == 40) {
                /* ====== arrow click ====== */
                if (this.selectOptions && this.selectOptions.length > 1) {
                    if (!this.newSelectedOptIdx) this.newSelectedOptIdx = 0;

                    if (key == 38) {
                        //up
                        this.newSelectedOptIdx = this.newSelectedOptIdx == 0 ? 0 : (this.newSelectedOptIdx - 1);
                    } else {
                        //down
                        this.newSelectedOptIdx = this.newSelectedOptIdx == (this.selectOptions.length - 1) ? (this.selectOptions.length - 1) : (this.newSelectedOptIdx + 1);
                    }

                    this.mustUpdateScroll = this.newSelectedOptIdx >= 2;
                }

                return;
            }
        }
    }

    @action onTextChange(e: any): void {
        let text: string = e.target.value;
        this.tbValue = text;
        this.isModelSet = false;
        this.mustUpdateScroll = false;
        this.props.modelReference.setValue(undefined);
        if (this.props.onChangeCallback) {
            this.props.onChangeCallback(null);
        }

        if (ObjectHelper.isStringNullOrEmpty(this.tbValue)) {
            this.newSelectedOptIdx = undefined;
            this.selectOptions = [];
        }

        if (ObjectHelper.isNullOrUndefined(this.props.triggerLength) || this.props.triggerLength == 0 || text.length >= this.props.triggerLength) {
            this.props.dataSourceSearchDelegat(text).then(res => {
                if (res) {
                    runInAction(() => {
                        this.selectOptions = res;

                        if (this.selectOptions && this.selectOptions.length > 0) {
                            this.newSelectedOptIdx = 0;
                            this.mustUpdateScroll = false;
                        }
                    });
                }
            });
        }
    }

    @action onTbFocus(e: any): void {
        let searchValue = this.isModelSet === true && ObjectHelper.isNullOrUndefined(this.props.modelReference.getValue()) ? '' : this.tbValue;

        if ((this.isModelSet == false && (ObjectHelper.isNullOrUndefined(this.props.triggerLength) || this.props.triggerLength == 0))
            ||
            (this.isModelSet === true && ObjectHelper.isNullOrUndefined(this.props.modelReference.getValue()))) {
            this.props.dataSourceSearchDelegat(searchValue).then(res => {
                if (res) {
                    this.selectOptions = res;
                    this.newSelectedOptIdx = 0;
                }
            });
        }
    }

    @action onAutoCompleteBlure(e: any): void {
        if ($(e.target).parent(`#${this.id}`).length == 0 && $(e.target).parent(`#${this.ulId}`).length == 0) {
            if (!this.isModelSet) {
                this.tbValue = '';
            }

            this.selectOptions = [];
            this.newSelectedOptIdx = undefined;
        }
    }

    @action onAutoCompleteItemClick(e: any): void {
        let li = $(e.currentTarget);
        let selectedId = ($(li).data('key') as string);
        let selectedOpt = ArrayHelper.queryable.from(this.selectOptions).single(o => o.id.toString() == selectedId);;

        this.setModel(selectedOpt);
    }

    autoCompletOptLstSetter(el: HTMLUListElement) {
        this.autoCompleteOptLst = el;
    }

    @action private setModel(selectedItem: IAutoCompleteItem): void {
        this.newSelectedOptIdx = undefined;
        this.props.modelReference.setValue(ObjectHelper.isNullOrUndefined(selectedItem) ? undefined : selectedItem.id);
        this.tbValue = ObjectHelper.isNullOrUndefined(selectedItem) ? '' : selectedItem.text;
        this.selectOptions = [];
        this.isModelSet = ObjectHelper.isNullOrUndefined(selectedItem) ? false : true;

        if (this.props.onChangeCallback) {
            this.props.onChangeCallback(ObjectHelper.isNullOrUndefined(selectedItem) ? null : selectedItem);
        }
    }

    @action autoCompleteMouseOver(e: any): void {
        if (this.selectOptions && this.selectOptions.length > 0) {
            let li = $(e.currentTarget);
            let itemId: any = li.data('key');

            for (let i: number = 0; i < this.selectOptions.length; i++) {
                if (this.selectOptions[i].id.toString() == (itemId as string)) {
                    this.newSelectedOptIdx = i;
                    return;
                }
            }
        }
    }
}

export const AutoCompleteUI = withSimpleErrorLabel(AutoCompleteUIImpl);
import { ObjectHelper } from 'cnsys-core';
import { BaseProps } from 'cnsys-ui-react';
import { EAUBaseComponent } from 'eau-core';
import { computed } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';

export interface CollectionItemsProps {
    initItem: () => any;
    addButtonLabelKey?: string;
    skipButton?: boolean;
    showDeleteButtonOnSingleElement?: boolean;
}

export function withCollectionItems(Component: any, props?: CollectionItemsProps) {

    @observer class Wrapper extends EAUBaseComponent<BaseProps, any>{
        private newItemAdded: boolean;
        private collectionId: string;

        @computed get items(): any[] {
            return this.model;
        }

        constructor(props?: BaseProps) {
            super(props);

            //Init
            this.newItemAdded = false;
            this.collectionId = ObjectHelper.newGuid();
            this.addItem = this.addItem.bind(this);
        }

        componentDidUpdate() {
            if (this.items && this.items.length > 0 && this.newItemAdded === true) {
                this.newItemAdded = false;
                let controlsInLastAddedItem = $(`#${this.collectionId}-${this.items.length - 1}`).find('input, textarea, select, button');
                if (controlsInLastAddedItem.length > 0)
                    controlsInLastAddedItem[0].focus();
            }
        }

        //#region renders 

        renderEdit(): JSX.Element {

            if (this.items && this.items.length > 0) {

                var renderItems: any[] = [];

                var allProps = { ...props, ...this.props }

                for (var i = 0; i < this.items.length; i++) {
                    this.renderItem(renderItems, i);
                }

                return <>
                    <ul className="list-filed">{renderItems.map((item, index) => {
                        return (
                            <li className="list-filed__item" role="group" key={ObjectHelper.newGuid()}>
                                <div id={`${this.collectionId}-container-${index}`} className="interactive-container interactive-container--form">
                                    <div id={renderItems.length > 1 ? `${this.collectionId}-${index}` : null} className="interactive-container__content record-container">
                                        {item}
                                    </div>
                                    <div className="interactive-container__controls">
                                        {
                                            (renderItems.length > 1 || props.showDeleteButtonOnSingleElement === true)
                                                ? <button title={this.getResource("GL_DELETE_L")} className="btn btn-light btn-sm"
                                                    onMouseOver={this.onHover.bind(this, index)}
                                                    onMouseLeave={this.onHoverLeave.bind(this, index)}
                                                    onFocus={this.onHover.bind(this, index)}
                                                    onBlur={this.onHoverLeave.bind(this, index)}
                                                    onClick={this.removeItem.bind(this, index)}>
                                                    <i className="ui-icon ui-icon-times" aria-hidden="true"></i>
                                                    <span className="d-none">{this.getResource("GL_DELETE_L")}</span>
                                                </button>
                                                : null
                                        }
                                    </div>
                                </div>
                            </li>);
                    })}
                    </ul>
                    {props.skipButton !== true && <div className="row">
                        <div className="form-group col">
                            <hr aria-hidden="true" />
                            <button className="btn btn-light" onClick={this.addItem.bind(this)} type="button">
                                <i className="ui-icon ui-icon-plus mr-1" aria-hidden="true"></i>
                                {allProps.addButtonLabelKey ? this.getResource(allProps.addButtonLabelKey) : this.getResource("GL_ADD_L")}
                            </button>
                        </div>
                    </div>}
                </>
            }

            return null;
        }

        renderDisplay(): JSX.Element {
            if (this.items && this.items.length > 0) {

                var renderItems: any[] = [];

                for (var i = 0; i < this.items.length; i++) {
                    this.renderItem(renderItems, i);
                }

                return <ul className="list-filed">
                    {renderItems.map((item, index) => <li className="list-filed__item" role="group" key={ObjectHelper.newGuid()}>{item}</li>)}
                </ul>
            }

            return null;
        }

        renderItem(uiElements: JSX.Element[], index: number) {
            /** тук има особеност, че при изтриване на елемент, който е по средата на списъка, трябва да се преизчертаят всичките компоненти преди него заради достъпа през BindableReference */
            var itemProps = this.bindArrayElement(m => m[index], [index]);
            var allProps = { ...props, ...this.props, ...itemProps }

            uiElements.push(<Component {...allProps} />)
        }

        //#endregion

        //#region collection items helpers

        private addItem() {
            let newItem = props.initItem();

            this.newItemAdded = true;
            this.items.push(newItem);
        }

        private removeItem(index: number) {
            if (index >= 0)
                this.items.splice(index, 1);
        }

        private onHover(index: number) {
            $(`#${this.collectionId}-container-${index}`).addClass("interactive-container--focus");
        }

        private onHoverLeave(index: number) {
            $(`#${this.collectionId}-container-${index}`).removeClass("interactive-container--focus");
        }

        //#endregion
    }

    return Wrapper as any;
}
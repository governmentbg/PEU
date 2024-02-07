import { SelectListItem, ObjectHelper } from 'cnsys-core';
import { BaseFieldComponent, BaseFieldProps } from 'cnsys-ui-react';
import { appConfig } from 'eau-core';
import { observer } from 'mobx-react';
import React from 'react';
import tinymce from "tinymce";
// Import bg localization
import 'tinymce-i18n/langs5/bg_BG';
import 'tinymce/icons/default/icons.js';
// Import plugins
import 'tinymce/plugins/code';
import 'tinymce/plugins/image';
import 'tinymce/plugins/link';
import 'tinymce/plugins/lists';
import 'tinymce/plugins/table';
import 'tinymce/skins/ui/oxide/content.inline.min.css';
// Import skin
import 'tinymce/skins/ui/oxide/skin.min.css';
import 'tinymce/themes/silver';
import { resourceManager } from '../../common/ResourceManager';
import { withSimpleErrorLabel } from '../WithSimpleErrorLabel';

interface TextEditorProps extends BaseFieldProps {
    /**Активните полета трябва да се подават заредени. Асинхронното зареждане не обновява текстовия едитор. */
    activeFields?: SelectListItem[];
}

@observer export class TextEditorImpl extends BaseFieldComponent<TextEditorProps> {

    private editorId: string;
    private toolbar: string;
    private plugins: string;

    constructor(props: TextEditorProps) {
        super(props);

        this.onChange = this.onChange.bind(this);

        this.init();
        this.initCustomPlugins()
    }

    componentWillUnmount() {
        tinymce.remove(this.editorId);
    }

    componentDidMount() {
        let that = this;
        let lang = appConfig.clientLanguage == '' ? 'bg' : appConfig.clientLanguage;

        tinymce.init({
            statusbar: false,
            menubar: false,
            forced_root_block: false,
            plugins: this.plugins,
            toolbar: this.toolbar,
            selector: `#${this.editorId}`,
            language: lang ? 'bg_BG' : '',
            setup: editor => {
                editor.on('keyup change', () => {
                    that.handleChange(editor?.getContent())
                })
            }
        });
    }

    renderInternal() {
        return (<textarea value={this.props.modelReference.getValue() || ""}
            onChange={this.onChange}
            name={this.getName()}
            id={this.editorId}
            {...this.fieldAttributes} />)
    }

    onChange() {
        //За да не дава warning-и
        //Event-а се прихваща в init -> setup
    }

    //override - за да предаде директно стойността, а не event-a
    getHandleChangeValue(value: any) {
        return value;
    }

    //#region init

    private init() {
        this.editorId = ObjectHelper.newGuid();
        this.plugins = 'code image link lists table activeFields';
        this.toolbar = 'undo redo | styleselect | bold italic underline | link code | align | bullist numlist table';

        if (this.props.activeFields && this.props.activeFields.length > 0)
            this.toolbar += ' activeFields'
    }

    //#endregion

    //#region Custom plugins

    private initCustomPlugins(): void {

        var items = [];

        this.props.activeFields && this.props.activeFields.forEach(field => {
            items.push({ text: field.text, value: field.value })
        })

        tinymce.PluginManager.add('activeFields', (editor, url) => {
            var openDialog = function () {
                return editor.windowManager.open({
                    title: resourceManager.getResourceByKey('GL_ADD_ACTIVE_FIELD_L'),
                    body: {
                        type: 'panel',
                        items: [
                            { type: 'selectbox', name: 'type', items: items }
                        ],
                    },
                    buttons: [
                        { type: 'cancel', text: resourceManager.getResourceByKey("GL_CANCEL_L") },
                        { type: 'submit', text: resourceManager.getResourceByKey("GL_ADD_L"), primary: true }
                    ],
                    onSubmit: (api) => {
                        let submitedData: any = api.getData();

                        editor.insertContent(submitedData.type);// insert markup
                        api.close(); // close the dialog
                    },
                    size: "medium"
                });
            };

            // Add a button that opens a window
            editor.ui.registry.addButton('activeFields', {
                tooltip: resourceManager.getResourceByKey('GL_ACTIVE_FIELD_L'),
                icon: 'comment-add',
                onAction: () => openDialog()
            });
        })
    }

    //#endregion
}

export const TextEditorUI = withSimpleErrorLabel(TextEditorImpl);
import React from "react";
import { observer } from "mobx-react";
import {
    Constants as ConstantsEAU,
    DocumentType,
    EAUBaseComponent,
    Nomenclatures,
    ServiceSearchCriteria
} from "eau-core";
import {AsyncUIProps, BaseProps, IMultipleSelectListItem, withAsyncFrame} from "cnsys-ui-react";
import {ObjectHelper, SelectListItem} from "cnsys-core";
import CardFooterUI from "../../common/CardFooterUI";
import {observable, runInAction} from "mobx";

interface ServicesSearchFormProps extends BaseProps, AsyncUIProps {
    selectedGroupItems: SelectListItem[];
    searchServices: (serviceSearchCriteria: ServiceSearchCriteria) => void;
}

@observer
class ServicesSearchFormUIImpl extends EAUBaseComponent<ServicesSearchFormProps, ServiceSearchCriteria> {

    private selectedStatusItems: SelectListItem[];
    private documentTypes: DocumentType[];

    @observable private selectedDocumentTypes: IMultipleSelectListItem[];

    constructor(props: ServicesSearchFormProps) {
        super(props);
        this.onRadioChange = this.onRadioChange.bind(this);
        this.search = this.search.bind(this);
        this.clear = this.clear.bind(this);
        this.getModelDocumentTypes = this.getModelDocumentTypes.bind(this);
        this.handleDocumentTypesChange = this.handleDocumentTypesChange.bind(this);
        this.init();
    }

    render() {
        return <>
            <div className="card-header">
                <h3>{this.getResource("GL_SEARCH_TITLE_L")}</h3>
            </div>
            <div className="card-body">
                    <div className="row">
                        <div className="form-group col-md-8">
                            {this.labelFor(m => m.name, "GL_DESIGNATION_L")}
                            {this.textBoxFor(m => m.name)}
                        </div>
                        <div className="form-group col-md-4">
                            {this.labelFor(m => m.isActive, "GL_STATUS_L")}
                            <div className="form-inline">
                                {this.radioButtonListFor(m => m.isActive, this.selectedStatusItems, { className: "custom-control-inline custom-control custom-radio" }, this.onRadioChange)}
                            </div>
                        </div>
                    </div>
                    <div className="row">
                        <div className="form-group col-md-4">
                            {this.labelFor(m => m.groupID, "GL_SERVICE_GROUP_NAME_L")}
                            {this.dropDownListFor(m => m.groupID, this.props.selectedGroupItems, null, null, true, this.getResource("GL_ALL_L"))}
                        </div>
                        <div className="form-group col-md-4">
                            {this.labelFor(m => m.attachedDocumentType.name, "GL_DOCUMENT_KIND_L")}
                            {!ObjectHelper.isArrayNullOrEmpty(this.selectedDocumentTypes) && this.model.attachedDocumentType && this.selectFor(m => m.attachedDocumentType.documentTypeID, this.selectedDocumentTypes, this.getModelDocumentTypes(), null, this.getResource("GL_ALL_L"), this.handleDocumentTypesChange, false)}
                        </div>
                        <div className="form-group col-md-4 col-xxl-3 ">
                            {this.labelFor(m => m.sunauServiceUri, "GL_URI_ADM_SERVICE_L")}
                            {this.textBoxFor(m => m.sunauServiceUri)}
                        </div>
                    </div>
            </div>
                <CardFooterUI onClear={this.clear} onSearch={this.search} />
        </>;
    }

    private getModelDocumentTypes(): IMultipleSelectListItem[] {
        if (!ObjectHelper.isNullOrUndefined(this.model.attachedDocumentType) && this.model.attachedDocumentType.documentTypeID) {
            return this.documentTypes.filter(d => this.model.attachedDocumentType.documentTypeID == d.documentTypeID).map(d => {
                return { label: d.name, value: d.documentTypeID.toString() };
            })
        }
    }

    private handleDocumentTypesChange(documentType: IMultipleSelectListItem) {
        if (documentType) {
            let currentDocumentType = this.documentTypes.filter(d => documentType.value == d.documentTypeID.toString())[0];

            this.model.attachedDocumentType.documentTypeID = currentDocumentType.documentTypeID;
            this.model.attachedDocumentType.type = currentDocumentType.type;
            this.model.attachedDocumentType.name = currentDocumentType.name;
            this.model.attachedDocumentType.uri = currentDocumentType.uri;
        } else {
            this.model.attachedDocumentType = new DocumentType();
        }
    }

    private getDocumentTypes() {
        this.props.registerAsyncOperation(Nomenclatures.getDocumentTypes().bind(this).then(documentTypes => {
            runInAction.bind(this)(() => {
                this.documentTypes = documentTypes.filter(docType => docType.type == ConstantsEAU.ATTACHABLE_DOCUMENT);

                this.documentTypes.forEach(documentType => {
                    this.selectedDocumentTypes.push({ label: documentType.name, value: documentType.documentTypeID.toString() });
                })
            })

        }))
    }

    public init() {
        this.selectedStatusItems = [
            new SelectListItem({ value: undefined, text: this.getResource("GL_ALL_L"), selected: this.model.isActive == undefined }),
            new SelectListItem({ value: true, text: this.getResource("GL_ACTIVE_L"), selected: this.model.isActive == true }),
            new SelectListItem({ value: false, text: this.getResource("GL_INACTIVE_L"), selected: this.model.isActive == false })
        ];
        this.selectedDocumentTypes = [];

        this.getDocumentTypes();
    }

    private onRadioChange(e: any): void {
        let state = e.target.value;

        if (state == "true") {
            this.model.isActive = true;
        } else if (state == "false") {
            this.model.isActive = false;
        } else {
            this.model.isActive = undefined;
        }
    }

    private search() {
        this.props.searchServices(this.model);
    }


    private clear() {
        this.model.name = "";
        this.model.groupID = null;
        this.model.isActive = null;
        this.model.attachedDocumentType.documentTypeID = null;
        this.model.attachedDocumentType.type = null;
        this.model.attachedDocumentType.name = null;
        this.model.attachedDocumentType.uri = null;
        this.model.sunauServiceUri = "";

        this.search();
    }
}

export const ServicesSearchFormUI = withAsyncFrame(ServicesSearchFormUIImpl, false);
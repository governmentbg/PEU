import { ObjectHelper, UrlHelper } from "cnsys-core";
import { AsyncUIProps, BaseProps, withAsyncFrame, withRouter } from 'cnsys-ui-react';
import { Constants, EAUBaseComponent, resourceManager } from 'eau-core';
import React from 'react';
import { Link } from 'react-router-dom';
import { CaseFileInfo } from "../../models/ModelsManualAdded";
import { ServiceInstancesDataService } from "../../services/ServiceInstancesDataService";

interface MyEServiceInstanceDocumentsUIProps extends BaseProps, AsyncUIProps {
    eServicesInstanceDocuments: CaseFileInfo;
    caseFileURI: string;
    serviceInstanceId: number;
}

class MyEServiceInstanceDocumentsUI extends EAUBaseComponent<MyEServiceInstanceDocumentsUIProps, CaseFileInfo>{

    render() {

        let dataResultRow: any = null;

        if (!ObjectHelper.isNullOrUndefined(this.props.eServicesInstanceDocuments)) {

            dataResultRow = <fieldset className="fields-group fields-group--no-border">
                <legend>
                    <h3 className="section-title">{this.getResource("GL_DOCUMENTS_L")}</h3>
                </legend>
                <div className="table-responsive-block">
                    <table className="table table-hover">
                        <thead className="">
                            <tr>
                                <th className="w-60">{this.getResource("GL_DESIGNATION_L")}</th>
                                <th>{this.getResource("GL_URI_L")}</th>
                                <th>{this.getResource("GL_CREATE_DATE_L")}</th>
                                <th className="text-right">{this.getResource("GL_ACTIONS_L")}</th>
                            </tr>
                        </thead>
                        <tbody>
                            {
                                this.props.eServicesInstanceDocuments.documents.length > 0
                                    ? this.props.eServicesInstanceDocuments.documents.map((document, indexDocument) => {

                                        let documentURI = document.documentURI.registerIndex + '-' + document.documentURI.sequenceNumber + '-' + document.documentURI.receiptOrSigningDate.format(Constants.DATE_FORMATS.date).substring(0, 10).toString();

                                        return <tr key={indexDocument}>
                                            <th>
                                                <p className="th-title d-sm-none">{this.getResource("GL_DOCUMENTS_L")}</p>
                                                <p className="td-text"><b>{document.documentTypeName}</b></p>
                                            </th>
                                            <td>
                                                <p className="th-title d-sm-none">{this.getResource("GL_URI_L")}</p>
                                                <p className="td-text">{documentURI}</p>
                                            </td>
                                            <td>
                                                <p className="th-title d-sm-none">{this.getResource("GL_CREATE_DATE_L")}</p>
                                                <p className="td-text">{document.creationTime.format(Constants.DATE_FORMATS.date)}</p>
                                            </td>
                                            <td className="actions-td">
                                                <p className="th-title d-sm-none">{this.getResource("GL_ACTIONS_L")}</p>
                                                <Link to={Constants.PATHS.ServiceInstanceDocumentPreview.replace(':caseFileURI', this.props.caseFileURI).replace(':documentURI', documentURI)} title={resourceManager.getResourceByKey("GL_PAGE_OPEN_IN_NEW_TAB_L")} target="_blank">
                                                    {this.getResource('GL_PREVIEW_L')}
                                                </Link>
                                                <a href="#" onClick={e => this.downloadDocument(e, this.props.serviceInstanceId, documentURI)}>{this.getResource('GL_DOWNLOAD_DOCUMENT_L')}</a>
                                            </td>
                                        </tr>
                                    })
                                    : null
                            }
                        </tbody>
                    </table>
                </div>
            </fieldset>
        }

        return <>
            {dataResultRow}
        </>
    }

    private downloadDocument(e: any, serviceInstanceId: number, documentURI: string) {
        e.preventDefault();

        let downloadUrl = new ServiceInstancesDataService().getServiceInstancesDocumentDownloadUrl(serviceInstanceId, documentURI)

        UrlHelper.downloadFile(downloadUrl);
    }
}

export default withRouter(withAsyncFrame(MyEServiceInstanceDocumentsUI, false));
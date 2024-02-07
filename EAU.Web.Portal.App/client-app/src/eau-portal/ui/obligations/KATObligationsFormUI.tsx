import { BindableReference, SelectListItem } from 'cnsys-core';
import { BaseProps } from 'cnsys-ui-react';
import { appConfig, attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, EAUBaseComponent, KATDocumentTypes } from 'eau-core';
import { action } from 'mobx';
import { observer } from 'mobx-react';
import React from 'react';
import { ANDObligationSearchCriteria } from '../../models/ModelsManualAdded';

interface KATObligationsFormUIProps extends BaseProps {
    modelReference: BindableReference;
}

@observer export class KATObligationsFormUI extends EAUBaseComponent<KATObligationsFormUIProps, ANDObligationSearchCriteria>{

    private allowedDocumenSeries: SelectListItem[] = [];

    constructor(props: KATObligationsFormUIProps) {
        super(props);

        //Bind
        this.onKATDocumentTypeChange = this.onKATDocumentTypeChange.bind(this);

        if (appConfig.possibleKATObligationsFishSeries && appConfig.possibleKATObligationsFishSeries.length > 0) {
            appConfig.possibleKATObligationsFishSeries.forEach(element => {
                this.allowedDocumenSeries.push(new SelectListItem({ text: element, value: element }))
            });
        }
    }

    @action private onKATDocumentTypeChange(e: any) {
        this.model.documentType = e.target.value

        if (e.target.value == KATDocumentTypes.PENAL_DECREE || e.target.value == KATDocumentTypes.AGREEMENT) {
            this.model.documentSeries = null;
        }

        this.model.clearErrors(true);
    }

    render() {
        return <fieldset className="fields-group">
            <legend>
                <h3 className="field-title">{this.getResource("GL_DATA_FOR_CHECK_L")}</h3>
            </legend>
            <div className="alert alert-info" role="alert" dangerouslySetInnerHTML={{ __html: this.getResource("GL_PP_OBLIGATION_RESULT_I") }}></div>
            <div className="row">
                <div className="form-group col-12">
                    <fieldset>
                        <legend className="form-control-label">{this.getResource('GL_OBLIGATION_DOCUMENT_TYPE_L')}</legend>
                        <div className="custom-control custom-radio">
                            <input className="custom-control-input" onChange={this.onKATDocumentTypeChange} name="RADIO_A" id="documentType_ticket"
                                value={KATDocumentTypes.TICKET}
                                checked={this.model.documentType == KATDocumentTypes.TICKET} type="radio" />
                            <label className="custom-control-label" htmlFor="documentType_ticket">{this.getResource('GL_TICKET_L')}</label>
                        </div>

                        <div className="custom-control custom-radio">
                            <input className="custom-control-input" onChange={this.onKATDocumentTypeChange} name="RADIO_A" id="documentType_penal_decree"
                                value={KATDocumentTypes.PENAL_DECREE}
                                checked={this.model.documentType == KATDocumentTypes.PENAL_DECREE} type="radio" />
                            <label className="custom-control-label" htmlFor="documentType_penal_decree">{this.getResource('GL_PENAL_DECREE_L')}</label>
                        </div>

                        <div className="custom-control custom-radio">
                            <input className="custom-control-input" onChange={this.onKATDocumentTypeChange} name="RADIO_A" id="documentType_agreement"
                                value={KATDocumentTypes.AGREEMENT}
                                checked={this.model.documentType == KATDocumentTypes.AGREEMENT} type="radio" />
                            <label className="custom-control-label" htmlFor="documentType_agreement">{this.getResource("GL_AGREEMENT_L")}</label>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div className="row">
                {
                    this.model.documentType == KATDocumentTypes.TICKET
                        ? <div className="form-group col-4 col-sm-3 col-md-2" id="DATA_0">
                            {this.labelFor(m => m.documentSeries, "GL_DOCUMENT_SERIES_L", attributesClassFormControlRequiredLabel)}
                            {this.dropDownListFor(m => m.documentSeries, this.allowedDocumenSeries, null, null, true, this.getResource("GL_CHOICE_L"))}
                        </div>
                        : null
                }
                <div className="form-group col-sm-4 col-md-5 col-lg-3" id="DATA_1">
                    {this.labelFor(m => m.documentNumber, "GL_DOCUMENT_NUMBER_L", attributesClassFormControlRequiredLabel)}
                    {this.textBoxFor(m => m.documentNumber, attributesClassFormControlReqired)}
                </div>
                <div className="form-group col-sm-5 col-lg-3" id="DATA_2">
                    {this.labelFor(m => m.amount, "GL_AMOUNT_BGN_L", attributesClassFormControlRequiredLabel)}
                    {this.amountFor(m => m.amount, attributesClassFormControlReqired)}
                </div>
            </div>
        </fieldset>
    }
}
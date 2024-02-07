import { BaseProps } from 'cnsys-ui-react';
import { attributesClassFormControlReqired, attributesClassFormControlRequiredLabel, EAUBaseComponent } from 'eau-core';
import React from 'react';
import { ANDObligationSearchCriteria } from '../../models/ModelsManualAdded';

interface ObligationsForObligatedEntityUIProps extends BaseProps {  }

export class ObligationsForObligatedEntityUI extends EAUBaseComponent<ObligationsForObligatedEntityUIProps, ANDObligationSearchCriteria>{

    render() {

        return <div className="row">
            <div className="form-group col-md-6 col-xl-4">
                {this.labelFor(m => m.uic, "GL_UIC_BULSTAT_L", attributesClassFormControlRequiredLabel)}
                {this.textBoxFor(m => m.uic, attributesClassFormControlReqired)}
            </div>
            <div className="form-group col-md-6 col-xl-4">
                {this.labelFor(m => m.obligedPersonIdent, "GL_PERSON_ID_L", attributesClassFormControlRequiredLabel)}
                {this.textBoxFor(m => m.obligedPersonIdent, attributesClassFormControlReqired)}
                {this.inlineHelpFor(m => m.obligedPersonIdent, 'GL_PersonalLegalRepresentativeEntity_Help_L')}
            </div>
            <div className="form-group col-md-6 col-xl-4">
                {this.labelFor(m => m.personalDocumentNumber, "GL_NUMBER_OF_BULGARIAN_ID_CARD_L", attributesClassFormControlRequiredLabel)}
                {this.textBoxFor(m => m.personalDocumentNumber, attributesClassFormControlReqired)}
                {this.inlineHelpFor(m => m.personalDocumentNumber, 'GL_PersonalLegalRepresentativeEntity_Help_L')}
            </div>
        </div>
    }
}
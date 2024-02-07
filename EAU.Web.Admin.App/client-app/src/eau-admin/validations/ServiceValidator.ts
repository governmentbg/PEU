import { EAUBaseValidator, Service, WaysToStartService } from "eau-core";
import { ObjectHelper } from "cnsys-core";

export class AddUpdateServiceValidator extends EAUBaseValidator<Service, any> {

    constructor() {
        super();

        this.ruleFor(x => x.name).notEmpty().withMessage(this.getMessage("GL_MANDATORY_SERVICE_NAME_E"));
        this.ruleFor(x => x.sunauServiceUri).notEmpty().withMessage(this.getMessage("GL_MANDATORY_URI_ADM_SERVICE_E"));
        this.ruleFor(x => x.sunauServiceUri).matches("^[1-9][0-9]*$").withMessage(this.getMessage("GL_MANDATORY_DIGIT_IISDA_NUMBER_E")).when(x => !ObjectHelper.isStringNullOrEmpty(x.sunauServiceUri));
        this.ruleFor(x => x.groupID).notEmpty().withMessage(this.getMessage("GL_MANDATORY_GROUP_E"));
        this.ruleFor(x => x.orderNumber).must(x => Number.isInteger(x.orderNumber)).withMessage(this.getMessage("GL_MANDATORY_NUMBER_IN_GROUP_E"));
        this.ruleFor(x => x.initiationTypeID).must(x => !ObjectHelper.isStringNullOrEmpty(x.initiationTypeID) && x.initiationTypeID > 0).withMessage(this.getMessage("GL_MANDATORY_SERVICE_INITIATION_METHOD_E"));
        this.ruleFor(x => x.documentTypeID).notEmpty().withMessage(this.getMessage("GL_MANDATORY_APPLICATION_INITIATION_SERVICE_E")).when(x => x.initiationTypeID == WaysToStartService.ByAplication);
        this.ruleFor(x => x.admStructureUnitName).notEmpty().withMessage(this.getMessage("GL_MANDATORY_NAME_STRUCTURAL_UNIT_E")).when(x => x.initiationTypeID == WaysToStartService.ByAplication);
        this.ruleFor(x => x.serviceUrl).notEmpty().withMessage(this.getMessage("GL_MANDATORY_SERVICE_ADDRESS_E")).when(x => x.initiationTypeID == WaysToStartService.ByRedirectToWebPage);
        this.ruleFor(x => x.deliveryChannels).notEmpty().withMessage(this.getMessage("GL_MANDATORY_RECEIVING_METHOD_E")).when(x => x.initiationTypeID == WaysToStartService.ByAplication);
    }

    public validate(obj: Service): boolean {
        let isValid = super.validate(obj);

        if(obj.initiationTypeID == WaysToStartService.ByAplication && obj.seviceTerms.length == 0) {
            obj.addError("emptyServiceTermsError",this.getMessage("GL_MANDATORY_TYPE_SERVICES_E"));
            isValid = false;
        }

        return isValid;
    }
}
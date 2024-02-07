import { ANDObligationSearchMode, EAUBaseValidator, ErrMsgCodesConstants, KATDocumentTypes, ResourceHelpers } from "eau-core";
import { AdditinalDataForObligatedPersonType, ANDObligationSearchCriteria, ObligatedPersonType } from "../models/ModelsManualAdded";

export class ANDObligationSearchCriteriaValidator extends EAUBaseValidator<ANDObligationSearchCriteria, any> {

    constructor() {
        super();

        //#region Обобщена проверка на задължения по фиш, НП или споразумение, с възможност за извършване на плащане

        //ЕГН/ЛНЧ/ЛН
        this.ruleFor(m => m.obligedPersonIdent).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'))
            .when(m => m.mode == ANDObligationSearchMode.ObligedPerson);
        this.ruleFor(m => m.obligedPersonIdent).isValidEGNLNCh().withMessage(this.getMessage("GL_INVALID_IDENT_E"))
            .when(m => m.mode == ANDObligationSearchMode.ObligedPerson);

        //Номер на СУМПС
        this.ruleFor(m => m.drivingLicenceNumber).notEmpty().withMessage(this.getMessage("GL_INPUT_FIELD_MUST_E"))
            .when(m => m.mode == ANDObligationSearchMode.ObligedPerson && m.obligatedPersonType == ObligatedPersonType.Personal && m.additinalDataForObligatedPersonType == AdditinalDataForObligatedPersonType.DrivingLicenceNumber);

        this.ruleFor(m => m.drivingLicenceNumber).matches('^(2\\d{8}|[A-Z]{2}\\d{7})$')
            .withMessage(ResourceHelpers.getErrorMessage(ErrMsgCodesConstants.FiledValueMustBeWith9DigitsWithFirst2Or2LatinLettersAnd7Digits, 'GL_DRIVING_LICENCE_NUMBER_L'))
            .when(m => m.mode == ANDObligationSearchMode.ObligedPerson && m.obligatedPersonType == ObligatedPersonType.Personal && m.additinalDataForObligatedPersonType == AdditinalDataForObligatedPersonType.DrivingLicenceNumber);

        //Номер на български личен картов документ
        this.ruleFor(m => m.personalDocumentNumber).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'))
            .when(m => m.mode == ANDObligationSearchMode.ObligedPerson
                && (m.obligatedPersonType == ObligatedPersonType.Entity
                    || (m.obligatedPersonType == ObligatedPersonType.Personal && m.additinalDataForObligatedPersonType == AdditinalDataForObligatedPersonType.PersonalDocumentNumber)));

        this.ruleFor(m => m.personalDocumentNumber).isValidBGDocNumber()
            .withMessage(ResourceHelpers.getErrorMessage(ErrMsgCodesConstants.FiledValueMustBeWith9DigitsOr7DigitsAnd2LatinLetters, 'GL_NUMBER_OF_BULGARIAN_ID_CARD_L'));

        //Чуждестранен регистрационен номер на МПС
        this.ruleFor(m => m.foreignVehicleNumber).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'))
            .when(m => m.mode == ANDObligationSearchMode.ObligedPerson && (m.obligatedPersonType == ObligatedPersonType.Personal && m.additinalDataForObligatedPersonType == AdditinalDataForObligatedPersonType.ForeignVehicleNumber));

        //ЕИК/БУЛСТАТ
        this.ruleFor(m => m.uic).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'))
            .when(m => m.mode == ANDObligationSearchMode.ObligedPerson && m.obligatedPersonType == ObligatedPersonType.Entity);
        this.ruleFor(m => m.uic).isValidBULSTAT().withMessage(this.getMessage("GL_INVALID_IDENT_E"))
            .when(m => m.mode == ANDObligationSearchMode.ObligedPerson && m.obligatedPersonType == ObligatedPersonType.Entity);

        //#endregion

        //#region Проверка на задължения по фиш, НП или споразумение, издадени от "Пътна полиция" и заплащане

        //Тип документ
        this.ruleFor(m => m.documentType).notEmpty().withMessage(this.getMessage("GL_INPUT_FIELD_MUST_E"))
            .when(m => m.mode == ANDObligationSearchMode.Document);

        //Серия
        this.ruleFor(m => m.documentSeries).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'))
            .when(m => m.mode == ANDObligationSearchMode.Document && m.documentType == KATDocumentTypes.TICKET);

        //Документ №

        this.ruleFor(m => m.documentNumber).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'))
            .when(m => m.mode == ANDObligationSearchMode.Document);

        //За Наказателно постановление / Полето може да съдържа само цифри и тире!
        this.ruleFor(m => m.documentNumber).matches('^[0-9]([0-9]|-(?!-))+$').withMessage(this.getMessage('GL_ONLY_DIGITS_DASH_ALLOWED_E'))
            .when(m => m.mode == ANDObligationSearchMode.Document && m.documentType == KATDocumentTypes.PENAL_DECREE);

        //Фиш / Полето може да съдържа само цифри!
        this.ruleFor(m => m.documentNumber).matches('^[0-9]+$').withMessage(this.getMessage('GL_ONLY_DIGITS_ALLOWED_E'))
            .when(m => m.mode == ANDObligationSearchMode.Document && m.documentType == KATDocumentTypes.TICKET);

        //Споразумение / Полето може да съдържа само цифри, тире и главна буква кирилица "С"!
        this.ruleFor(m => m.documentNumber).matches('^[0-9С-]+$').withMessage(this.getMessage('GL_ONLY_DIGITS_DASH_CAPITAL_CYRILLICLETTER_C_ALLOWED_E'))
            .when(m => m.mode == ANDObligationSearchMode.Document && m.documentType == KATDocumentTypes.AGREEMENT);

        //Дължима сума (BGN)
        this.ruleFor(m => m.amount).notEmpty().withMessage(this.getMessage('GL_INPUT_FIELD_MUST_E'))
            .when(m => m.mode == ANDObligationSearchMode.Document);

        //#endregion
    }

    public validate(obj: ANDObligationSearchCriteria): boolean {

        let isValid = super.validate(obj);

        return isValid;
    }
}
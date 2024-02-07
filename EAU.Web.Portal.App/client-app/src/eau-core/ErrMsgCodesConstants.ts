export namespace ErrMsgCodesConstants {
    /**Полето "<Field>" трябва да бъде попълнено.*/
    export const DefaultNotEmptyErrorMessage = "DOC_GL_DefaultNotEmptyErrorMessage_E";

    /**Полето трябва да бъде попълнено.*/
    export const FieldNotEmptyErrorMessage = "DOC_GL_MANDATORY_FIELD_E";

    /**Полето "<Field>" не е валидно съгласно нормативно установения формат.*/
    export const DefaultRegexErrorMessage = "DOC_GL_DefaultRegexErrorMessage_E";

    /**Полето "<Field>" надвишава максималния брой символи.*/
    export const DefaultLengthErrorMessage = "DOC_GL_DefaultLengthErrorMessage_E";

    /**Полето "<Field>" трябва да съдържа поне <Param1> символа.*/
    export const DefaultBottomRangeLengthErrorMessage = "DOC_GL_DefaultBottomRangeLengthErrorMessage_E";

    /**Възникна грешка!*/
    export const Error = "DOC_GL_Error_E";

    /** Грешка при зареждане на Xml-а. */
    export const XmlParseError = "DOC_GL_XmlParseError_E";

    /**Моля, въведете вида на въвеждания от Вас приложен документ.*/
    export const InvalidFileDescription = "DOC_GL_InvalidFileDescription_E";

    /**Полето "<Field>" трябва да съдържа валиден адрес на електронна поща.*/
    export const FieldMustContainsValidEmailAddress = "DOC_GL_InvalidEmailAddress_E"; //Item0006000025

    /**В полето "<Field>" може да се съдържат само <Param1>.*/
    export const FieldCanContainsOnly = "DOC_GL_FieldCanContainsOnly_E"; //Item0006000018

    /**Полето "<Field>" не може да съдържа по-малко от <Param1> и повече от <Param2> символа.*/
    export const FieldCanNotContainsLessThanOrMoreThanSymbols = "DOC_GL_FieldCanNotContainsLessThanOrMoreThanSymbols_E"; //Item0006000017

    /**Полето "<Field>" не може да съдържа повече от <Param2> символа.*/
    export const FieldCanNotContainsMoreThanSymbols = "DOC_GL_FieldCanNotContainsMoreThanSymbols_E";

    /**Полето "<Field>" трябва да съдържа стойност по-голяма от <Param1>.*/
    export const FiledGreatherField = "DOC_GL_FiledGreatherField_E";

    /**Полето "<Field>" трябва да съдържа точно <CharNumber> символа.*/
    export const FieldMustContainsExaclyNumSymbols = "DOC_GL_FieldMustContainsExaclyNumSymbols_E" //Item0006000020

    /**Полето "<Field>" трябва да съдържа число, по-голямо от <Param1>.*/
    export const FieldMustContainsDigitGreatThan = "DOC_GL_FieldMustContainsDigitGreatThan_E"; //Item0006000013

    /**Невалидно ЕГН.*/
    export const InvalidEGN = "DOC_GL_InvalidEGN_E";

    /**Невалидно ЛНЧ/ЛН.*/
    export const InvalidLNCh = "DOC_GL_InvalidLNCh_E";

    /**Невалидно ЛН. */
    export const InvalidLN = "DOC_GL_InvalidLN_E"

    /**Невалидно ЕГН/ЛНЧ.*/
    export const InvalidEGNLNCh = "DOC_GL_InvalidEGNLNCh_E";

    /**Невалиден БУЛСТАТ/ЕИК.*/
    export const InvalidBULSTATAndEIK = "DOC_GL_InvalidBULSTATAndEIK_E"

    /**Полето "<Field>" трябва да съдържа стойност по-малка от <CharsAllowed>.*/
    export const FiledLessThen = "DOC_GL_FiledLessThen_E";

    /**Полето "<Field>" трябва да съдържа стойност по-малка или равна от <Param1>.*/
    export const FiledLessThenOrEqual = "DOC_GL_FiledLessThenOrEqual_E";

    /**Полето "<Field>" трябва да съдържа стойност по-голяма от <CharsAllowed>.*/
    export const FiledGreatherThan = "DOC_GL_FiledGreatherThan_E";

    /**Изберете структура на МВР.*/
    export const AdministrativeDepartmentCode = "GL_STRUCTURE_RECEIVE_ISSUED_DOCUMENT_E";

    /**днешна дата*/
    export const CharsToday = "DOC_GL_CharsToday_E";

    /**Формата за дата на полето "<Field>" трябва да бъде "дд.мм.гггг", "мм.гггг" или "гггг".*/
    export const DateFormat = "DOC_GL_DateFormat_E";

    /**Невалидна дата на раждане.*/
    export const InvalidBirthDate = "DOC_GL_InvalidBirthDate_E";

    /**Дата на издаване не може да бъде по-голяма от днешната дата.*/
    export const DateMustBeSmallerThanNowDate = "DOC_GL_DateMustBeSmallerThanNowDate_E";

    /**В полето "<Field>" може да се съдържат само цифри.*/
    export const OnlyDigitsAllowed = "DOC_GL_OnlyDigitsAllowed_E";

    /**В полето "<Field>" може да се съдържат само цифри, като е позволено за започва с 2 главни букви на кирилица. */
    export const OnlyDigitsAllowedAnd2CyrilicLetters = "DOC_GL_OnlyDigitsAllowed_and2cyrilicLetters_E"

    /**главни букви на кирилица и цифри*/
    export const CharsAllowedCyrillicCapitalOnlyAndNums = "DOC_GL_CharsAllowedCyrillicCapitalOnlyAndNums_L";

    /**Попълването на секцията "<Section>" е задължително*/
    export const RequierFillSection = "DOC_GL_RequierFillSection_E"; //Item0006000023

    /**Полето "<Field>" от секцията "<Section>" да бъде попълнено*/
    export const RequierFillFieldFromSection = "DOC_GL_RequierFillFieldFromSection_E"; //Item0006000015

    /**Липсва задължителна декларация!*/
    export const MissingRequiredDeclaration = "DOC_GL_DeclarationVM_MissingRequiredDeclaration_E";

    /**Липсва задължителна политика за поверителност!*/
    export const MissingRequiredPolicy = "DOC_GL_DeclarationVM_MissingRequiredPolicy_E";

    /**В полето "<Field>" може да се съдържат само букви на кирилица и символите интервал, апостроф и тире.*/
    export const FieldCyrillicNameMustContainsSymbols = "DOC_GL_FieldCyrillicNameMustContainsSymbols_E";

    /**В полето "<Field>" може да се съдържат само букви на кирилица или латиница и символите интервал, апостроф и тире.*/
    export const FieldCyrillicLatinNameMustContainsSymbols = "DOC_GL_FieldCyrillicLatinNameMustContainsSymbols_E";

    /**Позволени символи са букви на кирилица, букви на латиница и арабски цифри */
    export const AllowedLettersAndNumsOnly = "DOC_GL_AllowedLettersAndNumsOnly";

    /**Полето "<Field>" може да съдържа само букви на латиница, цифри и символи !@#$%&*.-;. */
    export const SpecialSymbolsValidation = "GL_SPECIAL_SYMBOLS_VALIDATION_E";

    /**Полето "<Field>" може да съдържа само букви на кирилица, букви на латиница, арабски цифри, празна позиция или един от следните ~@#$%^&*()_+{}|":><.,/\?';-=|!.*/
    export const FieldValidationCyrillicLatinNumbersSymbols = "GL_DOC_FIELD_VALIDATION_CYRILLIC_LATIN_NUMBERS_SYMBOLS_E";

    /**Полето "<Field>" може да съдържа само букви на кирилица, букви на латиница, арабски цифри, празна позиция или един от следните ~@#$%^&*()_+{}|":><.,/\?';-=|!.№*/
    export const FieldValidationCyrillicLatinNumbersSymbols2 = "GL_DOC_FIELD_VALIDATION_CYRILLIC_LATIN_NUMBERS_SYMBOLS2_E";

    /**  Полето "<Field>" може да съдържа само букви на кирилица, арабски цифри, празна позиция или един от следните символи ~@#$%^&*()_+{}|":><.,/\?';-=|!*/
    export const FieldValidationCyrillicNumbersSymbols = "GL_DOC_FIELD_VALIDATION_CYRILLIC_NUMBERS_SYMBOLS_E"

    //Полето "<Field>" може да съдържа само главни букви на латиница без I, O и Q и цифри.
    export const CharsAllowedLatinCapitalOnlyAndNumsExcludeIOQ = "DOC_GL_CharsAllowedLatinCapitalOnlyAndNumsExcludeIOQ_E";

    /**В полето "<Field>" може да се съдържат само букви на кирилица, букви на латиница, интервал, арабски цифри и точка.*/
    export const AllowedLettersAndNumsAndDotOnly = "DOC_GL_AllowedLettersAndNumsAndDotOnly";

    /**Полето "<Field>" не може да съдържа символите "@" и "?". */
    export const NotAllowedQuestionmarksAndAT = "DOC_GL_NOT_ALLOWED_QUESTIONMARKS_AT_E";

    /**Некоректна стойност за полето "<Field>". */
    export const IncorrectValueForField = "DOC_GL_INCORRECT_VALUE_FOR_FIELD_E";

    /**Въведената дата не може да бъде по-късна от текущата*/
    export const DateCannotBeGreaterThanCurrent = "DOC_GL_DATE_CANNOT_BE_GREATER_THAN_CURRENT_E";

    /** Полето "<Field>" може да съдържа стойност между <Param1> и <Param2>. */
    export const FiledValueMustBeBetween = "DOC_GL_FiledValueMustBeBetween_E";

    /**В полето "<Field>" може да се съдържат само 9 цифри или водещи два символа, които са главни букви на латиница, последвани от 7 арабски цифри. */
    export const FiledValueMustBeWith9DigitsOr7DigitsAnd2LatinLetters = "GL_FiledValueMustBeWith9DigitsOr7DigitsAnd2LatinLetters_E";

    /**В полето "<Field>" може да се съдържат само 9 цифри, като първата цифра е "2" или водещи два символа, които са главни букви на латиница, последвани от 7 арабски цифри. */
    export const FiledValueMustBeWith9DigitsWithFirst2Or2LatinLettersAnd7Digits = "GL_FiledValueMustBeWith9DigitsWithFirst2Or2LatinLettersAnd7Digits_E";

    /**В полето "<Field>" може да се съдържат или 9 арабски цифри или водещи два символа, които са главни букви на латиница, последвани от 7 арабски цифри или между 8 и 9 символа, като първите два са главни букви на кирилица, последвани от арабски цифри. */
    export const FiledValueMustBeWith8Or9DigitsOr2LatinLettersAnd7Digits = "GL_FiledValueMustBeWith8Or9DigitsOr2LatinLettersAnd7Digits_E";
}
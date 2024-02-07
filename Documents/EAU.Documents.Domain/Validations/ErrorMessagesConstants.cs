namespace EAU.Documents.Domain.Validations
{
    public static class ErrorMessagesConstants
    {
        /// <summary>///Полето "Field" трябва да бъде попълнено./// </summary>
        public static string DefaultNotEmptyErrorMessage = "DOC_GL_DefaultNotEmptyErrorMessage_E";

        /// <summary>///Полето "Field" не е валидно съгласно нормативно установения формат./// </summary>
        public static string DefaultRegexErrorMessage = "DOC_GL_DefaultRegexErrorMessage_E";

        /// <summary>///Полето "Field" надвишава максималния брой символи./// </summary>
        public static string DefaultLengthErrorMessage = "DOC_GL_DefaultLengthErrorMessage_E";

        /// <summary>///Полето "Field" трябва да съдържа поне Param1 символа./// </summary>
        public static string DefaultBottomRangeLengthErrorMessage = "DOC_GL_DefaultBottomRangeLengthErrorMessage_E";

        /// <summary>///Полето "<Field>" не може да съдържа повече от <Param1> символа./// </summary>
        public static string DefaultMaxLengthErrorMessage = "DOC_GL_FieldCanNotContainsMoreThanSymbols_E";

        /// <summary>///Възникна грешка!/// </summary>
        public static string Error = "DOC_GL_Error_E";

        /// <summary>/// Грешка при зареждане на Xml-а. /// </summary>
        public static string XmlParseError = "DOC_GL_XmlParseError_E";

        /// <summary>///Моля, въведете вида на въвеждания от Вас приложен документ./// </summary>
        public static string InvalidFileDescription = "DOC_GL_InvalidFileDescription_E";

        /// <summary>///Полето "Field" трябва да съдържа валиден адрес на електронна поща./// </summary>
        public static string FieldMustContainsValidEmailAddress = "DOC_GL_InvalidEmailAddress_E"; //Item0006000025

        /// <summary>///В полето "Field" може да се съдържат само Param1./// </summary>
        public static string FieldCanContainsOnly = "DOC_GL_FieldCanContainsOnly_E"; //Item0006000018

        /// <summary>///Полето "Field" не може да съдържа по-малко от Param1 и повече от Param2 символа./// </summary>
        public static string FieldCanNotContainsLessThanOrMoreThanSymbols = "DOC_GL_FieldCanNotContainsLessThanOrMoreThanSymbols_E"; //Item0006000017

        /// <summary>///Полето "Field" трябва да съдържа стойност по-голяма от Param1./// </summary>
        public static string FiledGreatherField = "DOC_GL_FiledGreatherField_E";


        /// <summary>
        /// Полето "Field" трябва да съдържа точно Param1 символа.
        /// </summary>
        public static string FieldMustContainsExaclyNumSymbols = "DOC_GL_FieldMustContainsExaclyNumSymbols_E"; //Item0006000020

        /// <summary>///Полето "Field" трябва да съдържа число, по-голямо от Param1./// </summary>
        public static string FieldMustContainsDigitGreatThan = "DOC_GL_FieldMustContainsDigitGreatThan_E"; //Item0006000013

        /// <summary>///Невалидно ЕГН./// </summary>
        public static string InvalidEGN = "DOC_GL_InvalidEGN_E";

        /// <summary>///Невалидно ЛНЧ./// </summary>
        public static string InvalidLNCh = "DOC_GL_InvalidLNCh_E";

        /// <summary>///Невалидно ЕГН/ЛНЧ./// </summary>
        public static string InvalidEGNLNCh = "DOC_GL_InvalidEGNLNCh_E";

        /// <summary>///Невалиден идентификатор./// </summary>
        public static string InvalidIdent = "GL_INVALID_IDENT_E";

        /// <summary>
        /// Невалиден ИСО3166 код на държава.
        /// </summary>
        public static string InvalidISO3166CountryCode = "DOC_INVALID_ISO3166_COUNTRY_CODE_E";

        /// <summary>///Невалиден БУЛСТАТ/ЕИК./// </summary>
        public static string InvalidBULSTATAndEIK = "DOC_GL_InvalidBULSTATAndEIK_E";

        /// <summary>///Полето "Field" трябва да съдържа стойност по-малка от CharsAllowed./// </summary>
        public static string FiledLessThen = "DOC_GL_FiledLessThen_E";

        /// <summary>///Полето "Field" трябва да съдържа стойност по-малка или равна от Param1./// </summary>
        public static string FiledLessThenOrEqual = "DOC_GL_FiledLessThenOrEqual_E";

        /// <summary>///Полето "Field" трябва да съдържа стойност по-голяма от CharsAllowed./// </summary>
        public static string FiledGreatherThan = "DOC_GL_FiledGreatherThan_E";

        /// <summary>///Полето "Field" трябва да съдържа стойност по-голяма или равна от Param1./// </summary>
        public static string FiledGreatherThanOrEqual = "DOC_GL_FiledGreatherThanOrEqual_E";

        /// <summary>///От предложените структури на МВР, трябва да изберете ГДНП или РУ от посочено от Вас ОДМВР/СДВР./// </summary>
        public static string AdministrativeDepartmentCode = "DOC_GL_AdministrativeDepartmentCode_E";

        /// <summary>///днешна дата/// </summary>
        public static string CharsToday = "DOC_GL_CharsToday_E";

        /// <summary>///Формата за дата на полето "Field" трябва да бъде "дд.мм.гггг", "мм.гггг" или "гггг"./// </summary>
        public static string DateFormat = "DOC_GL_DateFormat_E";

        /// <summary>///Невалидна дата на раждане./// </summary>
        public static string InvalidBirthDate = "DOC_GL_InvalidBirthDate_E";

        /// <summary>///Дата на издаване не може да бъде по-голяма от днешната дата./// </summary>
        public static string DateMustBeSmallerThanNowDate = "DOC_GL_DateMustBeSmallerThanNowDate_E";

        /// <summary>///В полето "Field" може да се съдържат само цифри./// </summary>
        public static string OnlyDigitsAllowed = "DOC_GL_OnlyDigitsAllowed_E";

        ///<summary>/// В полето "Field" може да се съдържат само цифри, като е позволено за започва с 2 главни букви на кирилица. /// </summary>
        public static string OnlyDigitsAllowedAnd2CyrilicLetters = "DOC_GL_OnlyDigitsAllowed_and2cyrilicLetters_E";

        /// <summary>///главни букви на кирилица и цифри/// </summary>
        public static string CharsAllowedCyrillicCapitalOnlyAndNums = "DOC_GL_CharsAllowedCyrillicCapitalOnlyAndNums_L";

        /// <summary>///Попълването на секцията "Section" е задължително/// </summary>
        public static string RequierFillSection = "DOC_GL_RequierFillSection_E"; //Item0006000023

        /// <summary>///Полето "Field" от секцията "Section" да бъде попълнено/// </summary>
        public static string RequierFillFieldFromSection = "DOC_GL_RequierFillFieldFromSection_E"; //Item0006000015

        /// <summary>///Липсва задължителна декларация!/// </summary>
        public static string MissingRequiredDeclaration = "DOC_GL_DeclarationVM_MissingRequiredDeclaration_E";

        /// <summary>///Липсва задължителна политика за поверителност!/// </summary>
        public static string MissingRequiredPolicy = "DOC_GL_DeclarationVM_MissingRequiredPolicy_E";


        /// <summary>
        /// Полето "Телефон" изисква минимум 4 цифри, като преди тях се  допуска символ "+" (плюс).
        /// </summary>
        public static string InvalidPhone = "DOC_GL_WRONG_PHONE_NUMBER_E";

        /// <summary>
        /// Невалидна сума.
        /// </summary>
        public static string InvalidSum = "DOC_GL_INVALID_AMOUNT_E";

        /// <summary>
        ///Полето "Field" трябва да бъде число.
        /// </summary>
        public static string DefaultNonNumericErrorText = "DOC_GL_NON_NUMERIC_E";

        /// <summary>
        /// В полето "Field" може да се съдържат само букви на кирилица и символите интервал, апостроф и тире.
        /// </summary>
        public static string FieldCyrillicNameMustContainsSymbols = "DOC_GL_FieldCyrillicNameMustContainsSymbols_E";

        /// <summary>
        /// В полето "Field" може да се съдържат само букви на латиница и символите интервал, апостроф и тире.
        /// </summary>
        public static string FieldLatinNameMustContainsSymbols = "DOC_GL_FieldLatinNameMustContainsSymbols_E";

        /// <summary>
        /// Невалидна структура на обекта съгласно XML дефиницията му, вписана в регистъра на информационните обекти.
        /// </summary>
        public static string InvalidXmlElement = "DOC_GL_INVALID_XML_ELEMENT_E";

        /// <summary>
        /// Поне едно от полетата "Field" и "Param1"  в секцията  "Section" да бъде попълнено.
        /// </summary>
        public static string RequiredAtLeastOneOfTwoFields = "DOC_GL_RequiredAtLeastOneOfTwoFields_E";

        /// <summary>
        /// Не са посочени данни,  еднозначно идентифициращи чуждестранно юридическо лице.
        /// </summary>
        public static string InvalidForeignEntityBasicData = "DOC_GL_InvalidForeignEntityBasicData_E";

        /// <summary>
        /// Задължително е прикачването на документ за трайни увреждания, когато сте отбелязали в заявлението, че имате такъв.
        /// </summary>
        public static string RequireDocumentForDisabilities = "DOC_GL_RequireDocumentForDisabilities_E";

        /// <summary>
        /// Въведете валидна стойност в сантиментри в полето височина.
        /// </summary>
        public static string InsertValidHeight = "DOC_GL_InsertValidHeight_E";

        /// <summary>
        /// В поле "Field" има дублиращи елементи.
        /// </summary>
        public static string DuplicateElementsInCollection = "DOC_GL_DuplicateElementsInCollection_E";

        /// <summary>
        /// Списъчното поле "Field" трябва да съдържа точно един елемент.
        /// </summary>
        public static string RequierdOnlyOneElementInCollection = "DOC_GL_RequierdOnlyOneElementInCollection_E";

        /// <summary>
        /// Списъчното поле "Field" трябва да съдържа поне един елемент.
        /// </summary>
        public static string RequierdAtleastOneElementInCollection = "DOC_GL_RequierdAtleastOneElementInCollection_E";

        /// <summary>
        /// /**Полето "<Field>" може да съдържа само букви на кирилица, букви на латиница, арабски цифри, празна позиция или един от следните ~@#$%^&*()_+{}|":><.,/\?';-=|!.*/
        /// </summary>
        public static string FieldValidationCyrillicLatinNumbersSymbols = "GL_DOC_FIELD_VALIDATION_CYRILLIC_LATIN_NUMBERS_SYMBOLS_E";

        /// <summary>
        /// /**Полето "<Field>" може да съдържа само букви на кирилица, букви на латиница, арабски цифри, празна позиция или един от следните ~@#$%^&*()_+{}|":><.,/\?';-=|!.№*/
        /// </summary>
        public static string FieldValidationCyrillicLatinNumbersSymbols2 = "GL_DOC_FIELD_VALIDATION_CYRILLIC_LATIN_NUMBERS_SYMBOLS2_E";

        /**Полето "<Field>" не може да съдържа символите "@" и "?". */
        public static string NotAllowedQuestionmarksAndAT = "DOC_GL_NOT_ALLOWED_QUESTIONMARKS_AT_E";

        /// <summary>
        /// /**В полето "<Field>" може да се съдържат само главни букви на кирилица и арабски цифри. */
        /// </summary>
        public static string FieldCanContainsUpperCyrillicLettersAndDigits = "DOC_GL_FieldCanContainsUpperCaseCyrilicLettersAndDigits_L";

        /// <summary>
        /// /**  Полето "<Field>" може да съдържа само букви на кирилица, арабски цифри, празна позиция или един от следните символи ~@#$%^&*()_+{}|":><.,/\?';-=|!*/
        /// </summary>
        public static string FieldValidationCyrillicNumbersSymbols = "GL_DOC_FIELD_VALIDATION_CYRILLIC_NUMBERS_SYMBOLS_E"; 

        /// <summary>
        /// Задължително е прикачването на поне един приложен документ.
        /// </summary>
        public static string RequierAtLeastOneAttachedDocument = "DOC_GL_RequierAtLeastOneAttachedDocument_E";

        /// <summary>
        /// В полето "Field" може да се съдържат само букви на кирилица, букви на латиница, интервал, арабски цифри и точка.
        /// </summary>
        public static string AllowedLettersAndNumsAndDotOnly = "DOC_GL_AllowedLettersAndNumsAndDotOnly";

        /// <summary>
        /// Полето "Field" може да съдържа стойност между Param1 и Param2.
        /// </summary>
        public static string FiledValueMustBeBetween = "DOC_GL_FiledValueMustBeBetween_E";

        /// <summary>
        /// В полето "<Field>" може да се съдържат само 9 цифри или водещи два символа, които са главни букви на латиница, последвани от 7 арабски цифри.
        /// </summary>
        public static string FiledValueMustBeWith9DigitsOr7DigitsAnd2LatinLetters = "GL_FiledValueMustBeWith9DigitsOr7DigitsAnd2LatinLetters_E";

        /// <summary>
        /// В полето "<Field>" може да се съдържат само 9 цифри, като първата цифра е "2" или водещи два символа, които са главни букви на латиница, последвани от 7 арабски цифри.
        /// </summary>
        public static string FiledValueMustBeWith9DigitsWithFirst2Or2LatinLettersAnd7Digits = "GL_FiledValueMustBeWith9DigitsWithFirst2Or2LatinLettersAnd7Digits_E";

        /// <summary>
        /// В полето "<Field>" може да се съдържат или 9 арабски цифри или водещи два символа, които са главни букви на латиница, последвани от 7 арабски цифри или между 8 и 9 символа, като първите два са главни букви на кирилица, последвани от арабски цифри.
        /// </summary>
        public static string FiledValueMustBeWith8Or9DigitsOr2LatinLettersAnd7Digits = "GL_FiledValueMustBeWith8Or9DigitsOr2LatinLettersAnd7Digits_E";
    }
}
import { BaseErrorLocalizationResources, BaseLocalizationResources } from './common';

export interface LocalizationResources extends BaseLocalizationResources {
    /**дни*/
    Days: string;
    /**часове*/
    Hours: string;
    /**минути*/
    Minutes: string;
    /**секунди*/
    Seconds: string;
    /**Затвори*/
    Close: string;
    /** Днешна дата */
    dateNow: string;
}

export interface LocalizationErorrs extends BaseErrorLocalizationResources {
    /**Възникна грешка!*/
    Error: string;

    /** Грешка при зареждане на Xml-а. */
    XmlParseError: string;

    ///**Моля, въведете вида на въвеждания от Вас приложен документ.*/
    //InvalidFileDescription: string;

    ///**Полето "<Field>" трябва да бъде попълнено.*/
    //DefaultNotEmptyErrorMessage: string;

    ///**Полето "<Field>" трябва да съдържа поне <Param1> символа.*/
    //DefaultBottomRangeLengthErrorMessage: string;

    ///**Полето "<Field>" трябва да съдържа число, по-голямо от <Param1>.*/
    //Item0006000013: string;

    ///**Полето "<Field>" от секцията "<Section>" да бъде попълнено*/
    //Item0006000015: string;

    ///**Попълването на секцията "<Section>" е задължително*/
    //Item0006000023: string;

    ///**Полето "<Field>" не може да съдържа по-малко от <Param1> и повече от <Param2> символа.*/
    //Item0006000017: string;

    ///**В полето "<Field>" може да се съдържат само <Param1>.*/
    //Item0006000018: string;

    ///**Невалиден "ЕИК".*/
    //Item0006000024: string;

    ///**Полето "<Field>" трябва да съдържа валиден адрес на електронна поща.*/
    //Item0006000025: string;

    ///**Полето "<Field>" не е валидно съгласно нормативно установения формат.*/
    //DefaultRegexErrorMessage: string;
    ///**Полето "<Field>" надвишава максималния брой символи.*/
    //DefaultLengthErrorMessage: string;

    ///**Невалидно ЕГН.*/
    //InvalidEGN: string;

    ///**Невалидно ЛНЧ.*/
    //InvalidLNCh: string;

    ///**Невалидно ЕГН/ЛНЧ.*/
    //InvalidEGNLNCh: string;

    ///**Невалиден БУЛСТАТ/ЕИК.*/
    //InvalidBULSTATAndEIK: string;

    ///**Полето "<Field>" трябва да съдържа стойност по-малка от <CharsAllowed>.*/
    //FiledLessThen: string;

    ///**Полето "<Field>" трябва да съдържа стойност по-малка или равна от <Param1>.*/
    //FiledLessThenOrEqual: string;

    ///**Полето "<Field>" трябва да съдържа стойност по-голяма от <CharsAllowed>.*/
    //FiledGreatherThan: string;

    ///**днешна дата*/
    //CharsToday: string;

    ///**Полето "<Field>" трябва да съдържа стойност по-голяма от <Param1>.*/
    //FiledGreatherField: string;

    ///**От предложените структури на МВР, трябва да изберете ГДНП или РУ от посочено от Вас ОДМВР/СДВР.*/
    //AdministrativeDepartmentCode: string;

    ///**Формата за дата на полето "<Field>" трябва да бъде "дд.мм.гггг", "мм.гггг" или "гггг".*/
    //DateFormat: string;

    ///**Невалидна дата на раждане.*/
    //InvalidBirthDate: string;

    ///**Формата за дата на полето "<Field>" е невалидно.*/
    //InvalidDate: string;
    ///**Полето "<Field>" трябва да съдържа точно <CharNumber> символа.*/
    //Item0006000020: string;

    ///**главни букви на кирилица и цифри*/
    //CharsAllowedCyrillicCapitalOnlyAndNums: string;

    ///**Дата на издаване не може да бъде по-голяма от днешната дата.*/
    //DateMustBeSmallerThanNowDate: string;

    ///*В полето "<Field>" може да се съдържат само цифри.*/
    //OnlyDigitsAllowed: string;
}

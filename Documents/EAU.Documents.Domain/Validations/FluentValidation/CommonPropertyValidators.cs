using FluentValidation.Resources;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;

namespace EAU.Documents.Domain.Validations.FluentValidation
{
    public abstract class EAUPropertyValidator : PropertyValidator
    { 
        public EAUPropertyValidator(): base((IStringSource)null)
        {
            //for FV 10 https://github.com/FluentValidation/FluentValidation/blob/master/src/FluentValidation/Validators/PropertyValidator.cs
        }
    }

    #region NotEmpty

    public class EAUNotEmptyValidator : EAUPropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            object data = context.PropertyValue;

            if (data == null)
                return false;

            var result =  CnsysValidatorBase.IsObjectEmpty(data.GetType(), data);

            return !result;
        }
    }

    #endregion

    #region ЕГН валидация

    public class EGNValidator : EAUPropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            string data = (string)context.PropertyValue;

            if (!string.IsNullOrEmpty(data))
            {
                CnsysValidatorBase validatorBase = new CnsysValidatorBase();

                return validatorBase.ValidateEGN((string)context.PropertyValue);
            }
            else
                return true;
        }
    }

    #endregion

    #region ЛНЧ валидация

    public class LNCHValidator : EAUPropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            string data = (string)context.PropertyValue;

            if (!String.IsNullOrEmpty(data))
            {
                CnsysValidatorBase validatorBase = new CnsysValidatorBase();

                return validatorBase.ValidateLNCH(data);
            }
            else
                return true;
        }
    }

    #endregion

    #region ЕГН ЛНЧ клиентска валидация

    public class EgnLNCHClientValidator : EAUPropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            CnsysValidatorBase validatorBase = new CnsysValidatorBase();
            var propValue = (string)context.PropertyValue;

            if (!string.IsNullOrEmpty(propValue))
            {
                propValue = propValue.Trim();

                if (!validatorBase.ValidateLNCH(propValue))
                {
                    return validatorBase.ValidateEGN(propValue);
                }

                return true;
            }
            else
            {
                return true;
            }
        }
    }

    #endregion

    #region Булстат

    public class UICBulstatValidator : EAUPropertyValidator
    { 
        protected override bool IsValid(PropertyValidatorContext context)
        {
            string data = (string)context.PropertyValue;

            if (!String.IsNullOrEmpty(data))
            {
                CnsysValidatorBase validatorBase = new CnsysValidatorBase();

                return validatorBase.ValidateUICBulstat((string)context.PropertyValue);
            }
            else
                return true;
        }
    }

    #endregion

    #region Валидация на сума

    public class AmountValidator : EAUPropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            string data = (string)context.PropertyValue;

            if (!string.IsNullOrEmpty(data))
            {
                return CnsysValidatorBase.ValidateAmount(data);
            }
            else
                return true;
        }
    }

    #endregion

    #region Телефон

    public class PhoneValidator : EAUPropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            string data = (string)context.PropertyValue;

            if (!String.IsNullOrEmpty(data))
            {
                CnsysValidatorBase validatorBase = new CnsysValidatorBase();

                return validatorBase.PhoneValidation((string)context.PropertyValue);
            }
            else
                return true;
        }
    }

    #endregion

    #region Numeric data type validator

    public class NumericDataTypeValidator : EAUPropertyValidator
    {
        protected override bool IsValid(PropertyValidatorContext context)
        {
            object data = context.PropertyValue;

            return CnsysValidatorBase.IsNumericType(data);
        }
    }

    #endregion

    #region CountryISO3166

    public class CountryISO3166TwoLetterCodeValidator : EAUPropertyValidator
    {
        public static List<string> _codes = new List<string>() {  "AF",
                "AX",
                "AL",
                "DZ",
                "AS",
                "AD",
                "AO",
                "AI",
                "AQ",
                "AG",
                "AR",
                "AM",
                "AW",
                "AU",
                "AT",
                "AZ",
                "BS",
                "BH",
                "BD",
                "BB",
                "BY",
                "BE",
                "BZ",
                "BJ",
                "BM",
                "BT",
                "BO",
                "BQ",
                "BA",
                "BW",
                "BV",
                "BR",
                "IO",
                "BN",
                "BG",
                "BF",
                "BI",
                "KH",
                "CM",
                "CA",
                "CV",
                "KY",
                "CF",
                "TD",
                "CL",
                "CN",
                "CX",
                "CC",
                "CO",
                "KM",
                "CG",
                "CD",
                "CK",
                "CR",
                "CI",
                "HR",
                "CU",
                "CW",
                "CY",
                "CZ",
                "DK",
                "DJ",
                "DM",
                "DO",
                "EC",
                "EG",
                "SV",
                "GQ",
                "ER",
                "EE",
                "ET",
                "FK",
                "FO",
                "FJ",
                "FI",
                "FR",
                "GF",
                "PF",
                "TF",
                "GA",
                "GM",
                "GE",
                "DE",
                "GH",
                "GI",
                "GR",
                "GL",
                "GD",
                "GP",
                "GU",
                "GT",
                "GG",
                "GN",
                "GW",
                "GY",
                "HT",
                "HM",
                "VA",
                "HN",
                "HK",
                "HU",
                "IS",
                "IN",
                "ID",
                "IR",
                "IQ",
                "IE",
                "IM",
                "IL",
                "IT",
                "JM",
                "JP",
                "JE",
                "JO",
                "KZ",
                "KE",
                "KI",
                "KP",
                "KR",
                "KW",
                "KG",
                "LA",
                "LV",
                "LB",
                "LS",
                "LR",
                "LY",
                "LI",
                "LT",
                "LU",
                "MO",
                "MK",
                "MG",
                "MW",
                "MY",
                "MV",
                "ML",
                "MT",
                "MH",
                "MQ",
                "MR",
                "MU",
                "YT",
                "MX",
                "FM",
                "MD",
                "MC",
                "MN",
                "ME",
                "MS",
                "MA",
                "MZ",
                "MM",
                "NA",
                "NR",
                "NP",
                "NL",
                "NC",
                "NZ",
                "NI",
                "NE",
                "NG",
                "NU",
                "NF",
                "MP",
                "NO",
                "OM",
                "PK",
                "PW",
                "PS",
                "PA",
                "PG",
                "PY",
                "PE",
                "PH",
                "PN",
                "PL",
                "PT",
                "PR",
                "QA",
                "RE",
                "RO",
                "RU",
                "RW",
                "BL",
                "SH",
                "KN",
                "LC",
                "MF",
                "PM",
                "VC",
                "WS",
                "SM",
                "ST",
                "SA",
                "SN",
                "RS",
                "SC",
                "SL",
                "SG",
                "SX",
                "SK",
                "SI",
                "SB",
                "SO",
                "ZA",
                "GS",
                "SS",
                "ES",
                "LK",
                "SD",
                "SR",
                "SJ",
                "SZ",
                "SE",
                "CH",
                "SY",
                "TW",
                "TJ",
                "TZ",
                "TH",
                "TL",
                "TG",
                "TK",
                "TO",
                "TT",
                "TN",
                "TR",
                "TM",
                "TC",
                "TV",
                "UG",
                "UA",
                "AE",
                "GB",
                "US",
                "UM",
                "UY",
                "UZ",
                "VU",
                "VE",
                "VN",
                "VG",
                "VI",
                "WF",
                "EH",
                "YE",
                "ZM",
                "ZW"
        };

        protected override bool IsValid(PropertyValidatorContext context)
        {
            object data = context.PropertyValue;

            return data != null && _codes.Contains(data.ToString());
        }
    }

    #endregion
}

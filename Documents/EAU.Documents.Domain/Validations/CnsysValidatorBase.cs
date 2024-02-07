using CNSys;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace EAU.Documents.Domain.Validations
{
    public class CnsysValidatorBase
    {
        public static bool PhoneValidate(string phone)
        {
            phone = phone.Trim();

            if(phone.StartsWith("+359")) 
            {
                if (phone.Length == 13
                && OnlyDigitsValidate(phone.Substring(1))
                && (phone[4] == '8' || phone[4] == '9'))
                {
                    return true;
                }
            }
            else if (phone.StartsWith('+'))
            {
                if ((phone.Length >= 12 && phone.Length <= 16)
                && OnlyDigitsValidate(phone.Substring(1)))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool OnlyDigitsValidate(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                foreach (char c in text.ToCharArray())
                    if (!char.IsDigit(c))
                        return false;

                return true;
            }
            else
                return false;
        }

        public static bool IsObjectEmpty(Type propertyType, object value)
        {
            if (propertyType == typeof(string))
            {
                return value != null ? string.IsNullOrEmpty(value.ToString()) : true;
            }
            else if (Nullable.GetUnderlyingType(propertyType) != null)
            {
                return value == null;
            }
            else if (!propertyType.IsValueType)
            {
                return value == null || ObjectUtility.IsEmpty(value);
            }
            else
                return false;
        }

        public static bool IsObjectEmpty<TProperty>(object value)
        {
            return IsObjectEmpty(typeof(TProperty), value);
        }

        public static bool IsNumericType(object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        #region Phone

        public virtual bool PhoneValidation(string phone)
        {
            return PhoneValidate(phone);
        }

        #endregion

        #region Amount

        public static bool ValidateAmount(string amount)
        {
            bool result = true;
            float tmp;
            if (!float.TryParse(amount, out tmp))
            {
                result = false;
            }
            if (Math.Round(tmp, 2) != tmp)
            {
                result = false;
            }
            return result;
        }

        #endregion

        #region URI

        public static bool ValidateURI(int regIndex, int batchNum)
        {
            if (regIndex.ToString().Length > 4
                || batchNum.ToString().Length > 6)
            {
                return false;
            }

            //Проверка дали са цифри            
            char[] charDigits = regIndex.ToString().ToCharArray();
            for (int i = 0; i < regIndex.ToString().Length; i++)
            {
                if (!(Char.IsDigit(charDigits[i])))
                {
                    return false;
                }
            }

            charDigits = batchNum.ToString().ToCharArray();
            for (int i = 0; i < batchNum.ToString().Length; i++)
            {
                if (!(Char.IsDigit(charDigits[i])))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region UIC / Bulstat validation

        public virtual bool ValidateBulstat(int[] digits)
        {
            string bulstat = (digits == null) ? null : digits.Aggregate(string.Empty, (s, i) => s + i.ToString());
            return ValidateUic(bulstat, digits);
        }

        public virtual bool ValidateBulstat(string bulstat)
        {
            int[] digits = null;

            if (OnlyDigitsValidate(bulstat))
            {
                digits = (from d in bulstat.ToCharArray()
                          select Convert.ToInt32(d.ToString())).ToArray();

                return ValidateBulstat(bulstat, digits);
            }

            return false;
        }

        public virtual bool ValidateUic(int[] digits)
        {
            string uic = (digits == null) ? null : digits.Aggregate(string.Empty, (s, i) => s + i.ToString());
            return ValidateUic(uic, digits);
        }

        public virtual bool ValidateUic(string uic)
        {
            int[] digits = null;

            if (OnlyDigitsValidate(uic))
            {
                digits = (from d in uic.ToCharArray()
                          select Convert.ToInt32(d.ToString())).ToArray();

                return ValidateUic(uic, digits);
            }

            return false;
        }

        public virtual bool ValidateUICBulstat(int[] digits)
        {
            string uICBulstat = (digits == null) ? null : digits.Aggregate(string.Empty, (s, i) => s + i.ToString());
            return ValidateUICBulstat(uICBulstat, digits);
        }

        public virtual bool ValidateUICBulstat(string uICBulstat)
        {
            int[] digits = null;

            if (OnlyDigitsValidate(uICBulstat))
            {
                digits = (from d in uICBulstat.ToCharArray()
                          select Convert.ToInt32(d.ToString())).ToArray();

                return ValidateUICBulstat(uICBulstat, digits);
            }

            return false;
        }

        #endregion

        #region EGN Validation

        public virtual bool ValidateEGN(string egn)
        {
            int[] digits = null;

            if (OnlyDigitsValidate(egn))
            {
                digits = (from d in egn.ToCharArray()
                          select Convert.ToInt32(d.ToString())).ToArray();

                return ValidateEGN(digits);
            }

            return false;
        }

        public virtual bool ValidateEGN(int[] digits)
        {
            int[] coeffs = { 2, 4, 8, 5, 10, 9, 7, 3, 6 };
            int[] days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            if (digits.Count() != 10)
                return false;

            //проверка за дата на раждане във формат ггммдд.
            if (!ValidateBirthDatePr(digits))
            {
                return false;
            }

            //проверка за чексума
            int checksum = 0;

            for (int j = 0; j < coeffs.Length; j++)
            {
                checksum += digits[j] * coeffs[j];
            }
            checksum %= 11;
            if (10 == checksum)
            {
                checksum = 0;
            }

            if (digits[9] != checksum)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region LNCH Validation

        public virtual bool ValidateLNCH(string lnch)
        {
            int[] digits = null;

            if (OnlyDigitsValidate(lnch))
            {
                digits = (from d in lnch.ToCharArray()
                          select Convert.ToInt32(d.ToString())).ToArray();

                return ValidateLNCH(digits);
            }

            return false;
        }

        public virtual bool ValidateLNCH(int[] digits)
        {
            if (digits.Count() != 10)
                return false;

            int[] coeffs = { 21, 19, 17, 13, 11, 9, 7, 3, 1 };

            int checksum = 0;
            for (int j = 0; j < coeffs.Length; j++)
            {
                checksum += digits[j] * coeffs[j];
            }
            checksum %= 10;
            if (checksum == 10) checksum = 0;
            if (checksum != digits[9])
            {
                return false;
            }

            return true;
        }

        #endregion

        public virtual bool ValidateBirthDate(int[] digits)
        {
            return digits.Count() == 6 && ValidateBirthDatePr(digits);
        }

        //валидира дата на раждане във формати ДД.ММ.ГГГГ; ММ.ГГГГ; ГГГГ
        public virtual bool isValidBirthDate(string birthDate)
        {
            Regex regex = new Regex(@"^((3[01]|[12][0-9]|0[1-9]).(1[0-2]|0[1-9]).\d{4}|(1[0-2]|0[1-9]).\d{4}|\d{4})$", RegexOptions.IgnorePatternWhitespace);
            Match x = regex.Match(birthDate);
            if (x.Success == false) return false;

            int year;
            int month;
            int day;
            //само година
            if (birthDate.Length == 4)
            {
                if (int.TryParse(birthDate, out year) && year <= DateTime.Now.Year)
                    return true;
            }
            //месец и година
            if (birthDate.Length == 7)
            {
                if (int.TryParse(birthDate.Substring(0, 2), out month) && int.TryParse(birthDate.Substring(3), out year))
                {
                    if (month <= 12 &&
                        (month <= DateTime.Now.Month && year == DateTime.Now.Year ||
                        year < DateTime.Now.Year)
                        )
                        return true;
                }
            }
            //ден, месец, година
            if (birthDate.Length == 10)
            {
                if (int.TryParse(birthDate.Substring(0, 2), out day) && int.TryParse(birthDate.Substring(3, 2), out month) && int.TryParse(birthDate.Substring(6), out year))
                {
                    if (month <= 12 &&
                        (month <= DateTime.Now.Month && year == DateTime.Now.Year ||
                        year < DateTime.Now.Year) &&
                        day <= 31
                        )
                        return true;
                }
            }

            return false;
        }

        //валидира дата на раждане във формати ДДММГГГГ
        public virtual bool isValidBirthDateString(string birthDate, int? minYear, bool? maxToday)
        {
            Regex regex = new Regex(@"^((3[01]|[12][0-9]|0[1-9])(1[0-2]|0[1-9])\d{4})$", RegexOptions.IgnorePatternWhitespace);
            Match x = regex.Match(birthDate);
            if (x.Success == false) return false;

            int year;
            int month;
            int day;            
            //ден, месец, година
            if (birthDate.Length == 8)
            {
                if (int.TryParse(birthDate.Substring(0, 2), out day) && int.TryParse(birthDate.Substring(2, 2), out month) && int.TryParse(birthDate.Substring(4), out year))
                {
                    if (minYear.HasValue && year < minYear)
                        return false;

                    if (maxToday.HasValue && maxToday.Value)
                    {
                        if (
                            (year == DateTime.Now.Year && (month < DateTime.Now.Month || (month == DateTime.Now.Month && day <= DateTime.Now.Day))) 
                            || year < DateTime.Now.Year)
                            return true;
                        else
                            return false;
                    }
                }
                return true;
            }

            return false;
        }

        public virtual bool isMinorPerson(string egn)
        {
            int year;
            string yearStr = egn.Substring(0,2);
            int month = int.Parse(egn.Substring(2,2));
            int day = int.Parse(egn.Substring(4, 2));

            if (month > 40)
            {
                year = int.Parse("20" + yearStr);
                month = month - 40;
            }
            else
                year = int.Parse("19" + yearStr);

            DateTime dt = new DateTime(year, month, day, 0, 0, 0);
            dt = dt.AddYears(16);

            return dt > DateTime.Now;
        }

        #region Helpers

        /// <summary>
        /// Валидира Дата на раждане във формат ггммдд.
        /// </summary>
        /// <param name="id">Стойност на идентификатора.</param>
        /// <param name="digits">Масив от цифрите на идентирикатора.</param>
        /// <param name="errors">Колекция за грешки.</param>
        /// <returns>
        /// 0 - OK.
        /// 1 - Невалиден идентификатор.
        /// </returns>
        private static bool ValidateBirthDatePr(int[] digits)
        {
            int[] days = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

            //проверка за дата на раждане
            int dd = (digits[4] * 10) + digits[5],
                mm = (digits[2] * 10) + digits[3],
                yy = (digits[0] * 10) + digits[1],
                yyyy = 0;

            if (mm >= 1 && mm <= 12)
            {
                yyyy = 1900 + yy;
            }
            else if (mm >= 21 && mm <= 32)
            {
                mm -= 20;
                yyyy = 1800 + yy;
            }
            else if (mm >= 41 && mm <= 52)
            {
                mm -= 40;
                yyyy = 2000 + yy;
            }
            else
            {
                return false;
            }

            days[1] += DateTime.IsLeapYear(yyyy) ? 1 : 0;

            if (!(dd >= 1 && dd <= days[mm - 1]))
            {
                return false;
            }

            return true;
        }

        private bool ValidateBulstat(string id, int[] digits)
        {
            return (digits.Count() == 9 || digits.Count() == 13)
                    && digits[0] != 2
                    && ValidateUICBulstat(id, digits);
        }

        private bool ValidateUic(string id, int[] digits)
        {
            return digits.Count() == 9
                    && digits[0] == 2
                    && ValidateUICBulstat(id, digits);
        }

        private static bool ValidateUICBulstat(string id, int[] digits)
        {
            if (id.Length == 9)
                return ValidateShortUIC(id, digits);
            else if (id.Length == 13)
                return ValidateLongUIC(id, digits);
            else
                return false;
        }

        /// <summary>
        /// Валидира ЕИК (9 цифрен). 
        /// </summary>
        /// <param name="id">Стойност на идентификатора.</param>
        /// <param name="digits">Масив от цифрите на идентирикатора.</param>
        /// <returns>true- ako e validen, false- ne e validen</returns>
        private static bool ValidateShortUIC(string id, int[] digits)
        {
            int checksum = 0;

            if (id.Length == 9)
            {
                for (int j = 0; j < (id.Length - 1); j++)
                {
                    checksum += digits[j] * (j + 1);
                }

                checksum %= 11;

                if (10 == checksum)
                {
                    checksum = 0;

                    //tova otdolu trqbva da go ima 
                    for (int j = 0; j < (id.Length - 1); j++)
                    {
                        checksum += digits[j] * (j + 3);
                    }

                    checksum %= 11;

                    if (10 == checksum)
                        checksum = 0;
                }

                if (digits[8] != checksum)
                {
                    return false;
                }

                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Валидира ЕИК на клонове и поделения (13 цифрен). 
        /// </summary>
        /// <param name="id">Стойност на идентификатора.</param>
        /// <param name="digits">Масив от цифрите на идентирикатора.</param>
        /// <returns>true- ako e validen, false- ne e validen</returns>
        private static bool ValidateLongUIC(string id, int[] digits)
        {
            int checksum = 0;

            if (id.Length == 13)
            {
                var shortID = id.Substring(0, 9);
                var shortDigits = (from d in shortID.ToCharArray()
                                   select Convert.ToInt32(d.ToString())).ToArray();

                if (!ValidateShortUIC(shortID, shortDigits))
                    return false;

                checksum = 2 * digits[8] + 7 * digits[9] + 3 * digits[10] + 5 * digits[11];
                checksum %= 11;

                if (10 == checksum)
                {
                    checksum = 4 * digits[8] + 9 * digits[9] + 5 * digits[10] + 7 * digits[11];
                    checksum %= 11;

                    if (10 == checksum)
                        checksum = 0;
                }

                if (digits[12] != checksum)
                {
                    return false;
                }

                return true;
            }
            else
                return false;
        }

        #endregion
    }
}

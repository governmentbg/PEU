import { ObjectHelper } from './ObjectHelper';
import { Helper } from './Helper'
import moment from 'moment';

//TODO: Може да се преизползват тези функции в BaseValidator.ts
export namespace ValidationHelper {
    export function isValidEGN(x: string): boolean {

        if (ObjectHelper.isStringNullOrEmpty(x) || x.length != 10 || !/^\d{10}$/.test(x))
            return false;

        var year = Number(x.slice(0, 2));
        var month = Number(x.slice(2, 4));
        var day = Number(x.slice(4, 6));

        if (month >= 40) {
            year += 2000;
            month -= 40;
        } else if (month >= 20) {
            year += 1800;
            month -= 20;
        } else {
            year += 1900;
        }

        var date = new Date(year, month - 1, day);

        if (!(date && (date.getMonth() + 1) == month && date.getDate() == Number(day)))
            return false;

        var checkSum = 0;
        var weights = [2, 4, 8, 5, 10, 9, 7, 3, 6];

        for (var ii = 0; ii < weights.length; ++ii) {
            checkSum += weights[ii] * Number(x.charAt(ii));
        }

        checkSum %= 11;
        checkSum %= 10;

        if (checkSum !== Number(x.charAt(9)))
            return false;

        return true;
    }

    export function isValidLNCh(x: string): boolean {
        if (ObjectHelper.isStringNullOrEmpty(x) || x.length != 10 || !/^\d{10}$/.test(x))
            return false;

        var digits: Array<number> = [];
        for (var index = 0; index < x.length; index++) {
            var number = Number(x[index]);

            if (number != NaN)
                digits.push(number)
            else
                return false
        }

        var coeffs = [21, 19, 17, 13, 11, 9, 7, 3, 1]
        var checkSum = 0;

        coeffs.forEach(function (coeff, index) {
            checkSum += digits[index] * coeff
        })

        checkSum %= 10;
        if (checkSum == 10) checkSum = 0;

        if (checkSum != digits[9])
            return false;

        return true;
    }

    export function isValidEGNLNCh(x: string): boolean {
        return (isValidEGN(x) || isValidLNCh(x));
    }

    export function isValidPhone(x: string): boolean {
        let phone = x.trim();

        if (phone.startsWith("+359")) {
            if (phone.length == 13
                && ObjectHelper.isNumber(phone.substr(1))
                && (phone[4] == '8' || phone[4] == '9')) {
                return true;
            }
            return false;
        }
        else if (phone.startsWith('+')) {
            if ((phone.length >= 12 && phone.length <= 16)
                && ObjectHelper.isNumber(phone.substr(1))) {
                return true;
            }
            return false;
        }
        return false;
    }

    export function isEmailAddress(x: string) {
        const emailAddressPattern = /(?=^.{1,64}@)^[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])(;(?=.{1,64}@)[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9]))*$/i;

        if (ObjectHelper.isStringNullOrEmpty(x))
            return false;

        return emailAddressPattern.test(x);
    }

    export function isMinorPerson(egn: string): boolean {

        var year: number;
        var yearStr = egn.substr(0, 2);
        var month = parseInt(egn.substr(2, 2));
        var day = parseInt(egn.substr(4, 2));

        if (month > 40) {
            year = parseInt("20" + yearStr);
            month = month - 40;
        } else
            year = parseInt("19" + yearStr);

        var todayYear = moment().year()
        var todayDay = moment().date();
        var todayMonth = moment().month() + 1;

        return moment().year(year).month(month).date(day).add("year", 16) > moment().year(todayYear).month(todayMonth).date(todayDay) ||
            (year == (todayYear - 16) && month > todayMonth) ||
            (year == (todayYear - 16) && month == todayMonth && day > todayDay);

    }

    export function isValidBULSTATEIK(x: string): boolean {

        function validateShortUIC(id: string, digits: Array<number>) {
            var checkSum = 0;

            for (var j = 0; j < id.length - 1; j++) {
                checkSum += digits[j] * (j + 1);
            }

            checkSum %= 11

            if (10 == checkSum) {
                checkSum = 0;

                for (var j = 0; j < (id.length - 1); j++) {
                    checkSum += digits[j] * (j + 3);
                }

                checkSum %= 11;

                if (10 == checkSum)
                    checkSum = 0;
            }

            if (digits[8] != checkSum) {
                return false;
            }

            return true;
        }

        function validateLongUIC(id: string, digits: Array<number>) {
            var checksum = 0;

            if (id.length == 13) {
                var shortID = id.substr(0, 9);
                var shortDigits: Array<number> = [];

                for (var index = 0; index < shortID.length; index++) {
                    shortDigits.push(Number(shortID[index]))
                }

                if (!validateShortUIC(shortID, shortDigits))
                    return false;

                checksum = 2 * digits[8] + 7 * digits[9] + 3 * digits[10] + 5 * digits[11];
                checksum %= 11;

                if (10 == checksum) {
                    checksum = 4 * digits[8] + 9 * digits[9] + 5 * digits[10] + 7 * digits[11];
                    checksum %= 11;

                    if (10 == checksum)
                        checksum = 0;
                }

                if (digits[12] != checksum) {
                    return false;
                }

                return true;
            }
            else
                return false;
        }

        function validateUICBulstat(id: string, digits: Array<number>) {
            if (id.length == 9)
                return validateShortUIC(id, digits);
            else if (id.length == 13)
                return validateLongUIC(id, digits);
            else
                return false;
        }

        // проверката за липсваща стойност се прави от notNull()
        if (x == undefined || x == null || ObjectHelper.isStringNullOrEmpty(x)) {
            return true;
        }

        if (x.length != 9 && x.length != 13 && x[0] != "2")
            return false

        if (/^\d{9}$/.test(x) || /^\d{13}$/.test(x)) {
            var digits: Array<number> = [];
            for (var index = 0; index < x.length; index++) {
                var number = Number(x[index]);

                if (number != NaN)
                    digits.push(number)
                else
                    return false
            }

            return validateUICBulstat(x, digits)
        } else {
            return false;
        }
    }
}
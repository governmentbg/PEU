import moment from "moment";
import { Constants, resourceManager } from "eau-core";

export const formatDate = (date: any, format?: string) => {
    var dateAsMoment = moment(date);

    if (dateAsMoment.isValid()) {
        return format ? dateAsMoment.format(format) : dateAsMoment.format(Constants.DATE_FORMATS.date)
    }

    return '';
}

export const formatAmount = (amount: any, currency?: string) => {
    var amountAsNumber = Number(amount);

    if (!isNaN(amountAsNumber)) {
        return currency ? `${amountAsNumber.toFixed(2)} ${currency}` : `${amountAsNumber.toFixed(2)} ${resourceManager.getResourceByKey('GL_BGN_ABBRAVETION_L')}`
    }

    return '';
}
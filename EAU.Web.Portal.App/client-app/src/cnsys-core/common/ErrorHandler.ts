import { TypeSystem } from "./";
import { ApiError, ClientError } from "../models";

/**
 * Функцията извърша лог на подходящите грешки. Грешките, които идват от сървъра не се логват обратно, защото идват от там.
 * Функцията връща истина, ако грешката е логната в противен случай лъжа.
 * @param error
 */
export function handleErrorLog(error: ApiError | ClientError | Error): boolean {

    var exType = TypeSystem.getTypeInfo(error);

    if ((exType &&
        (exType.ctor != ApiError) ||
        exType == null)) {
        //logger.error(error);
        return true;
    }
    else
        return false;
}
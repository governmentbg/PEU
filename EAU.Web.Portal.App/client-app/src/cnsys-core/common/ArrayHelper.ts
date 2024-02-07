import { ObjectHelper } from './ObjectHelper'

export class ArrayHelper {
    /**
     *  Проверява за дублиращи елементи в масив.
     * @param arr масив.
     * @param areEqualDelegat делегат, който преценява дали два елемента са еднакви.
     */
    public static hasDuplicatedElements<T>(arr: T[], areEqualDelegat: (objA: T, objB: T) => boolean): boolean {
        if (arr && arr.length > 1) {
            let arrLength = arr.length;
            for (let i: number = 0; i < (arrLength - 1); i++) {
                let currElI = arr[i];
                for (let j: number = (i + 1); j < arrLength; j++) {
                    let currElJ = arr[j];

                    if (areEqualDelegat(currElI, currElJ)) {
                        return true;
                    }
                }
            }
        }

        return false;
    }


    /**Проверява дали двата масива са с еднакви елементи в един и същи ред ! */
    public static arraysEqual(a: any, b: any) {
        if (a === b) return true;
        if (a == null || b == null) return false;
        if (a.length != b.length) return false;

        // If you don't care about the order of the elements inside
        // the array, you should sort both arrays here.
        // Please note that calling sort on an array will modify that array.
        // you might want to clone your array first.

        for (var i = 0; i < a.length; ++i) {
            if (a[i] !== b[i]) return false;
        }
        return true;
    }

    public static removeElement(array: any[], elemToRemove: any): boolean {
        if (array) {
            var index = array.indexOf(elemToRemove);

            if (index > -1) {
                var ret = array.splice(index, 1);

                if (ret && ret.length > 0)
                    return true;
                else
                    return false;
            }
        }
        return false;
    }

    public static removeAllElements(array: any[], elemToRemove: any): boolean {
        let result: boolean = false;

        if (array) {
            var index = array.indexOf(elemToRemove);

            while (index > -1) {
                array.splice(index, 1);

                result = true;

                index = array.indexOf(elemToRemove);
            }
        }

        return result;
    }

    public static removeArrayOfElementsFromAnotherArray(baseArray: any[], elementsToRemove: any[]): void {
        let elementsToRemoveIndexes = ArrayHelper.queryable.from(baseArray).where((element: any) => elementsToRemove.indexOf(element) >= 0).select((element: any) => baseArray.indexOf(element)).toArray();
        elementsToRemoveIndexes.forEach((elementIndex: any) => baseArray.splice(elementIndex, 1));
    }

    public static firstIndexOf<TElement>(array: TElement[], callbackfn: (value: TElement, index: number, array: TElement[]) => boolean): number {

        for (var i = 0; i < array.length; i++) {
            if (callbackfn(array[i], i, array))
                return i;
        }
        return -1;
    }

    public static concat<TElement>(source: TElement[], item: TElement | TElement[]) {
        if (ObjectHelper.isArray(item)) {
            for (var elem of item) {
                source.push(elem);
            }
        }
        else {
            source.push(item);
        }
    }

    public static queryable = {
        from: function <T>(collection: Array<T>): IQueryable<T> {
            var that = this;

            var _where = function (predicate: (elem: T) => boolean): IQueryable<T> {
                var result: Array<T> = [];

                for (var elem of collection) {
                    if (predicate(elem))
                        result.push(elem);
                }

                return that.from(result);

            };

            var _select = function <TResult>(selector: (elem: T) => TResult): IQueryable<TResult> {
                var result: Array<TResult> = [];

                for (var elem of collection) {
                    result.push(selector(elem));
                }

                return that.from(result);
            };

            var _count = function (predicate: (elem: T) => boolean): number {
                if (!predicate)
                    return collection.length;

                var counter = 0;
                for (var elem of collection) {
                    if (predicate(elem))
                        counter++;
                }

                return counter;
            };

            var _toArray = function (): T[] {
                return collection;
            };

            var _first = function (predicate: (elem: T) => boolean): T {
                var firstElem = _firstOrDefault(predicate);

                if (firstElem) {
                    return firstElem;
                }
                else {
                    throw 'There is no element in collection';
                }
            };

            var _firstOrDefault = function (predicate: (elem: T) => boolean): T | undefined {
                var elems = collection;

                if (predicate) {
                    elems = _where(predicate).toArray();
                }

                if (ObjectHelper.isArrayNullOrEmpty(elems)) {
                    return undefined;
                }
                else {
                    return elems[0];
                }
            };

            var _single = function (predicate: (elem: T) => boolean): T {
                var singleElem = _singleOrDefault(predicate);

                if (singleElem) {
                    return singleElem;
                }
                else {
                    throw 'There is no element in collection';
                }
            };

            var _singleOrDefault = function (predicate: (elem: T) => boolean): T | undefined {
                var elems = collection;

                if (predicate) {
                    elems = _where(predicate).toArray();
                }

                if (ObjectHelper.isArrayNullOrEmpty(elems)) {
                    return undefined;
                }
                else {
                    if (elems.length == 1) {
                        return elems[0];
                    }
                    else {
                        throw 'There is more then one element in collection';
                    }
                }
            }

            return {
                where: _where,
                select: _select,
                count: _count,
                toArray: _toArray,
                first: _first,
                firstOrDefault: _firstOrDefault,
                single: _single,
                singleOrDefault: _singleOrDefault
            };
        }
    };
}

interface IQueryable<T> {
    where: (predicate: (elem: T) => boolean) => IQueryable<T>;
    select: <TResult>(selector: (elem: T) => TResult) => IQueryable<TResult>;
    count: (predicate?: (elem: T) => boolean) => number;
    first: (predicate?: (elem: T) => boolean) => T;
    firstOrDefault: (predicate?: (elem: T) => boolean) => T | undefined;
    single: (predicate: (elem: T) => boolean) => T;
    singleOrDefault: (predicate: (elem: T) => boolean) => T | undefined;
    toArray: () => T[];
}
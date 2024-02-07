import moment from 'moment';
import { Dictionary } from 'typescript-collections';

interface ThrottlerDictionaryElem<TResult> {
    date: Date;
    result?: TResult;
}

export class ThrottlerPromise<TItem, TResult> {
    private _itemExpirationTimeSpan: number;
    private _itemsDictionary: Dictionary<string, ThrottlerDictionaryElem<TResult>>;
    private _createHash: (input: TItem) => string;

    /**itemExpirationTimeSpan - Интервал от време, след който this.hasToTriggerActionForItem(itemHash) връща true.
    Пример: Логваме(пращаме към сървъра) грешка в момент X. Ако в следващият интервал от време itemExpirationTimeSpan се опитаме да логнем същата грешка отново,
    грешката няма да се прати към сървъра.
    createHash - функция, която после ползваме в triggerAction(), за да създадем ключ от подадения item и да проверим дали в _itemsDictionary вече има обект с такъв ключ (и ако няма, да добавим) 
    */
    constructor(itemExpirationTimeSpan: number, createHash: (input: TItem) => string) {
        this._itemExpirationTimeSpan = itemExpirationTimeSpan;
        this._itemsDictionary = new Dictionary<string, ThrottlerDictionaryElem<TResult>>();
        this._createHash = createHash;
    }

    /**item - обектът, върху който да се изпълни actionPromiseFunc-а;
    actionPromiseFunc - действието, което да се изпълни*/
    public triggerAction(item: TItem, actionPromiseFunc: (item: TItem) => Promise<TResult>): Promise<TResult> {
        let itemHash: string = this._createHash(item);

        if (this.hasToTriggerActionForItem(itemHash)) {
            this.clearOldItemsInDictionary();
            this.addItemTriggeredList(itemHash);

            return actionPromiseFunc(item).then(result => {
                this._itemsDictionary.getValue(itemHash).result = result;

                return result;
            });
        }

        return Promise.resolve<TResult>(this._itemsDictionary.getValue(itemHash).result as TResult);
    }

    private hasToTriggerActionForItem(itemHash: string): boolean {
        if (this._itemsDictionary.getValue(itemHash) !== undefined) {
            if (this._itemsDictionary.getValue(itemHash).date.getTime() + this._itemExpirationTimeSpan > moment().toDate().getTime()) {
                return false;
            }
        }

        return true;
    }

    private clearOldItemsInDictionary(): void {
        for (var i = 0; i < this._itemsDictionary.keys().length; i++) {
            let key = this._itemsDictionary.keys()[i];
            if (this._itemsDictionary.getValue(key).date.getTime() + this._itemExpirationTimeSpan < moment().toDate().getTime()) {
                this._itemsDictionary.remove(key);
            }
        }
    }

    private addItemTriggeredList(itemHash: string): void {
        this._itemsDictionary.setValue(itemHash, { date: moment().toDate() });
    }
}
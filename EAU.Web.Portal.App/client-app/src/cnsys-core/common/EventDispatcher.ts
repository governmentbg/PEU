
export interface IEventDispatcher {
    addEventListener(event: string, handler: (...args: any[]) => void): void;
    removeEventListener(event: string, handler: (...args: any[]) => void): void;
    dispatchEvent(event: string, ...args: any[]): void;
}

export class EventDispatcher implements IEventDispatcher {
    private _callbacks: any;

    addEventListener(event: string, handler: (...args: any[]) => void): void {
        this._callbacks = this._callbacks || {};
        (this._callbacks['$' + event] = this._callbacks['$' + event] || [])
            .push(handler);
    }

    removeEventListener(event: string, handler: (...args: any[]) => void): void {
        this._callbacks = this._callbacks || {};

        // all
        if (0 == arguments.length) {
            this._callbacks = {};
            return;
        }

        // specific event
        var callbacks = this._callbacks['$' + event];
        if (!callbacks) return;

        // remove all handlers
        if (1 == arguments.length) {
            delete this._callbacks['$' + event];
            return ;
        }

        // remove specific handler
        var cb: any;
        for (var i = 0; i < callbacks.length; i++) {
            cb = callbacks[i];
            if (cb === handler || cb.fn === handler) {
                callbacks.splice(i, 1);
                break;
            }
        }
    }


    dispatchEvent(event: string, ...a: any[]): void {
        this._callbacks = this._callbacks || {};
        var args = [].slice.call(arguments, 1)
            , callbacks = this._callbacks['$' + event];

        if (callbacks) {
            callbacks = callbacks.slice(0);
            for (var i = 0, len = callbacks.length; i < len; ++i) {
                callbacks[i].apply(this, args);
            }
        }
    }

    static mixin(obj: any) {
        for (var key in EventDispatcher.prototype) {
            obj[key] = (<any>EventDispatcher.prototype)[key];
        }
        return obj;
    }
    static mixinType(type: any) {
        this.mixin(type.prototype);
    }
}
var ObservableEvent = (function () {
    function ObservableEvent() {
        var subs = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            subs[_i - 0] = arguments[_i];
        }
        this.subscribers = subs || [];
    }
    ObservableEvent.prototype.subscribe = function (fn) {
        this.subscribers.push(fn);
    };
    ObservableEvent.prototype.notify = function (args) {
        this.subscribers.forEach(function (fn) { return fn(args); });
    };
    return ObservableEvent;
}());
//# sourceMappingURL=ObservableEvent.js.map
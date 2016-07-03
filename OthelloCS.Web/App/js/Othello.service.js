var Service = new (function () {
    function service() {
        this.post = function (url, data) {
            return $.ajax({
                type: 'POST',
                url: url,
                data: JSON.stringify(data),
                contentType: 'application/json'
            });
        };
    }
    service.prototype.getNewMatch = function (req) {
        return this.post("/api/othello/new", req);
    };
    service.prototype.getMoveResult = function (req) {
        return this.post("/api/othello/move", req);
    };
    return service;
}());
//# sourceMappingURL=Othello.service.js.map
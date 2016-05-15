var Service = new (function () {
    function service() {
    }
    service.prototype.getNewMatch = function (req) {
        return this.post("/api/othello/new", req);
    };
    service.prototype.getMoveResult = function (req) {
        return this.post("/api/othello/move", req);
    };
    service.prototype.post = function (url, data) {
        return $.ajax({
            type: 'POST',
            url: url,
            data: JSON.stringify(data),
            contentType: 'application/json'
        });
    };
    return service;
}());

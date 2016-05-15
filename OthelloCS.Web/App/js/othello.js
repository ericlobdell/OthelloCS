var Othello = new (function () {
    function othello() {
        var _this = this;
        this.init = function (mode) {
            var newMatchRequest = {
                PlayerOneName: "Eric",
                PlayerTwoName: "Santa",
                GameMode: mode
            };
            Service.getNewMatch(newMatchRequest)
                .then(function (response) {
                console.log("SUCCESS: ", response);
                _this.match.MatchId = response.MatchId;
                _this.match.GameMode = response.GameMode;
                _this.match.CurrentPlayer = response.CurrentPlayer;
                _this.match.Gameboard = response.Gameboard;
                _this.match.Players = response.Players;
                View.updateScoreBoards(response.Players, response.CurrentPlayer);
                View.renderGameboard(response.Gameboard, []);
            });
        };
        this.handleOnMove = function (moveArgs) {
            var moveRequest = {
                Row: moveArgs.Row,
                Column: moveArgs.Column,
                PlayerNumber: _this.match.CurrentPlayer,
                MatchId: _this.match.MatchId,
                GameMode: _this.match.GameMode,
                Gameboard: _this.match.Gameboard,
                Players: _this.match.Players
            };
            Service.getMoveResult(moveRequest)
                .then(function (response) {
                console.log("MOVE SUCCESS: ", response);
                if (response.IsEndOfGame) {
                    View.announceWinner(response.Winner);
                }
                else {
                    _this.match.CurrentPlayer = response.Result.CurrentPlayer;
                    _this.match.Gameboard = response.Result.Gameboard;
                    View.updateScoreBoards(response.Players, response.Result.CurrentPlayer);
                    View.renderGameboard(response.Result.Gameboard, response.Result.Captures);
                }
            });
        };
        this.match = new Match();
        View.onGameModeSelect.subscribe(this.init);
        View.onMove.subscribe(this.handleOnMove);
    }
    return othello;
}());

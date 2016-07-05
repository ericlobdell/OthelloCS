var Othello = new (function () {
    function othello() {
        var _this = this;
        this.onGameModeSelect = function (mode) {
            var newMatchRequest = {
                PlayerOneName: "Eric",
                PlayerTwoName: "Santa",
                GameMode: mode
            };
            Service.getNewMatch(newMatchRequest)
                .then(function (response) {
                _this.match.MatchId = response.MatchId;
                _this.match.GameMode = response.GameMode;
                _this.match.CurrentPlayer = response.CurrentPlayer;
                _this.match.Gameboard = response.Gameboard;
                _this.match.Players = response.Players;
                View.updateScoreBoards(response.Players, response.CurrentPlayer);
                View.renderGameboard(response.Gameboard);
            });
        };
        this.onMove = function (moveArgs) {
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
                var result = response.Result;
                if (result.IsEndOfMatch) {
                    View.announceWinner(response.Winner);
                }
                else {
                    _this.match.CurrentPlayer = result.CurrentPlayer;
                    _this.match.Gameboard = result.Gameboard;
                    if (result.ComputerMadeMove) {
                        var moveMessage = "Othello is taking position (" + result.ComputerMove.Row + "," + result.ComputerMove.Column + "), based on " + result.Criteria;
                        var self_1 = _this;
                        console.log(moveMessage);
                        setTimeout(function () {
                            self_1.onMove({
                                Row: result.ComputerMove.Row,
                                Column: result.ComputerMove.Column
                            });
                        }, 200);
                    }
                    View.updateScoreBoards(response.Players, result.CurrentPlayer);
                    View.renderGameboard(result.Gameboard);
                    View.animateCapturedGamePieces(result.Captures);
                }
            });
        };
        this.match = new Match();
        View.onGameModeSelect.subscribe(this.onGameModeSelect);
        View.onMove.subscribe(this.onMove);
    }
    return othello;
}());

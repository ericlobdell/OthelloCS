var View = new (function () {
    function view() {
        var _this = this;
        this.onMove = new ObservableEvent();
        this.onGameModeSelect = new ObservableEvent();
        $(function () {
            var $shell = $(".shell");
            $(".game-board").on("click", ".cell", function () {
                var $cell = $(this);
                if (!$cell.data("is-target"))
                    return;
                _this.onMove.notify({
                    Row: +$cell.data("row-num"),
                    Column: +$cell.data("col-num"),
                    IsHighScoring: $cell.data("is-highest-scoring")
                });
            });
            $shell
                .on("change", ".show-move-marks", function () {
                if (this.checked)
                    $shell.addClass('show-marks');
                else
                    $shell.removeClass('show-marks');
            })
                .on("change", ".show-logging", function () {
                if (this.checked)
                    $shell.addClass('show-logging');
                else
                    $shell.removeClass('show-logging');
            })
                .on("click", ".game-mode-button", function () {
                var mode = $(this).data("game-mode");
                var $chooseView = $(".choose-game-mode-view");
                $chooseView.addClass("animated-fast fadeOut");
                setTimeout(function () {
                    $chooseView.hide();
                    $(".game-view")
                        .show()
                        .addClass("animated-fast fadeIn");
                    _this.onGameModeSelect.notify(mode);
                }, 250);
            });
        });
    }
    view.prototype.renderGameboard = function (gameBoard, opponentCaptures) {
        var html = "";
        gameBoard.Positions.forEach(function (row) {
            row.forEach(function (cell) {
                var cellContents = cell.PlayerNumber ?
                    "<div class='player-game-piece'></div>" : '';
                html += "<div class='cell'\n                              data-distance=\"" + cell.Distance + "\"\n                              data-is-target=\"" + cell.IsTarget + "\"\n                              data-is-highest-scoring=\"" + cell.IsHighestScoring + "\"\n                              data-player-num=\"" + cell.PlayerNumber + "\"\n                              data-row-num='" + cell.Row + "'\n                              data-col-num='" + cell.Column + "'>" + cellContents + "</div>";
            });
        });
        $(".game-board").html(html);
        opponentCaptures
            .map(function (c) { return c.distance; })
            .filter(function (d, i, uniqueDistances) { return uniqueDistances.indexOf(d) === i; })
            .sort(function (d1, d2) { return d1 - d2; })
            .forEach(function (d, i) {
            setTimeout(function () {
                $("[data-distance='" + d + "'] .player-game-piece")
                    .addClass("animated-fast pulse");
            }, 75 * i);
        });
    };
    view.prototype.updateScoreBoards = function (players, currentPlayer) {
        players.forEach(function (player) {
            var $playerSoreBoard = $(".score-board.player-" + player.number);
            $playerSoreBoard
                .find(".score")
                .html(player.score.toString());
            if (player.number === currentPlayer)
                $playerSoreBoard
                    .addClass("active");
            else
                $playerSoreBoard
                    .removeClass("active");
        });
    };
    view.prototype.announceWinner = function (winner) {
        var $winningScoreBoard;
        if (winner)
            $winningScoreBoard = $(".score-board.player-" + winner);
        else
            $winningScoreBoard = $(".score-board");
        $winningScoreBoard.addClass("active animated tada");
    };
    view.prototype.updateLogging = function (entry) {
        var $log = $(".logging-container");
        $log.append(entry);
        $log.animate({ scrollTop: $log.prop("scrollHeight") }, 975);
    };
    return view;
}())();
//# sourceMappingURL=othello.view.js.map
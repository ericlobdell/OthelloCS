
const View = new class view {

    onMove: ObservableEvent;
    onGameModeSelect: ObservableEvent;

    constructor() {
        const _this = this;

        this.onMove = new ObservableEvent();
        this.onGameModeSelect = new ObservableEvent();

        $(() => {
            const $shell = $( ".shell" );

            $( ".game-board" ).on( "click", ".cell", function () {
                const $cell = $( this );

                if ( !$cell.data( "is-target" ) )
                    return;

                _this.onMove.notify( {
                    Row: +$cell.data( "row-num" ),
                    Column: +$cell.data( "col-num" ),
                    IsHighScoring: $cell.data( "is-highest-scoring" )
                });

            });

            $shell
                .on( "change", ".show-move-marks", function () {
                    if ( this.checked )
                        $shell.addClass( 'show-marks' );
                    else
                        $shell.removeClass( 'show-marks' );
                })
                .on( "change", ".show-logging", function () {
                    if ( this.checked )
                        $shell.addClass( 'show-logging' );
                    else
                        $shell.removeClass( 'show-logging' );

                })
                .on( "click", ".game-mode-button", function () {
                    const mode = $( this ).data( "game-mode" );
                    const $chooseView = $( ".choose-game-mode-view" );

                    $chooseView.addClass( "animated-fast fadeOut" );

                    setTimeout(() => {
                        $chooseView.hide();
                        $( ".game-view" )
                            .show()
                            .addClass( "animated-fast fadeIn" );

                        _this.onGameModeSelect.notify( mode );
                    }, 250 );

                });

        });

    }

    renderGameboard( gameBoard: IGameboard, opponentCaptures: ICell[] ) {
        let html = "";

        gameBoard.Positions
            .forEach( row => {
                row.forEach( cell => {

                    const cellContents = cell.PlayerNumber ?
                        `<div class='player-game-piece'></div>` : '';

                    html += `<div class='cell'
                              data-distance="${cell.Distance}"
                              data-is-target="${cell.IsTarget}"
                              data-is-highest-scoring="${cell.IsHighestScoring}"
                              data-player-num="${cell.PlayerNumber}"
                              data-row-num='${cell.Row}'
                              data-col-num='${cell.Column}'>${cellContents}</div>`;
                });
            });

        $( ".game-board" ).html( html );

        opponentCaptures
            .map( c => c.Distance )
            .filter(( d, i, uniqueDistances ) => uniqueDistances.indexOf( d ) === i )
            .sort(( d1, d2 ) => d1 - d2 )
            .forEach(( d, i ) => {
                setTimeout(() => {
                    $( `[data-distance='${d}'] .player-game-piece` )
                        .addClass( `animated-fast pulse` );
                }, 75 * i );
            });
    }

    updateScoreBoards( players: IPlayer[], currentPlayer: number ) {
        players.forEach( player => {
            const $playerScoreBoard = $( `.score-board.player-${player.Number}` );

            $playerScoreBoard
                .find( ".score" )
                .html( player.Score.toString() );

            $playerScoreBoard
                .find( ".name" )
                .html( player.Name );

            if ( player.Number === currentPlayer )
                $playerScoreBoard
                    .addClass( "active" );
            else
                $playerScoreBoard
                    .removeClass( "active" );

        });
    }

    announceWinner( winner: IPlayer ) {
        let $winningScoreBoard;

        if ( winner )
            $winningScoreBoard = $( `.score-board.player-${winner.Number}` );
        else
            $winningScoreBoard = $( `.score-board` );

        $winningScoreBoard.addClass( "active animated tada" );
    }

    updateLogging( entry: string ) {
        const $log = $( ".logging-container" );

        $log.append( entry );
        $log.animate( { scrollTop: $log.prop( "scrollHeight" ) }, 975 );
    }

}();


const Othello = new class othello {
    private match: Match;

    constructor() {
        this.match = new Match();

        View.onGameModeSelect.subscribe( this.onGameModeSelect );
        View.onMove.subscribe( this.onMove );
    }

    public onGameModeSelect = ( mode: number ) => {

        const newMatchRequest = {
            PlayerOneName: "Eric",
            PlayerTwoName: "Santa",
            GameMode: mode
        };

        Service.getNewMatch( newMatchRequest )
            .then(( response: INewMatchResponse ) => {

                this.match.MatchId = response.MatchId;
                this.match.GameMode = response.GameMode;
                this.match.CurrentPlayer = response.CurrentPlayer;
                this.match.Gameboard = response.Gameboard;
                this.match.Players = response.Players

                View.updateScoreBoards( response.Players, response.CurrentPlayer );
                View.renderGameboard( response.Gameboard );
            });
    }

    public onMove = ( moveArgs: IMoveArguments ) => {

        const moveRequest = {
            Row: moveArgs.Row,
            Column: moveArgs.Column,
            PlayerNumber: this.match.CurrentPlayer,
            MatchId: this.match.MatchId,
            GameMode: this.match.GameMode,
            Gameboard: this.match.Gameboard,
            Players: this.match.Players
        };

        Service.getMoveResult( moveRequest )
            .then(( response: IMoveResponse ) => {
                console.log( "MOVE SUCCESS: ", response );

                if ( response.IsEndOfGame ) {
                    View.announceWinner( response.Winner )
                }
                else {
                    this.match.CurrentPlayer = response.Result.CurrentPlayer;
                    this.match.Gameboard = response.Result.Gameboard;

                    View.updateScoreBoards( response.Players, response.Result.CurrentPlayer );
                    View.renderGameboard( response.Result.Gameboard );
                    View.animateCapturedGamePieces( response.Result.Captures );
                }
            });
    }
}


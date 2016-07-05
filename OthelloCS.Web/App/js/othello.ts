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

                const result = response.Result;

                if ( result.IsEndOfMatch ) {
                    View.announceWinner( response.Winner );
                }
                else  {
                    this.match.CurrentPlayer = result.CurrentPlayer;
                    this.match.Gameboard = result.Gameboard;

                    if ( result.ComputerMadeMove ) {
                        console.log( "Computer made move: ", result.ComputerMove );
                        console.log( "Move criteria: " + result.Criteria );
                        const moveMessage = `Othello is taking position (${result.ComputerMove.Row},${result.ComputerMove.Column}), based on ${result.Criteria}`;

                        let self = this;
                        setTimeout( function () {
                            console.log( moveMessage );
                            self.onMove( {
                                Row: result.ComputerMove.Row,
                                Column: result.ComputerMove.Column
                            });
                        }, 2000);
                        
                    }   
                    else {
                        View.updateScoreBoards( response.Players, result.CurrentPlayer );
                        View.renderGameboard( result.Gameboard );
                        View.animateCapturedGamePieces( result.Captures ); 
                    }
                }

            });
    }
}


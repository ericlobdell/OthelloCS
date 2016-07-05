using OthelloCS.Interfaces;
using OthelloCS.Models;
using OthelloCS.Services;
using System;
using System.Linq;
using System.Threading;

namespace OthelloCS.Strategies
{
    public class SinglePlayerGameModeStrategy : BaseGameModeStrategy, IGameModeStrategy
    {
        private const int ComputerPlayerNumber = 2;

        public void HandleEndOfGame( )
        {
            throw new NotImplementedException( );
        }

        public MoveResult OnMove( Move move, Guid matchId, Gameboard gameBoard )
        {
            var isEndOfMatch = false;
            var cleanGameBoard = BoardManager.ResetMoveRatings( gameBoard );
            Move computerMove = null;
            var computerMoveCriteria = "";

            move.Captures = ScoreKeeper.GetMoveCaptures( move.Row, move.Column, move.PlayerNumber, cleanGameBoard );

            var gameBoardAfterMove = BoardManager.RecordMove( move, cleanGameBoard );

            var nextPlayerNumber = GetOtherPlayerNumber( move.PlayerNumber );
            var nextPlayerPotentialMoves = ScoreKeeper.GetNextMovesForPlayer( nextPlayerNumber, gameBoardAfterMove );

            if ( nextPlayerPotentialMoves.Any( ) )
            {
                gameBoardAfterMove = BoardManager.MapNextMoveTargets( nextPlayerPotentialMoves, gameBoardAfterMove );

                if ( nextPlayerNumber == ComputerPlayerNumber )
                {
                    var moveSelection = OthelloAI.MakeMove( nextPlayerPotentialMoves );
                    computerMove = new Move( moveSelection.Position.Row, moveSelection.Position.Column, nextPlayerNumber );
                    computerMoveCriteria = moveSelection.Criteria;
                }
            }
               
            else
            {
                nextPlayerNumber = move.PlayerNumber;
                var currentPlayerPotentialMoves = ScoreKeeper.GetNextMovesForPlayer( move.PlayerNumber, gameBoardAfterMove );

                if ( currentPlayerPotentialMoves.Any( ) )
                {
                    gameBoardAfterMove = BoardManager.MapNextMoveTargets( currentPlayerPotentialMoves, gameBoardAfterMove );

                    if ( nextPlayerNumber == ComputerPlayerNumber )
                    {
                        var moveSelection = OthelloAI.MakeMove( currentPlayerPotentialMoves );
                        computerMove = new Move( moveSelection.Position.Row, moveSelection.Position.Column, nextPlayerNumber );
                        computerMoveCriteria = moveSelection.Criteria;
                    }
                }
                else
                {
                    isEndOfMatch = true;
                }
                    
            }

            return new MoveResult
            {
                CurrentPlayer = nextPlayerNumber,
                Gameboard = gameBoardAfterMove,
                MatchId = matchId,
                Captures = move.Captures,
                IsEndOfMatch = isEndOfMatch,
                ComputerMove = computerMove,
                Criteria = computerMoveCriteria
            };
        }
    }
}

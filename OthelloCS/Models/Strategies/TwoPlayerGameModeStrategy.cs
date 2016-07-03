using OthelloCS.Interfaces;
using OthelloCS.Services;
using System;
using System.Linq;

namespace OthelloCS.Models
{
    public class TwoPlayerGameModeStrategy : BaseGameModeStrategy, IGameModeStrategy
    {
        public void HandleEndOfGame( )
        {
            throw new NotImplementedException( );
        }

        public MoveResult OnMove( Move move, Guid matchId, Gameboard gameBoard )
        {
            var isEndOfMatch = false;
            var cleanGameBoard = BoardManager.ResetMoveRatings( gameBoard );

            move.Captures = ScoreKeeper.GetMoveCaptures( move.Row, move.Column, move.PlayerNumber, cleanGameBoard );

            var gameBoardAfterMove = BoardManager.RecordMove( move, cleanGameBoard );

            var nextPlayerNumber = GetOtherPlayerNumber( move.PlayerNumber );
            var nextPlayerPotentialMoves = ScoreKeeper.GetNextMovesForPlayer( nextPlayerNumber, gameBoardAfterMove );

            if ( nextPlayerPotentialMoves.Any( ) )
                gameBoardAfterMove = BoardManager.MapNextMoveTargets( nextPlayerPotentialMoves, gameBoardAfterMove );
            else
            {
                nextPlayerNumber = move.PlayerNumber;
                var currentPlayerPotentialMoves = ScoreKeeper.GetNextMovesForPlayer( move.PlayerNumber, gameBoardAfterMove );

                if ( currentPlayerPotentialMoves.Any( ) )
                    gameBoardAfterMove = BoardManager.MapNextMoveTargets( currentPlayerPotentialMoves, gameBoardAfterMove );
                else
                    isEndOfMatch = true;
            }
                

            return new MoveResult( nextPlayerNumber, gameBoardAfterMove, matchId, move.Captures, isEndOfMatch );
        }
    }
}

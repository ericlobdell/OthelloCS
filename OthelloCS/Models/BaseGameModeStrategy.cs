using OthelloCS.Interfaces;
using OthelloCS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloCS.Models
{
    public class BaseGameModeStrategy
    {
        public MoveResult GetMoveResult( MatchAction action, int nextPlayer)
        {
            var move = ScoreKeeper.MakeMove( action.Row, action.Column, action.PlayerNumber, action.Gameboard );
            var gb = BoardManager.RecordMove( move, action.Gameboard );
            var nextMoves = ScoreKeeper.GetNextMovesForPlayer( nextPlayer, gb );
            var updatedGameboard = BoardManager.MapNextMoves( nextMoves, gb );

            return new MoveResult( nextPlayer, updatedGameboard, action.MatchId, move.Captures );
        }
    }
}

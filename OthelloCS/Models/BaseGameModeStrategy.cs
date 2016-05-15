using OthelloCS.Services;

namespace OthelloCS.Models
{
    public class BaseGameModeStrategy
    {
        public MoveResult GetMoveResult( MoveRequest moveRequest, int nextPlayer)
        {
            var move = ScoreKeeper.MakeMove( moveRequest.Row, moveRequest.Column, moveRequest.PlayerNumber, moveRequest.Gameboard );
            var gb = BoardManager.RecordMove( move, moveRequest.Gameboard );
            var nextMoves = ScoreKeeper.GetNextMovesForPlayer( nextPlayer, gb );
            var updatedGameboard = BoardManager.MapNextMoves( nextMoves, gb );

            return new MoveResult( nextPlayer, updatedGameboard, moveRequest.MatchId, move.Captures );
        }
    }
}

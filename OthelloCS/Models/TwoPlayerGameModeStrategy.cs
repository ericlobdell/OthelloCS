using OthelloCS.Interfaces;
using System;

namespace OthelloCS.Models
{
    public class TwoPlayerGameModeStrategy : BaseGameModeStrategy, IGameModeStrategy
    {
        public void HandleEndOfGame( )
        {
            throw new NotImplementedException( );
        }

        public MoveResult OnMove( MoveRequest action )
        {
            var nextPlayer = action.PlayerNumber == 1 ? 2 : 1;
            return GetMoveResult( action, nextPlayer );
        }
    }
}

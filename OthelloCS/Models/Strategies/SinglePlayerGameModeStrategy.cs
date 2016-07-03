using OthelloCS.Interfaces;
using System;

namespace OthelloCS.Models
{
    public class SinglePlayerGameModeStrategy : IGameModeStrategy
    {

        public void HandleEndOfGame( )
        {
            throw new NotImplementedException( );
        }

        public MoveResult OnMove( Move move, Guid matchId, Gameboard gameBoard )
        {
            throw new NotImplementedException( );
        }
    }
}

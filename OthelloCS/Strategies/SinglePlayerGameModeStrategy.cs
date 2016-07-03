using OthelloCS.Interfaces;
using OthelloCS.Models;
using System;

namespace OthelloCS.Strategies
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

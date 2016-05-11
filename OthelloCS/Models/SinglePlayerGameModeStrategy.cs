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

        public MoveResult OnMove( MoveRequest action )
        {
            throw new NotImplementedException( );
        }
    }
}

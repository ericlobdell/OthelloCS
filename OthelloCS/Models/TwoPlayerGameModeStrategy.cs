using OthelloCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloCS.Models
{
    public class TwoPlayerGameModeStrategy : BaseGameModeStrategy, IGameModeStrategy
    {
        public void HandleEndOfGame( )
        {
            throw new NotImplementedException( );
        }

        public MoveResult OnMove( MatchAction action )
        {
            var nextPlayer = action.PlayerNumber == 1 ? 2 : 1;
            return GetMoveResult( action, nextPlayer );
        }
    }
}

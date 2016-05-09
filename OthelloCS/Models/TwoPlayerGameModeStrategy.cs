using OthelloCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloCS.Models
{
    public class TwoPlayerGameModeStrategy : IGameModeStrategy
    {
        public void HandleEndOfGame( )
        {
            throw new NotImplementedException( );
        }



        public MoveResult OnMoveCompleted( Move move, Gameboard gameBoard, Guid matchId, List<Cell> captures )
        {
            var nextPlayer = move.PlayerNumber == 1 ? 2 : 1;
            return new MoveResult( nextPlayer, gameBoard, matchId, captures );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloCS.Models
{
    public class MoveResult
    {
        public int CurrentPlayer { get; }
        public Gameboard Gameboard { get; }

        public MoveResult( int playerNumber, Gameboard gameBoard )
        {
            CurrentPlayer = playerNumber;
            Gameboard = gameBoard;
        }
    }
}

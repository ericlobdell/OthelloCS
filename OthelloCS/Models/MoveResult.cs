using System;
using System.Collections.Generic;

namespace OthelloCS.Models
{
    public class MoveResult
    {
        public int CurrentPlayer { get; }
        public Gameboard Gameboard { get; set; }
        public Guid MatchId { get; }
        public List<Cell> Captures { get; }

        public MoveResult( int player, Gameboard gameBoard, Guid matchId, List<Cell> captures )
        {
            CurrentPlayer = player;
            Gameboard = gameBoard;
            MatchId = matchId;
            Captures = captures;
        }
    }
}

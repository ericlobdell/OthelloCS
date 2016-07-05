using System;
using System.Collections.Generic;

namespace OthelloCS.Models
{
    public class MoveResult
    {
        public string Criteria { get; set; }
        public int CurrentPlayer { get; set; }
        public Gameboard Gameboard { get; set; }
        public Guid MatchId { get; set; }
        public List<Cell> Captures { get; set; }
        public bool IsEndOfMatch { get; set; }
        public Move ComputerMove { get; set; }
        public bool ComputerMadeMove { get { return ComputerMove != null; } }

        //public MoveResult( int player, Gameboard gameBoard, Guid matchId, List<Cell> captures, bool isEndOfMatch )
        //{
        //    CurrentPlayer = player;
        //    Gameboard = gameBoard;
        //    MatchId = matchId;
        //    Captures = captures;
        //    IsEndOfMatch = isEndOfMatch;
        //}
    }
}

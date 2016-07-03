using OthelloCS.Models;
using System;
using System.Collections.Generic;

namespace OthelloCS.Web.Models
{
    public class MoveRequest
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int PlayerNumber { get; set; }
        public Gameboard Gameboard { get; set; }
        public Guid MatchId { get; set; }
        public GameMode GameMode { get; set; }
        public List<Player> Players { get; set; }

    }
}
using System;
using System.Collections.Generic;

namespace OthelloCS.Models
{
    public class NewMatchResponse
    {
        public PlayerNumber CurrentPlayer { get; set; }
        public List<Player> Players { get; set; }
        public Gameboard Gameboard { get; set; }
        public Guid MatchId { get; set; }
        public GameMode GameMode { get; set; }
        public NewMatchResponse( )
        {
            Players = new List<Player>( );
        }
    }
}
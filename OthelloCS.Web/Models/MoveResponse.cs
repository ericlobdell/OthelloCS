using OthelloCS.Models;
using System.Collections.Generic;

namespace OthelloCS.Web.Models
{
    public class MoveResponse
    {
        public MoveResult Result { get; set; }
        public List<Player> Players { get; set; }
        public bool IsEndOfMatch { get; set; }
        public Player Winner { get; set; }

        public MoveResponse( )
        {
            Players = new List<Player>( );
        }
    }
}
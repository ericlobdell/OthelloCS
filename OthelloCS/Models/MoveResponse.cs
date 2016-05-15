using System.Collections.Generic;

namespace OthelloCS.Models
{
    public class MoveResponse
    {
        public MoveResult Result { get; set; }
        public List<Player> Players { get; set; }
        public bool IsEndOfGame { get; set; }
        public Player Winner { get; set; }

        public MoveResponse( )
        {
            Players = new List<Player>( );
        }
    }
}
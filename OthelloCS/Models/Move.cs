using System.Collections.Generic;

namespace OthelloCS.Models
{
    public class Move
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int PlayerNumber { get; set; }
        public List<Cell> Captures { get; set; }
        public int PointValue { get { return Captures.Count; } }
        public bool IsScoringMove { get { return PointValue > 0; } }

        public Move( int row, int col, int playerNumber )
        {
            Row = row;
            Column = col;
            PlayerNumber = playerNumber;
            Captures = new List<Cell>( );
        }
    }
}

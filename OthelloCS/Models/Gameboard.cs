using System.Collections.Generic;

namespace OthelloCS.Models
{
    public class Gameboard
    {

        public List< List<Cell>> Positions { get; set; }

        public Gameboard( )
        {
            Positions = GetInitialGameboard( );
        }

        private List<List<Cell>> GetInitialGameboard( )
        {
            List<List<Cell>> positions = new List<List<Cell>>( );
            List<Cell> cells;
            for ( int row = 0; row < 8; row++ )
            {
                cells = new List<Cell>( );
                for ( int col = 0; col < 8; col++ )
                {
                    cells.Add( new Cell
                    {
                        Row = row,
                        Column = col,
                        PlayerNumber = GetInitialPlayerNumber( row, col ),
                        IsTarget = CellIsInitialTarget( row, col )
                    } );
                }
                positions.Add( cells );
            }

            return positions;
        }

        public bool CellIsInitialTarget( int row, int col )
        {
            return ( row == 2 && col == 4 ||
                     row == 3 && col == 5 ||
                     row == 4 && col == 2 ||
                     row == 5 && col == 3 );
        }

        public int GetInitialPlayerNumber( int row, int col )
        {
            var playerNumber = 0;

            if ( ( row == 3 && col == 3 ) || ( row == 4 && col == 4 ) )
                playerNumber = 1;

            if ( ( row == 3 && col == 4 ) || ( row == 4 && col == 3 ) )
                playerNumber = 2;

            return playerNumber;
        }

    }

    
}

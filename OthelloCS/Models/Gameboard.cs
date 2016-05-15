using System.Collections.Generic;

namespace OthelloCS.Models
{
    public class Gameboard
    {

        public List<List<Cell>> Positions { get; set; }

        public Gameboard( )
        {
            Positions = GetInitialGameboard( );
        }

        private Gameboard( List<List<Cell>> positions )
        {
            Positions = positions;
        }

        public static Gameboard Empty
        {
            get
            {
                var positions = new List<List<Cell>>( );
                List<Cell> cells;
                for ( int row = 0; row < 8; row++ )
                {
                    cells = new List<Cell>( );
                    for ( int col = 0; col < 8; col++ )
                    {
                        cells.Add( new Cell
                        {
                            Row = row,
                            Column = col
                        } );
                    }
                    positions.Add( cells );
                }

                return new Gameboard( positions );
            }

        }

        private List<List<Cell>> GetInitialGameboard( )
        {
            var gameBoard = Gameboard.Empty;

            gameBoard.Positions [ 2 ] [ 4 ].IsTarget = true;
            gameBoard.Positions [ 3 ] [ 5 ].IsTarget = true;
            gameBoard.Positions [ 4 ] [ 2 ].IsTarget = true;
            gameBoard.Positions [ 5 ] [ 3 ].IsTarget = true;

            gameBoard.Positions [ 3 ] [ 3 ].PlayerNumber = 1;
            gameBoard.Positions [ 4 ] [ 4 ].PlayerNumber = 1;
            gameBoard.Positions [ 3 ] [ 4 ].PlayerNumber = 2;
            gameBoard.Positions [ 4 ] [ 3 ].PlayerNumber = 2;

            return gameBoard.Positions;
        }
    }


}

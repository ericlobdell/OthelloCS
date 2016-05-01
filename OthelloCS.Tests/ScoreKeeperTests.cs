using OthelloCS.Models;
using OthelloCS.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OthelloCS.Tests
{
    public class ScoreKeeperTests
    {
        [Fact]
        public void GetScoreForPlayer_returns_number_of_positions_occupied_by_player()
        {
            var gameBoard = new Gameboard( );
            var playerNumber = 2;
            gameBoard.Positions [ 1 ] [ 1 ].PlayerNumber = playerNumber;

            var sut = ScoreKeeper.GetScoreForPlayer( playerNumber, gameBoard );

            Assert.Equal( sut, 3 );
        }

        

        [Fact]
        public void EvaluateCell_IsEmptyPosition_true_when_player_zero()
        {
            var cell = new Cell { PlayerNumber = 0 };
            var currentPlayerNumber = 1;

            var sut = ScoreKeeper.EvaluateCell( cell, currentPlayerNumber );

            Assert.True( sut.IsEmptyPosition );
        }

        [Fact]
        public void EvaluateCell_IsEmptyPosition_false_when_player_not_zero( )
        {
            var cell = new Cell { PlayerNumber = 1 };
            var currentPlayerNumber = 1;

            var sut = ScoreKeeper.EvaluateCell( cell, currentPlayerNumber );

            Assert.False( sut.IsEmptyPosition );
        }

        [Fact]
        public void EvaluateCell_IsEmptyPosition_false_when_cell_null( )
        {
            Cell cell = null;
            var currentPlayerNumber = 0;

            var sut = ScoreKeeper.EvaluateCell( cell, currentPlayerNumber );

            Assert.False( sut.IsEmptyPosition );
        }

        [Fact]
        public void EvaluateCell_IsOpponentPosition_false_when_player_current( )
        {
            var cell = new Cell { PlayerNumber = 1 };
            var currentPlayerNumber = 1;

            var sut = ScoreKeeper.EvaluateCell( cell, currentPlayerNumber );

            Assert.False( sut.IsOpponentPosition );
        }

        [Fact]
        public void EvaluateCell_IsOpponentosition_false_when_cell_null( )
        {
            Cell cell = null;
            var currentPlayerNumber = 0;

            var sut = ScoreKeeper.EvaluateCell( cell, currentPlayerNumber );

            Assert.False( sut.IsOpponentPosition );
        }

        [Fact]
        public void EvaluateCell_IsOpponentPosition_true_when_player_is_opponent( )
        {
            var cell = new Cell { PlayerNumber = 2 };
            var currentPlayerNumber = 1;

            var sut = ScoreKeeper.EvaluateCell( cell, currentPlayerNumber );

            Assert.True( sut.IsOpponentPosition );
        }

        [Fact]
        public void EvaluateCell_IsInvalidPosition_true_when_cell_null( )
        {
            Cell cell = null;
            var currentPlayerNumber = 1;

            var sut = ScoreKeeper.EvaluateCell( cell, currentPlayerNumber );

            Assert.True( sut.IsInvalidPosition );
        }

        [Fact]
        public void EvaluateCell_IsInvalidPosition_false_when_cell_not_null( )
        {
            Cell cell = new Cell { PlayerNumber = 1 };
            var currentPlayerNumber = 1;

            var sut = ScoreKeeper.EvaluateCell( cell, currentPlayerNumber );

            Assert.False( sut.IsInvalidPosition );
        }

        [Fact]
        public void GetDirectionalCaptures_returns_empty_list_when_location_off_gameboard()
        {
            var gameBoard = new Gameboard( );

            var sut = ScoreKeeper.GetDirectionalCaptures( -1, 3, 1, 1, 1, gameBoard );

            Assert.IsType<List<Cell>>( sut );
            Assert.Equal( 0, sut.Count );
        }

        [Fact]
        public void GetDirectionalCaptures_returns_empty_list_when_search_walks_off_gameboard( )
        {
            var gameBoard = new Gameboard( );
            var verticalSearchDirection = -1; // up
            var startingRow = 0; // topmost row
            
            var sut = ScoreKeeper.GetDirectionalCaptures( startingRow, 3, verticalSearchDirection, 1, 1, gameBoard );

            Assert.IsType<List<Cell>>( sut );
            Assert.Equal( 0, sut.Count );


            var horizontalSearchDirection = 1; // right
            var startingColumn = 7; // rightmost column

            var sut2 = ScoreKeeper.GetDirectionalCaptures( 3, startingColumn, 1, horizontalSearchDirection, 1, gameBoard );

            Assert.IsType<List<Cell>>( sut2 );
            Assert.Equal( 0, sut2.Count );
        }

        [Fact]
        public void GetDirectionalCaptures_returns_list_of_captured_cells_when_scoring_move()
        {
            var gameBoard = new Gameboard( );
            var verticalSearchDirection = 1; // down
            var startingRow = 3;
            var horizontalSearchDirection = 0; // no horzontal change
            var startingColumn = 4;
            var playerNumber = 1;

            // capture position 3,4 initially assigned to player 2
            Assert.Equal( 2, gameBoard.Positions [ startingRow ] [ startingColumn ].PlayerNumber );
            var sut = ScoreKeeper.GetDirectionalCaptures( startingRow, startingColumn, verticalSearchDirection, horizontalSearchDirection, playerNumber, gameBoard );

            Assert.IsType<List<Cell>>( sut );
            Assert.Equal( 1, sut.Count );
        }

        [Fact]
        public void GetDirectionalCaptures_returns_empty_list_when_encounters_current_player_occupied_position( )
        {
            var gameBoard = new Gameboard( );
            var verticalSearchDirection = 1; // down
            var startingRow = 3;
            var horizontalSearchDirection = 0; // no horzontal change
            var startingColumn = 4;
            var playerNumber = 2;

            // capture position 3,4 initially assigned to player 2
            Assert.Equal( 2, gameBoard.Positions [ startingRow ] [ startingColumn ].PlayerNumber );
            var sut = ScoreKeeper.GetDirectionalCaptures( startingRow, startingColumn, verticalSearchDirection, horizontalSearchDirection, playerNumber, gameBoard );


            Assert.IsType<List<Cell>>( sut );
            Assert.Equal( 0, sut.Count );
        }

        [Fact]
        public void GetNextMovesForPlayer_returns_list_of_valid_next_moves()
        {
            var gameBoard = new Gameboard( );
            var validMovesForPlayerOne = new List<Cell>
            {
                gameBoard.Positions[2][4],
                gameBoard.Positions[3][5],
                gameBoard.Positions[4][2],
                gameBoard.Positions[5][3]
            };

            var sut = ScoreKeeper.GetNextMovesForPlayer( 1, gameBoard );

            Assert.Equal( validMovesForPlayerOne.Count, sut.Count );
            validMovesForPlayerOne.ForEach( cell =>
            {
                Assert.True( sut.Any( c => c.Row == cell.Row && c.Column == cell.Column ) );
            } );
        }


        [Fact]
        public void GetNextMovesForPlayer_should_mark_all_moves_as_target( )
        {
            var gameBoard = new Gameboard( );

            var sut = ScoreKeeper.GetNextMovesForPlayer( 1, gameBoard );

            Assert.True( sut.All( cell => cell.IsTarget ) );
        }

        // MakeMove Tests






    }
}

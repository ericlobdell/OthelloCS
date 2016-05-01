using OthelloCS.Models;
using OthelloCS.Services;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace OthelloCS.Tests
{
    public class BoardManagerTests
    {
        [Theory]
        [InlineData(-1,3,false)]
        [InlineData( 1, 3, true )]
        [InlineData( 1, 8, false )]
        [InlineData( 8, 3, false )]
        [InlineData( 1, -1, false )]
        [InlineData( 4, 7, true )]
        public void IsValidMove_false_if_off_gameboard( int row, int col, bool expected )
        {
            var sut = BoardManager.IsValidMove( row, col );

            Assert.Equal( expected, sut );
        }

        [Fact]
        public void TyrGetCell_returns_copy_of_Cell_if_valid_move()
        {
            var gameBoard = new Gameboard( );
            var sut = BoardManager.TryGetCell( 2, 3, gameBoard );

            var expected = gameBoard.Positions [ 2 ] [ 3 ];

            Assert.Equal( expected.Row, sut.Row );
            Assert.Equal( expected.Column, sut.Column );
            Assert.NotSame( expected, sut );

        }

        [Fact]
        public void TyrGetCell_returns_null_if_invalid_move( )
        {
            var gameBoard = new Gameboard( );
            var sut = BoardManager.TryGetCell( -1, 3, gameBoard );

            Assert.Null( sut );
        }

        [Fact]
        public void GetFlatGameboard_returns_positions_as_flat_list()
        {
            var gameBoard = new Gameboard( );
            var expected = new List<Cell>( );

            gameBoard.Positions.ForEach( row =>
            {
                row.ForEach( cell => expected.Add( cell ) );
            } );

            var sut = BoardManager.GetFlatGameboard( gameBoard );

            Assert.IsType<List<Cell>>( sut );
            Assert.Equal( 64, sut.Count );
        }

        [Fact]
        public void CopyCell_returns_copy_of_Cell()
        {
            var cell = new Cell
            {
                Row = 1,
                Column = 2,
                IsTarget = true,
                IsHighestScoring = false, 
                IsHit = true,
                PointValue = 3,
                Distance = 2,
                PlayerNumber = 1
                
            };

            var sut = BoardManager.CopyCell( cell );

            Assert.NotSame( cell, sut );
            Assert.Equal( sut.Row, cell.Row );
            Assert.Equal( sut.Column, cell.Column );
            Assert.Equal( sut.IsTarget, cell.IsTarget );
            Assert.Equal( sut.IsHit, cell.IsHit );
            Assert.Equal( sut.IsHighestScoring, cell.IsHighestScoring );
            Assert.Equal( sut.PlayerNumber, cell.PlayerNumber );
            Assert.Equal( sut.Distance, cell.Distance );
            Assert.Equal( sut.PointValue, cell.PointValue );
        }

        [Fact]
        public void GetFlatGameboard_returns_copies_of_Cells_not_references( )
        {
            var gameBoard = new Gameboard( );
            var expected = new List<Cell>( );

            gameBoard.Positions.ForEach( row =>
            {
                row.ForEach( cell => expected.Add( cell ) );
            } );

            var sut = BoardManager.GetFlatGameboard( gameBoard );

            sut.ForEach( cell => 
                Assert.NotSame( cell, gameBoard.Positions [ cell.Row ] [ cell.Column ] ) );
        }

        [Fact]
        public void ResetTargetPositions_returns_new_gameboard_with_all_positions_as_not_target( )
        {
            var gameBoard = new Gameboard( );

            var sut = BoardManager.ResetTargetPositions( gameBoard );

            Assert.IsType<Gameboard>( sut );
            Assert.NotSame( sut, gameBoard );
            Assert.True( BoardManager.GetFlatGameboard( sut ).All( p => p.IsTarget == false ) );
        }

        [Fact]
        public void ResetTargetPositions_doesnt_mutate_gameBoard_passed_in( )
        {
            var gameBoard = new Gameboard( );

            var sut = BoardManager.ResetTargetPositions( gameBoard );

            Assert.False( BoardManager.GetFlatGameboard( gameBoard ).All( p => p.IsTarget == false ) );
        }

        [Fact]
        public void ResetMoveRatings_sets_move_rating_properties_to_defaults( )
        {
            var gameBoard = new Gameboard( );
            var cellWithValuesSet = new Cell { IsHighestScoring = true, IsHit = true, IsTarget = true };
            gameBoard.Positions [ 1 ] [ 1 ] = cellWithValuesSet;

            Assert.True( BoardManager.GetFlatGameboard( gameBoard ).Any( c => c.IsTarget ) );
            Assert.True( BoardManager.GetFlatGameboard( gameBoard ).Any( c => c.IsHit ) );
            Assert.True( BoardManager.GetFlatGameboard( gameBoard ).Any( c => c.IsHighestScoring ) );

            var sut = BoardManager.ResetMoveRatings( gameBoard );


            Assert.NotSame( gameBoard, sut );
            Assert.False( BoardManager.GetFlatGameboard( sut ).Any( c => c.IsTarget ) );
            Assert.False( BoardManager.GetFlatGameboard( sut ).Any( c => c.IsHit ) );
            Assert.False( BoardManager.GetFlatGameboard( sut ).Any( c => c.IsHighestScoring ) );
        }

        [Theory]
        [InlineData(2,3,true)] // -1, 0 - above
        [InlineData( 4, 3, true )] // +1, 0 - below
        [InlineData( 2, 2, true )] // -1, -1 = above left
        [InlineData( 4, 2, true )] // +1, -1 = below left
        [InlineData( 2, 4, true )] // -1, +1 = above right
        [InlineData( 4, 4, true )] // -1, -1 = below right
        [InlineData( 3, 2, true )] // 0, -1 = left
        [InlineData( 3, 4, true )] // 0, +1 = right
        [InlineData( 3, 3, false )] // 0, 0 = actual
        [InlineData( 5, 2, false )] // +2, -1 = OOB
        [InlineData( 2, 5, false )] // -1, +2 = OOB
        public void GetAdjacentCells_returns_list_of_surrounding_cells_from_gameboard( int row, int col, bool expected )
        {
            var gameBoard = new Gameboard( );
            var testCell = gameBoard.Positions [ 3 ] [ 3 ];

            var sut = BoardManager.GetAdjacentCells( testCell, gameBoard );

            Assert.Equal( expected, sut.Any( c => c.Row == row && c.Column == col ) );
        }

        [Theory]
        [InlineData( -1, 3, false )] // -1, 0 - above
        [InlineData( 1, 3, true )] // +1, 0 - below
        [InlineData( -1, 2, false )] // -1, -1 = above left
        [InlineData( 1, 2, true )] // +1, -1 = below left
        [InlineData( -1, 4, false )] // -1, +1 = above right
        [InlineData( -1, 4, false )] // -1, -1 = below right
        [InlineData( 0, 2, true )] // 0, -1 = left
        [InlineData( 0, 4, true )] // 0, +1 = right
        [InlineData( 0, 3, false )] // 0, 0 = actual
        [InlineData( 2, 2, false )] // +2, -1 = OOB
        [InlineData( -1, 5, false )] // -1, +2 = OOB
        public void GetAdjacentCells_returns_only_valid_positions( int row, int col, bool expected )
        {
            var gameBoard = new Gameboard( );
            var testCell = gameBoard.Positions [ 0 ] [ 3 ];

            var sut = BoardManager.GetAdjacentCells( testCell, gameBoard );

            Assert.Equal( expected, sut.Any( c => c.Row == row && c.Column == col ) );
        }

        [Fact]
        public void GetOpenAdjacentCells_returns_only_unoccupied_cells()
        {
            var gameBoard = new Gameboard( );
            var testCell = gameBoard.Positions [ 3 ] [ 3 ];

            var sut = BoardManager.GetOpenAdjacentCells( testCell, gameBoard );

            Assert.True( sut.All( cell => cell.PlayerNumber == 0 ) );
        }

        [Fact]
        public void RecordMove_maps_captured_positions_to_gameboard()
        {
            var gameBoard = new Gameboard( );
            var currentPlayerNumber = 1;
            var opponentPlayerNumber = 2;
            var captures = new List<Cell>
            {
                new Cell { Row = 3, Column = 1, PlayerNumber = opponentPlayerNumber },
                new Cell { Row = 3, Column = 2, PlayerNumber = opponentPlayerNumber }
            };
            var move = new Move( 3, 0, currentPlayerNumber );
            move.Captures = captures;

            Assert.NotEqual( gameBoard.Positions [ 3 ] [ 0 ].PlayerNumber, currentPlayerNumber );
            Assert.NotEqual( gameBoard.Positions [ 3 ] [ 1 ].PlayerNumber, currentPlayerNumber );
            Assert.NotEqual( gameBoard.Positions [ 3 ] [ 2 ].PlayerNumber, currentPlayerNumber );

            var sut = BoardManager.RecordMove( move, gameBoard );

            Assert.NotSame( sut, gameBoard );
            Assert.Equal( sut.Positions [ 3 ] [ 0 ].PlayerNumber, currentPlayerNumber );
            Assert.Equal( sut.Positions [ 3 ] [ 1 ].PlayerNumber, currentPlayerNumber );
            Assert.Equal( sut.Positions [ 3 ] [ 2 ].PlayerNumber, currentPlayerNumber );

        }

        [Fact]
        public void GetPositionDistance_returns_vertical_or_horizontal_distance_of_cell_from_move_position( )
        {
            var gameBoard = new Gameboard( );
            var move = new Move( 1, 1, 1 );

            var sut = BoardManager.GetPositionDistance( move, 1, 3 );
            Assert.Equal( 2, sut );

            var sut2 = BoardManager.GetPositionDistance( move, 4, 1 );
            Assert.Equal( 3, sut2 );

            var sut3 = BoardManager.GetPositionDistance( move, 4, 4 );
            Assert.Equal( 3, sut2 );

        }
    }
}

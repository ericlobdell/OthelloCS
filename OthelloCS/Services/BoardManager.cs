using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OthelloCS.Models;

namespace OthelloCS.Services
{
    public static class BoardManager
    {
        public static bool IsValidMove( int row, int col )
        {
            return ( row > -1 && row < 8 && col > -1 && col < 8 );
        }

        public static Cell TryGetCell( int row, int col, Gameboard gameBoard )
        {
            if ( IsValidMove( row, col ) )
                return CopyCell( gameBoard.Positions [ row ] [ col ] );

            return null;
        }

        public static Gameboard ResetTargetPositions( Gameboard gameBoard )
        {
            var gameBoardCopy = new Gameboard( );
            gameBoard.Positions.ForEach( row =>
                row.ForEach( cell => {
                    var copy = CopyCell( cell );
                    copy.IsTarget = false;
                    gameBoardCopy.Positions [ cell.Row ] [ cell.Column ] = copy;
                } ) );

            return gameBoardCopy;
        }

        public static List<Cell> GetFlatGameboard( Gameboard gameBoard )
        {
            var positions = new List<Cell>( );

            gameBoard.Positions.ForEach( row =>
                row.ForEach( cell => positions.Add( CopyCell(cell) ) ) );

            return positions;
        }

        public static Cell CopyCell( Cell cell )
        {
            return new Cell
            {
                Row = cell.Row,
                Column = cell.Column,
                IsTarget = cell.IsTarget,
                IsHit = cell.IsHit,
                IsHighestScoring = cell.IsHighestScoring,
                PlayerNumber = cell.PlayerNumber,
                PointValue = cell.PointValue,
                Distance = cell.Distance
            };
        }

        public static List<Cell> GetAdjacentCells( Gameboard gameBoard,  Cell cell )
        {
            var above = TryGetCell( cell.Row - 1, cell.Column, gameBoard );
            var aboveRight = TryGetCell( cell.Row - 1, cell.Column + 1, gameBoard );
            var aboveLeft = TryGetCell( cell.Row - 1, cell.Column - 1, gameBoard );
            var left = TryGetCell( cell.Row, cell.Column - 1, gameBoard );
            var right = TryGetCell( cell.Row, cell.Column + 1, gameBoard );
            var below = TryGetCell( cell.Row + 1, cell.Column, gameBoard );
            var belowLeft = TryGetCell( cell.Row + 1, cell.Column - 1, gameBoard );
            var belowRight = TryGetCell( cell.Row + 1, cell.Column + 1, gameBoard );

            return new List<Cell> {
                above, aboveLeft, aboveRight, left, right, below, belowLeft, belowRight }
                .Where( c => c != null )
                .ToList();
        }

        public static List<Cell> GetOpenAdjacentCells( Gameboard gameBoard, Cell cell )
        {
            return GetAdjacentCells( gameBoard, cell )
                .Where( c => c.PlayerNumber == 0 )
                .ToList( );
        }
    }
}

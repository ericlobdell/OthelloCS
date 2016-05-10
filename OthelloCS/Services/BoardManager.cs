﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var gameBoardCopy = Gameboard.Empty;

            gameBoard.Positions.ForEach( row =>
                row.ForEach( cell =>
                {
                    var copy = CopyCell( cell );
                    copy.IsTarget = false;

                    gameBoardCopy.Positions [ cell.Row ] [ cell.Column ] = copy;
                } ) );

            return gameBoardCopy;
        }

        public static Gameboard ResetMoveRatings( Gameboard gameBoard )
        {
            var gameBoardCopy = Gameboard.Empty;

            GetFlatGameboard( gameBoard )
                .ForEach( cell =>
                {
                    cell.IsHighestScoring = false;
                    cell.IsHit = false;
                    cell.IsTarget = false;

                    gameBoardCopy.Positions [ cell.Row ] [ cell.Column ] = cell;
                } );

            return gameBoardCopy;
        }

        private static Gameboard CopyGameboard( Gameboard gameBoard )
        {
            var gameBoardCopy = Gameboard.Empty;

            GetFlatGameboard( gameBoard )
                .ForEach( cell => gameBoardCopy.Positions [ cell.Row ] [ cell.Column ] = CopyCell( cell ) );

            return gameBoardCopy;
        }

        public static int GetPositionDistance( Move move, int row, int col )
        {
            var xDiff = Math.Abs( row - move.Row );
            var yDiff = Math.Abs( col - move.Column );

            return Math.Max( xDiff, yDiff );
        }

        public static Gameboard RecordMove( Move move, Gameboard gameBoard )
        {
            var gameBoardCopy = CopyGameboard( gameBoard );

            gameBoardCopy.Positions [ move.Row ] [ move.Column ].PlayerNumber = move.PlayerNumber;

            move.Captures.ForEach( cell =>
            {
                cell.PlayerNumber = move.PlayerNumber;
                cell.Distance = GetPositionDistance( move, cell.Row, cell.Column );
                cell.IsHit = true;
                gameBoardCopy.Positions [ cell.Row ] [ cell.Column ] = cell;

            } );

            return gameBoardCopy;
        }

        public static Gameboard MapNextMoves( List<Cell> moves, Gameboard gameBoard )
        {
            var resetGameBoard = ResetMoveRatings( gameBoard );

            moves.ForEach( move =>
                resetGameBoard.Positions [ move.Row ] [ move.Column ] = move );

            return resetGameBoard;
        }

        public static List<Cell> GetPlayerCells( int playerNumber, Gameboard gameBoard )
        {
            return GetFlatGameboard( gameBoard )
                .Where( c => c.PlayerNumber == playerNumber )
                .ToList( );
        }

        public static List<Cell> GetFlatGameboard( Gameboard gameBoard )
        {
            return gameBoard.Positions
                .SelectMany( row =>
                    row.Select( CopyCell ) )
                       .ToList( );
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

        public static List<Cell> GetAdjacentCells( Cell cell, Gameboard gameBoard )
        {
            var above = TryGetCell( cell.Row - 1, cell.Column, gameBoard );
            var aboveRight = TryGetCell( cell.Row - 1, cell.Column + 1, gameBoard );
            var aboveLeft = TryGetCell( cell.Row - 1, cell.Column - 1, gameBoard );
            var left = TryGetCell( cell.Row, cell.Column - 1, gameBoard );
            var right = TryGetCell( cell.Row, cell.Column + 1, gameBoard );
            var below = TryGetCell( cell.Row + 1, cell.Column, gameBoard );
            var belowLeft = TryGetCell( cell.Row + 1, cell.Column - 1, gameBoard );
            var belowRight = TryGetCell( cell.Row + 1, cell.Column + 1, gameBoard );

            return new [] { above, aboveLeft, aboveRight, left, right, below, belowLeft, belowRight }
                .Where( c => c != null )
                .ToList( );
        }

        public static List<Cell> GetOpenAdjacentCells( Cell cell, Gameboard gameBoard )
        {
            return GetAdjacentCells( cell, gameBoard )
                .Where( c => c.PlayerNumber == 0 )
                .ToList( );
        }


    }
}

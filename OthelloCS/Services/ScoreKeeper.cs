using OthelloCS.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace OthelloCS.Services
{
    public static class ScoreKeeper
    {
        public static int GetScoreForPlayer( int playerNumber, Gameboard gameBoard )
        {
            return BoardManager.GetFlatGameboard( gameBoard )
                .Where( c => c.PlayerNumber == playerNumber )
                .Count();
        }

        public static int GetHitDistance( Move move, int row, int col )
        {
            var xDiff = Math.Abs( row - move.Row );
            var yDiff = Math.Abs( col - move.Column);

            return Math.Max( xDiff, yDiff );
        }

        public static CellEvaluationResult EvaluateCell( Cell cell, int currentPlayerNumber )
        {
            return new CellEvaluationResult
            {
                IsEmptyPosition = cell != null && cell?.PlayerNumber == 0,
                IsOpponentPosition = cell != null && cell?.PlayerNumber != 0 && cell?.PlayerNumber != currentPlayerNumber,
                IsInvalidPosition = cell == null
            };
        }

        public static List<Cell> GetMoveCaptures( int startingRow, int startingColumn, int playerNumber, Gameboard gameBoard )
        {
            var captures = new List<Cell>( );
            for ( int rowIncrement = -1; rowIncrement <= 1; rowIncrement++ )
                for ( int columnIncerment = -1; columnIncerment <= 1; columnIncerment++ )
                {
                    if ( rowIncrement == 0 && columnIncerment == 0 )
                        continue;
                    else
                        captures.AddRange( GetDirectionalCaptures( startingRow + rowIncrement, startingColumn + columnIncerment, rowIncrement, columnIncerment, playerNumber, gameBoard ) );
                }
            return captures;
        }

        public static List<Cell> GetDirectionalCaptures(int row, int col, int rowIncrement, int columnIncrement, int playerNumber, Gameboard gameBoard)
        {
            Func<int, int, List<Cell>> GetCapturesRecursive = null;
            var captures = new List<Cell>( );

            GetCapturesRecursive = ( r, c ) =>
            {
                var cell = BoardManager.TryGetCell( r, c, gameBoard );
                var evaluationResult = EvaluateCell( cell, playerNumber );

                if ( evaluationResult.IsEmptyPosition  || evaluationResult.IsInvalidPosition )
                {
                    return new List<Cell>( );
                }
                else if ( evaluationResult.IsOpponentPosition )
                {
                    captures.Add( cell );
                    return GetCapturesRecursive( r + rowIncrement, c + columnIncrement );
                }
                else
                {
                    return captures;
                }
            };

            return GetCapturesRecursive( row, col );
        }
    }

    public class CellEvaluationResult
    {
        public bool IsEmptyPosition { get; set; }
        public bool IsOpponentPosition { get; set; }
        public bool IsInvalidPosition { get; set; }
    }
}
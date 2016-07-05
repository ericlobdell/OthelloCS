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
                .Count( );
        }

        public static int GetWinningPlayerNumber(Gameboard gameBoard)
        {
            var positions = BoardManager.GetFlatGameboard( gameBoard );
            var playerOneScore = positions.Where( p => p.PlayerNumber == 1 ).Count();
            var playerTwoScore = positions.Where( p => p.PlayerNumber == 2 ).Count();

            if ( playerOneScore == playerTwoScore )
                return 0;
            else
                return playerOneScore > playerTwoScore ? 1 : 2;

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

        public static List<Cell> GetMoveCaptures(int startingRow, int startingColumn, int playerNumber, Gameboard gameBoard )
        {
            var gameBoardCopy = BoardManager.CopyGameboard( gameBoard );
            var captures = new List<Cell>( );

            for ( int rowIncrement = -1; rowIncrement <= 1; rowIncrement++ )
            {
                for ( int columnIncerment = -1; columnIncerment <= 1; columnIncerment++ )
                {
                    if ( rowIncrement == 0 && columnIncerment == 0 )
                        continue;
                    else
                        captures.AddRange( 
                            GetDirectionalCaptures( 
                                startingRow + rowIncrement,
                                startingColumn + columnIncerment, 
                                rowIncrement, 
                                columnIncerment, 
                                playerNumber, 
                                gameBoardCopy
                            ) 
                        );
                }
            }

            return captures;
        }

        public static List<Cell> GetDirectionalCaptures( int row, int col, int rowIncrement, int columnIncrement, int playerNumber, Gameboard gameBoard )
        {
            Func<int, int, List<Cell>> GetCapturesRecursive = null;
            var captures = new List<Cell>( );

            GetCapturesRecursive = ( r, c ) =>
            {
                var cell = BoardManager.TryGetCell( r, c, gameBoard );
                var evaluation = EvaluateCell( cell, playerNumber );

                if ( evaluation.IsEmptyPosition || evaluation.IsInvalidPosition )
                {
                    return new List<Cell>( );
                }
                else if ( evaluation.IsOpponentPosition )
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

        public static List<Cell> GetNextMovesForPlayer( int playerNumber, Gameboard gameBoard )
        {
            var potentialNextMoves = new List<Cell>( );
            var highestMoveScore = 0;
            var opponent = playerNumber == 1 ? 2 : 1;

            BoardManager.GetPlayerCells( opponent, gameBoard )
                .ForEach( opponentCell =>
                {
                    BoardManager.GetOpenAdjacentCells( opponentCell, gameBoard )
                        .ForEach( adjacentCell =>
                        {
                            var captures = GetMoveCaptures( adjacentCell.Row, adjacentCell.Column, playerNumber, gameBoard );
                            var pointsEarned = captures.Count;

                            if ( pointsEarned > 0 )
                            {
                                adjacentCell.IsTarget = true;
                                adjacentCell.PointValue = pointsEarned;
                                potentialNextMoves.Add( adjacentCell );
                            }

                            highestMoveScore = pointsEarned > highestMoveScore ? pointsEarned : highestMoveScore;

                        } );

                } );

            potentialNextMoves.ForEach( position =>
                position.IsHighestScoring = position.PointValue == highestMoveScore );

            return potentialNextMoves;
        }

        public static bool IsEndOfGame( Gameboard gameBoard )
        {
            var playerOneMoves = GetNextMovesForPlayer( 1, gameBoard );
            var playerTwoMoves = GetNextMovesForPlayer( 2, gameBoard );

            return playerOneMoves.Count == 0 && playerTwoMoves.Count == 0;
        }
    }

    public class CellEvaluationResult
    {
        public bool IsEmptyPosition { get; set; }
        public bool IsOpponentPosition { get; set; }
        public bool IsInvalidPosition { get; set; }
    }
}
using OthelloCS.Models;
using System;

namespace OthelloCS.Interfaces
{
    public interface IGameModeStrategy
    {
        void HandleEndOfGame( );
        MoveResult OnMove( Move move, Guid matchId, Gameboard gameBoard );
    }
}

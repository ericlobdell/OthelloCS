using OthelloCS.Models;

namespace OthelloCS.Interfaces
{
    public interface IGameModeStrategy
    {
        void HandleEndOfGame( );
        MoveResult OnMoveCompleted( Move move, Gameboard gameBoard );
    }

    
}

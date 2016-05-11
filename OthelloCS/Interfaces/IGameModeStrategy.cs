using OthelloCS.Models;

namespace OthelloCS.Interfaces
{
    public interface IGameModeStrategy
    {
        void HandleEndOfGame( );
        MoveResult OnMove( MoveRequest action );
    }
}

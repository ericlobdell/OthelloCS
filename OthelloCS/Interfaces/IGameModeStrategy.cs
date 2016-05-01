using OthelloCS.Models;

namespace OthelloCS.Interfaces
{
    public interface IGameModeStrategy
    {
        void HandleEndOfGame( );
        MoveResult OnMove( Move move );
    }

    public class MoveResult
    {
        public int CurrentPlayer { get; }
        public Gameboard Gameboard { get; }

        public MoveResult( int playerNumber, Gameboard gameBoard )
        {
            CurrentPlayer = playerNumber;
            Gameboard = gameBoard;
        }
    }
}

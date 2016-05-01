using OthelloCS.Interfaces;

namespace OthelloCS.Models
{
    public class Game
    {
        public Game( IGameModeStrategy strategy, Gameboard gameBoard )
        {


        }
    }

    public enum GameMode
    {
        OnePlayer,
        TwoPlayer,
        Training
    }
}

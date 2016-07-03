using OthelloCS.Services;

namespace OthelloCS.Models
{
    public class BaseGameModeStrategy
    {
        public int GetOtherPlayerNumber( int playerNumber )
        {
            return playerNumber == 1 ? 2 : 1;
        }
    }
}

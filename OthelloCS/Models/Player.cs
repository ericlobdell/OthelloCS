namespace OthelloCS.Models
{
    public class Player
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public int Score { get; set; }

        public Player( int number, string name = "" )
        {
            Number = number;
            Name = name == "" ? $"Player {number}" : name;
            Score = 2;
        }
    }

    public enum PlayerNumber
    {
        PlayerOne = 1,
        PlayerTwo = 2
    }
}

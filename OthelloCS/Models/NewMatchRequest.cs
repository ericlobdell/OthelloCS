namespace OthelloCS.Models
{
    public class NewMatchRequest
    {
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; }
        public GameMode GameMode { get; set; }
    }
}
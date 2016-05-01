namespace OthelloCS.Models
{
    public class Cell
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int PlayerNumber { get; set; }
        public bool IsTarget { get; set; }
        public bool IsHit { get; set; }
        public bool IsHighestScoring { get; set; }
        public int Distance { get; set; }
        public int PointValue { get; set; }
    }
}

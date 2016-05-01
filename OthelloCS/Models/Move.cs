namespace OthelloCS.Models
{
    public class Move
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int PlayerNumber { get; set; }
        public int PointValue { get; set; }
        public bool IsHighestScoring { get; set; }
    }
}

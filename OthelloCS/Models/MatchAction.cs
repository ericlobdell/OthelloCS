using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloCS.Models
{
    public class MatchAction
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public int PlayerNumber { get; set; }
        public Gameboard Gameboard { get; set; }
        public Guid MatchId { get; set; }
        public GameMode GameMode { get; set; }

    }
}

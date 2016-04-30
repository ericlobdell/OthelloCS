using OthelloCS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloCS.Models
{
    public class Game
    {
        public Game( GameMode mode, Gameboard gameBoard, ScoreKeeper scoreKeeper )
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

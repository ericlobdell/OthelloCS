using OthelloCS.Interfaces;
using OthelloCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloCS.Services
{
    public class GameModeStrategyFactory
    {
        public static IGameModeStrategy Create( GameMode mode )
        {
            IGameModeStrategy strategy;

            switch ( mode )
            {
                case GameMode.OnePlayer:
                    strategy = new SinglePlayerGameModeStrategy( );
                    break;
                case GameMode.TwoPlayer:
                    throw new NotImplementedException( );
                    break;
                case GameMode.Training:
                    throw new NotImplementedException( );
                    break;
                default:
                    throw new ArgumentException( "Unrecognized GameMode" );

            }

            return strategy;
        }
    }
}

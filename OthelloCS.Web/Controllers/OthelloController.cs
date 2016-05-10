using OthelloCS.Interfaces;
using OthelloCS.Models;
using OthelloCS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OthelloCS.Web.Controllers
{
    public class OthelloController : ApiController
    {
        [Route("api/othello/new/{mode:int}")]
        public NewMatchResponse Get( GameMode mode = GameMode.OnePlayer )
        {
            return new NewMatchResponse
            {
                Gameboard = new Gameboard( ),
                MatchId = Guid.NewGuid( ),
                CurrentPlayer = Player.PlayerOne,
                GameMode = mode
            };
        }

        [Route("api/othello/match")]
        [HttpPost]
        public MoveResult RecordMove( MatchAction action )
        {
            var strategy = ResolveGameModeStrategy( action.GameMode );
            var moveResult = strategy.OnMove( action );

            return moveResult;
        }

        private IGameModeStrategy ResolveGameModeStrategy( GameMode gameMode )
        {

            //if ( gameMode == GameMode.OnePlayer )
            //{
            //    return new SinglePlayerGameModeStrategy( );
            //}

            return new TwoPlayerGameModeStrategy( );

        }
    }

    public enum Player
    {
        PlayerOne = 1,
        PlayerTwo = 2
    }

    public class NewMatchResponse
    {
        public Player CurrentPlayer { get; set; }
        public Gameboard Gameboard { get; set; }
        public Guid MatchId { get; set; }
        public GameMode GameMode { get; set; }
    }

    

}

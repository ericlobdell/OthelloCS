using OthelloCS.Interfaces;
using OthelloCS.Models;
using OthelloCS.Services;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace OthelloCS.Web.Controllers
{
    public class OthelloController : ApiController
    {
        [HttpPost]
        [Route("api/othello/new")]
        public NewMatchResponse PostNew( NewMatchRequest newMatchRequest )
        {
            return new NewMatchResponse
            {
                Gameboard = new Gameboard( ),
                MatchId = Guid.NewGuid( ),
                CurrentPlayer = PlayerNumber.PlayerOne,
                GameMode = newMatchRequest.GameMode,
                Players = new List<Player>
                {
                    new Player(1, newMatchRequest.PlayerOneName),
                    new Player(2, newMatchRequest.PlayerTwoName)
                }
            };
        }

        [HttpPost]
        [Route( "api/othello/move" )]
        public MoveResponse PostMove( MoveRequest moveRequest )
        {
            var strategy = ResolveGameModeStrategy( moveRequest.GameMode );
            var moveResult = strategy.OnMove( moveRequest );

            moveRequest.Players.ForEach( p => p.Score =
                ScoreKeeper.GetScoreForPlayer( p.Number, moveResult.Gameboard )
            );

            return new MoveResponse
            {
                Result = moveResult,
                Players = moveRequest.Players,
                IsEndOfGame = false
            };
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

    

    

    

    



}

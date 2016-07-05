using OthelloCS.Interfaces;
using OthelloCS.Models;
using OthelloCS.Services;
using OthelloCS.Strategies;
using OthelloCS.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var move = new Move( moveRequest.Row, moveRequest.Column, moveRequest.PlayerNumber );
            var moveResult = strategy.OnMove( move, moveRequest.MatchId, moveRequest.Gameboard );

            moveRequest.Players.ForEach( p => 
                p.Score = ScoreKeeper.GetScoreForPlayer( p.Number, moveResult.Gameboard )
            );

            var response = new MoveResponse
            {
                Result = moveResult,
                Players = moveRequest.Players
            };

            if ( moveResult.IsEndOfMatch )
                response.Winner = moveRequest
                    .Players
                    .OrderByDescending( p => p.Score )
                    .First( );

            return response;
        }

        private IGameModeStrategy ResolveGameModeStrategy( GameMode gameMode )
        {
            if ( gameMode == GameMode.OnePlayer )
                return new SinglePlayerGameModeStrategy( );
            else
                return new TwoPlayerGameModeStrategy( );
        }
    }

    

    

    

    



}

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
        public MatchResponse Get( GameMode mode = GameMode.OnePlayer )
        {
            return new MatchResponse
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
            var move = ScoreKeeper.MakeMove( action.Row, action.Column, action.PlayerNumber, action.Gameboard );
            var gb = BoardManager.RecordMove( move, action.Gameboard );

            var moveResult = strategy.OnMoveCompleted( move, gb, action.MatchId, move.Captures );

            var nextMoves = ScoreKeeper.GetNextMovesForPlayer( moveResult.CurrentPlayer, gb );
            moveResult.Gameboard = BoardManager.MapNextMoves( nextMoves, gb );

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

    public class MatchResponse
    {
        public Player CurrentPlayer { get; set; }
        public Gameboard Gameboard { get; set; }
        public Guid MatchId { get; set; }
        public GameMode GameMode { get; set; }
    }

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

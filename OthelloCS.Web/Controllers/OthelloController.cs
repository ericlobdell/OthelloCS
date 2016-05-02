using OthelloCS.Models;
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
        [Route("api/othello/new")]
        public NewMatchResponse Get()
        {
            return new NewMatchResponse
            {
                Gameboard = new Gameboard( ),
                MatchId = Guid.NewGuid( )
            };
        }
    }

    public class NewMatchResponse
    {
        public Gameboard Gameboard { get; set; }
        public Guid MatchId { get; set; }
    }
}

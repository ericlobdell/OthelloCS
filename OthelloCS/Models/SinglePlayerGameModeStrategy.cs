﻿using OthelloCS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloCS.Models
{
    public class SinglePlayerGameModeStrategy : IGameModeStrategy
    {

        public void HandleEndOfGame( )
        {
            throw new NotImplementedException( );
        }

        public MoveResult OnMove( MatchAction action )
        {
            throw new NotImplementedException( );
        }
    }
}

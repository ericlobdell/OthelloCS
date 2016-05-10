using OthelloCS.Models;
using System;
using System.Collections.Generic;

namespace OthelloCS.Interfaces
{
    public interface IGameModeStrategy
    {
        void HandleEndOfGame( );
        MoveResult OnMove( MatchAction action );
    }
}

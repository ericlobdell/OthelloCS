﻿using OthelloCS.Models;
using System;
using System.Collections.Generic;

namespace OthelloCS.Interfaces
{
    public interface IGameModeStrategy
    {
        void HandleEndOfGame( );
        MoveResult OnMoveCompleted( Move move, Gameboard gameBoard, Guid matchId, List<Cell> captures );
    }
}

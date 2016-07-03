using OthelloCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OthelloCS.Services
{
    public static class OthelloAI
    {
        public static bool IsCorner(Cell position )
        {
            return ( position.Row == 0 || position.Row == 7 ) && 
                (position.Column == 0 || position.Column == 7);
        }

        public static bool IsEdge( Cell position )
        {
            return ( position.Row == 0 || position.Row == 7 ) ||
                ( position.Column == 0 || position.Column == 7 );
        }

        public static Cell GetHighestScoringPosition( List<Cell> positions )
        {
            return positions.OrderByDescending( p => p.PointValue ).First( );
        }

        public static MoveSelection
 MakeMove(List<Cell> positions)
        {
            var cornerPositions = positions.Where( IsCorner ).ToList();
            var edgePositions = positions.Where( IsEdge ).ToList( );

            if (cornerPositions.Any())
            {
                return new MoveSelection

                {
                    Position = GetHighestScoringPosition( cornerPositions ),
                    Criteria = MoveSelectionCriteria.CornerPositionAvailable
                };
            }

            else if (edgePositions.Any())
            {
                return new MoveSelection

                {
                    Position = GetHighestScoringPosition( edgePositions ),
                    Criteria = MoveSelectionCriteria.EdgePositionAvailable
                };
            }

            else
            {
                return new MoveSelection

                {
                    Position = GetHighestScoringPosition( positions ),
                    Criteria = MoveSelectionCriteria.HighestScoringPosition
                };
            }

        }
    }

    public struct MoveSelection
    {
        public string Criteria { get; set; }
        public Cell Position { get; set; }
    }

    public static class MoveSelectionCriteria
    {
        public static string CornerPositionAvailable { get; } = "Corner position available.";
        public static string EdgePositionAvailable { get; } = "Edge position available.";
        public static string HighestScoringPosition { get; } = "Highest scoring position.";
    }
}

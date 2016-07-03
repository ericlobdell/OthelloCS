using OthelloCS.Models;
using OthelloCS.Services;
using System;
using System.Collections.Generic;
using Xunit;

namespace OthelloCS.Tests
{
    public class OthelloTests
    {
        [Fact]
        public void IsCorner_true_when_corner( )
        {
            var position = new Cell { Row = 0, Column = 7 };
            Assert.True( OthelloAI.IsCorner( position ) );
        }

        [Fact]
        public void IsCorner_false_when_not_corner( )
        {
            var position = new Cell { Row = 5, Column = 7 };
            Assert.False( OthelloAI.IsCorner( position ) );
        }

        [Fact]
        public void IsEdge_true_when_edge( )
        {
            var position = new Cell { Row = 0, Column = 7 };
            Assert.True( OthelloAI.IsEdge( position ) );
        }

        [Fact]
        public void IsEdge_false_when_not_edge( )
        {
            var position = new Cell { Row = 5, Column = 3 };
            Assert.False( OthelloAI.IsEdge( position ) );
        }

        [Fact]
        public void GetHighestScoringPosition_returns_cell_with_highest_point_value( )
        {
            var positions = new List<Cell>
            {
                new Cell { PointValue = 2 },
                new Cell { PointValue = 1 },
                new Cell { PointValue = 7 },
                new Cell { PointValue = 5 }
            };

            var sut = OthelloAI.GetHighestScoringPosition( positions );

            Assert.Equal( 7, sut.PointValue );
        }

        [Fact]
        public void GetHighestScoringPosition_throws_when_empty_list( )
        {
            var positions = new List<Cell>( );

            var ex = Assert.Throws<InvalidOperationException>( ( ) => OthelloAI.GetHighestScoringPosition( positions ) );
        }

        [Fact]
        public void MakeMove_choses_corner_position_even_if_not_highest_scoring( )
        {
            var positions = new List<Cell>
            {
                new Cell { Row = 0, Column = 0, PointValue = 2 }, // corner
                new Cell { Row = 0, Column = 3, PointValue = 1 },
                new Cell { Row = 3, Column = 3, PointValue = 7 },
                new Cell { Row = 7, Column = 4, PointValue = 5 }
            };

            var sut = OthelloAI.MakeMove( positions );

            Assert.Equal( 2, sut.Position.PointValue );
            Assert.Equal( MoveSelectionCriteria.CornerPositionAvailable, sut.Criteria );

        }

        [Fact]
        public void MakeMove_choses_corner_position_with_highest_score_if_multiple_available( )
        {
            var positions = new List<Cell>
            {
                new Cell { Row = 0, Column = 0, PointValue = 2 }, // corner
                new Cell { Row = 0, Column = 7, PointValue = 4 }, // corner
                new Cell { Row = 3, Column = 3, PointValue = 7 },
                new Cell { Row = 7, Column = 4, PointValue = 5 }
            };

            var sut = OthelloAI.MakeMove( positions );

            Assert.Equal( 4, sut.Position.PointValue );
            Assert.Equal( MoveSelectionCriteria.CornerPositionAvailable, sut.Criteria );

        }

        [Fact]
        public void MakeMove_choses_edge_position_if_no_corner_even_if_not_highest_scoring( )
        {
            var positions = new List<Cell>
            {
                new Cell { Row = 2, Column = 5, PointValue = 2 },
                new Cell { Row = 0, Column = 3, PointValue = 1 }, // edge
                new Cell { Row = 3, Column = 3, PointValue = 7 },
                new Cell { Row = 6, Column = 4, PointValue = 5 }
            };

            var sut = OthelloAI.MakeMove( positions );

            Assert.Equal( 1, sut.Position.PointValue );
            Assert.Equal( MoveSelectionCriteria.EdgePositionAvailable, sut.Criteria );
        }

        [Fact]
        public void MakeMove_choses_edge_position_with_highest_score_if_multiple_available( )
        {
            var positions = new List<Cell>
            {
                new Cell { Row = 7, Column = 5, PointValue = 2 }, // edge
                new Cell { Row = 0, Column = 3, PointValue = 1 }, // edge
                new Cell { Row = 3, Column = 3, PointValue = 7 },
                new Cell { Row = 6, Column = 4, PointValue = 5 }
            };

            var sut = OthelloAI.MakeMove( positions );

            Assert.Equal( 2, sut.Position.PointValue );
            Assert.Equal( MoveSelectionCriteria.EdgePositionAvailable, sut.Criteria );
        }

        [Fact]
        public void MakeMove_choses_position_with_highest_score_if_no_corner_or_edge( )
        {
            var positions = new List<Cell>
            {
                new Cell { Row = 2, Column = 5, PointValue = 2 },
                new Cell { Row = 4, Column = 3, PointValue = 1 },
                new Cell { Row = 3, Column = 3, PointValue = 7 },
                new Cell { Row = 6, Column = 4, PointValue = 5 }
            };

            var sut = OthelloAI.MakeMove( positions );

            Assert.Equal( 7, sut.Position.PointValue );
            Assert.Equal( MoveSelectionCriteria.HighestScoringPosition, sut.Criteria );
        }


    }
}

using OthelloCS.Models;
using Xunit;

namespace OthelloCS.Tests
{
    public class GameboardTests
    {
        [Fact]
        public void Gameboard_should_consist_of_8_rows_of_8_cells()
        {
            var sut = new Gameboard( );

            Assert.Equal(8, sut.Positions.Count );

            sut.Positions.ForEach( cells => Assert.Equal(8, cells.Count ));
        }

        [Theory]
        [InlineData( 2, 4, 0 )]
        [InlineData( 3, 3, 1 )]
        [InlineData( 4, 4, 1 )]
        [InlineData( 3, 4, 2 )]
        [InlineData( 4, 3, 2 )]
        [InlineData( 0, 0, 0 )]
        [InlineData( 5, 4, 0 )]
        [InlineData( 6, 4, 0 )]
        public void Initial_Gameboard_should_populate_initial_moves(int row, int col, int player)
        {
            var sut = new Gameboard( );

            Assert.Equal(sut.Positions [ row] [ col ].PlayerNumber, player );
        }

        [Theory]
        [InlineData(2, 4, true)]
        [InlineData( 4, 2, true )]
        [InlineData( 3, 5, true )]
        [InlineData( 5, 3, true )]
        [InlineData( 2, 2, false )]
        [InlineData( 4, 4, false )]
        public void Initial_Gameboard_should_populate_mark_initial_moves_as_targets(int row, int col, bool expected )
        {
            var sut = new Gameboard( );

            Assert.Equal( sut.Positions [ row ] [ col ].IsTarget, expected );
        }



    }
}

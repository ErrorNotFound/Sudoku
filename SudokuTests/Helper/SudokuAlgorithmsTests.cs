using Sudoku.Game.Helper;
using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuTests.Helper
{
    internal class SudokuAlgorithmsTests
    {
        [Test]
        public void SolveBoard_UnsolvableSudoku_ReturnsFalse()
        {
            var unsolvable = "123000000000040000000000004000000000000000000000000000000000000000000000000000000";
            var board = new SudokuBoard(unsolvable);

            Assert.That(SudokuAlgorithms.SolveBoard(board), Is.False);
        }

        [Test]
        public void SolveBoard_SolvableSudoku_ReturnsTrue()
        {
            var solvable = "400070008057000390800903004006704500500000001002501800200807009018000730700010005";
            var board = new SudokuBoard(solvable);

            Assert.That(SudokuAlgorithms.SolveBoard(board), Is.True);
        }
    }
}

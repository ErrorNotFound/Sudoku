using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Validation
{
    public class AllSquaresSetValidator : IValidator
    {
        public bool IsValid(SudokuBoard board, out List<SudokuSquare> invalidSquares)
        {
            invalidSquares = board.Squares.Where(x => x.Number == null).ToList();

            return !invalidSquares.Any();
        }
    }
}

using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Validation
{
    public class VerticalValidator : IValidator
    {
        public bool IsValid(SudokuBoard board, out List<SudokuSquare> invalidSquares)
        {
            invalidSquares = new List<SudokuSquare>();

            var columns = board.GetColumns();

            foreach (var col in columns)
            {
                foreach (var square in col.Where(x => x.Number != null))
                {
                    if (col.Count(x => x.Number == square.Number) > 1)
                    {
                        invalidSquares.Add(square);
                    }
                }
            }

            return !invalidSquares.Any();
        }
    }
}

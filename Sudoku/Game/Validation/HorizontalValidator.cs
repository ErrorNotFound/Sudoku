using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Sudoku.Game.Validation
{
    /// <summary>
    /// Validates that no number is used twice in a row
    /// </summary>
    public class HorizontalValidator : IValidator
    {
        public bool IsValid(SudokuBoard board, out List<SudokuSquare> invalidSquares)
        {
            invalidSquares = new List<SudokuSquare>();

            var rows = board.GetRows();

            foreach (var row in rows)
            {
                foreach (var square in row.Where(x => x.Number != null))
                {
                    if (row.Count(x => x.Number == square.Number) > 1)
                    {
                        invalidSquares.Add(square);
                    }
                }
            }

            return !invalidSquares.Any();
        }
    }
}

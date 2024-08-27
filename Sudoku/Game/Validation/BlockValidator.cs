using Sudoku.Game.Helper;
using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Validation
{
    public class BlockValidator : IValidator
    {
        /// <summary>
        /// Validates that no number is identical within a block
        /// </summary>
        /// <param name="board"></param>
        /// <param name="invalidSquares"></param>
        /// <returns></returns>
        public bool IsValid(SudokuBoard board, out List<SudokuSquare> invalidSquares)
        {
            invalidSquares = new List<SudokuSquare>();

            var blocks = board.GetBlocks();

            foreach (var block in blocks)
            {
                foreach (var square in block.Where(x => x.Number != null))
                {
                    if(block.Count(x => x.Number == square.Number) > 1)
                    {
                        invalidSquares.Add(square);
                    }
                }
            }

            return !invalidSquares.Any();
        }
    }
}

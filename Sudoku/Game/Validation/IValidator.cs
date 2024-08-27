using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Validation
{
    public interface IValidator
    {
        bool IsValid(SudokuBoard board, out List<SudokuSquare> invalidSquares);
    }
}

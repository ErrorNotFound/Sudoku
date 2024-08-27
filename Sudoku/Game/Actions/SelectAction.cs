using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Actions
{
    public class SelectAction : IAction
    {
        private readonly SudokuSquare sourceSquare;
        private readonly IEnumerable<SudokuSquare> squares;

        public SelectAction(SudokuSquare clicked, IEnumerable<SudokuSquare> targetSquares)
        {
            sourceSquare = clicked;
            squares = targetSquares;   
        }

        public void Do()
        {
            var target = sourceSquare.Number;

            squares.Where(x => x.Number == target).All(x => { x.IsSelected = true; return true; });
        }

        public bool IsValidOperation()
        {
            return sourceSquare != null && squares != null;
        }
    }
}

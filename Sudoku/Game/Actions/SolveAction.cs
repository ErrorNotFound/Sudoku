using Sudoku.Game.Helper;
using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Actions
{
    public class SolveAction : AbstractUndoableActionBase
    {
        private SudokuBoard board;

        public SolveAction(SudokuBoard board) 
            :base(board.Squares)
        {
            this.board = board;
        }

        protected override void OnDo()
        {
            SudokuAlgorithms.SolveBoard(board);
        }

        protected override bool OnIsValidOperation()
        {
            return true;
        }
    }
}

using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Actions
{
    public class ChangeValueAction : AbstractUndoableActionBase
    {
        private int? newValue;

        public ChangeValueAction(IEnumerable<SudokuSquare> squares, int value)
            :base(squares)
        {
            newValue = value;
        }

        protected override void OnDo()
        {
            foreach (var square in HistoryNumbers.Keys)
            {
                square.Number = newValue;
                // clear all notes wenn setting a value
                square.TopNotes.Clear();
                square.CenterNotes.Clear();
            }
        }

        protected override bool OnIsValidOperation()
        {
            return newValue > 0 && newValue < 10;
        }
    }
}

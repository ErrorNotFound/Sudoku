using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Actions
{
    public class DeleteAction : AbstractUndoableActionBase
    {
        public DeleteAction(IEnumerable<SudokuSquare> squares)
            : base(squares)
        {
        }

        protected override void OnDo()
        {
            var squares = HistoryNumbers.Keys.ToList();

            // if there is any top note, we only want to delete top notes
            if (squares.Any(x => x.TopNotes.Any()))
            {
                squares.ForEach(squares => {  squares.TopNotes.Clear(); });
            }
            // if there is any center note, we only want to delete top notes
            else if (squares.Any(x => x.CenterNotes.Any()))
            {
                squares.ForEach(squares => { squares.CenterNotes.Clear(); });
            }
            else
            {
                squares.ForEach(squares => { squares.Number = null; });
            }
        }

        protected override bool OnIsValidOperation()
        {
            return true;
        }
    }
}

using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Actions
{
    public class ChangeTopNotesAction : AbstractUndoableActionBase
    {
        private int? newNote;

        public ChangeTopNotesAction(IEnumerable<SudokuSquare> squares, int? note)
            :base(squares)
        {
            newNote = note;
        }

        protected override void OnDo()
        {
            if (newNote == null) // Delete all notes
            {
                foreach(var squares in HistoryTopNotes.Keys)
                {
                    squares.TopNotes.Clear();
                }
            }
            else // Handle a value
            {
                var unsetSquares = HistoryTopNotes.Where(x => x.Key.Number == null);

                if (unsetSquares.All(x => x.Value.Contains((int)newNote)))
                {
                    // if all square contain the note, we delete it from the squares
                    unsetSquares.Where(x => x.Key.TopNotes.Contains((int)newNote)).ToList().ForEach(p => p.Key.TopNotes.Remove((int)newNote));

                }
                else
                {
                    // we add it to all remaining squares
                    unsetSquares.Where(x => !x.Key.TopNotes.Contains((int)newNote)).ToList().ForEach(p => p.Key.TopNotes.Add((int)newNote));
                }
            }
        }

        protected override bool OnIsValidOperation()
        {
            return HistoryTopNotes.Any() && (newNote == null || (newNote > 0 && newNote < 10));
        }
    }
}

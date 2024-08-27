using Sudoku.Game.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.Game.Actions
{
    public class ChangeCenterNotesAction : AbstractUndoableActionBase
    {
        private int? newNote;

        public ChangeCenterNotesAction(IEnumerable<SudokuSquare> squares, int? note)
            : base(squares)
        {
            newNote = note;
        }

        protected override void OnDo()
        {
            if (newNote == null) // Delete all notes
            {
                foreach (var squares in HistoryCenterNotes.Keys)
                {
                    squares.CenterNotes.Clear();
                }
            }
            else // Handle a value
            {
                var unsetSquares = HistoryCenterNotes.Where(x => x.Key.Number == null);

                if (unsetSquares.All(x => x.Value.Contains((int)newNote)))
                {
                    // if all square contain the note, we delete it from the squares
                    unsetSquares.Where(x => x.Key.CenterNotes.Contains((int)newNote)).ToList().ForEach(p => p.Key.CenterNotes.Remove((int)newNote));

                }
                else
                {
                    // we add it to all remaining squares
                    unsetSquares.Where(x => !x.Key.CenterNotes.Contains((int)newNote)).ToList().ForEach(p => p.Key.CenterNotes.Add((int)newNote));
                }
            }
        }

        protected override bool OnIsValidOperation()
        {
            return HistoryCenterNotes.Any() && (newNote == null || (newNote > 0 && newNote < 10));
        }
    }
}
